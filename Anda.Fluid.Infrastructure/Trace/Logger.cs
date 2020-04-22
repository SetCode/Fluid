using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Trace
{
    public class Logger : EntityBase<string>, ILoggable
    {
        private static readonly Logger @default = new Logger("log_default");
        public static Logger DEFAULT => @default;

        private ILogAppender logAppender;

        public LogCategory Category = LogCategory.ALL;

        public LogLevel Level = LogLevel.ALL;

        public bool UseDebugWrite = false;

        public Logger(string key) : base(key)
        {
            this.logAppender = new LogAppender(this);
        }

        public void Debug(string msg)
        {
            this.Debug(string.Empty, msg);
        }

        public void Debug(string tag, string msg)
        {
#if DEBUG
            this.logAppender.Append(LoggingEvent.Create(this.Key, LogCategory.CODE, LogLevel.DEBUG, tag, msg));
#endif
        }

        public void Throw(Exception e)
        {
            this.Throw(string.Empty, e);
        }

        public void Throw(string tag, Exception e)
        {
            this.logAppender.Append(LoggingEvent.Create(this.Key, LogCategory.CODE, LogLevel.EXEPT, tag, e));
        }

        public void Info(string msg)
        {
            this.Info(string.Empty, msg);
        }

        public void Info(string tag, string msg)
        {
            this.Info(LogCategory.ALL, tag, msg);
        }

        public void Info(LogCategory category, string msg)
        {
            this.Info(category, string.Empty, msg);
        }

        public void Info(LogCategory category, string tag, string msg)
        {
            this.logAppender.Append(LoggingEvent.Create(this.Key, category, LogLevel.INFO, tag, msg));
        }

        public void Warn(string msg)
        {
            this.Warn(string.Empty, msg);
        }

        public void Warn(string tag, string msg)
        {
            this.Warn(LogCategory.ALL, tag, msg);
        }

        public void Warn(LogCategory category, string msg)
        {
            this.Warn(category, string.Empty, msg);
        }

        public void Warn(LogCategory category, string tag, string msg)
        {
            this.logAppender.Append(LoggingEvent.Create(this.Key, category, LogLevel.WARN, tag, msg));
        }

        public void Error(string msg)
        {
            this.Error(string.Empty, msg);
        }

        public void Error(string tag, string msg)
        {
            this.Error(LogCategory.ALL, tag, msg);
        }

        public void Error(LogCategory category, string msg)
        {
            this.Error(category, string.Empty, msg);
        }

        public void Error(LogCategory category, string tag, string msg)
        {
            this.logAppender.Append(LoggingEvent.Create(this.Key, category, LogLevel.ERROR, tag, msg));
        }

        public void Fatal(string msg)
        {
            this.Fatal(string.Empty, msg);
        }

        public void Fatal(string tag, string msg)
        {
            this.Fatal(LogCategory.ALL, tag, msg);
        }

        public void Fatal(LogCategory category, string msg)
        {
            this.Fatal(category, string.Empty, msg);
        }

        public void Fatal(LogCategory category, string tag, string msg)
        {
            this.logAppender.Append(LoggingEvent.Create(this.Key, category, LogLevel.FATAL, tag, msg));
        }

        public void Crash(Exception ex)
        {
            this.logAppender.Append(LoggingEvent.Create(this.Key, LogCategory.CODE, LogLevel.CRASH, string.Empty, ex));
        }


        public void Init()
        {
            setUnhandledExceptionHandler();
        }

        private void setUnhandledExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += (sender, e) =>
            {
                onCrash(e.Exception);
            };
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                onCrash((Exception)e.ExceptionObject);
            };
        }

        private void onCrash(Exception e)
        {
            if (e == null)
            {
                return;
            }
            this.Crash(e);
        }

        public void Dispose()
        {
            this.logAppender.Dispose();
        }
    }
}
