using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    /// <summary>
    /// 对指定普通Pattern执行阵列操作
    /// </summary>
    [Serializable]
    public class StepAndRepeatCmdLine : CmdLine
    {
        public enum RepeatMode
        {
            X向蛇行,
            X向之行,
            Y向蛇行,
            Y向之行,
        }
        [CompareAtt("CMP")]
        public string PatternName;
        /// <summary>
        /// 阵列起始点
        /// </summary>
        [CompareAtt("CMP")]
        public PointD Origin;
        /// <summary>
        /// 阵列横向终点
        /// </summary>
        [CompareAtt("CMP")]
        public PointD HorizontalEnd;
        /// <summary>
        /// 阵列纵向终点
        /// </summary>
        [CompareAtt("CMP")]
        public PointD VerticalEnd;

        public override CmdLineType CmdLineName => CmdLineType.拼板阵列;

        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
                list.Add(new Tuple<PointD, string>(this.Origin, "起点"));
                list.Add(new Tuple<PointD, string>(this.HorizontalEnd, "X方向终点"));
                list.Add(new Tuple<PointD, string>(this.VerticalEnd, "Y方向终点"));
                return list;
            }
        }

        /// <summary>
        /// 阵列横向个数
        /// </summary>
        [CompareAtt("CMP")]
        public int HorizontalNums;
        /// <summary>
        /// 阵列纵向个数
        /// </summary>
        [CompareAtt("CMP")]
        public int VerticalNums;
        /// <summary>
        /// 阵列模式
        /// </summary>
        [CompareAtt("CMP")]
        public RepeatMode Mode;
        /// <summary>
        /// 阵列对应的所有DO命令集合
        /// </summary>
        public List<DoCmdLine> DoCmdLineList;

        public StepAndRepeatCmdLine() : this(null, 0, 0, 0, 0, 0, 0, 1, 1, null)
        {
        }

        public StepAndRepeatCmdLine(string patternName, double originX, double originY,
            double horizontalEndX, double horizontalEndY,
            double verticalEndX, double verticalEndY, 
            int horizontalNums, int verticalNums,
            List<DoCmdLine> doCmdLineList)
            : base(true)
        {
            PatternName = patternName;
            Origin = new PointD(originX, originY);
            HorizontalEnd = new PointD(horizontalEndX, horizontalEndY);
            VerticalEnd = new PointD(verticalEndX, verticalEndY);
            HorizontalNums = horizontalNums;
            VerticalNums = verticalNums;
            if (doCmdLineList == null)
            {
                DoCmdLineList = new List<DoCmdLine>();
            }
            else
            {
                DoCmdLineList = new List<DoCmdLine>(doCmdLineList);
            }
        }

        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // 校正前的机械坐标
            PointD pOrigin = (patternOldOrigin.ToSystem() + Origin).ToMachine();
            PointD pHend = (patternOldOrigin.ToSystem() + HorizontalEnd).ToMachine();
            PointD pVend = (patternOldOrigin.ToSystem() + VerticalEnd).ToMachine();
            // 校正后的机械坐标
            pOrigin = coordinateTransformer.Transform(pOrigin);
            pHend = coordinateTransformer.Transform(pHend);
            pVend = coordinateTransformer.Transform(pVend);
            // 相对系统坐标
            Origin.X = (pOrigin.ToSystem() - patternNewOrigin.ToSystem()).X;
            Origin.Y = (pOrigin.ToSystem() - patternNewOrigin.ToSystem()).Y;
            HorizontalEnd.X = (pHend.ToSystem() - patternNewOrigin.ToSystem()).X;
            HorizontalEnd.Y = (pHend.ToSystem() - patternNewOrigin.ToSystem()).Y;
            VerticalEnd.X = (pVend.ToSystem() - patternNewOrigin.ToSystem()).X;
            VerticalEnd.Y = (pVend.ToSystem() - patternNewOrigin.ToSystem()).Y;

            foreach (DoCmdLine doCmdLine in DoCmdLineList)
            {
                PointD p = (patternOldOrigin.ToSystem() + doCmdLine.Origin).ToMachine();
                p = coordinateTransformer.Transform(p);
                doCmdLine.Origin.X = (p.ToSystem() - patternNewOrigin.ToSystem()).X;
                doCmdLine.Origin.Y = (p.ToSystem() - patternNewOrigin.ToSystem()).Y;
            }
        }

        public override object Clone()
        {
            StepAndRepeatCmdLine stepAndRepeatCmdLine = MemberwiseClone() as StepAndRepeatCmdLine;
            stepAndRepeatCmdLine.Origin = Origin.Clone() as PointD;
            stepAndRepeatCmdLine.HorizontalEnd = HorizontalEnd.Clone() as PointD;
            stepAndRepeatCmdLine.VerticalEnd = VerticalEnd.Clone() as PointD;
            stepAndRepeatCmdLine.DoCmdLineList = new List<DoCmdLine>();
            foreach (DoCmdLine doCmdLine in DoCmdLineList)
            {
                stepAndRepeatCmdLine.DoCmdLineList.Add(doCmdLine.Clone() as DoCmdLine);
            }
            return stepAndRepeatCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("STEP AND REPEAT : ")
                .Append(PatternName).Append(" AT ").Append(MachineRel(Origin))
                .Append(", ").Append(HorizontalNums).Append(", ").Append(VerticalNums);
            return sb.ToString();
        }

       
    }
}
