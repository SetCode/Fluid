using System;
using System.Drawing;
using System.Threading;
using Anda.Fluid.Infrastructure;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    public class HikExecutor : ICameraExecutable
    {
        private MyCamera.MV_CC_DEVICE_INFO_LIST deviceList;
        private MyCamera camera;
        private MyCamera.cbOutputExdelegate imageCallback;

        private bool reverseY;
        private Bitmap newBmp;
        private byte[] newBytes;

        private DateTime currDisplayedTime;
        private DateTime lastDisplayedTime;

        public HikExecutor()
        {
            this.deviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        }

        public ManualResetEvent BmpGrabedEvnet { get; private set; } = new ManualResetEvent(false);

        public Bitmap CurrentBmp { get; private set; }

        public Bitmap LastBmp { get; private set; }

        public bool HasDisplayed { get; set; }

        public byte[] CurrentBytes { get; private set; }
  
        public double FrameRate { get; private set; }

        public int ImageWidth { get; private set; } = 1280;

        public int ImageHeight { get; private set; } = 960;

        public int SensorHeight { get; private set; }

        public int SensorWidth { get; private set; }

        public Sts TriggerSts { get; private set; } = new Sts();

        public Camera.Vendor Vendor => Camera.Vendor.Hik;

        public event Action<Bitmap> BitmapDisplayed;

        public event Action<Bitmap, bool> BitmapGrabbed;

        /// <summary>
        /// 硬触发且在图像回调中抓取了图片时触发
        /// </summary>
        public event Action<byte[]> BytesSaveBuffer;

        public short Open()
        {
            try
            {
                if (null == this.camera)
                {
                    this.camera = new MyCamera();
                }

                int nRet;
                // ch:创建设备列表 en:Create Device List
                System.GC.Collect();
                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceList);
                if (0 != nRet)
                {
                    return (short)nRet;
                }

                if (this.deviceList.nDeviceNum < 1)
                {
                    return -1;
                }

                // 获取设备信息
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceList.pDeviceInfo[0], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                }

                // ch:打开设备 | en:Open device
                nRet = this.camera.MV_CC_CreateDevice_NET(ref device);
                if (MyCamera.MV_OK != nRet)
                {
                    return -2;
                }

                nRet = this.camera.MV_CC_OpenDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    this.camera.MV_CC_DestroyDevice_NET();
                    return -2;
                }

                // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    int nPacketSize = this.camera.MV_CC_GetOptimalPacketSize_NET();
                    if (nPacketSize > 0)
                    {
                        nRet = this.camera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    }
                }

                //设置参数
                nRet = this.camera.MV_CC_SetExposureAutoMode_NET((uint)MyCamera.MV_CAM_EXPOSURE_AUTO_MODE.MV_EXPOSURE_AUTO_MODE_OFF);
                nRet = this.camera.MV_CC_SetGainMode_NET((uint)MyCamera.MV_CAM_GAIN_MODE.MV_GAIN_MODE_OFF);

                // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
                this.camera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);// ch:工作在连续模式 | en:Acquisition On Continuous Mode
                this.camera.MV_CC_SetEnumValue_NET("TriggerMode", 0);    // ch:连续模式 | en:Continuous
                // ch:注册回调函数 | en:Register image callback
                imageCallback = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
                nRet = this.camera.MV_CC_RegisterImageCallBackEx_NET(imageCallback, IntPtr.Zero);
                if (MyCamera.MV_OK != nRet)
                {
                    return -3;
                }

                return 0;
            }
            catch(Exception)
            {
                return -5;
            }
        }

        public short Init()
        {
            try
            {
                MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
                camera.MV_CC_GetFloatValue_NET("ResultingFrameRate", ref stParam);
                this.FrameRate = stParam.fCurValue;

                MyCamera.MVCC_INTVALUE nValue = new MyCamera.MVCC_INTVALUE();
                camera.MV_CC_GetIntValue_NET("Width", ref nValue);
                this.ImageWidth = (int)nValue.nCurValue;

                camera.MV_CC_GetIntValue_NET("Height", ref nValue);
                this.ImageHeight = (int)nValue.nCurValue;

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
            if (this.camera == null)
            {
                return -1;
            }

            // ch:关闭设备 | en:Close Device
            int nRet;

            nRet = this.camera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            nRet = this.camera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return 0;
        }

        public short StartGrabing()
        {
            // ch:开始采集 | en:Start Grabbing
            int nRet = this.camera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short StopGrabing()
        {
            int nRet;
            // ch:停止采集 | en:Stop Grabbing
            nRet = this.camera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                return -1;
            }
            return 0;
        }

        public short SetContinueMode()
        {
            int nRet;
            //设置连续模式
            nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short GrabOne()
        {
            return this.ExecuteSoftTrigger();
        }

        public short ContinueToSoftShot()
        {
            this.SetTriggerMode();
            return this.SetSoftTriggerSource();
        }

        public short SoftShotToContinue()
        {
            return this.SetContinueMode();
        }

        public short SetTriggerMode()
        {
            //设置为触发模式
            int nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short CloseTriggerMode()
        {
            int nRet;
            //设置连续模式
            nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short SetSoftTriggerSource()
        {
            // ch:触发源设为软触发 | en:Set trigger source as Software
            int nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short SetHardTriggerSource()
        {
            // ch:触发源设为硬触发 | en:Set trigger source as Hardware
            int nRet;
            nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
            nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerSelector", 6);
            nRet = this.camera.MV_CC_SetIntValue_NET("LineDebouncerTime", 100);
            nRet = this.camera.MV_CC_SetEnumValue_NET("TriggerActivation", 0);
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short ExecuteSoftTrigger()
        {
            // ch:触发命令 | en:Trigger command
            int nRet = this.camera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return 0;
        }

        public short SetExposure(int value)
        {
            int nRet = this.camera.MV_CC_SetFloatValue_NET("ExposureTime", value);
            if (nRet != MyCamera.MV_OK)
            {
                return -1;
            }
            return 0;
        }

        public short SetGain(int gain)
        {
            int nRet = this.camera.MV_CC_SetFloatValue_NET("Gain", gain);
            if (nRet != MyCamera.MV_OK)
            {
                return -1;
            }
            return 0;
        }

        public short GetExposureRange(out int minValue, out int maxValue)
        {
            minValue = 0;
            maxValue = 10000;
            try
            {
                MyCamera.MVCC_INTVALUE nValue = new MyCamera.MVCC_INTVALUE();
                int nRet = this.camera.MV_CC_GetIntValue_NET("AutoExposureTimeLowerLimit", ref nValue);
                if (nRet != MyCamera.MV_OK)
                {
                    return -1;
                }
                minValue = (int)nValue.nMin;
                maxValue = (int)nValue.nMax;
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
                MyCamera.MVCC_FLOATVALUE fValue = new MyCamera.MVCC_FLOATVALUE();
                int nRet = this.camera.MV_CC_GetFloatValue_NET("AutoGainLowerLimit", ref fValue);
                if (nRet != MyCamera.MV_OK)
                {
                    return -1;
                }
                minValue = (int)fValue.fMin;
                maxValue = (int)fValue.fMax;
                if (maxValue > 10000)
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

        public short SaveImage(string path)
        {
            if (this.CurrentBmp == null)
            {
                return -1;
            }
            this.CurrentBmp.Save(path);
            return 0;
        }

        private void ImageCallbackFunc(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            try
            {
                lock (this)
                {
                    newBmp = new Bitmap(pFrameInfo.nWidth, pFrameInfo.nHeight, PixelFormat.Format8bppIndexed);
                    // Lock the bits of the bitmap.
                    BitmapData bmpData = newBmp.LockBits(new Rectangle(0, 0, newBmp.Width, newBmp.Height), ImageLockMode.ReadWrite, newBmp.PixelFormat);
                    IntPtr ptrBmp = bmpData.Scan0;

                    //palette
                    System.Drawing.Imaging.ColorPalette palette = newBmp.Palette;
                    for (int i = 0; i < 256; ++i)
                    {
                        palette.Entries[i] = Color.FromArgb(255, i, i, i);
                    }
                    newBmp.Palette = palette;

                    //convert to bytes data
                    newBytes = new byte[bmpData.Stride * newBmp.Height];
                    Marshal.Copy(pData, newBytes, 0, newBytes.Length);
                    //reverse y
                    if(this.reverseY)
                    {
                        newBytes = this.bytesReverseY(newBytes, bmpData.Stride, newBmp.Height);
                    }
                    //copy to ptr
                    Marshal.Copy(newBytes, 0, ptrBmp, newBytes.Length);
                    newBmp.UnlockBits(bmpData);

                    this.CurrentBytes = newBytes;
                    this.LastBmp = this.CurrentBmp;
                    this.CurrentBmp = newBmp;
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
            catch
            {
                BitmapGrabbed?.Invoke(null, false);
            }

        }

        public short SetReverseX(bool value)
        {
            int nRet = this.camera.MV_CC_SetBoolValue_NET("ReverseX", value);
            if (nRet != MyCamera.MV_OK)
            {
                return -1;
            }
            return 0;
        }

        public short SetReverseY(bool value)
        {
            //int nRet = this.camera.MV_CC_SetBoolValue_NET("ReverseY", value);
            //if (nRet != MyCamera.MV_OK)
            //{
            //    return -1;
            //}
            this.reverseY = value;
            return 0;
        }


        private byte[] bytesReverseY(byte[] src, int w, int h)
        {
            if(src.Length != w * h)
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
