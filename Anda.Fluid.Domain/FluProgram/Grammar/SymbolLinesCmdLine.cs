using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    /// <summary>
    /// Description：银宝山特殊多段轨迹类型，轨迹衔接拐点改倒角处理
    /// Author：liyi
    /// Date：2019/09/18
    /// </summary>
    [Serializable]
    public class SymbolLinesCmdLine : CmdLine
    {
        protected List<SymbolLine> symbols = new List<SymbolLine>();

        protected MeasureHeightCmdLine bindMHCmdLine = null;

        /// <summary>
        /// 多段轨迹数组
        /// </summary>
        public List<SymbolLine> Symbols
        {
            get { return symbols; }
            set { symbols = value; }
        }

        public MeasureHeightCmdLine BindMHCmdLine
        {
            get { return bindMHCmdLine; }
            set { bindMHCmdLine = value; }
        }

        /// <summary>
        /// 轨迹圆弧基准速度(半径为1)
        /// </summary>
        protected double arcSpeed = 30;

        public double ArcSpeed { get { return arcSpeed; } set{arcSpeed = value;} }

        public override CmdLineType CmdLineName => CmdLineType.复合线;

        public double OffsetX;
        public double OffsetY;

        //获取该指令中的所有点坐标，用来实现快捷微调功能（Edit By 肖旭）
        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
                foreach (var item in this.symbols)
                {
                    if (item.symbolType == SymbolType.Line)
                    {
                        list.Add(new Tuple<PointD, string>(item.symbolPoints[0], "直线点"));
                        list.Add(new Tuple<PointD, string>(item.symbolPoints[1], "直线点"));
                    }
                    else if (item.symbolType == SymbolType.Arc)
                    {
                        list.Add(new Tuple<PointD, string>(item.symbolPoints[0], "圆弧点"));
                        list.Add(new Tuple<PointD, string>(item.symbolPoints[1], "圆弧点"));
                        list.Add(new Tuple<PointD, string>(item.symbolPoints[2], "圆弧点"));
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 所使用的线参数类型
        /// </summary>
        [CompareAtt("CMP")]
        public LineStyle LineStyle = LineStyle.TYPE_1;

        public SymbolLinesCmdLine() : base(true)
        {

        }

        public SymbolLinesCmdLine(List<SymbolLine> symbols,MeasureHeightCmdLine mhCmdLine) : this()
        {
            this.symbols = symbols;
            this.bindMHCmdLine = mhCmdLine;
        }
        public override object Clone()
        {
            SymbolLinesCmdLine symbolLinesCmdLine = this.MemberwiseClone() as SymbolLinesCmdLine;
            symbolLinesCmdLine.symbols = new List<SymbolLine>();
            foreach (SymbolLine item in this.symbols)
            {
                symbolLinesCmdLine.symbols.Add(item.Clone() as SymbolLine);
            }
            symbolLinesCmdLine.bindMHCmdLine = this.bindMHCmdLine.Clone() as MeasureHeightCmdLine;
            symbolLinesCmdLine.IdCode = symbolLinesCmdLine.GetHashCode();
            return symbolLinesCmdLine;
        }

        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            foreach (SymbolLine symbolLine in this.symbols)
            {
                symbolLine.Correct(patternOldOrigin, coordinateTransformer, patternNewOrigin);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("SymbolLines : ");
            sb.Append(this.symbols[0].type.ToString());

            if (this.TrackNumber != null)
            {
                sb.Append(this.TrackNumber);
            }
            return sb.ToString();
        }

    }
}
