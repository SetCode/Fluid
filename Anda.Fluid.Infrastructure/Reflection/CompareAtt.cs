using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Reflection
{
    [AttributeUsage(AttributeTargets.All)]
    public class CompareAtt:Attribute
    {
        private string name;
        public string Name
        { get { return name; }
            set { name = value; }
        }
       
        public CompareAtt(string name)
        {
          this.Name = name;
        }

    }
}
