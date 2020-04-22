using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DataStruct;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.DigitalGage
{
    public class DigitalGageEee:DigitalGagableCom,IDigitalGagable,IAlarmSenderable
    {
        public DigitalGageEee(EasySerialPort easySerialPort):base(easySerialPort)
        {

        }

        public object Obj => this;
        public string Name => this.GetType().Name;

        public Byte[] resArray;

        public int ReadHeight(out double height)
        {
            int replyCode = 0;
            Thread.Sleep(TimeSpan.FromMilliseconds(200));//读取延时,为了等待天平进入稳定状态
            height = 0;
            DateTime startTime = DateTime.Now;
            while (true)
            {
                replyCode = this.ReadTimes(5, out height);
                if (replyCode==0)
                {
                    break;
                }
                DateTime endTime = DateTime.Now;
                if (endTime.Subtract(startTime) > TimeSpan.FromMilliseconds(3000))
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.GageReadOutTime);
                    break;
                }
            }
            height = Math.Round(height,3);
            return replyCode;
        }
        public int ReadTimes(int readTimes,out double height)
        {
            
            if (readTimes < 5)
            {
                readTimes = 5;
            }
            double value;
            double maxValue =0;
            double minValue = 0;
            double tatolHeight=0;
            height = 0;
            int replyCode=0;
            for (int i=0;i< readTimes; i++)
            {
                Thread.Sleep(200);
                replyCode = Read(out value);
                if (replyCode == 0)
                {
                    if (minValue == 0)
                    {
                        minValue = maxValue = value;
                    }
                    else if (value < minValue)
                    {
                        minValue = value;
                    }
                    else if (value > maxValue)
                    {
                        maxValue = value;
                    }
                    tatolHeight += value;
                }
                else
                {
                    return replyCode;
                }
                
            }
            height=(tatolHeight - maxValue - minValue) / 3;
            return replyCode;
        }

        public int Read(out double value)
        {
            value = 0.0;
            if (this.EasySerialPort == null || !this.EasySerialPort.Connected)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                return -1;
            }
            try
            {
                ByteData reply = this.EasySerialPort.Reply;
                string replyStr = reply.ToString();
                //this.resArray = reply.Bytes.Take(9).ToArray();
                //数据解析  共
                value = double.Parse(replyStr);
                return 0;
            }
            catch
            {
                return 2;
            }
           
        }

    }
}
