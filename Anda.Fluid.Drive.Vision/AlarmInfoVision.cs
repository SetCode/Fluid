using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.Drive.Vision
{
    public static class AlarmInfoVision
    {
        public static AlarmInfo FatalCameraInit => AlarmInfo.Create(AlarmLevel.Fatal, 200, "初始化相机", "相机错误", AlarmHandleType.ImmediateHandle);//"init camera", "camera fatal"
        public static AlarmInfo ErrorCameraSetExposure => AlarmInfo.Create(AlarmLevel.Error, 201, "设置曝光", "相机设置曝光失败", AlarmHandleType.ImmediateHandle);//"set exposure", "camera set exposure failed"
        public static AlarmInfo ErrorCameraSetGain => AlarmInfo.Create(AlarmLevel.Error, 202, "设置增益", "相机设置增益失败", AlarmHandleType.ImmediateHandle);//"set gain", "camera set gain failed"
        public static AlarmInfo ErrorCameraConti => AlarmInfo.Create(AlarmLevel.Error, 203, "连续取图", "相机连续取图失败", AlarmHandleType.ImmediateHandle);//"grab continue", "camera grab continue failed"
        public static AlarmInfo ErrorCameraGetHardPrm => AlarmInfo.Create(AlarmLevel.Error, 204, "初始化", "相机获取硬件参数失败", AlarmHandleType.ImmediateHandle);//"init", "camera get hardware params failed"
        public static AlarmInfo ErrorCameraReverseX => AlarmInfo.Create(AlarmLevel.Error, 205, "翻转", "相机X方向翻转错误", AlarmHandleType.ImmediateHandle);//"reverse", "camera reverse x"
        public static AlarmInfo ErrorCameraReverseY => AlarmInfo.Create(AlarmLevel.Error, 205, "翻转", "相机Y方向翻转错误", AlarmHandleType.ImmediateHandle);//"reverse", "camera reverse y"
        public static AlarmInfo ErrorCameraSetTrigger => AlarmInfo.Create(AlarmLevel.Error, 206, "设置触发模式", "相机设置触发模式错误", AlarmHandleType.ImmediateHandle);//"set tigger mode", "camera set trigger mode failed"
        public static AlarmInfo ErrorCameraStartGrabing => AlarmInfo.Create(AlarmLevel.Error, 207, "开始连续采图错误", "相机开始连续采图时发生错误", AlarmHandleType.ImmediateHandle);//"start grabing failed", "camera start grabing failed"
        public static AlarmInfo ErrorCameraStopGrabing => AlarmInfo.Create(AlarmLevel.Error, 208, "停止连续采图错误", "相机停止连续采图时发生错误", AlarmHandleType.ImmediateHandle);//"stop grabing failed", "camera stop grabing failed"
        public static AlarmInfo WarnFindMarkFailed => AlarmInfo.Create(AlarmLevel.Warn, 220, "找Mark", "相机找Mark发生错误", AlarmHandleType.ImmediateHandle);//"find mark", "camera find mark failed"
        public static AlarmInfo WarnFindMarkOutOfTolerance => AlarmInfo.Create(AlarmLevel.Warn, 221, "找Mark", "相机找Mark超过设定公差值", AlarmHandleType.ImmediateHandle);//"find mark", "camera find mark out of tolerance"
        public static AlarmInfo WarnFindBarcodeFailed => AlarmInfo.Create(AlarmLevel.Warn, 222, "识别条码", "识别条码发生错误", AlarmHandleType.ImmediateHandle);//"find mark", "camera find mark failed"
        public static AlarmInfo InspectionResultNG => AlarmInfo.Create(AlarmLevel.Warn, 230, "机台采图和检测", "检测结果NG", AlarmHandleType.ImmediateHandle);//"Machine CaptureAndInspect", "inspection result is NG"

        public static AlarmInfo InspectionResultOutOfTolerance => AlarmInfo.Create(AlarmLevel.Warn, 240, "检测", "检测结果超出范围", AlarmHandleType.ImmediateHandle);


    }
}
