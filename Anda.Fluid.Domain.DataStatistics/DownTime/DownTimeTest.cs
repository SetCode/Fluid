using Anda.Fluid.Domain.DataStatistics.DownTime;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    public partial class DownTimeTest : Form
    {
        
        
        public DownTimeTest()
        {
            InitializeComponent();
            downTimeChart = new DownTimeChart();
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(downTimeChart);
        }

        private DownTimeChart downTimeChart;
        Dictionary<TimeType, Dictionary<DateTime, double>> spansMap = new Dictionary<TimeType, Dictionary<DateTime, double>>();
        private void btnQuery_Click(object sender, EventArgs e)
        {
           if(!DbService.Instance.Enable)
            {
                return;
            }
            int count = this.dtEndQuery.Value.Hour - this.dtStartQuery.Value.Hour;
            DateTime startQuery = new DateTime(this.dtStartQuery.Value.Year, this.dtStartQuery.Value.Month, this.dtStartQuery.Value.Day, this.dtStartQuery.Value.Hour, 0, 0);
            DateTime endQuery = new DateTime(this.dtEndQuery.Value.Year, this.dtEndQuery.Value.Month, this.dtEndQuery.Value.Day, this.dtEndQuery.Value.Hour, 0, 0);
            if (startQuery.Subtract(endQuery)==TimeSpan.FromHours(0))
            {
                MessageBox.Show("查询时间不可以相同");
                return;
            }
            Dictionary<TimeType,List<TimeRecorder>> recorderMap=QueryDownTime.QueryAll(startQuery, endQuery);
            if (recorderMap==null)
            {
                return;
            }         
            double spanSum = 0;
            this.spansMap.Clear();
            foreach (var key in recorderMap.Keys)
            {
                List<TimeRecorder> timeRecorder = recorderMap[key];
                timeRecorder = timeRecorder.OrderBy(tr => tr.StartTime).ToList();
                Dictionary<DateTime, double> spans = new Dictionary<DateTime, double>();
                for (int i = 0; i <= count; i++)
                {
                    DateTime start = startQuery.AddHours(i);
                    DateTime end = startQuery.AddHours(i + 1);
                    spanSum = 0;

                    foreach (var item in timeRecorder)
                    {
                        if (item.StartTime < start)
                        {
                            if (item.EndTime > start && item.EndTime < end)
                            {
                                spanSum += (item.EndTime - start).TotalMinutes;
                            }
                            else if (item.EndTime >= end)
                            {
                                spanSum += (end - start).TotalMinutes;
                            }
                        }
                        else if (item.StartTime >= start && item.StartTime < end)
                        {
                            if (item.EndTime >= start && item.EndTime < end)
                            {
                                spanSum += item.span;
                            }
                            else if (item.EndTime > end)
                            {
                                spanSum += (end - item.StartTime).TotalMinutes;
                            }
                        }
                    }
                    spans.Add(start, spanSum);

                }
                this.spansMap.Add(key, spans);

            }
       
            this.downTimeChart.UpdateChartData2(this.spansMap);

        }


        private void btnDownTime_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                DateTime start = this.dtStart.Value.AddMinutes(i);               

                TimeRecorderMgr.Instance.WaitForBoard.SetStartTime(start);
                TimeRecorderMgr.Instance.WaitForBoard.SetEndTime(start.AddMinutes(1));

                TimeRecorderMgr.Instance.Spray.SetStartTime(start.AddMinutes(2));
                TimeRecorderMgr.Instance.Spray.SetEndTime(start.AddMinutes(2.5));

                TimeRecorderMgr.Instance.BreakDown.SetStartTime(start.AddMinutes(3));
                TimeRecorderMgr.Instance.BreakDown.SetEndTime(start.AddMinutes(10));

            }
           
        }
    }
}
