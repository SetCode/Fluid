using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Drive.Motion.Scheduler;
using Anda.Fluid.Drive.Motion.Locations;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Sensors;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure.HotKeying;
using System.Diagnostics;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.Heater;
using System.Threading;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.MachineStates;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.Sensors.DigitalGage;
using Anda.Fluid.Drive.Sensors.Barcode;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Drive.SetupCard;
using Anda.Fluid.Drive.SetupIO;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using System.Drawing;
using Anda.Fluid.Infrastructure.Data;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Drive.Vision.Barcode;
using System.Runtime.InteropServices;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge;
using Anda.Fluid.Infrastructure.GenKey;
using System.IO;
using Anda.Fluid.Drive.Motion.Command;

namespace Anda.Fluid.Drive
{
    public sealed class Machine : IAlarmSenderable
    {
        #region Constructor

        private static readonly Machine instance = new Machine();
        private Machine()
        {

        }
        public static Machine Instance => instance;

        #endregion


        #region Properties

        public MachineSetting Setting { get; set; }

        public ICardSetupable CardSetupable { get; private set; }

        public IIOSetupable IOSetupable { get; private set; }

        public bool IsProducting { get; set; }

        public bool IsOffline { get; set; }

        public bool IsAborted { get; set; }

        /// <summary>
        /// 是否是运动异常导致停止
        /// </summary>
        public bool IsMotionErrStop { get; set; } = false;
        /// <summary>
        /// 是否气压异常导致停止
        /// </summary>
        public bool IsAirPressureErrStop { get; set; } = false;

        public bool IsAllInitDone => IsMotionInitDone && IsVisionInitDone && IsSensorsInitDone;

        public bool IsMotionInitDone => IsCardInitDone && this.Robot.IsHomeDone;

        public bool IsCardInitDone { get; private set; }

        public bool IsVisionInitDone => this.Camera.IsInitDone;

        public bool IsSensorsInitDone
        {
            get
            {
                bool result = (this.Laser.Laserable.CommunicationOK != ComCommunicationSts.ERROR)
                && (this.Scale.Scalable.CommunicationOK != ComCommunicationSts.ERROR)
                && (this.HeaterController1.HeaterControllable.CommunicationOK != ComCommunicationSts.ERROR)
                && (this.Proportioner1.Proportional.CommunicationOK != ComCommunicationSts.ERROR);
                if (this.Setting.ValveSelect == ValveSelection.双阀)
                {
                    result = result && (this.Proportioner2.Proportional.CommunicationOK != ComCommunicationSts.ERROR);
                }
                return result;
            }
        }

