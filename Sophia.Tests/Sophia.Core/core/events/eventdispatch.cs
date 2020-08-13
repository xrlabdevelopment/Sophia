using Sophia.Core.Events;

using NUnit.Framework;
using Sophia.Core.Utilities;

namespace Sophia.Tests.Core
{
    [TestFixture]
    public class testsuit_EventDispatch
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

        internal class MockSender : IEventSender {}

        internal class MockEventHandler_Test00 : IEventHandler
        {
            //-------------------------------------------------------------------------------------
            // Properties
            public BitField EventCategory
            {
                get { return new BitField((int)testsuit_EventDispatch.EventCategory.TEST_00); }
            }

            //-------------------------------------------------------------------------------------
            public bool handleEvent(IEvent evt)
            {
                return true;
            }
        }
        internal class MockEventHandler_Test01 : IEventHandler
        {
            //-------------------------------------------------------------------------------------
            // Properties
            public BitField EventCategory
            {
                get { return new BitField((int)testsuit_EventDispatch.EventCategory.TEST_01); }
            }

            //-------------------------------------------------------------------------------------
            public bool handleEvent(IEvent evt)
            {
                return true;
            }
        }
        internal class MockEventHandler_Test02 : IEventHandler
        {
            //-------------------------------------------------------------------------------------
            // Properties
            public BitField EventCategory
            {
                get { return new BitField((int)testsuit_EventDispatch.EventCategory.TEST_02); }
            }

            //-------------------------------------------------------------------------------------
            public bool handleEvent(IEvent evt)
            {
                return true;
            }
        }

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
                :base(s)
            {}
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
        internal class MockEvent_Test02 : Event
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public override int EventType
            {
                get { return (int)testsuit_EventDispatch.EventType.SOMETHING_02; }
            }
            public override BitField EventCategory
            {
                get { return new BitField((int)testsuit_EventDispatch.EventCategory.TEST_02); }
            }

            //--------------------------------------------------------------------------------------
            public MockEvent_Test02(MockSender s)
                : base(s)
            { }
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_eventdispatch_subscribe()
        {
            EventDispatch dispatch = new EventDispatch();

            dispatch.subscribe(new MockEventHandler_Test00());
            dispatch.subscribe(new MockEventHandler_Test01());
            dispatch.subscribe(new MockEventHandler_Test02());

            Assert.That(dispatch.CategoryCount, Is.EqualTo(3));

            Assert.That(dispatch.getHandlers().Length, Is.EqualTo(3));
            Assert.That(dispatch.getHandlers(new BitField((int)EventCategory.TEST_00)).Length, Is.EqualTo(1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_eventdispatch_unsubscribe()
        {
            EventDispatch dispatch = new EventDispatch();

            MockEventHandler_Test00 handler = new MockEventHandler_Test00();

            dispatch.subscribe(handler);
            dispatch.subscribe(new MockEventHandler_Test01());
            dispatch.subscribe(new MockEventHandler_Test02());

            Assert.That(dispatch.CategoryCount, Is.EqualTo(3));

            Assert.That(dispatch.getHandlers().Length, Is.EqualTo(3));
            Assert.That(dispatch.getHandlers(new BitField((int)EventCategory.TEST_00)).Length, Is.EqualTo(1));

            dispatch.unsubscribe(handler);

            Assert.That(dispatch.CategoryCount, Is.EqualTo(3));         // The category remains inside the dispatchers list, only the handler is removed

            Assert.That(dispatch.getHandlers().Length, Is.EqualTo(2));
            Assert.That(dispatch.getHandlers(handler.EventCategory).Length, Is.EqualTo(0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_eventdispatch_dispatch()
        {
            EventDispatch dispatch = new EventDispatch();

            MockEventHandler_Test00 handler = new MockEventHandler_Test00();

            dispatch.subscribe(handler);
            dispatch.subscribe(new MockEventHandler_Test01());
            dispatch.subscribe(new MockEventHandler_Test02());

            MockSender s = new MockSender();

            Assert.IsTrue(dispatch.dispatch(new MockEvent_Test00(s)));

            dispatch.unsubscribe(handler);

            Assert.IsFalse(dispatch.dispatch(new MockEvent_Test00(s)));
        }
    }
}
