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
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.App.Common;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditSnakelineMetro : MetroSetUserControl, IMsgSender,ICanSelectButton
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private SnakeLineCmdLine snakeLineCmdLine;
        private SnakeLineCmdLine snakeLineCmdLineBackUp;
        private List<LineCoordinate> lineCoordinateCache = new List<LineCoordinate>();
        private bool permitReacting = true;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditSnakelineMetro()
        {
            InitializeComponent();
        }
        public EditSnakelineMetro(Pattern pattern) : this(pattern, null)
        {
        }

        public EditSnakelineMetro(Pattern pattern, SnakeLineCmdLine snakeLineCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (snakeLineCmdLine == null)
            {
                isCreating = true;
                this.snakeLineCmdLine = new SnakeLineCmdLine();
            }
            else
            {
                isCreating = false;
                this.snakeLineCmdLine = snakeLineCmdLine;
                lineCoordinateCache.AddRange(this.snakeLineCmdLine.LineCoordinateList);
            }
            permitReacting = false;
            tbLineNumbers.Text = this.snakeLineCmdLine.LineNumbers.ToString();
            //系统坐标->机械坐标
            PointD p1 = this.pattern.MachineRel(this.snakeLineCmdLine.Point1);
            PointD p2 = this.pattern.MachineRel(this.snakeLineCmdLine.Point2);
            PointD p3 = this.pattern.MachineRel(this.snakeLineCmdLine.Point3);
            tbP1X.Text = p1.X.ToString("0.000");
            tbP1Y.Text = p1.Y.ToString("0.000");
            tbP2X.Text = p2.X.ToString("0.000");
            tbP2Y.Text = p2.Y.ToString("0.000");
            tbP3X.Text = p3.X.ToString("0.000");
            tbP3Y.Text = p3.Y.ToString("0.000");
            permitReacting = true;
            this.LoadLines2Box();
            cbWeightControl.Checked = this.snakeLineCmdLine.IsWeightControl;
            this.chkContinuous.Checked = this.snakeLineCmdLine.IsContinuous;
            if (this.snakeLineCmdLine != null)
            {
                this.snakeLineCmdLineBackUp = (SnakeLineCmdLine)this.snakeLineCmdLine.Clone();
            }
        }

        private void btnEditLineParams_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void btnPoint1Select_Click(object sender, EventArgs e)
        {
            tbP1X.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbP1Y.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnPoint1GoTo_Click(object sender, EventArgs e)
        {
            if (!tbP1X.IsValid || !tbP1Y.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbP1X.Value, origin.Y + tbP1Y.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbP1X.Value, origin.Y + tbP1Y.Value);
        }

        private void btnPoint2Select_Click(object sender, EventArgs e)
        {
            tbP2X.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbP2Y.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnPoint2GoTo_Click(object sender, EventArgs e)
        {
            if (!tbP2X.IsValid || !tbP2Y.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbP2X.Value, origin.Y + tbP2Y.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbP2X.Value, origin.Y + tbP2Y.Value);
        }

        private void btnPoint3Select_Click(object sender, EventArgs e)
        {
            tbP3X.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbP3Y.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnPoint3GoTo_Click(object sender, EventArgs e)
        {
            if (!tbP3X.IsValid || !tbP3Y.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbP3X.Value, origin.Y + tbP3Y.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbP3X.Value, origin.Y + tbP3Y.Value);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            lineCoordinateCache.Clear();
            if (!tbLineNumbers.IsValid
                || !tbP1X.IsValid || !tbP1Y.IsValid
                || !tbP2X.IsValid || !tbP2Y.IsValid
                || !tbP3X.IsValid || !tbP3Y.IsValid
                || tbLineNumbers.Value < 2
                || (tbP1X.Value == tbP2X.Value && tbP1Y.Value == tbP2Y.Value)
                || (tbP1X.Value == tbP3X.Value && tbP1Y.Value == tbP3Y.Value)
                || (tbP2X.Value == tbP3X.Value && tbP2Y.Value == tbP3Y.Value))
            {
                return;
            }
            int num = tbLineNumbers.Value;
            //机械坐标->系统坐标
            PointD p1 = pattern.SystemRel(tbP1X.Value, tbP1Y.Value);
            PointD p2 = pattern.SystemRel(tbP2X.Value, tbP2Y.Value);
            PointD p3 = pattern.SystemRel(tbP3X.Value, tbP3Y.Value);
            if (MathUtils.IsInOneLine(p1, p2, p3))
            {
                //MessageBox.Show("The three points cannot be in one line.");
                MessageBox.Show("三个点不可以在同一条轨迹线上.");
                return;
            }
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            PointD start = null, end = null;
            double gap = 0;
            if (dx == 0)
            {
                gap = Math.Abs(p3.X - p1.X) / (num - 1);
                gap = p3.X > p1.X ? gap : -gap;
                for (int i = 0; i < num; i++)
                {
                    if (i % 2 == 0)
                    {
                        start = new PointD(p1.X + gap * i, p1.Y);
                        end = new PointD(p1.X + gap * i, p2.Y);
                    }
                    else
                    {
                        start = new PointD(p1.X + gap * i, p2.Y);
                        end = new PointD(p1.X + gap * i, p1.Y);
                    }
                    lineCoordinateCache.Add(new LineCoordinate(start, end));
                }
            }
            else if (dy == 0)
            {
                gap = Math.Abs(p1.Y - p3.Y) / (num - 1);
                gap = p3.Y > p1.Y ? gap : -gap;
                for (int i = 0; i < num; i++)
                {
                    if (i % 2 == 0)
                    {
                        start = new PointD(p1.X, p1.Y + gap * i);
                        end = new PointD(p2.X, p1.Y + gap * i);
                    }
                    else
                    {
                        start = new PointD(p2.X, p1.Y + gap * i);
                        end = new PointD(p1.X, p1.Y + gap * i);
                    }
                    lineCoordinateCache.Add(new LineCoordinate(start, end));
                }
            }
            else
            {
                double k = (p1.Y - p2.Y) / (p1.X - p2.X);
                // 求p3 到直线(p1, p2)的垂足
                PointD vfoot = MathUtils.GetVerticalFoot(p3, p1, k);
                lineCoordinateCache.Add(new LineCoordinate(new PointD(p1), new PointD(p2)));
                // p3到直线(p1, p2)的垂线与当前直线的交点
                PointD p = new PointD();
                for (int i = 1; i < num; i++)
                {
                    p.X = vfoot.X + (p3.X - vfoot.X) * i / (num - 1);
                    p.Y = vfoot.Y + (p3.Y - vfoot.Y) * i / (num - 1);
                    if (i % 2 == 0)
                    {
                        start = MathUtils.GetVerticalFoot(p1, p, k);
                        end = MathUtils.GetVerticalFoot(p2, p, k);
                    }
                    else
                    {
                        start = MathUtils.GetVerticalFoot(p2, p, k);
                        end = MathUtils.GetVerticalFoot(p1, p, k);
                    }
                    lineCoordinateCache.Add(new LineCoordinate(start, end));
                }
            }

            this.ContinuousHandle();
            this.LoadLines2Box();
        }

        private void LoadLines2Box()
        {
            listBoxPoints.Items.Clear();
            foreach (LineCoordinate coordinate in lineCoordinateCache)
            {
                listBoxPoints.Items.Add(string.Format("{0}:{1},{2}mg,{3}", listBoxPoints.Items.Count, coordinate.LineStyle, coordinate.Weight, coordinate));
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbLineNumbers.IsValid
                || !tbP1X.IsValid || !tbP1Y.IsValid
                || !tbP2X.IsValid || !tbP2Y.IsValid
                || !tbP3X.IsValid || !tbP3Y.IsValid
                || tbLineNumbers.Value < 2
                || (tbP1X.Value == tbP2X.Value && tbP1Y.Value == tbP2Y.Value)
                || (tbP1X.Value == tbP3X.Value && tbP1Y.Value == tbP3Y.Value)
                || (tbP2X.Value == tbP3X.Value && tbP2Y.Value == tbP3Y.Value))
            {
                //MessageBox.Show("please input valid values.");
                MetroSetMessageBox.Show(this, "请输入正确的点1、点2、点3的值.");
                return;
            }
            if (lineCoordinateCache.Count <= 0)
            {
                //MessageBox.Show("Line points is empty.");
                MetroSetMessageBox.Show(this, "线轨迹上点个数不可以小于0.");
                return;
            }
            //机械坐标->系统坐标
            PointD p1 = pattern.SystemRel(tbP1X.Value, tbP1Y.Value);
            PointD p2 = pattern.SystemRel(tbP2X.Value, tbP2Y.Value);
            PointD p3 = pattern.SystemRel(tbP3X.Value, tbP3Y.Value);
            if (MathUtils.IsInOneLine(p1, p2, p3))
            {
                //MessageBox.Show("The three points cannot be in one line.");
                MessageBox.Show("三点不可以在同一条线轨迹上.");
                return;
            }
            snakeLineCmdLine.LineNumbers = tbLineNumbers.Value;
            snakeLineCmdLine.Point1.X = p1.X;
            snakeLineCmdLine.Point1.Y = p1.Y;
            snakeLineCmdLine.Point2.X = p2.X;
            snakeLineCmdLine.Point2.Y = p2.Y;
            snakeLineCmdLine.Point3.X = p3.X;
            snakeLineCmdLine.Point3.Y = p3.Y;
            snakeLineCmdLine.LineCoordinateList.Clear();
            snakeLineCmdLine.LineCoordinateList.AddRange(lineCoordinateCache);
            snakeLineCmdLine.IsWeightControl = cbWeightControl.Checked;
            snakeLineCmdLine.IsContinuous = this.chkContinuous.Checked;

            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, snakeLineCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, snakeLineCmdLine);
            }
            if (!this.isCreating)
            {
                if (this.snakeLineCmdLine != null && this.snakeLineCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.snakeLineCmdLine, this.snakeLineCmdLineBackUp, null, this.GetType().Name, true);
                    for (int i = 0; i < this.snakeLineCmdLine.LineCoordinateList.Count; i++)
                    {
                        string pathRoot = this.GetType().Name + "\\snakeLineCmdLine\\LineCoordinateList";
                        CompareObj.CompareField(this.snakeLineCmdLine.LineCoordinateList[i], this.snakeLineCmdLineBackUp.LineCoordinateList[i], null, pathRoot, true);
                    }
                }

            }
        }

        private void cbWeightControl_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEditWt_Click(object sender, EventArgs e)
        {
            if (new EditLineWeightForm(this.snakeLineCmdLine, this.lineCoordinateCache).ShowDialog() == DialogResult.OK)
            {
                this.LoadLines2Box();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }

        private void chkContinuous_CheckedChanged(object sender, EventArgs e)
        {
            this.ContinuousHandle();

            this.LoadLines2Box();
        }

        /// <summary>
        /// 连续以及非连续的处理
        /// </summary>
        private void ContinuousHandle()
        {
            //线数量小于3时，不做反应
            if (this.tbLineNumbers.Value < 3)
            {
                return;
            }
            //如果选为连续并且线数量等于设定数量时，需要增加连续过渡线
            if (this.chkContinuous.Checked && this.lineCoordinateCache.Count == this.tbLineNumbers.Value)
            {
                for (int i = 0; i < 2 * this.tbLineNumbers.Value - 1; i++)
                {
                    //过度线应该插入在奇数段
                    if (i % 2 == 1)
                    {
                        PointD start = this.lineCoordinateCache[i - 1].End;
                        PointD end = this.lineCoordinateCache[i].Start;
                        this.lineCoordinateCache.Insert(i, new LineCoordinate(start, end));
                    }

                }
            }
            //如果选为不连续并且线数量大于设定数量时，需要删除连续过渡线
            else if (!this.chkContinuous.Checked && this.lineCoordinateCache.Count > this.tbLineNumbers.Value)
            {
                for (int i = 1; i < this.tbLineNumbers.Value; i++)
                {
                    //删除过渡线
                    this.lineCoordinateCache.RemoveAt(i);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnPoint1Select);
            buttons.Add(this.btnPoint2Select);
            buttons.Add(this.btnPoint3Select);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }
    }
}
