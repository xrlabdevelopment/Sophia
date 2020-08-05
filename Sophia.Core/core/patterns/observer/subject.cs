using System.Collections.Generic;
using Sophia.Core.Events;

namespace Sophia.Core.Patterns
{
    public interface ISubject
    {
        int ObserverCount { get; }

        void detachAll();

        TSubjectType getAs<TSubjectType>() where TSubjectType : class, ISubject;
    }

    public abstract class BaseSubject<T> : ISubject
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public int ObserverCount
        {
            get { return observers.Count; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private readonly List<T> observers;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the subject clas
        /// </summary>
        public BaseSubject()
        {
            observers = new List<T>();
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Attach a new observer to this subject
        /// </summary>
        /// <param name="observer">Observer to be attached</param>
        public virtual void attach(T observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Detach a specific observer from this subject
        /// </summary>
        /// <param name="observer">Observer to be detached</param>
        public virtual void detach(T observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Detach all observers from this subject
        /// </summary>
        public void detachAll()
        {
            observers.Clear();
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Convert this subject to another type
        /// </summary>
        /// <typeparam name="TSubjectType">The subject type we would like to retrieve.</typeparam>
        /// <returns>The requested subject type if convertible otherwise null.</returns>
        public TSubjectType getAs<TSubjectType>()
            where TSubjectType : class, ISubject
        {
            return this as TSubjectType;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// A list of all observers
        /// </summary>
        /// <returns>The list of containing all observers</returns>
        protected List<T> getObservers()
        {
            return observers;
        }
    }

    public class Subject<T> : BaseSubject<T>
        where T : IObserver
    {
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Notify all observers something happened
        /// </summary>
        /// <param name="evt">The event that occured</param>
        public virtual bool notify()
        {
            getObservers().RemoveAll(o => o == null);

            if (ObserverCount == 0)
                return false;

            bool handled = false;
            foreach (IObserver observer in getObservers())
                handled |= observer.notify(this);
            return handled;
        }
    }

    public class EventSubject<T> : BaseSubject<T>
        where T : IEventObserver
    {
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Notify all observers something happened
        /// </summary>
        /// <param name="evt">The event that occured</param>
        public virtual bool notify(IEvent evt)
        {
            getObservers().RemoveAll(o => o == null);

            if (ObserverCount == 0)
                return false;

            bool handled = false;
            foreach (IEventObserver observer in getObservers())
            {
                if ((observer.EventCategory & evt.EventCategory) != 0)
                    handled |= observer.notify(this, evt);
            }
            return handled;
        }
    }
}
