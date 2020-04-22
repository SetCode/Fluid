using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DataStruct;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    public class ScalableMettler : ScalableCom, IScalable, IAlarmSenderable
    {
        public ScalableMettler(EasySerialPort easySerialPort)
            : base(easySerialPort)
        {
            this.EasySerialPort.isDelimiter = true;
        }

        public string ExternalCaliCmd => string.Empty;

        public string InternalCaliCmd => string.Empty;

        public string PrintCmd => "S\r\n";

        public string ResetCmd => string.Empty;

        public string TareCmd => "T\r\n";

        public string ZeroCmd => "Z\r\n";

        public string ZeroTareCombiCmd => string.Empty;

        //天平参数
        public ScalePrm Prm { get; set; }

        public ScalePrm PrmBackUp { get; set; }
        public Scale.Vendor Vendor => Scale.Vendor.Mettler;

        public object Obj => this;

        public string Name => this.GetType().Name;

        public ComCommunicationSts CommunicationOK { get;  private set; }

        public bool ExternalCali()
        {
            return false;
        }

        public bool InternalCali()
        {
            return false;
        }

        public int Print(TimeSpan timeout,int readTimes, out double value)
        {
            value = 0;
            if (this.EasySerialPort == null)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleState);
                this.CommunicationOK = ComCommunicationSts.ERROR;
                return -1;
            }
            double weightMin = 0;
            double weightMax = 0;
            double perValue = 0;
            double weightSum = 0;
            int nullCount = 0;
            int sCount = 0;
            int inComplete = 0;
            int readErr = 0;
            int ReadCnt = 0;
            ByteData reply = null;
            Stopwatch startTime = new Stopwatch();
            startTime.Start();
            if (readTimes < 3)
            {
                readTimes = 5;
            }
            while (true)
            {
                if (startTime.ElapsedMilliseconds > (timeout.TotalMilliseconds * (readTimes + 2)))
                {
                    inComplete = 3;
                    break;
                }
                reply = this.EasySerialPort.WriteAndGetReply(this.PrintCmd, TimeSpan.FromSeconds(1));
                if (reply == null)
                {
                    nullCount++;
                    perValue = 0;
                }
                else
                {
                    try
                    {
                        //"S S   200.0000 g"
                        string str = reply.ToString();
                        string s = str.Substring(0, 3);
                        if (s != "S S")
                        {
                            sCount++;
                            break;
                        }
                        //减去前面的S S的长度和后面g\r的长度
                        s = str.Substring(s.Length, str.Length - s.Length - 2).Trim();
                        perValue = double.Parse(s) * 1000;
                    }
                    catch
                    {
                        readErr++;
                    }
                }
                if (ReadCnt == 0)
                {
                    weightMax = perValue;
                    weightMin = perValue;
                }
                if (weightMax < perValue)
                {
                    weightMax = perValue;
                }
                if (weightMin > perValue)
                {
                    weightMin = perValue;
                }
                weightSum += perValue;
                ReadCnt++;
                if (ReadCnt == readTimes)
                {
                    value = (weightSum - weightMax - weightMin) / (ReadCnt - 2);
                    break;
                }
            }
            startTime.Stop();
            if (ReadCnt < readTimes) // 达到指定读取次数不报错
            {
                if (sCount > 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadOverrun);
                    return 2; //超重或者失重
                }
                else if (nullCount > 2)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadNull);
                    //this.CommunicationOK = false;
                    this.CommunicationOK = ComCommunicationSts.ERROR;
                    return -1;//空值 表示天平不稳定
                }
                else if (inComplete > 2)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadIncomplete);
                    return 1;//不完整的值 表示天平不稳定
                }
                else if (readErr > 2)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadIncomplete);
                    return 3;
                }
            }
            //this.CommunicationOK = true;
            this.CommunicationOK = ComCommunicationSts.OK;
            Log.Dprint("Read Weight:" + value);
            return 0;
        }

        public bool Reset()
        {
            return true;
        }

        public bool Tare()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            ByteData reply = this.EasySerialPort.WriteAndGetReply(this.TareCmd, TimeSpan.FromSeconds(1));
            if(reply == null)
            {
                return false;
            }

            try
            {
                return reply.ToString().Substring(0, 3) == "T S";
            }
            catch
            {
                return false;
            }
        }

        public bool Zero()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            ByteData reply = this.EasySerialPort.WriteAndGetReply(this.ZeroCmd, TimeSpan.FromSeconds(1));
            if(reply == null)
            {
                return false;
            }

            try
            {
                return reply.ToString().Substring(0, 3) == "Z A";
            }
            catch
            {
                return false;
            }
        }

        public bool ZeroTareCombi()
        {
            if(!this.Tare())
            {
                return false;
            }
            return this.Zero();
        }

        public bool CommunicationTest()
        {
            double value;
            return this.Print(TimeSpan.FromSeconds(1),5, out value) >= 0;
        }
        public int Print(TimeSpan timeout , out string value)
        {
            double WeightValue;
            this.Print(timeout, 5, out WeightValue);
            value = WeightValue.ToString();
            return 0;
        }

        public void Update()
        {
            if (!this.EasySerialPort.Connected)
            {
                //this.CommunicationOK = false;
                this.CommunicationOK = ComCommunicationSts.ERROR ;
            }
        }

        public void StopReadWeight()
        {
            throw new NotImplementedException();
        }
    }
}
