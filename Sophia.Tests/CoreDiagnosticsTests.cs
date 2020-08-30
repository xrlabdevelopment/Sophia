using Sophia.Core.Diagnostics;

using NUnit.Framework;

namespace Sophia.Tests.Core
{
    [TestFixture]
    public class testsuit_Logger
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_logger_log()
        {
            string log_message = "my_message";

            LoggerCreationInfo info = new LoggerCreationInfo();

            info.log_function = delegate (string message)
            {
                Assert.That(message, Is.EqualTo(log_message));
            };

            Logger logger = new Logger(info);
            logger.log(log_message);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_logger_warn()
        {
            string log_message = "my_message";

            LoggerCreationInfo info = new LoggerCreationInfo();

            info.warn_function = delegate (string message)
            {
                Assert.That(message, Is.EqualTo(log_message));
            };

            Logger logger = new Logger(info);
            logger.warning(log_message);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_logger_error()
        {
            string log_message = "my_message";

            LoggerCreationInfo info = new LoggerCreationInfo();

            info.error_function = delegate (string message)
            {
                Assert.That(message, Is.EqualTo(log_message));
            };

            Logger logger = new Logger(info);
            logger.error(log_message);
        }
    }
}
