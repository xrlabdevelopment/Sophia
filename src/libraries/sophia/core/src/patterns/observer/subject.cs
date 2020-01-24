using System.Collections.Generic;

namespace Sophia.Core
{
    public class Subject
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public int ObserverCount
        {
            get { return observers.Count; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private List<IObserver> observers;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the subject clas
        /// </summary>
        public Subject()
        {
            observers = new List<IObserver>();
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Attach a new observer to this subject
        /// </summary>
        /// <param name="observer">Observer to be attached</param>
        public virtual void attach(IObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Detach a specific observer from this subject
        /// </summary>
        /// <param name="observer">Observer to be detached</param>
        public virtual void detach(IObserver observer)
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
        /// Notify all observers something happend
        /// </summary>
        /// <param name="evt">The event that occured</param>
        public virtual bool notify()
        {
            observers.RemoveAll(o => o == null);

            if (observers.Count == 0)
                return false;

            bool handled = false;
            foreach (IObserver observer in observers)
                handled |= observer.notify(this);
            return handled;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// A list of all observers
        /// </summary>
        /// <returns>The list of containing all observers</returns>
        protected List<IObserver> getObservers()
        {
            return observers;
        }
    }

    public class EventSubject
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public int ObserverCount
        {
            get { return observers.Count; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private List<IEventObserver> observers;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the subject clas
        /// </summary>
        public EventSubject()
        {
            observers = new List<IEventObserver>();
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Attach a new observer to this subject
        /// </summary>
        /// <param name="observer">Observer to be attached</param>
        public virtual void attach(IEventObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Detach a specific observer from this subject
        /// </summary>
        /// <param name="observer">Observer to be detached</param>
        public virtual void detach(IEventObserver observer)
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
        /// Notify all observers something happend
        /// </summary>
        /// <param name="evt">The event that occured</param>
        public virtual bool notify(IObserverEvent evt)
        {
            observers.RemoveAll(o => o == null);

            if (observers.Count == 0)
                return false;

            bool handled = false;
            foreach (IEventObserver observer in observers)
                handled |= observer.notify(this, evt);
            return handled;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// A list of all observers
        /// </summary>
        /// <returns>The list of containing all observers</returns>
        protected List<IEventObserver> getObservers()
        {
            return observers;
        }
    }
}
