using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    public class CpkMove
    {        

        /// <summary>
        /// 移动到起点不下降
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="posZ"></param>
        /// <returns></returns>
        public static  Result moveToPos(double posX, double posY, double posZ)
        {
            double posZTem = posZ + 10;
            if (posZTem > -2)
            {
                posZTem = -2;
            }
            Result res = Result.OK;
            if (Math.Abs(Machine.Instance.Robot.AxisZ.Pos- posZTem)>2)
            {
                res = Machine.Instance.Robot.MovePosZAndReply(posZTem);
                if (!res.IsOk)
                {
                    return res;
                }
            }

            //res = Machine.Instance.Robot.MovePosXYAndReply(posX, posY);
            res= Machine.Instance.Robot.ManualMovePosXYAndReply(posX, posY);
            if (!res.IsOk)
            {
                return res;
            }
            return res;
        }

        /// <summary>
        /// 移动的起点
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="posZ"></param>
        /// <returns></returns>
        public static Result MoveToPosHigh(double posX, double posY)
        {
            Result res = Result.OK;
            double vel = CPKMgr.Instance.Prm.CPKVelXYH;
            res = Machine.Instance.Robot.MovePosXYAndReply(new PointD(posX, posY),vel);
            if (!res.IsOk)
            {
                return res;
            }            
            return res;

        }
      
        /// <summary>
        /// 移动到指定点  下降
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="posZ"></param>
        /// <returns></returns>

        public static Result moveToPosDown(double posX, double posY, double posZ)
        {
            double posZTem = posZ + 10;
            if (posZTem > -2)
            {
                posZTem = -2;
            }
            Result res = Result.OK;
            if (Math.Abs(Machine.Instance.Robot.AxisZ.Pos - posZTem) > 2)
            {
                res = Machine.Instance.Robot.MovePosZAndReply(posZTem);
                if (!res.IsOk)
                {
                    return res;
                }
            }                   
            res = Machine.Instance.Robot.ManualMovePosXYAndReply(posX, posY);
            if (!res.IsOk)
            {
                return res;
            }
            Axis axis = Machine.Instance.Robot.AxisX;
            double vel = axis.Prm.MaxManualVel * axis.Prm.ManualLow;
            res = Machine.Instance.Robot.MovePosZAndReply(posZ, vel);
            if (!res.IsOk)
            {                
                return res;
            }
            return res;

        }

        public static Result MoveToPosSlowly(double posX, double posY)
        {
            Result res = Result.OK;
            double  vel = CPKMgr.Instance.Prm.CPKVelXYL;           
            res = Machine.Instance.Robot.MovePosXYAndReply(new PointD(posX, posY), vel);
            if (!res.IsOk)
            {
                return res;
            }
            return res;
        }


        public static Result moveToSafe(double posXSafe, double posYSafe)
        {
            //Result res = Machine.Instance.Robot.MovePosXYAndReply(posXSafe, posYSafe);
            Result res = Machine.Instance.Robot.ManualMovePosXYAndReply(posXSafe, posYSafe);
            if (!res.IsOk)
            {                
                return res;
            }
            return res;
        }

        public static Result MoveZHigh(double pos)
        {
            Result res = Result.OK;
            Axis axisz = Machine.Instance.Robot.AxisZ;
            double vel = CPKMgr.Instance.Prm.CPKVelZH;
            res = Machine.Instance.Robot.MovePosZAndReply(pos);
            if (!res.IsOk)
            {
                return res;
            }
            return res;
            
        }

        public static Result MoveZSlowly(double posZ)
        {
            Result res = Result.OK; 
            double vel =  CPKMgr.Instance.Prm.CPKVelZL;          
            res = Machine.Instance.Robot.MovePosZAndReply(posZ, vel);
            if (!res.IsOk)
            {
                return res;
            }
            return res;
        }
        public static Result move4CpkZ(double posX, double posY, double posZ)
        {
            Result res = Result.OK;
            Axis axis = Machine.Instance.Robot.AxisZ;
            double vel = axis.Prm.MaxManualVel * axis.Prm.ManualHigh;
            res = Machine.Instance.Robot.MovePosZAndReply(posZ, vel);
            if (!res.IsOk)
            {               
                return res;
            }
            axis = Machine.Instance.Robot.AxisX;
            vel = axis.Prm.MaxManualVel * axis.Prm.ManualLow;
            res = Machine.Instance.Robot.MovePosXYAndReply(new PointD(posX, posY), vel);
            if (!res.IsOk)
            {               
                return res;
            }
            return res;
        }
    }
}
