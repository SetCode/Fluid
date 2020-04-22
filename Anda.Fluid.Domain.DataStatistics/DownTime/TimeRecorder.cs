using Anda.Fluid.Domain.Data;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public  class TimeRecorder
    {
        public DateTime StartTime = DateTime.Now;
        public DateTime EndTime = DateTime.Now;
        public double span = 0;
        protected WorkState workState = WorkState.Normal;
        protected TimeType timeType = TimeType.All;
        public bool IsStarted = false;
        public bool IsEnded = false;
        public TimeRecorder()
        { }
        public TimeRecorder(WorkState workState, TimeType timeType)
        {
            this.workState = workState;
            this.timeType = timeType;
        }
        

        public void SetStartTime()
        {
            if (!this.IsStarted)
            {
                this.StartTime = DateTime.Now;
                this.IsStarted = true;
                this.IsEnded = false;
            }
            
        }
        public void SetStartTime(DateTime startTime)
        {
            this.StartTime = startTime;
        }

        public void SetEndTime()
        {
            if (!DbService.Instance.Enable)
            {
                return;
            }
            if (!this.IsEnded && this.IsStarted)
            {
                this.EndTime = DateTime.Now;
                //计算区间值
                this.span = (this.EndTime - this.StartTime).TotalMinutes;
                //写入数据库
                //this.WriteToDb();
                DbService.Instance.Fire(this.WriteToDb);
                this.IsEnded = true;
                this.IsStarted = false;
            }

        }

        public void SetEndTime(DateTime endTime)
        {
            if (!DbService.Instance.Enable)
            {
                return;
            }
            this.EndTime = endTime;
            //计算区间值
            this.span = Math.Round((this.EndTime - this.StartTime).TotalMinutes, 1);
            //写入数据库
            DbService.Instance.Fire(this.WriteToDb);

        }
        public void WriteToDb()
        {
            try
            {
                using (afmdbEntities ctx = new afmdbEntities())
                {
                    tbtimerecorder timeRecorder = new tbtimerecorder();
                    timeRecorder.workState = (int)this.workState;
                    timeRecorder.startTime = this.StartTime;
                    timeRecorder.endTime = this.EndTime;
                    timeRecorder.span = this.span;
                    timeRecorder.subType = (int)this.timeType;
                    ctx.tbtimerecorder.Add(timeRecorder);
                    ctx.SaveChangesAsync();
                }
            }
            catch
            {

            }

           

        }


    }
}
