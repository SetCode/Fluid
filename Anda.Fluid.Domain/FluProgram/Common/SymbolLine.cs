using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Common
{
    /// <summary>
    /// Description：特殊多段轨迹中的每一段的属性
    /// Author：liyi
    /// Date：2019/09/18
    /// </summary>
    [Serializable]
    public class SymbolLine : ICloneable
    {
        /// <summary>
        /// 该段轨迹类型
        /// </summary>
        public SymbolType symbolType = SymbolType.Line;
        /// <summary>
        /// 该段轨迹的点位
        /// Line: 0-start,1-end  
        /// ||
        /// Arc : 0-start,1-center,2-end
        /// </summary>
        public List<PointD> symbolPoints = new List<PointD>();
        /// <summary>
        /// 轨迹参数类型
        /// </summary>
        public LineStyle type = LineStyle.TYPE_1;
        /// <summary>
        /// 轨迹针嘴起始角度（直线起始和结束角度相同）
        /// 该值需要在Mark定位之后重新计算
        /// </summary>
        public double StartAngle { get; set; } = 0.0;
        /// <summary>
        /// 轨迹针嘴结束角度（直线起始和结束角度相同）
        /// 该值需要在Mark定位之后重新计算
        /// </summary>
        public double EndAngle { get; set; } = 0.0;

        /// <summary>
        /// 轨迹针嘴起始角度（直线起始和结束角度相同）
        /// </summary>
        public double TrackStartAngle { get; set; } = 0.0;
        /// <summary>
        /// 轨迹针嘴结束角度（直线起始和结束角度相同）
        /// </summary>
        public double TrackEndAngle { get; set; } = 0.0;

        /// <summary>
        /// 轨迹弧度
        /// </summary>
        public double TrackSweep { get; set; } = 0.0;

        /// <summary>
        /// 当前段结束高度
        /// </summary>
        public double EndZ { get; set; } = 0.0;
        /// <summary>
        /// 轨迹是否顺时针旋转（仅限圆弧轨迹）
        /// 0：顺时针
        /// 1：逆时针
        /// </summary>
        public int clockwise { get; set; } = 0;

        /// <summary>
        /// 过渡的圆弧半径
        /// </summary>
        public double transitionR { get; set; } = 2;
        /// <summary>
        /// 轨迹测高
        /// </summary>
        public List<MeasureHeightCmd> MHCmdList = new List<MeasureHeightCmd>();
        [NonSerialized]
        public List<MeasureHeight> MHList = new List<MeasureHeight>();
     
        /// <summary>
        /// 当前段测高数量，默认结尾测一段
        /// </summary>
        public int MHCount { get; set; } = 1;

        /// <summary>
        /// 每个点对应的手动示教补偿值，没有手动示教时，是new PointD(0,0)
        /// </summary>
        public List<PointD> symbolOffset { get; set; } = new List<PointD>();

        /// <summary>
        /// 每个点是否需要手动示教补偿
        /// </summary>
        public List<bool> needOffset { get; set; } = new List<bool>();

        public object Clone()
        {
            SymbolLine symbolLine = this.MemberwiseClone() as SymbolLine;
            symbolLine.symbolPoints = new List<PointD>();
            symbolLine.MHCmdList = new List<MeasureHeightCmd>();
            foreach (PointD item in this.symbolPoints)
            {
                symbolLine.symbolPoints.Add(item.Clone() as PointD);
            }


            foreach (MeasureHeightCmd item in MHCmdList)
            {
                symbolLine.MHCmdList.Add(item.Clone() as MeasureHeightCmd);
            }         
            return symbolLine;
        }
        /// <summary>
        /// 编程界面校准
        /// </summary>
        /// <param name="patternOldOrigin"></param>
        /// <param name="coordinateTransformer"></param>
        /// <param name="patternNewOrigin"></param>
        public void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            foreach (PointD item in this.symbolPoints)
            {
                // 校正前的机械坐标
                PointD newPoint = (patternOldOrigin.ToSystem() + item).ToMachine();
                // 校正后的机械坐标
                newPoint = coordinateTransformer.Transform(newPoint);
                // 相对系统坐标
                item.X = (newPoint.ToSystem() - patternNewOrigin.ToSystem()).X;
                item.Y = (newPoint.ToSystem() - patternNewOrigin.ToSystem()).Y;
            }                                   
           
        }
        /// <summary>
        /// 运行时的校准
        /// </summary>
        /// <param name="runnableModule"></param>
        /// <param name="coordinateCorrector"></param>
        public void Correct(RunnableModule runnableModule, CoordinateCorrector coordinateCorrector)
        {
            foreach (PointD item in this.symbolPoints)
            {
                // 校正后的机械坐标(运行时，每个symbolPoint已经是实际的相机机械坐标了)
                PointD newPoint = coordinateCorrector.Correct(runnableModule, item, Executor.Instance.Program.ExecutantOriginOffset);
                // 相对系统坐标
                item.X = newPoint.X;
                item.Y = newPoint.Y;
            }
        }

        /// <summary>
        /// 为每个点添加补偿
        /// </summary>
        public void AddOffset()
        {
            for (int i = 0; i < this.symbolPoints.Count; i++)
            {
                PointD offset = new PointD();
                if (this.symbolOffset.Count == 0) 
                {
                    offset = new PointD(0, 0);
                }
                else
                {
                    offset = this.symbolOffset[i];
                }
                this.symbolPoints[i].X += offset.X;
                this.symbolPoints[i].Y += offset.Y;
            }
        }
    }
}

