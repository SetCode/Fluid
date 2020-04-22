using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Flag
{
    public class FlagBitMgr:EntityMgr<FlagBit,int>
    {
        private static FlagBitMgr instance = new FlagBitMgr();
        private FlagBitMgr()
        {
            this.Add(new FlagBit(0));
            this.Add(new FlagBit(1));
        }
        public static FlagBitMgr Instance => instance;
        public int BoardCounts { get; set; } = 1;
        public bool Conveyor2Exist { get; set; } = false;

        public double ConveyorWidth { get; set; } = 10;
    }
}
