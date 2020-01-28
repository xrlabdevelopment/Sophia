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
<<<<<<< HEAD:src/libraries/sophia/core/src/patterns/observer/observer.cs
        bool notify(EventSubject subject, IObserverEvent evt);
=======
        void notify(Subject subject, IEvent evt);
>>>>>>> bug/reorder_files:src/libraries/sophia/core/src/core/patterns/observer/observer.cs
    }
}
