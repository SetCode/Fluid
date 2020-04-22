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
    /// Description	:生产过程异常信息数据统计表
    /// Author  	:liyi
    /// Date		:2019/07/03
    ///</summary>   
    public class NGInfoChart : ChartBlue
    {
        public NGInfoChart()
        {
            AddChartArea();
            this.Chart.Titles[0].Text = "分时生产异常数据";
            Title title2 = AddTitle("整体良率及产能统计");
            title2.DockedToChartArea = this.Chart.ChartAreas[1].Name;
            title2.Docking = Docking.Right;
            title2.TextOrientation = TextOrientation.Stacked;
            title2.IsDockedInsideChartArea = false;
            this.Chart.Legends[0].Enabled = true;
            Legend c2Legend = AddLegend();
            c2Legend.Enabled = false;
            this.Dock = DockStyle.Fill;
        }

        ///<summary>
        /// Description	:更新表格数据
        /// Author  	:liyi
        /// Date		:2019/07/03
        ///</summary>   
        /// <param name="ngInfoData">
        ///     List[0]:Mark异常次数
        ///     List[1]:测高异常次数
        ///     List[2]:中止次数
        ///     List[3]:气压异常次数
        ///     List[4]:其他异常次数
        /// </param>
        public void UpdateChartData(Dictionary<DateTime, List<int>> ngInfoData)
        {
            #region 分时统计表
            ngInfoData = ngInfoData.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
            DateTime[] times = ngInfoData.Keys.ToArray();
            //ChartArea chartArea2 = AddChartArea();
            //Mark 异常
            Series s0 = AddSeries(0, 0);
            //测高异常
            Series s1 = AddSeries(0, 0);
            //中止异常
            Series s2 = AddSeries(0, 0);
            //气压异常
            Series s3 = AddSeries(0, 0);
            //其他异常
            Series s4 = AddSeries(0, 0);

            s0.ChartType = SeriesChartType.StackedColumn100;
            s1.ChartType = SeriesChartType.StackedColumn100;
            s2.ChartType = SeriesChartType.StackedColumn100;
            s3.ChartType = SeriesChartType.StackedColumn100;
            s4.ChartType = SeriesChartType.StackedColumn100;

            s0.LegendText = "Mark异常";
            s1.LegendText = "测高异常";
            s2.LegendText = "手动停止";
            s3.LegendText = "气压异常";
            s4.LegendText = "其他异常";

            s0.IsValueShownAsLabel = true;
            s1.IsValueShownAsLabel = true;
            s2.IsValueShownAsLabel = true;
            s3.IsValueShownAsLabel = true;
            s4.IsValueShownAsLabel = true;

            s0.Label = "#PERCENT{P0}\r\nCount:#VAL";
            s1.Label = "#PERCENT{P0}\r\nCount:#VAL";
            s2.Label = "#PERCENT{P0}\r\nCount:#VAL";
            s3.Label = "#PERCENT{P0}\r\nCount:#VAL";
            s4.Label = "#PERCENT{P0}\r\nCount:#VAL";

            this.Chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            this.Chart.ChartAreas[0].AxisX.Minimum = times[0].AddHours(-1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.Maximum = times[ngInfoData.Count - 1].AddHours(1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.Interval = DateTime.Parse("01:01:00").Hour;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("02:00:00").Hour;

            this.Chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0}%";
            foreach (DateTime item in ngInfoData.Keys)
            {
                DataPoint[] dataPoint = new DataPoint[5];
                for (int i = 0; i < 5; i++)
                {
                    dataPoint[i] = new DataPoint(item.ToOADate(), ngInfoData[item][i]);
                    if (ngInfoData[item][i] == 0)
                    {
                        dataPoint[i].IsValueShownAsLabel = false;
                        dataPoint[i].Label = "";
                    }
                }
                s0.Points.Add(dataPoint[0]);
                s1.Points.Add(dataPoint[1]);
                s2.Points.Add(dataPoint[2]);
                s3.Points.Add(dataPoint[3]);
                s4.Points.Add(dataPoint[4]);
            }

            int err0Count = 0;
            int err1Count = 0;
            int err2Count = 0;
            int err3Count = 0;
            int err4Count = 0;
            err0Count = (int)Math.Ceiling(this.Chart.DataManipulator.Statistics.Mean(s0.Name) * ngInfoData.Count);
            err1Count = (int)Math.Ceiling(this.Chart.DataManipulator.Statistics.Mean(s1.Name) * ngInfoData.Count);
            err2Count = (int)Math.Ceiling(this.Chart.DataManipulator.Statistics.Mean(s2.Name) * ngInfoData.Count);
            err3Count = (int)Math.Ceiling(this.Chart.DataManipulator.Statistics.Mean(s3.Name) * ngInfoData.Count);
            err4Count = (int)Math.Ceiling(this.Chart.DataManipulator.Statistics.Mean(s4.Name) * ngInfoData.Count);
            #endregion

            #region 整体统计表
            Series c2s0 = AddSeries(1, 1);
            c2s0["PieLabelStyle"] = "Outside";

            this.Chart.Legends[0].DockedToChartArea = this.Chart.ChartAreas[1].Name;
            this.Chart.Legends[0].IsDockedInsideChartArea = false;
            this.Chart.Legends[0].Docking = Docking.Left;

            c2s0.ChartType = SeriesChartType.Doughnut;

            DataPoint err0DataPoint = new DataPoint(0, err0Count);
            err0DataPoint.CustomProperties = "OriginalPointIndex=0";
            err0DataPoint.IsValueShownAsLabel = true;
            err0DataPoint.Label = "#PERCENT Count:#VAL";

            DataPoint err1DataPoint = new DataPoint(0, err1Count);
            err1DataPoint.CustomProperties = "OriginalPointIndex=1";
            err1DataPoint.IsValueShownAsLabel = true;
            err1DataPoint.Label = "#PERCENT Count:#VAL";

            DataPoint err2DataPoint = new DataPoint(0, err2Count);
            err2DataPoint.CustomProperties = "OriginalPointIndex=2";
            err2DataPoint.IsValueShownAsLabel = true;
            err2DataPoint.Label = "#PERCENT Count:#VAL";

            DataPoint err3DataPoint = new DataPoint(0, err3Count);
            err3DataPoint.CustomProperties = "OriginalPointIndex=3";
            err3DataPoint.IsValueShownAsLabel = true;
            err3DataPoint.Label = "#PERCENT Count:#VAL";

            DataPoint err4DataPoint = new DataPoint(0, err4Count);
            err4DataPoint.CustomProperties = "OriginalPointIndex=4";
            err4DataPoint.IsValueShownAsLabel = true;
            err4DataPoint.Label = "#PERCENT Count:#VAL";

            c2s0.Points.Add(err0DataPoint);
            c2s0.Points.Add(err1DataPoint);
            c2s0.Points.Add(err2DataPoint);
            c2s0.Points.Add(err3DataPoint);
            c2s0.Points.Add(err4DataPoint);
            #endregion
        }
    }
}
