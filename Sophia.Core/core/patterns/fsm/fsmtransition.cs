using System;

namespace Sophia
{
    namespace Patterns
    {
        /// <summary>
        /// A transition from one state to another.
        /// </summary>
        public class FSMTransition
        {
            //--------------------------------------------------------------------------------
            // Fields
            private readonly FSMEvent trigger;
            private readonly FSMState target;
            private readonly Guard guard;

            private readonly Action<FSMEvent, FSMState> action;

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// The name of the event triggering the transition.
            /// </summary>
            public FSMEvent Trigger
            {
                get { return trigger; }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// The target state after the transition occurs.
            /// </summary>
            public FSMState Target
            {
                get { return target; }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Create a new transition
            /// </summary>
            /// <param name="trigger">The trigger for this transition</param>
            /// <param name="target">The target state we will transition to</param>
            /// <param name="guard">The function that will check if we can transition</param>
            /// <param name="action">The action we want to execture when we tranition</param>
            public FSMTransition(FSMEvent trigger, FSMState target, Guard guard, Action<FSMEvent, FSMState> action)
            {
                this.trigger = trigger;
                this.target = target;
                this.guard = guard;
                this.action = action;
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Can we transition
            /// </summary>
            /// <returns></returns>
            public bool checkGuard()
            {
                if (guard != null)
                    return guard(Trigger, Target);

                return true;
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Execute an action when we transition
            /// </summary>
            public void fireAction()
            {
                if (action != null)
                    action(Trigger, Target);
            }
        }
    }
}
