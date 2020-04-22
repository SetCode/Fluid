using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Anda.Fluid.Infrastructure.Controls.Charts
{
    public partial class ChartBase : UserControl
    {
        private int chartAreaNum = 0;
        private int chartTitleNum = 0;
        private int chartLegendNum = 0;
        private int chartSeriesNum = 0;

        public Chart Chart => this.chart1;

        public ChartBase()
        {
            InitializeComponent();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();
            chart1.Series.Clear();
        }

        public virtual ChartArea AddChartArea()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.Name = string.Format("ChartArea{0}", chartAreaNum);
            chartAreaNum++;
            chart1.ChartAreas.Add(chartArea);
            return chartArea;
        }

        public virtual Title AddTitle(string text)
        {
            Title title = new Title();
            title.Name = string.Format("Title{0}", chartTitleNum);
            title.Text = text;
            chartTitleNum++;
            chart1.Titles.Add(title);
            return title;
        }

        public virtual Legend AddLegend()
        {
            Legend legend = new Legend();
            legend.Name = string.Format("Legend{0}", chartLegendNum);
            chartLegendNum++;
            chart1.Legends.Add(legend);
            return legend;
        }

        public virtual Series AddSeries(int chartAreaId, int legendId)
        {
            Series series = new Series();
            series.Name = string.Format("Series{0}", chartSeriesNum);
            series.ChartArea = string.Format("ChartArea{0}", chartAreaId);
            series.Legend = string.Format("Legend{0}", legendId);
            this.chartSeriesNum++;
            chart1.Series.Add(series);
            return series;
        }
    }
}
