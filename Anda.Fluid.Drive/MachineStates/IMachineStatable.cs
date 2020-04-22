using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public interface IMachineStatable
    {
        string StateName { get; }
        void Enter(Machine machine);
        void Execute(Machine machine);
        void Exit(Machine machine);
    }
}
