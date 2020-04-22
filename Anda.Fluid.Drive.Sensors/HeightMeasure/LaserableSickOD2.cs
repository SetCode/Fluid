using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DataStruct;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Sensors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.HeightMeasure
{
    public class LaserableSickOD2 : LaserableCom, Ilaserable, IAlarmSenderable
    {
        private const byte STX = 0x02;
        private const byte ETX = 0x03;

        public LaserableSickOD2(EasySerialPort easySerialPort)
            : base(easySerialPort)
        {
            this.CmdReadValue = GetCmdReadValue();
        }

        public Laser.Vendor Vendor => Laser.Vendor.SickOD2;

        public string CmdReadValue { get; private set; }

        public double ErrorValue => 34.699; // 实际是34.7999，保留余量

        public object Obj => this;

        public string Name => this.GetType().Name;

        public ComCommunicationSts CommunicationOK { get; private set; }
        
        public static string GetCmdReadValue()
        {
            char charSTX = (char)0x02;
            char charETX = (char)0x03;
            StringBuilder builder = new StringBuilder();
            builder.Append(charSTX).Append("MEASURE").Append(charETX);
            return builder.ToString();
        }

        /// <summary>
        /// 读取测高值
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        /// <returns>0：成功|-1：通信错误|1：转换错误|2：超量程</returns>
        public int ReadValue(TimeSpan timeout, out double value)
        {
            value = 0;
            try
            {
                this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 9;//"28.0000"
                ByteData reply = this.EasySerialPort.WriteAndGetReply(CmdReadValue, timeout);
                if (reply == null)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserState);
                    this.CommunicationOK = ComCommunicationSts.ERROR;
                    Log.Dprint("laser return -1");
                    return -1;
                }

                this.CommunicationOK = ComCommunicationSts.OK;
                int len = reply.Bytes.Length;
                if(len < 2)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserRead);
                    Log.Dprint("laser return 1,error data:" + reply.ToString());
                    return 1;
                }
                //判断起始标志STX
                if (reply.Bytes[0] != STX)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserRead);
                    Log.Dprint("laser return 1,error data:" + reply.ToString());
                    return 1;
                }
                //结束标志ETX有时存在有时不存在
                byte[] data = null;
                if (reply.Bytes[len - 1] == ETX)
                {
                    data = new byte[len - 2];
                }
                else
                {
                    data = new byte[len - 1];
                }
                //转换结果数据
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = reply.Bytes[i + 1];
                }
                string dataString = Encoding.UTF8.GetString(data);
                Log.Print("laser height :" + dataString);
                double result = double.Parse(dataString);

                if (result >= this.ErrorValue)
                {
                    Log.Dprint("laser return 1,error data:" + reply.ToString());
                    return 2;
                }

                //取反是为了和基恩士激光尺统一
                value = -result;

                return 0;
            }
            catch(Exception ex)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserRead.AppendMsg(ex.Message));
                Log.Dprint("laser return 1");
                return 1;
            }
        }

        public void Update()
        {
            if (!this.EasySerialPort.Connected)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }
    }
}
