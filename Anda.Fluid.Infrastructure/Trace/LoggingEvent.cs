using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    class LoggingEvent
    {
        public string LoggerName = string.Empty;

        public LogCategory Category = LogCategory.ALL;

        public LogLevel Level = LogLevel.ALL;

        public DateTime Time = DateTime.Now;

        public string Tag = string.Empty;

        public string Message = string.Empty;

        public Exception Exeption = null;

        public static LoggingEvent Create(string loggerName, LogCategory category, LogLevel level, string tag, string message)
        {
            return new LoggingEvent()
            {
                LoggerName = loggerName,
                Category = category,
                Level = level,
                Tag = tag,
                Message = message
            };
        }

        public static LoggingEvent Create(string loggerName, LogCategory category, LogLevel level, string tag, Exception ex)
        {
            return new LoggingEvent()
            {
                LoggerName = loggerName,
                Category = category,
                Level = level,
                Tag = tag,
                Message = string.Empty,
                Exeption = ex
            };
        }
    }
}
