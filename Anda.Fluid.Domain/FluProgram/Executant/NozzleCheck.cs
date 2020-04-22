using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Common;
using System.Threading;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Alarming;
using System.Windows.Forms;
using Anda.Fluid.Drive.Vision.GrayFind;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.LightSystem;

namespace Anda.Fluid.Domain.FluProgram.Executant
{

    ///<summary>
    /// Description	:喷嘴检测执行指令
    /// Author  	:liyi
    /// Date		:2019/07/17
    ///</summary>
    [Serializable]
    public class NozzleCheck : Directive,IAlarmSenderable
    {
        private NozzleCheckStyle nozzleStyle = NozzleCheckStyle.Valve1;

        /// <summary>
        /// 用于判定是全局检测还是每个拼版检测
        /// </summary>
        public bool isGlobal { get; set; } = true;

        /// <summary>
        /// 胶阀检测类型（valve1、valve2、both）
        /// </summary>
        public NozzleCheckStyle NozzleStyle { get { return nozzleStyle; } }
        /// <summary>
        /// 模板匹配参数
        /// </summary>
        public ModelFindPrm ModelFindPrm { get; private set; }

        public GrayCheckPrm GrayCheckPrm { get; private set; }

        public CheckThm CheckThm { get; private set; }

        public bool IsOkAlarm { get; private set; }

        private PointD position;
        /// <summary>
        /// 机械坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        private DotParam param;
        /// <summary>
        /// 点参数
        /// </summary>
        public DotParam Param
        {
            get { return param; }
        }

        private bool isWeightControl = false;
        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        public bool IsWeightControl
        {
            get { return isWeightControl; }
        }

        private double weight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定重量值，单位：mg
        /// </summary>
        public double Weight
        {
            get
            {
                return weight;
            }
        }
        [NonSerialized]
        private double curMeasureHeightValue;

        public NozzleCheckCmd NozzleCheckCmd { get; protected set; }

        public object Obj => this;

        public string Name => this.GetType().Name;

        public NozzleCheck(NozzleCheckCmd nozzleCheckCmd, CoordinateCorrector coordinateCorrector)
        {
            this.NozzleCheckCmd = nozzleCheckCmd;
            Log.Dprint("nozzle check dot position : " + nozzleCheckCmd.Position + ", real : " + position);
            this.position = coordinateCorrector.Correct(nozzleCheckCmd.RunnableModule,
                nozzleCheckCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            this.ModelFindPrm = nozzleCheckCmd.ModelFindPrm;
            this.GrayCheckPrm = nozzleCheckCmd.GrayCheckPrm;
            this.CheckThm = nozzleCheckCmd.CheckThm;
            this.IsOkAlarm = nozzleCheckCmd.IsOkAlarm;
            this.param = nozzleCheckCmd.RunnableModule.CommandsModule.Program.ProgramSettings.GetDotParam(nozzleCheckCmd.DotStyle);
            this.isWeightControl = nozzleCheckCmd.IsWeightControl;
            this.weight = nozzleCheckCmd.Weight;
            this.isGlobal = nozzleCheckCmd.isGlobal;
            this.nozzleStyle = nozzleCheckCmd.NozzleStyle;
            this.RunnableModule = nozzleCheckCmd.RunnableModule;
            Program = nozzleCheckCmd.RunnableModule.CommandsModule.Program;

            if (nozzleCheckCmd.AssociatedMeasureHeightCmd != null)
            {
                curMeasureHeightValue = nozzleCheckCmd.AssociatedMeasureHeightCmd.RealHtValue;
            }
            else
            {
                curMeasureHeightValue = this.RunnableModule.MeasuredHt;
            }
        }
        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Log.Dprint("begin to execute nozzle check Dot");
            Result ret = Result.OK;
            PointD pos;
            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectDot)
            {
                pos = new PointD(Position);
            }
            else
            {
                // 以相机为中心的坐标转换成以喷嘴为中心
                pos = Position.ToNeedle(this.Valve);
            }

            //副阀点胶起点位置(默认值为设定间距)
            PointD simulPos = GetSimulPos(pos)/*-胶阀原点间距？*/;

            double currZ = Machine.Instance.Robot.PosZ;
            double targZ = 0;

            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = this.Program.RuntimeSettings.BoardZValue + param.DispenseGap;
            }
            else
            {
                targZ = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
            }

