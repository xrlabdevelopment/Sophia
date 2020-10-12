#pragma warning disable 0649

using System.Collections.Generic;
using Sophia.Core.Patterns;
using UnityEngine;

namespace Sophia.Platform.Patterns
{
    /// <summary>
    /// Interface that each state should implement
    /// </summary>
    public interface IState
    {
        string Name { get; }
        List<ITransition> Transitions { get; set; }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// This is only called once per state.
        /// Implement this function to initialize some properties on a certain state.
        /// </summary>
        void onInitialize(StateMachine stateMachineController);
        //--------------------------------------------------------------------------------
        /// <summary>
        /// We enter this state
        /// </summary>
        void onEnter();
        //--------------------------------------------------------------------------------
        /// <summary>
        /// We update this state
        /// </summary>
        void onUpdate();
        //--------------------------------------------------------------------------------
        /// <summary>
        /// We exit this state
        /// </summary>
        void onExit();
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Fired when we transition from state towards a different state
        /// </summary>
        /// <param name="trigger">Transition trigger</param>
        /// <param name="state">State to transition to</param>
        void onTransition(FSMEvent trigger, FSMState state);
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Fired when we guard ourselves against a transition
        /// </summary>
        /// <param name="trigger">Transition trigger</param>
        /// <param name="state">State to transition to</param>
        /// <returns></returns>
        bool onGuard(FSMEvent trigger, FSMState state);

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add a new transition
        /// </summary>
        /// <param name="transition">Transition to be added</param>
        /// <returns>Return true when the transition is added, false otherwise</returns>
        bool addTransition(ITransition transition);
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Add new transitions
        /// </summary>
        /// <param name="transition">Transitions to be added</param>
        /// <returns>Return true when the transitions are added, false otherwise</returns>
        bool addTransitions(List<ITransition> transitions);
    }

    /// <summary>
    /// Abstract base state
    /// Use this if you have a custom transition type
    /// </summary>
    /// <typeparam name="TTransitionType"></typeparam>
    public abstract class BaseState<TTransitionType> : ScriptableObject, IState
        where TTransitionType : ScriptableObject, ITransition
    {
        //--------------------------------------------------------------------------------
        // Inspector
        /// <summary>
        /// List of transitions this state has
        /// </summary>
        [SerializeField]
        private List<TTransitionType> StateTransitions;

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
        public List<ITransition> Transitions
        {
            get
            {
                List<ITransition> transition_interfaces = new List<ITransition>();
                foreach (TTransitionType t in StateTransitions)
                    transition_interfaces.Add(t);
                return transition_interfaces;
            }
            set
            {
                List<TTransitionType> actual_transitions = new List<TTransitionType>();
                foreach(ITransition t in value)
                {
                    TTransitionType actual_t = t as TTransitionType;
                    if (actual_t == null)
                        continue;

                    actual_transitions.Add(actual_t);
                }

                StateTransitions = actual_transitions;
            }
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
        public bool addTransition(ITransition transition)
        {
            if (Transitions.Find(t => t.InstanceID == transition.InstanceID) != null)
            {
                Debug.Log("Transition with name: " + transition.InstanceName + "was already added");
                return false;
            }

            Transitions.Add(transition);
            return true;
        }
        //--------------------------------------------------------------------------------
        public bool addTransitions(List<ITransition> transitions)
        {
            bool success = true;
            foreach (ITransition t in transitions)
            {
                success &= addTransition(t);
            }

            return success;
        }

        //--------------------------------------------------------------------------------
        public static IState create<TStateType>()
            where TStateType : ScriptableObject, IState
        {
            IState state = ScriptableObject.CreateInstance<TStateType>();

            state.Transitions = new List<ITransition>();

            return state;
        }
        //--------------------------------------------------------------------------------
        public static IState create<TStateType>(List<ITransition> transitions)
            where TStateType : ScriptableObject, IState
        {
            IState state = ScriptableObject.CreateInstance<TStateType>();

            state.Transitions = transitions == null
                ? new List<ITransition>()
                : transitions;

            return state;
        }
    }

    /// <summary>
    /// Default state class
    /// Using default transition objects
    /// </summary>
    public abstract class State : BaseState<Transition> { }
}
