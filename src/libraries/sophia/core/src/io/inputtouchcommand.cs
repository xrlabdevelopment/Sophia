using System;

namespace Sophia.Core
{
    public abstract class InputTouchCommand : IInputCommand
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public IInputManager InputManager
        {
            get
            {
                return input_manager;
            }
        }

        public bool IsMultifuntional
        {
            get
            {
                return is_multi_funtional;
            }

            set
            {
                is_multi_funtional = value;
            }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private IInputManager input_manager;
        private bool is_multi_funtional;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the input command
        /// </summary>
        /// <param name="axis">axis of the input command</param>
        /// <param name="manager">input manager to use</param>
        public InputTouchCommand(IInputManager manager, MultiFunctionalCommand multi)
        {
            input_manager = manager;
            is_multi_funtional = multi == MultiFunctionalCommand.YES;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the input command
        /// </summary>
        /// <param name="receiver">The receiver of the input command</param>
        /// <returns>True if we handled the command, false if not</returns>
        public abstract bool execute(ICommandReceiver receiver);

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Is the input command triggered
        /// </summary>
        /// <returns>True if the command is triggered, false if not</returns>
        public bool isTriggered()
        {
            return false;
        }
    }
}