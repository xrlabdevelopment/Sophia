using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public class DesktopInputManager : IInputManager
    {
        //-------------------------------------------------------------------------------------
        public float getAxis(string axis)
        {
            return Input.GetAxis(axis);            
        }

        //-------------------------------------------------------------------------------------
        public bool isDown(Sophia.Core.KeyCode code)
        {
            return Input.GetKey((UnityEngine.KeyCode)code);
        }
        //-------------------------------------------------------------------------------------
        public bool isUp(Sophia.Core.KeyCode code)
        {
            return !Input.GetKey((UnityEngine.KeyCode)code);
        }

        //-------------------------------------------------------------------------------------
        public bool isPressed(Sophia.Core.KeyCode code)
        {
            return Input.GetKeyDown((UnityEngine.KeyCode)code);
        }
        //-------------------------------------------------------------------------------------
        public bool isReleased(Sophia.Core.KeyCode code)
        {
            return Input.GetKeyUp((UnityEngine.KeyCode)code);
        }
    }
}