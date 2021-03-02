using Sophia.Core.Utilities;

namespace Sophia.Core.Events
{
    /// <summary>
    /// Interface of what an event should look like
    /// </summary>
    public interface IEvent
    {
        //-------------------------------------------------------------------------------------
        // Properties
        /// <summary>
        /// Did we handled this event or not
        /// </summary>
        bool Handled { get; set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        int EventType { get; }

        /// <summary>
        /// The category of the event
        /// </summary>
        BitField EventCategory { get; }

        /// <summary>
        /// The instigator of the event
        /// </summary>
        IEventSender Sender { get; }
    }
}
