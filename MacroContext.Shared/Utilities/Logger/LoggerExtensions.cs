using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Shared.Utilities.Logger
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message)
        {
            var entry = new LogEntry(LoggingEventType.Information, message, null);
            logger.Log(entry);
        }

        public static void Log(this ILogger logger, string message, Exception exception)
        {
            var entry = new LogEntry(LoggingEventType.Error, exception.Message, exception);
            logger.Log(entry);
        }

        public static void Log(this ILogger logger, LoggingEventType eventType, string message, Exception exception = null)
        {
            var entry = new LogEntry(eventType, exception.Message, exception);
            logger.Log(entry);
        }
    }
}
