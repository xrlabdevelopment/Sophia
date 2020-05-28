using UnityEngine;

namespace Sophia.Deprecated.Input
{
    public class DesktopInputManager : IInputManager, IDesktopInputManager
    {       
        //-------------------------------------------------------------------------------------
        public float getAxis(string axis)
        {
            return UnityEngine.Input.GetAxis(axis);            
        }

        //-------------------------------------------------------------------------------------
        public bool isDown(KeyCode code)
        {
            return UnityEngine.Input.GetKey((UnityEngine.KeyCode)code);
        }
        //-------------------------------------------------------------------------------------
        public bool isUp(KeyCode code)
        {
            return !UnityEngine.Input.GetKey((UnityEngine.KeyCode)code);
        }

        //-------------------------------------------------------------------------------------
        public bool isPressed(KeyCode code)
        {
            return UnityEngine.Input.GetKeyDown((UnityEngine.KeyCode)code);
        }
        //-------------------------------------------------------------------------------------
        public bool isReleased(KeyCode code)
        {
            return UnityEngine.Input.GetKeyUp((UnityEngine.KeyCode)code);
        }
    }
}
