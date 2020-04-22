using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    /// <summary>
    /// 相机所有操作的接口
    /// </summary>
    public interface ICameraExecutable
    {
        bool HasDisplayed { get; set; }

        /// <summary>
        /// 当前图像Bitmap
        /// </summary>
        Bitmap CurrentBmp { get; }

        /// <summary>
        /// 上一次的图像
        /// </summary>
        Bitmap LastBmp { get; }

        /// <summary>
        /// 当前图像Bytes
        /// </summary>
        byte[] CurrentBytes { get; }

        /// <summary>
        /// 图像宽度
        /// </summary>
        int ImageWidth { get; }

        /// <summary>
        /// 图像高度
        /// </summary>
        int ImageHeight { get; }

        /// <summary>
        /// 相机品牌
        /// </summary>
        Camera.Vendor Vendor { get; }

        /// <summary>
        /// 帧率
        /// </summary>
        double FrameRate { get; }

        /// <summary>
        /// 图像传感器宽度
        /// </summary>
        int SensorWidth { get; }

        /// <summary>
        /// 图像传感器高度
        /// </summary>
        int SensorHeight { get; }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        short Open();

        /// <summary>
        /// 初始化获取硬件必要参数
        /// </summary>
        /// <returns></returns>
        short Init();

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        short Close();

        short SetReverseX(bool value);

        short SetReverseY(bool value);

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="microsecond">单位ms</param>
        /// <returns></returns>
        short SetExposure(int microsecond);

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        /// <returns></returns>
        short SetGain(int gain);

        /// <summary>
        /// 获取曝光范围
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        short GetExposureRange(out int minValue, out int maxValue);

        /// <summary>
        /// 获取增益范围
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        short GetGainRange(out int minValue, out int maxValue);

        /// <summary>
        /// 启动采集
        /// </summary>
        /// <returns></returns>
        short StartGrabing();

        /// <summary>
        /// 设置取像模式
        /// </summary>
        /// <returns></returns>
        short SetContinueMode();

        /// <summary>
        /// 开始取像
        /// </summary>
        /// <returns></returns>
        short GrabOne();

        /// <summary>
        /// 停止取像
        /// </summary>
        /// <returns></returns>
        short StopGrabing();

        /// <summary>
        /// 连续模式切换到软触发模式
        /// </summary>
        /// <returns></returns>
        short ContinueToSoftShot();

        /// <summary>
        /// 软触发模式切换到连续模式
        /// </summary>
        /// <returns></returns>
        short SoftShotToContinue();

        /// <summary>
        /// 设置为触发模式
        /// </summary>
        /// <returns></returns>
        short SetTriggerMode();

        /// <summary>
        /// 关闭触发模式
        /// </summary>
        /// <returns></returns>
        short CloseTriggerMode();

        /// <summary>
        /// 设置软触发源
        /// </summary>
        /// <returns></returns>
        short SetSoftTriggerSource();

        /// <summary>
        /// 设置硬触发源
        /// </summary>
        /// <returns></returns>
        short SetHardTriggerSource();

        /// <summary>
        /// 执行软触发
        /// </summary>
        /// <returns></returns>
        short ExecuteSoftTrigger();

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <returns></returns>
        short SaveImage(string path);

        /// <summary>
        /// 触发状态
        /// </summary>
        Sts TriggerSts { get; }

        /// <summary>
        /// 图像采集成功Event
        /// </summary>
        ManualResetEvent BmpGrabedEvnet { get; }

        /// <summary>
        /// 采集图像回调
        /// </summary>
        event Action<Bitmap, bool> BitmapGrabbed;

        /// <summary>
        /// 显示图像回调
        /// </summary>
        event Action<Bitmap> BitmapDisplayed;

        /// <summary>
        /// 外部硬触发图像缓存回调
        /// </summary>
        event Action<byte[]> BytesSaveBuffer;
    }
}
