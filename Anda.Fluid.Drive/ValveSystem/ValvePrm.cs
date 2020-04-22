using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem
{

    [JsonObject(MemberSerialization.OptIn)]
    public class ValvePrm : EntityBase<ValveType>
    {
        public ValvePrm(ValveType key) : base(key)
        {

        }

        [JsonProperty]
        public ValveSeries ValveSeires { get; set; }

        [JsonProperty]
        public JtValvePrm JtValvePrm { get; set; } = new JtValvePrm();

        [JsonProperty]
        public SvValvePrm SvValvePrm { get; set; } = new SvValvePrm();

        [JsonProperty]
        public GearValvePrm GearValvePrm { get; set; } = new GearValvePrm();

        public void ResetToDefault()
        {
            this.ValveSeires = ValveSeries.喷射阀;
            this.JtValvePrm = SettingUtil.ResetToDefault<JtValvePrm>(this.JtValvePrm);
            this.SvValvePrm = SettingUtil.ResetToDefault<SvValvePrm>(this.SvValvePrm);
            this.GearValvePrm = SettingUtil.ResetToDefault<GearValvePrm>(this.GearValvePrm);
        }
    }

    public sealed class ValvePrmMgr : EntityMgr<ValvePrm, ValveType>
    {
        private static readonly ValvePrmMgr instance = new ValvePrmMgr(SettingsPath.PathMachine);
        private ValvePrmMgr() { }
        private ValvePrmMgr(string dir) : base(dir)
        {

        }
        public static ValvePrmMgr Instance => instance;
    }
    public enum Cmp2dSrcType
    {
        规划器,
        编码器
    }

    public enum ValveFluidMode
    {
        一维比较优先,
        二维比较优先,
        时间控制优先,
    }

    public enum ValveMoveMode
    {
        单次插补,
        连续插补
    }

}
