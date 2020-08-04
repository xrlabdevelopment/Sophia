using System.Collections.Generic;

namespace Sophia
{
    namespace Events
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
            /// Subscribe ourself to the event dispatcher
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
            /// Un-subscribe ourself from this event dispatcher
            /// This will remove the handler from being notified when an event is fired
            /// </summary>
            /// <param name="handler">The handler to be unsubscribed</param>
            public void unsubscribe(IEventHandler handler)
            {
                foreach (KeyValuePair<int, List<IEventHandler>> pair in handlers)
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
                    //
                    // We are required to add 1 to our category because using the AND operator on a category that is already zero,
                    //  will end up in not handling the event. ( event_category & handler_category )
                    //
                    // It will be very confusing from a client perspective to instantiate event categories starting from 1.
                    //
                    // Hence we resolve the issue here by adding one to the category before testing it.
                    //
                    int event_category      = evt.EventCategory + 1;
                    int handler_category    = pair.Key + 1;

                    if ((event_category & handler_category) != 0)
                    {
                        foreach (IEventHandler handler in pair.Value)
                            handled |= handler.handleEvent(evt);
                    }
                }
                return handled;
            }
        }
    }
}
