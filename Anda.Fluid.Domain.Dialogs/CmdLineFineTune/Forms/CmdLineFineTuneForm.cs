using Anda.Fluid.Domain.Dialogs.CmdLineFineTune.MVC;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.Forms
{
    public partial class CmdLineFineTuneForm : Form,IFineTuneViewable
    {
        private Pattern pattern;
        private IFineTuneModelable model;
        private CmdLineFineTuneForm()
        {
            InitializeComponent();
        }
        public CmdLineFineTuneForm(Pattern pattern):this()
        {
            this.pattern = pattern;
            //初始化控制器
            this.Controller = new FineTuneController();
            //初始化模型
            this.model = new FineTuneModel(pattern);
            //为控制器设置要控制的模型
            this.Controller.SetModel(this.model);
            //将此窗口作为观察者添加到模型中
            this.model.AddObserver(this);

            //listView1.Columns.Add("command", this.listView1.Width, HorizontalAlignment.Left);
        }

        public IFineTuneControlable Controller { get; }

        private void CmdLineFineTuneForm_Load(object sender, EventArgs e)
        {
            //所有的轨迹类型都被勾选
            foreach (var item in this.panel1.Controls)
            {
                CheckBox chk = item as CheckBox;
                chk.Checked = true;
            }

            this.InitDisItems();
        }

        private void btnSelectedAll_Click(object sender, EventArgs e)
        {
            if (this.btnSelectedAll.Text.Equals("全不选"))
            {
                //所有的轨迹类型都不勾选
                foreach (var item in this.panel1.Controls)
                {
                    CheckBox chk = item as CheckBox;
                    chk.Checked = false;
                }
                this.btnSelectedAll.Text = "全选";
            }
            else
            {
                //所有的轨迹类型都勾选
                foreach (var item in this.panel1.Controls)
                {
                    CheckBox chk = item as CheckBox;
                    chk.Checked = true;
                }
                this.btnSelectedAll.Text = "全不选";
            }
        }

        private void Chk_ValueChanged(object sender, EventArgs e)
        {
            List<Tuple<CmdLineType, bool>> list = new List<Tuple<CmdLineType, bool>>();
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.点, this.chkDot.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.直线, this.chkSingleLine.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.多段线, this.chkPolyLine.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.多线段, this.chkMultiLines.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.蛇形线, this.chkSnakeLines.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.圆弧或圆环, this.chkArcEnable.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.复合线, this.chkSymbolLines.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.执行拼板, this.chkDoPattern.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.拼板阵列, this.chkStepAndRepeat.Checked));
            list.Add(new Tuple<CmdLineType, bool>(CmdLineType.执行分组拼板, this.chkDoMultiPassPattern.Checked));

            this.Controller.SetEnable(list);
        }

        public void UpdateByModelChange(IFineTuneModelable model)
        {
            this.BeginInvoke(new Action(() =>
            {
                //将所有的轨迹命令显示在列表中
                this.listView1.Clear();
                for (int i = 0; i < model.CmdLineList.Count; i++)
                {
                    this.listView1.Items.Add(string.Format("{0}:{1}", i + 1, model.CmdLineList[i].ToString()));
                }

                this.dataGridView1.Rows.Clear();
                for (int i = 0; i < model.CurrCmdPointsList.Count; i++)
                {
                    //只有勾选的轨迹类型才会添加
                    if (model.CurrCmdPointsList[i].CmdLineEnable)
                    {
                        DataGridViewRow drRow = new DataGridViewRow();

                        //添加编号
                        DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                        string s = string.Format("{0}-{1}", model.CurrCmdPointsList[i].CmdLineNo + 1, model.CurrCmdPointsList[i].PointNo + 1);
                        cell0.Value = s;

                        //添加轨迹名称
                        DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                        cell1.Value = model.CurrCmdPointsList[i].CmdLineType;

                        //添加点名称
                        DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                        cell2.Value = model.CurrCmdPointsList[i].PointDescribe;

                        //添加是否跳过该点
                        DataGridViewCheckBoxCell cell3 = new DataGridViewCheckBoxCell();
                        cell3.Value = model.CurrCmdPointsList[i].Skip;

                        //添加点的X坐标和Y坐标
                        DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                        cell4.Value = model.CurrCmdPointsList[i].Point.X.ToString();
                        DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                        cell5.Value = model.CurrCmdPointsList[i].Point.Y.ToString();

                        //将所有行信息添加
                        drRow.Cells.Add(cell0);
                        drRow.Cells.Add(cell1);
                        drRow.Cells.Add(cell2);
                        drRow.Cells.Add(cell3);
                        drRow.Cells.Add(cell4);
                        drRow.Cells.Add(cell5);
                        this.dataGridView1.Rows.Add(drRow);
                    }
                }
            }));
        }

        public void UpdateBySelectedChange(IFineTuneModelable model)
        {
            this.BeginInvoke(new Action(() =>
            {
                //跳转到选中轨迹
                this.listView1.EnsureVisible(model.SelectedCmdLineNo);
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    this.listView1.Items[i].BackColor = Color.White;
                }
                this.listView1.Items[model.SelectedCmdLineNo].BackColor = Color.Red;

                //跳转到选中点
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                int index = model.GetSelectedInCurrList();
                if (index != -1)
                {
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = index;
                    this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                }

                //显示选中的点的坐标和点在列表中的编号
                CmdLinePoint selcetedPoint = model.GetSelectedPoint();
                if (selcetedPoint == null)
                {
                    this.txtPointX.Text = "0";
                    this.txtPointY.Text = "0";
                }
                else
                {
                    this.txtPointX.Text = selcetedPoint.Point.X.ToString();
                    this.txtPointY.Text = selcetedPoint.Point.Y.ToString();
                }
            }));

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //如果是在有跳过勾选框的那一列
            if (this.dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)this.dataGridView1.CurrentCell;

                Tuple<int, int> cmdLineAndPointNo = this.GetCmdLineAndPointNo(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0]);

                //如果被勾选，则取消勾选
                if ((bool)chk.EditedFormattedValue)
                {
                    chk.Value = false;
                    this.Controller.SetSkip(cmdLineAndPointNo.Item1, cmdLineAndPointNo.Item2, false);
                }
                else
                {
                    chk.Value = true;
                    this.Controller.SetSkip(cmdLineAndPointNo.Item1, cmdLineAndPointNo.Item2, true);
                }
            }
        }

        private void btnMoveTo_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
                return;

            //获得选中行的Skip
            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)this.dataGridView1.SelectedRows[0].Cells[3];      
            //如果被勾选，则提示无法定位到跳过点
            if ((bool)chk.EditedFormattedValue)
            {
                MessageBox.Show("无法定位到跳过点");
                return;
            }

            //如果是空行，则提示无法定位到该点
            DataGridViewTextBoxCell txt = (DataGridViewTextBoxCell)this.dataGridView1.SelectedRows[0].Cells[0];
            if (txt.Value == null) 
            {
                MessageBox.Show("无法定位到该点");
                return;
            }

            Tuple<int, int> cmdLineAndPointNo = this.GetCmdLineAndPointNo(this.dataGridView1.SelectedRows[0].Cells[0]);
            this.Controller.MoveToPoint(cmdLineAndPointNo.Item1, cmdLineAndPointNo.Item2);
        }

        /// <summary>
        /// 通过第一个编号网格，得到该点的指令编号和点编号
        /// </summary>
        /// <param name="noCell"></param>
        /// <returns></returns>
        private Tuple<int,int> GetCmdLineAndPointNo(DataGridViewCell noCell)
        {
            string cmdLineAndPointNo = noCell.Value.ToString();
            string[] no = cmdLineAndPointNo.Split('-');
            int cmdLineNo = Convert.ToInt16(no[0]) - 1;
            int pointNo = Convert.ToInt16(no[1]) - 1;
            return new Tuple<int, int>(cmdLineNo, pointNo);
            
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if(!this.Controller.PreTrack())
            {
                MessageBox.Show("无法移动到前一个点");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!this.Controller.NextTrack())
            {
                MessageBox.Show("无法移动到后一个点");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PointD origin = this.pattern.GetOriginPos();
            double x = Machine.Instance.Robot.PosX - origin.X;
            double y = Machine.Instance.Robot.PosY - origin.Y;
            this.txtCurrX.Text = x.ToString("0.000");
            this.txtCurrY.Text = y.ToString("0.000");
        }

        private void btnMoveToPre_Click(object sender, EventArgs e)
        {
            if (!this.Controller.PreTrack())
            {
                MessageBox.Show("无法移动到前一个点");
            }
        }

        private void btnMoveToNext_Click(object sender, EventArgs e)
        {
            if (!this.Controller.NextTrack())
            {
                MessageBox.Show("无法移动到后一个点");
            }
        }

        private void btnAutoMove_Click(object sender, EventArgs e)
        {
            if(this.Controller.AutoTrack)
            {
                this.Controller.AutoTrack = false;
                this.btnAutoMove.Text = "自动跟踪";
                this.btnMoveToPre.Enabled = true;
                this.btnMoveToNext.Enabled = true;
            }
            else
            {
                this.Controller.AutoTrack = true;
                this.btnAutoMove.Text = "结束跟踪";
                this.btnMoveToPre.Enabled = false;
                this.btnMoveToNext.Enabled = false;
            }
        }

        private void btnUpLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.UpLeft, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.UpLeft, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.Up, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.Up, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnUpRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.UpRight, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.UpRight, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                this.Controller.Move(DirectionEnum.Left, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.Left, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.Right, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.Right, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnDownLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.DownLeft, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.DownLeft, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.Down, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.Down, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnDownRigth_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Controller.Move(DirectionEnum.DownRight, double.Parse(this.cmbLeftClickDis.SelectedItem.ToString()));
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Controller.Move(DirectionEnum.DownRight, double.Parse(this.cmbRightClickDis.SelectedItem.ToString()));
            }
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            PointD point = new PointD(double.Parse(this.txtCurrX.Text), double.Parse(this.txtCurrY.Text));
            this.Controller.TeachPoint(point);
        }

        private void InitDisItems()
        {
            this.cmbLeftClickDis.Items.Add(0.001);
            this.cmbLeftClickDis.Items.Add(0.002);
            this.cmbLeftClickDis.Items.Add(0.005);
            this.cmbLeftClickDis.Items.Add(0.01);
            this.cmbLeftClickDis.Items.Add(0.02);
            this.cmbLeftClickDis.Items.Add(0.03);
            this.cmbLeftClickDis.Items.Add(0.04);
            this.cmbLeftClickDis.Items.Add(0.05);
            this.cmbLeftClickDis.Items.Add(0.1);
            this.cmbLeftClickDis.Items.Add(0.2);
            this.cmbLeftClickDis.Items.Add(0.3);
            this.cmbLeftClickDis.Items.Add(0.4);
            this.cmbLeftClickDis.Items.Add(0.5);
            this.cmbLeftClickDis.Items.Add(1);
            this.cmbLeftClickDis.Items.Add(2);
            this.cmbLeftClickDis.SelectedIndex = 0;

            this.cmbRightClickDis.Items.Add(0.001);
            this.cmbRightClickDis.Items.Add(0.002);
            this.cmbRightClickDis.Items.Add(0.005);
            this.cmbRightClickDis.Items.Add(0.01);
            this.cmbRightClickDis.Items.Add(0.02);
            this.cmbRightClickDis.Items.Add(0.03);
            this.cmbRightClickDis.Items.Add(0.04);
            this.cmbRightClickDis.Items.Add(0.05);
            this.cmbRightClickDis.Items.Add(0.1);
            this.cmbRightClickDis.Items.Add(0.2);
            this.cmbRightClickDis.Items.Add(0.3);
            this.cmbRightClickDis.Items.Add(0.4);
            this.cmbRightClickDis.Items.Add(0.5);
            this.cmbRightClickDis.Items.Add(1);
            this.cmbRightClickDis.Items.Add(2);
            this.cmbRightClickDis.SelectedIndex = 0;

            this.cmbKeyDis.Items.Add(0.001);
            this.cmbKeyDis.Items.Add(0.002);
            this.cmbKeyDis.Items.Add(0.005);
            this.cmbKeyDis.Items.Add(0.01);
            this.cmbKeyDis.Items.Add(0.02);
            this.cmbKeyDis.Items.Add(0.03);
            this.cmbKeyDis.Items.Add(0.04);
            this.cmbKeyDis.Items.Add(0.05);
            this.cmbKeyDis.Items.Add(0.1);
            this.cmbKeyDis.Items.Add(0.2);
            this.cmbKeyDis.Items.Add(0.3);
            this.cmbKeyDis.Items.Add(0.4);
            this.cmbKeyDis.Items.Add(0.5);
            this.cmbKeyDis.Items.Add(1);
            this.cmbKeyDis.Items.Add(2);
            this.cmbKeyDis.SelectedIndex = 0;

            if (Machine.Instance.Robot != null)
            {
                Machine.Instance.Robot.ManualIncOrJog = true;
            }

            try
            {
                Machine.Instance.Robot.ManualIncValue = double.Parse(this.cmbKeyDis.Text);
            }
            catch
            {
                Machine.Instance.Robot.ManualIncValue = 0;
                this.cmbKeyDis.Text = "0";
            }
        }

        private void cmbKeyDis_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Machine.Instance.Robot.ManualIncValue = double.Parse(this.cmbKeyDis.Text);
            }
            catch
            {
                Machine.Instance.Robot.ManualIncValue = 0;
                this.cmbKeyDis.Text = "0";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.btnTeach_Click(0, new EventArgs());
            }
            return true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
                return;

            //获得选中行
            DataGridViewTextBoxCell txt = (DataGridViewTextBoxCell)this.dataGridView1.SelectedRows[0].Cells[0];

            if (txt.Value == null)
                return;

            string s = txt.Value.ToString();
            string[] no = s.Split('-');
            int index = int.Parse(no[0]) -1;

            //跳转到选中轨迹
            this.listView1.EnsureVisible(index);
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                this.listView1.Items[i].BackColor = Color.White;
            }
            this.listView1.Items[index].BackColor = Color.Red;
        }
    }
}
