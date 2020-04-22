using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.UI
{
    public interface IControlUpdating
    {
        void Updating();
    }

    public class ControlUpdating
    {
        public int Key { get; set; }
        public IControlUpdating Control { get; set; }
    }

    public sealed class ControlUpatingMgr
    {
        private static List<ControlUpdating> controls = new List<ControlUpdating>();

        public static List<ControlUpdating> Controls => controls;

        public static void Add(IControlUpdating ctl, int key = 0)
        {
            controls.Add(new ControlUpdating()
            {
                Key = key,
                Control = ctl
            });
        }

        public static void UpdateControls(int key = 0)
        {
            foreach (var item in controls)
            {
                if (item.Control == null)
                {
                    continue;
                }
                if (item.Key == key)
                {
                    item.Control.Updating();
                }
            }
        }
    }
}
