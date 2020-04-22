using Anda.Fluid.Domain.Data;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.DataStatistics.YiedAndCapacity
{
    public partial class YiedAndCapacityForm : Form
    {
        public YiedAndCapacityForm()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (!DbService.Instance.Enable)
            {
                return;
            }
            string start = this.dateTimePickerQueryStart.Value.ToString("yyyy-MM-dd HH:00:00");
            string end = this.dateTimePickerQueryEnd.Value.ToString("yyyy-MM-dd HH:00:00");
            int span = this.dateTimePickerQueryEnd.Value.Hour - this.dateTimePickerQueryStart.Value.Hour;
            
            DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            dtf.ShortDatePattern = "yyyy-MM-dd HH";
            DateTime startTime = Convert.ToDateTime(start, dtf);
            string str = "SELECT "
                        + "DATE_FORMAT(`EndTime`,'%Y-%m-%d %H:00:00') AS `Time`, "
                        + "COUNT(*) AS `num`, "
                        + "SUM(`Result`) AS `ok`, "
                         + "(COUNT(*) - SUM(`Result`)) AS `ng`, "
                         + "(SUM(`Result`) / COUNT(*)) AS `okPercent`"
                        + "FROM `afmdb`.`workpiece` "
                        + "WHERE "
                        + "`EndTime` BETWEEN '" + start + "' AND '" + end + "' "
                        + "AND `StartTime` IS NOT NULL "
                        + "GROUP BY `Time`";
           
            string tableName = "workpiece";
            MySQLDBHelp help = new MySQLDBHelp();
            DataTable dt = help.GetTable(str, tableName);
            if (dt==null)
            {
                return;
            }
            //List 0 OK  1 ng
            Dictionary<DateTime, List<int>> res = new Dictionary<DateTime, List<int>>();         
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<int> data = new List<int>();
                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd HH";
                DateTime time = Convert.ToDateTime(dt.Rows[i][0].ToString(), dtFormat);
                data.Add(int.Parse(dt.Rows[i][2].ToString()));
                data.Add(int.Parse(dt.Rows[i][3].ToString()));                               
                res.Add(time, data);               
            }
            for (int i = 0; i <=span; i++)
            {                
                if (!res.ContainsKey(startTime.AddHours(i)))
                {
                    List<int> data = new List<int>();
                    data.Add(0);
                    data.Add(0);
                    res.Add(startTime.AddHours(i), data);
                }
            }         
            this.yiedAndCapacityChart1.UpdateChartData(res);

        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            DbService.Instance.Fire(this.writeToDB);
        }
        private void writeToDB()
        {
            int result = 0;
            afmdbEntities ctx = new afmdbEntities();
            workpiece workpiece = new workpiece();
            workpiece.Name = "work";
            workpiece.RunMode = 1;
            workpiece.LotNum = "2019-09-07";
            workpiece.Operator = "xiao";
            workpiece.Barcode = "2019-09-07 16:55";
            workpiece.StartTime = this.dateTimePickerInputStart.Value;
            workpiece.EndTime = this.dateTimePickerInputEnd.Value;
            if (!int.TryParse(this.txtOkNg.Text, out result))
            {
                result = 0;
            }
            workpiece.Result = result;
            workpiece.Mark1X = 21;
            workpiece.Mark1Y = 32;
            workpiece.Mark2X = 45;
            workpiece.Mark2Y = 65;
            ctx.workpiece.Add(workpiece);
            ctx.SaveChangesAsync();
        }
    }
}
