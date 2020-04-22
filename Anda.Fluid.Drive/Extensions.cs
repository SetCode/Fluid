using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.Locations;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive
{
    public static class Extensions
    {
        public static short Set(this DoType d, bool b)
        {
            DO o = DOMgr.Instance.FindBy((int)d);
            if(o == null)
            {
                return -1;
            }
            return o.Set(b);
        }

        public static short SetIfNot(this DoType d, bool b)
        {
            if(d.Sts().Value == b)
            {
                return 0;
            }
            return d.Set(b);
        }

        public static Sts Sts(this DoType d)
        {
            DO o = DOMgr.Instance.FindBy((int)d);
            if(o == null)
            {
                return new Infrastructure.Sts();
            }
            return o.Status;
        }

        public static Sts Sts(this DiType d)
        {
            DI i = DIMgr.Instance.FindBy((int)d);
            if(i == null)
            {
                return new Infrastructure.Sts();
            }
            return i.Status;
        }

        /// <summary>
        /// 相机坐标转换喷嘴坐标，只考虑相机偏移
        /// </summary>
        /// <param name="point"></param>
        /// <param name="valveType"></param>
        /// <returns></returns>
        public static PointD ByNeedleCamera(this PointD point, ValveType valveType)
        {
            PointD rtn = null;
            switch(valveType)
            {
                case ValveType.Valve1:
                    rtn = (point.ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleCamera1).ToMachine();
                    break;
                case ValveType.Valve2:

                    break;
            }
            return rtn;
        }

        /// <summary>
        /// 相机坐标 + 打胶偏移
        /// </summary>
        /// <param name="point"></param>
        /// <param name="valveType"></param>
        /// <returns></returns>
        public static PointD ByNeedleJet(this PointD point, ValveType valveType)
        {
            PointD rtn = null;
            switch (valveType)
            {
                case ValveType.Valve1:
                    rtn = (point.ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleJet1).ToMachine();
                    break;
                case ValveType.Valve2:

                    break;
            }
            return rtn;
        }

        /// <summary>
        /// 相机坐标转换喷嘴坐标
        /// </summary>
        /// <param name="point"></param>
        /// <param name="valveType"></param>
        /// <returns></returns>
        public static PointD ToNeedle(this PointD point, ValveType valveType)
        {
            PointD rtn = null;
            switch (valveType)
            {
                case ValveType.Valve1:
                    rtn = (point.ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleCamera1 - Machine.Instance.Robot.CalibPrm.NeedleJet1).ToPoint().ToMachine();
                    //rtn = new PointD(
                    //    point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X,
                    //    point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y);
                    break;
                case ValveType.Valve2:
                    if (Machine.Instance.Setting.DualValveMode != DualValveMode.跟随)
                    {
                        rtn = new PointD(
                        point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X + Math.Abs(Machine.Instance.Robot.PosA),
                        point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y + Math.Abs(Machine.Instance.Robot.PosB));
                    }
                    else
                    {
                        rtn = new PointD(
                        point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X,
                        point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y);
                    }
                    break;
            }
            return rtn;
        }
        /// <summary>
        /// 相机坐标转换倾斜喷嘴坐标
        /// </summary>
        /// <param name="point"></param>
        /// <param name="valveType"></param>
        /// <param name="tiltType"></param>
        /// <returns></returns>
        public static PointD ToNeedle(this PointD point, ValveType valveType,TiltType tiltType)
        {
            PointD rtn = null;
            if (valveType == ValveType.Valve1 || valveType == ValveType.Both)
            {
                if (tiltType == TiltType.NoTilt)
                {
                    return rtn = (point.ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleCamera1 - Machine.Instance.Robot.CalibPrm.NeedleJet1).ToPoint().ToMachine();
                }
                AngleHeightPosOffset curTiltCalibPrm = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Find(t => t.TiltType.Equals(tiltType));
                if (curTiltCalibPrm == null)
                {
                    return null;
                }
                rtn = (point.ToSystem() + curTiltCalibPrm.ValveCameraOffset - curTiltCalibPrm.DispenseOffset).ToPoint().ToMachine();

            }
            else
            {
                if (Machine.Instance.Setting.DualValveMode != DualValveMode.跟随)
                {
                    rtn = new PointD(
                          point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X + Math.Abs(Machine.Instance.Robot.PosA),
                          point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y + Math.Abs(Machine.Instance.Robot.PosB));
                }
                else
                {
                    rtn = new PointD(
                         point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X,
                         point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y);

                }
            }
            return rtn;
        }

        public static Location ToNeedle(this Location loc, ValveType valve)
        {
            Location rtn = null;
            switch (valve)
            {
                case ValveType.Valve1:
                    rtn = new Location()
                    {
                        X = loc.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X,
                        Y = loc.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y,
                        Z = loc.Z
                    };
                    break;
                case ValveType.Valve2:
                    if (Machine.Instance.Setting.DualValveMode != DualValveMode.跟随)
                    {
                        rtn = new Location()
                        {
                            X = loc.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X + Math.Abs(Machine.Instance.Robot.PosA),
                            Y = loc.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y + Math.Abs(Machine.Instance.Robot.PosB),
                            Z = loc.Z
                        };
                    }
                    else
                    {
                        rtn = new Location()
                        {
                            X = loc.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X,
                            Y = loc.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y,
                            Z = loc.Z
                        };
                    }
                    break;
            }
            return rtn;
        }

        public static PointD ToLaser(this PointD point)
        {
            return new PointD(point.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                point.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
        }

        public static PointD ToSystem(this PointD point)
        {
            return Machine.Instance.Robot.MachineToMap(point);
        }

        public static PointD ToMachine(this PointD point)
        {
            return Machine.Instance.Robot.MapToMachine(point);
        }

        public static double ToAxisRPos(this double needleAngle)
        {
            double delta = needleAngle - Machine.Instance.Robot.CalibPrm.AngleGap;
            return (Machine.Instance.Robot.CalibPrm.NeedleRotated +Machine.Instance.Robot.CalibPrm.Direct*delta);
        }

        public static PointD ToCamera(this PointD point, ValveType valveType)
        {
            PointD rtn = null;
            switch (valveType)
            {
                case ValveType.Valve1:
                    //rtn = (point.ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleCamera1 - Machine.Instance.Robot.CalibPrm.NeedleJet1).ToPoint().ToMachine();
                    rtn = (point.ToSystem() + Machine.Instance.Robot.CalibPrm.NeedleJet1 - Machine.Instance.Robot.CalibPrm.NeedleCamera1).ToPoint().ToMachine();
                    break;
                case ValveType.Valve2:
                    if (Machine.Instance.Setting.DualValveMode != DualValveMode.跟随)
                    {
                        // todo 待处理--不转棋盘坐标么？？？
                        rtn = new PointD(
                        point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera2.X - Machine.Instance.Robot.CalibPrm.NeedleJet2.X + Math.Abs(Machine.Instance.Robot.PosA),
                        point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y - Machine.Instance.Robot.CalibPrm.NeedleJet2.Y + Math.Abs(Machine.Instance.Robot.PosB));
                    }
                    else
                    {
                        // todo 待处理--不转棋盘坐标么？？？
                        rtn = new PointD(point.X - Machine.Instance.Robot.CalibPrm.NeedleCamera2.X + Machine.Instance.Robot.CalibPrm.NeedleJet2.X,
                                point.Y - Machine.Instance.Robot.CalibPrm.NeedleCamera2.Y + Machine.Instance.Robot.CalibPrm.NeedleJet2.Y);
                    }
                    break;
            }
            return new PointD();
        }
    }
}
