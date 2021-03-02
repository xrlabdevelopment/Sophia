using System;
using System.Collections.Generic;

namespace Sophia.Core.Patterns
{
    /// <summary>
    /// A state in a finite state machine.
    /// </summary>
    /// <typeparam name="TData">The data type used for transition guard checks.</typeparam>
    public class FSMState
    {
        //--------------------------------------------------------------------------------
        // Fields
        private readonly string name;

        private readonly Action tick_action;
        private readonly Action enter_action;
        private readonly Action exit_action;

        private readonly Dictionary<FSMEvent, List<FSMTransition>> transitions = new Dictionary<FSMEvent, List<FSMTransition>>();

        //-------------------------------------------------------------------------------------	
        /// <summary>
        /// The name of the state.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        public Dictionary<FSMEvent, List<FSMTransition>> Transitions
        {
            get { return transitions; }
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Create a new state
        /// </summary>
        /// <param name="name">Name of the state</param>
        /// <param name="tickAction">Action to trigger when we update the state</param>
        /// <param name="enterAction">Action to trigger when we enter a state</param>
        /// <param name="exitAction">Action to trigger when we exit a state</param>
        public FSMState(string name, Action tickAction, Action enterAction, Action exitAction)
        {
            this.name = name;

            this.tick_action = tickAction;
            this.enter_action = enterAction;
            this.exit_action = exitAction;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Add a new transition to this state
        /// </summary>
        /// <param name="transition">The transition we want to add</param>
        public void addTransition(FSMTransition transition)
        {
            List<FSMTransition> ts = null;
            if (!transitions.ContainsKey(transition.Trigger))
            {
                ts = new List<FSMTransition>();
                transitions[transition.Trigger] = ts;
            }
            else
            {
                ts = transitions[transition.Trigger];
            }

            ts.Add(transition);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Update this state
        /// </summary>
        public void update()
        {
            if (tick_action != null)
                tick_action();
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Trigger an event on this state
        /// This will execute a transition to a different ( or the same ) state.
        /// </summary>
        /// <param name="trigger">The event we want to trigger</param>
        /// <returns>The new state we are in</returns>
        public FSMState triggerEvent(FSMEvent trigger)
        {
            if (!transitions.ContainsKey(trigger))
                return null;

            foreach (FSMTransition transition in transitions[trigger])
            {
                if (transition.checkGuard())
                {
                    fireExitAction();
                    transition.fireAction();
                    FSMState newState = transition.Target;
                    newState.fireEnterAction();
                    return newState;
                }
            }
            return null;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the enter action when we start the state
        /// </summary>
        public void fireEnterAction()
        {
            if (enter_action != null)
                enter_action();
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Execute the exit action when we exit the state
        /// </summary>
        public void fireExitAction()
        {
            if (exit_action != null)
                exit_action();
        }
    }
}
