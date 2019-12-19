using System;
using System.Diagnostics;
using System.IO;

namespace Sophia.Core
{
    public class CopyTask : Task
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private string copy_from;
        private string copy_to;
        private bool should_overwrite;

        //--------------------------------------------------------------------------------------
        public CopyTask(string from, string to, bool overwrite)
            :base()
        {
            copy_from = from;
            copy_to = to;
            should_overwrite = overwrite;
        }
        //--------------------------------------------------------------------------------------
        public CopyTask(string from, string to, bool overwrite, Action<string> log)
            :base(log)
        {
            copy_from = from;
            copy_to = to;
            should_overwrite = overwrite;
        }

        //--------------------------------------------------------------------------------------
        public override void execute()
        {
            if (onStarted != null)
                onStarted(this);

            if (File.Exists(copy_from))
            {
                File.Copy(copy_from, copy_to, should_overwrite);

                if(LogFnc != null)
                    LogFnc("File was copied to: " + copy_to);
            }
            else if(LogFnc != null) LogFnc("Cannot copy file from: " + copy_from + " to: " + copy_to + ". No such file directory");

            if (onFinished != null)
                onFinished(this);
        }
    }
}
