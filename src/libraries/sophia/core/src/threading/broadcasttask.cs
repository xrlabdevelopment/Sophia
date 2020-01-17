using System.Collections.Generic;

namespace Sophia.Core
{
    public class BroadCastTask : Task
    {
        private Queue<IEvent> event_queue;
        private List<IEventHandler> event_handlers;

        //-------------------------------------------------------------------------------------
        public BroadCastTask(Queue<IEvent> queue, List<IEventHandler> handlers)
        {
            event_queue = queue;
            event_handlers = handlers;
        }

        //-------------------------------------------------------------------------------------
        public override void execute()
        {
            if(onStarted != null)
                onStarted(this);

            while(event_queue.Count > 0)
            {
                IEvent evt = event_queue.Dequeue();
                foreach (IEventHandler handler in event_handlers)
                    handler.handleEvent(evt);
            }

            if (onFinished != null)
                onFinished(this);
        }
    }
}
