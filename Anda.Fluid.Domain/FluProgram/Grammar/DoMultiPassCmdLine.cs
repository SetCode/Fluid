using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class DoMultiPassCmdLine : CmdLine
    {
        public string PatternName;
        [CompareAtt("CMP")]
        public PointD Origin;

        public DoMultiPassCmdLine() : this(null, 0, 0)
        {
        }

        public int BoardNo { get; set; } = 0;

        public override CmdLineType CmdLineName => CmdLineType.执行分组拼板;

        public override List<Tuple<PointD, string>> PointsAndDescrie
        {
            get
            {
                List<Tuple<PointD, string>> list = new List<Tuple<PointD, string>>();
                list.Add(new Tuple<PointD, string>(this.Origin, "原点"));
                return list;
            }
        }

        public DoMultiPassCmdLine(string patternName, double originX, double originY) : base(true)
        {
            PatternName = patternName;
            Origin = new PointD(originX, originY);
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
            // 校正后的机械坐标
            pOrigin = coordinateTransformer.Transform(pOrigin);
            // 相对系统坐标
            Origin.X = (pOrigin.ToSystem() - patternNewOrigin.ToSystem()).X;
            Origin.Y = (pOrigin.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public override object Clone()
        {
            DoMultiPassCmdLine doMultiPassCmdLine = MemberwiseClone() as DoMultiPassCmdLine;
            doMultiPassCmdLine.Origin = Origin.Clone() as PointD;
            return doMultiPassCmdLine;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && Machine.Instance.Setting.DualValveMode == DualValveMode.跟随)
            {
                sb.Append(this.Valve.ToString() + " ");
            }
            sb.Append("DO MULTIPASS : ").Append(PatternName).Append(" AT ").Append(MachineRel(Origin)).Append(" Board ").Append(this.BoardNo);
            return sb.ToString();
        }

       
    }
}