            // CV模式 | 检测模式下的运行逻辑
            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectDot)
            {
                ret = this.CvAndInspectLogic(pos);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            // Wet模式||Dry模式下的运动逻辑(不包含打胶)
            else
            {
                ret = this.WetAndDryMovelogic(pos, simulPos, currZ, targZ);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            // 等待一段时间 Settling Time
            Log.Dprint("wait Settling Time(s) : " + param.SettlingTime);
            Thread.Sleep(TimeSpan.FromSeconds(param.SettlingTime));

            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Dry)
            {

                if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
                {
                    ret = this.JtValveDispenseLogic();
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
                else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀
                    || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
                {
                    ret = this.SvValveDispenseLogic();
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }

                // 抬高一段距离 Retract Distance
                if (param.RetractDistance > 0)
                {
                    Log.Dprint("move up RetractDistance : " + param.RetractDistance);
                    ret = Machine.Instance.Robot.MoveIncZAndReply(param.RetractDistance, param.RetractSpeed, param.RetractAccel);
                }

                if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀
                    || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
                {
                    if (this.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        ret = Machine.Instance.DualValve.SuckBack(param.SuckBackTime);
                    }
                    else
                    {
                        ret = Machine.Instance.Valve1.SuckBack(param.SuckBackTime);
                    }
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 视觉检测逻辑
        /// </summary>
        /// <returns></returns>
        public Result CheckExecute()
        {
            Result ret = Result.OK;
            //点胶结束后的检测逻辑
            //先移动到安全高度
            PointD pos;
            PointD simulPos;
            pos = Position;//主阀点胶点位的相机坐标
            //simulPos = GetSimulPos(pos);//副阀点胶点位的相机坐标
            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }
            if (this.RunnableModule.Mode == ModuleMode.MainMode)
            {
                simulPos = this.RunnableModule.SimulTransformer.Transform(pos);
                ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                    this.Program.MotionSettings.VelXY,
                    this.Program.MotionSettings.AccXY);
                if (!ret.IsOk)
                {
                    return ret;
                }
                // 检测主阀
                Log.Dprint("capture nozzle check Point");
                // 拍照检测
                ret = doCheck();
                Log.Dprint("nozzle check result : " + (ret.IsOk ? "OK" : "NG"));
                if (!ret.IsOk)
                {
                    Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
                    dic.Add(DialogResult.Abort, new Action(() => { }));
                    dic.Add(DialogResult.Ignore, new Action(() => { }));
                    DialogResult? result = AlarmServer.Instance.Fire(this, AlarmInfoDomain.NozzleCheckError, dic);
                    if (result == DialogResult.Abort)
                    {
                        return ret;
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        //return Result.OK;
                    }
                }
                //相机移动到副阀点胶点位
                ret = Machine.Instance.Robot.MovePosXYAndReply(simulPos,
                    this.Program.MotionSettings.VelXY,
                    this.Program.MotionSettings.AccXY);
                if (!ret.IsOk)
                {
                    return ret;
                }

                // todo 检测副阀
                Log.Dprint("capture nozzle check Point");
                // 拍照检测
                ret = doCheck();
                Log.Dprint("nozzle check result : " + (ret.IsOk ? "OK" : "NG"));
                if (!ret.IsOk)
                {
                    Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
                    dic.Add(DialogResult.Abort, new Action(() => { }));
                    dic.Add(DialogResult.Ignore, new Action(() => { }));
                    DialogResult? result = AlarmServer.Instance.Fire(this, AlarmInfoDomain.NozzleCheckError, dic);
                    if (result == DialogResult.Abort)
                    {
                        return ret;
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        return Result.OK;
                    }
                }
            }
            else
            {
                ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                    this.Program.MotionSettings.VelXY,
                    this.Program.MotionSettings.AccXY);
                if (!ret.IsOk)
                {
                    return ret;
                }
                // todo 检测主阀
                Log.Dprint("capture nozzle check Point");
                // 拍照检测
                ret = doCheck();
                Log.Dprint("nozzle check result : " + (ret.IsOk ? "OK" : "NG"));
                if (!ret.IsOk)
                {
                    Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
                    dic.Add(DialogResult.Abort, new Action(() => { }));
                    dic.Add(DialogResult.Ignore, new Action(() => { }));
                    DialogResult? result = AlarmServer.Instance.Fire(this, AlarmInfoDomain.NozzleCheckError, dic);
                    if (result == DialogResult.Abort)
                    {
                        return ret;
                    }
                    else if( result == DialogResult.Ignore)
                    {
                        return Result.OK;
                    }
                }
            }
            return ret;
        }

        private Result doCheck()
        {
            Log.Dprint("capture Bad mark");
            VisionFindPrmBase visionFindPrm = null;
            switch (this.CheckThm)
            {
                case CheckThm.GrayScale:
                    visionFindPrm = this.GrayCheckPrm;
                    break;
                case CheckThm.ModelFind:
                    visionFindPrm = this.ModelFindPrm;
                    break;
            }
            //设置拍照参数
            visionFindPrm.PosInMachine = new PointD(Position.X, Position.Y);
            //Machine.Instance.Light.SetLight(visionFindPrm.LightType);
            Machine.Instance.Light.SetLight(visionFindPrm.ExecutePrm);

            Machine.Instance.Camera.SetExposure(visionFindPrm.ExposureTime);
            Machine.Instance.Camera.SetGain(visionFindPrm.Gain);
            //采集图像
            Thread.Sleep(visionFindPrm.SettlingTime);
            byte[] bytes = Machine.Instance.Camera.TriggerAndGetBytes(TimeSpan.FromSeconds(1)).DeepClone();
            if (bytes == null)
            {
                return Result.FAILED;
            }

            //执行检测算法
            Result result = Result.OK;
            //移动到拍照高度
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                Machine.Instance.Robot.MoveToMarkZAndReply();
            }
            if (this.CheckThm == CheckThm.GrayScale)
            {
                GrayCheckPrm.CheckData = GrayCheckPrm.GetROI(bytes, Machine.Instance.Camera.Executor.ImageWidth, Machine.Instance.Camera.Executor.ImageHeight);
                GrayCheckPrm.CheckWidth = GrayCheckPrm.ModelWidth;
                GrayCheckPrm.CheckHeight = GrayCheckPrm.ModelHeight;
                if (!GrayCheckPrm.Execute())
                {
                    result = Result.FAILED;
                }
            }
            else if (this.CheckThm == CheckThm.ModelFind)
            {
                ModelFindPrm.ImgData = bytes;
                ModelFindPrm.ImgWidth = Machine.Instance.Camera.Executor.ImageWidth;
                ModelFindPrm.ImgHeight = Machine.Instance.Camera.Executor.ImageHeight;
                if (!ModelFindPrm.Execute())
                {
                    result = Result.FAILED;
                }

            }
            Log.Dprint(string.Format("bad mark result : {0}", result.IsOk));

            //如果OK跳过则结果取反
            if (this.IsOkAlarm)
            {
                result = result.IsOk ? Result.FAILED : Result.OK;
            }
            return result;
        }

        private Result SvValveDispenseLogic()
        {
            Result ret = Result.OK;
            // 计算螺杆阀喷胶时间:
            // 1. 如果是重量线， time = 总重量(mg) / 当前速度下的胶水重量(mg/s)
            // 2. 如果是普通线  time = Num. Shots * Valve On Time

            double valveWeight;
            FluidProgram.Current.RuntimeSettings.VavelSpeedDic.TryGetValue(this.Program.RuntimeSettings.SvOrGearValveCurrSpeed, out valveWeight);
            //Program.VavelSpeedDic.TryGetValue(this.Program.SvValveCurrSpeed, out valveWeight);
            Log.Dprint("isWeightControl=" + isWeightControl + ", weight=" + weight + ", valveWeight=" + valveWeight);

            double time = isWeightControl ? (weight / valveWeight) : param.NumShots * param.ValveOnTime;
            Log.Dprint("time： " + time);

            //如果打胶过程中需要抬高
            if (param.MultiShotDelta > 0)
            {
                //抬高次数
                int UpNumber = param.NumShots - 1;
                double UpInterval = time / param.NumShots;

                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                {
                    if (this.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.Spraying();
                    }
                    else
                    {
                        Machine.Instance.Valve1.Spraying();
                    }

                    for (int i = 0; i < UpNumber; i++)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(UpInterval));
                        Machine.Instance.Robot.MoveIncZAndReply(param.MultiShotDelta, param.RetractSpeed, param.RetractAccel);
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(time - UpNumber * UpInterval));

                    if (this.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.SprayOff();
                    }
                    else
                    {
                        Machine.Instance.Valve1.SprayOff();
                    }
                }

                // 等待一段时间 Dwell Time
                Log.Dprint("DwellTime(s) : " + param.DwellTime);
                Thread.Sleep(TimeSpan.FromSeconds(param.DwellTime));
            }
            //打胶过程中不需要抬高
            else
            {
                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                {
                    if (this.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.Spraying();
                    }
                    else
                    {
                        Machine.Instance.Valve1.Spraying();
                    }
                    //一直打time时长的胶水
                    Thread.Sleep(TimeSpan.FromSeconds(time));
                    if (this.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.SprayOff();
                    }
                    else
                    {
                        Machine.Instance.Valve1.SprayOff();
                    }
                }

                // 等待一段时间 Dwell Time
                Log.Dprint("DwellTime(s) : " + param.DwellTime);
                Thread.Sleep(TimeSpan.FromSeconds(param.DwellTime));
            }

            return ret;
        }

        /// <summary>
        /// 喷射阀打胶逻辑
        /// </summary>
        /// <returns></returns>
        private Result JtValveDispenseLogic()
        {
            Result ret = Result.OK;
            // 计算喷胶次数:
            // 1. 如果是重量线， shots = 总重量 / 单滴胶水的重量
            // 2. 如果是普通线 shots = Num. Shots
            Log.Dprint("isWeightControl=" + isWeightControl + ", weight=" + weight + ", singleDropWeight=" + Program.RuntimeSettings.SingleDropWeight);
            int shots = isWeightControl ? (int)((decimal)weight / (decimal)Program.RuntimeSettings.SingleDropWeight) : param.NumShots;
            Log.Dprint("shots ： " + shots);

            if (param.MultiShotDelta > 0)
            {
                // 开始喷胶
                for (int i = 0; i < shots; i++)
                {
                    if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                    {
                        // 喷射一滴胶水
                        Log.Dprint("spray : " + (i + 1));
                        //Machine.Instance.Valve1.SprayOne();
                        if (this.RunnableModule.Mode == ModuleMode.MainMode)
                        {
                            Machine.Instance.DualValve.SprayOneAndWait();
                        }
                        else
                        {
                            Machine.Instance.Valve1.SprayOneAndWait();
                        }
                    }
                    DateTime sprayEnd = DateTime.Now;

                    // 非最后一滴胶水，抬高一段距离 Multi-shot Delta
                    if (param.MultiShotDelta > 0 && i != shots - 1)
                    {
                        Log.Dprint("move up Multi-shot Delta : " + param.MultiShotDelta);
                        ret = Machine.Instance.Robot.MoveIncZAndReply(param.MultiShotDelta, param.RetractSpeed, param.RetractAccel);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }

                    // 等待一段时间 Dwell Time
                    double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                    Log.Dprint("DwellTime(s) : " + param.DwellTime + ", ellapsed : " + ellapsed);
                    double realDwellTime = param.DwellTime - ellapsed;
                    if (realDwellTime > 0)
                    {
                        Log.Dprint("wait real DwellTime(s) : " + realDwellTime);
                        Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                    }
                }
            }
            else
            {
                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Wet)
                {
                    if (this.RunnableModule.Mode == ModuleMode.MainMode)
                    {
                        Machine.Instance.DualValve.SprayCycleAndWait((short)shots);
                    }
                    else
                    {
                        Machine.Instance.Valve1.SprayCycleAndWait((short)shots);
                    }
                }
                DateTime sprayEnd = DateTime.Now;

                // 等待一段时间 Dwell Time
                double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                Log.Dprint("DwellTime(s) : " + param.DwellTime + ", ellapsed : " + ellapsed);
                double realDwellTime = param.DwellTime - ellapsed;
                if (realDwellTime > 0)
                {
                    Log.Dprint("wait real DwellTime(s) : " + realDwellTime);
                    Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                }
            }

            return ret;
        }

        /// <summary>
        /// CV模式和点检测模式的运行逻辑
        /// </summary>
        /// <returns></returns>
        private Result CvAndInspectLogic(PointD pos)
        {
            Result ret = Result.OK;
            ret = Machine.Instance.Robot.MoveSafeZAndReply();

            if (!ret.IsOk)
            {
                return ret;
            }

            // 移动到指定位置
            Log.Dprint("move to position XY : " + pos);
            ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                this.Program.MotionSettings.VelXY,
                this.Program.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }

            if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectDot)
            {
                InspectionDot inspectionDot = InspectionMgr.Instance.FindBy((int)InspectionKey.Dot1) as InspectionDot;
                if (inspectionDot != null)
                {
                    Thread.Sleep(inspectionDot.SettlingTime);
                    double dx, dy;
                    Machine.Instance.CaptureAndInspect(inspectionDot);
                    dx = inspectionDot.PhyResultX;
                    dy = inspectionDot.PhyResultY;
                    string line = string.Format("{0},{1},{2},{3}", Math.Round(pos.X, 3), Math.Round(pos.Y, 3), Math.Round(dx, 3), Math.Round(dy, 3));
                    CsvUtil.WriteLine(Program.RuntimeSettings.FilePathInspectDot, line);
                    Thread.Sleep(inspectionDot.DwellTime);
                }
            }

            return ret;
        }

        /// <summary>
        /// Wet模式和Dry模式下的运动逻辑
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="simulPos"></param>
        /// <param name="currZ"></param>
        /// <param name="targZ"></param>
        /// <returns></returns>
        private Result WetAndDryMovelogic(PointD pos, PointD simulPos, double currZ, double targZ)
        {
            Result ret = Result.OK;

            if (currZ > targZ)
            {
                // 移动到指定位置
                Log.Dprint("move to position XY : " + pos);
                if (this.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos, 
                        this.Program.MotionSettings.VelXYAB,
                        this.Program.MotionSettings.AccXYAB,
                        (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                        this.Program.MotionSettings.VelXY,
                        this.Program.MotionSettings.AccXY);
                }
                if (!ret.IsOk)
                {
                    return ret;
                }
                double z = 0;
                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                {
                    z = targZ;
                }
                else
                {
                    z = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
                }

                Log.Dprint("move down to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, param.DownSpeed, param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            else
            {
                double z = 0;
                if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
                {
                    z = targZ;
                }
                else
                {
                    z = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
                }
                Log.Dprint("move up to Z : " + z.ToString("0.000000") + ", DispenseGap=" + param.DispenseGap.ToString("0.000000"));
                ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(z, param.DownSpeed, param.DownAccel);
                if (!ret.IsOk)
                {
                    return ret;
                }

                Log.Dprint("move to position XY : " + pos);
                if (this.RunnableModule.Mode == ModuleMode.MainMode)
                {
                    ret = Machine.Instance.Robot.MovePosXYABAndReply(pos, simulPos,
                            this.Program.MotionSettings.VelXYAB,
                            this.Program.MotionSettings.AccXYAB,
                            (int)Machine.Instance.Setting.CardSelect);
                }
                else
                {
                    ret = Machine.Instance.Robot.MovePosXYAndReply(pos,
                            this.Program.MotionSettings.VelXY,
                            this.Program.MotionSettings.AccXY);
                }
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取副阀的AB轴的移动目标位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private PointD GetSimulPos(PointD pos)
        {
            PointD simulPos = new PointD();
            ///生成副阀相关参数(起点、插补点位)
            if (this.RunnableModule.Mode == ModuleMode.MainMode)
            {
                //副阀插补坐标绝对值(X方向实际坐标取负值) = 主阀机械坐标-副阀机械坐标-双阀原点间距（理论情况-不考虑坐标系不平行）
                VectorD SimulModuleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1;
                simulPos = pos - this.RunnableModule.SimulTransformer.Transform(pos).ToVector() - SimulModuleOffset;
                simulPos.X = -Math.Abs(simulPos.X) / Machine.Instance.Robot.CalibPrm.HorizontalRatio;
                simulPos.Y = -simulPos.Y / Machine.Instance.Robot.CalibPrm.VerticalRatio;
            }
            else
            {
                simulPos = new PointD(Program.RuntimeSettings.SimulDistence, 0);
            }

            return simulPos;
        }
    }
}
