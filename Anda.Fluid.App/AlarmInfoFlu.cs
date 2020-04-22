using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App
{
    public static class AlarmInfoFlu
    {
        public static AlarmInfo WarnProgramNull => AlarmInfo.Create(AlarmLevel.Warn, 1000, "程序为空", "", AlarmHandleType.ImmediateHandle);//program is null.
        public static AlarmInfo WarnProgramAlreadyLoaded => AlarmInfo.Create(AlarmLevel.Warn, 1001, "加载", "程序已加载", AlarmHandleType.ImmediateHandle);//laod , program has already loaded.
        public static AlarmInfo ErrorLoadingProgram => AlarmInfo.Create(AlarmLevel.Error, 1002, "加载", "程序加载错误", AlarmHandleType.ImmediateHandle);//Load , program loading error.
        //public static AlarmInfo ErrorLoadingProgram => AlarmInfo.Create(AlarmLevel.Error, 1002, "加载", "程序加载错误", AlarmHandleType.OnlyRecord);//Load , program loading error.
    }
}
