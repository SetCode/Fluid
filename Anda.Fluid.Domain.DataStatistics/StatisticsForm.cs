using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using static Anda.Fluid.Domain.DataStatistics.ProgramDataChart;
using Anda.Fluid.Domain.Data;
using MySql.Data.MySqlClient;
using Anda.Fluid.Domain.DataStatistics.DownTime;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.DataStatistics.YiedAndCapacity;

namespace Anda.Fluid.Domain.DataStatistics
{

    ///<summary>
    /// Description	:数据报表DEMO测试窗体
    /// Author  	:liyi
    /// Date		:2019/07/02
    ///</summary>   
    public partial class StatisticsForm : FormEx
    {
        private YiedAndCapacityChart yiedAndCapacityChart;
        private CycleTimeChart ctChart;
        private NGInfoChart ngInfoChart;
        private DownTimeChart downTimeChart;
        private ProgramDataChart programDataChart;
        public StatisticsForm()
        {
            InitializeComponent();
            this.ReadLanguageResources();

            this.yiedAndCapacityChart = new YiedAndCapacityChart();
            this.yiedAndCapacityChart.Dock = DockStyle.Fill;

            this.ctChart = new CycleTimeChart();
            this.ctChart.Dock = DockStyle.Fill;

            this.ngInfoChart = new NGInfoChart();
            this.ctChart.Dock = DockStyle.Fill;

            this.downTimeChart = new DownTimeChart();
            this.downTimeChart.Dock = DockStyle.Fill;

            this.programDataChart = new ProgramDataChart();
            this.programDataChart.Dock = DockStyle.Fill;

            UpdateChartData();
            this.pnlChartContainer.Controls.Add(this.yiedAndCapacityChart);
        }


