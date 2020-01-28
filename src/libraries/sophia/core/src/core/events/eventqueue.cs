using System.Collections.Generic;
using System.Threading;

namespace Sophia.Core
{
    public class EventQueue
    {
        //-------------------------------------------------------------------------------------
        // Fields
        private Queue<IEvent>           events;
        private Queue<Task>             tasks;
        
        private List<IEventHandler>     handlers;

        private bool                    locked;

        //-------------------------------------------------------------------------------------
        public EventQueue()
        {
            events      = new Queue<IEvent>();
            tasks       = new Queue<Task>();

            handlers    = new List<IEventHandler>();
        }

        //-------------------------------------------------------------------------------------
        public void subscribe(IEventHandler handler)
        {
            if (!handlers.Contains(handler))
                return;

            handlers.Add(handler);
        }
        //-------------------------------------------------------------------------------------
        public void unsubscribe(IEventHandler handler)
        {
            if (handlers.Contains(handler))
                return;

            handlers.Remove(handler);
        }

        //-------------------------------------------------------------------------------------
        public void push(IEvent evt)
        {
            // When there are no handlers it is of no use to push any events.
            if (handlers.Count == 0)
                return;

            events.Enqueue(evt);
        }
        //-------------------------------------------------------------------------------------
        public void broadcast()
        {
            // When nobody is handling our events ...
            // When there are no events to handle ...
            //      We do nothing.
            if (handlers.Count == 0 || events.Count == 0)
                return;


            if (!locked)
            {
                Task task = tasks.Count == 0
                    ? createBroadCastingTask()
                    : tasks.Dequeue();

                Thread thread = new Thread(task.execute);
                thread.Start();
            }
            else
            {
                tasks.Enqueue(createBroadCastingTask());
            }

            events.Clear();
        }

        //-------------------------------------------------------------------------------------
        private Task createBroadCastingTask()
        {
            Task task = new BroadCastTask(events, handlers);

            task.onStarted += startedBroadCasting;
            task.onFinished += stoppedBroadCasting;

            return task;
        }

        //-------------------------------------------------------------------------------------
        private void startedBroadCasting(Task task)
        {
            locked = true;
        }
        //-------------------------------------------------------------------------------------
        private void stoppedBroadCasting(Task task)
        {
            locked = false;
        }
    }
}
