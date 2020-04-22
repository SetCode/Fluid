using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Proportionor
{
    public class Proportioner : EntityBase<int>, IAlarmSenderable
    {
        public delegate void ChangeProgramAir(ushort value);
        public event ChangeProgramAir ChangeProgramAirEvent;
        public enum ControlType
        {
            Direct,
            PLC,
            Disable
        }

        public Proportioner(int key)
            : base(key)
        {
        }

        public string Name => this.GetType().Name;

        public object Obj => this;

        public IProportional Proportional { get; set; }

        public Proportioner LoadSetting(EasySerialPort easySerialPort, ControlType controlType, int channel)
        {
            switch (controlType)
            {
                case ControlType.Direct:
                    this.Proportional = new ProportionorAnda(easySerialPort);
                    break;
                case ControlType.PLC:
                    this.Proportional = new ProportionorPLC(easySerialPort);
                    break;
                case ControlType.Disable:
                    this.Proportional = new ProportionorDisable(easySerialPort);
                    break;
            }
            this.Proportional.Channel = channel;
            return this;
        }

        public Proportioner SetProportionor(int id, ControlType controlType, ProportionerSetting setting)
        {
            switch(id)
            {
                case 1:
                    setting.ControlType1 = controlType;
                    break;
                case 2:
                    setting.ControlType2 = controlType;
                    break;
            }
            switch (controlType)
            {
                case ControlType.Direct:
                    this.Proportional = new ProportionorAnda(setting.EasySerialPort);
                    break;
                case ControlType.PLC:
                    this.Proportional = new ProportionorPLC(setting.EasySerialPort);
                    break;
                case ControlType.Disable:
                    this.Proportional = new ProportionorDisable(setting.EasySerialPort);
                    break;
            }
            return this;
        }

        public void Init(bool isConnect)
        {
            if (isConnect)
            {
                //open proportionor
                this.Proportional.Disconnect();
                if (!this.Proportional.Connect(TimeSpan.FromSeconds(1)))
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                    return;
                }
            }
            //test
            bool flag = false;
            for (int i = 0; i < 3; i++)
            {
                if (this.Proportional.SetValue(0))
                {
                    flag = true;
                    break;  
                }
            }
            if(!flag)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorProporSet);
            }
        }

        public static void Sleep()
        {
            System.Threading.Thread.Sleep(500);
        }

        public void ChangeProgramValue(ushort value)
        {
            this.ChangeProgramAirEvent?.Invoke(value);
        }
    }
}
