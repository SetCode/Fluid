using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Trace
{
    public interface ILoggable
    {
        void Debug(string msg);
        void Debug(string tag, string msg);
        void Throw(Exception e);
        void Throw(string tag, Exception e);
        void Info(string msg);
        void Info(string tag, string msg);
        void Info(LogCategory category, string msg);
        void Info(LogCategory category, string tag, string msg);
        void Warn(string msg);
        void Warn(string tag, string msg);
        void Warn(LogCategory category, string msg);
        void Warn(LogCategory category, string tag, string msg);
        void Error(string msg);
        void Error(string tag, string msg);
        void Error(LogCategory category, string msg);
        void Error(LogCategory category, string tag, string msg);
        void Fatal(string msg);
        void Fatal(string tag, string msg);
        void Fatal(LogCategory category, string msg);
        void Fatal(LogCategory category, string tag, string msg);
        void Crash(Exception e);
    }
}
