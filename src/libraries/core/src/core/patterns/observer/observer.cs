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
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Called when a subject is broadcasting an event.
        /// </summary>
        /// <param name="subject">The sender</param>
        /// <param name="evt">The event that occured</param>
        bool notify(EventSubject subject, IEvent evt);
    }
}
