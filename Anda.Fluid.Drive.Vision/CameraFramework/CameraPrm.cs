using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CameraPrm : EntityBase<int>,ICloneable
    {
        public CameraPrm(int key)
            : base(key)
        {

        }

        [JsonProperty]
        public bool ReverseX { get; set; }

        [JsonProperty]
        public bool ReverseY { get; set; }

        [JsonProperty]
        public Camera.Vendor Vendor { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int Exposure { get; set; }

        [JsonProperty]
        public int Gain { get; set; }

        //[JsonProperty]
        ////public LightType LightType { get; set; }
        //public ExecutePrm ExecutePrm { get; set; }

        [JsonProperty]
        public Camera.TrigSrcType TrigSrc { get; set; }

        public CameraPrm Default()
        {
            this.Exposure = 5000;
            this.Gain = 300;
            return this;
        }

        public object Clone()
        {
            return  (CameraPrm)this.MemberwiseClone();            
        }

    }
    public sealed class CameraPrmMgr : EntityMgr<CameraPrm, int>
    {
        private readonly static CameraPrmMgr instance = new CameraPrmMgr(SettingsPath.PathMachine);
        private CameraPrmMgr() { }
        private CameraPrmMgr(string dir) : base(dir)
        { }
        public static CameraPrmMgr Instance => instance;
    }
}
