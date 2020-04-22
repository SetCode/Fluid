using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    /// <summary>
    /// 多拼版阵列指令窗口
    /// </summary>
    public partial class EditStepAndRepeatForm1 : EditFormBase, IMsgSender
    {
        enum RepeatMode
        {
            X向蛇行,
            X向之行,
            Y向蛇行,
            Y向之行,
        }

        private Pattern pattern;
        private PointD origin;
        private List<string> patternNameList = new List<string>();
        //需要阵列的原始拼版数组（可以单拼版也可以多拼版）
        private List<PatternItem> patternOriList = new List<PatternItem>();
        //生成的阵列拼版数组
        private List<PatternItem> patternItems = new List<PatternItem>();

        public EditStepAndRepeatForm1(Pattern pattern) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            // patter list
            foreach (Pattern item in FluidProgram.Current.Patterns)
            {
                if (item.HasPassBlocks)
                {
                    continue;
                }
                patternNameList.Add(item.Name);
            }
            foreach (string patternName in patternNameList)
            {
                listBoxPatterns.Items.Add(patternName);
            }
            // 新建时，默认选中第一条
            if (listBoxPatterns.Items.Count > 0)
            {
                listBoxPatterns.SelectedIndex = 0;
            }
            tbHNums.Text = "0";
            tbVNums.Text = "0";
            //系统坐标->机械坐标
            tbOriginX.Text = "0.000";
            tbOriginY.Text = "0.000";
            tbHEndX.Text = "0.000";
            tbHEndY.Text = "0.000";
            tbVEndX.Text = "0.000";
            tbVEndY.Text = "0.000";
            this.listBoxOrigins.SelectedIndexChanged += ListBoxOrigins_SelectedIndexChanged;

            this.cbxMode.SelectedIndexChanged += cbxMode_SelectedIndexChanged;
            this.cbxMode.Items.Add(RepeatMode.X向蛇行);
            this.cbxMode.Items.Add(RepeatMode.X向之行);
            this.cbxMode.Items.Add(RepeatMode.Y向蛇行);
            this.cbxMode.Items.Add(RepeatMode.Y向之行);
            this.cbxMode.SelectedIndex = 0;

            this.ReadLanguageResources();
        }


        ///<summary>
        /// Description	:切换多个拼版阵列时的基准点
        /// Author  	:liyi
        /// Date		:2019/06/20
        ///</summary>   
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxOrigins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxOrigins.Items.Count > 0)
            {
                if (this.listBoxOrigins.SelectedIndex != -1)
                {
                    tbOriginX.Text = patternOriList[this.listBoxOrigins.SelectedIndex].Point.X.ToString("0.000");
                    tbOriginY.Text = patternOriList[this.listBoxOrigins.SelectedIndex].Point.Y.ToString("0.000");
                }
                else
                {
                    tbOriginX.Text = patternOriList[0].Point.X.ToString("0.000");
                    tbOriginY.Text = patternOriList[0].Point.Y.ToString("0.000");
                }
            }
        }

        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditStepAndRepeatForm1()
        {
            InitializeComponent();
        }
        private void tbHNums_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbVNums_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbOriginX_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbOriginY_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbHEndX_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbHEndY_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbVEndX_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
        }

        private void tbVEndY_TextChanged(object sender, System.EventArgs e)
        {
            calculate();
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
            if ((tbOriginX.Value == tbHEndX.Value && tbOriginY.Value == tbHEndY.Value)
                || (tbOriginX.Value == tbVEndX.Value && tbOriginY.Value == tbVEndY.Value)
                || (tbHEndX.Value == tbVEndX.Value && tbHEndY.Value == tbVEndY.Value))
            {
                return;
            }
            if (listBoxOrigins.Items.Count < 1)
            {
                //MessageBox.Show("No pattern selected");
                MessageBox.Show("请选择一个拼版");
                return;
            }
            //机械坐标->系统坐标
            PointD pO = this.pattern.MachineRel(tbOriginX.Value, tbOriginY.Value);
            PointD pH = this.pattern.MachineRel(tbHEndX.Value, tbHEndY.Value);
            PointD pV = this.pattern.MachineRel(tbVEndX.Value, tbVEndY.Value);
            double hxgap = tbHNums.Value == 1 ? 0 : (pH.X - pO.X) / (tbHNums.Value - 1);
            double hygap = tbHNums.Value == 1 ? 0 : (pH.Y - pO.Y) / (tbHNums.Value - 1);
            double vxgap = tbVNums.Value == 1 ? 0 : (pV.X - pO.X) / (tbVNums.Value - 1);
            double vygap = tbVNums.Value == 1 ? 0 : (pV.Y - pO.Y) / (tbVNums.Value - 1);

            int basePattern = this.listBoxOrigins.SelectedIndex;
            if (basePattern < 0)
            {
                basePattern = 0;
            }

            patternItems.Clear();
            switch ((RepeatMode)this.cbxMode.SelectedIndex)
            {
                case RepeatMode.X向蛇行:
                    for (int i = 0; i < tbVNums.Value; i++)
                    {
                        if (i % 2 == 0)
                        {
                            for (int j = 0; j < tbHNums.Value; j++)
                            {
                                PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                for (int index = 0; index < patternOriList.Count; index++)
                                {
                                    VectorD offset = patternOriList[index].Point - patternOriList[basePattern].Point;
                                    patternItems.Add(new PatternItem(p + offset, patternOriList[index].PatternName));
                                }
                            }
                        }
                        else
                        {
                            for (int j = tbHNums.Value - 1; j >= 0; j--)
                            {
                                PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                for (int index = patternOriList.Count - 1; index >= 0; index--)
                                {
                                    VectorD offset = patternOriList[index].Point - patternOriList[basePattern].Point;
                                    patternItems.Add(new PatternItem(p + offset, patternOriList[index].PatternName));
                                }
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
                            for (int index = 0; index < patternOriList.Count; index++)
                            {
                                VectorD offset = patternOriList[index].Point - patternOriList[basePattern].Point;
                                patternItems.Add(new PatternItem(p + offset, patternOriList[index].PatternName));
                            }
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
                                for (int index = 0; index < patternOriList.Count; index++)
                                {
                                    PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                    VectorD offset = patternOriList[index].Point - patternOriList[basePattern].Point;
                                    patternItems.Add(new PatternItem(p + offset, patternOriList[index].PatternName));
                                }
                            }
                        }
                        else
                        {
                            for (int i = tbVNums.Value - 1; i >= 0; i--)
                            {
                                for (int index = patternOriList.Count - 1; index >= 0; index--)
                                {
                                    PointD p = new PointD(pO.X + j * hxgap + i * vxgap, pO.Y + j * hygap + i * vygap);
                                    VectorD offset = patternOriList[index].Point - patternOriList[basePattern].Point;
                                    patternItems.Add(new PatternItem(p + offset, patternOriList[index].PatternName));
                                }
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
                            for (int index = 0; index < patternOriList.Count; index++)
                            {
                                VectorD offset = patternOriList[index].Point - patternOriList[basePattern].Point;
                                patternItems.Add(new PatternItem(p + offset, patternOriList[index].PatternName));
                            }
                        }
                    }
                    break;
            }
        }

        private void btnTeachOrigin_Click(object sender, System.EventArgs e)
        {
            //没选中需要添加的pattern
            if (this.listBoxPatterns.SelectedIndex < 0)
            {
                return;
            }
            string selectPatternName = this.listBoxPatterns.SelectedItem.ToString();
            PointD pos = new PointD(Machine.Instance.Robot.PosX - origin.X, Machine.Instance.Robot.PosY - origin.Y);

            #region 添加需要阵列的pattern
            //已有的不添加
            int index = -1;
            for (int i = 0; i < patternOriList.Count; i++)
            {
                if (patternOriList[i].PatternName == selectPatternName)
                {
                    index = i;
                }
            }
            if (index == -1)
            {
                PatternItem pI = new PatternItem(pos, selectPatternName);
                patternOriList.Add(pI);
            }
            else
            {
                patternOriList[index].Point = pos;
            }
            #endregion

            listBoxOrigins.Items.Clear();
            foreach (PatternItem item in patternOriList)
            {
                listBoxOrigins.Items.Add(item);
            }
            tbOriginX.Text = patternOriList[0].Point.X.ToString("0.000");
            tbOriginY.Text = patternOriList[0].Point.Y.ToString("0.000");
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
            //Machine.Instance.Robot.MovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
        }

        private void btnGoToHEnd_Click(object sender, System.EventArgs e)
        {
            if (!tbHEndX.IsValid || !tbHEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbHEndX.Value, origin.Y + tbHEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbHEndX.Value, origin.Y + tbHEndY.Value);
        }

        private void btnGoToVEnd_Click(object sender, System.EventArgs e)
        {
            if (!tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbVEndX.Value, origin.Y + tbVEndY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbVEndX.Value, origin.Y + tbVEndY.Value);
        }

        private void btnGoTo_Click(object sender, System.EventArgs e)
        {
            if (listBoxOrigins.SelectedIndex < 0)
            {
                return;
            }
            PointD p = patternItems[listBoxOrigins.SelectedIndex].Point;
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY((origin.ToSystem() + p).ToMachine());
        }

        ///<summary>
        /// Description	:移除不需要阵列的拼版
        /// Author  	:liyi
        /// Date		:2019/06/20
        ///</summary>   
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //无阵列拼版返回
            if (patternOriList.Count < 1 || listBoxOrigins.Items.Count < 1)
            {
                return;
            }
            //无选中返回
            if (listBoxOrigins.SelectedIndex < 0)
            {
                return;
            }

            patternOriList.RemoveAt(listBoxOrigins.SelectedIndex);
            listBoxOrigins.Items.RemoveAt(listBoxOrigins.SelectedIndex);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }


        ///<summary>
        /// Description	:根据界面设置参数生成阵列的Do指令
        /// Author  	:liyi
        /// Date		:2019/06/20
        ///</summary>   
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (listBoxPatterns.SelectedIndex < 0)
            {
                //MessageBox.Show("No Pattern is selected.");
                MessageBox.Show("请选择一个拼版");
                return;
            }
            if (listBoxOrigins.Items.Count < 1)
            {
                //MessageBox.Show("No Pattern is selected.");
                MessageBox.Show("请选择一个拼版");
                return;
            }
            if (!tbHNums.IsValid || !tbVNums.IsValid || !tbOriginX.IsValid || !tbOriginY.IsValid
                || !tbHEndX.IsValid || !tbHEndY.IsValid || !tbVEndX.IsValid || !tbVEndY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请选择一个拼版");
                return;
            }
            if (tbVNums.Value < 1)
            {
                //MessageBox.Show("Vertical Nums can not be smaller than 1.");
                MessageBox.Show("纵向拼版数不可以小于 1.");
                return;
            }
            if (tbHNums.Value < 1)
            {
                //MessageBox.Show("Horizontal Nums can not be smaller than 1.");
                MessageBox.Show("横向拼版数不可以小于 1.");
                return;
            }
            if (tbOriginX.Value == tbHEndX.Value && tbOriginY.Value == tbHEndY.Value)
            {
                //MessageBox.Show("Origin can not be same with horizontal end.");
                MessageBox.Show("原点坐标和横向终点坐标不可以相同.");
                return;
            }
            if (tbOriginX.Value == tbVEndX.Value && tbOriginY.Value == tbVEndY.Value)
            {
                //MessageBox.Show("Origin can not be same with vertical end.");
                MessageBox.Show("原点坐标和纵向终点坐标不可以相同.");
                return;
            }
            if (tbHEndX.Value == tbVEndX.Value && tbHEndY.Value == tbVEndY.Value)
            {
                //MessageBox.Show("Horizontal end can not be same with vertical end.");
                MessageBox.Show("横向的终点坐标和纵向的终点坐标不可以相同.");
                return;
            }
            //机械坐标->系统坐标
            PointD pO = this.pattern.MachineRel(tbOriginX.Value, tbOriginY.Value);
            PointD pH = this.pattern.MachineRel(tbHEndX.Value, tbHEndY.Value);
            PointD pV = this.pattern.MachineRel(tbVEndX.Value, tbVEndY.Value);
            List<CmdLine> doCmdLineList = new List<CmdLine>();
            foreach (PatternItem item in patternItems)
            {
                doCmdLineList.Add(new DoCmdLine(item.PatternName, item.Point.X, item.Point.Y));
            }
            if (doCmdLineList.Count > 0)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINES, this, doCmdLineList);
            }
            Close();
        }

        private void cbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxMode.SelectedIndex < 0)
            {
                return;
            }
            calculate();
        }

        ///<summary>
        /// Description	:示教需要阵列的拼版原点
        /// Author  	:liyi
        /// Date		:2019/06/20
        ///</summary>   
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeachPattern_Click(object sender, EventArgs e)
        {
            if (this.listBoxPatterns.SelectedIndex < 0)
            {
                return;
            }
            PatternItem pI = new PatternItem(new PointD(), this.listBoxPatterns.SelectedItem.ToString());
            //已有的不添加
            foreach (var item in patternOriList)
            {
                if (item.PatternName == pI.PatternName)
                {
                    return;
                }
            }
            patternOriList.Add(pI);
            listBoxOrigins.Items.Add(pI);
        }

        class PatternItem
        {
            public PatternItem(PointD point, string patternName)
            {
                this.Point = point;
                this.PatternName = patternName;
            }
            public string PatternName { get; set; }
            public PointD Point { get; set; }
            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("{0} [{1},{2}]", PatternName, Point.X.ToString("0.000"), Point.Y.ToString("0.000")));
                return builder.ToString();
            }
        }

    }
}
