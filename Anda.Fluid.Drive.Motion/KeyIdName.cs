using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Motion
{
    public class KeyIdName
    {
        public KeyIdName(int key, short id, string name)
        {
            this.Key = key;
            this.Id = id;
            this.Name = name;
        }

        public int Key { get; set; }
        public short Id { get; set; }
        public string Name { get; set; }
    }
}
