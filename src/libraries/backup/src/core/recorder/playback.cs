using System;
using System.Collections.Generic;
using Sophia.Deprecated;

namespace Sophia.Deprecated
{
    public class Playback
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public bool IsRunning
        {
            get
            {
                return recorded_actions.Count > 0;
            }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly IActionFactory action_factory;
        private readonly Queue<IAction> recorded_actions;

        //-------------------------------------------------------------------------------------
        public Playback(IActionFactory actionFactory)
        {
            action_factory = actionFactory;
            recorded_actions = new Queue<IAction>();
        }

        //-------------------------------------------------------------------------------------
        public void load(List<IXMLObject> actions)
        {
            foreach(IXMLObject action in actions)
            {
                SerializableAction _action = action as SerializableAction;
                if (_action == null)
                    continue;

                recorded_actions.Enqueue(action_factory.createAction(_action.ActionType));
            }
        }

        //-------------------------------------------------------------------------------------
        public void prepare()
        {
            recorded_actions.Peek();
        }
        //-------------------------------------------------------------------------------------
        public void play()
        {
            recorded_actions.Dequeue();
        }
    }
}
