using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.DeviceType
{
    public enum AxisType
    {
        X轴,
        Y轴,
        Z轴,
        A轴,
        B轴,
        R轴, //针嘴旋转
        U轴, //胶阀倾斜

        //RTV
        //RTV上轨道运输
        Axis5,
        //RTV上轨道调宽
        Axis6,
        //RTV下轨道运输
        Axis7,
        //RTV下轨道调宽
        Axis8
    }
}
