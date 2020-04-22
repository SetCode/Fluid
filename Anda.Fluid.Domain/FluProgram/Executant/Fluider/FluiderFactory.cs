using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveFluider;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive.ValveSystem.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.Executant.Fluider
{
    public class FluiderFactory
    {
        private static FluiderFactory instance = new FluiderFactory();

        private FluiderFactory() { }

        public static FluiderFactory Instance => instance;

        public IFluider CreatFluider(ValveType valveType)
        {
            if (valveType == ValveType.Both)
            {
                if (Machine.Instance.DualValve is JtDualValve)
                {
                    return new JtValveFluider();
                }
                else if (Machine.Instance.DualValve is SvDualValve)
                {
                    return new SvValveFluider();
                }
                else
                {
                    return new GearValveFluider();
                }
            }
            else
            {
                Valve valve = null;
                if (valveType == ValveType.Valve1)
                {
                    valve = Machine.Instance.Valve1;
                }
                else 
                {
                    valve = Machine.Instance.Valve2;
                }
                if (valve is JtValve)
                {
                    return new JtValveFluider();
                }
                else if (valve is SvValve)
                {
                    return new SvValveFluider();
                }
                else
                {
                    return new GearValveFluider();
                }
            }
        }

        public IFluider CreatFluider(Valve valve)
        {
            if (valve is JtValve)
            {
                return new JtValveFluider();
            }
            else if (valve is SvValve)
            {
                return new SvValveFluider();
            }
            else
            {
                return new GearValveFluider();
            }
        }
    }
}