        ///<summary>
        /// Description	:更新表格数据
        /// Author  	:liyi
        /// Date		:2019/07/02
        ///</summary>   
        public void UpdateChartData(int dataType = -1)
        {
            #region 良率及产能示例数据
            Dictionary<DateTime, List<int>> sampleYiedData = new Dictionary<DateTime, List<int>>();
            sampleYiedData.Add(DateTime.Parse("09:00"), new List<int>() { 185, 12 });
            sampleYiedData.Add(DateTime.Parse("10:00"), new List<int>() { 265, 22 });
            sampleYiedData.Add(DateTime.Parse("11:00"), new List<int>() { 192, 11 });
            sampleYiedData.Add(DateTime.Parse("12:00"), new List<int>() { 220, 30 });
            sampleYiedData.Add(DateTime.Parse("13:00"), new List<int>() { 150, 10 });
            sampleYiedData.Add(DateTime.Parse("14:00"), new List<int>() { 300, 4 });
            sampleYiedData.Add(DateTime.Parse("15:00"), new List<int>() { 120, 4 });
            sampleYiedData.Add(DateTime.Parse("16:00"), new List<int>() { 200, 6 });
            sampleYiedData.Add(DateTime.Parse("17:00"), new List<int>() { 211, 2 });
            sampleYiedData.Add(DateTime.Parse("18:00"), new List<int>() { 100, 1 });
            this.yiedAndCapacityChart.UpdateChartData(sampleYiedData);
            #endregion

            #region CT统计示例数据

            Dictionary<int, double> sampleCTData = new Dictionary<int, double>();
            Random random = new Random();
            for (int i = 0;i<100;i++)
            {
                sampleCTData.Add(i, 30 + random.Next(100) * 0.01);
            }
            this.ctChart.UpdateChartData(sampleCTData);
            #endregion

            #region 生产过程异常信息示例数据
            Dictionary<DateTime, List<int>> sampleNgInfoData = new Dictionary<DateTime, List<int>>();
            sampleNgInfoData.Add(DateTime.Parse("09:00"), new List<int>() { 1, 4, 1, 2, 4 });
            sampleNgInfoData.Add(DateTime.Parse("10:00"), new List<int>() { 2, 2, 1, 0, 4 });
            sampleNgInfoData.Add(DateTime.Parse("11:00"), new List<int>() { 3, 1, 0, 5, 4 });
            sampleNgInfoData.Add(DateTime.Parse("12:00"), new List<int>() { 4, 3, 5, 7, 4 });
            sampleNgInfoData.Add(DateTime.Parse("13:00"), new List<int>() { 5, 8, 8, 3, 4 });
            sampleNgInfoData.Add(DateTime.Parse("14:00"), new List<int>() { 6, 1, 2, 4, 4 });
            sampleNgInfoData.Add(DateTime.Parse("15:00"), new List<int>() { 7, 0, 8, 8, 4 });
            sampleNgInfoData.Add(DateTime.Parse("16:00"), new List<int>() { 8, 1, 2, 7, 4 });
            sampleNgInfoData.Add(DateTime.Parse("17:00"), new List<int>() { 9, 4, 1, 4, 4 });
            sampleNgInfoData.Add(DateTime.Parse("18:00"), new List<int>() { 0, 0, 0, 0, 0 });
            this.ngInfoChart.UpdateChartData(sampleNgInfoData);
            #endregion

            #region 停机时间信息示例数据
            Dictionary<DateTime, List<TimeSpan>> sampleDownTimeoData = new Dictionary<DateTime, List<TimeSpan>>();
            
            sampleDownTimeoData.Add(DateTime.Parse("09:00:00"), new List<TimeSpan>() { new TimeSpan(0,1,0), new TimeSpan(0, 2, 0), new TimeSpan(0, 3, 0), new TimeSpan(0, 4, 0), new TimeSpan(0, 5, 0) });
            sampleDownTimeoData.Add(DateTime.Parse("10:00:00"), new List<TimeSpan>() { new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0) });
            sampleDownTimeoData.Add(DateTime.Parse("11:00:00"), new List<TimeSpan>() { new TimeSpan(0,1,0), new TimeSpan(0,5,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,4,0) });
            sampleDownTimeoData.Add(DateTime.Parse("12:00:00"), new List<TimeSpan>() { new TimeSpan(0,4,0), new TimeSpan(0,1,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0) });
            sampleDownTimeoData.Add(DateTime.Parse("13:00:00"), new List<TimeSpan>() { new TimeSpan(0,7,0), new TimeSpan(0,7,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0) });
            sampleDownTimeoData.Add(DateTime.Parse("14:00:00"), new List<TimeSpan>() { new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,1,0) });
            sampleDownTimeoData.Add(DateTime.Parse("15:00:00"), new List<TimeSpan>() { new TimeSpan(0,0,0), new TimeSpan(0,1,0), new TimeSpan(0,1,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0) });
            sampleDownTimeoData.Add(DateTime.Parse("16:00:00"), new List<TimeSpan>() { new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,5,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0) });
            sampleDownTimeoData.Add(DateTime.Parse("17:00:00"), new List<TimeSpan>() { new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,12,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0) });
            sampleDownTimeoData.Add(DateTime.Parse("18:00:00"), new List<TimeSpan>() { new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(0,0,0), new TimeSpan(1,40,0) });
            this.downTimeChart.UpdateChartData(sampleDownTimeoData);
            #endregion

            #region 硬件读数示例数据
            Dictionary<int, double> sampleProgramData = new Dictionary<int, double>();
            Random random2 = new Random();
            double standrad = 0;
            for (int i = 0; i < 200; i++)
            {
                if (dataType == 0)//单点
                {
                    if (i%10 == 0)
                    {
                        if ((i/10)%2 == 0)
                        {
                            sampleProgramData.Add(i, 0.2 + random.Next(10) * 0.0001);
                        }
                        else
                        {
                            sampleProgramData.Add(i, 0.2 - random.Next(10) * 0.0001);
                        }
                    }
                    else
                    {
                        sampleProgramData.Add(i, 0.2 + random.Next(10) * 0.0001);
                    }
                    standrad = 0.205;
                }
                else if (dataType == 1)//测高
                {
                    if (i % 10 == 0)
                    {
                        if ((i / 10) % 2 == 0)
                        {
                            sampleProgramData.Add(i, 2 + random.Next(5) * 0.1 + random.Next(10) * 0.001);
                        }
                        else
                        {
                            sampleProgramData.Add(i, 2 - random.Next(5) * 0.1 - random.Next(10) * 0.001);
                        }
                    }
                    else
                    {
                        sampleProgramData.Add(i, 2 + random.Next(5) * 0.1 + random.Next(10) * 0.001);
                    }
                    standrad = 2.05;
                }
                else//温度
                {
                    if (i % 10 == 0)
                    {
                        if ((i / 10) % 2 == 0)
                        {
                            sampleProgramData.Add(i, 40 + random.Next(5) + random.Next(10) * 0.1);
                        }
                        else
                        {
                            sampleProgramData.Add(i, 40 - random.Next(5) - random.Next(10) * 0.1);
                        }
                    }
                    else
                    {
                        sampleProgramData.Add(i, 40 + random.Next(5) + random.Next(10) * 0.1);
                    }
                    standrad = 40;
                }
            }
            
            this.programDataChart.UpdateChartData(sampleProgramData,(DataType)dataType,standrad,0.1);
            #endregion
        }

        private void rdoCapacityChart_CheckedChanged(object sender, EventArgs e)
        {
            this.yiedAndCapacityChart = new YiedAndCapacityChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.yiedAndCapacityChart);
            UpdateChartData();
        }

        private void rdoCTChart_CheckedChanged(object sender, EventArgs e)
        {
            this.ctChart = new CycleTimeChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.ctChart);
            UpdateChartData();
        }

        private void rdoNGInfoChart_CheckedChanged(object sender, EventArgs e)
        {
            this.ngInfoChart = new NGInfoChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.ngInfoChart);
            UpdateChartData();
        }

        private void rdoDownTimeChart_CheckedChanged(object sender, EventArgs e)
        {
            this.downTimeChart = new DownTimeChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.downTimeChart);
            UpdateChartData();
        }

        private void rdoDotWeight_CheckedChanged(object sender, EventArgs e)
        {
            this.programDataChart = new ProgramDataChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.programDataChart);
            UpdateChartData(0);
        }

        private void rdoLaserData_CheckedChanged(object sender, EventArgs e)
        {
            this.programDataChart = new ProgramDataChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.programDataChart);
            UpdateChartData(1);
        }

        private void rdoTemperature_CheckedChanged(object sender, EventArgs e)
        {
            this.programDataChart = new ProgramDataChart();
            this.pnlChartContainer.Controls.Clear();
            this.pnlChartContainer.Controls.Add(this.programDataChart);
            UpdateChartData(2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mysql = "dataBase=root;Password=ANDA";
            MySqlConnection con;
            try
            {
                con = new MySqlConnection(mysql);
                //MessageBox.Show("ok");
                MessageBox.Show("成功");
            }
            catch (Exception)
            {
                //MessageBox.Show("failed");
                MessageBox.Show("失败");
            }
        //  conveyor conveyor = new conveyor();
        //  conveyor.MachineId = 123;
        //  afmdbEntities afmdbEntities = new afmdbEntities();
        //  afmdbEntities.conveyor.Add(conveyor);
        //  afmdbEntities.SaveChanges();
        }

        private void btnDownTime_Click(object sender, EventArgs e)
        {
            new DownTimeTest().ShowDialog();
        }

        private void ckbEnable_CheckedChanged(object sender, EventArgs e)
        {
            DbService.Instance.Enable = this.ckbEnable.Checked;
        }

        private void btnCapacity_Click(object sender, EventArgs e)
        {
            new YiedAndCapacityForm().ShowDialog();
        }
    }

    //public enum DownTime
    //{
    //    BreakDown
    //}
}
