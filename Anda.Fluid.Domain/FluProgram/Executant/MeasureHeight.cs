using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 测量高度命令
    /// </summary>
    [Serializable]
    public class MeasureHeight : Directive
    {
        private PointD position;
        /// <summary>
        /// 测量高度的位置
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 标准测高值
        /// </summary>
        public double StandardHt { get; set; } = 0;

        /// <summary>
        /// 上限
        /// </summary>
        public double ToleranceMax { get; set; } = 5;

        /// <summary>
        /// 下限
        /// </summary>
        public double ToleranceMin { get; set; } = -5;

        public double ZPos { get; set; } = -2;

        public double HeightMeasured { get; set; } = -1;

        public MeasureHeightCmd MeasureHeightCmd;

        public MeasureHeight(MeasureHeightCmd measureHeightCmd, CoordinateCorrector coordinateCorrector)
        {
            position = coordinateCorrector.Correct(measureHeightCmd.RunnableModule, measureHeightCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            Log.Dprint("MeasureHeight position : " + measureHeightCmd.Position + ", real : " + position);
            Program = measureHeightCmd.RunnableModule.CommandsModule.Program;
            this.RunnableModule = measureHeightCmd.RunnableModule;
            this.StandardHt = measureHeightCmd.StandardHt;
            this.ToleranceMax = measureHeightCmd.ToleranceMax;
            this.ToleranceMin = measureHeightCmd.ToleranceMin;
            this.ZPos = measureHeightCmd.ZPos;
            this.MeasureHeightCmd = measureHeightCmd;
        }

        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Log.Dprint("begin to execute MeasureHeight");
            // 以相机为中心的坐标转换成以激光测高为中心
            PointD pos = Position.ToLaser();
            Result ret;
            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }
            // 移动到指定位置
            Log.Dprint("move to position by laser : " + pos + ", postion by camera : " + Position);
            ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                this.Program.MotionSettings.VelXYHeight,
                this.Program.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.YBSX)
            {
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(ZPos, 
                    this.Program.MotionSettings.VelZ,
                    this.Program.MotionSettings.AccZ);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            Thread.Sleep(100);
            // 测量高度
            Log.Dprint("measure height ");
            double value = 0;
            ret = Machine.Instance.MeasureHeight(out value);
            if (!ret.IsOk)
            {
                return ret;
            }

            if (value > this.StandardHt + this.ToleranceMax || value < this.StandardHt + this.ToleranceMin)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Laser, AlarmInfoSensors.WarnMeasureHeight);
                return Result.FAILED;
            }
            this.MeasureHeightCmd.RealHtValue = value;
            //if(value > this.Program.RuntimeSettings.StandardBoardHeight + this.Program.RuntimeSettings.MaxTolerance
            //    || value < this.Program.RuntimeSettings.StandardBoardHeight + this.Program.RuntimeSettings.MinTolerance)
            //{
            //    AlarmServer.Instance.Fire(Machine.Instance.Laser, AlarmInfoSensors.WarnMeasureHeight);
            //    return Result.FAILED;
            //}
            Log.Dprint("measured value:" + value.ToString("0.000"));

            // 设置运行时基板高度参数
            //this.RunnableModule.MeasuredHt = value;
            //将高度传出，由对应轨迹获取对应测高点的测高值
            Result result = new Result(ret.IsOk, value);
            Log.Dprint(string.Format("Pattern-{0} Ht:{1}", this.RunnableModule.CommandsModule.Name, value));

            return result;
        }
    }
}
