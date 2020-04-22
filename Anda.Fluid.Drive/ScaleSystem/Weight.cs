using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.DomainBase;
using System.ComponentModel;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Drive.ScaleSystem
{
    /* 称重单步骤：

        1、到清洁位，清洁喷嘴；
        2、到称重位，读取重量；
        3、清零读数；
        4、控制胶阀打指定次数胶；
        5、读重量，计算单点重量。
        
    */
    /*称重自动过程：
        1、执行单步的1~3步；
        2、获取拼版数、每个拼版打点数、每个拼版时间间隔参数，得到总的打点数，控制胶阀打胶；
        3、读重量，计算单点重量。
    */

    public class Weight : EntityBase<int>, IAlarmSenderable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prm"></param>
        public Weight(int key)
            : base(key)
        {
        }

        /// <summary>
        /// 称重参数
        /// </summary>
        public WeightPrm Prm { get; set; }

        [Browsable(false)]
        object IAlarmSenderable.Obj => this;
        [Browsable(false)]
        string IAlarmSenderable.Name => this.ToString();


        /// <summary>
        /// 加载称重参数
        /// </summary>
        /// <returns></returns>
        public bool LoadPrm()
        {
            this.Prm = JsonUtil.Deserialize<WeightPrm>(typeof(WeightPrm).Name);
            return this.Prm != null;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <returns></returns>
        public bool SavePrm()
        {
            return JsonUtil.Serialize<WeightPrm>(typeof(WeightPrm).Name, this.Prm);
        }

        /// <summary>
        /// 天平清零
        /// </summary>
        /// <returns></returns>
        public bool ZeroWeight()
        {
            if (Machine.Instance.Scale.Scalable.Zero())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 重启天平
        /// </summary>
        /// <returns></returns>
        public bool ResetScale()
        {
            if (Machine.Instance.Scale.Scalable.Reset())
            {
                Thread.Sleep(5000);//天平重启至少需要5秒以上
                return true;
            }
            return false;
        }

        /// <summary>
        /// 天平校准（内部校准，即“软校准”）
        /// </summary>
        /// <returns></returns>
        public bool CalibScale()
        {
            if (Machine.Instance.Scale.Scalable.InternalCali())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 校验重量是否超出标准重量范围
        /// </summary>
        /// <returns>false:超出，true:在范围内</returns>
        public bool CalibWeight()
        {
            if (this.Prm.StandardWeight == 0 && this.Prm.WeightOffset == 0)
            {
                return true;//表示客户没有输入校验标准重量和偏差
            }
            if (this.Prm.ReadWeight<this.Prm.StandardWeight * (1-this.Prm.WeightOffset / 100.0d)|| this.Prm.ReadWeight > this.Prm.StandardWeight * (1 + this.Prm.WeightOffset / 100.0d))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 去称重天平位置
        /// </summary>
        /// <returns></returns>
        public Result MoveToScaleLoc()
        {
            return Machine.Instance.Robot.MoveToLocAndReply(Machine.Instance.Robot.SystemLocations.ScaleLoc);
        }

        /// <summary>
        /// 模拟实际生产
        /// </summary>
        /// <param name="times">打多少次</param>
        /// <param name="dots">每次打多少点</param>
        /// <param name="interval">每次的时间间隔</param>
        /// <returns></returns>
        public bool Shot(int times, short dots, int interval)
        {
            for (int i = 0; i < times; i++)
            {
                Machine.Instance.Valve1.SprayCycleAndWait(dots);
                //Thread.Sleep(interval);
            }
            return true;
        }

        /// <summary>
        /// 读取重量
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public string ReadWeight(out double weight)
        {
            int replyCode = 0;
            Thread.Sleep(this.Prm.ReadDelay);//读取延时,为了等待天平进入稳定状态
            weight = 0;
            TimeSpan startTime = new TimeSpan(DateTime.Now.Ticks);
            while (true)
            {
                replyCode = this.ReadValueByTimes(new TimeSpan(0, 0, 0, this.Prm.SingleReadTimeOut), out weight, (ushort)this.Prm.ReadTimes);
                if (replyCode == 0)
                {
                    break;
                }
                TimeSpan endTime = new TimeSpan(DateTime.Now.Ticks);
                if (endTime.Subtract(startTime).TotalMilliseconds > Machine.Instance.Weight.Prm.StabilityTimeOut) //稳定读数超时
                {
                    AlarmServer.Instance.Fire(Machine.Instance.Weight, AlarmInfoWeight.StabilityTimeOutAlarm);
                    break;
                }
            }
            weight = Math.Round(weight, 3);
            return GetReplyCode(replyCode);
        }
        
        /// <summary>
        /// 计算单点重量
        /// </summary>
        public void CalSingleDotWeight(double weight, int dots)
        {
            this.Prm.SetSingleDotWeight(Math.Round(weight / dots, 3));
        }

        /// <summary>
        /// 通过指定次数读取重量
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        /// <param name="readTimes"></param>
        /// <returns></returns>
        private int ReadValueByTimes(TimeSpan timeout, out double value, ushort readTimes = 3)
        {
            value = 0;
            double minValue = 0;
            double maxValue = 0;
            double totalWeight = 0;
            int replyCode = 0;
            if (readTimes < 3)
            {
                replyCode = 4;
            }
            for (int i = 0; i < readTimes; i++)
            {
                replyCode = Machine.Instance.Scale.Scalable.Print(timeout, out value);
                if (replyCode == 0)
                {
                    if (minValue == 0)
                    {
                        minValue = maxValue = value;
                    }
                    else if (value > maxValue)
                    {
                        maxValue = value;
                    }
                    else if (value < minValue)
                    {
                        minValue = value;
                    }
                    totalWeight += value;
                }
                else
                {
                   return replyCode;
                }
            }
            value = (totalWeight - maxValue - minValue) / (readTimes - 2);
            return replyCode;
        }

        /// <summary>
        /// 自动运行称重 (模拟实际生产)
        /// </summary>
        public void AutoRunWeighing()
        {
            try
            {
                double weight = 0;
                Machine.Instance.Valve1.DoPurgeAndPrime();
                this.MoveToScaleLoc();
                this.ZeroWeight();
                this.Shot(this.Prm.Panels, (short)this.Prm.ShotDotsEachPanel, this.Prm.Interval);
                this.ReadWeight(out weight);
                this.Prm.SetReadWeight(weight);
                this.AddCumulativeWeight(weight);
                if (this.IsScaleCupOverFlow())//胶杯超重
                {
                    AlarmServer.Instance.Fire(this,AlarmInfoWeight.ScaleCupOverFlowAlarm);
                }
                this.CalSingleDotWeight(weight, this.GetTotalDots());
                Machine.Instance.Robot.MoveSafeZAndReply();
            }
            catch (Exception)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoWeight.WeightAutoRunAlarm);
            }
        }

        /// <summary>
        /// 增加累计重量
        /// </summary>
        /// <param name="weight"></param>
        private void AddCumulativeWeight(double weight)
        {
            this.Prm.CumulativeWeight += Math.Round(weight,1);
        }

        /// <summary>
        /// 胶杯是否超重
        /// </summary>
        /// <returns></returns>
        private bool IsScaleCupOverFlow()
        {
            return this.Prm.CumulativeWeight >= this.Prm.MaxCapacity * this.Prm.WarningStartPercentage * 0.01d;
        }

        /// <summary>
        /// 在模拟生产中，计算总共打点次数
        /// </summary>
        /// <returns></returns>
        private int GetTotalDots()
        {
            return this.Prm.ShotDotsEachPanel * this.Prm.Panels;
        }

        /// <summary>
        /// 获得读取重量过程的返回信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        private string GetReplyCode(int replyCode)
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
    }
}
