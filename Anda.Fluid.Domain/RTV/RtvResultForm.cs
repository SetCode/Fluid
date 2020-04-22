using Anda.Fluid.Domain.Custom;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.MachineStates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.RTV
{
    public partial class RtvResultForm : Form
    {
        private List<string[]> results;
        private List<int> indexs;
        public RtvResultForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Machine.Instance.FSM.ChangeProductionState(ProductionState.Alarm);
        }

        public RtvResultForm SetUp(List<string[]> resultList, List<int> indexList)
        {
            this.results = resultList;
            this.indexs = indexList;
            this.LoadData();
            return this;
        }

        private void LoadData()
        {
            for (int i = 0; i < this.results.Count; i++)
            {
                DataGridViewRow drRow = new DataGridViewRow();

                //添加编号
                DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                cell0.Value = (i + 1).ToString();

                //添加测宽值
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = this.results[i][0];

                //添加测宽上公差
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = this.results[i][1];

                //添加测宽下公差
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = this.results[i][2];

                //添加测高值
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = this.results[i][3];

                //添加测高上公差
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = this.results[i][4];

                //添加测高下公差
                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = this.results[i][5];

                //将所有行信息添加
                drRow.Cells.Add(cell0);
                drRow.Cells.Add(cell1);
                drRow.Cells.Add(cell2);
                drRow.Cells.Add(cell3);
                drRow.Cells.Add(cell4);
                drRow.Cells.Add(cell5);
                drRow.Cells.Add(cell6);
                this.dataGridView1.Rows.Add(drRow);
            }

            //给测宽失败项添加背景色
            foreach (var item in this.indexs)
            {
                this.dataGridView1.Rows[item -1].DefaultCellStyle.BackColor = Color.Red;
            }
        }

        private void btnStopVoice_Click(object sender, EventArgs e)
        {
            this.StopVoice();
        }

        private void RtvResultForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.StopVoice();
        }

        private void StopVoice()
        {
            if (Machine.Instance.FSM.CurrState is MachineProductionState)
            {
                Machine.Instance.FSM.ChangeProductionState(ProductionState.Normal);
            }
            else if (Machine.Instance.FSM.CurrState is MachineAlarmState)
            {
                Machine.Instance.FSM.ChangeState(MachineIdleState.Instance);
            }
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Retry;
        }

        private void btnNoSave_Click(object sender, EventArgs e)
        {
            if (Executor.Instance.GetCustom() is CustomRTV)
            {
                CustomRTV rtv = (CustomRTV)Executor.Instance.GetCustom();
                rtv.SaveResult(Executor.Instance.Program.RuntimeSettings.CustomParam.RTVParam.DataLocalPathDir);
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSaveInMes_Click(object sender, EventArgs e)
        {
            if (Executor.Instance.GetCustom() is CustomRTV)
            {
                CustomRTV rtv = (CustomRTV)Executor.Instance.GetCustom();
                rtv.SaveResult(Executor.Instance.Program.RuntimeSettings.CustomParam.RTVParam.DataMesPathDir);
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
