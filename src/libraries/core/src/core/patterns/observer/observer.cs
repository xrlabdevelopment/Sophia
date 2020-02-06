namespace Sophia.Core
{
    public interface IObserver
    {
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Called when a subject is broadcasting an event.
        /// </summary>
        /// <param name="subject">The sender</param>
        bool notify(Subject subject);
    }

    public interface IEventObserver
    {
        /// <summary>
        /// If the event that is fired is equal to the eventcategory of the handler
        /// The event will be passed along, otherwise this handler will be skipped
        /// </summary>
        int EventCategory { get; }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Called when a subject is broadcasting an event.
        /// </summary>
        /// <param name="subject">The sender</param>
        /// <param name="evt">The event that occured</param>
        bool notify(EventSubject subject, IEvent evt);
    }
}
