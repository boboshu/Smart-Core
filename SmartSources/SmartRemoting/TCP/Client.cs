using System;
using System.IO;
using System.Net.Sockets;

namespace Smart.TCP
{
    public class Client : Client<Context>
    {
        public Client(string host, int port, Handler<Context> handler = null) : base(host, port, handler) { }
    }

    public class FailedToConnectException : Exception
    {
    }

    public class Client<C> : IDisposable
        where C : Context, new()
    {
        private readonly TcpClient _tcpClient;
        public readonly Channel<C> Channel;
        public Handler<C> Handler;

        public Client(string host, int port, Handler<C> handler = null)
        {
            Handler = handler ?? new Handler<C>();
            _tcpClient = new TcpClient();

            try
            {
                var result = _tcpClient.BeginConnect(host, port, null, null);
                if (!result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3)))
                    throw new FailedToConnectException();
                _tcpClient.EndConnect(result);
                //_tcpClient.Connect(IPAddress.Parse(host), port);
            }
            catch (Exception e)
            {
                Handler.HandleException(e);
                throw;
            }

            Channel = new Channel<C>(_tcpClient.Client, Handler);
        }

        public bool IsConnected()
        {
            return _tcpClient != null && _tcpClient.Connected;
        }

        public void Dispose()
        {
            if (_tcpClient == null) return;
            if (!_tcpClient.Connected) return;
            _tcpClient.Close();
            if (Channel != null) Channel.Dispose();
        }

        public void Send(string commandName)
        {
            if (Channel == null) return;
            Channel.Send(commandName, null);
        }

        public void Send(string commandName, Action<BinaryWriter> commandParameters)
        {
            if (Channel == null) return;
            Channel.Send(commandName, commandParameters);
        }

        public void ProcessMessages()
        {
            if (Channel == null) return;
            Channel.ProcessMessages();
        }
    }
}
