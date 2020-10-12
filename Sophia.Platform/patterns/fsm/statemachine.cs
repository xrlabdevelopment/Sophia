#pragma warning disable 0649

using System.Collections.Generic;
using System.Linq;
using Sophia.Core.Patterns;
using UnityEngine;

namespace Sophia.Platform.Patterns
{
    public class StateMachine : IStateMachine
    {
        //--------------------------------------------------------------------------------
        // Delegates
        public delegate void StateMachineEvent();
        public delegate void StateMachineTransitionEvent(FSMState previousState, FSMState newState);

        public StateMachineEvent OnStateMachineStarted;
        public StateMachineEvent OnStateMachineUpdated;
        public StateMachineEvent OnStateMachineReset;
        public StateMachineEvent OnStateMachineStopped;
        public StateMachineTransitionEvent OnStateMachineTransitioned;

        //--------------------------------------------------------------------------------
        // Properties
        public FSMState CurrentState
        {
            get { return state_machine.CurrentState; }
        }
        public UnityEngine.Object Owner
        {
            get { return owner; }
        }

        //--------------------------------------------------------------------------------
        // Fields
        private FiniteStateMachine state_machine;
        private readonly List<string> queued_transitions = new List<string>();
        private readonly UnityEngine.Object owner;
        private bool initialized;

        //--------------------------------------------------------------------------------
        public StateMachine(UnityEngine.Object owner = null)
        {
            this.owner = owner;
            this.initialized = false;
        }

        //--------------------------------------------------------------------------------
        public void initialize(IState startState, List<IState> otherStates)
        {
            state_machine = new FiniteStateMachine();

            addStateToStateMachine(startState, true);
            foreach (IState state in otherStates)
                addStateToStateMachine(state, false);

            addStateTransitions(startState);
            foreach (IState state in otherStates)
                addStateTransitions(state);

            initialized = true;
        }

        //--------------------------------------------------------------------------------
        public void start()
        {
            UnityEngine.Debug.Assert(initialized, "Initialize state machine first");

            if (OnStateMachineStarted != null)
                OnStateMachineStarted();

            state_machine.start();
        }
        //--------------------------------------------------------------------------------
        public void update()
        {
            UnityEngine.Debug.Assert(initialized, "Initialize state machine first");

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
            UnityEngine.Debug.Assert(initialized, "Initialize state machine first");

            if (!state_machine.IsRunning)
                return;

            state_machine.reset();
            if (OnStateMachineReset != null)
                OnStateMachineReset();
        }
        //--------------------------------------------------------------------------------
        public void stop()
        {
            UnityEngine.Debug.Assert(initialized, "Initialize state machine first");

            state_machine.stop();
            if (OnStateMachineStopped != null)
                OnStateMachineStopped();
        }

        //--------------------------------------------------------------------------------
        public void transition(string eventName)
        {

            UnityEngine.Debug.Assert(initialized, "Initialize state machine first");

            if (!state_machine.IsRunning)
            {
                queued_transitions.Add(eventName);
            }
            else
            {
                FSMState current_state = state_machine.CurrentState;

                if(state_machine.triggerEvent(eventName))
                {
                    FSMState new_state = state_machine.CurrentState;

                    if (OnStateMachineTransitioned != null)
                        OnStateMachineTransitioned(current_state, new_state);
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void addStateToStateMachine(IState state, bool startState)
        {
            state.onInitialize(this);
            state_machine.addState(state.Name, startState, state.onUpdate, state.onEnter, state.onExit);
        }
        //--------------------------------------------------------------------------------
        private void addStateTransitions(IState state)
        {
            foreach (ITransition transition in state.Transitions)
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
