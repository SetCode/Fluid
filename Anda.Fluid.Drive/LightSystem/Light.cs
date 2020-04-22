using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Lighting.Custom;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Sensors;

namespace Anda.Fluid.Drive.LightSystem
{
    public class Light : EntityBase<int>,IAlarmSenderable
    {      
      
        public Light(int key):base(key)
        { }
        public ExecutePrm ExecutePrm { get; set; }
        private string path = SettingsPath.PathMachine + "\\" + typeof(ExecutePrm).Name;
        public ILighting Lighting { get; private set; }

        public object Obj => this;

        public string Name => this.GetType().Name;       

        private LightSetting setting;
        public Light GetLight(LightSetting setting)
        {
            this.setting = setting;
            switch (setting.Vendor)
            {
                case LightVendor.Anda:
                    Lighting = new AndaLight();
                    break;
                case LightVendor.Custom:
                    //Lighting = new LightingOPT(setting.prm);
                    Lighting = new LightCustom(setting.EasySerialPort);
                    break;
                case LightVendor.OPT:
                    Lighting = new LightingOPT(setting.EasySerialPort);
                    break;
            }
            return this;
        }
        public void None()
        {
            this.Lighting.None();
        }
        public void ResetToLast()
        {
            this.Lighting.ResetToLast();
        }
        public void SetLight(ExecutePrm prm)
        {
            if (prm==null)
            {
                prm = new ExecutePrm();
            }
            this.ExecutePrm = prm.Clone() as ExecutePrm;
            this.Lighting.SetLight(this.ExecutePrm);
        }


        public void Init()
        {
            if (this.setting.Vendor==LightVendor.OPT)
            {
                //断开连接
                LightingOPT opt = Lighting as LightingOPT;
                opt.Disconnect();
                //连接
                if (!opt.Connect(TimeSpan.FromSeconds(1)))
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLightConncet);
                }
                return;
            }


        }
        public bool SavePrm()
        {
            return JsonUtil.Serialize<ExecutePrm>(this.path, this.ExecutePrm);
        }
        public bool LoadPrm()
        {
            this.ExecutePrm =JsonUtil.Deserialize<ExecutePrm>(this.path);
            if (this.ExecutePrm==null)
            {
                this.ExecutePrm = new ExecutePrm();
            }
            return true;
        }

    }
}
