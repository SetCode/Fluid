using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anda.Fluid.App.Setting
{
    public class MachineSetting : ICloneable
    {
        public MachineSelection MachineSelect { get; set; }

        public ValveSelection ValveSelect { get; set; }

        public ConveyorSelection ConveyorSelect { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public enum MachineSelection
    {
        AD16,
        iJet7,
        iJet6
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
}
