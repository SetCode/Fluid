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
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using static Anda.Fluid.Domain.FluProgram.Grammar.StepAndRepeatCmdLine;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditPatternArrayMetro : MetroSetUserControl, IMsgSender, ICanSelectButton
    {
        private Pattern pattern;
        private PointD origin;
        private StepAndRepeatCmdLine stepAndRepeatCmdLine;
        private StepAndRepeatCmdLine stepAndRepeatCmdLineBackUp;
        private bool isCreating;
        private List<string> patternNameList = new List<string>();
        private List<PatternItem> patternItems = new List<PatternItem>();

        private bool loadFirst = false;
        private int selectedIndex = 0;

        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditPatternArrayMetro()
        {
            InitializeComponent();
        }

        public EditPatternArrayMetro(Pattern pattern) : this(pattern, null)
        {
        }

        public EditPatternArrayMetro(Pattern pattern, StepAndRepeatCmdLine stepAndRepeatCmdLine)
        {
            InitializeComponent();
            this.loadFirst = true;
            //this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            isCreating = stepAndRepeatCmdLine == null;
            this.lstDoPatterns.SelectionMode = SelectionMode.MultiExtended;
            if (isCreating)
            {
                this.stepAndRepeatCmdLine = new StepAndRepeatCmdLine();
            }
            else
            {
                this.stepAndRepeatCmdLine = stepAndRepeatCmdLine;
            }
            // patter list
            foreach (Pattern item in FluidProgram.Current.Patterns)
            {
                if (item.HasPassBlocks)
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
            if (isCreating)
            {
                // 新建时，默认选中第一条
                if (listBoxPatterns.Items.Count > 0)
                {
                    listBoxPatterns.SelectedIndex = 0;
                }
            }
            // 选中当前patter name
            else
            {
                int index = -1;
                for (int i = 0; i < patternNameList.Count; i++)
                {
                    if (patternNameList[i] == this.stepAndRepeatCmdLine.PatternName)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > -1)
                {
                    listBoxPatterns.SelectedIndex = index;
                }
            }
            tbHNums.Text = this.stepAndRepeatCmdLine.HorizontalNums.ToString();
            tbVNums.Text = this.stepAndRepeatCmdLine.VerticalNums.ToString();
            //系统坐标->机械坐标
            PointD p0 = this.pattern.MachineRel(this.stepAndRepeatCmdLine.Origin);
            PointD pH = this.pattern.MachineRel(this.stepAndRepeatCmdLine.HorizontalEnd);
            PointD pV = this.pattern.MachineRel(this.stepAndRepeatCmdLine.VerticalEnd);
            tbOriginX.Text = p0.X.ToString("0.000");
            tbOriginY.Text = p0.Y.ToString("0.000");
            tbHEndX.Text = pH.X.ToString("0.000");
            tbHEndY.Text = pH.Y.ToString("0.000");
            tbVEndX.Text = pV.X.ToString("0.000");
            tbVEndY.Text = pV.Y.ToString("0.000");

            this.cbxMode.SelectedIndexChanged += cbxMode_SelectedIndexChanged;
            this.cbxMode.Items.Add(RepeatMode.X向蛇行);
            this.cbxMode.Items.Add(RepeatMode.X向之行);
            this.cbxMode.Items.Add(RepeatMode.Y向蛇行);
            this.cbxMode.Items.Add(RepeatMode.Y向之行);
            this.cbxMode.SelectedIndex = (int)this.stepAndRepeatCmdLine.Mode;

            if (!this.isCreating)
            {
                for (int i = 0; i < this.stepAndRepeatCmdLine.DoCmdLineList.Count; i++)
                {
                    patternItems[i].Enabled = stepAndRepeatCmdLine.DoCmdLineList[i].Enabled;
                }
            }
            this.lstDoPatterns.MouseDown += LstDoPatterns_MouseDown;
            if (this.stepAndRepeatCmdLine != null)
            {
                this.stepAndRepeatCmdLineBackUp = (StepAndRepeatCmdLine)this.stepAndRepeatCmdLine.Clone();
            }
        }

        private void LstDoPatterns_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.lstDoPatterns.Items.Count <= 0)
            {
                return;
            }

            if (this.lstDoPatterns.SelectedIndices.Count <= 0)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                this.cmsValveType.Show(PointToScreen(new Point(this.lstDoPatterns.Location.X + e.Location.X,
                    this.lstDoPatterns.Location.Y + e.Location.Y)));
            }
        }

        private void tbHNums_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbVNums_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbOriginX_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbOriginY_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbHEndX_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbHEndY_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbVEndX_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void tbVEndY_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
            showCklistBox();
        }

        private void calculate()
        {

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

            patternItems.Clear();
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
                        patternItems.Add(pI);
                    }
                }
                else
                {
                    for (int j = 0; j < total; j++)
                    {
                        PointD p = new PointD(pO.X + j * hxgap + j * vxgap, pO.Y + j * hygap + j * vygap);
                        PatternItem pI = new PatternItem(j, 0, p, ValveType.Valve1, true);
                        patternItems.Add(pI);
                    }
                }
            }
            else
            {
                switch (stepAndRepeatCmdLine.Mode)
                {
                    case RepeatMode.X向蛇行:
                        for (int i = 0; i < tbVNums.Value; i++)
                        {
                            if (i % 2 == 0)
                            {
                                for (int j = 0; j < tbHNums.Value; j++)
                                {
                                    PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                    PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                                    patternItems.Add(pI);
                                }
                            }
                            else
                            {
                                for (int j = tbHNums.Value - 1; j >= 0; j--)
                                {
                                    PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                    PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                                    patternItems.Add(pI);
                                }
                            }
                        }
                        break;
                    case RepeatMode.X向之行:
                        for (int i = 0; i < tbVNums.Value; i++)
                        {
                            for (int j = 0; j < tbHNums.Value; j++)
                            {
                                PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                                patternItems.Add(pI);
                            }
                        }
                        break;
                    case RepeatMode.Y向蛇行:
                        for (int j = 0; j < tbHNums.Value; j++)
                        {
                            if (j % 2 == 0)
                            {
                                for (int i = 0; i < tbVNums.Value; i++)
                                {
                                    PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                    PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                                    patternItems.Add(pI);
                                }
                            }
                            else
                            {
                                for (int i = tbVNums.Value - 1; i >= 0; i--)
                                {
                                    PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                    PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                                    patternItems.Add(pI);
                                }
                            }
                        }
                        break;
                    case RepeatMode.Y向之行:
                        for (int j = 0; j < tbHNums.Value; j++)
                        {
                            for (int i = 0; i < tbVNums.Value; i++)
                            {
                                PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                PatternItem pI = new PatternItem(i, j, p, ValveType.Valve1, true);
                                patternItems.Add(pI);
                            }
                        }
                        break;
                }
            }
        }
        private void showCklistBox()
        {
            lstDoPatterns.Items.Clear();
            lstDoPatterns.BeginUpdate();
            for (int i = 0; i < this.patternItems.Count; i++)
            {
                this.lstDoPatterns.Items.Add(this.patternItems[i]);
            }
            lstDoPatterns.EndUpdate();
        }

        private void btnTeachOrigin_Click(object sender, System.EventArgs e)
        {
            tbOriginX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbOriginY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnTeachHEnd_Click(object sender, System.EventArgs e)
        {
            tbHEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbHEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnTeachVEnd_Click(object sender, System.EventArgs e)
        {
            tbVEndX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbVEndY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoToOrigin_Click(object sender, System.EventArgs e)
        {
            if (!tbOriginX.IsValid || !tbOriginY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
        }

        private void btnGoToHEnd_Click(object sender, System.EventArgs e)
        {
            if (!tbHEndX.IsValid || !tbHEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbHEndX.Value, origin.Y + tbHEndY.Value);
        }

        private void btnGoToVEnd_Click(object sender, System.EventArgs e)
        {
            if (!tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbVEndX.Value, origin.Y + tbVEndY.Value);
        }


        private void btnGoTo_Click(object sender, System.EventArgs e)
        {
            if (lstDoPatterns.SelectedIndex < 0)
            {
                return;
            }
            PointD p = patternItems[lstDoPatterns.SelectedIndex].Point;
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + p.X, origin.Y + p.Y);
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.selectedIndex == this.patternItems.Count - 1)
            {
                return;
            }
            selectedIndex++;
            this.lstDoPatterns.SelectedIndex = -1;
            this.lstDoPatterns.SelectedIndex = this.selectedIndex;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.selectedIndex == 0)
            {
                return;
            }
            this.selectedIndex--;
            this.lstDoPatterns.SelectedIndex = -1;
            this.lstDoPatterns.SelectedIndex = this.selectedIndex;
        }
        private void lstDoPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstDoPatterns.SelectedIndex < 0)
            {
                return;
            }
            this.selectedIndex = lstDoPatterns.SelectedIndex;
            this.goToPos(this.selectedIndex);
        }
        private void goToPos(int index)
        {
            if (index >= 0 && index <= this.patternItems.Count - 1)
            {
                PointD p = patternItems[index].Point;
                Machine.Instance.Robot.MoveSafeZ();
                Machine.Instance.Robot.ManualMovePosXY((origin.ToSystem() + p).ToMachine());
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (lstDoPatterns.SelectedIndices.Count < 0)
            {
                return;
            }
            var selectedIndexs = lstDoPatterns.SelectedIndices;
            foreach (int index in selectedIndexs)
            {
                patternItems[index].Enabled = !patternItems[index].Enabled;
            }
            this.showCklistBox();
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            if (lstDoPatterns.SelectedIndices.Count < 0)
            {
                return;
            }
            var selectedIndexs = lstDoPatterns.SelectedIndices;
            foreach (int index in selectedIndexs)
            {
                this.patternItems[index].Reverse = !this.patternItems[index].Reverse;
            }
            this.showCklistBox();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (listBoxPatterns.SelectedIndex < 0)
            {
                //MessageBox.Show("No Pattern is selected.");
                MetroSetMessageBox.Show(this, "请选择一个拼版.");
                return;
            }
            if (!tbHNums.IsValid || !tbVNums.IsValid || !tbOriginX.IsValid || !tbOriginY.IsValid
                || !tbHEndX.IsValid || !tbHEndY.IsValid || !tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MetroSetMessageBox.Show(this, "请输入正确的参数.");
                return;
            }
            if (tbVNums.Value < 1)
            {
                //MessageBox.Show("Vertical Nums can not be smaller than 1.");
                MetroSetMessageBox.Show(this, "纵向拼版数不可以小于 1.");
                return;
            }
            if (tbHNums.Value < 1)
            {
                //MessageBox.Show("Horizontal Nums can not be smaller than 1.");
                MetroSetMessageBox.Show(this, "横向拼版个数不可以小于1.");
                return;
            }
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
            stepAndRepeatCmdLine.PatternName = patternNameList[listBoxPatterns.SelectedIndex];
            //机械坐标->系统坐标
            PointD pO = this.pattern.SystemRel(tbOriginX.Value, tbOriginY.Value);
            PointD pH = this.pattern.SystemRel(tbHEndX.Value, tbHEndY.Value);
            PointD pV = this.pattern.SystemRel(tbVEndX.Value, tbVEndY.Value);
            stepAndRepeatCmdLine.Origin.X = pO.X;
            stepAndRepeatCmdLine.Origin.Y = pO.Y;
            stepAndRepeatCmdLine.HorizontalEnd.X = pH.X;
            stepAndRepeatCmdLine.HorizontalEnd.Y = pH.Y;
            stepAndRepeatCmdLine.VerticalEnd.X = pV.X;
            stepAndRepeatCmdLine.VerticalEnd.Y = pV.Y;
            stepAndRepeatCmdLine.HorizontalNums = tbHNums.Value;
            stepAndRepeatCmdLine.VerticalNums = tbVNums.Value;
            stepAndRepeatCmdLine.DoCmdLineList.Clear();
            foreach (PatternItem item in patternItems)
            {
                DoCmdLine doCmdLine = new DoCmdLine(stepAndRepeatCmdLine.PatternName, item.Point.X, item.Point.Y) { Enabled = item.Enabled, Reverse = item.Reverse };
                doCmdLine.Valve = item.Valve;
                stepAndRepeatCmdLine.DoCmdLineList.Add(doCmdLine);
            }
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, stepAndRepeatCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, stepAndRepeatCmdLine);
            }

            CompareObj.CompareField(this.stepAndRepeatCmdLine, this.stepAndRepeatCmdLineBackUp, null, this.GetType().Name, true);
            if (this.stepAndRepeatCmdLineBackUp.DoCmdLineList != null && this.stepAndRepeatCmdLine.DoCmdLineList != null)
            {
                if (this.stepAndRepeatCmdLineBackUp.DoCmdLineList.Count == this.stepAndRepeatCmdLine.DoCmdLineList.Count)
                {
                    for (int i = 0; i < this.stepAndRepeatCmdLine.DoCmdLineList.Count; i++)
                    {
                        string pathRoot = this.GetType().Name + "\\stepAndRepeatCmdLine\\DoCmdLineList";
                        CompareObj.CompareField(this.stepAndRepeatCmdLine.DoCmdLineList[i], this.stepAndRepeatCmdLineBackUp.DoCmdLineList[i], null, pathRoot, true);
                    }

                }
            }

        }

        private void cbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxMode.SelectedIndex < 0)
            {
                return;
            }
            this.stepAndRepeatCmdLine.Mode = (RepeatMode)this.cbxMode.SelectedIndex;
            calculate();
            this.showCklistBox();
        }

        private void EditStepAndRepeatForm3_Load(object sender, EventArgs e)
        {
            if (this.patternItems != null && this.loadFirst)
            {

                if (this.stepAndRepeatCmdLine.HorizontalNums * this.stepAndRepeatCmdLine.VerticalNums == this.lstDoPatterns.Items.Count)
                {
                    this.loadFirst = false;
                    for (int i = 0; i < this.lstDoPatterns.Items.Count; i++)
                    {
                        patternItems[i].Reverse = this.stepAndRepeatCmdLine.DoCmdLineList[i].Reverse;
                        patternItems[i].Enabled = this.stepAndRepeatCmdLine.DoCmdLineList[i].Enabled;
                        patternItems[i].Valve = this.stepAndRepeatCmdLine.DoCmdLineList[i].Valve;
                    }
                    this.showCklistBox();
                }
            }
        }

        private void lstDoPatterns_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush redBrush = Brushes.Red;
            Brush BlackBrush = Brushes.Black;
            ListBox listBox = sender as ListBox;
            if (e.Index > -1)
            {
                e.DrawBackground();
                string s = listBox.Items[e.Index].ToString();
                if (!this.patternItems[e.Index].Enabled)
                {
                    e.Graphics.DrawString(s, e.Font, redBrush, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(s, e.Font, BlackBrush, e.Bounds);
                }
                e.DrawFocusRectangle();
            }
        }

        private void tsiValve1_Click(object sender, EventArgs e)
        {
            if (this.lstDoPatterns.SelectedIndices.Count <= 0)
            {
                return;
            }

            foreach (PatternItem item in this.lstDoPatterns.SelectedItems)
            {
                item.Valve = ValveType.Valve1;
            }
            this.lstDoPatterns.Invalidate();
        }

        private void tsiValve2_Click(object sender, EventArgs e)
        {
            if (this.lstDoPatterns.SelectedIndices.Count <= 0)
            {
                return;
            }

            foreach (PatternItem item in this.lstDoPatterns.SelectedItems)
            {
                item.Valve = ValveType.Valve2;
            }
            this.lstDoPatterns.Invalidate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnTeachOrigin);
            buttons.Add(this.btnTeachHEnd);
            buttons.Add(this.btnTeachVEnd);
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
