using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Shared.Utilities.Logger
{
    public class LogEntry
    {
        public LoggingEventType Severity { get; }
        public string Message { get; }
        public Exception Exception { get; }

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (message == string.Empty) throw new ArgumentException("empty", "message");

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
