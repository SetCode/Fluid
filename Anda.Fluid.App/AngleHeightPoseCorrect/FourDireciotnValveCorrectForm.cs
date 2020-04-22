using Anda.Fluid.App.AngleHeightPoseCorrect.TestType;
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

namespace Anda.Fluid.App.AngleHeightPoseCorrect
{
    public partial class FourDireciotnValveCorrectForm : Form
    {
        public FourDireciotnValveCorrectForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.UpdateDate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new TestTypeCoreectAngleForm().ShowDialog();
            this.UpdateDate();
        }
        private void UpdateDate()
        {
            this.dataGridView1.Rows.Clear();

            for (int i = 0; i < Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Count; i++)
            {
                DataGridViewRow drRow = new DataGridViewRow();

                //添加角度
                DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                cell0.Value = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i].ValveAngle.ToString();

                //添加胶阀-相机偏差
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i].ValveCameraOffset.ToString();

                //添加标准高度
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i].StandardZ.ToString();

                //添加距板高度
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i].Gap.ToString();

                //添加胶点-阀嘴偏差
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i].DispenseOffset.ToString();


                //将所有行信息添加
                drRow.Cells.Add(cell0);
                drRow.Cells.Add(cell1);
                drRow.Cells.Add(cell2);
                drRow.Cells.Add(cell3);
                drRow.Cells.Add(cell4);
                this.dataGridView1.Rows.Add(drRow);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("确定清除当前所有倾斜校正数据?",null,MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Clear();
                this.UpdateDate();
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("确定清除当前所有倾斜校正数据?", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int index = this.dataGridView1.SelectedRows[0].Index;
                index = this.dataGridView1.SelectedCells[0].RowIndex;
                Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.RemoveAt(index);
                this.UpdateDate();
            }
        }
    }
}
