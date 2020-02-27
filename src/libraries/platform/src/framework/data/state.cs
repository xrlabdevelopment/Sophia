#pragma warning disable 0649

using System.Collections.Generic;
using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    public abstract class State : ScriptableObject
    {
        //--------------------------------------------------------------------------------
        // Inspector
        /// <summary>
        /// List of transitions this state has
        /// </summary>
        [SerializeField]
        private List<Transition> StateTransitions;

        //--------------------------------------------------------------------------------
        // Properties
        /// <summary>
        /// Name of this state
        /// </summary>
        public string Name
        {
            get { return GetType().ToString(); }
        }

        /// <summary>
        /// List of transitions this state has
        /// </summary>
        public List<Transition> Transitions
        {
            get { return StateTransitions; }
            internal set { StateTransitions = value; }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// This is only called once per state.
        /// Implement this function to initialize some properties on a certain state.
        /// </summary>
        public abstract void onInitialize(StateMachine stateMachineController);

        //--------------------------------------------------------------------------------
        /// <summary>
        /// We enter this state
        /// </summary>
        public abstract void onEnter();
        //--------------------------------------------------------------------------------
        /// <summary>
        /// We update this state
        /// </summary>
        public abstract void onUpdate();
        //--------------------------------------------------------------------------------
        /// <summary>
        /// We exit this state
        /// </summary>
        public abstract void onExit();

        //--------------------------------------------------------------------------------
        public abstract void onTransition(FSMEvent trigger, FSMState state);
        //--------------------------------------------------------------------------------
        public abstract bool onGuard(FSMEvent trigger, FSMState state);

        //--------------------------------------------------------------------------------
        public static State create<T>(List<Transition> transitions = null)
            where T : State
        {
            State state = ScriptableObject.CreateInstance<T>();

            state.Transitions = transitions == null ? new List<Transition>() : transitions;

            return state;
        }
    }
}
