using Sophia.Core;
using Sophia.Core.Input;
using UnityEngine;

namespace Sophia.Platform.Input
{
    public class DesktopInputManager : IInputManager, IDesktopInputManager
    {       
        //-------------------------------------------------------------------------------------
        public float getAxis(string axis)
        {
            return UnityEngine.Input.GetAxis(axis);            
        }

        //-------------------------------------------------------------------------------------
        public bool isDown(Core.Input.KeyCode code)
        {
            return UnityEngine.Input.GetKey((UnityEngine.KeyCode)code);
        }
        //-------------------------------------------------------------------------------------
        public bool isUp(Core.Input.KeyCode code)
        {
            return !UnityEngine.Input.GetKey((UnityEngine.KeyCode)code);
        }

        //-------------------------------------------------------------------------------------
        public bool isPressed(Core.Input.KeyCode code)
        {
            return UnityEngine.Input.GetKeyDown((UnityEngine.KeyCode)code);
        }
        //-------------------------------------------------------------------------------------
        public bool isReleased(Core.Input.KeyCode code)
        {
            return UnityEngine.Input.GetKeyUp((UnityEngine.KeyCode)code);
        }
    }
}
