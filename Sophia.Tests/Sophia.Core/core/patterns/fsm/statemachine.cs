using NUnit.Framework;
using Sophia.Core.Patterns;

namespace Sophia.Tests.Core
{
    [TestFixture]
    public class testsuit_Statemachine
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_statemachine_registerEvent()
        {
            FiniteStateMachine statemachine = new FiniteStateMachine();

            statemachine.registerEvent("event_01");

            Assert.That(statemachine.RegisteredEvents.Length, Is.EqualTo(1));
            Assert.That(statemachine.RegisteredEvents[0].Name, Is.EqualTo("event_01"));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_statemachine_addState()
        {
            FiniteStateMachine statemachine = new FiniteStateMachine();

            statemachine.addState("state_01", true);
            statemachine.addState("state_02", false);

            Assert.That(statemachine.States.Length, Is.EqualTo(2));
            Assert.That(statemachine.States[0].Name, Is.EqualTo("state_01"));
            Assert.That(statemachine.States[1].Name, Is.EqualTo("state_02"));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_statemachine_addTransition()
        {
            FiniteStateMachine statemachine = new FiniteStateMachine();

            statemachine.addState("state_01", true);
            statemachine.addState("state_02", false);

            FSMEvent evt = statemachine.registerEvent("trigger");

            statemachine.addTransition("state_01", "state_02", evt);
            statemachine.start();

            Assert.That(statemachine.CurrentState.Name, Is.EqualTo("state_01"));

            statemachine.triggerEvent("trigger");

            Assert.That(statemachine.CurrentState.Name, Is.EqualTo("state_02"));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_statemachine_addTransition_guard()
        {
            FiniteStateMachine statemachine = new FiniteStateMachine();

            statemachine.addState("state_01", true);
            statemachine.addState("state_02", false);

            FSMEvent evt = statemachine.registerEvent("trigger");

            Guard g = delegate (FSMEvent trigger, FSMState state) { return false; };

            statemachine.addTransition("state_01", "state_02", evt, g, null);
            statemachine.start();

            Assert.That(statemachine.CurrentState.Name, Is.EqualTo("state_01"));

            statemachine.triggerEvent("trigger");

            Assert.That(statemachine.CurrentState.Name, Is.EqualTo("state_01"));    // We guarded the transition.
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_statemachine_run()
        {
            bool updated = false;
            System.Action tick_action = () => updated = true;

            FiniteStateMachine statemachine = new FiniteStateMachine();

            statemachine.addState("state_01", true);
            statemachine.addState("state_02", false, tick_action, null, null);

            FSMEvent evt = statemachine.registerEvent("trigger");

            statemachine.addTransition("state_01", "state_02", evt);

            statemachine.start();
            statemachine.triggerEvent("trigger");

            Assert.That(statemachine.CurrentState.Name, Is.EqualTo("state_02"));

            statemachine.update();

            Assert.IsTrue(updated);

            statemachine.stop();

            Assert.IsFalse(statemachine.IsRunning);

            statemachine.reset();

            Assert.That(statemachine.CurrentState.Name, Is.EqualTo("state_01"));
        }
    }
}
