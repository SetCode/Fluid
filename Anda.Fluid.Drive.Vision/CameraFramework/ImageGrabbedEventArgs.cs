using System;
using System.Drawing;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    /// <summary>
    /// 包含抓取到的图片信息
    /// </summary>
    public class ImageGrabbedEventArgs : EventArgs
    {
        public ImageGrabbedEventArgs(Bitmap image,object data, ImageFormatType imageFormat)
        {
            Image = image;
            Data = data;
            ImageFormat = imageFormat;
            Error = 0;
        }
        /// <summary>
        /// 异常时的构造函数
        /// </summary>
        /// <param name="error"></param>
        public ImageGrabbedEventArgs(short error)
        {
            Error = error;
        }
        /// <summary>
        /// Bitmap格式的图片
        /// </summary>
        public Bitmap Image { get; private set; }
        /// <summary>
        /// 图片格式
        /// </summary>
        public ImageFormatType ImageFormat { get; private set; }
        /// <summary>
        /// 不是Bitmap格式时，图片存放在这里
        /// </summary>
        public object Data { get; private set; }
        /// <summary>
        /// 异常信息，0为无异常
        /// </summary>
        public short Error { get; private set; }
    }
}