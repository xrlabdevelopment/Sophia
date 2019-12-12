using System;

namespace Sophia.Core
{
    public abstract class InputAxisCommand : IInputCommand
    {
        public IInputManager InputManager
        {
            get
            {
                return input_manager;
            }
        }

        private string input_axis_name;
        private float input_axis_value;
        private IInputManager input_manager;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the input command
        /// </summary>
        /// <param name="axis">axis of the input command</param>
        /// <param name="manager">input manager to use</param>
        public InputAxisCommand(string axis, IInputManager manager)
        {
            input_axis_name = axis;

            input_manager = manager;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the input command
        /// </summary>
        /// <param name="receiver">The receiver of the input command</param>
        public abstract void execute(ICommandReceiver receiver);

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Is the input command triggered
        /// </summary>
        /// <returns>True if the command is triggered, false if not</returns>
        public bool isTriggered()
        {
            input_axis_value = input_manager.getAxis(input_axis_name);
            return Math.Abs(input_axis_value) > float.Epsilon;
        }
    }
}