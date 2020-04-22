using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main.RTV
{
    public partial class RTVInfoCtl : UserControl, IMsgReceiver
    {
        public RTVInfoCtl()
        {
            InitializeComponent();
        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (msgName.Equals(LngMsg.MSG_Barcode_Info))
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.textBox1.Text = args[0].ToString();
                }));
            }
            else if (msgName.Equals(LngMsg.MSG_WidthAndHeight_Info))
            {
                this.BeginInvoke(new Action(() =>
                {
                    DataGridViewRow drRow = new DataGridViewRow();

                    //添加测宽值
                    DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                    cell0.Value = args[0].ToString();

                    //添加测宽上公差
                    DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                    cell1.Value = args[1].ToString();

                    //添加测宽下公差
                    DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                    cell2.Value = args[2].ToString();

                    //添加测高值
                    DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                    cell3.Value = args[3].ToString();

                    //添加测高上公差
                    DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                    cell4.Value = args[4].ToString();

                    //添加测高下公差
                    DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                    cell5.Value = args[5].ToString();

                    //将所有行信息添加
                    drRow.Cells.Add(cell0);
                    drRow.Cells.Add(cell1);
                    drRow.Cells.Add(cell2);
                    drRow.Cells.Add(cell3);
                    drRow.Cells.Add(cell4);
                    drRow.Cells.Add(cell5);
                    this.dataGridView1.Rows.Add(drRow);
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = this.dataGridView1.Rows.Count - 1;
                }));
                
            }
            else if (msgName.Equals(LngMsg.MSG_Clear_RtvInfo))
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.textBox1.Text = "";
                    this.dataGridView1.Rows.Clear();
                }));               
            }
        }
    }
}
