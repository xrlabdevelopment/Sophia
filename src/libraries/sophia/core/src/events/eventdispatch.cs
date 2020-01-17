using System.Collections.Generic;

namespace Sophia.Core
{
    public class EventDispatch
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private List<IEventHandler> handlers;

        //-------------------------------------------------------------------------------------
        public EventDispatch()
        {
            handlers = new List<IEventHandler>();
        }

        //-------------------------------------------------------------------------------------
        public void subscribe(IEventHandler handler)
        {
            if (handlers.Contains(handler))
                return;

            handlers.Add(handler);
        }
        //-------------------------------------------------------------------------------------
        public void unsubscribe(IEventHandler handler)
        {
            if (!handlers.Contains(handler))
                return;

            handlers.Remove(handler);
        }

        //-------------------------------------------------------------------------------------
        public void dispatch(IEvent evt)
        {
            foreach (IEventHandler handler in handlers)
                handler.handleEvent(evt);
        }
    }
}
