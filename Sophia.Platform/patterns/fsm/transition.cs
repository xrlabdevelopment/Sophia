#pragma warning disable 0649

using UnityEngine;

namespace Sophia.Platform.Patterns
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Default Transition")]
    public class Transition : ScriptableObject, ITransition
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
        /// Instance ID of this transition object
        /// </summary>
        public int InstanceID
        {
            get { return GetInstanceID(); }
        }

        /// <summary>
        /// Instance name of this transition object
        /// </summary>
        public string InstanceName
        {
            get { return name; }
        }

        /// <summary>
        /// Name of the event that will trigger this transition
        /// </summary>
        public string EventName
        {
            get { return TriggerEventName; }
            set { TriggerEventName = value; }
        }

        //--------------------------------------------------------------------------------
        //The target event we want to transition to.
        public IState TargetState
        {
            get { return TransitionState; }
            set
            {
                Debug.Assert((value as State) != null, "Given value was not an actual \" " + TransitionState.GetType().Name + " \" ");

                TransitionState = value as State;
            }
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
