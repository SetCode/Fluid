using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Custom
{
    public static class CustomFactory
    {
        private static ICustomary custom = null;

        //private static List<CustomEnum> customEnums = new List<CustomEnum>();
        private static List<MachineSelection> customEnums = new List<MachineSelection>();

        public static ICustomary GetCustom(CustomEnum customEnum)
        {
            switch(customEnum)
            {
                case CustomEnum.AFN:
                    custom = new CustomAFN();
                    break;
                case CustomEnum.RTV:
                    custom = new CustomRTV();
                    break;
                default:
                    custom = new CustomDefault();
                    break;
            }
            return custom;
        }
        public static ICustomary GetCustom(MachineSelection customEnum)
        {
            switch (customEnum)
            {
                case MachineSelection.AFN:
                    custom = new CustomAFN();
                    break;
                case MachineSelection.RTV:
                    custom = new CustomRTV();
                    break;
                case MachineSelection.YBSX:
                    custom = new Custom银宝山();
                    break;
                default:
                    custom = new CustomDefault();
                    break;
            }
            return custom;
        }
        public static List<MachineSelection> GetCustomEnums()
        {
            customEnums.Clear();
            //customEnums.Add(CustomEnum.Default);
            //customEnums.Add(CustomEnum.AFN);
            //customEnums.Add(CustomEnum.RTV);
            foreach (int item in Enum.GetValues(typeof(MachineSelection)))
            {
                customEnums.Add((MachineSelection)item);
            }
            return customEnums;
        }
        
    }
}
