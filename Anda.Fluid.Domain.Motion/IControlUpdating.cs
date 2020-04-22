using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Motion
{
    public interface IControlUpdating
    {
        void Updating();
    }

    public sealed class ControlUpatingMgr
    {
        private static List<IControlUpdating> controls = new List<IControlUpdating>();

        public static void Add(IControlUpdating ctl)
        {
            controls.Add(ctl);
        }

        public static void Remove(IControlUpdating ctl)
        {
            controls.Remove(ctl);
        }

        public static void Update()
        {
            foreach (var item in controls)
            {
                if(item == null)
                {
                    continue;
                }
                item.Updating();
            }
        }

        public static List<IControlUpdating> Controls => controls;
    }
}
