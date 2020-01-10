using Sophia.Core;
using UnityEngine;
using System.Collections.Generic;



namespace Sophia.Platform
{
    public class BaseActionRecorder : BaseMonoBehaviour
    {
        protected List<IAction> recorded_actions = new List<IAction>();
        [SerializeField] protected BaseJsonParser JsonParser = null;

        //-------------------------------------------------------------------------------------
        protected void Start()
        {
            if (JsonParser == null)
                JsonParser = new BaseJsonParser();
        }

        //-------------------------------------------------------------------------------------
        public int addAction(IAction action)
        {
            recorded_actions.Add(action);
            //return stored index (optional use)
            return recorded_actions.Count - 1;
        }

        //-------------------------------------------------------------------------------------
        public bool removeActionAtIndex(int index)
        {
            if (recorded_actions.Count > index)
            {
                recorded_actions.RemoveAt(index);
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------------------------
        public bool removeLastAction()
        {
            if (recorded_actions.Count > 0)
            {
                recorded_actions.RemoveAt(recorded_actions.Count - 1);
                return true;
            }
            return false;
        }
    }
}