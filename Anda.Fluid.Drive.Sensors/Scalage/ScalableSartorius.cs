using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DataStruct;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Sensors;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.Trace;
using System.Threading;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    /// <summary>
    /// 赛多利斯天平类
    /// </summary>
    public class ScalableSartorius : ScalableCom, IScalable, IAlarmSenderable
    {
        public ScalableSartorius(EasySerialPort easySerialPort)
            : base(easySerialPort)
        {
            this.EasySerialPort.isDelimiter = true;
        }

        /// <summary>
        /// 外部校准
        /// </summary>
        public string ExternalCaliCmd => @"ESCW" + Environment.NewLine;

        /// <summary>
        /// 内部校准
        /// </summary>
        public string InternalCaliCmd => @"ESCZ" + Environment.NewLine;

        /// <summary>
        /// 打印输出指令
        /// </summary>
        public string PrintCmd => @"ESCP" + Environment.NewLine;

        /// <summary>
        /// 重启指令
        /// </summary>
        public string ResetCmd => @"ESCS" + Environment.NewLine;

        /// <summary>
        /// 去皮
        /// </summary>
        public string TareCmd => @"ESCU" + Environment.NewLine;

        /// <summary>
        /// 清零
        /// </summary>
        public string ZeroCmd => @"ESCV" + Environment.NewLine;

        /// <summary>
        /// 去皮清零组合
        /// </summary>
        public string ZeroTareCombiCmd => @"ESCT" + Environment.NewLine;


        //天平参数
        public ScalePrm Prm { get; set; }

        public ScalePrm PrmBackUp { get; set; }
        public Scale.Vendor Vendor => Scale.Vendor.Sartorius;

        public object Obj => this;

        public string Name => this.GetType().Name;

        public ComCommunicationSts CommunicationOK { get;  private set; }


        /// <summary>
        /// 外部校准
        /// </summary>
        /// <returns></returns>
        public bool ExternalCali()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Write(this.ExternalCaliCmd);
        }

        /// <summary>
        /// 内部校准
        /// </summary>
        /// <returns></returns>
        public bool InternalCali()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Write(this.InternalCaliCmd);
        }

        private int GetResult(string str,out double value)
        {
            value = 0;
            if (!str.Contains('\r') && str.Length != 21)
            {
                return 1;//不完整的值 表示天平不稳定
            }
            string id = str.Substring(0, 5).Replace(" ", "");//ID码             
            string result = str.Substring(6, 10).Replace(" ", "");//结果数据         
            string unit = str.Substring(17, 2).Replace(" ", "");//单位
            if (id.Contains("N") && unit.Contains("mg"))
            {
                double.TryParse(result, out value);
                Log.Print(value.ToString());
            }
            else if (id.Contains("S"))
            {
                return 2; //超重或者失重
            }
            return 0;//执行成功，获取正常值
        }

        /// <summary>
        /// 打印结果
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Print(TimeSpan timeout,int readTimes, out double value)
        {
            value = 0.0;
            if (this.EasySerialPort == null && !this.EasySerialPort.Connected)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleState);
                //this.CommunicationOK = false;
                this.CommunicationOK =ComCommunicationSts.ERROR;
                return -1;//串口为空或未打开
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
            if (readTimes < 3)
            {
                readTimes = 5;
            }
            List<double> results = new List<double>();
            double mean = 0;
            double variance = 0;
            double specifiedValue = 0;
            Stopwatch startTime = new Stopwatch();
            startTime.Start();
            //返回22个字节   N         0.0000 g  \CR\LF
            //this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 22;
            ByteData reply = null;
            while(true)
            {
                if (this.isStop)
                {
                    Logger.DEFAULT.Debug(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    this.isStop = false;
                    return 4;
                }
                perValue = 0;
                if (startTime.ElapsedMilliseconds > (timeout.TotalMilliseconds * (readTimes+2)))
                {
                    inComplete = 3;
                    break;
                }
                //每次读取串口前延时
                Thread.Sleep(this.Prm.SingleReadDelay);
                reply = this.EasySerialPort.WriteAndGetReply(this.PrintCmd, timeout);
                if (reply == null)
                {
                    nullCount++;
                    perValue = 0;
                    continue;
                }
                else//reply not null
                {
                    try
                    {
                        string str = reply.ToString();
                        int rtn = GetResult(str,out perValue);
                        Logger.DEFAULT.Debug("rtn:"+rtn+"  current Weight:" + perValue);
                        if (rtn == 2)//超重
                        {
                            sCount++;
                            break;
                        }
                        else if(rtn == 1)//不完整
                        {
                            inComplete++;
                            continue;
                        }
                        results.Add(perValue);
                        if (results.Count>=2)
                        {
                            mean=MathUtils.CalMean(results.ToArray());
                            variance = MathUtils.CalVariance(results.ToArray());
                            specifiedValue = mean * 0.1;
                            Logger.DEFAULT.Debug("variance:" + variance + "  specifiedValue:" + specifiedValue);
                            if (variance>specifiedValue)
                            {
                                results.Remove(perValue);
                                continue;
                            }
                        }
                        if (results.Count== readTimes)
                        {
                            value= MathUtils.CalMean(results.ToArray());
                            ReadCnt = results.Count;
                            break;
                        }
                        //if (ReadCnt == 0)
                        //{
                        //    weightMax = perValue;
                        //    weightMin = perValue;
                        //}
                        //if (weightMax < perValue)
                        //{
                        //    weightMax = perValue;
                        //}
                        //if (weightMin > perValue)
                        //{
                        //    weightMin = perValue;
                        //}
                        
                        //weightSum += perValue;
                        //ReadCnt++;
                        //if (ReadCnt == readTimes)
                        //{
                        //    value = (weightSum - weightMax - weightMin) / (ReadCnt - 2);
                        //    break;
                        //}

                    }
                    catch
                    {
                        readErr++;
                    }
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
            Logger.DEFAULT.Debug("Read Weight:" + value);
            return 0;
        }
        private Object lockObj = new object();
        private bool isStop = false;
        public void StopReadWeight()
        {
            lock (lockObj)
            {
                this.isStop = true;
                Logger.DEFAULT.Debug(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            
        }

        /// <summary>
        /// 重启
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Write(this.ResetCmd);
        }

        /// <summary>
        /// 去皮
        /// </summary>
        /// <returns></returns>
        public bool Tare()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Write(this.TareCmd);
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public bool Zero()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Write(this.ZeroCmd);
        }

        /// <summary>
        /// 去皮清零组合
        /// </summary>
        /// <returns></returns>
        public bool ZeroTareCombi()
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            return this.EasySerialPort.Write(this.ZeroTareCombiCmd);
        }

        /// <summary>
        /// 通讯检测
        /// </summary>
        /// <returns></returns>
        public bool CommunicationTest()
        {
            double value;
            return this.Print(TimeSpan.FromSeconds(0.5),5, out value) >= 0;
        }

        public int  Print(TimeSpan timeout,out string value)
        {
            value = string.Empty;

            if (this.EasySerialPort == null && !this.EasySerialPort.Connected)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleState);
                //this.CommunicationOK = false;
                this.CommunicationOK = ComCommunicationSts.ERROR;
                return -1;//串口为空或未打开
            }
            //返回22个字节   N         0.0000 g  \CR\LF
            //this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 22;
            ByteData reply = this.EasySerialPort.WriteAndGetReply(this.PrintCmd, timeout);
            if (reply == null)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadNull);
                //this.CommunicationOK = false;
                this.CommunicationOK = ComCommunicationSts.ERROR;
                return -1;//空值 表示天平不稳定
            }

            //this.CommunicationOK = true;
            this.CommunicationOK = ComCommunicationSts.OK;
            string data = reply.ToString();
            Debug.WriteLine(data);
            try
            {
                if (!data.Contains('\r') && data.Length != 21)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadIncomplete);
                    return 1;//不完整的值 表示天平不稳定
                }
                string id = data.Substring(0, 5).Replace(" ", "");//ID码 
                string result = data.Substring(6, 10).Replace(" ", "");//结果数据
                string unit = data.Substring(17, 2).Replace(" ", "");//单位
                value = result.Trim();
                return 0;//执行成功，获取正常值
            }
            catch
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorScaleReadIncomplete);
                return 3;
            }

        }

        public void Update()
        {
            if(!this.EasySerialPort.Connected)
            {
                //this.CommunicationOK = false;
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }
    }
}
