using System.Collections.Generic;

namespace Sophia.Core.Input
{
    public abstract class InputController
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<string, List<IInputCommand>> input_command_lists = new Dictionary<string, List<IInputCommand>>();

        //--------------------------------------------------------------------------------------
        public abstract void initialize();
        //--------------------------------------------------------------------------------------
        public void shutdown()
        {
            foreach(KeyValuePair<string, List<IInputCommand>> input_command_list in input_command_lists)
                input_command_list.Value.Clear();

            input_command_lists.Clear();
        }

        //--------------------------------------------------------------------------------------
        public void processInputFor(ICommandReceiver receiver)
        {
            foreach (KeyValuePair<string, List<IInputCommand>> input_command_list in input_command_lists)
                processCommands(input_command_list.Value.FindAll(command => command.isTriggered()), receiver);
        }

        //--------------------------------------------------------------------------------------
        protected void addCommand(string listName, IInputCommand command)
        {
            if(input_command_lists.ContainsKey(listName))
            {
                input_command_lists[listName].Add(command);
            }
            else
            {
                input_command_lists[listName] = new List<IInputCommand> { command };
            }
        }

        //--------------------------------------------------------------------------------------
        private void processCommands(List<IInputCommand> commands, ICommandReceiver receiver)
        {
            foreach (IInputCommand command in commands)
            {
                if (command.execute(receiver) && !command.IsMultifuntional) break;
            }
        }
    }
}
