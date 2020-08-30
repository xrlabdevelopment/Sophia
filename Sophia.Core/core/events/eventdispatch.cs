using System;
using System.Collections.Generic;
using System.Linq;
using Sophia.Core.Utilities;

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
        public IEventHandler[] getHandlers(BitField category)
        {
            if (!handlers.ContainsKey(category.Value))
                return null;

            return handlers[category.Value].ToArray();
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the event handlers for all categories
        /// </summary>
        /// <returns>The event handlers for all categories</returns>
        public IEventHandler[][] getHandlers()
        {
            List<int> available_handlers = new List<int>();
            foreach(var pair in handlers)
            {
                if (pair.Value != null && pair.Value.Count > 0)
                    available_handlers.Add(pair.Key);
            }

            IEventHandler[][] all_handlers = new IEventHandler[available_handlers.Count][];
            for(int i = 0; i < available_handlers.Count; ++i)
            {
                all_handlers[i] = handlers[available_handlers[i]].ToArray();
            }

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
            System.Diagnostics.Debug.Assert(handler.EventCategory != null && handler.EventCategory.Value >= 0, "An event category cannot be smaller than are equal to 0");
            if (handler.EventCategory == null || handler.EventCategory.Value <= 0)
            {
                System.Diagnostics.Debug.WriteLine("Handler not created because of one of the following reasons: ");
                System.Diagnostics.Debug.WriteLine("Event category is null");
                System.Diagnostics.Debug.WriteLine("Event category smaller than or equal to zero");
                return;
            }

            if (handlers.ContainsKey(handler.EventCategory.Value))
            {
                handlers[handler.EventCategory.Value].Add(handler);
            }
            else
            {
                handlers.Add(handler.EventCategory.Value, new List<IEventHandler>() { handler });
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
            System.Diagnostics.Debug.Assert(evt.EventCategory != null && evt.EventCategory.Value >= 0, "An event category cannot be smaller than are equal to 0");
            if (evt.EventCategory == null || evt.EventCategory.Value <= 0)
            {
                System.Diagnostics.Debug.WriteLine("Event not dispatched because of one of the following reasons: ");
                System.Diagnostics.Debug.WriteLine("Event category is null");
                System.Diagnostics.Debug.WriteLine("Event category smaller than or equal to zero");
                return false;
            }

            bool handled = false;
            foreach (KeyValuePair<int, List<IEventHandler>> pair in handlers)
            {
                if ((evt.EventCategory & pair.Key) != 0)
                {
                    foreach (IEventHandler handler in pair.Value)
                        handled |= handler.handleEvent(evt);
                }
            }
            return handled;
        }
    }
}
