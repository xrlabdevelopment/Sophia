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

        //--------------------------------------------------------------------------------
        /// <summary>
        /// This is only called once per state.
        /// Implement this function to initialize some properties on a certain state.
        /// </summary>
        void onInitialize(IStateMachine stateMachineController);
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
        public List<TTransitionType> Transitions
        {
            get { return StateTransitions; }
            set { StateTransitions = value; }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// This is only called once per state.
        /// Implement this function to initialize some properties on a certain state.
        /// </summary>
        public abstract void onInitialize(IStateMachine stateMachineController);

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
        public bool addTransition(TTransitionType transition)
        {
            if (StateTransitions.Find(t => t.InstanceID == transition.InstanceID) != null)
            {
                Debug.Log("Transition with name: " + transition.InstanceName + "was already added");
                return false;
            }

            StateTransitions.Add(transition);
            return true;
        }
        //--------------------------------------------------------------------------------
        public bool addTransitions(List<TTransitionType> transitions)
        {
            StateTransitions = transitions;
            return true;
        }

        //--------------------------------------------------------------------------------
        public static TStateType create<TStateType>()
            where TStateType : ScriptableObject, IState
        {
            BaseState<TTransitionType> state = ScriptableObject.CreateInstance<TStateType>() as BaseState<TTransitionType>;

            state.Transitions = new List<TTransitionType>();

            return state as TStateType;
        }
        //--------------------------------------------------------------------------------
        public static TStateType create<TStateType>(List<TTransitionType> transitions)
            where TStateType : ScriptableObject, IState
        {
            BaseState<TTransitionType> state = ScriptableObject.CreateInstance<TStateType>() as BaseState<TTransitionType>;

            state.Transitions = transitions == null
                ? new List<TTransitionType>()
                : transitions;

            return state as TStateType;
        }
    }

    /// <summary>
    /// Default state class
    /// Using default transition objects
    /// </summary>
    public abstract class State : BaseState<Transition> { }
}
