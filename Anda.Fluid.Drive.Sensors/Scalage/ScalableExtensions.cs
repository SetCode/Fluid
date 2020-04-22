using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    public static class ScalableExtensions
    {
        /// <summary>
        /// 重启天平
        /// </summary>
        /// <returns></returns>
        public static bool ResetScale( this IScalable scale)
        {
            if (scale.Reset())
            {
                Thread.Sleep(5000);//天平重启至少需要5秒以上
                return true;
            }
            return false;
        }

        /// <summary>
        /// 读取重量  
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static int ReadWeight(this IScalable scale, out double weight)
        {
            weight = 0;
            int replyCode = 0;
            //if (scale.Prm.ReadTimes*(scale.Prm.SingleReadTimeOut+ scale.Prm.SingleReadDelay)>= scale.Prm.StabilityTimeOut*0.9)
            //{
            //    AlarmServer.Instance.Fire(scale, AlarmInfoScale.ScalePrmSettingAlarm);
            //    return -1;
            //}            
            Thread.Sleep(scale.Prm.ReadDelay);//读取延时,为了等待天平进入稳定状态

            //TimeSpan startTime = new TimeSpan(DateTime.Now.Ticks);
            //while (true)
            //{
            //    replyCode = scale.ReadValueByTimes(new TimeSpan(0, 0, 0, 0,scale.Prm.SingleReadTimeOut), out weight, (ushort)scale.Prm.ReadTimes);
            //    //停止
            //    if (replyCode==4)
            //    {
            //        break;
            //    }
            //    if (replyCode == 0)//读取成功
            //    {
            //        break;
            //    }
            //    TimeSpan endTime = new TimeSpan(DateTime.Now.Ticks);
            //    if (endTime.Subtract(startTime).TotalMilliseconds > scale.Prm.StabilityTimeOut) //稳定读数超时
            //    {                    
            //        Logger.DEFAULT.Warn(LogCategory.MANUAL | LogCategory.RUNNING, scale.GetType().Name, "weight reading reaches stability time out");
            //        AlarmServer.Instance.Fire(scale,AlarmInfoScale.StabilityTimeOutAlarm );
            //        break;
            //    }
            //}
            replyCode = scale.ReadValueByTimes(new TimeSpan(0, 0, 0, 0, scale.Prm.SingleReadTimeOut), out weight, (ushort)scale.Prm.ReadTimes);
            weight = Math.Round(weight, 3);
            string msg = string.Format("Current value of scale is {0}", weight);
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, scale.GetType().Name, msg);
            //return GetReplyCode(replyCode);
            return replyCode;
        }


        /// <summary>
        /// 通过指定次数读取重量
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        /// <param name="readTimes"></param>
        /// <returns></returns>
        private static int ReadValueByTimes(this IScalable scale, TimeSpan timeout, out double value, ushort readTimes = 3)
        {
            value = 0;
            int replyCode = 0;
            if (readTimes < 3)
            {
                replyCode = 4;
            }
            replyCode = scale.Print(timeout,readTimes, out value);
            return replyCode;
        }

        /// <summary>
        /// 胶杯是否超重
        /// </summary>
        /// <returns></returns>
        public  static Result IsScaleCupOverFlow(this IScalable scale, double CumulativeWeight)
        {

            Result ret = Result.OK;

            if (CumulativeWeight >= scale.Prm.MaxCapacity * scale.Prm.WarningStartPercentage * 0.01d)
            {

                AlarmServer.Instance.Fire(scale, AlarmInfoScale.ScaleCupOverFlowAlarm);
                ret = Result.FAILED;
                
            }
            return ret;
        }

      

        /// <summary>
        /// 获得读取重量过程的返回信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        private static string GetReplyCode(int replyCode)
        {
            switch (replyCode)
            {
                case 0:
                    return "read success";
                case 1:
                    return "scale is not steady to read";
                case 2:
                    return "overload or weightloss";
                case 3:
                    return "process exception occurs";
                case 4:
                    return "read times less than 3";
                case -1:
                    return "serialport close";
                default:
                    break;
            }
            return "unknow code";
        }

        public static  bool LoadPrm(this IScalable scale)
        {
            string path = SettingsPath.PathMachine + "\\" + typeof(ScalePrm).Name;
            scale.Prm = JsonUtil.Deserialize<ScalePrm>(path);
            scale.PrmBackUp = JsonUtil.Deserialize<ScalePrm>(path);
            return scale.Prm != null;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <returns></returns>
        public static bool SavePrm(this IScalable scale)
        {
            string path = SettingsPath.PathMachine + "\\" + typeof(ScalePrm).Name;
            return JsonUtil.Serialize<ScalePrm>(path, scale.Prm);
        }


    }
   
}
