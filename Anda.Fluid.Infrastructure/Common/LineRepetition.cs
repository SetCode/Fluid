using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Common
{
    [Serializable]
    /// <summary>
    /// 线首尾点重复结果
    /// </summary>    
    public class LineRepetition
    {
        public bool StartIsRepeat;

        public bool EndIsRepeat;

        public bool TotalRepeat
        {
            get
            {
                return StartIsRepeat && EndIsRepeat;
            }
        }

        public LineRepetition()
        {
            this.StartIsRepeat = false;
            this.EndIsRepeat = false;
        }
        public LineRepetition (bool startIsRepeat,bool endIsRepeat)
        {
            this.StartIsRepeat = startIsRepeat;
            this.EndIsRepeat = endIsRepeat;
        }
    }
}
