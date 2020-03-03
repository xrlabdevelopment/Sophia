using System;
using System.Diagnostics;
using System.IO;

namespace Sophia.Threading
{
    public class CopyData
    {
        public string from;
        public string to;
        public bool overwrite;
    }

    public class CopyTask : Task
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private CopyData[] copy_data;

        //--------------------------------------------------------------------------------------
        public CopyTask(CopyData[] copyData)
            :base()
        {
            copy_data = copyData;
        }
        //--------------------------------------------------------------------------------------
        public CopyTask(CopyData[] copyData, Action<string> log)
            :base(log)
        {
            copy_data = copyData;
        }

        //--------------------------------------------------------------------------------------
        public override void execute()
        {
            if (onStarted != null)
                onStarted(this);

            foreach(CopyData data in copy_data)
            {
                if (File.Exists(data.from))
                {
                    File.Copy(data.from, data.to, data.overwrite);

                    if (LogFnc != null)
                        LogFnc("File was copied to: " + data.to);
                }
                else if (LogFnc != null)
                    LogFnc("Cannot copy file from: " + data.from + " to: " + data.to + ". No such file directory");
            }

            if (onFinished != null)
                onFinished(this);
        }
    }
}
