using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class MoveXyCmd : SupportDirectiveCmd
    {
        //private PointD offset;
        ///// <summary>
        ///// 移动的目标位置
        ///// </summary>
        //public PointD Offset
        //{
        //    get { return offset; }
        //}

        private PointD position;
        /// <summary>
        /// 点坐标，相对于当前所在Pattern的原点
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 是否返回安全高度
        /// </summary>
        public bool ToSafeZ { get; set; }

        public MoveXyCmd(RunnableModule runnableModule, MoveXyCmdLine moveXyCmdLine) : base(runnableModule, moveXyCmdLine)
        {
            //offset = new PointD(moveXyCmdLine.Offset.X, moveXyCmdLine.Offset.Y);
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            position = structure.ToMachine(runnableModule, moveXyCmdLine.Position);
            this.ToSafeZ = moveXyCmdLine.ToSafeZ;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new MoveXy(this, coordinateCorrector);
        }
    }
}