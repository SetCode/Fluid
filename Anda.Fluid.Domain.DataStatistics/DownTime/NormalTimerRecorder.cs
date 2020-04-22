using Anda.Fluid.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public class NormalTimerRecorder:TimeRecorderBase
    {
        private NormalType normalType = NormalType.All;
        
        public NormalTimerRecorder(NormalType normalType)
        {
            this.workState = WorkState.Normal;
            this.normalType = normalType;
        }
        public override void WriteToDb()
        {
            using (afmdbEntities ctx=new afmdbEntities())
            {
                tbtimerecorder timeRecorder = new tbtimerecorder();
                timeRecorder.workState = (int)this.workState;
                timeRecorder.startTime = this.StartTime;
                timeRecorder.endTime = this.EndTime;
                timeRecorder.span = this.span;
                timeRecorder.subType = (int)this.normalType;
                ctx.tbtimerecorder.Add(timeRecorder);
            }
                
        }
    }
}
