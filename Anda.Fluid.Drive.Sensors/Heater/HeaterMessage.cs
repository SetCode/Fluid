using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class HeaterMessage
    {
        private HeaterMsg content;
        private HeaterController receiver;
        private bool enable;
        private int chanelNo;
        /// <summary>
        /// 写入时调用
        /// </summary>
        /// <param name="content"></param>
        /// <param name="value"></param>
        /// <param name="receiver"></param>
        public HeaterMessage(HeaterMsg content,double value,int chanelNo, HeaterController receiver)
        {
            this.content = content;
            this.receiver = receiver;
            this.Value = value;
            this.chanelNo = chanelNo;
        }
        /// <summary>
        /// 获取温度时调用
        /// </summary>
        /// <param name="content"></param>
        /// <param name="receiver"></param>
        public HeaterMessage(HeaterMsg content,HeaterController receiver,int chanelNo)
        {
            this.content = content;
            this.receiver = receiver;
            this.chanelNo = chanelNo;
        }

        /// <summary>
        /// 设置温控器启动加热或停止加热时调用
        /// </summary>
        /// <param name="content"></param>
        /// <param name="receiver"></param>
        /// <param name="enable"></param>
        public HeaterMessage(HeaterMsg content,HeaterController receiver,bool enable, int chanelNo)
        {
            this.content = content;
            this.receiver = receiver;
            this.enable = enable;
            this.chanelNo = chanelNo;
        }

        public HeaterMsg Content => this.content;
        public double Value { get; set; }
        public HeaterController Receiver => this.receiver;

        public bool Enable => this.enable;

        public int ChanelNo => this.chanelNo;

        public HeaterMessage HandleWriteMessage()
        {
            this.Receiver.HandleMessage(this,this.Value);
            return this;
        }
        public HeaterMessage HandleReadMessage()
        {
            this.Receiver.HandleMessage(this);
            return this;
        }

    }
    public enum HeaterMsg
    {
        获取标准温度值,
        获取温度上限值,
        获取温度下限值,
        获取温度漂移值,

        设置标准温度值,
        设置温度上限值,
        设置温度下限值,
        设置温度漂移值,

        开始加热,
        停止加热
    }
}
