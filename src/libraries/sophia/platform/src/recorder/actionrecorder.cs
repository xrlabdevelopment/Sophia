using Sophia.Core;
using System;
using System.Collections.Generic;

namespace Sophia.Platform
{
    public class ActionRecorder
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private List<IAction> recorded_actions;

        //-------------------------------------------------------------------------------------
        public ActionRecorder(string filePath, string fileName)
        {
            recorded_actions = new List<IAction>();
        }

        //-------------------------------------------------------------------------------------
        public bool add(IAction action)
        {
            if (recorded_actions.Find(a => a.UUID == action.UUID) == null)
            {
                recorded_actions.Add(action);
                return true;
            }

            return false;
        }

        //-------------------------------------------------------------------------------------
        public bool remove(Guid uuid)
        {
            int index = recorded_actions.FindIndex(a => a.UUID == uuid);
            if (index == -1)
                return false;

            recorded_actions.RemoveAt(index);
            return true;
        }
        //-------------------------------------------------------------------------------------
        public bool removeLast()
        {
            if (recorded_actions.Count == 0)
                return false;
            
            recorded_actions.RemoveAt(recorded_actions.Count - 1);
            return true;
        }
    }
}