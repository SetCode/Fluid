using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.HotKeying
{
    public class HotKey
    {
        public HotKey(int id, Keys keyCode)
        {
            this.Id = id;
            this.KeyCode = keyCode;
        }

        public HotKey(Keys keyCode)
            : this(-1, keyCode)
        {

        }

        public int Id { get; private set; }

        public bool Enabled { get; set; }

        public Keys KeyCode { get; set; }

        public Sts State { get; private set; } = new Sts();

        public Action ActionKeyDown { get; set; }

        public Action ActionKeyUp { get; set; }
    }
}
