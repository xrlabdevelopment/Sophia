namespace Sophia.Core.Events
{
    /// <summary>
    /// Abstract class of all events
    /// </summary>
    public abstract class Event : IEvent
    {
        //-------------------------------------------------------------------------------------
        // Properties
        /// <summary>
        /// Did we handled this event or not
        /// </summary>
        public bool Handled
        {
            get;
            set;
        } = false;

        /// <summary>
        /// The type of the event
        /// </summary>
        public abstract int EventType { get; }
        /// <summary>
        /// The category of the event
        /// </summary>
        public abstract int EventCategory { get; }

        /// <summary>
        /// The instigator of the event
        /// </summary>
        public IEventSender Sender
        {
            get
            {
                return sender;
            }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private readonly IEventSender sender;

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of an abstract event
        /// </summary>
        /// <param name="s">Instigator of the event</param>
        protected Event(IEventSender s)
        {
            sender = s;
        }
    }
}
