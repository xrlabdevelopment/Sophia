using System.Collections.Generic;

namespace Sophia.Core
{
    public abstract class InputController
    {
        private readonly List<InputKeyCommand>  input_key_commands = new List<InputKeyCommand>();
        private readonly List<InputAxisCommand> input_axis_commands = new List<InputAxisCommand>();
        private readonly List<InputTouchCommand> input_touch_commands = new List<InputTouchCommand>();

        //--------------------------------------------------------------------------------------
        public abstract void initialize();
        //--------------------------------------------------------------------------------------
        public void shutdown()
        {
            input_key_commands.Clear();
            input_axis_commands.Clear();
            input_touch_commands.Clear();
        }

        //--------------------------------------------------------------------------------------
        public void processInputFor(ICommandReceiver receiver)
        {
            processKeyCommands(input_key_commands.FindAll(command => command.isTriggered()), receiver);
            processAxisCommands(input_axis_commands.FindAll(command => command.isTriggered()), receiver);
            processTouchCommands(input_touch_commands.FindAll(command => command.isTriggered()), receiver);
        }

        //--------------------------------------------------------------------------------------
        protected void addKeyCommand(InputKeyCommand command)
        {
            input_key_commands.Add(command);
        }
        //--------------------------------------------------------------------------------------
        protected void addAxisCommand(InputAxisCommand command)
        {
            input_axis_commands.Add(command);
        }
        //--------------------------------------------------------------------------------------
        protected void addTouchCommand(InputTouchCommand command)
        {
            input_touch_commands.Add(command);
        }

        //--------------------------------------------------------------------------------------
        private void processKeyCommands(List<InputKeyCommand> commands, ICommandReceiver receiver)
        {
            foreach (InputKeyCommand command in commands)
            {
                if (command.execute(receiver) && !command.IsMultifuntional) break;
            }
                
        }
        //--------------------------------------------------------------------------------------
        private void processAxisCommands(List<InputAxisCommand> commands, ICommandReceiver receiver)
        {
            foreach (InputAxisCommand command in commands)
            {
                if (command.execute(receiver) && !command.IsMultifuntional) break;
            }
        }
        //--------------------------------------------------------------------------------------
        private void processTouchCommands(List<InputTouchCommand> commands, ICommandReceiver receiver)
        {
            foreach (InputTouchCommand command in commands)
            {
                if (command.execute(receiver) && !command.IsMultifuntional) break;
            }
        }
    }
}