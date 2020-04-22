using Anda.Fluid.Domain.Data;
using Anda.Fluid.Domain.DataStatistics.DownTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public class TimeRecorderMgr
    {
        private static readonly TimeRecorderMgr instance=new TimeRecorderMgr();
        private TimeRecorderMgr() { }
        public static TimeRecorderMgr Instance => instance;

        public TimeRecorder Running = new TimeRecorder(WorkState.Normal,TimeType.All);

        public TimeRecorder WaitForBoard = new TimeRecorder(WorkState.Normal,TimeType.WaitForBoard);
        public TimeRecorder Spray = new TimeRecorder(WorkState.Normal,TimeType.Spray);

        public TimeRecorder BreakDown = new TimeRecorder(WorkState.Abnormal,TimeType.BreakDown);
       
    }
}
