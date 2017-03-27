using System;
using System.Collections.Generic;

namespace Smart.Types
{
    public class PendingCommands
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public class Command
        {
            public float countdown;
            public float duration;
            public Action onExecute;
            public Action<float> onExecuteTimed;
        }

        private readonly List<Command> _commands = new List<Command>();

        //--------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            _commands.Clear();
        }

        public void Add(Action command)
        {
            if (command == null) return;
            _commands.Add(new Command { countdown = 0, duration = 0, onExecute = command });
        }

        public void Add(float duration, Action command)
        {
            if (command == null) return;
            if (duration <= 0.01f) duration = 0;
            _commands.Add(new Command { countdown = duration, duration = duration, onExecute = command });
        }

        public void Add(float duration, Action<float> command)
        {
            if (command == null) return;
            _commands.Add(duration <= 0.01f
                ? new Command {countdown = 0, duration = 0, onExecute = () => command(1)}
                : new Command {countdown = duration, duration = duration, onExecuteTimed = command});
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Update(float deltaTime)
        {
            // 1. Execute commands
            foreach (var command in _commands)
            {
                if (command.countdown <= 0) command.onExecute();
                else
                {
                    if (command.onExecuteTimed == null) command.onExecute();
                    else command.onExecuteTimed(command.countdown / command.duration);
                    command.countdown -= deltaTime;
                }                
            }            

            // 2. Remove finished commands
            _commands.RemoveAll(c => c.countdown <= 0);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
