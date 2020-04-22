using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using System.Threading;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.DeviceType;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge
{
    internal class RTVPurge : IPurgable,IAlarmSenderable
    {
        public static AlarmInfo ErrorPurge => AlarmInfo.Create(AlarmLevel.Error, 800, "清洗", "清洗海绵需要更换", AlarmHandleType.DelayHandle);

        public string Name => "RTV清洗";

        public object Obj => this;

        public Result DoPurge(Valve valve)
        {
            Result ret = Result.OK;

            if (RTVPurgePrm.Instance.Lines.Count == 0
                || RTVPurgePrm.Instance.CurrLineIndex >= RTVPurgePrm.Instance.Lines.Count
                || RTVPurgePrm.Instance.CurrLineCycle > RTVPurgePrm.Instance.Cycles -1)  
                return Result.FAILED;

            ret = this.ExecuteLine(RTVPurgePrm.Instance.Lines[RTVPurgePrm.Instance.CurrLineIndex].Item1,
                         RTVPurgePrm.Instance.Lines[RTVPurgePrm.Instance.CurrLineIndex].Item2, RTVPurgePrm.Instance.posZ,
                         RTVPurgePrm.Instance.Vel, RTVPurgePrm.Instance.DispenseDelay);

            //如果当前清洗线的执行次数等于设定的次数，则要跳到下一条清洗线
            if (RTVPurgePrm.Instance.CurrLineCycle == RTVPurgePrm.Instance.Cycles -1)
            {
                RTVPurgePrm.Instance.CurrLineCycle = 0;

                //如果是最后一条清洗线，报警处理。
                if (RTVPurgePrm.Instance.CurrLineIndex == RTVPurgePrm.Instance.Lines.Count - 1)
                {
                    RTVPurgePrm.Instance.CurrLineIndex = 0;
                    AlarmServer.Instance.Fire(this, ErrorPurge);
                }
                else
                {
                    RTVPurgePrm.Instance.CurrLineIndex++;
                }
            }
            else
            {
                RTVPurgePrm.Instance.CurrLineCycle++;
            }

            RTVPurgePrm.Save();
            return ret;
        }

        private Result ExecuteLine(PointD start,PointD end,double posZ,double vel,int delay)
        {
            Result ret = Result.OK;
            ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
                return ret;

            ret = Machine.Instance.Robot.MovePosXYAndReply(start);
            if (!ret.IsOk)
                return ret;

            ret = Machine.Instance.Robot.MovePosZAndReply(posZ);
            if (!ret.IsOk)
                return ret;

            //开胶延时一定时间后开始移动
            Thread.Sleep(delay);

            ret = Machine.Instance.Robot.MovePosXYAndReply(end, vel);
            if (!ret.IsOk)
                return ret;

            ret = Machine.Instance.Robot.MoveSafeZAndReply();

            return ret;
        }
    }
}
