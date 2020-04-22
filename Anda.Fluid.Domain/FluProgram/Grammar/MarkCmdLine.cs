using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class MarkCmdLine : CmdLine
    {
        private ModelFindPrm modelFindPrm = new ModelFindPrm();

        public ModelFindPrm ModelFindPrm => modelFindPrm;

        public bool IsFromFile = false;

        public PointD PosInPattern { get; private set; } = new PointD();


        [Obsolete]
        public PointD PosInMachine { get; private set; } = new PointD();

        public MarkCmdLine() : base(true)
        {

        }
        public MarkCmdLine(PointD posInPattern,bool isNoStandardMark = false) : base(true)
        {
            this.PosInPattern = posInPattern;
        }
        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {

            //根据拍照目标位置更新系统坐标
            //if (!this.modelFindPrm.IsUnStandard)
            //{
            //    VectorD v = modelFindPrm.TargetInMachine.ToSystem() - patternNewOrigin.ToSystem();
            //    PosInPattern.X = v.X;
            //    PosInPattern.Y = v.Y;
            //}
            //else
            //{
            //    PointD oldPosMachine = (PosInPattern + patternOldOrigin.ToSystem()).ToMachine();
            //    PointD newPosMachine = coordinateTransformer.Transform(oldPosMachine);
            //    VectorD v = newPosMachine.ToSystem() - patternNewOrigin.ToSystem();
            //    PosInPattern.X = v.X;
            //    PosInPattern.Y = v.Y;
            //}

            // 此时已经传入了坐标校正器和新的pattern原点，可以直接用旧的理论坐标转换为新的理论坐标
            // 语义层使用 TargetInMachine 有风险，可能为null，或是之前的目标值。
            PointD oldPosMachine = (PosInPattern + patternOldOrigin.ToSystem()).ToMachine();
            PointD newPosMachine = coordinateTransformer.Transform(oldPosMachine);
            VectorD v = newPosMachine.ToSystem() - patternNewOrigin.ToSystem();
            PosInPattern.X = v.X;
            PosInPattern.Y = v.Y;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            return sb.Append("MARK : ").Append(MachineRel(PosInPattern)).ToString();
        }

    }
}