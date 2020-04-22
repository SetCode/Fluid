using Anda.Fluid.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public class AbnormalTimerRecorder:TimeRecorderBase
    {
        private AbNormalType abNormalType = AbNormalType.BreakDown;
        public AbnormalTimerRecorder(AbNormalType abNormalType)
        {
            this.workState = WorkState.Abnormal;
            this.abNormalType = abNormalType;
        }
        public override void WriteToDb()
        {
            using (afmdbEntities ctx=new afmdbEntities())
            {
                tbtimerecorder timeRecorder = new tbtimerecorder();
                timeRecorder.workState = (int)this.workState;
                timeRecorder.startTime = this.StartTime;
                timeRecorder.endTime = this.EndTime;
                timeRecorder.subType = (int)this.abNormalType;
                ctx.tbtimerecorder.Add(timeRecorder);
            }
                
        }
    }
}
