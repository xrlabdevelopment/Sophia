using Sophia.Core.Algorithms;

using NUnit.Framework;
using Sophia.Core.Patterns;
using Sophia.Core.Events;
using Sophia.Core.Utilities;

namespace Sophia.Tests.Core
{
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
