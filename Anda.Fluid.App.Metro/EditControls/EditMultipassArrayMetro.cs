using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditMultipassArrayMetro : MetroSetUserControl, IMsgSender, ICanSelectButton
    {
        private PointD origin;
        private Pattern pattern;
        private List<string> patternNameList = new List<string>();
        private List<PatternItem> patternOrigins = new List<PatternItem>();

        private int selectedIndex = 0;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditMultipassArrayMetro()
        {
            InitializeComponent();
            //this.ReadLanguageResources();
        }
        public EditMultipassArrayMetro(Pattern pattern)
        {
            InitializeComponent();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            // patter list
            foreach (Pattern item in FluidProgram.Current.Patterns)
            {
                if (!item.HasPassBlocks)
                {
                    continue;
                }
                if (item.Name == this.pattern.Name) // 阵列指令所在拼版不显示，自循环引用防呆
                {
                    continue;
                }
                patternNameList.Add(item.Name);
            }
            foreach (string patternName in patternNameList)
            {
                listBoxPatterns.Items.Add(patternName);
            }
            // 默认选中第一条
            if (listBoxPatterns.Items.Count > 0)
            {
                listBoxPatterns.SelectedIndex = 0;
            }
            this.listBoxOrigins.SelectionMode = SelectionMode.MultiExtended;
            this.listBoxOrigins.MouseDown += ListBoxOrigins_MouseDown;
        }

        private void ListBoxOrigins_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listBoxOrigins.Items.Count <= 0)
            {
                return;
            }

            if (this.listBoxOrigins.SelectedIndices.Count <= 0)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                this.cmsValveType.Show(new Point(this.Location.X + this.listBoxOrigins.Location.X + e.Location.X,
                    this.Location.Y + this.listBoxOrigins.Location.Y + e.Location.Y + this.cmsValveType.Size.Height / 2));
            }
        }

        private void tbHNums_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbVNums_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbOriginX_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbOriginY_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbHEndX_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbHEndY_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbVEndX_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbVEndY_TextChanged(object sender, EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void calculate()
        {
            listBoxOrigins.Items.Clear();
            if (!tbHNums.IsValid || !tbVNums.IsValid || !tbOriginX.IsValid || !tbOriginY.IsValid
                || !tbHEndX.IsValid || !tbHEndY.IsValid || !tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                return;
            }
            if (tbHNums.Value < 1 || tbVNums.Value < 1)
            {
                return;
            }
            int total = tbHNums.Value * tbVNums.Value;
            if (total == 1)
            {
                return;
            }
            if (tbHEndX.Value == tbVEndX.Value && tbHEndY.Value == tbVEndY.Value)
            {
                return;
            }
            if (tbOriginX.Value == tbHEndX.Value && tbOriginY.Value == tbHEndY.Value)
            {
                if (tbVNums.Value == 1 || tbHNums.Value != 1)
                {
                    return;
                }
            }
            if (tbOriginX.Value == tbVEndX.Value && tbOriginY.Value == tbVEndY.Value)
            {
                if (tbHNums.Value == 1 || tbVNums.Value != 1)
                {
                    return;
                }
            }
            patternOrigins.Clear();
            //机械坐标->系统坐标
            PointD pO = this.pattern.SystemRel(tbOriginX.Value, tbOriginY.Value);
            PointD pH = this.pattern.SystemRel(tbHEndX.Value, tbHEndY.Value);
            PointD pV = this.pattern.SystemRel(tbVEndX.Value, tbVEndY.Value);
            double hxgap = tbHNums.Value == 1 ? 0 : (pH.X - pO.X) / (tbHNums.Value - 1);
            double hygap = tbHNums.Value == 1 ? 0 : (pH.Y - pO.Y) / (tbHNums.Value - 1);
            double vxgap = tbVNums.Value == 1 ? 0 : (pV.X - pO.X) / (tbVNums.Value - 1);
            double vygap = tbVNums.Value == 1 ? 0 : (pV.Y - pO.Y) / (tbVNums.Value - 1);

            if (tbHNums.Value == 1 || tbVNums.Value == 1)
            {
                if (tbHNums.Value == 1)
                {
                    for (int j = 0; j < total; j++)
                    {
                        PointD p = new PointD(pO.X + j * hxgap + j * vxgap, pO.Y + j * hygap + j * vygap);
                        PatternItem pI = new PatternItem(0, j, p, ValveType.Valve1, true);
                        patternOrigins.Add(pI);
                    }
                }
                else
                {
                    for (int j = 0; j < total; j++)
                    {
                        PointD p = new PointD(pO.X + j * hxgap + j * vxgap, pO.Y + j * hygap + j * vygap);
                        PatternItem pI = new PatternItem(j, 0, p, ValveType.Valve1, true);
                        patternOrigins.Add(pI);
                    }
                }
            }
            else
            {
                for (int i = 0; i < tbVNums.Value; i++)
                {
                    if (i % 2 == 0)
                    {
                        for (int j = 0; j < tbHNums.Value; j++)
                        {
                            PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                            PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                            patternOrigins.Add(pI);
                        }
                    }
                    else
                    {
                        for (int j = tbHNums.Value - 1; j >= 0; j--)
                        {
                            PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                            PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                            patternOrigins.Add(pI);
                        }
                    }
                }
            }
        }

        private void btnSelectOrigin_Click(object sender, EventArgs e)
        {
            tbOriginX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbOriginY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnSelectHEnd_Click(object sender, EventArgs e)
        {
            tbHEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbHEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnSelectVEnd_Click(object sender, EventArgs e)
        {
            tbVEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbVEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoToOrigin_Click(object sender, EventArgs e)
        {
            if (!tbOriginX.IsValid || !tbOriginY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
        }

        private void btnGoToHEnd_Click(object sender, EventArgs e)
        {
            if (!tbHEndX.IsValid || !tbHEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbHEndX.Value, origin.Y + tbHEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbHEndX.Value, origin.Y + tbHEndY.Value);
        }

        private void btnGoToVEnd_Click(object sender, EventArgs e)
        {
            if (!tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbVEndX.Value, origin.Y + tbVEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbVEndX.Value, origin.Y + tbVEndY.Value);
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (listBoxOrigins.SelectedIndex < 0)
            {
                return;
            }
            //系统坐标->机械坐标
            PointD p = this.pattern.MachineRel(patternOrigins[listBoxOrigins.SelectedIndex].Point);
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + p.X, origin.Y + p.Y);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + p.X, origin.Y + p.Y);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.selectedIndex == 0)
            {
                return;
            }
            this.selectedIndex--;
            this.listBoxOrigins.SelectedIndex = -1;
            this.listBoxOrigins.SelectedIndex = this.selectedIndex;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.selectedIndex == this.patternOrigins.Count - 1)
            {
                return;
            }
            selectedIndex++;
            this.listBoxOrigins.SelectedIndex = -1;
            this.listBoxOrigins.SelectedIndex = this.selectedIndex;
        }

        private void listBoxOrigins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxOrigins.SelectedIndex < 0)
            {
                return;
            }
            this.selectedIndex = listBoxOrigins.SelectedIndex;
            this.goToPos(this.selectedIndex);
        }

        private void goToPos(int index)
        {
            if (index >= 0 && index <= this.patternOrigins.Count - 1)
            {
                PointD p = this.patternOrigins[index].Point;
                Machine.Instance.Robot.MoveSafeZ();
                Machine.Instance.Robot.ManualMovePosXY((origin.ToSystem() + p).ToMachine());
            }
        }

        private void tsiValve1_Click(object sender, EventArgs e)
        {
            if (this.listBoxOrigins.SelectedIndices.Count <= 0)
            {
                return;
            }

            foreach (PatternItem item in this.listBoxOrigins.SelectedItems)
            {
                item.Valve = ValveType.Valve1;
            }
            showCklistBox();
        }

        private void tsiValve2_Click(object sender, EventArgs e)
        {
            if (this.listBoxOrigins.SelectedIndices.Count <= 0)
            {
                return;
            }

            foreach (PatternItem item in this.listBoxOrigins.SelectedItems)
            {
                item.Valve = ValveType.Valve2;
            }
            showCklistBox();
        }

        private void showCklistBox()
        {
            listBoxOrigins.Items.Clear();
            listBoxOrigins.BeginUpdate();
            for (int i = 0; i < this.patternOrigins.Count; i++)
            {
                this.listBoxOrigins.Items.Add(this.patternOrigins[i]);
            }
            listBoxOrigins.EndUpdate();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedIndex < 0)
            {
                //MessageBox.Show("No Pattern is selected.");
                MetroSetMessageBox.Show(this, "请选择一个拼版");
                return;
            }
            if (!tbHNums.IsValid || !tbVNums.IsValid || !tbOriginX.IsValid || !tbOriginY.IsValid
                || !tbHEndX.IsValid || !tbHEndY.IsValid || !tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MetroSetMessageBox.Show(this, "请输入合理的值");
                return;
            }
            if (tbVNums.Value < 1)
            {
                //MessageBox.Show("Vertical Nums can not be smaller than 1.");
                MetroSetMessageBox.Show(this, "横向的值不可以小于1");
                return;
            }
            if (tbHNums.Value < 1)
            {
                //MessageBox.Show("Horizontal Nums can not be smaller than 1.");
                MetroSetMessageBox.Show(this, "纵向的值不可以小于1");
                return;
            }
            int total = tbVNums.Value * tbHNums.Value;
            if (tbHNums.Value * tbVNums.Value == 1)
            {
                MetroSetMessageBox.Show(this, "拼版数量不可以等于1.");
                return;
            }
            if (tbOriginX.Value == tbHEndX.Value && tbOriginY.Value == tbHEndY.Value)
            {
                //MessageBox.Show("Origin can not be same with horizontal end.");
                if (tbVNums.Value == 1 || tbHNums.Value != 1)
                {
                    MetroSetMessageBox.Show(this, "原点坐标和横向终点坐标不可以相同");
                    return;
                }
            }
            if (tbOriginX.Value == tbVEndX.Value && tbOriginY.Value == tbVEndY.Value)
            {
                //MessageBox.Show("Origin can not be same with vertical end.");
                if (tbHNums.Value == 1 || tbVNums.Value != 1)
                {
                    MetroSetMessageBox.Show(this, "原点坐标和纵向终点坐标不可以相同");
                    return;
                }
            }
            if (tbHEndX.Value == tbVEndX.Value && tbHEndY.Value == tbVEndY.Value)
            {
                //MessageBox.Show("Horizontal end can not be same with vertical end.");
                if (tbHNums.Value != 1 && tbVNums.Value != 1)
                {
                    MetroSetMessageBox.Show(this, "横向终点和纵向终点不能相同");
                    return;
                }
            }
            List<CmdLine> cmdLineList = new List<CmdLine>();
            foreach (PatternItem pI in patternOrigins)
            {
                DoMultiPassCmdLine doMultiPassCmdLine = new DoMultiPassCmdLine(patternNameList[listBoxPatterns.SelectedIndex], pI.Point.X, pI.Point.Y);
                doMultiPassCmdLine.Valve = pI.Valve;
                cmdLineList.Add(doMultiPassCmdLine);
            }
            MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, cmdLineList.ToArray());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnSelectOrigin);
            buttons.Add(this.btnSelectHEnd);
            buttons.Add(this.btnSelectVEnd);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }

        class PatternItem
        {
            public PatternItem(int i, int j, PointD point, ValveType valve, bool enabled)
            {
                this.I = i;
                this.J = j;
                this.Point = point;
                this.Valve = valve;
                this.Enabled = enabled;
            }
            public int I { get; set; }
            public int J { get; set; }
            public PointD Point { get; set; }
            public bool Enabled { get; set; }

            public ValveType Valve { get; set; }

            public bool Reverse { get; set; } = false;
            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                if (!Enabled)
                {
                    builder.Append("Disable ");
                }
                string valveDisplay = "";
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    valveDisplay = this.Valve == ValveType.Valve1 ? "valve1" : "valve2";
                }
                builder.Append(string.Format("[{0},{1}] {2} {3} {4}", I, J, Point, valveDisplay, Reverse == true ? "Rev" : ""));
                return builder.ToString();
            }
        }
    }
}
