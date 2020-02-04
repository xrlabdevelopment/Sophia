namespace Sophia.Core
{
    public abstract class Event : IEvent
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public abstract int EventType { get; }

        public IEventSender Sender
        {
            get
            {
                return sender;
            }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private IEventSender sender;

        //-------------------------------------------------------------------------------------
        public Event(IEventSender s)
        {
            sender = s; 
        }
    }
}
