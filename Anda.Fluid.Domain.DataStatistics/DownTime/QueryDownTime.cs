using Anda.Fluid.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public class QueryDownTime
    {
        private static Dictionary<TimeType, List<TimeRecorder>> timeRecorders = new Dictionary<TimeType, List<TimeRecorder>>();        
       
        public static Dictionary<TimeType, List<TimeRecorder>> QueryAll(DateTime startTime, DateTime endTime)
        {
            
            DateTime endTime2 = endTime.AddHours(1);
            timeRecorders.Clear();
            try
            {
                using (afmdbEntities ctx = new afmdbEntities())
                {
                    IQueryable<tbtimerecorder> downTimes = from dt in ctx.tbtimerecorder
                                                           where (dt.startTime < startTime && dt.endTime > endTime) ||
                                                           (dt.startTime >= startTime && dt.startTime < endTime2)
                                                            && dt.span != null
                                                           select dt;
                    foreach (var item in downTimes)
                    {
                        TimeRecorder timeRec = new TimeRecorder((WorkState)item.workState, (TimeType)item.subType);
                        timeRec.StartTime = (DateTime)item.startTime;
                        timeRec.EndTime = (DateTime)item.endTime;
                        timeRec.span = (double)item.span;
                        AddTimeRecorder((TimeType)item.subType, timeRec);
                    }
                    return timeRecorders;

                }

            }
            catch 
            {
                return default(Dictionary<TimeType, List<TimeRecorder>>);
               
            }
            

        }
   
       
        public static void AddTimeRecorder(TimeType t, TimeRecorder timeRecorder)
        {            
            List<TimeRecorder> timeRecs = null;
            if (!timeRecorders.ContainsKey(t))
            {
                timeRecs = new List<TimeRecorder>();
                timeRecorders.Add(t, timeRecs);
            }
            else
            {
                timeRecs = timeRecorders[t];
            }
            timeRecs.Add(timeRecorder);

        }
        


    }
}
