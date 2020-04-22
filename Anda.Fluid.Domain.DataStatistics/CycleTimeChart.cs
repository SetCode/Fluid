using Anda.Fluid.Infrastructure.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Anda.Fluid.Domain.DataStatistics
{

    ///<summary>
    /// Description	:CycleTime统计图表
    /// Author  	:liyi
    /// Date		:2019/07/03
    ///</summary>   
    public class CycleTimeChart : ChartBlue
    {
        public CycleTimeChart()
        {
            this.Dock = DockStyle.Fill;
            this.Chart.Titles[0].Text = "CT时间统计";
        }

        public void UpdateChartData(Dictionary<int, double> ctTimeDatas)
        {
            ctTimeDatas = ctTimeDatas.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
            List<int> DateTimeList = ctTimeDatas.Keys.ToList();
            List<double> ctTimeList = ctTimeDatas.Values.ToList();

            ctTimeList.Sort();
            Series s0 = AddSeries(0, 0);

            s0.ChartType = SeriesChartType.Line;
            s0.IsValueShownAsLabel = false;
            s0.MarkerStyle = MarkerStyle.Circle;
            s0.MarkerColor = System.Drawing.Color.Red;
            s0.MarkerSize = 5;
            s0.ShadowOffset = 3;
            s0.BorderWidth = 3;
            s0.ToolTip = "#VALY";

            this.Chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
            this.Chart.ChartAreas[0].AxisX.Minimum = DateTimeList[0];
            this.Chart.ChartAreas[0].AxisX.Maximum = DateTimeList[DateTimeList.Count - 1];
            this.Chart.ChartAreas[0].AxisY.Minimum = ctTimeList[0] - 1;
            this.Chart.ChartAreas[0].AxisY.Maximum = ctTimeList[ctTimeList.Count - 1] + 1;
            this.Chart.ChartAreas[0].AxisY.Title = "Cycle Time";
            this.Chart.ChartAreas[0].AxisX.Interval = 1;
            this.Chart.ChartAreas[0].AxisX.Title = "产品ID";
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Number;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = 2;

            foreach (int item in ctTimeDatas.Keys)
            {
                s0.Points.AddXY(item, ctTimeDatas[item]);
            }

            double mean = this.Chart.DataManipulator.Statistics.Mean(s0.Name);
            StripLine stripLine = new StripLine();
            stripLine.IntervalOffset = mean;
            stripLine.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(180)))), ((int)(((byte)(65)))));
            stripLine.BorderWidth = 2;
            stripLine.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stripLine.Text = string.Format("Mean:{0:G5}", mean);
            stripLine.TextLineAlignment = System.Drawing.StringAlignment.Far;
            this.Chart.ChartAreas[0].AxisY.StripLines.Add(stripLine);
        }
    }
}
