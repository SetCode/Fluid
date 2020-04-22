using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System.Collections.Generic;

namespace Anda.Fluid.Drive
{
    public class Converter
    {
        /// <summary>
        /// 坐标转换： 以相机为中心 -> 以喷嘴为中心 (加上喷嘴中心与实际胶水位置的偏差)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        //public static PointD CameraToNeedle1(PointD point)
        //{
        //    return new PointD(
        //        point.X + Machine.Instance.Robot.CalibPrm.NeedleCamera1.X - Machine.Instance.Robot.CalibPrm.NeedleJet1.X,
        //        point.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y - Machine.Instance.Robot.CalibPrm.NeedleJet1.Y);
        //}

        //public static PointD Needle1ToCamera(PointD point)
        //{
        //    return new PointD(
        //        point.X - Machine.Instance.Robot.CalibPrm.NeedleCamera1.X + Machine.Instance.Robot.CalibPrm.NeedleJet1.X,
        //        point.Y - Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y + Machine.Instance.Robot.CalibPrm.NeedleJet1.Y);
        //}

        /// <summary>
        /// 坐标转换： 以相机为中心 -> 以激光测高为中心
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        //public static PointD CameraToLaser(PointD point)
        //{
        //    return new PointD(point.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
        //        point.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
        //}

        /// <summary>
        /// 坐标转换： Z轴坐标 -> 喷嘴距离基板高度
        /// </summary>
        /// <param name="currZ">当前Z轴坐标</param>
        /// <param name="boardHeight">激光测高测量的基板高度值</param>
        /// <returns></returns>
        public static double Z2NeedleBoard(double currZ, double boardHeight)
        {
            return currZ - Machine.Instance.Robot.CalibPrm.StandardZ - (boardHeight - Machine.Instance.Robot.CalibPrm.StandardHeight);
        }

        /// <summary>
        /// 坐标转换： 喷嘴距离基板高度 -> Z轴坐标
        /// </summary>
        /// <param name="needleBoard">距板高度</param>
        /// <param name="boardHeight">基板测高值</param>
        /// <returns></returns>
        public static double NeedleBoard2Z(double needleBoard, double boardHeight)
        {
            Log.Dprint("NeedleBoard2Z  needleBoard=" + needleBoard + ", boardHeight=" + boardHeight
                + ", StandardZ=" + Machine.Instance.Robot.CalibPrm.StandardZ + ", StandardHeight=" + Machine.Instance.Robot.CalibPrm.StandardHeight);
            return needleBoard + Machine.Instance.Robot.CalibPrm.StandardZ + (boardHeight - Machine.Instance.Robot.CalibPrm.StandardHeight);
        }

        /// <summary>
        /// 坐标转换（倾斜状态）： 喷嘴距离基板高度 -> Z轴坐标
        /// </summary>
        /// <param name="needleBoard">距板高度</param>
        /// <param name="boardHeight">基板测高值</param>
        /// <returns></returns>
        public static double NeedleBoard2Z(double needleBoard, double boardHeight,TiltType tiltType)
        {
            Log.Dprint("NeedleBoard2Z  needleBoard=" + needleBoard + ", boardHeight=" + boardHeight
                + ", StandardZ=" + Machine.Instance.Robot.CalibPrm.StandardZ + ", StandardHeight=" + Machine.Instance.Robot.CalibPrm.StandardHeight);
            if (tiltType == TiltType.NoTilt)
            {
                return needleBoard + Machine.Instance.Robot.CalibPrm.StandardZ + (boardHeight - Machine.Instance.Robot.CalibPrm.StandardHeight);
            }
            else
            {
                List<AngleHeightPosOffset> curTiltCalibPrms = Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.FindAll(t => t.TiltType.Equals(tiltType));
                if (curTiltCalibPrms.Count <= 0)
                {
                    return 0;
                }
                AngleHeightPosOffset curTiltCalibPrm = curTiltCalibPrms.Find(t => t.Gap.Equals(needleBoard));
                if (curTiltCalibPrm == null)
                {
                    return 0;
                }
                else
                {
                    if (curTiltCalibPrm.Gap <= 0.0001)
                    {
                        return 0;
                    }
                    else
                    {
                        return curTiltCalibPrm.Gap + curTiltCalibPrm.StandardZ + (boardHeight - Machine.Instance.Robot.CalibPrm.StandardHeight);
                    }
                }
            }
        }

        /// <summary>
        /// 运动加速度默认是pulse/s^2, 转成mm/s^2
        /// </summary>
        /// <param name="pulse"></param>
        /// <returns></returns>
        //public static double Pulse2Mm(double pulse)
        //{
        //    return pulse * 1000;
        //}

    }
}
