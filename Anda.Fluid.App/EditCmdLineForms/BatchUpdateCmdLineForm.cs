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
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;

namespace Anda.Fluid.App.EditCmdLineForms
{

    ///<summary>
    /// Description	:批量修改选中点胶轨迹属性窗体，支持增量修改或修改为固定值
    /// Author  	:liyi
    /// Date		:2019/07/24
    ///</summary>   
    public partial class BatchUpdateCmdLineForm : EditFormBase, IMsgSender
    {
        private List<CmdLine> updateCmdLines = new List<CmdLine>();
        /// <summary>
        /// 是否固定胶量
        /// </summary>
        private bool isConstantWeight = true;
        /// <summary>
        /// 所有轨迹都是相同控制模式的情况，是速度还是胶量
        /// </summary>
        private bool weightControlType = true;
        /// <summary>
        /// 获取的轨迹是否都是同一模式
        /// </summary>
        private bool WeightControlTypeIsSame = true;
        /// <summary>
        /// 获取的所有线、圆弧、多线段的参数类型
        /// </summary>
        private int cmdLineType = -1;
        /// <summary>
        /// 获取的所有点的参数类型
        /// </summary>
        private int cmdDotType = -1;
        /// <summary>
        /// 线类型的轨迹的参数是否都是同一类型
        /// </summary>
        private bool cmdLineTypeIsSame = true;
        /// <summary>
        /// 点轨迹的参数是否都是同一类型
        /// </summary>
        private bool cmdDotTypeIsSame = true;
        /// <summary>
        /// 防止控件循环响应变量
        /// </summary>
        private bool permitReacting = true;

        /// <summary>
        /// 是否有变量
        /// </summary>
        private bool lineParamGet = false;
        private bool dotParamGet = false;
        //Symbol 线角度
        private double transPosR = 2;
        private Pattern pattern;
        private PointD origin;

        private bool hasSymbolLine = false;
        private BatchUpdateCmdLineForm()
        {
            InitializeComponent();
        }

        public BatchUpdateCmdLineForm(Pattern pattern, List<CmdLine> cmdLines)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            this.updateCmdLines = cmdLines;
            this.Load += BatchUpdateCmdLineForm_Load;
            this.cbIsWeightControl.Click += CbIsWeightControl_Click;
            this.cbxDotType.SelectedIndexChanged += CbxDotType_SelectedIndexChanged;
            this.cbxLineType.SelectedIndexChanged += CbxLineType_SelectedIndexChanged;
            this.rdoIncrementWeight.Checked = true;
            this.tbRefX.Text = 0.ToString("0.000");
            this.tbRefY.Text = 0.ToString("0.000");
            this.tbTargetX.Text = 0.ToString("0.000");
            this.tbTargetY.Text = 0.ToString("0.000");
            this.tbXOffset.Text = 0.ToString("0.000");
            this.tbYOffset.Text = 0.ToString("0.000");
            this.tbRefX.TextChanged += TbRefX_TextChanged;
            this.tbRefY.TextChanged += TbRefY_TextChanged;
            this.tbTargetX.TextChanged += TbTargetX_TextChanged;
            this.tbTargetY.TextChanged += TbTargetY_TextChanged;
            this.tbXOffset.TextChanged += TbXOffset_TextChanged;
            this.tbYOffset.TextChanged += TbYOffset_TextChanged;
            this.btnInsert.Enabled = this.cbxUseInsert.Checked;
            this.btnUpdate.Enabled = !this.cbxUseInsert.Checked;
            this.cbxRotate.Items.Add("0");
            this.cbxRotate.Items.Add("90");
            this.cbxRotate.Items.Add("180");
            this.cbxRotate.Items.Add("270");
            this.cbxRotate.Items.Add("360");
            this.cbxRotate.SelectedIndex = 0;
            for (int i = 0; i < 10; i++)
            {
                this.cbxDotType.Items.Add((DotStyle)i);
                this.cbxLineType.Items.Add((LineStyle)i);
            }

            this.tabPgSymbol.Hide();
            this.tabParm.TabPages.Remove(tabPgSymbol);
            //this.tabParm.Hide();
        }

