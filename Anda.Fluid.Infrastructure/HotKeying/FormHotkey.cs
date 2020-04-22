using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.HotKeying
{
    public class FormHotKey : HotKey
    {
        public FormHotKey(int id, Keys keyCode)
            : base(id, keyCode)
        {
        }

        public bool IsCtrl { get; set; }
        public bool IsShift { get; set; }
        public bool IsAlt { get; set; }
    }
}
