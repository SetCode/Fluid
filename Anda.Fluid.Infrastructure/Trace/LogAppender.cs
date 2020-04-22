using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Trace
{
    class LogAppender : ILogAppender
    {
        private Logger logger;
        private ILogFilter logFilter;
        private ILogLayout logLayout;
        private FileLog fileLog;

        public LogAppender(Logger logger)
        {
            this.logger = logger;
            this.logFilter = new LogFilter();
            this.logLayout = new LogLayout();
            this.fileLog = new FileLog();
        }

        public void Append(LoggingEvent e)
        {
            if(e.Level < this.logger.Level)
            {
                return;
            }
            if((e.Category & this.logger.Category) != e.Category)
            {
                return;
            }
            if(!this.logFilter.Filt(e))
            {
                return;
            }
            string s = this.logLayout.Layout(e);
            if (this.logger.UseDebugWrite)
            {
                Debug.WriteLine(s);
            }
            if (e.Level == LogLevel.CRASH)
            {
                this.fileLog.printOnCrash(e.Exeption);
                MessageBox.Show(e.Exeption.ToString());
            }
            else
            {
                this.fileLog.print(s);
            }
        }

        public void Dispose()
        {
            this.fileLog?.Dispose();
        }
    }
}