        private void TbYOffset_TextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbYOffset.IsValid)
            {
                return;
            }
            permitReacting = false;
            double oldOffset = this.tbTargetY.Value - this.tbRefY.Value;
            this.tbTargetY.Text = (this.tbTargetY.Value + this.tbYOffset.Value - oldOffset).ToString("0.000");
            permitReacting = true;
        }

        private void TbXOffset_TextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbXOffset.IsValid)
            {
                return;
            }
            permitReacting = false;
            double oldOffset = this.tbTargetX.Value - this.tbRefX.Value;
            this.tbTargetX.Text = (this.tbTargetX.Value + this.tbXOffset.Value - oldOffset).ToString("0.000");
            permitReacting = true;
        }

        private void TbTargetY_TextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbTargetY.IsValid)
            {
                return;
            }
            permitReacting = false;
            this.tbYOffset.Text = (this.tbTargetY.Value - this.tbRefY.Value).ToString("0.000");
            permitReacting = true;
        }

        private void TbTargetX_TextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbTargetY.IsValid)
            {
                return;
            }
            permitReacting = false;
            this.tbXOffset.Text = (this.tbTargetX.Value - this.tbRefX.Value).ToString("0.000");
            permitReacting = true;
        }

        private void TbRefY_TextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbRefY.IsValid)
            {
                return;
            }
            permitReacting = false;
            this.tbYOffset.Text = (this.tbTargetY.Value - this.tbRefY.Value).ToString("0.000");
            permitReacting = true;
        }

        private void TbRefX_TextChanged(object sender, EventArgs e)
        {
            if (!permitReacting)
            {
                return;
            }
            if (!tbRefX.IsValid)
            {
                return;
            }
            permitReacting = false;
            this.tbXOffset.Text = (this.tbTargetX.Value - this.tbRefX.Value).ToString("0.000");
            permitReacting = true;
        }

        private void CbxLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmdLineType = this.cbxLineType.SelectedIndex;
        }

        private void CbxDotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmdDotType = this.cbxDotType.SelectedIndex;
        }

        private void CbIsWeightControl_Click(object sender, EventArgs e)
        {
            if (this.cbIsWeightControl.CheckState != CheckState.Unchecked)
            {
                this.rdoConstantWeight.Enabled = true;
                this.rdoIncrementWeight.Enabled = true;
                this.tbWeight.Enabled = true;
            }
            else
            {
                this.rdoConstantWeight.Enabled = false;
                this.rdoIncrementWeight.Enabled = false;
                this.tbWeight.Enabled = false;
            }
        }

        private void BatchUpdateCmdLineForm_Load(object sender, EventArgs e)
        {
            this.RemainGlueCmd();
            this.GetSymbolParamType();
            this.GetSameProperty();
            if (this.WeightControlTypeIsSame)
            {
                //非胶量模式不启用胶量数据
                this.cbIsWeightControl.Checked = this.weightControlType;
                this.rdoConstantWeight.Enabled = this.weightControlType;
                this.rdoIncrementWeight.Enabled = this.weightControlType;
                this.tbWeight.Enabled = this.weightControlType;
            }
            else
            {
                //不是所有轨迹都启用胶量模式，勾选框灰色
                this.cbIsWeightControl.CheckState = CheckState.Indeterminate;
            }

            if (!this.lineParamGet)
            {
                this.lblLineType.Enabled = false;
                this.cbxLineType.Enabled = false;
            }
            if (!this.dotParamGet)
            {
                this.lblDotType.Enabled = false;
                this.cbxDotType.Enabled = false;
            }

            this.cbxDotType.SelectedIndex = this.cmdDotType;
            this.cbxLineType.SelectedIndex = this.cmdLineType;
            if (this.hasSymbolLine)
            {
                this.symbolLine1.SetPrm(this.transPosR);
                this.tabParm.TabPages.Add(tabPgSymbol);                
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            this.UpdateCmdLineParam(this.updateCmdLines);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author: liyi
        /// Date: 2019/08/20
        /// Description: 判断是否所有轨迹是否都是胶量模式、是否所有参数类型
        /// </summary>
        private void GetSameProperty()
        {
            foreach (CmdLine cmdLine in this.updateCmdLines)
            {
                if (this.cmdLineTypeIsSame)
                {
                    if (cmdLine is CircleCmdLine)
                    {
                        CircleCmdLine temp = cmdLine as CircleCmdLine;
                        if (temp.IsWeightControl != this.weightControlType)
                        {
                            this.WeightControlTypeIsSame = false;
                        }
                        if (this.cmdLineType != (int)temp.LineStyle)
                        {
                            this.cmdLineTypeIsSame = false;
                        }
                    }
                    else if (cmdLine is ArcCmdLine)
                    {
                        ArcCmdLine temp = cmdLine as ArcCmdLine;
                        if (temp.IsWeightControl != this.weightControlType)
                        {
                            this.WeightControlTypeIsSame = false;
                        }
                        if (this.cmdLineType != (int)temp.LineStyle)
                        {
                            this.cmdLineTypeIsSame = false;
                        }
                    }
                    else if (cmdLine is SnakeLineCmdLine)
                    {
                        SnakeLineCmdLine temp = cmdLine as SnakeLineCmdLine;
                        if (temp.IsWeightControl != this.weightControlType)
                        {
                            this.WeightControlTypeIsSame = false;
                        }
                        if (this.cmdLineType != (int)temp.LineStyle)
                        {
                            this.cmdDotTypeIsSame = false;
                        }
                    }
                    else if (cmdLine is LineCmdLine)
                    {
                        LineCmdLine temp = cmdLine as LineCmdLine;
                        if (temp.IsWeightControl != this.weightControlType)
                        {
                            this.WeightControlTypeIsSame = false;
                        }
                        if (this.cmdLineType != (int)temp.LineStyle)
                        {
                            this.cmdLineTypeIsSame = false;
                        }
                    }
                }
                if (this.cmdDotTypeIsSame)
                {
                    if (cmdLine is DotCmdLine)
                    {
                        DotCmdLine temp = cmdLine as DotCmdLine;
                        if (temp.IsWeightControl != this.weightControlType)
                        {
                            this.WeightControlTypeIsSame = false;
                        }
                        if (this.cmdDotType != (int)temp.DotStyle)
                        {
                            this.cmdDotTypeIsSame = false;
                        }
                    }
                }

                if ((!this.cmdDotTypeIsSame || !this.dotParamGet) && (!this.cmdLineTypeIsSame || !this.lineParamGet) && !this.WeightControlTypeIsSame)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 获取第一点轨迹和第一个线轨迹的参数类型
        /// </summary>
        private void GetSymbolParamType()
        {
            //只批量修改和选中的第一条轨迹相同类型的轨迹
            int index = 0;
            foreach (CmdLine cmdLine in this.updateCmdLines)
            {
                if (!lineParamGet)
                {
                    if (cmdLine is CircleCmdLine)
                    {
                        CircleCmdLine temp = cmdLine as CircleCmdLine;
                        this.cmdLineType = (int)temp.LineStyle;
                        if (index == 0)
                        {
                            this.weightControlType = temp.IsWeightControl;
                        }
                        lineParamGet = true;
                    }
                    else if (cmdLine is ArcCmdLine)
                    {
                        ArcCmdLine temp = cmdLine as ArcCmdLine;
                        this.cmdLineType = (int)temp.LineStyle;
                        if (index == 0)
                        {
                            this.weightControlType = temp.IsWeightControl;
                        }
                        lineParamGet = true;
                    }
                    else if (cmdLine is SnakeLineCmdLine)
                    {
                        SnakeLineCmdLine temp = cmdLine as SnakeLineCmdLine;
                        this.cmdLineType = (int)temp.LineStyle;
                        if (index == 0)
                        {
                            this.weightControlType = temp.IsWeightControl;
                        }
                        lineParamGet = true;
                    }
                    else if (cmdLine is LineCmdLine)
                    {
                        LineCmdLine temp = cmdLine as LineCmdLine;
                        this.cmdLineType = (int)temp.LineStyle;
                        if (index == 0)
                        {
                            this.weightControlType = temp.IsWeightControl;
                        }
                        lineParamGet = true;
                    }
                }

                if (!dotParamGet)
                {
                    if (cmdLine is DotCmdLine)
                    {
                        DotCmdLine temp = cmdLine as DotCmdLine;
                        this.cmdDotType = (int)temp.DotStyle;
                        if (index == 0)
                        {
                            this.weightControlType = temp.IsWeightControl;
                        }
                        dotParamGet = true;
                    }
                }
                index++;
                //第一个点和第一个线的参数都获取后，停止循环
                if (lineParamGet && dotParamGet)
                {
                    break;
                }
            }
        }
      
        /// <summary>
        /// Author: liyi
        /// Date:   2019/08/20
        /// Description:去除非点胶指令
        /// </summary>
        private void RemainGlueCmd()
        {
            List<CmdLine> temp = new List<CmdLine>();
            foreach (CmdLine cmdLine in this.updateCmdLines)
            {
                if (cmdLine is CircleCmdLine || cmdLine is ArcCmdLine || cmdLine is DotCmdLine || cmdLine is SnakeLineCmdLine || cmdLine is LineCmdLine
                    || cmdLine is MultiTracesCmdLine||cmdLine is SymbolLinesCmdLine)
                {
                    temp.Add(cmdLine);
                    if (cmdLine is SymbolLinesCmdLine)
                    {
                        SymbolLinesCmdLine symbolLine = cmdLine as SymbolLinesCmdLine;
                        this.hasSymbolLine = true;
                        this.transPosR = symbolLine.Symbols[0].transitionR;
                    }
                }
            }
            this.updateCmdLines = temp;
        }

        ///<summary>
        /// Description	:根据输入的数据类型返回最终结果值
        /// Author  	:liyi
        /// Date		:2019/07/24
        ///</summary>   
        /// <param name="oldValue">原始数据</param>
        /// <param name="inputValue">输入数据</param>
        /// <param name="isConstant">是否是固定值，true：固定值，false：在原数值上增量变化</param>
        /// <returns></returns>
        private double GetNewValue(double oldValue, double inputValue, bool isConstant)
        {
            if (isConstant)
            {
                //如果不改变所有胶量模式，且输入数值为0时，胶量模式轨迹胶量不变
                if (this.cbIsWeightControl.CheckState == CheckState.Indeterminate && inputValue == 0)
                {
                    return oldValue;
                }
                return inputValue;
            }
            else
            {
                return oldValue + inputValue;
            }
        }
        /// <summary>
        /// Author: liyi
        /// Date:   2019/08/27
        /// Description:获取新坐标点
        /// </summary>
        /// <param name="pos">原始坐标</param>
        /// <param name="rotateCenter">旋转中心</param>
        /// <param name="offsetX">X偏移量</param>
        /// <param name="offsetY">Y偏移量</param>
        /// <param name="rotateAngle">旋转角</param>
        /// <returns></returns>
        private PointD GetNewPosition(PointD pos, PointD rotateCenter, double offsetX, double offsetY, double rotateAngle)
        {
            PointD result = new PointD();
            // 平移旋转处理
            // 先旋转
            if (rotateAngle != 0)
            {
                result = pos.Rotate(rotateCenter, rotateAngle);
            }
            else
            {
                result.X = pos.X;
                result.Y = pos.Y;
            }
            // 再平移
            result.X += offsetX;
            result.Y += offsetY;

            return result;
        }

        private void btnTeachRef_Click(object sender, EventArgs e)
        {
            tbRefX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbRefY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnTeachTarget_Click(object sender, EventArgs e)
        {
            tbTargetX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbTargetY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnRefGoto_Click(object sender, EventArgs e)
        {
            if (!tbRefX.IsValid || !tbRefY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(this.origin.X + tbRefX.Value, this.origin.Y + tbRefY.Value);
        }

        private void btnTargetGoto_Click(object sender, EventArgs e)
        {
            if (!tbTargetX.IsValid || !tbTargetY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(this.origin.X + tbTargetX.Value, this.origin.Y + tbTargetY.Value);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            List<CmdLine> temp = new List<CmdLine>();
            foreach (CmdLine item in this.updateCmdLines)
            {
                temp.Add(item.Clone() as CmdLine);
            }
            this.UpdateCmdLineParam(temp);
            if (temp.Count <= 0)
            {
                return;
            }
            MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINES, this, temp);
        }

        private void cbxUseInsert_CheckedChanged(object sender, EventArgs e)
        {
            this.btnInsert.Enabled = this.cbxUseInsert.Checked;
            this.btnUpdate.Enabled = !this.cbxUseInsert.Checked;
        }

        /// <summary>
        /// Author: liyi
        /// Date:   2019/08/27
        /// Description:用于将界面参数更新至传入的轨迹数组
        /// </summary>
        /// <param name="cmdLines"></param>
        private void UpdateCmdLineParam(List<CmdLine> cmdLines)
        {
            if (cmdLines.Count <= 0)
            {
                return;
            }
            if (this.rdoIncrementWeight.Checked)
            {
                isConstantWeight = false;
            }
            else if (this.rdoConstantWeight.Checked)
            {
                isConstantWeight = true;
            }
            if (this.cbxRotate.SelectedIndex == -1)
            {
                this.cbxRotate.SelectedIndex = 0;
            }
            double rotateAngle = this.cbxRotate.SelectedIndex * 90;

            // 机械坐标 -> 系统坐标
            PointD referencePoint = this.pattern.SystemRel(new PointD(this.tbRefX.Value, this.tbRefY.Value));
            PointD offsetPoint = this.pattern.SystemRel(new PointD(this.tbXOffset.Value, this.tbYOffset.Value));
            double offsetX = offsetPoint.X; //this.tbXOffset.Value;
            double offsetY = offsetPoint.Y; //this.tbYOffset.Value;
            foreach (CmdLine cmdLine in cmdLines)
            {
                //判断是否是胶量模式
                //1.胶量模式，全部启用胶量模式
                //2.非胶量模式，全部禁用胶量模式
                //3.都有的状态，只增加胶量值，不更改模式
                if (cmdLine is CircleCmdLine)
                {
                    CircleCmdLine circleCmdLine = cmdLine as CircleCmdLine;
                    circleCmdLine.Weight = GetNewValue(circleCmdLine.Weight, tbWeight.Value, isConstantWeight);
                    PointD temp = GetNewPosition(circleCmdLine.Start, referencePoint, offsetX, offsetY, rotateAngle);
                    circleCmdLine.Start.X = temp.X;
                    circleCmdLine.Start.Y = temp.Y;
                    temp = GetNewPosition(circleCmdLine.Start, referencePoint, offsetX, offsetY, rotateAngle);
                    circleCmdLine.End.X = temp.X;
                    circleCmdLine.End.Y = temp.Y;
                    temp = GetNewPosition(circleCmdLine.Middle, referencePoint, offsetX, offsetY, rotateAngle);
                    circleCmdLine.Middle.X = temp.X;
                    circleCmdLine.Middle.Y = temp.Y;
                    temp = GetNewPosition(circleCmdLine.Middle, referencePoint, offsetX, offsetY, rotateAngle);
                    circleCmdLine.Center.X = temp.X;
                    circleCmdLine.Center.Y = temp.Y;
                    if (this.cbxLineType.SelectedIndex != -1)
                    {
                        circleCmdLine.LineStyle = (LineStyle)this.cmdLineType;
                    }
                    if (this.cbIsWeightControl.CheckState != CheckState.Indeterminate)
                    {
                        circleCmdLine.IsWeightControl = this.cbIsWeightControl.Checked;
                    }
                }
                else if (cmdLine is ArcCmdLine)
                {
                    ArcCmdLine arcCmdLine = cmdLine as ArcCmdLine;
                    arcCmdLine.Weight = GetNewValue(arcCmdLine.Weight, tbWeight.Value, isConstantWeight);
                    PointD temp = GetNewPosition(arcCmdLine.Start, referencePoint, offsetX, offsetY, rotateAngle);
                    arcCmdLine.Start.X = temp.X;
                    arcCmdLine.Start.Y = temp.Y;
                    temp = GetNewPosition(arcCmdLine.End, referencePoint, offsetX, offsetY, rotateAngle);
                    arcCmdLine.End.X = temp.X;
                    arcCmdLine.End.Y = temp.Y;
                    temp = GetNewPosition(arcCmdLine.Middle, referencePoint, offsetX, offsetY, rotateAngle);
                    arcCmdLine.Middle.X = temp.X;
                    arcCmdLine.Middle.Y = temp.Y;
                    temp = GetNewPosition(arcCmdLine.Center, referencePoint, offsetX, offsetY, rotateAngle);
                    arcCmdLine.Center.X = temp.X;
                    arcCmdLine.Center.Y = temp.Y;
                    if (this.cbxLineType.SelectedIndex != -1)
                    {
                        arcCmdLine.LineStyle = (LineStyle)this.cmdLineType;
                    }
                    if (this.cbIsWeightControl.CheckState != CheckState.Indeterminate)
                    {
                        arcCmdLine.IsWeightControl = this.cbIsWeightControl.Checked;
                    }
                }
                else if (cmdLine is DotCmdLine)
                {
                    DotCmdLine dotCmdLine = cmdLine as DotCmdLine;
                    dotCmdLine.Weight = GetNewValue(dotCmdLine.Weight, tbWeight.Value, isConstantWeight);
                    PointD temp = GetNewPosition(dotCmdLine.Position, referencePoint, offsetX, offsetY, rotateAngle);
                    dotCmdLine.Position.X = temp.X;
                    dotCmdLine.Position.Y = temp.Y;
                    if (this.cbxDotType.SelectedIndex != -1)
                    {
                        dotCmdLine.DotStyle = (DotStyle)this.cmdDotType;
                    }
                    if (this.cbIsWeightControl.CheckState != CheckState.Indeterminate)
                    {
                        dotCmdLine.IsWeightControl = this.cbIsWeightControl.Checked;
                    }
                }
                else if (cmdLine is SnakeLineCmdLine)
                {
                    SnakeLineCmdLine snakeLineCmdLine = cmdLine as SnakeLineCmdLine;
                    if (this.cbxLineType.SelectedIndex != -1)
                    {
                        snakeLineCmdLine.LineStyle = (LineStyle)this.cmdLineType;
                    }
                    if (this.cbIsWeightControl.CheckState != CheckState.Indeterminate)
                    {
                        snakeLineCmdLine.IsWeightControl = this.cbIsWeightControl.Checked;
                    }
                    PointD temp = new PointD();
                    foreach (LineCoordinate item in snakeLineCmdLine.LineCoordinateList)
                    {
                        temp = GetNewPosition(item.Start, referencePoint, offsetX, offsetY, rotateAngle);
                        item.Start.X = temp.X;
                        item.Start.Y = temp.Y;
                        temp = GetNewPosition(item.End, referencePoint, offsetX, offsetY, rotateAngle);
                        item.End.X = temp.X;
                        item.End.Y = temp.Y;
                    }
                }
                else if (cmdLine is LineCmdLine)
                {
                    LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                    if (this.cbxLineType.SelectedIndex != -1)
                    {
                        lineCmdLine.LineStyle = (LineStyle)this.cmdLineType;
                    }
                    for (int i = 0; i < lineCmdLine.LineCoordinateList.Count; i++)
                    {
                        lineCmdLine.LineCoordinateList[i].LineStyle = lineCmdLine.LineStyle;
                    }
                    if (this.cbIsWeightControl.CheckState != CheckState.Indeterminate)
                    {
                        lineCmdLine.IsWeightControl = this.cbIsWeightControl.Checked;
                    }
                    if (lineCmdLine.LineMethod == LineMethod.Single)
                    {
                        lineCmdLine.WholeWeight = GetNewValue(lineCmdLine.WholeWeight, tbWeight.Value, isConstantWeight);
                    }
                    else if (lineCmdLine.LineMethod == LineMethod.Multi)
                    {
                        lineCmdLine.WholeWeight = GetNewValue(lineCmdLine.WholeWeight, tbWeight.Value, isConstantWeight);
                    }
                    else if (lineCmdLine.LineMethod == LineMethod.Poly)
                    {
                        lineCmdLine.WholeWeight = GetNewValue(lineCmdLine.WholeWeight, tbWeight.Value, isConstantWeight);
                    }
                    PointD temp = new PointD();
                    foreach (LineCoordinate item in lineCmdLine.LineCoordinateList)
                    {
                        temp = GetNewPosition(item.Start, referencePoint, offsetX, offsetY, rotateAngle);
                        item.Start.X = temp.X;
                        item.Start.Y = temp.Y;
                        temp = GetNewPosition(item.End, referencePoint, offsetX, offsetY, rotateAngle);
                        item.End.X = temp.X;
                        item.End.Y = temp.Y;
                    }
                }
                else if (cmdLine is MultiTracesCmdLine)
                {
                    MultiTracesCmdLine multiTrace = cmdLine as MultiTracesCmdLine;
                    PointD temp = new PointD();
                    foreach (var item in multiTrace.Traces)
                    {
                        if (item is TraceLine)
                        {
                            temp = GetNewPosition(item.Start, referencePoint, offsetX, offsetY, rotateAngle);
                            item.Start.X = temp.X;
                            item.Start.Y = temp.Y;
                            temp = GetNewPosition(item.End, referencePoint, offsetX, offsetY, rotateAngle);
                            item.End.X = temp.X;
                            item.End.Y = temp.Y;
                        }
                        else if (item is TraceArc)
                        {
                            TraceArc traceArc = item as TraceArc;
                            temp = GetNewPosition(traceArc.Start, referencePoint, offsetX, offsetY, rotateAngle);
                            traceArc.Start.X = temp.X;
                            traceArc.Start.Y = temp.Y;
                            temp = GetNewPosition(traceArc.Mid, referencePoint, offsetX, offsetY, rotateAngle);
                            traceArc.Mid.X = temp.X;
                            traceArc.Mid.Y = temp.Y;
                            temp = GetNewPosition(traceArc.End, referencePoint, offsetX, offsetY, rotateAngle);
                            traceArc.End.X = temp.X;
                            traceArc.End.Y = temp.Y;
                        }
                    }
                }
                else if (cmdLine is SymbolLinesCmdLine)
                {
                    SymbolLinesCmdLine symbolLinesCmdLine = cmdLine as SymbolLinesCmdLine;
                    PointD temp = new PointD();
                    double r = this.symbolLine1.GetPrm();
                    foreach (var item in symbolLinesCmdLine.Symbols)
                    {
                        item.transitionR = r;
                        if (item.symbolType==SymbolType.Line)
                        {
                            temp = GetNewPosition(item.symbolPoints[0], referencePoint, offsetX, offsetY, rotateAngle);
                            item.symbolPoints[0].X = temp.X;
                            item.symbolPoints[0].Y = temp.Y;
                            temp = GetNewPosition(item.symbolPoints[1], referencePoint, offsetX, offsetY, rotateAngle);
                            item.symbolPoints[1].X = temp.X;
                            item.symbolPoints[1].Y = temp.Y;                          
                        }
                        else if(item.symbolType==SymbolType.Arc)
                        {
                            temp = GetNewPosition(item.symbolPoints[0], referencePoint, offsetX, offsetY, rotateAngle);
                            item.symbolPoints[0].X = temp.X;
                            item.symbolPoints[0].Y = temp.Y;
                            temp = GetNewPosition(item.symbolPoints[1], referencePoint, offsetX, offsetY, rotateAngle);
                            item.symbolPoints[1].X = temp.X;
                            item.symbolPoints[1].Y = temp.Y;
                            temp = GetNewPosition(item.symbolPoints[2], referencePoint, offsetX, offsetY, rotateAngle);
                            item.symbolPoints[2].X = temp.X;
                            item.symbolPoints[2].Y = temp.Y;

                        }
                    }
                    

                }

            }
        }

    }
}
