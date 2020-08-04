using System;

namespace Sophia
{
    namespace Diagnostics
    {
        /// <summary>
        /// Function callbacks provided to " log " certain events.
        /// </summary>
        public struct LoggerCreationInfo
        {
            public Action<string> log_function;
            public Action<string> warn_function;
            public Action<string> error_function;
        }

        /// <summary>
        /// A class that will handle logging functionality inside Sophia
        /// This will handle support for externally generated loggers ( e.g.: UnityEngine.Debug )
        /// </summary>
        public class Logger
        {
            //-------------------------------------------------------------------------------------
            // Fields
            private readonly Action<string> log_function;
            private readonly Action<string> warn_function;
            private readonly Action<string> error_function;

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Constructor of this logger class
            /// </summary>
            /// <param name="logFn">Function we will invoke when the </param>
            public Logger(LoggerCreationInfo info)
            {
                log_function = info.log_function;
                warn_function = info.warn_function;
                error_function = info.error_function;
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Print an information log
            /// </summary>
            /// <param name="message">Message we would like to print</param>
            public void log(string message)
            {
                System.Diagnostics.Debug.Assert(log_function != null, "Log function was not specified");
                log_function(message);
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Print a warning
            /// </summary>
            /// <param name="message">Message we would like to print</param>
            public void warning(string message)
            {
                System.Diagnostics.Debug.Assert(log_function != null, "Log function was not specified");
                warn_function(message);
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Print an error
            /// </summary>
            /// <param name="message">Message we would like to print</param>
            public void error(string message)
            {
                System.Diagnostics.Debug.Assert(log_function != null, "Log function was not specified");
                error_function(message);
            }
        }
    }
}
