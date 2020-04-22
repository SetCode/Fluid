using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// X、Y方向上移动一段相对距离
    /// </summary>
    [Serializable]
    public class MoveXy : Directive
    {
        //private PointD offset;
        ///// <summary>
        ///// X、Y方向上相对距离
        ///// </summary>
        //public PointD Offset
        //{
        //    get { return offset; }
        //}        
        private PointD position;
        /// <summary>
        /// 机械坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 是否返回安全高度
        /// </summary>
        private bool toSafeZ;

        public MoveXy(MoveXyCmd moveXyCmd, CoordinateCorrector coordinateCorrector)
        {            
            Program = moveXyCmd.RunnableModule.CommandsModule.Program;
            this.RunnableModule = moveXyCmd.RunnableModule;
            if (this.RunnableModule.Mode == ModuleMode.AssignMode1 || this.RunnableModule.Mode == ModuleMode.MainMode || this.RunnableModule.Mode == ModuleMode.DualFallow)
            {
                this.Valve = ValveType.Valve1;
            }
            else
            {
                this.Valve = ValveType.Valve2;
            }
            position = coordinateCorrector.Correct(moveXyCmd.RunnableModule, moveXyCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);

            this.toSafeZ = moveXyCmd.ToSafeZ;                     
        }

        public override Result Execute()
        {
            //Log.Dprint("begin to execute MoveOffsetXy, offset=" + offset);            
            Result ret = Result.OK;
            switch (Machine.Instance.Valve1.RunMode)
            {
                case ValveRunMode.Wet:
                    this.executeWetAndDry();
                    break;
                case ValveRunMode.Dry:
                    this.executeWetAndDry();
                    break;
                case ValveRunMode.Look:
                    this.executeLook();
                    break;                
                default:
                    break;
            }
            return ret;
        }

        private Result executeWetAndDry()
        {
            Result ret = Result.OK;

            PointD pos = position.ToNeedle(this.Valve); 

            PointD simulPos = GetSimulPos(pos, this);/*-胶阀原点间距？*/

            double currZ = Machine.Instance.Robot.PosZ;           

            if (currZ > Machine.Instance.Robot.CalibPrm.SafeZ)
            {
                // 移动到指定位置
                //Log.Dprint("move to position XY : " + pos);
                Logger.DEFAULT.Info("move to position XY : " + pos);
                if (this.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    //ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos, (int)Machine.Instance.Setting.CardSelect);
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos,
                     FluidProgram.Current.MotionSettings.VelXYAB,
                     FluidProgram.Current.MotionSettings.AccXYAB,
                     (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    //ret = Machine.Instance.Robot.MovePosXYAndReply(pos);
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                      FluidProgram.Current.MotionSettings.VelXY,
                      FluidProgram.Current.MotionSettings.AccXY);
                }
                if (!ret.IsOk)
                {
                    return ret;
                }         
             
            }
            else
            {
                if (this.toSafeZ)
                {
                    Machine.Instance.Robot.MoveSafeZAndReply();
                }
                if (this.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    //ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos, (int)Machine.Instance.Setting.CardSelect);
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos,
                     FluidProgram.Current.MotionSettings.VelXYAB,
                     FluidProgram.Current.MotionSettings.AccXYAB,
                     (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    //ret = Machine.Instance.Robot.MovePosXYAndReply(pos);
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                      FluidProgram.Current.MotionSettings.VelXY,
                      FluidProgram.Current.MotionSettings.AccXY);
                }
                if (!ret.IsOk)
                {
                    return ret;
                }
                
            }
            return null;
        }

        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        protected PointD GetSimulPos(PointD pos, MoveXy moveXy)
        {
            PointD simulPos = new PointD();

            ///生成副阀相关参数(起点、插补点位)
            if (moveXy.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - moveXy.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -simulPos.Y / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(moveXy.Program.RuntimeSettings.SimulDistence, 0);
            }
            //副阀点胶起点位置(默认值为设定间距)
            PointD simulOffset = new PointD(moveXy.Program.RuntimeSettings.SimulOffsetX, moveXy.Program.RuntimeSettings.SimulOffsetY);
            return simulPos + simulOffset;
        }

        private Result executeLook()
        {           
            Logger.DEFAULT.Info("begin to execute MoveXy-Look");
            Result ret = Result.OK;
            //抬起到安全高度
            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }

            // 移动到指定位置
            Log.Dprint("move to position XY : " + this.Position);
            ret = Machine.Instance.Robot.MovePosXYAndReply(this.Position);
            if (!ret.IsOk)
            {
                return ret;
            }         

            return ret;
        }
    }
}