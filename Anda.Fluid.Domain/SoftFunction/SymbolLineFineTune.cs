using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
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

namespace Anda.Fluid.Domain.SoftFunction
{
    public partial class SymbolLineFineTune : Form
    {
        private List<SymbolLinesCmd> symbolLinesCmd = new List<SymbolLinesCmd>();
        private CoordinateCorrector coordinateCorrector;
        private List<ModifyPoint> modifyPoints;
        int index = 0;
        public SymbolLineFineTune()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        /// <summary>
        /// 传入SymbolLinesCmd集合，如果有需要手动校正的点，则弹出窗体
        /// </summary>
        /// <param name="symbolLinesCmd"></param>
        /// <param name="coordinateCorrector"></param>
        public void SetData(List<SymbolLinesCmd> symbolLinesCmd, CoordinateCorrector coordinateCorrector)
        {
            this.symbolLinesCmd = symbolLinesCmd;
            this.coordinateCorrector = coordinateCorrector;

            if (NeedTeach())
            {
                Machine.Instance.IsProducting = false;
                this.ParseSymbolLinesCmds();
                Machine.Instance.Robot.ManualMovePosXY(this.modifyPoints[0].PointWithCorrect);
                this.ShowDialog();
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 是否有需要手动示教的点
        /// </summary>
        /// <returns></returns>
        private bool NeedTeach()
        {
            foreach (SymbolLinesCmd cmd in symbolLinesCmd)
            {
                foreach (SymbolLine line in cmd.Symbls)
                {
                    foreach (bool item in line.needOffset)
                    {
                        if (item)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 解析所有的命令，得到要进行示教修改点的集合
        /// </summary>
        private void ParseSymbolLinesCmds()
        {
            this.modifyPoints = new List<ModifyPoint>();

            foreach (SymbolLinesCmd cmd in symbolLinesCmd)
            {
                foreach (SymbolLine line in cmd.Symbls)
                {
                    for (int i = 0; i < line.needOffset.Count; i++)
                    {
                        if (line.needOffset[i])
                        {
                            ModifyPoint modifyPoint = new ModifyPoint(line, i, cmd, this.coordinateCorrector);
                            this.modifyPoints.Add(modifyPoint);
                        }
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.index <= this.modifyPoints.Count)
            {
                this.ModifyPoint();
            }
        }

        private void ModifyPoint()
        {
            //记录被修改的点数量，普通情况是1个点，但如果两个点坐标一样说明是首尾相连的点，则应该两个点都要被修改
            int skip = 0;

            //得到补偿值
            PointD offset = new PointD();
            offset.X = Machine.Instance.Robot.PosX - this.modifyPoints[index].PointWithCorrect.X;
            offset.Y = Machine.Instance.Robot.PosY - this.modifyPoints[index].PointWithCorrect.Y;

            //修改当前点的补偿值
            if (this.modifyPoints[index].SymbolLine.symbolOffset.Count == 0)
            {
                foreach (var item in this.modifyPoints[index].SymbolLine.symbolPoints)
                {
                    this.modifyPoints[index].SymbolLine.symbolOffset.Add(new PointD(0, 0));
                }
            }
            this.modifyPoints[index].SymbolLine.symbolOffset[this.modifyPoints[index].PointNo] = offset;

            //有一个点需要被修改
            skip++;

            //如果当前的点索引已经超出了范围，则说明所有点已经被修改，可以关闭窗体
            if (index >= this.modifyPoints.Count - 1)
            {
                this.Close();
                return;
            }

            //如果下一个点和当前点坐标一样，也对其进行修改
            if (this.modifyPoints[index + 1].PointWithCorrect.Equals(this.modifyPoints[index].PointWithCorrect))
            {
                //修改当前点的补偿值
                if (this.modifyPoints[index + 1].SymbolLine.symbolOffset.Count == 0)
                {
                    foreach (var item in this.modifyPoints[index + 1].SymbolLine.symbolPoints)
                    {
                        this.modifyPoints[index + 1].SymbolLine.symbolOffset.Add(new PointD(0, 0));
                    }
                }
                this.modifyPoints[index + 1].SymbolLine.symbolOffset[this.modifyPoints[index + 1].PointNo] = offset;
                //有两个点被修改
                skip++;
            }

            //重新赋值当前点
            this.index += skip;

            //如果当前点还是范围内，则机台移动过去
            if (index < this.modifyPoints.Count)
            {
                Machine.Instance.Robot.ManualMovePosXY(this.modifyPoints[index].PointWithCorrect);
            }
            //如果不是，则关闭窗体
            else
            {
                this.Close();
            }

        }

        private void SymbolLineFineTune_FormClosing(object sender, FormClosingEventArgs e)
        {
            Machine.Instance.IsProducting = true;
        }
    }

    /// <summary>
    /// 需要手动示教修改的点
    /// </summary>
    internal class ModifyPoint
    {
        /// <summary>
        /// 该点位经过Mark校正后的坐标值(机械坐标)
        /// </summary>
        public PointD PointWithCorrect { get; set; }

        /// <summary>
        /// 该点位从属的symbolLine
        /// </summary>

        public SymbolLine SymbolLine { get; set; }

        /// <summary>
        /// 该点位在其从属的symbolLine中的序号(0,1,2)
        /// </summary>
        public int PointNo { get; set; }

        /// <summary>
        /// 该点位从属的Symbollines
        /// </summary>
        public SymbolLinesCmd SymbolLinesCmd { get; set; }

        public ModifyPoint(SymbolLine symbolLine, int pointNo, SymbolLinesCmd symbolLinesCmd, CoordinateCorrector coordinateCorrector)
        {
            this.SymbolLine = symbolLine;
            this.PointNo = pointNo;
            this.SymbolLinesCmd = symbolLinesCmd;
            PointD point = new PointD(symbolLine.symbolPoints[pointNo].X, symbolLine.symbolPoints[pointNo].Y);
            this.PointWithCorrect = coordinateCorrector.Correct(symbolLinesCmd.RunnableModule, point, Executor.Instance.Program.ExecutantOriginOffset);
        }
    }
}
