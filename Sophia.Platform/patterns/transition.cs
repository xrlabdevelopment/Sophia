#pragma warning disable 0649

using UnityEngine;

namespace Sophia.Deprecated
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Transition")]
    public class Transition : ScriptableObject
    {
        //--------------------------------------------------------------------------------
        // Inspector
        [Tooltip("The event's name that will trigger this transition")]
        [SerializeField]
        private string TriggerEventName;

        [Tooltip("The state we want to transition towards")]
        [SerializeField]
        private State TransitionState;

        //--------------------------------------------------------------------------------
        // Properties
        /// <summary>
        /// Name of the event that will trigger this transition
        /// </summary>
        public string EventName
        {
            get { return TriggerEventName; }
            internal set { TriggerEventName = value; }
        }

        //--------------------------------------------------------------------------------
        //The target event we want to transition to.
        public State TargetState
        {
            get { return TransitionState; }
            internal set { TransitionState = value; }
        }

        //--------------------------------------------------------------------------------
        public static Transition create(string eventName, State target)
        {
            Transition transition = ScriptableObject.CreateInstance<Transition>();

            transition.EventName = eventName;
            transition.TargetState = target;

            return transition;
        }
    }
}
