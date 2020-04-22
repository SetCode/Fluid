using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Anda.Fluid.Infrastructure;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System.Collections.Concurrent;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    public class BaslerExecutor : ICameraExecutable
    {
        private Basler.Pylon.Camera camera;
        private PixelDataConverter converter;

        private Bitmap newBmp;
        private byte[] newBytes;
        private bool reverseY;

        private DateTime currDisplayedTime;
        private DateTime lastDisplayedTime; 
  
        public BaslerExecutor()
        {
            currDisplayedTime = DateTime.Now;
            lastDisplayedTime = DateTime.MinValue;
        }

        /// <summary>
        /// 当前图像Bitmap
        /// </summary>
        public Bitmap CurrentBmp { get; private set; }

        public Bitmap LastBmp { get; private set; }

        public bool HasDisplayed { get; set; }

        /// <summary>
        /// 当前图像字节数组
        /// </summary>
        public byte[] CurrentBytes { get; private set; }

        /// <summary>
        /// 相机厂商
        /// </summary>
        public Camera.Vendor Vendor => Camera.Vendor.Basler;

        /// <summary>
        /// the model name of the camera.
        /// </summary>
        public string ModelName { get; private set; }

        /// <summary>
        /// 帧率
        /// </summary>
        public double FrameRate { get; private set; }

        /// <summary>
        /// 像素高度
        /// </summary>
        public int SensorHeight { get; private set; }

        /// <summary>
        /// 像素宽度
        /// </summary>
        public int SensorWidth { get; private set; }

        /// <summary>
        /// 图像宽度
        /// </summary>
        public int ImageWidth { get; private set; } = 1280;

        /// <summary>
        /// 图像高度
        /// </summary>
        public int ImageHeight { get; private set; } = 960;

        public ManualResetEvent BmpGrabedEvnet { get; private set; } = new ManualResetEvent(false);

        /// <summary>
        /// 触发状态
        /// </summary>
        public Sts TriggerSts { get; private set; } = new Sts();

        /// <summary>
        /// 抓取一张图片时触发
        /// </summary>
        public event Action<Bitmap, bool> BitmapGrabbed;

        public event Action<Bitmap> BitmapDisplayed;

        /// <summary>
        /// 硬触发且在图像回调中抓取了图片时触发
        /// </summary>
        public event Action<byte[]> BytesSaveBuffer;

        public short Open()
        {
            try
            {
                camera = new Basler.Pylon.Camera();
                this.ModelName = camera.CameraInfo[CameraInfoKey.ModelName];

                // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                camera.CameraOpened += Configuration.AcquireContinuous;

                // Open the connection to the camera device.
                camera.Open();

                // The parameter MaxNumBuffer can be used to control the amount of buffers
                // allocated for grabbing. The default value of this parameter is 10.
                camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);

                //如果触发是AcquisisionStart，则要设置AcquisionFrameCount值为1
                //camera.Parameters[PLCamera.AcquisitionFrameCount].SetValue(1);

                //设置参数
                //camera.Parameters[PLCamera.ExposureAuto].SetValue(PLCamera.ExposureAuto.Off);
                //camera.Parameters[PLCamera.GainAuto].SetValue(PLCamera.GainAuto.Off);

                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);//连续取图模式

                this.converter = new PixelDataConverter();
                camera.StreamGrabber.ImageGrabbed += onIamgeGrabbed;

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public short Init()
        {
            try
            {
                this.FrameRate = (int)camera.Parameters[PLCamera.ResultingFrameRateAbs].GetValue();
                this.SensorHeight = (int)camera.Parameters[PLCamera.SensorHeight].GetValue();
                this.SensorWidth = (int)camera.Parameters[PLCamera.SensorWidth].GetValue();
                this.ImageWidth = (int)camera.Parameters[PLCamera.Width].GetValue();
                this.ImageHeight = (int)camera.Parameters[PLCamera.Height].GetValue();
                if (this.ImageWidth <= 0) this.ImageWidth = 1280;
                if (this.ImageHeight <= 0) this.ImageHeight = 960;

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short Close()
        {
            if(camera == null)
            {
                return -1;
            }
            else
            {
                camera.Close();
                camera.Dispose();
                camera = null;
                return 0;
            }
        }

        public short StartGrabing()
        {
            try
            {
                camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short StopGrabing()
        {
            try
            {
                camera.StreamGrabber.Stop();
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short SetContinueMode()
        {
            try
            {
                this.CloseTriggerMode();
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);//连续取图模式
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short GrabOne()
        {
            try
            {
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short ContinueToSoftShot()
        {
            short rtn = this.SetTriggerMode();
            if (rtn != 0)
            {
                return -1;
            }
            rtn = this.SetSoftTriggerSource();
            return rtn;
        }

        public short SoftShotToContinue()
        {
            return this.SetContinueMode();
        }

        public short SetTriggerMode()
        {
            try
            {
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                return 0;
            }
            catch 
            {
                return -1;
            }
        }

        public short CloseTriggerMode()
        {
            try
            {
                camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short SetSoftTriggerSource()
        {
            try
            {
                camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Software);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short SetHardTriggerSource()
        {
            try
            {
                camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line1);
                camera.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                camera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.RisingEdge);
                camera.Parameters[PLCamera.LineDebouncerTimeAbs].SetValue(100.0);
                return 0;
            }
            catch 
            {
                return -1;
            }
        }

        public short ExecuteSoftTrigger()
        {
            try
            {
                camera.Parameters[PLCamera.TriggerSoftware].Execute();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public short SetExposure(int ms)
        {
            try
            {
                camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(ms);
                int exposure = (int)camera.Parameters[PLCamera.ExposureTimeAbs].GetValue();
                if (exposure != ms) //未设置成功，返回2
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 1;
            }
        }

        public short SetGain(int gain)
        {
            try
            {
                camera.Parameters[PLCamera.GainRaw].SetValue(gain);
                int gainValue = (int)camera.Parameters[PLCamera.GainRaw].GetValue();
                if (gainValue != gain) //未设置成功，返回2
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 1;
            }
        }

        public short GetExposureRange(out int minValue, out int maxValue)
        {
            minValue = 0;
            maxValue = 10000;
            try
            {
                minValue = (int)camera.Parameters[PLCamera.ExposureTimeAbs].GetMinimum();
                maxValue = (int)camera.Parameters[PLCamera.ExposureTimeAbs].GetMaximum();
                if(maxValue > 10000)
                {
                    maxValue = 10000;
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short GetGainRange(out int minValue, out int maxValue)
        {
            minValue = 0;
            maxValue = 1000;
            try
            {
                minValue = (int)camera.Parameters[PLCamera.GainRaw].GetMinimum();
                maxValue = (int)camera.Parameters[PLCamera.GainRaw].GetMaximum();
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short SaveImage(string path)
        {
            if(this.CurrentBmp == null)
            {
                return -1;
            }
            this.CurrentBmp.Save(path);
            return 0;
        }

        private void onIamgeGrabbed(object sender, Basler.Pylon.ImageGrabbedEventArgs e)
        {
            try
            {
                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                if (!grabResult.GrabSucceeded)
                {
                    BitmapGrabbed?.Invoke(null, false);
                    return;
                }

                // Check if the image can be displayed.
                if (!grabResult.IsValid)
                {
                    BitmapGrabbed?.Invoke(null, false);
                    return;
                }
                lock (this)
                {
                    newBmp = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);
                    // Lock the bits of the bitmap.
                    BitmapData bmpData = newBmp.LockBits(new Rectangle(0, 0, newBmp.Width, newBmp.Height), ImageLockMode.ReadWrite, newBmp.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.
                    converter.OutputPixelFormat = PixelType.Mono8;
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * newBmp.Height, grabResult);
                    //palette
                    System.Drawing.Imaging.ColorPalette palette = newBmp.Palette;
                    for (int i = 0; i < 256; ++i)
                    {
                        palette.Entries[i] = Color.FromArgb(255, i, i, i);
                    }
                    newBmp.Palette = palette;
                    //convert to bytes data
                    newBytes = new byte[bmpData.Stride * newBmp.Height];
                    Marshal.Copy(ptrBmp, newBytes, 0, newBytes.Length);
                    //reverse y
                    if (this.reverseY)
                    {
                        newBytes = this.bytesReverseY(newBytes, bmpData.Stride, newBmp.Height);
                    }
                    //copy to ptr
                    Marshal.Copy(newBytes, 0, ptrBmp, newBytes.Length);
                    newBmp.UnlockBits(bmpData);

                    this.CurrentBytes = this.newBytes;
                    this.LastBmp = this.CurrentBmp;
                    this.CurrentBmp = this.newBmp;
                }
                BmpGrabedEvnet.Set();
                this.TriggerSts.Update(true);
                BitmapGrabbed?.Invoke(CurrentBmp, true);
                BytesSaveBuffer?.Invoke(CurrentBytes);
                // Reduce the number of displayed images to a reasonable amount 
                // if the camera is acquiring images very fast.
                this.currDisplayedTime = DateTime.Now;
                if (this.currDisplayedTime - this.lastDisplayedTime > TimeSpan.FromMilliseconds(70))
                {
                    this.BitmapDisplayed?.Invoke(this.CurrentBmp);
                    this.lastDisplayedTime = this.currDisplayedTime;
                    // the LastBmp can be dispose after BitmapDisplayed invoke execute.
                    this.LastBmp.Dispose();
                }

            }
            catch (Exception)
            {
                BitmapGrabbed?.Invoke(null, false);
            }
        }

        public short SetReverseX(bool value)
        {
            try
            {
                camera.Parameters[PLCamera.ReverseX].SetValue(value);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public short SetReverseY(bool value)
        {
            //try
            //{
            //    camera.Parameters[PLCamera.ReverseY].SetValue(value);
            //    return 0;
            //}
            //catch
            //{
            //    return -1;
            //}
            this.reverseY = value;
            return 0;
        }

        private byte[] bytesReverseY(byte[] src, int w, int h)
        {
            if (src.Length != w * h)
            {
                return src;
            }
            byte[] rtn = new byte[src.Length];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    rtn[w * i + j] = src[w * (h - i - 1) + j];
                }
            }
            return rtn;
        }
    }
}
