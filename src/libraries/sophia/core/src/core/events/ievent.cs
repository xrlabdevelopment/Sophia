namespace Sophia.Core
{
    /// <summary>
    /// Inteface of what an event should look like
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// The type of the event
        /// </summary>
        int EventType { get; }

        /// <summary>
        /// The category of the event
        /// </summary>
        int EventCategory { get; }
        
        /// <summary>
        /// The instigator of the event
        /// </summary>
        IEventSender Sender { get; }
    }
}
