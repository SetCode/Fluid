using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Utils;
using System.ComponentModel;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Drive
{
    public class MachineSetting : ICloneable
    {
        [DisplayName("0.机型")]
        public MachineSelection MachineSelect { get; set; }

        [DisplayName("1.阀配置")]
        public ValveSelection ValveSelect { get; set; }

        [DisplayName("2.轨道配置")]
        public ConveyorSelection ConveyorSelect { get; set; }

        [DisplayName("3.板卡配置")]
        public CardSelection CardSelect { get; set; }

        [DisplayName("4.轴配置")]
        public RobotAxesStyle AxesStyle { get; set; }

        [DisplayName("5.双阀模式")]
        public DualValveMode DualValveMode { get; set; }

        [Browsable (false)]
        /// <summary>
        /// 双轨双程序
        /// </summary>
        public bool DoubleProgram { get; set; } = false;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Save()
        {
            string path = SettingsPath.PathMachine + "\\" + typeof(MachineSetting).Name;
            return JsonUtil.Serialize<MachineSetting>(path, this);
        }
    }

    public enum MachineSelection
    {
        AD16,
        iJet7,
        iJet6,
        AD19,
        YBSX,
        RTV,
        AFN,
        TSV300,
    }

    public enum ValveSelection
    {
        单阀,
        双阀
    }

    public enum ConveyorSelection
    {
        单轨,
        双轨,
    }

    public enum CardSelection
    {
        Gts8x1,
        Gts4x2,
        Gts4x1,
        ADMC4,
        Gts8_RTV
    }
}
