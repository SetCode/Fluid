using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class PIDParamStruct
    {
        /// <summary>
        /// 采样时间
        /// </summary>
        public ushort SampleTime { get; set; }
        /// <summary>
        /// 控制系数D0
        /// </summary>
        public ushort SeriesD0 { get; set; }
        /// <summary>
        /// 控制系数D0
        /// </summary>
        public ushort SeriesD1 { get; set; }
        /// <summary>
        /// 控制系数D0
        /// </summary>
        public ushort SeriesD2 { get; set; }
        /// <summary>
        /// 控制范围
        /// </summary>
        public ushort CtrlArea { get; set; }
        /// <summary>
        /// 自适应基数
        /// </summary>
        public ushort OutputBase { get; set; }
    }
}
