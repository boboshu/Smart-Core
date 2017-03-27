using System;
using System.Collections.Generic;
using System.IO;

namespace Smart.TCP
{
    public class Handler : Handler<Context>
    {
    }

    public class Handler<C> where C : Context, new()
    {
        private readonly Dictionary<string, Action<BinaryReader, Channel<C>>> _handlers = new Dictionary<string, Action<BinaryReader, Channel<C>>>();

        public Action<Exception> onException;
        public Action<Channel<C>> onCloseChannel;
        internal Action<Channel<C>> onCloseChannelInternal; // to let server have its own handler for it`s channels

        public void Register(string commandName, Action<BinaryReader, Channel<C>> onExecute)
        {
            _handlers.Remove(commandName);
            _handlers.Add(commandName, onExecute);
        }

        public void UnRegister(string commandName)
        {
            _handlers.Remove(commandName);
        }

        public void Execute(BinaryReader br, Channel<C> channel)
        {
            var commandName = br.ReadString();
            Action<BinaryReader, Channel<C>> handler;
            if (_handlers.TryGetValue(commandName, out handler)) handler(br, channel);
        }

        public void HandleException(Exception e)
        {
            if (onException != null) onException(e);
        }

        public void HandleCloseChannel(Channel<C> channel)
        {
            if (onCloseChannel != null) onCloseChannel(channel);
            if (onCloseChannelInternal != null) onCloseChannelInternal(channel);
        }
    }
}