        public string HardwareErrorString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                if (!this.IsMotionInitDone) builder.AppendLine("Motion Error!");
                if (!this.IsVisionInitDone) builder.AppendLine("Vision Error!");
                if (this.Laser.Laserable.CommunicationOK == ComCommunicationSts.ERROR) builder.AppendLine("Laser Error!");
                if (this.Scale.Scalable.CommunicationOK==ComCommunicationSts.ERROR) builder.AppendLine("Scale Error!");
                if (this.HeaterController1.HeaterControllable.CommunicationOK == ComCommunicationSts.ERROR) builder.AppendLine("Heater Error!");
                if (this.Proportioner1.Proportional.CommunicationOK == ComCommunicationSts.ERROR) builder.AppendLine("Proportioner1 Error!");
                if (this.Setting.ValveSelect == ValveSelection.双阀)
                {
                    if (this.Proportioner2.Proportional.CommunicationOK == ComCommunicationSts.ERROR) builder.AppendLine("Proportioner2 Error!");
                }
                return builder.ToString();
            }
        }

        public RobotXYZ Robot { get; private set; }

        public Valve Valve1 { get; set; }

        public Valve Valve2 { get; set; }

        public DualValve DualValve { get; set; }

        public Camera Camera { get; private set; }

        public CameraMotion cameraMotion { get; private set; }

        public Light Light { get; private set; }

        public Laser Laser { get; private set; }

        public HeaterController HeaterController1 { get; private set; }

        public HeaterController HeaterController2 { get; private set; }

        public Scale Scale { get; private set; }

        public Proportioner Proportioner1 { get; private set; }

        public Proportioner Proportioner2 { get; private set; }

        public LightTower LightTower { get; private set; } = new LightTower();

        public DigitalGage DigitalGage { get; private set; }

        public BarcodeScanner BarcodeSanncer1 { get; private set; }

        public BarcodeScanner BarcodeSanncer2 { get; private set; }

        public bool IsInitializing { get; private set; }

        public StateMachine FSM { get; private set; } = new StateMachine();


        public bool IsBusy
        {
            get
            {
                if (this.IsProducting)
                {
                    return true;
                }
                if (this.Robot.State == ActiveItemState.Busy)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion


        #region Interfaces

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.GetType().Name;

        #endregion


        #region Execute

        public Result MeasureHeightBefore()
        {
            Result ret = Result.OK;
            if (this.Setting.MachineSelect == MachineSelection.RTV)
            {
                DoType.测高阀.Set(true);
                Thread.Sleep(1000);
                //Z轴移动
                ret=this.Robot.MoveMeasureHeightZAndReply();                
            }
            return ret;
        }

        public Result MeasureHeightAfter()
        {
            Result ret = Result.OK;
            //如果是RTV
            if (this.Setting.MachineSelect == MachineSelection.RTV)
            {
                DoType.测高阀.Set(false);
                //Z轴移动
                ret = this.Robot.MoveSafeZAndReply();                
            }
            return ret;
        }

        public Result MeasureHeight(out double value)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                value = 0;
                return Result.OK;
            }
                      
            int rtn = this.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value);
            if (rtn != 0)
            {
                AlarmServer.Instance.Fire(this.Laser, AlarmInfoSensors.WarnMeasureHeight);
                return Result.FAILED;
            }            

            return Result.OK;
        }

        public Result CaptureMark(MarkFindPrmBase markFindPrm, out Bitmap bmp)
        {
            bmp = null;
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            //this.Light.SetLight(markFindPrm.LightType);
            this.Light.SetLight(markFindPrm.ExecutePrm);
            this.Camera.SetExposure(markFindPrm.ExposureTime);
            this.Camera.SetGain(markFindPrm.Gain);
            Thread.Sleep(markFindPrm.SettlingTime);

            var a = this.Camera.TriggerAndReply(TimeSpan.FromSeconds(1));
            if (a == null)
            {
                return Result.FAILED;
            }
            if (a.Item1 == null)
            {
                return Result.FAILED;
            }
            //bmp = a.Item2;//.DeepClone();
            markFindPrm.ImgData = a.Item1.DeepClone();
            markFindPrm.ImgWidth = this.Camera.Executor.ImageWidth;
            markFindPrm.ImgHeight = this.Camera.Executor.ImageHeight;
            markFindPrm.TargetInMachine = new PointD(markFindPrm.PosInMachine.X, markFindPrm.PosInMachine.Y);
            //移动到拍照高度
            if(this.Setting.MachineSelect==MachineSelection.RTV)
            {
                this.Robot.MoveToMarkZAndReply();
            }
            if (!markFindPrm.Execute())
            {
                Logger.DEFAULT.Warn(LogCategory.RUNNING | LogCategory.MANUAL, this.GetType().Name, "camera find mark failed");
                //AlarmServer.Instance.Fire(this.Camera, AlarmInfoVision.WarnFindMarkFailed);
                return Result.FAILED;
            }

            markFindPrm.TargetInMachine += this.Camera.ToMachine(markFindPrm.MarkInImg);
            if (markFindPrm.IsUnStandard && markFindPrm.UnStandardType == 1)
            {
                markFindPrm.TargetInMachine2 = new PointD(markFindPrm.PosInMachine.X, markFindPrm.PosInMachine.Y) + this.Camera.ToMachine(markFindPrm.MarkInImg2);
            }

            if (markFindPrm.IsOutOfTolerance())
            {
                Logger.DEFAULT.Warn(LogCategory.RUNNING | LogCategory.MANUAL, this.GetType().Name, "camera find mark out of tolerance");
                //AlarmServer.Instance.Fire(this.Camera, AlarmInfoVision.WarnFindMarkOutOfTolerance);
                return Result.FAILED;
            }

            // bytes to bitmap, bitmap操作不是线程安全的
            bmp = markFindPrm.ImgData.ToBitmap(markFindPrm.ImgWidth, markFindPrm.ImgHeight);

            return Result.OK;
        }

        public Result CaptureMark(MarkFindPrmBase markFindPrm)
        {
            Bitmap bmp;
            return this.CaptureMark(markFindPrm, out bmp);
        }

        public Result CaptureAndInspect(Inspection inspection)
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            //this.Light.SetLight(inspection.LightType);
            this.Light.SetLight(inspection.ExecutePrm);            
            this.Camera.SetExposure(inspection.ExposureTime);
            this.Camera.SetGain(inspection.Gain);
            Thread.Sleep(inspection.SettlingTime);
            byte[] bytes = this.Camera.TriggerAndGetBytes(TimeSpan.FromSeconds(1)).DeepClone();
            if (bytes == null)
            {
                return Result.FAILED;
            }
            //移动到拍照高度
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                Machine.Instance.Robot.MoveToMarkZAndReply();
            }
            inspection.Execute(Camera.Executor.CurrentBytes, Camera.Executor.ImageWidth, Camera.Executor.ImageHeight);
            if (!inspection.IsCurrResultOk)
            {
                AlarmServer.Instance.Fire(this.Camera, AlarmInfoVision.InspectionResultNG);
                return Result.FAILED;
            }
            if (inspection is InspectionDot)
            {
                InspectionDot inspectionDot = inspection as InspectionDot;
                if (inspectionDot == null)
                {
                    Logger.DEFAULT.Warn(LogCategory.RUNNING | LogCategory.MANUAL, this.GetType().Name, "inspection dot result is NG");
                    return Result.FAILED;
                }
                PointD p = this.Camera.ToMachine(inspectionDot.PixResultX, inspectionDot.PixResultY);
                inspectionDot.PhyResultX = p.X;
                inspectionDot.PhyResultY = p.Y;
            }
            else if (inspection is InspectionLine)
            {
                InspectionLine inspectionLine = inspection as InspectionLine;
                if (inspectionLine == null)
                {
                    return Result.FAILED;
                }
                PointD p1 = this.Camera.ToMachine(Camera.Executor.ImageWidth / 2, Camera.Executor.ImageHeight / 2 + inspectionLine.PixWidth1);
                PointD p2 = this.Camera.ToMachine(Camera.Executor.ImageWidth / 2, Camera.Executor.ImageHeight / 2 + inspectionLine.PixWidth2);
                inspectionLine.PhyWidth1 = p1.Y;
                inspectionLine.PhyWidth2 = p2.Y;
            }
            return Result.OK;
        }

        public Result CaptureAndMeasure(MeasurePrm measurePrm, out Bitmap bmp)
        {
            bmp = null;
            Result res=Result.OK;
            if (Machine.instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            this.Light.SetLight(measurePrm.ExecutePrm);
            this.Camera.SetExposure(measurePrm.ExposureTime);
            this.Camera.SetGain(measurePrm.Gain);
            Thread.Sleep(measurePrm.SettlingTime);

            var a = this.Camera.TriggerAndReply(TimeSpan.FromSeconds(1));
            if (a == null || a.Item1 == null)
            {
                measurePrm.PhyResult = -1;
                return Result.FAILED;
            }

            measurePrm.ImgData = a.Item1.DeepClone();
            measurePrm.ImgWidth = this.Camera.Executor.ImageWidth;
            measurePrm.ImgHeight = this.Camera.Executor.ImageHeight;
            //移动到拍照高度
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                Machine.Instance.Robot.MoveToMarkZAndReply();
            }
            bool ret = measurePrm.Execute();
            if (!ret)
            {
                AlarmServer.Instance.Fire(this.Camera, AlarmInfoVision.InspectionResultNG);
                measurePrm.PhyResult = -1;
                return Result.FAILED;
            }
            if (measurePrm.Vendor == MeasureVendor.ASV)
            {
                if (MeasureType.线宽 == measurePrm.measureType)
                {
                    PointD p = this.Camera.ToMachine(this.Camera.Executor.ImageWidth / 2, this.Camera.Executor.ImageHeight / 2 + measurePrm.PixResult);
                    double width = p.DistanceTo(new PointD(0, 0));
                    measurePrm.PhyResult = width;
                }
                else if (MeasureType.圆半径 == measurePrm.measureType)
                {
                    PointD p = this.Camera.ToMachine(this.Camera.Executor.ImageWidth / 2, this.Camera.Executor.ImageHeight / 2 + measurePrm.PixResult);
                    double radius = p.DistanceTo(new PointD(0, 0));
                    measurePrm.PhyResult = radius;
                }
            }
            if (measurePrm.IsOutofTolerance())
            {
                //不报警  也不赋值为-1 edit by 肖旭
                //measurePrm.PhyResult = -1;
                //AlarmServer.Instance.Fire(this.Camera, AlarmInfoVision.InspectionResultNG);
                //res = Result.FAILED;
            }

            bmp = measurePrm.ImgData.ToBitmap(measurePrm.ImgWidth, measurePrm.ImgHeight);
            return res;
        }

        public Result CaptureAndBarcode(BarcodePrm barcodePrm, out Bitmap bmp)
        {
            bmp = null;
            if (Machine.instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            this.Light.SetLight(barcodePrm.ExecutePrm);
            this.Camera.SetExposure(barcodePrm.ExposureTime);
            this.Camera.SetGain(barcodePrm.Gain);
            Thread.Sleep(barcodePrm.SettlingTime);

            var a = this.Camera.TriggerAndReply(TimeSpan.FromSeconds(1));
            if (a == null || a.Item1 == null)
            {
                return Result.FAILED;
            }

            barcodePrm.ImgData = a.Item1.DeepClone();
            barcodePrm.ImgWidth = this.Camera.Executor.ImageWidth;
            barcodePrm.ImgHeight = this.Camera.Executor.ImageHeight;
            //barcodePrm.Bmp = this.Camera.Executor.CurrentBmp.DeepClone();
            //移动到拍照高度
            if (this.Setting.MachineSelect == MachineSelection.RTV)
            {
                this.Robot.MoveToMarkZAndReply();
            }
            bool ret = barcodePrm.Execute();
            if (!ret)
            {
                //AlarmServer.Instance.Fire(this.Camera, AlarmInfoVision.InspectionResultNG);
                return Result.FAILED;
            }

            // bytes to bitmap, bitmap操作不是线程安全的
            bmp = barcodePrm.ImgData.ToBitmap(barcodePrm.ImgWidth, barcodePrm.ImgHeight);
            return Result.OK;
        }

        public Result StartBufFluid(InitLook lookAheadPrm)
        {
            this.Robot.UpdateBufItems();
            return this.Valve1.StartBufFluid(lookAheadPrm);
        }

        public BarcodeScanner GetCurConveyorBarcodeScanner(int conveyorNo)
        {
            if (conveyorNo == 0)
            {
                return Machine.instance.BarcodeSanncer1;
            }
            else
            {
                return Machine.instance.BarcodeSanncer2;
            }
        }


        #endregion


        #region setup & unload

        public enum Hardware
        {
            Motion,
            Vision,
            Laser,
            Scale,
            Heater,
            Proportioner,
            Scanner,
            MoveHome
        }

        public event Action<Hardware> HardwareIniting;

        public event Action<Hardware, bool, int> HardwareInited;

        public event Action<bool> AllHardwareInited;

        public bool SetupAll()
        {
            //if (!GenKeySN.CheckDate())
            //{
            //    MessageBox.Show("AFM软件未注册！");
            //    return false;
            //}
            this.SetupSetting();
            this.SetupAlarm();
            this.SetupMotion();
            this.SetupIO();
            this.SetupLocations();
            this.SetupVision();
            this.SetupASV();
            this.SetupSensors();
            this.Robot?.InitStripMap();
            DataServer.Instance.Start();

            return true;
        }

        public void InitAll()
        {
            AlarmServer.Instance.MachineInitDone = false;
            this.IsInitializing = true;
            this.InitMotion();
            //由于更改加热控制方式，把打开加热控制器的步骤移到控制器内部执行。
            //this.InitIO();
            this.InitVision();
            this.InitSensors();
            this.MoveHome();
            this.AllHardwareInited?.Invoke(this.IsAllInitDone);
            this.IsInitializing = false;
            AlarmServer.Instance.MachineInitDone = true;
        }

        public void UnloadAll()
        {
            this.UnloadIO();
            this.UnloadMotion();
            this.UnloadVision();
            this.UnloadASV();
            this.UnloadSensors();
            this.UnloadAlarm();
        }

        public void SetupSetting()
        {
            string path = SettingsPath.PathMachine + "\\" + typeof(MachineSetting).Name;
            this.Setting = JsonUtil.Deserialize<MachineSetting>(path);
            if (Setting == null)
            {
                Setting = new MachineSetting();
            }
        }

        public void SetupAlarm()
        {
            AlarmServer.Instance.Register(this.Robot);
            AlarmServer.Instance.Start();
        }

        public void UnloadAlarm()
        {
            AlarmServer.Instance.Stop();
        }

        public void SetupMotion()
        {
            this.CardSetupable = CardSetupFactory.GetCardSetup(this.Setting);
            this.Robot = this.CardSetupable.Setup();
            this.SetupValve();

            //setup default prm
            if (!this.Robot.LoadDefaultPrm())
            {
                this.Robot.DefaultPrm = new RobotDefaultPrm();
                SettingUtil.ResetToDefault<RobotDefaultPrm>(this.Robot.DefaultPrm);
            }

            //setup calib prm
            if (!this.Robot.LoadCalibPrm())
            {
                this.Robot.CalibPrm = new RobotCalibPrm().Default();
            }

            //setup home prm
            if(!this.Robot.LoadHomePrm())
            {
                this.Robot.HomePrm = new RobotHomePrm().Default();
            }

            //setup 9PtCalib prm
            if(!this.Robot.LoadCalibBy9dPrm())
            {
                this.Robot.CalibBy9dPrm = new CalibBy9dPrm().Default();
            }
            this.Robot.CalibBy9dPrm.Update();

            //setup crd prm
            this.Robot.TrcPrm = new Motion.Command.MoveTrcPrm();
            this.Robot.TrcPrm.CsId = 1;
            this.Robot.TrcPrm.VelMax = Machine.Instance.Robot.DefaultPrm.MaxVelXY;
            this.Robot.TrcPrm.AccMax = Machine.Instance.Robot.DefaultPrm.MaxAccXY;
            this.Robot.TrcPrm.EvenTime = 0;
            this.Robot.TrcPrm.OrgX = 0;
            this.Robot.TrcPrm.OrgY = 0;

            this.Robot.TrcPrmWeight = new Motion.Command.MoveTrcPrm()
            {
                CsId = 1,
                VelMax = this.Robot.DefaultPrm.MaxVelXY,
                AccMax = this.Robot.DefaultPrm.MaxAccXY,
                EvenTime = 0,
                OrgX = 0,
                OrgY = 0
            };

            this.Robot.TrcPrm4Axis = new Motion.Command.MoveTrcPrm4Axis()
            {
                CsId = 1,
                VelMax = this.Robot.DefaultPrm.MaxVelXY,
                AccMax = this.Robot.DefaultPrm.MaxAccXY,
                EvenTime = 0,
                OrgX = 0,
                OrgY = 0,
                OrgA = 0,
                OrgB = 0,
            };


            this.Robot.TrcPrm3Axis = new Motion.Command.MoveTrcPrm3Axis()
            {
                CsId = 1,
                VelMax = this.Robot.DefaultPrm.MaxVelXY,
                AccMax = this.Robot.DefaultPrm.MaxAccXY,
                EvenTime = 0,
                OrgX = 0,
                OrgY = 0,
                OrgA = 0,
            };

            //加载RTV清洗动作文件 edit by 肖旭
            RTVPurgePrm.Load();

            //start scheduler
            SchedulerMotion.Instance.RegisterObserver(this.Robot);

            SchedulerMotion.Instance.Start();
            if (this.Robot.RobotIsXYZU || this.Robot.RobotIsXYZUV)
            {
                this.Robot.AxisU.LimitTrigger += AxisULimitUpdate;
            }
        }

        public void SetupValve()
        {
            if (this.Setting.ValveSelect == ValveSelection.单阀)
            {
                Axis axisA = AxisMgr.Instance.FindBy((int)AxisType.A轴);
                if (axisA != null) axisA.Enabled = false;
                Axis axisB = AxisMgr.Instance.FindBy((int)AxisType.B轴);
                if (axisB != null) axisB.Enabled = false;
            }
            else if(this.Setting.DualValveMode == DualValveMode.跟随)
            {
                if (AxisMgr.Instance.FindBy((int)AxisType.A轴)!=null)
                {
                    AxisMgr.Instance.FindBy((int)AxisType.A轴).Enabled = false;
                }
                if (AxisMgr.Instance.FindBy((int)AxisType.B轴)!=null)
                {
                    AxisMgr.Instance.FindBy((int)AxisType.B轴).Enabled = false;
                }
                
            }
        }

        public bool InitMotion()
        {
            this.IsCardInitDone = true;
            this.HardwareIniting?.Invoke(Hardware.Motion);

            this.IsCardInitDone = this.CardSetupable.Init();

            if(this.Setting.CardSelect == CardSelection.ADMC4)
            {
                Robot.HomePrm.HomePrmX.MoveDir = 0;
                Robot.HomePrm.HomePrmX.HomeOffset = 2;
                Robot.HomePrm.HomePrmY.MoveDir = 0;
                Robot.HomePrm.HomePrmY.HomeOffset = 2;
                Robot.HomePrm.HomePrmZ.HomeOffset = -3;
            }
            else
            {
                Robot.HomePrm.HomePrmX.MoveDir = -1;
                Robot.HomePrm.HomePrmX.HomeOffset = 1;
                Robot.HomePrm.HomePrmY.MoveDir = -1;
                Robot.HomePrm.HomePrmY.HomeOffset = 1;
                Robot.HomePrm.HomePrmZ.HomeOffset = -1;
            }

            //init axes
            foreach (var item in AxisMgr.Instance.FindAll())
            {
                //Log.Dprint(string.Format("initing axis: {0}...", item.Name));
                Logger.DEFAULT.Info(string.Format("initing axis: {0}...", item.Name));
                if(item.Key == (int)AxisType.A轴 || item.Key == (int)AxisType.B轴)
                {
                    continue;
                }
                if (item.Init() != 0)
                {
                    IsCardInitDone = false;
                    AlarmServer.Instance.Fire(item, AlarmInfoMotion.FatalAxisInit);
                }
                item.Servo(true);
            }

            //set min z
            if(this.Robot.DefaultPrm.MinZ > -5)
            {
                this.Robot.DefaultPrm.MinZ = -5;
            }
            this.Robot.AxisZ.SetLimit(200, this.Robot.DefaultPrm.MinZ);
            this.Robot.AxisX.SetLimit(999999, -999999);
            this.Robot.AxisY.SetLimit(999999, -999999);
            if (Setting.ValveSelect == ValveSelection.双阀 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                this.Robot.AxisA.SetLimit(999999, -999999);
                this.Robot.AxisB.SetLimit(999999, -999999);
            }

            this.HardwareInited?.Invoke(Hardware.Motion, this.IsCardInitDone, 30);
            return this.IsCardInitDone;
        }

        public bool MoveHome()
        {
            Log.Dprint("move home...");
            this.HardwareIniting?.Invoke(Hardware.MoveHome);
            //双阀回原点
            if (this.Setting.ValveSelect == ValveSelection.双阀 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                this.Robot.EanbleAB = true;
            }
            else
            {
                this.Robot.EanbleAB = false;
            }
            //RTV
            if (MachineSelection.RTV== this.Setting.MachineSelect)
            {
                DoType.工作顶升.Set(true);
            }
            this.Robot.MoveHomeAndReply();
            this.HardwareInited?.Invoke(Hardware.MoveHome, this.Robot.IsHomeDone, 10);
            return this.Robot.IsHomeDone;
        }

        public void UnloadMotion()
        {
            //unload axes
            foreach (var item in AxisMgr.Instance.FindAll())
            {
                Log.Dprint(string.Format("close axis: {0}", item.Name));
                item.Servo(false);
            }
            //unload cards
            foreach (var item in CardMgr.Instance.FindAll())
            {
                Log.Dprint(string.Format("close card: {0}", item.Name));
                item.Close();
            }
            //unload extMdl
            foreach (var item in ExtMdlMgr.Instance.FindAll())
            {
                Log.Dprint(string.Format("close extMdl: {0}", item.Name));
                item.Close();
            }
            //stop scheduler motion
            SchedulerMotion.Instance.Stop();
        }

        public void SetupIO()
        {
            this.IOSetupable = IOSetupFactory.GetIOSetupable(this.Setting);
            if (this.IOSetupable == null)
            {
                return;
            }
            if (!this.IOSetupable.LoadIOPrm())
            {
                this.IOSetupable.SetupDIPrm();
                this.IOSetupable.SetupDOPrm();
            }
            DIMgr.Instance.Clear();
            DOMgr.Instance.Clear();
            this.IOSetupable.SetupIO();
        }

        public void InitIO()
        {
            Log.Dprint(string.Format("init IO: {0}", DoType.胶枪加热1));
            this.HeaterController1.HeaterControllable.StartHeating(0);
            if (this.Setting.ValveSelect == ValveSelection.双阀)
            {
                Log.Dprint(string.Format("init IO: {0}", DoType.胶枪加热2));
                this.HeaterController2.HeaterControllable.StartHeating(0);
            }
        }

        public void UnloadIO()
        {
            foreach (var item in DOMgr.Instance.FindAll())
            {
                item.Set(false);
            }
        }

        public void SetupFSM()
        {
            this.FSM.ChangeState(MachineEStopState.Instance);
            MachineServer.Instance.Start();
        }

        public void SetupLocations()
        {   
            this.UpdateLocations();
            //setup locations
            LocationMgr.Instance.Load();
            LocationMgr.Instance.InsertRange(0, this.Robot.SystemLocations.All);
        }

        public void UpdateLocations()
        {
            this.Robot.SystemLocations.PurgeLoc.X = this.Robot.CalibPrm.PurgeLoc.X;
            this.Robot.SystemLocations.PurgeLoc.Y = this.Robot.CalibPrm.PurgeLoc.Y;
            this.Robot.SystemLocations.PurgeLoc.Z = this.Robot.CalibPrm.StandardZ
                      + this.Robot.CalibPrm.PurgeZbyHS - this.Robot.CalibPrm.StandardHeight 
                      + this.Robot.CalibPrm.PurgeIntervalHeight;

            this.Robot.SystemLocations.ScaleLoc.X = this.Robot.CalibPrm.ScaleLoc.X;
            this.Robot.SystemLocations.ScaleLoc.Y = this.Robot.CalibPrm.ScaleLoc.Y;

            this.Robot.SystemLocations.ScaleLoc.Z = this.Robot.CalibPrm.StandardZ
                   + this.Robot.CalibPrm.ScaleZbyHS - this.Robot.CalibPrm.StandardHeight 
                   + this.Robot.CalibPrm.ScaleIntervalHeight;
            this.Robot.SystemLocations.PrimeLoc.X = this.Robot.CalibPrm.PrimeLoc.X;
            this.Robot.SystemLocations.PrimeLoc.Y = this.Robot.CalibPrm.PrimeLoc.Y;
            this.Robot.SystemLocations.PrimeLoc.Z = this.Robot.CalibPrm.PrimeZ;

            this.Robot.SystemLocations.ScrapeLoc.X = this.Robot.CalibPrm.ScrapeLocation.X;
            this.Robot.SystemLocations.ScrapeLoc.Y = this.Robot.CalibPrm.ScrapeLocation.Y;
            this.Robot.SystemLocations.ScrapeLoc.Z = this.Robot.CalibPrm.ScrapeLocation.Z;
        }

        public void SetupVision()
        {
            if(!CameraPrmMgr.Instance.Load())
            {
                CameraPrmMgr.Instance.Add(new CameraPrm(0).Default());
            }
            this.Camera = new Camera(0, CameraPrmMgr.Instance.FindBy(0));
            this.Camera.PrmBackUP = (CameraPrm)CameraPrmMgr.Instance.FindBy(0).Clone();
            CameraMgr.Instance.Add(this.Camera);
            TempVisionData.Ins.Load();
        }

        public bool InitVision()
        {
            this.HardwareIniting?.Invoke(Hardware.Vision);
            Log.Dprint("init vision...");
            this.Camera.Close();
            if (this.Camera.Open() == 0)
            {
                this.Camera.Init();
            }
            //初始化光源
            this.Light.Init();
            Log.Dprint("reset light to last");
            this.Light.ResetToLast();
            this.HardwareInited?.Invoke(Hardware.Vision, this.IsVisionInitDone, 20);
            //this.Light.SetLight(LightType.Coax);
            //this.Light.SetLight();
            return this.IsVisionInitDone;
        }

        public void UnloadVision()
        {
            this.Camera?.Close();
        }

        public void SetupASV()
        {
            string path = Application.StartupPath + "\\ASVCoreInspection";
            if (Directory.Exists(path))
            {
                string[] dirs = Directory.GetDirectories(path);
                if (dirs.Length > 40)
                {
                    for (int i = 40; i < dirs.Length; i++)
                    {
                        Directory.Delete(dirs[i], true);
                    }
                }
            }            
            ASVCore.InitiateAsv(true);           
            ASVCore.LoadFrom(path);

            bool flag = true;
            if (InspectionMgr.Instance.Load())
            {
                if(InspectionMgr.Instance.List.Count != 40)
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }

            if (!flag)
            {
                InspectionMgr.Instance.List.Clear();

                for (int i = (int)InspectionKey.Dot1; i <= (int)InspectionKey.Dot5; i++)
                {
                    ASVCore.CreatInspection();
                    InspectionMgr.Instance.List.Add(new InspectionDot(i));
                }
                
                for (int i = (int)InspectionKey.Line1; i <= (int)InspectionKey.Line5; i++)
                {
                    ASVCore.CreatInspection();
                    InspectionMgr.Instance.List.Add(new InspectionLine(i));
                }

                // 非标Mark
                for (int i = (int)InspectionKey.Mark1; i <= (int)InspectionKey.Mark10; i++)
                {
                    ASVCore.CreatInspection();
                    InspectionMgr.Instance.List.Add(new Inspection(i));
                }
                // 检测尺寸
                for (int i = (int)InspectionKey.Measure1; i <= (int)InspectionKey.Measure10; i++)
                {
                    ASVCore.CreatInspection();
                    InspectionMgr.Instance.List.Add(new Inspection(i));
                }
                // 条码识别
                for (int i = (int)InspectionKey.Barcode1; i <= (int)InspectionKey.Barcode10; i++)
                {
                    ASVCore.CreatInspection();
                    InspectionMgr.Instance.List.Add(new Inspection(i));
                }
            }
        }

        public void UnloadASV()
        {
            ASVCore.CloseAsv();
        }

        public void SetupSensors()
        {
            if (!SensorMgr.Instance.Load())
            {
                EasySerialPort easySerialPort = new EasySerialPort((int)SerialPortType.Laser)
                { Name = "Laser", PortName = "COM1", BaudRate = BaudRate.BR_38400, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false,RTS=false};
                SensorMgr.Instance.Laser = new LaserSetting()
                { EasySerialPort = easySerialPort, Vendor = Laser.Vendor.IL };

                easySerialPort = new EasySerialPort((int)SerialPortType.Scale)
                { Name = "Scale", PortName = "COM2", BaudRate = BaudRate.BR_9600, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = true, RTS = false};
                SensorMgr.Instance.Scale = new ScaleSetting()
                { EasySerialPort = easySerialPort, Vendor = Scale.Vendor.Sartorius };

                easySerialPort = new EasySerialPort((int)SerialPortType.Heater){Name = "Heater",PortName = "COM3",BaudRate = BaudRate.BR_9600,DataBits = DataBits.DB_8,Parity = System.IO.Ports.Parity.None,StopBits = System.IO.Ports.StopBits.One,DTR = false,RTS = false};
                SensorMgr.Instance.Heater = new HeaterSetting() { EasySerialPort = easySerialPort, Vendor = HeaterControllerMgr.Vendor.Omron };

                easySerialPort = new EasySerialPort((int)SerialPortType.Proportioners)
                { Name = "Proportioner", PortName = "COM5", BaudRate = BaudRate.BR_9600, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
                SensorMgr.Instance.Proportioners = new ProportionerSetting()
                { EasySerialPort = easySerialPort, Channel1 = 3, ControlType1 = Proportioner.ControlType.Direct, Channel2 = 4, ControlType2 = Proportioner.ControlType.Direct };

                SensorMgr.Instance.Conveyor1 = new EasySerialPort((int)SerialPortType.Conveyor1)
                { Name = "Conveyor1", PortName = "COM4", BaudRate = BaudRate.BR_9600, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };

                SensorMgr.Instance.DigitalGage = new EasySerialPort((int)SerialPortType.DigitalGage)
                { Name="DigitalGage",PortName="COM11",BaudRate=BaudRate.BR_9600,DataBits=DataBits.DB_8,StopBits=System.IO.Ports.StopBits.One,Parity=System.IO.Ports.Parity.None, DTR = false, RTS = false };

                easySerialPort = new EasySerialPort((int)SerialPortType.BarcodeScanner1)
                { Name = "BarcodeScanner1", PortName = "COM8", BaudRate = BaudRate.BR_19200, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
                SensorMgr.Instance.barcodeScanner1 = new BarcodeScanSetting()
                { EasySerialPort = easySerialPort, Vendor = BarcodeScanner.Vendor.SR700 };

                easySerialPort = new EasySerialPort((int)SerialPortType.BarcodeScanner2)
                { Name = "BarcodeScanner2", PortName = "COM6", BaudRate = BaudRate.BR_19200, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
                SensorMgr.Instance.barcodeScanner2 = new BarcodeScanSetting()
                { EasySerialPort = easySerialPort, Vendor = BarcodeScanner.Vendor.SR700 };

                SensorMgr.Instance.SvValve = new EasySerialPort((int)SerialPortType.SvValve)
                { Name = "SvValve", PortName = "COM9", BaudRate = BaudRate.BR_9600, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };

                //光源
                easySerialPort = new EasySerialPort((int)SerialPortType.Light)
                { Name = "Light", PortName = "COM10", BaudRate = BaudRate.BR_19200, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
                SensorMgr.Instance.Light = new LightSetting()
                { EasySerialPort= easySerialPort ,Vendor=LightVendor.Anda};

            }

            //setup digitalGage
            if (SensorMgr.Instance.DigitalGage == null)
            {
                SensorMgr.Instance.DigitalGage = new EasySerialPort((int)SerialPortType.DigitalGage)
                { Name = "DigitalGage", PortName = "COM11", BaudRate = BaudRate.BR_9600, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
            }
            this.DigitalGage = new DigitalGage(0).LoadSetting(new DigitalGageEee(SensorMgr.Instance.DigitalGage));
            
            //setup laser
            this.Laser = new Laser(0).LoadSetting(SensorMgr.Instance.Laser);

            //setup scale
            this.Scale = new Scale(0).LoadSetting(SensorMgr.Instance.Scale);
            if (!this.Scale.Scalable.LoadPrm())
            {
                this.Scale.Scalable.Prm = new ScalePrm(0);
                SettingUtil.ResetToDefault<ScalePrm>(this.Scale.Scalable.Prm);
            }

            //Setup BarcodeScanner
            if (SensorMgr.Instance.barcodeScanner1 == null)
            {
                EasySerialPort easySerialPort = new EasySerialPort((int)SerialPortType.BarcodeScanner1)
                { Name = "BarcodeScanner", PortName = "COM8", BaudRate = BaudRate.BR_19200, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false,RTS=false };
                SensorMgr.Instance.barcodeScanner1 = new BarcodeScanSetting()
                { EasySerialPort = easySerialPort, Vendor = BarcodeScanner.Vendor.SR700 };
            }
            this.BarcodeSanncer1 = new BarcodeScanner(0).LoadSetting(SensorMgr.Instance.barcodeScanner1);
            if (Machine.instance.Setting.ConveyorSelect == ConveyorSelection.双轨)
            {
                //Setup BarcodeScanner
                if (SensorMgr.Instance.barcodeScanner2 == null)
                {
                    EasySerialPort easySerialPort = new EasySerialPort((int)SerialPortType.BarcodeScanner2)
                    { Name = "BarcodeScanner", PortName = "COM6", BaudRate = BaudRate.BR_19200, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
                    SensorMgr.Instance.barcodeScanner2 = new BarcodeScanSetting()
                    { EasySerialPort = easySerialPort, Vendor = BarcodeScanner.Vendor.SR700 };
                }
                this.BarcodeSanncer2 = new BarcodeScanner(1).LoadSetting(SensorMgr.Instance.barcodeScanner2);
            }

            //setup heater
            if (!HeaterPrmMgr.Instance.Load())
            {
                HeaterPrmMgr.Instance.Add(new HeaterPrm(0));
                HeaterPrmMgr.Instance.Add(new HeaterPrm(1));
            }
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika) 
            {
                this.HeaterController1 = new HeaterController(0, 8, new AiKaThermostat(1, SensorMgr.Instance.Heater.EasySerialPort), HeaterPrmMgr.Instance.FindBy(0));
                this.HeaterController2 = new HeaterController(1, 1, new InvalidThermostat(SensorMgr.Instance.Heater.EasySerialPort), HeaterPrmMgr.Instance.FindBy(1));
            }
            else if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Omron)
            {
                this.HeaterController1 = new HeaterController(0, 1, new ThermostatOmron(2, SensorMgr.Instance.Heater.EasySerialPort), HeaterPrmMgr.Instance.FindBy(0));
                this.HeaterController2 = new HeaterController(1, 1, new ThermostatOmron(3, SensorMgr.Instance.Heater.EasySerialPort), HeaterPrmMgr.Instance.FindBy(1));
            }
            else if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Disable)
            {
                this.HeaterController1 = new HeaterController(0, 8, new InvalidThermostat(SensorMgr.Instance.Heater.EasySerialPort), HeaterPrmMgr.Instance.FindBy(0));
                this.HeaterController2 = new HeaterController(1, 1, new InvalidThermostat(SensorMgr.Instance.Heater.EasySerialPort), HeaterPrmMgr.Instance.FindBy(1));
            }
            HeaterControllerMgr.Instance.Add(this.HeaterController1);
            HeaterControllerMgr.Instance.Add(this.HeaterController2);

            //setup valve
            this.Proportioner1 = new Proportioner(0).LoadSetting(SensorMgr.Instance.Proportioners.EasySerialPort, SensorMgr.Instance.Proportioners.ControlType1, SensorMgr.Instance.Proportioners.Channel1);
            this.Proportioner2 = new Proportioner(1).LoadSetting(SensorMgr.Instance.Proportioners.EasySerialPort, SensorMgr.Instance.Proportioners.ControlType2, SensorMgr.Instance.Proportioners.Channel2);
            if (!ValveWeightPrmMgr.Instance.Load())
            {
                ValveWeightPrmMgr.Instance.Add(SettingUtil.ResetToDefault<ValveWeightPrm>(new ValveWeightPrm(ValveType.Valve1)));
                ValveWeightPrmMgr.Instance.Add(SettingUtil.ResetToDefault<ValveWeightPrm>(new ValveWeightPrm(ValveType.Valve2)));
            }

            if (!ValvePrmMgr.Instance.Load())
            {
                ValvePrmMgr.Instance.Add(SettingUtil.ResetToDefault<ValvePrm>(new ValvePrm(ValveType.Valve1)));
                ValvePrmMgr.Instance.Add(SettingUtil.ResetToDefault<ValvePrm>(new ValvePrm(ValveType.Valve2)));
            }

            if (ValvePrmMgr.Instance.FindBy(ValveType.Valve1).ValveSeires == ValveSeries.喷射阀)
            {
                this.Valve1 = new JtValve(ValveType.Valve1, this.Proportioner1, CardMgr.Instance.FindBy(0), 0, ValvePrmMgr.Instance.FindBy(ValveType.Valve1));
            }
            else if (ValvePrmMgr.Instance.FindBy(ValveType.Valve1).ValveSeires == ValveSeries.螺杆阀)
            {
                this.Valve1 = new SvValve(ValveType.Valve1, this.Proportioner1, CardMgr.Instance.FindBy(0), 0, ValvePrmMgr.Instance.FindBy(ValveType.Valve1));
            }
            else if (ValvePrmMgr.Instance.FindBy(ValveType.Valve1).ValveSeires == ValveSeries.齿轮泵阀)
            {
                this.Valve1 = new GearValve(ValveType.Valve1, this.Proportioner1, CardMgr.Instance.FindBy(0), 0, ValvePrmMgr.Instance.FindBy(ValveType.Valve1));
            }

            if (ValvePrmMgr.Instance.FindBy(ValveType.Valve2).ValveSeires == ValveSeries.喷射阀)
            {
                this.Valve2 = new JtValve(ValveType.Valve2, this.Proportioner2, CardMgr.Instance.FindBy(0), 1, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
            }
            else if (ValvePrmMgr.Instance.FindBy(ValveType.Valve2).ValveSeires == ValveSeries.螺杆阀)
            {
                this.Valve2 = new SvValve(ValveType.Valve2, this.Proportioner2, CardMgr.Instance.FindBy(0), 1, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
            }
            else if (ValvePrmMgr.Instance.FindBy(ValveType.Valve1).ValveSeires == ValveSeries.齿轮泵阀) 
            {
                this.Valve2 = new GearValve(ValveType.Valve2, this.Proportioner2, CardMgr.Instance.FindBy(0), 1, ValvePrmMgr.Instance.FindBy(ValveType.Valve2));
            }

            this.Valve1.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve1);
            this.Valve2.weightPrm = ValveWeightPrmMgr.Instance.FindBy(ValveType.Valve2);

            this.cameraMotion = new CameraMotion(CardMgr.Instance.FindBy(0), 0, 10000, 200, 10);

            //ValveMgr.Instance.Add(Valve1);
            //ValveMgr.Instance.Add(Valve2);
            if (this.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                this.DualValve = new JtDualValve(CardMgr.Instance.FindBy(0), this.Valve1, this.Valve2);
            }
            else if (this.Valve1.ValveSeries == ValveSeries.螺杆阀)
            {
                this.DualValve = new SvDualValve(CardMgr.Instance.FindBy(0), this.Valve1, this.Valve2);
            }
            else if (this.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                this.DualValve = new GearDualValve(CardMgr.Instance.FindBy(0), this.Valve1, this.Valve2);
            }
            //Light
            if (SensorMgr.Instance.Light==null || SensorMgr.Instance.Light.EasySerialPort==null)
            {
                EasySerialPort easySerialPort = new EasySerialPort((int)SerialPortType.Light)
                { Name = "Light", PortName = "COM10", BaudRate = BaudRate.BR_19200, DataBits = DataBits.DB_8, StopBits = System.IO.Ports.StopBits.One, Parity = System.IO.Ports.Parity.None, DTR = false, RTS = false };
                SensorMgr.Instance.Light = new LightSetting()
                { EasySerialPort = easySerialPort, Vendor = LightVendor.Anda };
            }
            this.Light=new Light(0).GetLight(SensorMgr.Instance.Light);
            this.Light.LoadPrm();           

            HeaterServer.Instance.Start();
            ValveSprayServer.Instance.Start();
        }

        public bool InitSensors()
        {
            this.HardwareIniting?.Invoke(Hardware.Laser);
            Log.Dprint("init laser...");
            this.Laser.Init();
            this.HardwareInited?.Invoke(Hardware.Laser, this.Laser.Laserable.CommunicationOK != ComCommunicationSts.ERROR, 10);

            this.HardwareIniting?.Invoke(Hardware.Scale);
            Log.Dprint("init scale...");
            this.Scale.Init();
            this.HardwareInited?.Invoke(Hardware.Scale, this.Scale.Scalable.CommunicationOK!= ComCommunicationSts.ERROR, 10);

           

            this.HardwareIniting?.Invoke(Hardware.Heater);
            Log.Dprint("init heater...");
            this.HeaterController1.Opeate(OperateHeaterController.启动软件时);
            this.HeaterController2.Opeate(OperateHeaterController.启动软件时);
            this.HardwareInited?.Invoke(Hardware.Heater, this.HeaterController1.HeaterControllable.CommunicationOK != ComCommunicationSts.ERROR, 5);

            this.HardwareIniting?.Invoke(Hardware.Scanner);
            Log.Dprint("init heater...");
            this.BarcodeSanncer1.Init();
            bool scanState = this.BarcodeSanncer1.BarcodeScannable.CommunicationOK != ComCommunicationSts.ERROR;
            if (this.Setting.ConveyorSelect == ConveyorSelection.双轨)
            {
                this.BarcodeSanncer2.Init();
                scanState = (this.BarcodeSanncer1.BarcodeScannable.CommunicationOK != ComCommunicationSts.ERROR) && (this.BarcodeSanncer2.BarcodeScannable.CommunicationOK != ComCommunicationSts.ERROR);
            }
            this.HardwareInited?.Invoke(Hardware.Scanner, scanState, 5);


            this.HardwareIniting?.Invoke(Hardware.Proportioner);
            Log.Dprint("init proportioner1...");
            this.Proportioner1.Init(true);
            if (this.Setting.ValveSelect == ValveSelection.双阀)
            {
                Proportioner.Sleep();
                Log.Dprint("init proportioner2...");
                this.Proportioner2.Init(false);
            }
            this.HardwareInited?.Invoke(Hardware.Proportioner, (this.Proportioner1.Proportional.CommunicationOK != ComCommunicationSts.ERROR && this.Proportioner2.Proportional.CommunicationOK != ComCommunicationSts.ERROR), 10);
            
            //open conveyor1
            SensorMgr.Instance.Conveyor1.Close();
            if(!SensorMgr.Instance.Conveyor1.Open())
            {
                AlarmServer.Instance.Fire(SensorMgr.Instance.Conveyor1, AlarmInfoSensors.SerialPortOpenAlarm);
            }

            
            //set heater params
            SensorMgr.Instance.Heater.SetParam();
            //start heating
            SensorMgr.Instance.Heater.StartHeating();

            return this.IsSensorsInitDone;
        }

        public void UnloadSensors()
        {
            this.Laser.Laserable.Disconnect();
            this.Scale.Scalable.Disconnect();          
            this.HeaterController1.HeaterControllable.Disconnect();
            this.Valve1.Proportioner.Proportional.Disconnect();
            if(this.Setting.ValveSelect == ValveSelection.双阀)
            {
                this.Valve2.Proportioner.Proportional.Disconnect();
            }
            SensorMgr.Instance.Conveyor1.Close();

        }

       
        #endregion


        #region settings

        public void SaveAllSettings()
        {
            SaveMotionSettings();
            SaveVisionSettings();
            SaveSensorSettings();
            SaveValveSettings();
            SaveWeightSetting();
            SaveLightSettings();
        }

        public void SaveMotionSettings()
        {
            AxisPrmMgr.Instance.Save();
            LocationMgr.Instance.Save();
            //DIMgr.Instance.Save();
            //DOMgr.Instance.Save();
            //IOPrmMgr.Instance.Save();
            this.IOSetupable?.SaveIOPrm();
            this.Robot?.SaveDefaultPrm();
            this.Robot?.SaveCalibPrm();
            this.Robot?.SaveHomePrm();
            //this.Robot?.SaveCalibBy9dPrm();
        }

        public void ResetMotionSettings()
        {
            foreach (var item in AxisPrmMgr.Instance.FindAll())
            {
                SettingUtil.ResetToDefault<AxisPrm>(item);
            }
            SettingUtil.ResetToDefault<RobotDefaultPrm>(this.Robot?.DefaultPrm);
            this.Robot?.CalibPrm.Default();
            this.Robot?.HomePrm.Default();
        }

        public void SaveValveSettings()
        {
            ValvePrmMgr.Instance.Save();

            //阀称重
            ValveWeightPrmMgr.Instance.Save();
        }

        public void ResetValveSettings()
        {
            foreach (var item in ValvePrmMgr.Instance.FindAll())
            {                
                item.ResetToDefault();
            }
            this.ResetValveWeightSettings();
        }

        public void ResetValveWeightSettings()
        {
            foreach (var item in ValveWeightPrmMgr.Instance.FindAll())
            {
                SettingUtil.ResetToDefault<ValveWeightPrm>(item);
            }
        }

        public void SaveWeightSetting()
        {            
            this.Scale.Scalable.SavePrm();            
        }

        public void ResetWeightSetting()
        {
            //SettingUtil.ResetToDefault<WeightPrm>(this.Weight.Prm);
            SettingUtil.ResetToDefault<ScalePrm>(Machine.Instance.Scale.Scalable.Prm);
        }

        public void SaveVisionSettings()
        {
            CameraPrmMgr.Instance.Save();          
            InspectionMgr.Instance.Save();
        }

        public void SaveSensorSettings()
        {
            SensorMgr.Instance.Save();
        }

        public void SaveLightSettings()
        {
            this.Light.SavePrm();
        }

        #endregion


        #region Axis Event
        /// <summary>
        /// IO限位状态刷新(当前仅U轴使用)
        /// </summary>
        public void AxisULimitUpdate()
        {
            bool isTrigger = DIMgr.Instance.FindBy((int)DiType.U轴限位).Status.Value;
            if (isTrigger && this.Robot.AxisU.Pos > 0)
            {
                this.Robot.AxisU.IsPLmt.Update(true);
                this.Robot.AxisU.IsNLmt.Update(false);
                if (this.Robot.AxisU.State.MoveMode != AxisMoveMode.MoveHome)
                {
                    AlarmServer.Instance.Fire(this.Robot.AxisU, AlarmInfoMotion.WarnMoveToPLmt);
                }
            }
            else if (isTrigger && this.Robot.AxisU.Pos < 0)
            {
                this.Robot.AxisU.IsPLmt.Update(false);
                this.Robot.AxisU.IsNLmt.Update(true);
                if (this.Robot.AxisU.State.MoveMode != AxisMoveMode.MoveHome)
                {
                    AlarmServer.Instance.Fire(this.Robot.AxisU, AlarmInfoMotion.WarnMoveToNLmt);
                }
            }
            else if (isTrigger && this.Robot.AxisU.Pos == 0)
            {
                this.Robot.AxisU.IsNLmt.Update(true);
                if (this.Robot.AxisU.State.MoveMode != AxisMoveMode.MoveHome)
                {
                    AlarmServer.Instance.Fire(this.Robot.AxisU, AlarmInfoMotion.WarnMoveToNLmt);
                }
            }
            else
            {
                this.Robot.AxisU.IsNLmt.Update(false);
                this.Robot.AxisU.IsPLmt.Update(false);
                AlarmServer.Instance.RemoveAlarm(this.Robot.AxisU, AlarmInfoMotion.WarnMoveToNLmt);
                AlarmServer.Instance.RemoveAlarm(this.Robot.AxisU, AlarmInfoMotion.WarnMoveToPLmt);
            }
            // 非生产状态且触发限位，U轴急停(回原点时不停止)
            if ((this.Robot.AxisU.IsNLmt.IsRising || this.Robot.AxisU.IsPLmt.IsRising) && this.Robot.AxisU.State.MoveMode != AxisMoveMode.MoveHome)
            {
                this.Robot.AxisU.MoveAbruptStop();
            }
        }

        #endregion

        #region ohter

        public bool IsErrorStop()
        {
            return Machine.instance.IsAirPressureErrStop || Machine.instance.IsMotionErrStop;
        }

        /// <summary>
        /// 返回当前机型是否有轨道，且使用启动按钮运行
        /// </summary>
        /// <returns></returns>
        public bool IsNoConveyor()
        {
            if (this.Setting.MachineSelect == MachineSelection.TSV300 
                || this.Setting.MachineSelect == MachineSelection.YBSX)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
