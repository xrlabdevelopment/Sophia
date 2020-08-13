using Sophia.Core.Events;

using NUnit.Framework;
using Sophia.Core.Utilities;

namespace Sophia.Tests.Core
{
    [TestFixture]
    public class testsuit_EventHandler
    {
        internal enum EventCategory
        {
            TEST_00,
            TEST_01,
            TEST_02,
        }
        internal enum EventType
        {
            SOMETHING_00,
            SOMETHING_01,
            SOMETHING_02,
        }

        internal class MockSender : IEventSender { }

        internal class MockEventHandler : IEventHandler
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
                return (EventCategory & evt.EventCategory) != 0;
            }
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
        public void test_eventhandler_handleEvent()
        {
            MockEventHandler handler = new MockEventHandler();

            MockSender sender = new MockSender();

            Assert.IsTrue(handler.handleEvent(new MockEvent_Test01(sender)));
            Assert.IsFalse(handler.handleEvent(new MockEvent_Test02(sender)));
        }
    }
}
