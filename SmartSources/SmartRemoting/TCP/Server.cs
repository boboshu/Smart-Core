using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Smart.TCP
{
    public class Server : Server<Context>
    {
        public Server(ushort port, Handler<Context> handler) : base(port, handler) { }
    }

    public class Server<C> : IDisposable
        where C : Context, new()
    {
        public Action onClientConnected;
        public Action onClientDisconnected;

        private byte _detectDisconnectsCounter;
        private TcpListener _tcpListener;
        public readonly List<Channel<C>> Channels = new List<Channel<C>>();
        public Handler<C> Handler;

        public Server(ushort port, Handler<C> handler = null)
        {
            Handler = handler ?? new Handler<C>();
            Channels = new List<Channel<C>>();
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Start();

            Handler.onCloseChannelInternal = c => { Channels.Remove(c); HandleClientDisconnected(); };
        }

        private void HandleClientConnected()
        {
            if (onClientConnected != null) onClientConnected();
        }

        private void HandleClientDisconnected()
        {
            if (onClientDisconnected != null) onClientDisconnected();
        }

        public void Dispose()
        {
            _tcpListener.Stop();
            _tcpListener = null;
        }

        public void ProcessMessages()
        {
            if (_tcpListener == null) return;

            while (_tcpListener.Pending())
            {
                var socket = _tcpListener.AcceptSocket();
                Channels.Add(new Channel<C>(socket, Handler));
                HandleClientConnected();
            }

            foreach (var channel in Channels)
                channel.ProcessMessages();

            if (_detectDisconnectsCounter++ > 200)
            {
                _detectDisconnectsCounter = 0;
                foreach (var c in Channels.Where(c => c.ConnectionIsLost()).ToArray())
                    Handler.HandleCloseChannel(c);
            }
        }
    }
}
