using System.Collections.Generic;

namespace Sophia.Core
{
    /// <summary>
    /// An event dispatcher
    /// </summary>
    public class EventDispatch
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private readonly Dictionary<int, List<IEventHandler>> handlers;

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor of the event dispatcher
        /// </summary>
        public EventDispatch()
        {
            handlers = new Dictionary<int, List<IEventHandler>>();
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Subscribe ourselfs to the event dispatcher
        /// This will make sure the handler is notified when an event is fired
        /// </summary>
        /// <param name="handler">The event handler to be notified</param>
        public void subscribe(IEventHandler handler)
        {
            if (handlers.ContainsKey(handler.EventCategory))
            {
                handlers[handler.EventCategory].Add(handler);
            }
            else
            {
                handlers.Add(handler.EventCategory, new List<IEventHandler>() { handler });
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Unsubscribe ourselfs from this event dispatcher
        /// This will remove the handler from being notified when an event is fired
        /// </summary>
        /// <param name="handler">The handler to be unsubscribed</param>
        public void unsubscribe(IEventHandler handler)
        {
            foreach(KeyValuePair<int, List<IEventHandler>> pair in handlers)
                pair.Value.Remove(handler);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Fire this function to dispatch an event to all subscribed handlers
        /// </summary>
        /// <param name="evt">The event to be dispatched</param>
        /// <returns>If the event was handled by one of the handlers this will return true, otherwise it will return false</returns>
        public bool dispatch(IEvent evt)
        {
            bool handled = false;
            foreach (KeyValuePair<int, List<IEventHandler>> pair in handlers)
            {
                if ((evt.EventCategory & pair.Key) != 0)
                {
                    foreach(IEventHandler handler in pair.Value)
                        handled |= handler.handleEvent(evt);
                }
            }
            return handled;
        }
    }
}
