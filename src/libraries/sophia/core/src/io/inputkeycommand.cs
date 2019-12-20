

namespace Sophia.Core
{
    public abstract class InputKeyCommand : IInputCommand
    {
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

        private KeyCode key_code;
        private KeyState key_state;
        private IInputManager input_manager;
        private bool is_multi_funtional;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the input command
        /// </summary>
        /// <param name="code">Key we need to check</param>
        /// <param name="state">State of the key we need to check</param>
        /// <param name="manager">Inputmanager to use</param>
        public InputKeyCommand(KeyCode code, KeyState state, IInputManager manager, MultiFunctionalCommand multi)
        {
            key_code = code;
            key_state = state;
            input_manager = manager;
            is_multi_funtional = multi == MultiFunctionalCommand.YES;
    }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the input command
        /// </summary>
        /// <param name="receiver">The receiver of the input command</param>
        public abstract bool execute(ICommandReceiver receiver);

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Is the input command triggered
        /// </summary>
        /// <returns>True if the command is triggered, false if not</returns>
        public bool isTriggered()
        {
            switch (key_state)
            {
                case KeyState.DOWN:
                    return isDown();

                case KeyState.UP:
                    return isUp();

                case KeyState.PRESSED:
                    return isPressed();

                case KeyState.RELEASED:
                    return isReleased();
            }

            return false;
        }

        //--------------------------------------------------------------------------------------
        private bool isDown()
        {
            return input_manager.isDown(key_code);
        }
        //--------------------------------------------------------------------------------------
        private bool isUp()
        {
            return input_manager.isUp(key_code);
        }
        //--------------------------------------------------------------------------------------
        private bool isPressed()
        {
            return input_manager.isPressed(key_code);
        }
        //--------------------------------------------------------------------------------------
        private bool isReleased()
        {
            return input_manager.isReleased(key_code);
        }
    }
}