using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Sensors;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    /// <summary>
    /// 天平类
    /// </summary>
    public class Scale : EntityBase<int>, IAlarmSenderable
    {
        public enum Vendor
        {
            Sartorius,
            Mettler,
            Disable
        }

        public Scale(int key)
            : base(key)
        {
        }

        public string Name => this.GetType().Name;

        public object Obj => this;

        /// <summary>
        /// 天平接口
        /// </summary>
        public IScalable Scalable { get; set; }

        public Scale LoadSetting(ScaleSetting scaleSetting)
        {
            switch (scaleSetting.Vendor)
            {
                case Vendor.Sartorius:
                    this.Scalable = new ScalableSartorius(scaleSetting.EasySerialPort);
                    break;
                case Vendor.Mettler:
                    this.Scalable = new ScalableMettler(scaleSetting.EasySerialPort);
                    break;
                case Vendor.Disable:
                    this.Scalable = new ScalebleDisable(scaleSetting.EasySerialPort);
                    break;
            }
            return this;
        }

        public Scale SetScalable(Vendor vendor)
        {
            SensorMgr.Instance.Scale.Vendor = vendor;
            switch(vendor)
            {
                case Vendor.Sartorius:
                    this.Scalable = new ScalableSartorius(SensorMgr.Instance.Scale.EasySerialPort);
                    break;
                case Vendor.Mettler:
                    this.Scalable = new ScalableMettler(SensorMgr.Instance.Scale.EasySerialPort);
                    break;
                case Vendor.Disable:
                    this.Scalable = new ScalebleDisable(SensorMgr.Instance.Scale.EasySerialPort);
                    break;
            }
            return this;
        }

        /// <summary>
        /// 获取天平的读取、去皮、清零指令
        /// </summary>
        /// <param name="vendor">厂商</param>
        /// <returns></returns>
        public static Tuple<string, string, string> GetCmds(Vendor vendor)
        {
            IScaleCmdalbe cmdable = null;
            switch(vendor)
            {
                case Vendor.Sartorius:
                    cmdable = new ScalableSartorius(null);
                    break;
                case Vendor.Mettler:
                    cmdable = new ScalableMettler(null);
                    break;
                case Vendor.Disable:
                    cmdable = new ScalebleDisable(null);
                    break;
            }
            return new Tuple<string, string, string>(cmdable.PrintCmd, cmdable.TareCmd, cmdable.ZeroCmd);
        }

        public void Init()
        {
            //open scale
            this.Scalable.Disconnect();
            if (!this.Scalable.Connect(TimeSpan.FromSeconds(1)))
            {
                Logger.DEFAULT.Error(LogCategory.MANUAL | LogCategory.RUNNING, Scalable.GetType().Name, "serial port fatal");
                if (this.Scalable is ScalebleDisable == false)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                }            
            }

            //test scale
            bool flagTest = false;
            for (int i = 0; i < 3; i++)
            {
                if (this.Scalable.CommunicationTest())
                {
                    flagTest = true;
                    break;
                }
            }
            if (!flagTest)
            {
                Logger.DEFAULT.Error(LogCategory.MANUAL | LogCategory.RUNNING, Scalable.GetType().Name, "serial port fatal");
                if (this.Scalable is ScalebleDisable == false)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                }
            }
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, Scalable.GetType().Name, "serial port connected successfully");
        }
    }
}
