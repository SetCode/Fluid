using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{ 
    public interface ICPKable
    {
        CpkPrm CpkPrm { get; set; }
        ArrayList dataInput{ get; set; }
        ArrayList SpecfArr { get; set; }
        Specifications Specf { get; set; }
        void Execute(CpkPrm prm);
        void Stop();
    }
}
