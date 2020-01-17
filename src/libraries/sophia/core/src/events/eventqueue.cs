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
            events.Enqueue(evt);
        }
        //-------------------------------------------------------------------------------------
        public void broadcast()
        {
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
        private void startedBroadCasting(ITask task)
        {
            locked = true;
        }
        //-------------------------------------------------------------------------------------
        private void stoppedBroadCasting(ITask task)
        {
            locked = false;
        }
    }
}
