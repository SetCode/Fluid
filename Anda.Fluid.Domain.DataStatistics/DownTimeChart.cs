using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Controls.Charts;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using Anda.Fluid.Domain.DataStatistics.DownTime;

namespace Anda.Fluid.Domain.DataStatistics
{

    ///<summary>
    /// Description	:系统Down时间数据统计表
    /// Author  	:liyi
    /// Date		:2019/07/03
    ///</summary>   
    public class DownTimeChart : ChartBlue
    {
       
        private Series SeriesWaitBord;
        private Series SeriesBreakDown;
        private Series SeriesSpray;
        public DownTimeChart()
        {
            this.Dock = DockStyle.Fill;

            this.Chart.Titles[0].Text = "机台时间统计数据";
            this.Chart.Legends[0].Enabled = true;
            this.Chart.Legends[0].Docking = Docking.Bottom;
            
            SeriesWaitBord = AddSeries(0, 0);
            SeriesSpray = AddSeries(0, 0);
            SeriesBreakDown = AddSeries(0, 0);
            

            SeriesWaitBord.ChartType = SeriesChartType.Column;
            SeriesBreakDown.ChartType = SeriesChartType.Column;

            SeriesWaitBord.LegendText = "等板时间";
            SeriesBreakDown.LegendText = "机台故障时间";
            SeriesSpray.LegendText = "点胶时间";

        }

        ///<summary>
        /// Description	:更新图表数据
        /// Author  	:liyi
        /// Date		:2019/07/03
        ///</summary>   
        /// <param name="downTimeData">
        ///     List[0] : 故障停机时间
        ///     List[1] : 机台换胶时间
        ///     List[2] : 机台调试时间
        ///     List[3] : 其它原因停机时间
        /// </param>
        public void UpdateChartData(Dictionary<DateTime,List<TimeSpan>> downTimeData)
        {

            downTimeData = downTimeData.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
            DateTime[] times = downTimeData.Keys.ToArray();

            this.Chart.Titles[0].Text = "机台停机时间统计数据";
            this.Chart.Legends[0].Enabled = true;
            this.Chart.Legends[0].Docking = Docking.Bottom;
            Series s0 = AddSeries(0, 0);
            Series s1 = AddSeries(0, 0);
            Series s2 = AddSeries(0, 0);
            Series s3 = AddSeries(0, 0);

            s0.ChartType = SeriesChartType.Column;
            s1.ChartType = SeriesChartType.Column;
            s2.ChartType = SeriesChartType.Column;
            s3.ChartType = SeriesChartType.Column;

            s0.LegendText = "故障停机时间";
            s1.LegendText = "机台换胶时间";
            s2.LegendText = "机台调试时间";
            s3.LegendText = "其它原因停机时间";

//             s0.IsValueShownAsLabel = true;
//             s1.IsValueShownAsLabel = true;
//             s2.IsValueShownAsLabel = true;
//             s3.IsValueShownAsLabel = true;

            this.Chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            this.Chart.ChartAreas[0].AxisX.Minimum = times[0].AddHours(-1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.Maximum = times[downTimeData.Count - 1].AddHours(1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.Interval = DateTime.Parse("01:00:00").Hour;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("02:00:00").Hour;

            this.Chart.ChartAreas[0].AxisY.Title = "时间(分钟)";


            foreach (DateTime item in downTimeData.Keys)
            {
                DataPoint[] dataPoint = new DataPoint[5];
                for (int i = 0; i < 4; i++)
                {
                    DataPoint p = new DataPoint();
                    
                    dataPoint[i] = new DataPoint(item.ToOADate(), downTimeData[item][i].TotalMinutes);
                    if (downTimeData[item][i].TotalMinutes != 0)
                    {
                        dataPoint[i].IsValueShownAsLabel = true;
                        //dataPoint[i].Label = "";
                    }
                }
                s0.Points.Add(dataPoint[0]);
                s1.Points.Add(dataPoint[1]);
                s2.Points.Add(dataPoint[2]);
                s3.Points.Add(dataPoint[3]);
            }
        }

       
        public void UpdateChartData2(Dictionary<TimeType,Dictionary<DateTime, double>> downTimeData)
        {
            this.SeriesWaitBord.Points.Clear();
            this.SeriesBreakDown.Points.Clear();
            this.SeriesSpray.Points.Clear();
            if (downTimeData.Count==0)
            {                
                return;
            }          
            DateTime[] times = downTimeData.Values.First().Keys.ToArray();

            this.Chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            this.Chart.ChartAreas[0].AxisX.Minimum = times[0].AddHours(-1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.Maximum = times[times.Length - 1].AddHours(1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.Interval = DateTime.Parse("01:00:00").Hour;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("02:00:00").Hour;

            this.Chart.ChartAreas[0].AxisY.Title = "时间(分钟)";

            DataPoint dataPoint = null;            
            foreach (var type in downTimeData.Keys)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach (var dt in downTimeData[type].Keys)
                {
                    dataPoint = new DataPoint(dt.ToOADate(), downTimeData[type][dt]);
                    if (downTimeData[type][dt] != 0)
                    {
                        dataPoint.IsValueShownAsLabel = true;
                    }                   
                    dataPoints.Add(dataPoint);
                }

                if (type == TimeType.WaitForBoard)
                {                    
                    this.addPoints(this.SeriesWaitBord, dataPoints);
                }
                else if (type == TimeType.BreakDown)
                {
                    this.addPoints(this.SeriesBreakDown, dataPoints);
                }
                else if(type == TimeType.Spray)
                {
                    this.addPoints(this.SeriesSpray, dataPoints);
                }
            }
          
        }

        private void addPoints(Series series,List<DataPoint> points)
        {
            foreach (var item in points)
            {
                series.Points.Add(item);
            }            

        }
    }
}
