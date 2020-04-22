using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain
{
    public static class AlarmInfoDomain
    {
        public static AlarmInfo NozzleCheckError => AlarmInfo.Create(AlarmLevel.Error, 4001, "检查点", "检查点错误，请重新检查", AlarmHandleType.ImmediateHandle);//"Nozzle check"   Nozzle check error,please check nozzle
        public static AlarmInfo MoveWorkPieceOrg => AlarmInfo.Create(AlarmLevel.Error, 4002, "移动到workpiece原点", "移动到workpiece原点错误", AlarmHandleType.ImmediateHandle);

        public static AlarmInfo InfoMeasuredGlueValue => AlarmInfo.Create(AlarmLevel.Warn, 4005, "测胶宽", "数值：超出上下限", AlarmHandleType.ImmediateHandle);//"measure height", "value:"

        public static AlarmInfo SingleDotWeight => AlarmInfo.Create(AlarmLevel.Fatal, 4010, "单点重量", "胶单点重量<= 0", AlarmHandleType.OnlyRecord);//"measure height", "value:"

        public static AlarmInfo GetBarcodeError => AlarmInfo.Create(AlarmLevel.Error, 4011, "获取条码","条码扫描失败", AlarmHandleType.ImmediateHandle);
    }
}
