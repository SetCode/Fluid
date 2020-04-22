using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Sensors;
using Anda.Fluid.Infrastructure.Interfaces;
using System.Threading;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class HeaterController : EntityBase<int>, IAlarmSenderable
    {
        private double[] currentTemp, tempHighValue, tempLowValue, tempOffset;
        private IHeaterControllable entityHeaterControllable;
        private bool IsWorking, stopDone;
        private DateTime idleStart;
        private bool alarmEnable = true;

        /// <summary>
        /// 温控器是否关闭过
        /// </summary>
        public bool wasClosed { get; set; } = false;

        public HeaterController(int key, byte channelCount, IHeaterControllable heaterControllable,HeaterPrm heaterPrm)
            : base(key)
        {
            this.ChannelCount = channelCount;
            this.HeaterControllable = heaterControllable;
            this.HeaterPrm = heaterPrm;
            this.entityHeaterControllable = heaterControllable;
            this.currentTemp = new double[8];
            this.tempHighValue = new double[8];
            this.tempLowValue = new double[8];
            this.tempOffset = new double[8];
            this.IsWorking = true;
            this.stopDone = true;
        }

        public IHeaterControllable HeaterControllable { get; private set; }

        public byte ChannelCount { get; private set; }

        public HeaterPrm HeaterPrm { get; set; }

        public double[] CurrentTemp
        {
            get
            {
                lock(this)
                {
                    return currentTemp;
                }
            }
        }

        public double[] TempHighValue => this.tempHighValue;

        public double[] TempLowValue => this.tempLowValue;

        public double[] TempOffset => this.tempOffset;

        public object Obj => this;

        public string Name => this.GetType().Name;

        public bool AlarmEnable
        {
            get
            {
                if (!this.HeaterPrm.IsContinuseHeating && this.HeaterPrm.CloseHeatingWhenIdle)
                    return false;
                else
                    return this.alarmEnable;
            }
            set
            {
                this.alarmEnable = value;
            }
        }


        /// <summary>
        /// 当需要设置温控器为无效时，传入true;有效时传入false。
        /// </summary>
        /// <param name="isInvalid"></param>
        public void SetHeaterControllable(bool isInvalid)
        {
            if (isInvalid)
            {
                this.HeaterControllable = new InvalidThermostat(SensorMgr.Instance.Heater.EasySerialPort);
                return;
            }
            this.HeaterControllable = this.entityHeaterControllable;
        }

        /// <summary>
        /// 胶阀加热是否完成
        /// </summary>
        public bool HeatingIsFinished
        {
            get
            {
                if (this.HeaterControllable is ThermostatOmron)
                {
                    if (this.CurrentTemp[0] <= HeaterPrm.High[0] && this.CurrentTemp[0] >= HeaterPrm.Low[0])
                    {
                        return true;
                    }                                            
                    else
                    {
                        return false;
                    }
                       
                }
                else if (this.HeaterControllable is AiKaThermostat)
                {
                    if ((this.CurrentTemp[0] <= HeaterPrm.High[0] && this.CurrentTemp[0] >= HeaterPrm.Low[0])
                         && (this.CurrentTemp[1] <= HeaterPrm.High[1] && this.CurrentTemp[1] >= HeaterPrm.Low[1]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public bool[] TempOk()
        {
            bool[] result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                if ((this.CurrentTemp[i] <= HeaterPrm.High[i] && this.CurrentTemp[i] >= HeaterPrm.Low[i])
                    || (!this.HeaterPrm.IsContinuseHeating && this.HeaterPrm.CloseHeatingWhenIdle)
                    || this.CurrentTemp[i] == 120)  
                {
                    result[i] = true;
                }
                else
                {
                    result[i] = false;
                }
            }

            return result;
        }

        public void UpdateTemp()
        {
            for (int i = 0; i < this.ChannelCount; i++)
            {
                if (this.HeaterPrm.acitveChanel[i])
                {
                    this.HeaterControllable.GetTemp(out currentTemp[i], i);
                }
                Thread.Sleep(30);
            }
        }

        public void Fire(HeaterMessage heaterMessage)
        {
            HeaterServer.Instance.Fire(heaterMessage);
        }

        public void Fire(HeaterMsg heaterMsg, double value,int chanelNo)
        {
            this.Fire(new HeaterMessage(heaterMsg, value, chanelNo, this));
        }

        public void HandleMessage(HeaterMessage heaterMessage,double value)
        {
            switch (heaterMessage.Content)
            {
                case HeaterMsg.设置标准温度值:
                    this.HeaterControllable.SetTemp(value, heaterMessage.ChanelNo);
                    this.HeaterPrm.Standard[heaterMessage.ChanelNo] = value;
                    break;
                case HeaterMsg.设置温度上限值:
                    this.HeaterControllable.SetAlarmTemp(value, ToleranceType.High, heaterMessage.ChanelNo);
                    this.HeaterPrm.High[heaterMessage.ChanelNo] = value;
                    break;
                case HeaterMsg.设置温度下限值:
                    this.HeaterControllable.SetAlarmTemp(value, ToleranceType.Low, heaterMessage.ChanelNo);
                    this.HeaterPrm.Low[heaterMessage.ChanelNo] = value;
                    break;
                case HeaterMsg.设置温度漂移值:
                    this.HeaterControllable.SetTempOffset(value, heaterMessage.ChanelNo);
                    this.HeaterPrm.Offset[heaterMessage.ChanelNo] = value;
                    break;
            }
        }

        public void HandleMessage(HeaterMessage heaterMessage)
        {
            switch (heaterMessage.Content)
            {
                case HeaterMsg.获取标准温度值:
                    this.HeaterControllable.GetTemp(out currentTemp[heaterMessage.ChanelNo], heaterMessage.ChanelNo);
                    heaterMessage.Value = currentTemp[heaterMessage.ChanelNo];           
                    break;
                case HeaterMsg.获取温度上限值:
                    this.HeaterControllable.GetAlarmTemp(out tempHighValue[heaterMessage.ChanelNo], ToleranceType.High, heaterMessage.ChanelNo);
                    heaterMessage.Value = tempHighValue[heaterMessage.ChanelNo];
                    break;
                case HeaterMsg.获取温度下限值:
                    this.HeaterControllable.GetAlarmTemp(out tempLowValue[heaterMessage.ChanelNo], ToleranceType.Low, heaterMessage.ChanelNo);
                    heaterMessage.Value = tempLowValue[heaterMessage.ChanelNo];
                    break;
                case HeaterMsg.获取温度漂移值:
                    this.HeaterControllable.GetTempOffset(out tempOffset[heaterMessage.ChanelNo], heaterMessage.ChanelNo);
                    heaterMessage.Value = tempOffset[heaterMessage.ChanelNo];
                    break;
                case HeaterMsg.开始加热:
                    this.HeaterControllable.StartHeating(heaterMessage.ChanelNo);
                    break;
                case HeaterMsg.停止加热:
                    this.HeaterControllable.StopHeating(heaterMessage.ChanelNo);
                    break;
            }
        }

        public void Init()
        {
            //open heater
            this.HeaterControllable.Disconnect();
            if (!this.HeaterControllable.Connect(TimeSpan.FromSeconds(1)))
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
            }
        }


        /// <summary>
        /// 操控温控器，会根据时机自动进行开关操作
        /// </summary>
        /// <param name="opretaTime"></param>
        /// <returns></returns>
        public bool Opeate(OperateHeaterController opretaTime)
        {
            bool result = false;
            switch (opretaTime)
            {
                case OperateHeaterController.启动软件时:
                    if (this.HeaterPrm.IsContinuseHeating)
                        result = this.StartController();
                    else
                        result = true;
                    break;
                case OperateHeaterController.打开程序参数设定界面时:
                    if (!this.HeaterPrm.IsContinuseHeating)
                        result = this.StartController();
                    else
                        result = true;
                    break;
                case OperateHeaterController.关闭程序参数设定界面时:
                    if (!this.HeaterPrm.IsContinuseHeating)
                        result = this.StopController();
                    else
                        result = true;
                    break;
                case OperateHeaterController.打开温控器设定界面时:
                    if (!this.HeaterPrm.IsContinuseHeating)
                        result = this.StartController();
                    else
                        result = true;
                    break;
                case OperateHeaterController.关闭温控器设定界面时:
                    if (!this.HeaterPrm.IsContinuseHeating)
                        result = this.StopController();
                    else
                        result = true;
                    break;
                case OperateHeaterController.程序开始运行时:
                    if (!this.HeaterPrm.IsContinuseHeating)
                    {
                        result = this.StartController();
                        this.IsWorking = true;                      
                    }
                    else
                        result = true;
                    break;
                case OperateHeaterController.程序结束运行时:
                    if (!this.HeaterPrm.IsContinuseHeating)
                        result = this.StopController();
                    else
                        result = true;
                    break;
                case OperateHeaterController.可以启动自动关闭功能:
                    if (this.HeaterPrm.CloseHeatingWhenIdle
                        && !this.HeaterPrm.IsContinuseHeating)
                    {
                        this.IsWorking = false;
                        this.stopDone = false;
                        this.idleStart = DateTime.Now;
                    }
                    else
                        result = true;
                    break;
            }
            return result;
        }

        public HeaterController SetHeater(HeaterControllerMgr.Vendor vendor)
        {
            SensorMgr.Instance.Heater.Vendor = vendor;
            switch (vendor)
            {
                case HeaterControllerMgr.Vendor.Omron:
                    if (this.Key == 0)
                    {
                        this.HeaterControllable = new ThermostatOmron(2, SensorMgr.Instance.Heater.EasySerialPort);
                    }
                    else
                    {
                        this.HeaterControllable = new ThermostatOmron(3, SensorMgr.Instance.Heater.EasySerialPort);
                    }
                    break;
                case HeaterControllerMgr.Vendor.Aika:
                    if (this.Key == 0)
                    {
                        this.HeaterControllable = new AiKaThermostat(1, SensorMgr.Instance.Heater.EasySerialPort);
                    }
                    else
                    {
                        this.HeaterControllable = new InvalidThermostat(SensorMgr.Instance.Heater.EasySerialPort);
                    }
                    break;
                case HeaterControllerMgr.Vendor.Disable:
                    this.SetHeaterControllable(true);
                    break;
            }
            return this;
        }

        internal void UpdateAutoClose()
        {
            if (!this.IsWorking && !this.stopDone)
            {
                if (DateTime.Now - this.idleStart > TimeSpan.FromSeconds(this.HeaterPrm.IdleDecideTime))
                {
                    this.StopController();
                    this.stopDone = true;
                    this.wasClosed = true;
                } 
            }
        }

        /// <summary>
        /// 开启温控器，欧姆龙温控器会打开IO
        /// </summary>
        /// <returns></returns>
        private bool StartController()
        {
            if (this.HeaterControllable is ThermostatOmron == false) 
                return true;
            if (this.Key == 0)
            {
                DO o = DOMgr.Instance.FindByName("胶枪加热1");
                if (o == null)
                    return false;
                int result = o.Set(true);
                if (result != 0)
                    return false;
            }
            else if (this.Key == 1)
            {
                DO o = DOMgr.Instance.FindByName("胶枪加热2");
                if (o == null)
                    return false;
                int result = o.Set(true);
                if (result != 0)
                    return false;
            }

            Thread.Sleep(100);

            this.Init();

            return true;
        }

        /// <summary>
        /// 关闭温控器，欧姆龙温控器会关闭IO
        /// </summary>
        /// <returns></returns>
        private bool StopController()
        {
            if (this.HeaterControllable is ThermostatOmron == false) 
                return true;
            if (this.Key == 0)
            {
                DO o = DOMgr.Instance.FindByName("胶枪加热1");
                if (o == null)
                    return false;
                int result = o.Set(false);
                if (result != 0)
                    return false;
            }
            else if (this.Key == 1)
            {
                DO o = DOMgr.Instance.FindByName("胶枪加热2");
                if (o == null)
                    return false;
                int result = o.Set(false);
                if (result != 0)
                    return false;
            }
            return true;
        }
    }


    public enum OperateHeaterController
    {
        启动软件时,
        打开程序参数设定界面时,
        关闭程序参数设定界面时,
        打开温控器设定界面时,
        关闭温控器设定界面时,
        程序开始运行时,
        程序结束运行时,
        可以启动自动关闭功能,
    }
}
