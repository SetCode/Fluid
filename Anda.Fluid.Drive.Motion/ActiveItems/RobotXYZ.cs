using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.Scheduler;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion.Locations;
using System.Threading;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    public enum RobotState
    {
        WaitHome,
        Homing,
        Idel,
        Moving,
        Alarm
    }

    /// <summary>
    /// 
    /// </summary>
    public enum RobotAxesStyle
    {
        /// <summary>
        /// 单阀 
        /// </summary>
        XYZ,
        /// <summary>
        /// 双阀
        /// </summary>
        XYZAB,
        /// <summary>
        /// 单阀 + 针嘴旋转
        /// </summary>
        XYZR,
        /// <summary>
        /// 单阀 + 胶阀倾斜
        /// </summary>
        XYZU,
        /// <summary>
        /// 单阀 + 四方位
        /// </summary>
        XYZUV
    }

    public class RobotXYZ : ActiveItem, IAlarmObservable, IAlarmSenderable
    {

        #region Constructor

        public RobotXYZ(Axis axisX, Axis axisY, Axis axisZ)
        {
            this.AxisX = axisX;
            this.AxisY = axisY;
            this.AxisZ = axisZ;
            this.AxesXY = new Axis[] { this.AxisX, this.AxisY };
            this.AxesXYZ = new Axis[] { this.AxisX, this.AxisY, this.AxisZ };
            this.AxesAll = this.AxesXYZ;
            this.AxesStyle = RobotAxesStyle.XYZ;
        }

        public RobotXYZ(Axis axisX, Axis axisY, Axis axisA, Axis axisB, Axis axisZ)
            : this(axisX, axisY, axisZ)
        {
            this.AxisA = axisA;
            this.AxisB = axisB;
            this.AxesAB = new Axis[] { axisA, axisB };
            this.AxesXYAB = new Axis[] { axisX, axisY, axisA, axisB };
            this.AxesXYABZ = new Axis[] { axisX, axisY, axisA, axisB, axisZ };
            this.AxesAll = this.AxesXYABZ;
            this.AxesStyle = RobotAxesStyle.XYZAB;
        }

        public RobotXYZ(Axis axisX, Axis axisY, Axis axisZ, Axis axisR, RobotAxesStyle axesStyle)
            : this(axisX, axisY, axisZ)
        {
            if (axesStyle == RobotAxesStyle.XYZR)
            {
                this.AxisR = axisR;
                this.AxesXYR = new Axis[] { this.AxisX, this.AxisY, this.AxisR };
                this.AxesXYZR = new Axis[] { this.AxisX, this.AxisY, this.AxisZ, this.AxisR };
                this.AxesAll = this.AxesXYZR;
            }
            else if (axesStyle == RobotAxesStyle.XYZU)
            {
                this.AxisU = axisR;
                this.AxesXYZU = new Axis[] { this.AxisX, this.AxisY, this.AxisZ, this.AxisU };
                this.AxesAll = this.AxesXYZU;
            }
            else if (axesStyle == RobotAxesStyle.XYZUV)
            {
                this.AxisU = axisR;
                this.AxesXYZU = new Axis[] { this.AxisX, this.AxisY, this.AxisZ, this.AxisU };
                this.AxesAll = this.AxesXYZU;
            }
            else
            {
                throw new Exception("select axes style error.");
            }
            this.AxesStyle = axesStyle;
            if (this.RobotIsXYZU || this.RobotIsXYZUV)
            {
                this.AxisU.useActionLimitTrigger = true;
                this.AxisU.useProfilePos = true;
            }
        }


        #endregion


        #region Properties

        /// <summary>
        /// 启用AB轴，只有配置为双阀时有效
        /// </summary>
        public bool EanbleAB { get; set; }

        public bool RobotIsXYZU => this.AxesStyle == RobotAxesStyle.XYZU;

        public bool RobotIsXYZUV => this.AxesStyle == RobotAxesStyle.XYZUV;

        public RobotAxesStyle AxesStyle { get; private set; } = RobotAxesStyle.XYZ;

        public bool IsSimulation { get; set; } = false;

        public Axis AxisX { get; private set; }

        public Axis AxisY { get; private set; }

        public Axis AxisZ { get; private set; }

        public Axis AxisR { get; private set; }

        public Axis AxisU { get; private set; }

        public Axis AxisA { get; private set; }

        public Axis AxisB { get; private set; }

        public Axis[] AxesXY { get; private set; }

        public Axis[] AxesAB { get; private set; }

        public Axis[] AxesXYZ { get; private set; }

        public Axis[] AxesXYAB { get; private set; }

        public Axis[] AxesXYABZ { get; private set; }

        public Axis[] AxesXYR { get; private set; }

        public Axis[] AxesXYZR { get; private set; }

        public Axis[] AxesXYZU { get; private set; }

        public Axis[] AxesAll { get; private set; }

        public bool IsHomeDone { get; set; }

        public double PosX => this.AxisX.Pos;

        public double PosY => this.AxisY.Pos;

        public double PosZ => this.AxisZ.Pos;

        public double PosR => this.AxisR.Pos;

        public double PosU => this.AxisU.Pos;

        public double PosA => this.AxisA.Pos;

        public double PosB => this.AxisB.Pos;

        public PointD PosXY => new PointD(PosX, PosY);

        public List<BufFluidItem> BufFluidItems { get; private set; } = new List<BufFluidItem>();

        #endregion


        #region Variables

        public RobotHomePrm HomePrm = new RobotHomePrm();

        public RobotDefaultPrm DefaultPrm = new RobotDefaultPrm();

        public RobotCalibPrm CalibPrm = new RobotCalibPrm();

        public RobotCalibPrm CalibPrmBackUp = new RobotCalibPrm();

        public RobotSystemLoc SystemLocations = new RobotSystemLoc();

        public CalibBy9dPrm CalibBy9dPrm = new CalibBy9dPrm();

        public CalibMapPrm CalibMapPrm = new CalibMapPrm();

        public bool IsMapValid = false;

        public bool IsRBFValid = false;

        public MoveTrcPrm TrcPrm = new MoveTrcPrm();

        public MoveTrcPrm TrcPrmWeight = new MoveTrcPrm();

        public MoveTrcPrm4Axis TrcPrm4Axis = new MoveTrcPrm4Axis();

        public MoveTrcPrm3Axis TrcPrm3Axis = new MoveTrcPrm3Axis();

        #endregion


        #region Interfaces

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.GetType().Name;

        public void HandleAlarmEvent(AlarmEvent e)
        {
            if (e.Info.Level == AlarmLevel.Fatal)
            {
                this.MoveStop();
            }
        }

        #endregion


        #region Prm Save & Load

        private string pathDefaultPrm = SettingsPath.PathMachine + "\\" + typeof(RobotDefaultPrm).Name;
        private string pathCalibPrm = SettingsPath.PathMachine + "\\" + typeof(RobotCalibPrm).Name;
        private string pathHomePrm = SettingsPath.PathMachine + "\\" + typeof(RobotHomePrm).Name;
        private string pathCalibBy9dPrm = SettingsPath.PathMachine + "\\" + typeof(CalibBy9dPrm).Name;

        public bool SaveDefaultPrm()
        {            
            return JsonUtil.Serialize<RobotDefaultPrm>(pathDefaultPrm, this.DefaultPrm);
        }

        public bool LoadDefaultPrm()
        {
            this.DefaultPrm = JsonUtil.Deserialize<RobotDefaultPrm>(pathDefaultPrm);
            return this.DefaultPrm != null;
        }

        public bool SaveCalibPrm()
        {
            return JsonUtil.Serialize<RobotCalibPrm>(pathCalibPrm, this.CalibPrm);
        }

        public bool LoadCalibPrm()
        {
            this.CalibPrm = JsonUtil.Deserialize<RobotCalibPrm>(pathCalibPrm);
            this.CalibPrmBackUp = JsonUtil.Deserialize<RobotCalibPrm>(pathCalibPrm);
            if (this.CalibPrm == null)
            {
                return false;
            }
            if (this.CalibPrm.HorizontalRatio < 0.9 || this.CalibPrm.HorizontalRatio > 1.1)
            {
                this.CalibPrm.HorizontalRatio = 1;
            }
            if (this.CalibPrm.VerticalRatio < 0.9 || this.CalibPrm.VerticalRatio > 1.1)
            {
                this.CalibPrm.VerticalRatio = 1;
            }
            return this.CalibPrm != null;
        }

        public bool SaveHomePrm()
        {
            return JsonUtil.Serialize<RobotHomePrm>(this.pathHomePrm, this.HomePrm);
        }

        public bool LoadHomePrm()
        {
            this.HomePrm = JsonUtil.Deserialize<RobotHomePrm>(this.pathHomePrm);
            if (this.HomePrm == null) return false;
            if (this.HomePrm.HomePrmX == null) return false;
            if (this.HomePrm.HomePrmY == null) return false;
            if (this.HomePrm.HomePrmZ == null) return false;
            if (this.HomePrm.HomePrmR == null) return false;
            if (this.HomePrm.HomePrmU == null) return false;
            if (this.HomePrm.HomePrmA == null) return false;
            if (this.HomePrm.HomePrmB == null) return false;
            if (this.HomePrm.HomePrm5 == null) return false;
            if (this.HomePrm.HomePrm6 == null) return false;
            if (this.HomePrm.HomePrm7 == null) return false;
            if (this.HomePrm.HomePrm8 == null) return false;
            return true;
        }

        public bool SaveCalibBy9dPrm()
        {
            return JsonUtil.Serialize<CalibBy9dPrm>(pathCalibBy9dPrm, this.CalibBy9dPrm);
        }

        public bool LoadCalibBy9dPrm()
        {
            this.CalibBy9dPrm = JsonUtil.Deserialize<CalibBy9dPrm>(pathCalibBy9dPrm);
            return this.CalibBy9dPrm != null;
        }

        #endregion


        #region Cmp2d

        public short ADCmp2DData(short cardId, short chn, short axisxId, short axisyId, int[] x, int[] y, short outType, ushort startLevel, ushort pulseWidthH, ushort pulseWidthL, short threshold)
        {
            Card card = this.AxisX.Card;
            return card.Executor.ADCmp2Data(cardId, chn, this.AxisX.AxisId, this.AxisY.AxisId, x, y, outType, startLevel, pulseWidthH, pulseWidthL, threshold);
   
        }


        public short Cmp2dInit(short chn, short src, short pulseWidth, short maxerr, short threshold)
        {
            try
            {
                Card card = this.AxisX.Card;
                //clear 2d compare data
                short rtn = card.Executor.Cmp2dClear(card.CardId, chn);
                if (rtn != 0) return rtn;
                //set 2d compare mode
                rtn = card.Executor.Cmp2dMode(card.CardId, chn);
                if (rtn != 0) return rtn;
                //set 2d compare params
                return card.Executor.Cmp2dSetPrm(card.CardId, chn, this.AxisX.AxisId, this.AxisY.AxisId,
                    src, 0, 0, pulseWidth, maxerr, threshold);
            }
            catch (Exception)
            {
                return -10;
            }
        }
        public short Cmp2dMode1dInit(short chn, short src, short pulseWidth, short maxerr, short threshold)
        {
            try
            {
                Card card = this.AxisX.Card;
                short rtn = card.Executor.Cmp2dClear(card.CardId, chn);
                if (rtn != 0) return rtn;
                rtn = card.Executor.Cmp2dMode1d(card.CardId, chn);
                if (rtn != 0) return rtn;
                //X比较轴数值无效，但只能设置为非0值
                return card.Executor.Cmp2dSetPrm(card.CardId, chn, 1, this.AxisY.AxisId,
                    src, 0, 0, pulseWidth, maxerr, threshold);
            }
            catch (Exception)
            {

                return -10;
            }
        }

        /// <summary>
        /// 数据压入默认fifo
        /// </summary>
        /// <param name="chn"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public short Cmp2dData(short chn, PointD[] points)
        {
            try
            {
                Card card = this.AxisX.Card;
                int[] pulsx = new int[points.Length];
                int[] pulsy = new int[points.Length];

                for (int i = 0; i < points.Length; i++)
                {
                    pulsx[i] = this.AxisX.ConvertPos2Card(points[i].X);
                    pulsy[i] = this.AxisY.ConvertPos2Card(points[i].Y);
                }

                return card.Executor.Cmp2dData(card.CardId, chn, (short)points.Length, pulsx, pulsy, 0);
            }
            catch (Exception)
            {
                return -10;
            }
        }

        /// <summary>
        /// 数据压入fifo可选
        /// </summary>
        /// <param name="chn"></param>
        /// <param name="points"></param>
        /// <param name="fifo"></param>
        /// <returns></returns>
        public short Cmp2dData(short chn, PointD[] points, short fifo)
        {
            try
            {
                Card card = this.AxisX.Card;
                int[] pulsx = new int[points.Length];
                int[] pulsy = new int[points.Length];

                for (int i = 0; i < points.Length; i++)
                {
                    pulsx[i] = this.AxisX.ConvertPos2Card(points[i].X);
                    pulsy[i] = this.AxisY.ConvertPos2Card(points[i].Y);
                }
                return card.Executor.Cmp2dData(card.CardId, chn, (short)points.Length, pulsx, pulsy, fifo);
            }
            catch (Exception)
            {
                return -10;
            }
        }

        public short Cmp2dStart(short chn)
        {
            Card card = this.AxisX.Card;
            //start 2d compare
            return card.Executor.Cmp2dStart(card.CardId, chn);
        }

        public short Cmp2dStop(short chn)
        {
            Card card = this.AxisX.Card;
            return card.Executor.Cmp2dStop(card.CardId, chn);
        }

        #endregion


        #region unblocking command

        public ICommandable Servo(bool b, params Axis[] axes)
        {
            CommandServo command = new CommandServo(axes, b);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeX()
        {
            CommandMoveHome command = new CommandMoveHome(this.AxisX, this.HomePrm.HomePrmX);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeY()
        {
            CommandMoveHome command = new CommandMoveHome(this.AxisY, this.HomePrm.HomePrmY);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeZ()
        {
            this.AxisZ.ClrSts();
            this.AxisZ.ZeroPos();

            ICommandable command;
            // 部分机器Z轴行程太短，用固定距离可能超限位
            if (this.AxisZ.IsPLmt.Value)
            {
                //escape z PLmt;
                command = new CommandEscapeLmt(this.AxisZ, this.HomePrm.EscapeLmtPrmZ);
                this.Fire(command);
                //this.MoveIncZ(-2, 50);
            }
            //move home z
            command = new CommandMoveHome(this.AxisZ, this.HomePrm.HomePrmZ);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeR()
        {
            CommandMoveHome command = new CommandMoveHome(this.AxisR, this.HomePrm.HomePrmR);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeU()
        {
            CommandMoveHomeU command = new CommandMoveHomeU(this.AxisU, this.HomePrm.HomePrmU);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeXY()
        {
            //move home xy
            if (this.AxisX.IsNLmt.Value)
            {
                this.MoveIncX(50, 50);
            }
            if (this.AxisY.IsNLmt.Value)
            {
                this.MoveIncY(50, 50);
            }
            CommandMoveHome command = new CommandMoveHome(this.AxesXY, new MoveHomePrm[] { this.HomePrm.HomePrmX, this.HomePrm.HomePrmY });
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeXYR()
        {
            //move home xy
            if (this.AxisX.IsNLmt.Value)
            {
                this.MoveIncX(50, 50);
            }
            if (this.AxisY.IsNLmt.Value)
            {
                this.MoveIncY(50, 50);
            }
            CommandMoveHome command = new CommandMoveHome(this.AxesXYR, new MoveHomePrm[] { this.HomePrm.HomePrmX, this.HomePrm.HomePrmY, this.HomePrm.HomePrmR });
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeA()
        {
            CommandMoveHome command = new CommandMoveHome(this.AxisA, this.HomePrm.HomePrmA);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeB()
        {
            CommandMoveHome command = new CommandMoveHome(this.AxisB, this.HomePrm.HomePrmB);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeAB()
        {
            //move home AB
            if (this.AxisA.IsNLmt.Value)
            {
                this.MoveIncA(5, 5);
            }
            if (this.AxisB.IsNLmt.Value)
            {
                this.MoveIncB(5, 5);
            }
            CommandMoveHome command = new CommandMoveHome(this.AxesAB, new MoveHomePrm[] { this.HomePrm.HomePrmA, this.HomePrm.HomePrmB });
            this.Fire(command);
            return command;
        }

        public ICommandable MoveHomeXYAB()
        {
            //move home XYAB

            if (this.AxisX.IsNLmt.Value)
            {
                this.MoveIncX(50, 50);
            }
            if (this.AxisY.IsNLmt.Value)
            {
                this.MoveIncY(50, 50);
            }
            if (this.AxisA.IsPLmt.Value)
            {
                this.MoveIncA(-5, 5);
            }
            if (this.AxisB.IsPLmt.Value)
            {
                this.MoveIncB(-5, 5);
            }
            CommandMoveHome command = new CommandMoveHome(this.AxesXYAB, new MoveHomePrm[] { this.HomePrm.HomePrmX, this.HomePrm.HomePrmY, this.HomePrm.HomePrmA, this.HomePrm.HomePrmB });
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosX(double pos)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = this.DefaultPrm.VelXY,
                Acc = this.DefaultPrm.AccXY,
                Dec = this.DefaultPrm.AccXY
            };
            CommandMovePos command = new CommandMovePos(this.AxisX, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosY(double pos)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = this.DefaultPrm.VelXY,
                Acc = this.DefaultPrm.AccXY,
                Dec = this.DefaultPrm.AccXY
            };
            CommandMovePos command = new CommandMovePos(this.AxisY, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosA(double pos)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = this.DefaultPrm.VelAB,
                Acc = this.DefaultPrm.AccAB,
                Dec = this.DefaultPrm.AccAB
            };
            CommandMovePos command = new CommandMovePos(this.AxisA, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosB(double pos)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = this.DefaultPrm.VelAB,
                Acc = this.DefaultPrm.AccAB,
                Dec = this.DefaultPrm.AccAB
            };
            CommandMovePos command = new CommandMovePos(this.AxisB, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosXYR(double posX, double posY,double angleA)
        {
            return this.MoveLnXYR(posX, posY,angleA);
        }

        public ICommandable MovePosXYR(double posX, double posY, double angleA, double vel, double acc)
        {
            return this.MoveLnXYR(posX, posY, angleA, vel, acc);
        }

        public ICommandable MovePosXY(double posX, double posY)
        {
            return this.MoveLnXY(posX, posY);

            //MovePosPrm prmX = new MovePosPrm()
            //{
            //    Pos = posX,
            //    Vel = this.DefaultPrm.VelXY,
            //    Acc = this.DefaultPrm.AccXY,
            //    Dec = this.DefaultPrm.AccXY
            //};
            //MovePosPrm prmY = new MovePosPrm()
            //{
            //    Pos = posY,
            //    Vel = this.DefaultPrm.VelXY,
            //    Acc = this.DefaultPrm.AccXY,
            //    Dec = this.DefaultPrm.AccXY
            //};
            //CommandMovePos command = new CommandMovePos(new Axis[] { this.AxisX, this.AxisY }, new MovePosPrm[] { prmX, prmY });
            //this.Fire(command);
            //return command;
        }

        public ICommandable MovePosXY(PointD pos)
        {
            return this.MovePosXY(pos.X, pos.Y);
        }

        public ICommandable MovePosAB(double posA, double posB)
        {
            return this.MoveLnAB(posA, posB);
        }

        public ICommandable MovePosAB(PointD pos)
        {
            return this.MovePosAB(pos.X, pos.Y);
        }

        public ICommandable MovePosXYAB(double posX, double posY, double posA, double posB, int cardType)
        {
            return this.MoveLnXYAB(posX, posY, posA, posB, cardType);
        }

        public ICommandable MovePosXYAB(PointD posXY, PointD posAB, int cardType)
        {
            return this.MovePosXYAB(posXY.X, posXY.Y, posAB.X, posAB.Y, cardType);
        }

        public ICommandable MovePosZ(double pos, double vel, double acc)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = vel,
                Acc = acc,
                Dec = acc,
            };
            CommandMovePos command = new CommandMovePos(this.AxisZ, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosZ(double pos)
        {
            return this.MovePosZ(pos, this.DefaultPrm.VelZ, this.AxisZ.Prm.Acc);
        }

        public ICommandable MoveSafeZ()
        {
            return this.MovePosZ(this.CalibPrm.SafeZ);
        }

        public ICommandable MovePosR(double angle, double vel, double acc)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = angle,
                Vel = vel,
                Acc = acc,
                Dec = acc,
            };
            CommandMovePos command = new CommandMovePos(this.AxisR, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosR(double angle, double vel)
        {
            return this.MovePosR(angle, vel, this.AxisR.Prm.Acc);
        }

        public ICommandable MovePosR(double angle)
        {
            return this.MovePosR(angle, this.DefaultPrm.VelR);
        }

        public ICommandable MovePosU(double angle, double vel, double acc)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = angle,
                Vel = vel,
                Acc = acc,
                Dec = acc,
            };
            CommandMovePos command = new CommandMovePos(this.AxisU, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MovePosU(double angle, double vel)
        {
            return this.MovePosU(angle, vel, this.AxisU.Prm.Acc);
        }

        public ICommandable MovePosU(double angle)
        {
            return this.MovePosU(angle, this.DefaultPrm.VelU);
        }

        public ICommandable MoveIncX(double inc, double vel)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = this.DefaultPrm.AccXY,
                Dec = this.DefaultPrm.AccXY
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisX, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncX(double inc, double vel, double acc)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = acc,
                Dec = acc
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisX, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncY(double inc, double vel)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = this.DefaultPrm.AccXY,
                Dec = this.DefaultPrm.AccXY
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisY, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncY(double inc, double vel, double acc)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = acc,
                Dec = acc
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisY, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncA(double inc, double vel)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = this.DefaultPrm.AccXYAB,
                Dec = this.DefaultPrm.AccXYAB
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisA, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncA(double inc, double vel, double acc)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = acc,
                Dec = acc
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisA, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncB(double inc, double vel)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = this.DefaultPrm.AccXYAB,
                Dec = this.DefaultPrm.AccXYAB
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisB, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncB(double inc, double vel, double acc)
        {
            MovePosPrm prm = new Command.MovePosPrm()
            {
                Pos = inc,
                Vel = vel,
                Acc = acc,
                Dec = acc
            };
            CommandMoveInc command = new CommandMoveInc(this.AxisB, prm);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveIncX(double inc)
        {
            return this.MoveIncX(inc, this.DefaultPrm.VelXY);
        }

        public ICommandable MoveIncY(double inc)
        {
            return this.MoveIncY(inc, this.DefaultPrm.VelXY);
        }

        public ICommandable MoveIncA(double inc)
        {
            return this.MoveIncA(inc, this.DefaultPrm.VelAB);
        }

        public ICommandable MoveIncB(double inc)
        {
            return this.MoveIncB(inc, this.DefaultPrm.VelAB);
        }

        public ICommandable MoveIncXY(double incX, double incY)
        {
            return this.MovePosXY(this.PosX + incX, this.PosY + incY);
        }

        public ICommandable MoveIncXY(PointD inc)
        {
            return this.MoveIncXY(inc.X, inc.Y);
        }

        public ICommandable MoveIncXY(PointD inc, double vel, double acc)
        {
            return this.MoveLnXY(this.PosXY + inc, vel, acc);
        }

        public ICommandable MoveIncAB(double incX, double incY)
        {
            return this.MovePosAB(this.PosA + incX, this.PosB + incY);
        }

        public ICommandable MoveIncAB(PointD inc)
        {
            return this.MoveIncAB(inc.X, inc.Y);
        }

        public ICommandable MoveIncZ(double inc, double vel, double acc)
        {
            return this.MovePosZ(this.PosZ + inc, vel, acc);
        }

        public ICommandable MoveIncZ(double inc, double vel)
        {
            return this.MoveIncZ(inc, vel, this.AxisZ.Prm.Acc);
        }

        public ICommandable MoveIncZ(double inc)
        {
            return this.MoveIncZ(inc, this.DefaultPrm.VelZ);
        }

        public ICommandable MoveIncR(double angle, double vel, double acc)
        {
            return this.MovePosR(this.PosR + angle, vel, acc);
        }

        public ICommandable MoveIncR(double angle)
        {
            return this.MovePosR(this.PosR + angle);
        }

        public ICommandable MoveIncU(double angle, double vel, double acc)
        {
            return this.MovePosU(this.PosU + angle, vel, acc);
        }

        public ICommandable MoveIncU(double angle)
        {
            return this.MovePosU(this.PosU + angle);
        }

        public ICommandable MoveLnXYR(double endPosX, double endPosY,double angleA, double vel, double acc)
        {
            CrdLnXYR crd = new CrdLnXYR()
            {
                EndPosX = endPosX,
                EndPosY = endPosY,
                EndPosR = angleA,
                Vel = vel,
                Acc = acc,
                VelEnd = 0
            };

            CommandMoveTrc3Axis command = new CommandMoveTrc3Axis(this.AxisX, this.AxisY, this.AxisR, this.TrcPrm4Axis, crd, 0)
            {
                EnableINP = this.DefaultPrm.EnableINP
            };
            this.Fire(command);
            return command;
        }

        public ICommandable MoveLnXY(double endPosX, double endPosY, double vel, double acc)
        {
            CrdLnXY crd = new CrdLnXY()
            {
                EndPosX = endPosX,
                EndPosY = endPosY,
                Vel = vel,
                Acc = acc,
                VelEnd = 0
            };

            CommandMoveTrc command = new CommandMoveTrc(this.AxisX, this.AxisY, this.TrcPrm, crd)
            {
                EnableINP = this.DefaultPrm.EnableINP
            };
            this.Fire(command);
            return command;
        }

        public ICommandable MoveLnXY(double endPosX, double endPosY)
        {
            return this.MoveLnXY(endPosX, endPosY, this.DefaultPrm.VelXY, this.DefaultPrm.AccXY);
        }

        public ICommandable MoveLnXYR(double endPosX, double endPosY,double angleA)
        {
            return this.MoveLnXYR(endPosX, endPosY,angleA, this.DefaultPrm.VelXY, this.DefaultPrm.AccXY);
        }

        public ICommandable MoveLnXY(PointD pos, double vel, double acc)
        {
            return this.MoveLnXY(pos.X, pos.Y, vel, acc);
        }

        public ICommandable MoveLnAB(double endPosA, double endPosB, double vel, double acc)
        {
            if((this.AxisA == null) || (this.AxisB == null))  //副阀跟随时无AB
            {
                return null;
            }
            CrdLnXY crd = new CrdLnXY()
            {
                EndPosX = endPosA,
                EndPosY = endPosB,
                Vel = vel,
                Acc = acc,
                VelEnd = 0
            };

            CommandMoveTrc command = new CommandMoveTrc(this.AxisA, this.AxisB, this.TrcPrm, crd)
            {
                EnableINP = this.DefaultPrm.EnableINP
            };
            this.Fire(command);
            return command;
        }

        public ICommandable MoveLnAB(double endPosA, double endPosB)
        {
            return this.MoveLnAB(endPosA, endPosB, this.DefaultPrm.VelAB, this.DefaultPrm.AccAB);
        }

        public ICommandable MoveLnAB(PointD pos, double vel, double acc)
        {
            return this.MoveLnAB(pos.X, pos.Y, vel, acc);
        }

        public ICommandable MoveLnXYAB(double endPosX, double endPosY, double endPosA, double endPosB, double vel, double acc, int cardType)
        {
            CrdLnXYAB crd = new CrdLnXYAB()
            {
                EndPosX = endPosX,
                EndPosY = endPosY,
                EndPosA = endPosA,
                EndPosB = endPosB,
                Vel = vel,
                Acc = acc,
                VelEnd = 0
            };

            CommandMoveTrc4Axis command = new CommandMoveTrc4Axis(this.AxisX, this.AxisY, this.AxisA, this.AxisB, this.TrcPrm4Axis, crd, cardType)
            {
                EnableINP = this.DefaultPrm.EnableINP
            };
            this.Fire(command);
            return command;
        }

        public ICommandable MoveLnXYAB(double endPosX, double endPosY, double endPosA, double endPosB, int cardType)
        {
            return this.MoveLnXYAB(endPosX, endPosY, endPosA, endPosB, this.DefaultPrm.VelXYAB, this.DefaultPrm.AccXYAB, cardType);
        }

        public ICommandable MoveLnXYAB(PointD posXY, PointD posAB, double vel, double acc, int cardType)
        {
            return this.MoveLnXYAB(posXY.X, posXY.Y, posAB.X, posAB.Y, vel, acc, cardType);
        }

        public ICommandable MoveArc(double endPosX, double endPosY, double centerX, double centerY, short clockwize, double vel, double acc, double velEnd)
        {
            CrdArcXYC crd = new CrdArcXYC()
            {
                EndPosX = endPosX,
                EndPosY = endPosY,
                CenterX = centerX,
                CenterY = centerY,
                Clockwise = clockwize,
                Vel = vel,
                Acc = acc,
                VelEnd = velEnd
            };
            CommandMoveTrc command = new CommandMoveTrc(this.AxisX, this.AxisY, this.TrcPrm, crd);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveZOnDI(double vel, int diKey, StsType stsType)
        {
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = -200,
                Vel = vel,
                Acc = this.AxisZ.Prm.Acc,
                Dec = this.AxisZ.Prm.Dec
            };
            DI d = DIMgr.Instance.FindBy(diKey);
            CommandMoveOnDI command = new CommandMoveOnDI(this.AxisZ, prm, d, stsType);
            this.Fire(command);
            return command;
        }

        public ICommandable MoveToLoc(Location loc)
        {
            if (loc.Z < this.CalibPrm.SafeZ)
            {
                this.MoveSafeZ();
                this.MovePosXY(loc.X, loc.Y);
                return this.MovePosZ(loc.Z);
            }
            else
            {
                this.MovePosZ(loc.Z);
                return this.MovePosXY(loc.X, loc.Y);
            }
        }

        /// <summary>
        /// 飞拍底层插补指令生成
        /// </summary>
        /// <param name="crdPoints">传入的坐标点必须是已排序并修正为一条直线的</param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="flyCornerSpeed"></param>
        /// <returns></returns>
        public Result MoveFlyMarksPos(List<List<PointD>> crdPoints, double vel, double acc, double flyCornerSpeed)
        {
            List<ICrdable> crdList = new List<ICrdable>();
            double crdSpeed = vel;
            bool isRowFly;
            VectorD dirVector = crdPoints[0][0] - crdPoints[0][1];
            if (Math.Abs(dirVector.X) > Math.Abs(dirVector.Y))
            {
                isRowFly = true;
            }
            else
            {
                isRowFly = false;
            }
            for (int i = 0; i < crdPoints.Count; i++)
            {
                for (int j = 0; j < crdPoints[i].Count; j++)
                {
                    if (j == 0/* || j == (crdPoints[i].Count-1)*/)
                    {
                        crdSpeed = flyCornerSpeed;
                    }
                    else
                    {
                        crdSpeed = vel;
                    }
                    ICrdable crd;
                    if (i != 0 && j == 0)
                    {
                        short clockWise;
                        PointD center;
                        if (isRowFly)
                        {
                            center = new PointD(crdPoints[i][j].X, (crdPoints[i - 1].Last().Y + crdPoints[i][0].Y) / 2);
                            if (i % 2 == 0)
                            {
                                clockWise = 0;
                            }
                            else
                            {
                                clockWise = 1;
                            }
                        }
                        else
                        {
                            center = new PointD((crdPoints[i - 1].Last().X + crdPoints[i][0].X) / 2, crdPoints[i][j].Y);
                            if (i % 2 == 0)
                            {
                                clockWise = 1;
                            }
                            else
                            {
                                clockWise = 0;
                            }
                        }
                        crd = new CrdArcXYC()
                        {
                            EndPosX = crdPoints[i][j].X,
                            EndPosY = crdPoints[i][j].Y,
                            CenterX = center.X - crdPoints[i - 1].Last().X,
                            CenterY = center.Y - crdPoints[i - 1].Last().Y,
                            Clockwise = clockWise,
                            Vel = crdSpeed,
                            Acc = acc,
                            VelEnd = crdSpeed
                        };
                    }
                    else
                    {
                        crd = new CrdLnXY()
                        {
                            EndPosX = crdPoints[i][j].X,
                            EndPosY = crdPoints[i][j].Y,
                            Vel = crdSpeed,
                            Acc = acc,
                            VelEnd = crdSpeed
                        };
                    }
                    crdList.Add(crd);
                }
            }
            short initLookCount = (short)(crdList.Count-10);
            if (crdList.Count >= 100)
            {
                initLookCount = 90;
            }
            InitLook iniLookPrm = new InitLook()
            {
                crd = this.TrcPrm.CsId,
                fifo = 0,
                Ltime = 8,
                Lmax = 1,
                n = initLookCount
            };
            CommandMoveTrcFly command = new CommandMoveTrcFly(this.AxisX, this.AxisY, this.TrcPrm, crdList, iniLookPrm)
            {
                EnableINP = this.DefaultPrm.EnableINP
            };
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        public void MoveStop()
        {
            SchedulerMotion.Instance.Notify(CmdMsgType.Stop, this);
        }

        #endregion


        #region reply command

        public Result MoveHomeZAndReply()
        {
            this.AxisZ.ClrSts();
            this.AxisZ.ZeroPos();

            ICommandable command;
            // 部分机器Z轴行程太短，用固定距离可能超限位
            if (this.AxisZ.IsPLmt.Value)
            {
                //escape z PLmt;
                command = new CommandEscapeLmt(this.AxisZ, this.HomePrm.EscapeLmtPrmZ);
                this.Fire(command);
                if (this.WaitCommandReply(command) != Result.OK)
                {
                    return Result.FAILED;
                }
            }

            //move home z
            command = new CommandMoveHome(this.AxisZ, this.HomePrm.HomePrmZ);
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        public Result MoveHomeAndReply()
        {
            this.IsHomeDone = false;
            if (IsSimulation)
            {
                return Result.OK;
            }

            //// 判断轴的报警状态，重新上电解除报警需要时间
            //bool flag = false;
            //foreach (var item in AxisMgr.Instance.FindAll())
            //{
            //    if (item.IsAlarm.Value)
            //    {
            //        flag = true;
            //        AlarmServer.Instance.Fire(item, AlarmInfoMotion.FatalMoveHomeAlarm);
            //    }
            //}
            //if (flag)
            //{
            //    this.IsHomeDone = false;
            //    return Result.FAILED;
            //}

            // 轴使能，CommandServo设置超时时间1s
            ICommandable command = null;
            switch (this.AxesStyle)
            {
                case RobotAxesStyle.XYZ:
                    command = new CommandServo(this.AxesXYZ, true);
                    break;
                case RobotAxesStyle.XYZAB:
                    if (this.EanbleAB)
                    {
                        command = new CommandServo(this.AxesXYABZ, true);
                    }
                    else
                    {
                        command = new CommandServo(this.AxesXYZ, true);
                    }
                    break;
                case RobotAxesStyle.XYZR:
                    command = new CommandServo(this.AxesXYZR, true);
                    break;
                case RobotAxesStyle.XYZU:
                    command = new CommandServo(this.AxesXYZU, true);
                    break;
                case RobotAxesStyle.XYZUV:
                    command = new CommandServo(this.AxesXYZU, true);
                    break;
            }
            this.Fire(command);
            if (!this.WaitCommandReply(command).IsOk)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoMotion.FatalServoOn);
                return Result.FAILED;
            }
            AlarmServer.Instance.RemoveAlarm(this, AlarmInfoMotion.FatalServoOn);

            // 执行Z回零动作
            if (!this.MoveHomeZAndReply().IsOk)
            {
                return Result.FAILED;
            }

            Result rtn = Result.OK;
            // 执行U回零动作
            if (this.AxesStyle == RobotAxesStyle.XYZU || this.AxesStyle == RobotAxesStyle.XYZUV)
            {
                rtn = this.WaitCommandReply(this.MoveHomeU());
            }

            // 执行XY回零动作
            switch (this.AxesStyle)
            {
                case RobotAxesStyle.XYZ:
                    rtn = this.WaitCommandReply(this.MoveHomeXY());
                    break;
                case RobotAxesStyle.XYZAB:
                    if (this.EanbleAB)
                    {
                        rtn = this.WaitCommandReply(this.MoveHomeXYAB());
                    }
                    else
                    {
                        rtn = this.WaitCommandReply(this.MoveHomeXY());
                    }
                    break;
                case RobotAxesStyle.XYZR:
                    rtn = this.WaitCommandReply(this.MoveHomeXYR());
                    break;
                case RobotAxesStyle.XYZU:
                    rtn = this.WaitCommandReply(this.MoveHomeXY());
                    break;
                case RobotAxesStyle.XYZUV:
                    rtn = this.WaitCommandReply(this.MoveHomeXY());
                    break;
            }
            this.IsHomeDone = rtn.IsOk;
            return rtn;
        }

        /// <summary>
        /// Z轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos">目标位置</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加速度</param>
        /// <returns></returns>
        public Result MovePosZAndReply(double pos, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosZ(pos, vel, acc));
        }

        /// <summary>
        /// Z轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos">目标位置</param>
        /// <param name="vel">速度</param>
        /// <returns></returns>
        public Result MovePosZAndReply(double pos, double vel)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.MovePosZAndReply(pos, vel, this.AxisZ.Prm.Acc);
        }

        /// <summary>
        /// Z轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos">位置</param>
        /// <returns></returns>
        public Result MovePosZAndReply(double pos)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.MovePosZAndReply(pos, this.DefaultPrm.VelZ, this.AxisZ.Prm.Acc);
        }

        /// <summary>
        ///  Z轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="tolerance">如果当前Z轴位置在tolerance范围内，则不运动直接返回OK</param>
        /// <returns></returns>
        public Result MovePosZByToleranceAndReply(double pos, double vel, double acc, double tolerance = 0.005)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            if (MathUtils.Compare(pos, this.PosZ, tolerance))
            {
                return Result.OK;
            }
            return this.MovePosZAndReply(pos, vel, acc);
        }

        /// <summary>
        /// Z轴移动到安全高度，堵塞等待结果
        /// </summary>
        /// <returns></returns>
        public Result MoveSafeZAndReply()
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            if (this.PosZ > this.CalibPrm.SafeZ - 0.005)
            {
                return Result.OK;
            }
            return this.MovePosZAndReply(this.CalibPrm.SafeZ);
        }

        public Result MoveMeasureHeightZAndReply()
        {
            if (IsSimulation)
            {
                return Result.OK;
            }          
            return this.MovePosZAndReply(this.CalibPrm.MeasureHeightZ);
        }
        public Result MoveToMarkZAndReply()
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.MovePosZAndReply(this.CalibPrm.MarkZ);
        }

        /// <summary>
        /// Z轴增量运动，堵塞等待结果
        /// </summary>
        /// <param name="inc">增量</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加速度</param>
        /// <returns></returns>
        public Result MoveIncZAndReply(double inc, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncZ(inc, vel, acc));
        }

        public Result MoveIncZAndReply(double inc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.MoveIncZAndReply(inc, this.DefaultPrm.VelZ, this.AxisZ.Prm.Acc);
        }

        public Result MovePosRAndReply(double angle)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosR(angle));
        }

        public Result MoveIncRAndReply(double angle)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncR(angle));
        }

        public Result MovePosUAndReply(double angle, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosU(angle, vel, acc));
        }

        public Result MoveIncUAndReply(double angle)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncU(angle));
        }

        /// <summary>
        /// XY轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos">目标位置</param>
        /// <param name="vel">合成速度</param>
        /// <returns></returns>
        public Result MovePosXYAndReply(PointD pos)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosXY(pos.X, pos.Y));
        }

        public Result MovePosXYAndReply(PointD pos, double vel)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveLnXY(pos.X, pos.Y, vel, this.DefaultPrm.AccXY));
        }

        public Result MovePosXYAndReply(double posX, double posY)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosXY(posX, posY));
        }

        public Result MovePosXYAndReply(PointD pos, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveLnXY(pos.X, pos.Y, vel, acc));
        }

        /// <summary>
        /// XYR点位运动，阻塞等待
        /// </summary>
        /// <param name="pos">XY坐标</param>
        /// <param name="angleA">A轴旋转角度</param>
        /// <returns></returns>
        public Result MovePosXYRAndReply(PointD pos, double angleA)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosXYR(pos.X, pos.Y, angleA));
        }

        public Result MovePosXYRAndReply(PointD pos, double angleA, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosXYR(pos.X, pos.Y, angleA, vel, acc));
        }

        /// <summary>
        /// AB轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos">目标位置</param>
        /// <param name="vel">合成速度</param>
        /// <returns></returns>
        public Result MovePosABAndReply(PointD pos)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosAB(pos.X, pos.Y));
        }

        public Result MovePosABAndReply(double posX, double posY)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosAB(posX, posY));
        }

        public Result MovePosABAndReply(PointD pos, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveLnAB(pos.X, pos.Y, vel, acc));
        }

        /// <summary>
        /// XYAB轴点位运动，堵塞等待结果
        /// </summary>
        /// <param name="pos">目标位置</param>
        /// <param name="vel">合成速度</param>
        /// <returns></returns>
        public Result MovePosXYABAndReply(PointD posXY, PointD posAB, int cardType)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosXYAB(posXY.X, posXY.Y, posAB.X, posAB.Y, cardType));
        }

        public Result MovePosXYABAndReply(double posX, double posY, double posA, double posB, int cardType)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MovePosXYAB(posX, posY, posA, posB, cardType));
        }

        public Result MovePosXYABAndReply(PointD posXY, PointD posAB, double vel, double acc, int cardType)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveLnXYAB(posXY.X, posXY.Y, posAB.X, posAB.Y, vel, acc, cardType));
        }

        /// <summary>
        /// XY轴增量运动，堵塞等待结果
        /// </summary>
        /// <param name="inc">目标增量</param>
        /// <param name="vel">合成速度</param>
        /// <returns></returns>
        public Result MoveIncXYAndReply(PointD inc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncXY(inc.X, inc.Y));
        }

        public Result MoveIncXYAndReply(PointD inc, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncXY(inc, vel, acc));
        }

        /// <summary>
        /// AB轴增量运动，堵塞等待结果
        /// </summary>
        /// <param name="inc">目标增量</param>
        /// <param name="vel">合成速度</param>
        /// <returns></returns>
        public Result MoveIncABAndReply(PointD inc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncAB(inc.X, inc.Y));
        }

        /// <summary>
        /// A轴增量运动，堵塞等待结果
        /// </summary>
        /// <param name="inc"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        public Result MoveIncAAndReply(double inc, double vel)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncA(inc, vel));
        }

        /// <summary>
        /// B轴增量运动，堵塞等待结果
        /// </summary>
        /// <param name="inc"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        public Result MoveIncBAndReply(double inc, double vel)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.MoveIncB(inc, vel));
        }

        /// <summary>
        /// 圆弧运动，堵塞等待结果
        /// </summary>
        /// <param name="endPos">目标位置</param>
        /// <param name="center">圆心位置</param>
        /// <param name="clockwize">0：顺时针，1：逆时针</param>
        /// <param name="vel">合成速度</param>
        /// <returns></returns>
        public Result MoveArcAndReply(PointD endPos, PointD center, short clockwize, double vel)
        {
            return this.MoveArcAndReply(endPos, center, clockwize, vel, this.DefaultPrm.AccXY);
        }

        public Result MoveArcAndReply(PointD endPos, PointD center, short clockwize, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }

            CrdArcXYC crd = new CrdArcXYC()
            {
                EndPosX = endPos.X,
                EndPosY = endPos.Y,
                CenterX = center.X - this.PosX,
                CenterY = center.Y - this.PosY,
                Clockwise = clockwize,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            CommandMoveTrc command = new CommandMoveTrc(this.AxisX, this.AxisY, this.TrcPrm, crd);
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        public Result MoveZOnDIAndReply(double vel, int diKey, StsType stsType)
        {
            return this.WaitCommandReply(this.MoveZOnDI(vel, diKey, stsType));
        }

        public Result MoveToLocAndReply(Location loc)
        {
            return this.WaitCommandReply(this.MoveToLoc(loc));
        }

        public Result UniformLineAndReply(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }

            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = lineStartPos.X,
                EndPosY = lineStartPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            CrdLnXY crdLn = new CrdLnXY()
            {
                EndPosX = lineEndPos.X,
                EndPosY = lineEndPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            List<ICrdable> crdList = new List<ICrdable>();
            crdList.Add(crdAcc);
            crdList.Add(crdLn);
            crdList.Add(crdDec);

            CommandMoveTrc command = new CommandMoveTrc(this.AxisX, this.AxisY, this.TrcPrm, crdList);
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        public Result UniformArcAndReply(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, double acc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }

            CrdLnXY crdAcc = new CrdLnXY()
            {
                EndPosX = arcStartPos.X,
                EndPosY = arcStartPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            CrdArcXYC crdArc = new CrdArcXYC()
            {
                EndPosX = arcEndPos.X,
                EndPosY = arcEndPos.Y,
                CenterX = center.X - arcStartPos.X,
                CenterY = center.Y - arcStartPos.Y,
                Clockwise = clockwize,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            CrdLnXY crdDec = new CrdLnXY()
            {
                EndPosX = decEndPos.X,
                EndPosY = decEndPos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };

            List<ICrdable> crdList = new List<ICrdable>();
            crdList.Add(crdAcc);
            crdList.Add(crdArc);
            crdList.Add(crdDec);

            CommandMoveTrc command = new CommandMoveTrc(this.AxisX, this.AxisY, this.TrcPrm, crdList);
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        /// <summary>
        /// 连续插补运动，支持刀向跟随
        /// 例如要保持圆弧刀向跟随，需在圆弧轨迹前加入CrdXYGear
        /// </summary>
        /// <param name="crdList"></param>
        /// <returns></returns>
        public Result MoveTrcXYReply(List<ICrdable> crdList)
        {
            CommandMoveTrc command = new CommandMoveTrc(this.AxisX, this.AxisY, this.TrcPrm, crdList);
            this.Fire(command);
            return this.WaitCommandReply(command);
        }

        public Result MoveAxisROnDIPosBack(double pos, int diKey, StsType stsType, out double angle)
        {
            double posRes = 0;
            Action getPos = () => { posRes = this.PosR; };
            MovePosPrm prm = new MovePosPrm()
            {
                Pos=pos,
                Vel=5,
                Acc=1,
                Dec=1
            };
            Result res = this.MoveAxisOnDIPosBack(this.AxisR, prm, diKey, stsType, getPos);
            angle = posRes;
            return res;


        }

        public Result MoveAxisYOnDIPosBack(double pos, int diKey, StsType stsType, out double posY)
        {
            double posRes = 0;
            Action getPos = () => { posRes = this.PosY; };
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = 5,
                Acc = 1,
                Dec = 1
            };
            Result res = this.MoveAxisOnDIPosBack(this.AxisY, prm, diKey, stsType, getPos);
            posY = posRes;
            return res;
        }

        public Result MoveAxisZOnDIPosBack(double pos, int diKey, StsType stsType, out double posZ)
        {
            double posRes = 0;
            Action getPos = () => { posRes = this.PosZ; };
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = 5,
                Acc = 1,
                Dec = 1
            };
            Result res = this.MoveAxisOnDIPosBack(this.AxisZ, prm, diKey, stsType, getPos);
            posZ = posRes;
            return res;
        }

        public Result MoveAxisOnDIPosBack(Axis axis, MovePosPrm prm, int diKey, StsType stsType, Action getPos)
        {            
            DI d = DIMgr.Instance.FindBy(diKey);
            CommandMoveOnDIPos command = new CommandMoveOnDIPos(axis, prm, d, stsType);
            command.ResultBack = getPos;
            this.Fire(command);
            return this.WaitCommandReply(command);           
        }

        #endregion


        #region BufMove

        public Result BufMoveAxis(Axis axis, double pos, double vel, double acc)
        {
            CrdBufMove crd = new CrdBufMove()
            {
                Axis = axis,
                Pos = pos,
                Vel = vel,
                Acc = acc,
                Mode = 1
            };
            this.BufFluidItems.Add(new BufFluidItem(crd));
            return Result.OK;
        }

        public Result BufMovePosZ(double pos, double vel, double acc)
        {
            if (Math.Abs(pos - this.PosZ) <= 0.005) return Result.OK;
            return this.BufMoveAxis(this.AxisZ, pos, vel, acc);
        }

        public Result BufMoveIncZ(double inc, double vel, double acc)
        {
            return this.BufMovePosZ(this.PosZ + inc, vel, acc);
        }

        public Result BufMoveSafeZ()
        {
            return this.BufMovePosZ(this.CalibPrm.SafeZ, this.DefaultPrm.VelZ, this.AxisZ.Prm.Acc);
        }

        public Result BufDelay(int ms)
        {
            CrdDelay crd = new CrdDelay()
            {
                DelayTime = (ushort)ms
            };
            this.BufFluidItems.Add(new BufFluidItem(crd));
            return Result.OK;
        }

        public Result BufMoveLnXY(PointD pos, double vel, double acc)
        {
            CrdLnXY crd = new CrdLnXY()
            {
                EndPosX = pos.X,
                EndPosY = pos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel,
            };
            this.BufFluidItems.Add(new BufFluidItem(crd));
            return Result.OK;
        }

        public Result BufFluidLnXY(PointD pos, double vel, double acc, List<PointD> points)
        {
            CrdLnXY crd = new CrdLnXY()
            {
                EndPosX = pos.X,
                EndPosY = pos.Y,
                Vel = vel,
                Acc = acc,
                VelEnd = vel,
            };
            this.BufFluidItems.Add(new BufFluidItem(crd, points));
            return Result.OK;
        }

        public Result BufMoveArcXY(PointD endPos, PointD center, short clockwize, double vel, double acc)
        {
            CrdArcXYC crd = new CrdArcXYC()
            {
                EndPosX = endPos.X,
                EndPosY = endPos.Y,
                CenterX = center.X - this.PosX,
                CenterY = center.Y - this.PosY,
                Clockwise = clockwize,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };
            this.BufFluidItems.Add(new BufFluidItem(crd));
            return Result.OK;
        }

        public Result BufFluidArcXY(PointD endPos, PointD center, short clockwize, double vel, double acc, List<PointD> points)
        {
            CrdArcXYC crd = new CrdArcXYC()
            {
                EndPosX = endPos.X,
                EndPosY = endPos.Y,
                CenterX = center.X - this.PosX,
                CenterY = center.Y - this.PosY,
                Clockwise = clockwize,
                Vel = vel,
                Acc = acc,
                VelEnd = vel
            };
            this.BufFluidItems.Add(new BufFluidItem(crd, points));
            return Result.OK;
        }

        public void UpdateBufItems()
        {
            if (this.BufFluidItems.Count == 0) return;
            for (int i = 0; i < this.BufFluidItems.Count - 1; i++)
            {
                BufFluidItem currFluidItem = this.BufFluidItems[i];
                BufFluidItem nextFluidItem = this.BufFluidItems[i + 1];
                if(currFluidItem.Crd is CrdLnXY && nextFluidItem.Crd is CrdLnXY)
                {
                    (currFluidItem.Crd as CrdLnXY).VelEnd = (nextFluidItem.Crd as CrdLnXY).Vel;
                }
                else if(currFluidItem.Crd is CrdLnXY)
                {
                    (currFluidItem.Crd as CrdLnXY).VelEnd = 0;
                }
            }
            BufFluidItem endFluidItem = this.BufFluidItems.Last();
            if(endFluidItem.Crd is CrdLnXY)
            {
                (endFluidItem.Crd as CrdLnXY).VelEnd = 0;
            }
        }

        #endregion


        #region manual move

        public bool ManualHighOrLow = false;
        public bool ManualIncOrJog = false;
        public double ManualIncValue = 0.1;

        public void ManualMove(Axis axis, bool posiOrNega, bool highOrLow)
        {
            double inc = this.ManualIncValue;
            double vel = highOrLow ? axis.Prm.MaxManualVel * axis.Prm.ManualHigh : axis.Prm.MaxManualVel * axis.Prm.ManualLow;
            double acc = this.DefaultPrm.ManulAccXY;
            if (!posiOrNega)
            {
                if (ManualIncOrJog)
                {
                    inc *= -1;
                }
                else
                {
                    vel *= -1;
                }
            }
            if (axis.Key != this.AxisZ.Key)
            {
                Result ret = Result.OK;
                ret = this.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return;
                }
            }

            if (axis.Key == this.AxisX.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncX(inc, vel, acc);
                }
                else
                {
                    this.AxisX.MoveJog(vel);
                }
            }
            else if (axis.Key == this.AxisY.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncY(inc, vel, acc);
                }
                else
                {
                    this.AxisY.MoveJog(vel);
                }
            }
            else if (axis.Key == this.AxisZ.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncZ(inc, vel, acc);
                }
                else
                {
                    this.AxisZ.MoveJog(vel);
                }
            }
            else if (this.AxesStyle == RobotAxesStyle.XYZR && axis.Key == this.AxisR?.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncR(inc, vel, acc);
                }
                else
                {
                    this.AxisR.MoveJog(vel);
                }
            }
            else if ((this.RobotIsXYZU || this.RobotIsXYZUV) && axis.Key == this.AxisU?.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncU(inc, vel, acc);
                }
                else
                {
                    this.AxisU.MoveJog(vel);
                }
            }
            else if (axis.Key == this.AxisA.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncA(inc, vel, acc);
                }
                else
                {
                    this.AxisA.MoveJog(vel);
                }
            }
            else if (axis.Key == this.AxisB.Key)
            {
                if (ManualIncOrJog)
                {
                    this.MoveIncB(inc, vel, acc);
                }
                else
                {
                    this.AxisB.MoveJog(vel);
                }
            }
        }

        public void ManualMoveStop(Axis axis)
        {
            if (!ManualIncOrJog)
            {
                axis.MoveSmoothStop();
            }
        }

        public ICommandable ManulMoveIncXY(PointD inc)
        {
            return this.MoveLnXY(this.PosXY + inc, this.DefaultPrm.ManualVelXY, this.DefaultPrm.ManulAccXY);
        }

        public Result ManualMoveIncXYAndReply(PointD inc)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.ManulMoveIncXY(inc));
        }

        public ICommandable ManualMovePosXY(double targetX, double targetY)
        {
            return this.MoveLnXY(new PointD(targetX, targetY), this.DefaultPrm.ManualVelXY, this.DefaultPrm.ManulAccXY);
        }

        public Result ManualMovePosXYAndReply(double posX, double posY)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.ManualMovePosXY(posX, posY));
        }

        public void ManualMovePosXY(PointD pos)
        {
            this.MoveLnXY(pos, this.DefaultPrm.ManualVelXY, this.DefaultPrm.ManulAccXY);
        }

        public Result ManualMovePosXYAndReply(PointD pos)
        {
            if (IsSimulation)
            {
                return Result.OK;
            }
            return this.WaitCommandReply(this.ManualMovePosXY(pos.X, pos.Y));
        }

        #endregion


        #region Strip Mapping

        private string pathCalibMapPrm = SettingsPath.PathMachine + "\\" + typeof(CalibMapPrm).Name;

        /// <summary>
        /// 保存棋盘校正参数
        /// </summary>
        /// <returns></returns>
        public bool SaveStripMapPrm()
        {
            return JsonUtil.Serialize<CalibMapPrm>(pathCalibMapPrm, this.CalibMapPrm);
        }

        /// <summary>
        /// 初始化棋盘校正（双线性）
        /// </summary>
        /// <returns></returns>
        public bool InitStripMap()
        {
            this.CalibMapPrm = JsonUtil.Deserialize<CalibMapPrm>(pathCalibMapPrm);
            if (this.CalibMapPrm == null)
            {
                this.CalibMapPrm = new CalibMapPrm();
                this.IsMapValid = false;
                return false;
            }
            CalibMap.creatNewClbMap(this.CalibMapPrm.ColNum, this.CalibMapPrm.RowNum, this.CalibMapPrm.Interval);
            foreach (var item in this.CalibMapPrm.Items)
            {
                CalibMap.addPointData(item.RealX, item.RealY, item.RobotX, item.RobotY, item.IndexX, item.IndexY);
            }
            this.IsMapValid = true;
            return true;
        }

        /// <summary>
        /// 棋盘坐标转机械坐标（双线性）
        /// </summary>
        /// <param name="p">棋盘位置</param>
        /// <returns></returns>
        public PointD MapToMachine(PointD p)
        {
            if (!this.IsMapValid || !this.DefaultPrm.EnableMap)
            {
                return p;
            }
            double x = p.X;
            double y = p.Y;
            CalibMap.mapToMach(p.X, p.Y, ref x, ref y);
            return new PointD(x, y);
        }

        /// <summary>
        /// 机械坐标转棋盘坐标（双线性）
        /// </summary>
        /// <param name="p">机械坐标</param>
        /// <returns></returns>
        public PointD MachineToMap(PointD p)
        {
            if (!this.IsMapValid || !this.DefaultPrm.EnableMap)
            {
                return p;
            }
            double x = p.X;
            double y = p.Y;
            CalibMap.mapToReal(p.X, p.Y, ref x, ref y);
            return new PointD(x, y);
        }

        /// <summary>
        /// 初始化棋盘校正（神经网络）
        /// </summary>
        public void InitRBF2D()
        {
            this.IsRBFValid = false;
            CalibNet.buildRBFnetwork(400);
            foreach (var item in this.CalibMapPrm.Items)
            {
                CalibNet.readinSample(item.RealX, item.RealY, item.RobotX - item.RealX, item.RobotY - item.RealY);
            }
            int rtn = CalibNet.startLearning();
            CalibNet.saveRBFprms();
            if (rtn == 0)
            {
                this.IsRBFValid = true;
            }
        }

        /// <summary>
        /// 机械坐标转网络坐标（神经网络）
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public PointD MachineToNet(PointD p)
        {
            if(this.IsRBFValid)
            {
                double outx = p.X;
                double outy = p.Y;
                CalibNet.calcCoordinate(p.X, p.Y, ref outx, ref outy, true);
                return new PointD(outx, outy);
            }
            return p;
        }

        /// <summary>
        /// 网络坐标转机械坐标（神经网络）
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public PointD NetToMachine(PointD p)
        {
            if(this.IsRBFValid)
            {
                double outx = p.X;
                double outy = p.Y;
                CalibNet.calcCoordinate(p.X, p.Y, ref outx, ref outy);
                return new PointD(outx, outy);
            }
            return p;
        }

        #endregion
    }
}
