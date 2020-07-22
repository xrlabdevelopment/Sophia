using System;
using Sophia.Diagnostics;

namespace Sophia
{
    namespace Threading
    {
        public abstract class Task
        {
            //--------------------------------------------------------------------------------------
            // Delegates
            public delegate void Finished(Task task);
            public delegate void Started(Task task);

            public Finished onFinished;
            public Started onStarted;

            //--------------------------------------------------------------------------------------
            // Properties
            protected Logger Logger
            {
                get;
                private set;
            }

            //--------------------------------------------------------------------------------------
            protected Task()
            {
                Logger = null;
            }
            //--------------------------------------------------------------------------------------
            protected Task(Logger logger)
            {
                Logger = logger;
            }

            //--------------------------------------------------------------------------------------
            public abstract void execute();
        }
    }
}
