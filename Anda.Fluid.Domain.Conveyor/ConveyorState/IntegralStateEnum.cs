using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState
{
    public enum IntegralStateEnum
    {
        //UI层状态
        初始状态,
        Offline模式,
        等待运行,
        Auto模式,
        Demo模式,
        PassThrough模式,

        //模式层状态
        //Demo
        运行Demo,

        //Offline

        //Pass
        运行PassThrough,

        //Auto
        Auto起始状态,
        轨道检查,
        轨道复位,
        运行子站,
        运行结束,
        卡板

        //子站层状态在各子站文件夹内
        
    }
    public enum SubState
    {
        Idle,
        Running,
    }
    
}
