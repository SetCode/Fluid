using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.Drive.Motion
{
    public static class AlarmInfoMotion
    {
        public static AlarmInfo FatalCardInit => AlarmInfo.Create(AlarmLevel.Fatal, 100, "初始化", "运动控制卡初始化错误", AlarmHandleType.ImmediateHandle);//"init", card fatal
        public static AlarmInfo FatalExtMdlInit => AlarmInfo.Create(AlarmLevel.Fatal, 101, "初始化", "拓展卡初始化错误", AlarmHandleType.ImmediateHandle);//"init", ExtMdl fatal

        public static AlarmInfo FatalAxisInit => AlarmInfo.Create(AlarmLevel.Fatal, 110, "初始化", "轴初始化错误", AlarmHandleType.ImmediateHandle);//"init", axis fatal
        public static AlarmInfo FatalServoOn => AlarmInfo.Create(AlarmLevel.Fatal, 111, "伺服", "轴伺服状态错误", AlarmHandleType.AutoHandle);//"servo", servo on fatal
        public static AlarmInfo FatalMoveHomeAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 112, "回原点", "回原点时伺服错误", AlarmHandleType.ImmediateHandle);//"move home", servo alarm
        public static AlarmInfo FatalMovePosAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 113, "点动", "点动时伺服错误", AlarmHandleType.ImmediateHandle);//"move pos", servo alarm
        public static AlarmInfo FatalMoveJogAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 114, "连续运动", "连续运动时伺服错误", AlarmHandleType.ImmediateHandle);//"move jog", servo alarm
        public static AlarmInfo FatalMoveTrcAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 115, "插补运动", "插补运动时伺服错误", AlarmHandleType.ImmediateHandle);// "move trc",servo alarm
        public static AlarmInfo FatalEscapeLmtAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 116, "逃离极限", "逃离极限时伺服错误", AlarmHandleType.ImmediateHandle);//"escape lmt", servo alarm
        public static AlarmInfo FatalMovePosError => AlarmInfo.Create(AlarmLevel.Fatal, 117, "点动", "点动时运动错误", AlarmHandleType.ImmediateHandle);// "move pos", move error
        public static AlarmInfo FatalMoveJogError => AlarmInfo.Create(AlarmLevel.Fatal, 118, "连续运动", "连续运动时运动错误", AlarmHandleType.ImmediateHandle);//"move jog",move error
        public static AlarmInfo FatalMoveTrcError => AlarmInfo.Create(AlarmLevel.Fatal, 119, "插补运动", "插补运动时运动错误", AlarmHandleType.ImmediateHandle);//"move trc",move error
        public static AlarmInfo FatalEscapeLmtError => AlarmInfo.Create(AlarmLevel.Fatal, 120, "逃离极限", "逃离极限时运动错误", AlarmHandleType.ImmediateHandle);//"escape lmt",move error
        public static AlarmInfo FatalMoveHomeError => AlarmInfo.Create(AlarmLevel.Fatal, 121, "回原点", "回原点错误", AlarmHandleType.AutoHandle);//"move home", "home error"
        public static AlarmInfo FatalIsError => AlarmInfo.Create(AlarmLevel.Fatal, 122, "轴", "轴发生错误", AlarmHandleType.AutoHandle);//"Axis", "Axis is Error"
        public static AlarmInfo FatalIsAlarm => AlarmInfo.Create(AlarmLevel.Fatal, 123, "轴", "轴发生报警", AlarmHandleType.AutoHandle);//"Axis", "Axis is Alarm"

        public static AlarmInfo FatalCrdInit => AlarmInfo.Create(AlarmLevel.Fatal, 130, "初始化轴卡", "轴卡错误", AlarmHandleType.ImmediateHandle);//"init crd", "crd fatal"
        public static AlarmInfo FatalCmp2dStart => AlarmInfo.Create(AlarmLevel.Fatal, 131, "开始二维比较", "二维比较错误", AlarmHandleType.ImmediateHandle);//"start cmp2d", "cmp2d fatal"
        public static AlarmInfo FatalCmp2dStop => AlarmInfo.Create(AlarmLevel.Fatal, 132, "停止二维比较", "二维比较错误", AlarmHandleType.ImmediateHandle);//"stop cmp2d", "cmp2d fatal"
        public static AlarmInfo FatalCmp2dInit => AlarmInfo.Create(AlarmLevel.Fatal, 133, "初始化二维比较", "二维比较错误", AlarmHandleType.ImmediateHandle);//"init cmp2d", "cmp2d fatal"
        public static AlarmInfo FatalCmp2dData => AlarmInfo.Create(AlarmLevel.Fatal, 134, "添加二维比较数据", "二维比较错误", AlarmHandleType.ImmediateHandle);//"add cmp2d data", "cmp2d fatal"

        public static AlarmInfo WarnMoveToPLmt => AlarmInfo.Create(AlarmLevel.Warn, 150, "移动", "触发正极限", AlarmHandleType.AutoHandle);//"move", "trigger positive limit"
        public static AlarmInfo WarnMoveToNLmt => AlarmInfo.Create(AlarmLevel.Warn, 151, "移动", "触发负极限", AlarmHandleType.AutoHandle);//"move", "trigger negative limit"
        public static AlarmInfo WarnLowerThanMinZ => AlarmInfo.Create(AlarmLevel.Warn, 152, "Z轴移动", "低于Z轴最小值", AlarmHandleType.ImmediateHandle);//"move z", "lower than min z"



    }
}
