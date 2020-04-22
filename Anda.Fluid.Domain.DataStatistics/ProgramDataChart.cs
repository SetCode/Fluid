using Anda.Fluid.Infrastructure.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Anda.Fluid.Domain.DataStatistics
{

    ///<summary>
    /// Description	:机台硬件数据表
    /// Author  	:liyi
    /// Date		:2019/07/03
    ///</summary>   
    public class ProgramDataChart : ChartBlue
    {
        public enum DataType
        {
            DotWeight = 0,
            Height,
            Temperature
        }
        public ProgramDataChart()
        {
            this.Dock = DockStyle.Fill;
        }

        ///<summary>
        /// Description	:更新硬件数据记录
        /// Author  	:liyi
        /// Date		:2019/07/03
        ///</summary>      
        /// <param name="programData">传入数据</param>
        /// <param name="dataType">
        ///     0:单点数据
        ///     1:测高数据
        ///     2:温度数据
        /// </param>
        public void UpdateChartData(Dictionary<int,double> programData,DataType dataType,double standradValue,double offset)
        {
            #region 折线图
            programData = programData.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
            List<int> index = programData.Keys.ToList();
            List<double> dataList = programData.Values.ToList();
            dataList.Sort();
            if (dataType == DataType.DotWeight)
            {
                this.Chart.Titles[0].Text = "单点重量数据";
                this.Chart.ChartAreas[0].AxisY.Title = "点重(mg)";
            }
            else if (dataType == DataType.Height)
            {
                this.Chart.Titles[0].Text = "测高数据";
                this.Chart.ChartAreas[0].AxisY.Title = "读数(mm)";
            }
            else if(dataType == DataType.Temperature)
            {
                this.Chart.Titles[0].Text = "温度数据";
                this.Chart.ChartAreas[0].AxisY.Title = "读数(℃)";
            }

            Series s0 = AddSeries(0, 0);
            s0.ChartType = SeriesChartType.Line;
            s0.MarkerStyle = MarkerStyle.Circle;
            s0.MarkerColor = System.Drawing.Color.Red;
            s0.MarkerSize = 5;
            s0.ShadowOffset = 3;
            s0.BorderWidth = 2;
            this.Chart.ChartAreas[0].AxisY.Minimum = dataList[0] * 0.8;
            this.Chart.ChartAreas[0].AxisY.Maximum = dataList[dataList.Count - 1] * 1.2;
            this.Chart.ChartAreas[0].AxisX.Interval = 5;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Number;
            this.Chart.ChartAreas[0].AxisX.LabelStyle.Interval = 20;

            foreach (int key in programData.Keys)
            {
                s0.Points.AddXY(key, programData[key]);
            }
            foreach (DataPoint point in s0.Points)
            {
                point.ToolTip = "#VALY";
            }

            StripLine upperLine = new StripLine();
            upperLine.IntervalOffset = standradValue*(1+offset);
            upperLine.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(180)))), ((int)(((byte)(65)))));
            upperLine.BorderWidth = 2;
            upperLine.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            upperLine.Text = string.Format("Upper Limit:{0:G5}", standradValue * (1 + offset));
            upperLine.TextLineAlignment = System.Drawing.StringAlignment.Far;

            StripLine DownLine = new StripLine();
            DownLine.IntervalOffset = standradValue * (1 - offset);
            DownLine.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(180)))), ((int)(((byte)(65)))));
            DownLine.BorderWidth = 2;
            DownLine.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            DownLine.Text = string.Format("Down Limit:{0:G5}", standradValue * (1 + offset));
            DownLine.TextLineAlignment = System.Drawing.StringAlignment.Far;

            this.Chart.ChartAreas[0].AxisY.StripLines.Add(upperLine);
            this.Chart.ChartAreas[0].AxisY.StripLines.Add(DownLine);

            #endregion

            #region 正态分布
            ChartArea chartArea2 = AddChartArea();
            Series s1 = AddSeries(1, 0);
            s1.ChartType = SeriesChartType.Area;

            double average = dataList.Average();
            double sum = dataList.Sum(d => Math.Pow(d-average,2));

            double σ = Math.Sqrt(sum / dataList.Count);//this.Chart.DataManipulator.Statistics.Variance(s1.Name,false);
            double μ = average;

            // Calculate coefficient
            double y;
            double coef = 1.0 / (Math.Sqrt(2 * Math.PI) * σ);
            double doubleX;

            // Fill data points with values from Normal distribution
            double maxY = 0;
            double minX = 9999;
            double maxX = -9999;
            //foreach(double value in dataList)
            for(double i = -50 * σ;i <= 50* σ;i+= σ)
            {
                doubleX = i/10;
                y = coef * Math.Exp(- (doubleX * doubleX) / (2 * σ * σ));
                s1.Points.AddXY(doubleX, y);
                if (y>maxY)
                {
                    maxY = y;
                }
                if (doubleX > maxX)
                {
                    maxX = doubleX;
                }
                if (doubleX < minX)
                {
                    minX = doubleX;
                }
            }
            // Set axis values
            chartArea2.AxisY.Minimum = 0;
            chartArea2.AxisY.Maximum = maxY*1.05;
            chartArea2.AxisX.Minimum = minX;
            chartArea2.AxisX.Maximum = maxX;
            chartArea2.AxisX.Interval = σ;
            chartArea2.AxisX.LabelStyle.IsStaggered = false;
            chartArea2.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Number;
            chartArea2.AxisX.LabelStyle.Interval = σ;
            chartArea2.AxisX.LabelStyle.Format = "{0:G5}";



            foreach (DataPoint point in this.Chart.Series[1].Points)
            {
                if ( standradValue*0.1 < Math.Abs(point.XValue))
                {
                    point.Color = Color.FromArgb(255, 65, 140, 240);
                }
                else
                {
                    point.Color = Color.FromArgb(255, 224, 64, 10);
                }
            }

            //chartArea2.AxisX.IntervalOffset = 1 * σ;
            #endregion
        }
    }
}
