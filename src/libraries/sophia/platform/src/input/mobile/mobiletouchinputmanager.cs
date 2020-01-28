using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public class MobileTouchInputManager : IInputManager, IMobileInputManager
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public int touchCount
        {
            get
            {
                return Input.touchCount;
            }
        }

        //-------------------------------------------------------------------------------------
        public Touch getTouch(int index)
        {
            return Input.GetTouch(index);
        }

        //-------------------------------------------------------------------------------------
        public float getAxis(string axis)
        {
            return Input.GetAxis(axis);
        }
    }
}