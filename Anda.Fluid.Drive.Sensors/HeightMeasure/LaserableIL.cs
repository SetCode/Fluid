using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DataStruct;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.HeightMeasure
{
    /// <summary>
    /// 基恩士IL测量类
    /// </summary>
    public class LaserableIL : LaserableCom, Ilaserable, IAlarmSenderable
    {
        public const string CMD_READ_VALUE = @"SR,00,038";
        public LaserableIL(EasySerialPort easySerialPort)
            : base(easySerialPort)
        {
        }

        public string CmdReadValue => CMD_READ_VALUE;

        /// <summary>
        /// 读取通讯模块与探测器的连接状态指令
        /// </summary>
        private string CmdConnectState => @"SR,00,195";

        /// <summary>
        /// 零点偏移指令
        /// </summary>
        private string CmdZeroOffset => @"SW,00,001";

        /// <summary>
        /// 取消零点偏移指令
        /// </summary>
        private string CmdCancelZeroOffset => @"SW,00,002";

        /// <summary>
        /// 零点操作转台指令，包括零点偏移和取消零点偏移
        /// </summary>
        private string CmdZeroState => @"SR,00,054";

        public Laser.Vendor Vendor => Laser.Vendor.IL;

        public double ErrorValue => -99.000; // 实际是-99.998，保留余量

        public object Obj => this;

        public string Name => this.GetType().Name;

        public ComCommunicationSts CommunicationOK { get; private set; }
                
        /// <summary>
        /// 读取测高值
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        /// <returns>0：成功|-1：通信错误|1：转换错误|2：超量程</returns>
        public int ReadValue(TimeSpan timeout, out double value)
        {
            value = 0;
            if (this.EasySerialPort == null)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserState);
                this.CommunicationOK = ComCommunicationSts.ERROR;
                Log.Dprint("laser return -1");
                return -1;
            }
            try
            {
                this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 18;//例如：SR,00,038,±**.***\r\n
                //读取测量值正确的返回格式：SR,ID编号，数据编号，±**.***CR LF
                ByteData reply = this.EasySerialPort.WriteAndGetReply(CmdReadValue + Environment.NewLine, timeout);
                if (reply == null)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserState);
                    this.CommunicationOK = ComCommunicationSts.ERROR;
                    Log.Dprint("laser return -1");
                    return -1;
                }

                this.CommunicationOK = ComCommunicationSts.OK;
                if (!DealWithReadValue(reply.ToString(), out value))//解析数据
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserRead);
                    Log.Dprint("laser return 1");
                    return 1;
                }

                if (value <= this.ErrorValue)
                {
                    Log.Dprint("laser return 2");
                    return 2;
                }
                Log.Dprint("laser height :" + value.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserRead.AppendMsg(ex.Message));
                Log.Dprint("laser return 2");
                return 1;
            }
        }

        /// <summary>
        /// 解析测量值
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="value">结果数据</param>
        /// <returns></returns>
        private bool DealWithReadValue(string data, out double value)
        {
            string[] strArr = data.ToString().Split(',');
            if (strArr[0] == "ER")
            {
                value = 0;
                return false;
            }
            //解析数据，获取结果
            string result = strArr[3].Substring(1, 6);//截除±
            double.TryParse(result, out value);
            if (strArr[3].Contains("-"))//如果为负数
            {
                value *= -1;
            }
            return true;
        }

        /// <summary>
        /// 读取通讯模块与探测器的连接状态
        /// </summary>
        /// <param name="timeout">超时</param>
        /// <param name="isConnected">是否连接</param>
        /// <returns></returns>
        public bool ReadConnectState(TimeSpan timeout)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            try
            {
                //例如 返回数据：SR,00,195,****\r\n  触发接收事件输入缓冲区为16个字节
                this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 16;
                //读取状态正确返回格式：****，將“*”表示為“0～9”的任意一個數字，
                ByteData reply = this.EasySerialPort.WriteAndGetReply(this.CmdConnectState + Environment.NewLine, timeout);
                if (reply == null || reply.ToString().Count() != 16)
                {
                    return false;
                }
                string[] splitArray = reply.ToString().Split(',');
                if (splitArray[0] == "ER")
                {
                    return false;
                }
                if (splitArray[3].Substring(0, 4) == "0001")//0001代表IL-030
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 零点偏移
        /// </summary>
        /// <param name="timeout">超时</param>
        /// <returns></returns>
        public bool ZeroOffset(TimeSpan timeout)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            try
            {
                // SW,00,001\CR\LF : 11个字节
                this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 11;
                ByteData reply = this.EasySerialPort.WriteAndGetReply(CmdZeroOffset + ",0" + Environment.NewLine, timeout);
                if (reply == null)
                {
                    return false;
                }
                if (string.Equals(reply.ToString().Substring(0, 9), CmdZeroOffset))
                {
                    reply = this.EasySerialPort.WriteAndGetReply(this.CmdZeroOffset + ",1" + Environment.NewLine, timeout);
                    if (reply == null)
                    {
                        return false;
                    }
                    if (string.Equals(reply.ToString().Substring(0, 9), CmdZeroOffset))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 取消零点偏移
        /// </summary>
        /// <param name="timeout">超时</param>
        /// <returns></returns>
        public bool CancelZeroOffset(TimeSpan timeout)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            try
            {
                // SW,00,002\CR\LF : 11个字节
                this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 11;
                ByteData reply = this.EasySerialPort.WriteAndGetReply(this.CmdCancelZeroOffset + ",0" + Environment.NewLine, timeout);
                if (reply == null)
                {
                    return false;
                }
                if (string.Equals(reply.ToString().Substring(0, 9), CmdCancelZeroOffset))
                {
                    reply = this.EasySerialPort.WriteAndGetReply(this.CmdCancelZeroOffset + ",1" + Environment.NewLine, timeout);
                    if (reply == null)
                    {
                        return false;
                    }
                    if (string.Equals(reply.ToString().Substring(0, 9), CmdCancelZeroOffset))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update()
        {
            if(!this.EasySerialPort.Connected)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }
    }
}
