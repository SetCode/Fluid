using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.Main
{
    interface IStatable
    {
        void OnIdel();
        void OnRunning();
        void OnPaused();
    }
}
