using System.Collections.Generic;

namespace Sophia.Core.Events
{
    /// <summary>
    /// An event dispatcher
    /// </summary>
    public class EventDispatch
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public int CategoryCount
        {
            get { return handlers.Keys.Count; }
        }

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
        /// Retrieve the event handlers for a specific category
        /// </summary>
        /// <param name="category">The category we require</param>
        /// <returns>The event handlers for this category, null if it does not exist.</returns>
        public IEventHandler[] getHandlers(int category)
        {
            if (!handlers.ContainsKey(category))
                return null;

            return handlers[category].ToArray();
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the event handlers for all categories
        /// </summary>
        /// <returns>The event handlers for all categories</returns>
        public IEventHandler[][] getHandlers()
        {
            IEventHandler[][] all_handlers = new IEventHandler[handlers.Count][];

            foreach(var pair in handlers)
                all_handlers[pair.Key] = pair.Value.ToArray();

            return all_handlers;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Subscribe ourself to the event dispatcher
        /// This will make sure the handler is notified when an event is fired
        /// </summary>
        /// <param name="handler">The event handler to be notified</param>
        public void subscribe(IEventHandler handler)
        {
            // We should not handle negative event categories
            System.Diagnostics.Debug.Assert(handler.EventCategory >= 0, "An event category cannot be smaller than 0");
            if (handler.EventCategory < 0)
            {
                System.Diagnostics.Debug.WriteLine("Event category smaller than zero, this hander will not be added");
                return;
            }

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
            // We should not handle negative event categories
            System.Diagnostics.Debug.Assert(evt.EventCategory >= 0, "An event category cannot be smaller than 0");
            if (evt.EventCategory < 0)
            {
                System.Diagnostics.Debug.WriteLine("Event category smaller than zero, this event will not be handled");
                return false;
            }

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
                int event_category = evt.EventCategory + 1;
                int handler_category = pair.Key + 1;

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
