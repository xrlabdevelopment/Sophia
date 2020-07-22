using System;
using System.Diagnostics;
using System.IO;
using Sophia.Diagnostics;

namespace Sophia
{
    namespace Threading
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
            private readonly CopyData[] copy_data;

            //--------------------------------------------------------------------------------------
            public CopyTask(CopyData[] copyData)
                : base()
            {
                copy_data = copyData;
            }
            //--------------------------------------------------------------------------------------
            public CopyTask(CopyData[] copyData, Logger logger)
                : base(logger)
            {
                copy_data = copyData;
            }

            //--------------------------------------------------------------------------------------
            public override void execute()
            {
                if (onStarted != null)
                    onStarted(this);

                foreach (CopyData data in copy_data)
                {
                    if (File.Exists(data.from))
                    {
                        File.Copy(data.from, data.to, data.overwrite);

                        if (Logger != null)
                            Logger.log("File was copied to: " + data.to);
                    }
                    else if (Logger != null)
                        Logger.log("Cannot copy file from: " + data.from + " to: " + data.to + ". No such file directory");
                }

                if (onFinished != null)
                    onFinished(this);
            }
        }
    }
}
