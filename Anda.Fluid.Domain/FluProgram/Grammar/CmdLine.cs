using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using Anda.Fluid.Drive.Motion.ActiveItems;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public abstract class CmdLine : ICloneable
    {
        public ValveType Valve { get; set; }

        /// <summary>
        /// 所属CommandsModule
        /// </summary>
        public CommandsModule CommandsModule;

        private bool editable;
        /// <summary>
        /// 该命令行是否可以被用户编辑
        /// </summary>
        public bool Editable
        {
            get { return editable; }
        }

        public string TrackNumber { get; set; } = null;
        public PointD OrgOffset { get; set; }

        private bool enable = true;
        /// <summary>
        /// 命令行禁用开关
        /// </summary>
        public virtual bool Enabled
        {
            get { return enable; }
            set { enable = value; }
        }      

        public CmdLine(bool editable)
        {
            this.editable = editable;
            this.IdCode = this.GetHashCode();
        }

        /// <summary>
        /// 当前轨迹命令中的点集合以及对该点的描述
        /// </summary>
        public virtual List<Tuple<PointD, string>> PointsAndDescrie { get; }

        public TiltType Tilt { get; set; } = TiltType.NoTilt;

        /// <summary>
        /// 当前轨迹的名称
        /// </summary>
        public virtual CmdLineType CmdLineName { get; }

        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public abstract void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin);

        public abstract object Clone();

        public abstract new string ToString();

        public PointD MachineRel(PointD p)
        {
            if(this.CommandsModule is FluidProgram)
            {
                return (this.CommandsModule as FluidProgram).Workpiece.GetOriginPos();
            }
            Pattern pattern = this.CommandsModule as Pattern;
            return pattern.MachineRel(p);
        }

        public long IdCode { get; set; } 

    }

    public enum CmdLineType
    {
        点,
        直线,
        多段线,
        多线段,
        蛇形线,
        圆弧或圆环,
        复合线,
        执行拼板,
        拼板阵列,
        执行分组拼板
    }
}