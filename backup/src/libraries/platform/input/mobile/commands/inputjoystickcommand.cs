using Sophia.Core;
using Sophia.Core.Input;
using System;

namespace Sophia.Platform
{
    public enum JoyStickAxis
    {
        HORIZONTAL,
        VERTICAL
    }

    public abstract class InputJoyStickCommand : IInputCommand
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public string AxisName
        {
            get { return input_axis.ToString(); }
        }
        public float AxisValue
        {
            get { return input_axis_value; }
        }
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
        private JoyStickAxis input_axis;
        private float input_axis_value;
        private IInputManager input_manager;
        private bool is_multi_funtional;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the input command
        /// </summary>
        /// <param name="axis">axis of the input command</param>
        /// <param name="manager">input manager to use</param>
        public InputJoyStickCommand(JoyStickAxis axis, IInputManager manager, MultiFunctionalCommand multi)
        {
            input_axis = axis;
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
            input_axis_value = input_manager.getAxis(AxisName);
            return System.Math.Abs(input_axis_value) > float.Epsilon;
        }
    }
}
