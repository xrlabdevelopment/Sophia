using System;
using System.Collections.Generic;

namespace Sophia.Core
{
    public delegate bool Guard(FSMEvent trigger, FSMState newState);

    /// <summary>
    /// A finite state machine.
    /// </summary>
    /// <typeparam name="TData">The data type used for transition guard checks.</typeparam>
    public class FiniteStateMachine : IStateMachine
    {
        //--------------------------------------------------------------------------------
        // Properties
        public bool IsRunning
        {
            get { return running; }
        }

        //--------------------------------------------------------------------------------
        // Fields
        private Dictionary<string, FSMState> states = new Dictionary<string, FSMState>();

        private FSMState start_state;
        private FSMState current_state;

        private int next_event = 0;
        private bool running = false;

        private Dictionary<string, FSMEvent> registered_events = new Dictionary<string, FSMEvent>();

        //--------------------------------------------------------------------------------
        /// <summary>
        /// The current state.
        /// </summary>
        public FSMState CurrentState
        {
            get { return current_state; }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Create a finite state machine.
        /// </summary>
        /// <param name="data">The data used for predicate guard checks</param>
        public FiniteStateMachine()
        {
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Register a new event
        /// </summary>
        /// <param name="name">The name of the event</param>
        public FSMEvent registerEvent(string name)
        {
            if (registered_events.ContainsKey(name))
                throw new ArgumentException("Event '" + name + "' already registered");

            FSMEvent newEvent = new FSMEvent(next_event++, name);
            registered_events[name] = newEvent;
            return newEvent;
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a state.
        /// </summary>
        /// <param name="name">The name of the state</param>
        /// <param name="startState"><code>true</code> if this state is the start state</param>
        public void addState(string name, bool startState)
        {
            addState(name, startState, null, null, null);
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a state.
        /// </summary>
        /// <param name="name">The name of the state</param>
        /// <param name="startState"><code>true</code> if this state is the start state</param>
        /// <param name="tickAction">An action called when the state is ticked</param>
        /// <param name="enterAction">An action called when entering the state</param>
        /// <param name="exitAction">An action called when exiting the state</param>
        public void addState(string name, bool startState, Action tickAction, Action enterAction, Action exitAction)
        {
            FSMState newState = new FSMState(name, tickAction, enterAction, exitAction);
            states.Add(name, newState);
            if (startState)
            {
                if (this.start_state != null)
                    throw new ArgumentException("Start state already set!");

                this.start_state = newState;
            }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a transition.
        /// </summary>
        /// <param name="source">The source state name.</param>
        /// <param name="target">The target state name.</param>
        /// <param name="eventName">The event name that triggers this transition.</param>
        public void addTransition(string source, string target, FSMEvent trigger)
        {
            addTransition(source, target, trigger, null, null);
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a transition.
        /// </summary>
        /// <param name="source">The source state name.</param>
        /// <param name="target">The target state name.</param>
        /// <param name="eventName">The event name that triggers this transition.</param>
        /// <param name="guard">The guard predicate for this transition.</param>
        public void addTransition(string source, string target, FSMEvent trigger, Guard guard)
        {
            addTransition(source, target, trigger, guard, null);
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a transition.
        /// </summary>
        /// <param name="source">The source state name.</param>
        /// <param name="target">The target state name.</param>
        /// <param name="eventName">The event name that triggers this transition.</param>
        /// <param name="action">An action callback to be called if this transition is triggered.</param>
        public void addTransition(string source, string target, FSMEvent trigger, Action<FSMEvent, FSMState> action)
        {
            addTransition(source, target, trigger, null, action);
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a transition.
        /// </summary>
        /// <param name="source">The source state name.</param>
        /// <param name="target">The target state name.</param>
        /// <param name="eventName">The event name that triggers this transition.</param>
        /// <param name="predicate">The guard predicate for this transition.</param>
        /// <param name="action">An action callback to be called if this transition is triggered.</param>
        public void addTransition(string source, string target, FSMEvent trigger, Guard guard, Action<FSMEvent, FSMState> action)
        {
            if (!states.ContainsKey(source))
                throw new ArgumentException("No such source state found");
            if (!states.ContainsKey(target))
                throw new ArgumentException("No such target state found");

            states[source].addTransition(new FSMTransition(trigger, states[target], guard, action));
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Enters the start state.
        /// </summary>
        public void start()
        {
            if (start_state == null)
                throw new InvalidOperationException("No start state set!");
            if (current_state != null)
                throw new InvalidOperationException("FSM already in a state");

            start_state.fireEnterAction();
            current_state = start_state;

            running = true;
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Reset this FSM to the start state.
        /// </summary>
        public void reset()
        {
            if (current_state != null)
            {
                current_state.fireExitAction();
                current_state = null;
            }

            start();
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Tick the current state's update action (if any).
        /// </summary>
        public void update()
        {
            if (current_state == null)
                throw new InvalidOperationException("No current state (you may need to call Start)");

            if(running)
                current_state.update();
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Stop updating this statemachine
        /// </summary>
        public void stop()
        {
            running = false;
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Trigger an event. If a transition exists for this event name in
        /// the current state, the state will change to the transition's
        /// target state.
        /// </summary>
        /// <param name="eventName">The event name.</param>
        public void triggerEvent(string eventName)
        {
            if (current_state == null)
                throw new InvalidOperationException("No current state (you may need to call Start)");
            if (!registered_events.ContainsKey(eventName))
                throw new InvalidOperationException("Event is not registered");

            FSMState newState = current_state.triggerEvent(registered_events[eventName]);
            if (newState != null)
                current_state = newState;
        }
    }
}
