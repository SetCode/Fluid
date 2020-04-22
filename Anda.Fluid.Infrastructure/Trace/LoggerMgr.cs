using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Trace
{
    public sealed class LoggerMgr : EntityMgr<Logger, string>
    {
        private static readonly LoggerMgr instance = new LoggerMgr();
        LoggerMgr(){}
        public static LoggerMgr Instance => instance;
    }
}
