using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Smart.TCP
{
    public class Channel<C> : IDisposable
        where C : Context, new()
    {
        private readonly MemoryStream _readSequenceBuffer = new MemoryStream();
        private readonly MemoryStream _sendMessageBuffer = new MemoryStream();
        private readonly MemoryStream _readMessageBuffer = new MemoryStream();

        private readonly Byte[] _receiveDataBuffer = new Byte[65536];
        private int _nextMessagePosition;

        private readonly BinaryReader _sequenceReader;
        private readonly BinaryReader _messageReader;
        private readonly BinaryWriter _messageWriter;

        public readonly Socket Socket;
        public readonly Handler<C> Handler;
        public readonly C Context;

        public Channel(Socket socket, Handler<C> handler)
        {
            _sequenceReader = new BinaryReader(_readSequenceBuffer);
            _messageReader = new BinaryReader(_readMessageBuffer);
            _messageWriter = new BinaryWriter(_sendMessageBuffer);

            Context = new C();
            Socket = socket;
            Handler = handler;

            Socket.Blocking = false;
        }

        public void Dispose()
        {
            _readMessageBuffer.Dispose();
            _readSequenceBuffer.Dispose();
            _sendMessageBuffer.Dispose();
        }

        private bool RetrieveStream()
        {
            var lastSequencePosition = (int)_readSequenceBuffer.Position;
            _readSequenceBuffer.Position = _nextMessagePosition;
            var nextMessageSize = _sequenceReader.ReadInt32();

            if (_readSequenceBuffer.Position + nextMessageSize > lastSequencePosition) // can`t read whole message
            {
                _readSequenceBuffer.Position = lastSequencePosition;
                return false;
            }

            _readMessageBuffer.SetLength(nextMessageSize);
            _readMessageBuffer.Position = 0;
            _readMessageBuffer.Write(_readSequenceBuffer.GetBuffer(), (int) _readSequenceBuffer.Position, nextMessageSize);
            _readMessageBuffer.Position = 0;
            _readSequenceBuffer.Position += nextMessageSize;
            _nextMessagePosition = (int) _readSequenceBuffer.Position;

            if (lastSequencePosition == _nextMessagePosition)
            {
                _nextMessagePosition = 0;
                lastSequencePosition = 0;
            }
            _readSequenceBuffer.Position = lastSequencePosition;
            return true;
        }

        public void Send(string commandName, Action<BinaryWriter> commandParameters)
        {
            _sendMessageBuffer.SetLength(0);
            _messageWriter.Write(0);
            _messageWriter.Write(commandName);
            if (commandParameters != null) commandParameters(_messageWriter);
            _sendMessageBuffer.Position = 0;
            _messageWriter.Write((int)(_sendMessageBuffer.Length - 4));

            try
            {
                Socket.Send(_sendMessageBuffer.GetBuffer(), (int)_sendMessageBuffer.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                Handler.HandleException(e);
            }
        }

        public void Terminate()
        {
            Socket.Close();
            Handler.HandleCloseChannel(this);
        }

        public void ProcessMessages()
        {
            if (!Socket.Connected) { Handler.HandleCloseChannel(this); return; } // outer connection breke

            while (Socket.Available > 0)
            {
                var receivedSize = Socket.Receive(_receiveDataBuffer, 0, _receiveDataBuffer.Length, 0);
                _readSequenceBuffer.Write(_receiveDataBuffer, 0, receivedSize);
                while (RetrieveStream())
                    Handler.Execute(_messageReader, this);
            }
        }

        public bool ConnectionIsLost()
        {
            return Socket.Poll(1, SelectMode.SelectRead) && (Socket.Available == 0);
        }

        public string GetRemoteAddress()
        {
            var ipEndPoint = Socket.RemoteEndPoint as IPEndPoint;
            return (ipEndPoint == null) ? "" : ipEndPoint.Address.ToString();
        }
    }
}
