#pragma warning disable 0649

using System.Collections.Generic;
using System.Linq;
using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public class StateMachine : IStateMachine
    {
        //--------------------------------------------------------------------------------
        // Delegates
        public delegate void StateMachineEvent();

        public StateMachineEvent OnStateMachineStarted;
        public StateMachineEvent OnStateMachineUpdated;
        public StateMachineEvent OnStateMachineReset;
        public StateMachineEvent OnStateMachineStopped;

        //--------------------------------------------------------------------------------
        // Properties
        public FSMState CurrentState
        {
            get { return state_machine.CurrentState; }
        }
        public Component Owner
        {
            get { return owner; }
        }

        //--------------------------------------------------------------------------------
        // Fields
        private FiniteStateMachine state_machine;
        private List<string> queued_transitions = new List<string>();
        private Component owner;
        private bool initialized;

        //--------------------------------------------------------------------------------
        public StateMachine(Component owner = null)
        {
            this.owner = owner;
            this.initialized = false;
        }

        //--------------------------------------------------------------------------------
        public void initialize(State startState, List<State> otherStates)
        {
            state_machine = new FiniteStateMachine();

            addStateToStateMachine(startState, true);
            foreach (State state in otherStates)
                addStateToStateMachine(state, false);

            addStateTransitions(startState);
            foreach (State state in otherStates)
                addStateTransitions(state);

            initialized = true;
        }

        //--------------------------------------------------------------------------------
        public void start()
        {
            Debug.Assert(initialized, "Initialize state machine first");

            if (OnStateMachineStarted != null)
                OnStateMachineStarted();

            state_machine.start();
        }
        //--------------------------------------------------------------------------------
        public void update()
        {
            Debug.Assert(initialized, "Initialize state machine first");

            if (!state_machine.IsRunning)
                return;

            state_machine.update();
            if (OnStateMachineUpdated != null)
                OnStateMachineUpdated();

            executeQueuedTransition();
        }
        //--------------------------------------------------------------------------------
        public void reset()
        {
            Debug.Assert(initialized, "Initialize state machine first");

            if (!state_machine.IsRunning)
                return;

            state_machine.reset();
            if (OnStateMachineReset != null)
                OnStateMachineReset();
        }
        //--------------------------------------------------------------------------------
        public void stop()
        {
            Debug.Assert(initialized, "Initialize state machine first");

            state_machine.stop();
            if (OnStateMachineStopped != null)
                OnStateMachineStopped();
        }

        //--------------------------------------------------------------------------------
        public void transition(string eventName)
        {
            Debug.Assert(initialized, "Initialize state machine first");

            if (!state_machine.IsRunning)
            {
                queued_transitions.Add(eventName);
            }
            else
            {
                state_machine.triggerEvent(eventName);
            }
        }

        //--------------------------------------------------------------------------------
        private void addStateToStateMachine(State state, bool startState)
        {
            state.onInitialize(this);
            state_machine.addState(state.Name, startState, state.onUpdate, state.onEnter, state.onExit);
        }
        //--------------------------------------------------------------------------------
        private void addStateTransitions(State state)
        {
            foreach (Transition transition in state.Transitions)
            {
                FSMEvent evt = state_machine.registerEvent(transition.EventName);
                state_machine.addTransition(state.Name, transition.TargetState.Name, evt, state.onGuard, state.onTransition);
            }
        }

        //--------------------------------------------------------------------------------
        private void executeQueuedTransition()
        {
            if (queued_transitions.Count > 0)
            {
                string event_name = queued_transitions.First();

                queued_transitions.Remove(event_name);

                transition(event_name);
            }
        }
    }
}
