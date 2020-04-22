using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.SVO
{
    public class TeachTaskSts
    {
        private static TeachTaskSts teachTaskSts;
        public static TeachTaskSts Instance
        {
            get
            {
                if(teachTaskSts==null)
                {
                    teachTaskSts = new TeachTaskSts();
                }
                return teachTaskSts;
            }
        }
        public bool TeachSafeZ_IsDone { get; set; } = false;
        public bool TeachNeedleToCamera_IsDone { get; set; } = false;
        public bool TeachLaserToCamera_IsDone { get; set; } = false;
        public bool TeachNeedleToHeight_IsDone { get; set; } = false;
        public bool TeachPurgeLocation_IsDone { get; set; } = false;
        public bool TeachScaleLocation_IsDone { get; set; } = false;
    }
}
