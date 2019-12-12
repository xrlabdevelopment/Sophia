namespace Sophia.Core
{
    public interface IObserver
    {
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Called when a subject is broadcasting an event.
        /// </summary>
        /// <param name="subject">The sender</param>
        /// <param name="evt">The event that occured</param>
        void notify(Subject subject, IObserverEvent evt);
    }
}