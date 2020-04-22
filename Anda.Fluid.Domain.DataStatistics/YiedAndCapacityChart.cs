using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Controls.Charts;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.DataStatistics
{
    public class YiedAndCapacityChart : ChartBlue
    {
        private Series seriesDoughnut;
        private Series seriesOk;
        private Series seriesNg;
        private Series seriesOkPercent;
        public YiedAndCapacityChart()
        {
            this.Dock = DockStyle.Fill;

            //环状图
            ChartArea chartArea2 = AddChartArea();
            this.seriesDoughnut = AddSeries(1, 0);
            this.seriesDoughnut.ChartType = SeriesChartType.Doughnut;
            this.seriesDoughnut["PieLabelStyle"] = "Outside";

            Title titleTotalPercent = AddTitle("整体良率及产能统计");
            titleTotalPercent.DockedToChartArea = this.Chart.ChartAreas[1].Name;
            titleTotalPercent.Docking = Docking.Right;
            titleTotalPercent.TextOrientation = TextOrientation.Stacked;
            titleTotalPercent.IsDockedInsideChartArea = false;
            //柱状折线图
            this.seriesOk = AddSeries(0, 0);
            this.seriesNg = AddSeries(0, 0);
            this.seriesOkPercent = AddSeries(0, 0);

        }

        public void UpdateChartData(Dictionary<DateTime, List<int>> yiedData)
        {
            if (yiedData.Count<=0)
            {
                return;
            }
            yiedData = yiedData.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
            UpdateDoughnutData(yiedData);
            UpdateLineAndBarData(yiedData);
            this.Chart.Legends[0].Enabled = true;
            this.Chart.Legends[0].Docking = Docking.Left;
            this.Chart.Titles[0].Text = "分时良率及产能统计";
           
            this.Chart.Legends[0].DockedToChartArea = this.Chart.ChartAreas[1].Name;
            this.Chart.Legends[0].IsDockedInsideChartArea = false;
        }

        ///<summary>
        /// Description	:更新整体良率环状图数据
        /// Author  	:liyi
        /// Date		:2019/07/02
        ///</summary>   
        /// <param name="okCount">生产OK数量，List每项为一小时数据</param>
        /// <param name="ngCount"></param>
        private void UpdateDoughnutData(Dictionary<DateTime, List<int>> yiedData)
        {
            int okTotalCount = 0;
            int ngTotalCount = 0;
            foreach (List<int> item in yiedData.Values)
            {
                okTotalCount += item[0];
                ngTotalCount += item[1];
            }
            int totalCount = okTotalCount + ngTotalCount;
            if (totalCount == 0)
            {
                totalCount = 1;
            }
            double okPercent = okTotalCount / (totalCount * 1.0f);
            double ngPercent = ngTotalCount / (totalCount * 1.0f);

            DataPoint okDataPoint = new DataPoint(0, okPercent);
            okDataPoint.CustomProperties = "OriginalPointIndex=0";
            okDataPoint.IsValueShownAsLabel = false;
            okDataPoint.Label = string.Format("OK:{0:F}%,{1}", okPercent * 100, okTotalCount);
            okDataPoint.LegendText = "Total ok data";

            DataPoint ngDataPoint = new DataPoint(0, ngPercent);
            ngDataPoint.CustomProperties = "OriginalPointIndex=1";
            ngDataPoint.IsValueShownAsLabel = false;
            ngDataPoint.Label = string.Format("NG:{0:F}%,{1}", ngPercent * 100, ngTotalCount);
            ngDataPoint.LegendText = "Total ng data";
            ngDataPoint.Color = System.Drawing.Color.Red;

            this.seriesDoughnut.Points.Clear();
            this.seriesDoughnut.Points.Add(okDataPoint);
            this.seriesDoughnut.Points.Add(ngDataPoint);
        }

        ///<summary>
        /// Description	:更新分时良率柱状折线图数据
        /// Author  	:liyi
        /// Date		:2019/07/02
        ///</summary>    
        private void UpdateLineAndBarData(Dictionary<DateTime, List<int>> yiedData)
        {
            DateTime[] times = yiedData.Keys.ToArray();
            List<int>[] CountList = yiedData.Values.ToArray();
            int maxCount = 0;
            foreach (List<int> item in CountList)
            {
                foreach (int count in item)
                {
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }
                }
            }
            if (maxCount<=0)
            {
                maxCount = 1;                
            }
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";            
            this.Chart.ChartAreas[0].AxisX.Minimum = times[0].AddHours(-1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.Maximum = times[yiedData.Count - 1].AddHours(1).ToOADate();
            this.Chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.Interval = DateTime.Parse("01:01:00").Hour;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("02:00:00").Hour;

            this.Chart.ChartAreas[0].AxisY.Minimum = 0;
            this.Chart.ChartAreas[0].AxisY.Maximum = 1.3 * maxCount;
            this.Chart.ChartAreas[0].AxisY.Interval = 1.3 * maxCount / 2;
            this.Chart.ChartAreas[0].AxisY.Title = "数量";

            this.Chart.ChartAreas[0].AxisY2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.Chart.ChartAreas[0].AxisY2.LabelStyle.Format = "0%";
            this.Chart.ChartAreas[0].AxisY2.TitleFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.Chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

            this.Chart.ChartAreas[0].AxisY2.Title = "良率";

           
            this.seriesOk.ChartType = SeriesChartType.StackedColumn;
            //this.seriesOk.IsValueShownAsLabel = true;
            this.seriesOk.Color = System.Drawing.Color.LightSeaGreen;
            this.seriesOk.LegendText = "OK count per hour";

            this.seriesNg.ChartType = SeriesChartType.StackedColumn;
            //this.seriesNg.IsValueShownAsLabel = true;
            this.seriesNg.LegendText = "NG count per hour";
            this.seriesNg.Color = System.Drawing.Color.Red;

            this.seriesOkPercent.ChartType = SeriesChartType.Line;
            this.seriesOkPercent.LegendText = "OK percent per hour";
            this.seriesOkPercent.MarkerSize = 7;
            this.seriesOkPercent.BorderWidth = 3;
            this.seriesOkPercent.MarkerStyle = MarkerStyle.Circle;
            this.seriesOkPercent.MarkerColor = System.Drawing.Color.Blue;
            this.seriesOkPercent.YAxisType = AxisType.Secondary;
            this.seriesOkPercent.Color = System.Drawing.Color.Tomato;
            //this.seriesOkPercent.IsValueShownAsLabel = true;
            this.seriesOkPercent.LabelFormat = @"{0%}";
            this.seriesOkPercent.ShadowOffset = 3;
            double minPercent = yiedData[times[0]][0] * 1.0f / (yiedData[times[0]][0] + yiedData[times[0]][1]);
            this.seriesOk.Points.Clear();
            this.seriesNg.Points.Clear();
            this.seriesOkPercent.Points.Clear();
            foreach (DateTime time in yiedData.Keys)
            {
                double okPercent = 0;
                if (yiedData[time][0] + yiedData[time][1]>0)
                {
                    okPercent = yiedData[time][0] * 1.0f / (yiedData[time][0] + yiedData[time][1]);
                    if (minPercent > okPercent)
                    {
                        minPercent = okPercent;
                    }
                }
                DataPoint PointOk = new DataPoint(time.ToOADate(), yiedData[time][0]);
                if (yiedData[time][0]>0)
                {
                    PointOk.IsValueShownAsLabel = true;
                }
                this.seriesOk.Points.Add(PointOk);
                DataPoint PointNg = new DataPoint(time.ToOADate(), yiedData[time][1]);
                if (yiedData[time][1] > 0)
                {
                    PointNg.IsValueShownAsLabel = true;
                }
                this.seriesNg.Points.Add(PointNg);
                DataPoint PointPercent = new DataPoint(time.ToOADate(), okPercent);
                if (okPercent > 0)
                {
                    PointPercent.IsValueShownAsLabel = true;
                }
                this.seriesOkPercent.Points.Add(PointPercent);
            }
            if (minPercent > 0.1)
            {
                minPercent = minPercent - 0.1;
            }
            this.Chart.ChartAreas[0].AxisY2.Minimum = minPercent;
            this.Chart.ChartAreas[0].AxisY2.Maximum = 1;
            this.Chart.ChartAreas[0].AxisY2.Interval = (1 - minPercent) / 10;
        }

        
    }
}
