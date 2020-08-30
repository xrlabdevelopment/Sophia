using Sophia.Core.Algorithms;

using NUnit.Framework;
using Sophia.Core.Patterns;
using Sophia.Core.Events;
using Sophia.Core.Utilities;

namespace Sophia.Tests.Core
{
    //singleton.cs
    [TestFixture]
    public class testsuit_Singleton
    {
        internal class MySingleton : Singleton<MySingleton>
        { }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_singleton_create_destroy()
        {
            MySingleton.createInstance();

            Assert.That(MySingleton.Instance, Is.Not.Null);

            MySingleton.destroyInstance();

#if DEBUG
            Assert.That(MySingleton.Instance, Is.Not.Null);
#else
            Assert.That(MySingleton.Instance, Is.Null);
#endif

        }
    }
    //statemachine.cs
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
    //observer.cs
    [TestFixture]
    public class testsuit_Observer
    {
        internal enum EventCategory
        {
            TEST_00,
            TEST_01,
            TEST_02
        }
        internal enum EventType
        {
            SOMETHING_00,
            SOMETHING_01,
            SOMETHING_02
        }

        internal class MockSender : IEventSender { }

        internal class MockEvent_Test00 : Event
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public override int EventType
            {
                get { return (int)testsuit_EventDispatch.EventType.SOMETHING_00; }
            }
            public override BitField EventCategory
            {
                get { return new BitField((int)testsuit_EventDispatch.EventCategory.TEST_00); }
            }

            //--------------------------------------------------------------------------------------
            public MockEvent_Test00(MockSender s)
                : base(s)
            { }
        }
        internal class MockEvent_Test01 : Event
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public override int EventType
            {
                get { return (int)testsuit_EventDispatch.EventType.SOMETHING_01; }
            }
            public override BitField EventCategory
            {
                get { return new BitField((int)testsuit_EventDispatch.EventCategory.TEST_01); }
            }

            //--------------------------------------------------------------------------------------
            public MockEvent_Test01(MockSender s)
                : base(s)
            { }
        }

        internal class MyObserver : IObserver
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public bool hasBeenNotified
            {
                get;
                private set;
            } = false;

            //--------------------------------------------------------------------------------------
            public bool notify(ISubject subject)
            {
                hasBeenNotified = true;
                return true;
            }
        }
        internal class MyEventObserver : IEventObserver
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public bool hasBeenNotified
            {
                get;
                private set;
            } = false;

            public BitField EventCategory
            {
                get
                {
                    return new BitField((int)testsuit_Observer.EventCategory.TEST_00);
                }
            }

            //--------------------------------------------------------------------------------------
            public bool notify(ISubject subject, IEvent evt)
            {
                hasBeenNotified = true;
                return true;
            }
        }

        internal class MySubject : Subject<MyObserver>
        { }
        internal class MyEventSubject : EventSubject<MyEventObserver>
        { }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_observer_notify()
        {
            MySubject subject = new MySubject();
            MyObserver[] observers = new MyObserver[] { new MyObserver(), new MyObserver() };

            foreach (MyObserver o in observers)
                subject.attach(o);

            subject.notify();

            foreach (MyObserver o in observers)
                Assert.IsTrue(o.hasBeenNotified);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_observer_notifyEvent()
        {
            MockSender s = new MockSender();

            MyEventSubject subject = new MyEventSubject();
            MyEventObserver[] observers = new MyEventObserver[] { new MyEventObserver(), new MyEventObserver() };

            foreach (MyEventObserver o in observers)
                subject.attach(o);

            subject.notify(new MockEvent_Test01(s));

            foreach (MyEventObserver o in observers)
                Assert.IsFalse(o.hasBeenNotified);

            subject.notify(new MockEvent_Test00(s));

            foreach (MyEventObserver o in observers)
                Assert.IsTrue(o.hasBeenNotified);
        }
    }
}
