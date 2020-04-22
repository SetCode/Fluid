using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.MathTools;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    /// <summary>
    /// Description：银宝山特殊多线段语义层指令
    /// Author：liyi
    /// Date：2019/09/18
    /// </summary>
    [Serializable]
    public class SymbolLinesCmd : SupportDirectiveCmd
    {
        /// <summary>
        /// 当前指令关联的测高指令
        /// </summary>
        //public MeasureHeightCmd associatedMeasureHeightCmd = null;

        //public MeasureHeightCmd AssociatedMeasureHeightCmd
        //{
        //    get { return associatedMeasureHeightCmd; }
        //}

        private List<SymbolLine> symbols = new List<SymbolLine>();

        protected double arcSpeed = 30;

        public double ArcSpeed
        {
            get
            {
                return arcSpeed;
            }
            set
            {
                arcSpeed = value;
            }
        }
        public double OffsetX;
        public double OffsetY;
        public LineParam LineParam { get; private set; } = new LineParam();
        public List<SymbolLine> Symbls
        {
            get { return this.symbols; }
        }

        public SymbolLinesCmd(RunnableModule runnableModule,SymbolLinesCmdLine symbolLinesCmdLine/*, MeasureHeightCmd mhCmd*/) : base(runnableModule, symbolLinesCmdLine)
        {
            this.Valve = symbolLinesCmdLine.Valve;
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            this.symbols = new List<SymbolLine>();
            this.arcSpeed = symbolLinesCmdLine.ArcSpeed;
            foreach (SymbolLine symbolLine in symbolLinesCmdLine.Symbols)
            {
                //此处计算轨迹旋转角没用，轨迹执行时再计算每段轨迹的起始角度和结束角度
                SymbolLine newSymbolLine = symbolLine.Clone() as SymbolLine;
                for (int i = 0; i < newSymbolLine.symbolPoints.Count; i++)
                {
                    newSymbolLine.symbolPoints[i] = structure.ToMachine(runnableModule, newSymbolLine.symbolPoints[i]);
                }
                this.symbols.Add(newSymbolLine);
            }
            // 添加每一段需要的测高
            for (int i = 0; i < this.symbols.Count; i++)
            {
                ////轨迹首点测高
                //if (i == 0)
                //{
                //    MeasureHeightCmd mhCmd = new MeasureHeightCmd(runnableModule, symbolLinesCmdLine.BindMHCmdLine);
                //    mhCmd.Position.X = this.symbols[i].symbolPoints[0].X;
                //    mhCmd.Position.Y = this.symbols[i].symbolPoints[0].Y;
                //    this.symbols[i].MHCmdList.Add(mhCmd);
                //}
                //if (this.symbols[i].symbolType != SymbolType.Arc)
                //{
                //    for (int j = 0; j < this.symbols[i].MHCount; j++)
                //    {
                //        PointD newPosition = SymbolLineMathTools.GetScalePointOnLine(this.symbols[i].symbolPoints[0], this.symbols[i].symbolPoints[1], (double)(j+1) / this.symbols[i].MHCount);
                //        MeasureHeightCmd mhCmd = new MeasureHeightCmd(runnableModule, symbolLinesCmdLine.BindMHCmdLine);
                //        mhCmd.Position.X = newPosition.X;
                //        mhCmd.Position.Y = newPosition.Y;
                //        this.symbols[i].MHCmdList.Add(mhCmd);
                //    }
                //}


                //圆弧测高只测圆弧的中点
                if (this.symbols[i].symbolType == SymbolType.Arc)
                {
                    MeasureHeightCmd mhCmd = new MeasureHeightCmd(runnableModule, symbolLinesCmdLine.BindMHCmdLine);
                    mhCmd.Position.X = this.symbols[i].symbolPoints[1].X;
                    mhCmd.Position.Y = this.symbols[i].symbolPoints[1].Y;
                    this.symbols[i].MHCmdList.Add(mhCmd);
                }
                else
                {
                    for (int j = 0; j < this.symbols[i].MHCount; j++)
                    {
                        PointD newPosition = SymbolLineMathTools.GetScalePointOnLine(this.symbols[i].symbolPoints[0], this.symbols[i].symbolPoints[1], (double)(j + 1) / (this.symbols[i].MHCount + 1));
                        MeasureHeightCmd mhCmd = new MeasureHeightCmd(runnableModule, symbolLinesCmdLine.BindMHCmdLine);
                        mhCmd.Position.X = newPosition.X;
                        mhCmd.Position.Y = newPosition.Y;
                        this.symbols[i].MHCmdList.Add(mhCmd);
                    }
                }
            }

            this.LineParam = this.RunnableModule.CommandsModule.Program.ProgramSettings.GetLineParam(symbolLinesCmdLine.LineStyle);

            this.OffsetX = symbolLinesCmdLine.OffsetX;
            this.OffsetY = symbolLinesCmdLine.OffsetY;
        }


        public List<MeasureHeightCmd> GetAllMeasureCmdLineList()
        {
            List<MeasureHeightCmd> result = new List<MeasureHeightCmd>();
            foreach (SymbolLine item in this.symbols)
            {
                result.AddRange(item.MHCmdList);
            }
            return result;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new SymbolLines(this, coordinateCorrector);
        }
    }
}
