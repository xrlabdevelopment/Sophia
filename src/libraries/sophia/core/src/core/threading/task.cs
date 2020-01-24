using System;

namespace Sophia.Core
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
        protected Action<string> LogFnc
        {
            get;
            private set;
        }

        //--------------------------------------------------------------------------------------
        protected Task()
        {
            LogFnc = null;
        }
        //--------------------------------------------------------------------------------------
        protected Task(Action<string> log)
        {
            LogFnc = log;
        }

        //--------------------------------------------------------------------------------------
        public abstract void execute();
    }
}
