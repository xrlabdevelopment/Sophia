using Sophia.Core.Utilities;

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
        public abstract BitField EventCategory { get; }

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

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Helper function to check if an event is consumed
        /// </summary>
        /// <param name="evt">The event we need to check</param>
        /// <returns>Return true if it is consumed, false if it is not</returns>
        public static bool isConsumed(IEvent evt)
        {
            return evt is IConsumable && evt.Handled;
        }
    }
}
