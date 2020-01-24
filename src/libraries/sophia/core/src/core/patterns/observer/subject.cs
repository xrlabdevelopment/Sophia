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
        public virtual void notify(IEvent evt)
        {
            if (observers.Count == 0)
                return;

            // Remove all null objects
            observers.RemoveAll(o => o == null);

            foreach(IObserver observer in observers)
                observer.notify(this, evt);
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
}
