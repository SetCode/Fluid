using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Drawing;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Calib;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    public class Camera : EntityBase<int>, IAlarmSenderable
    {
        public enum Vendor
        {
            Basler,
            Hik
        }

        public enum TrigSrcType
        {
            Soft,
            Ex
        }

        public Camera(int key, CameraPrm prm)
            : base(key)
        {
            this.Prm = prm;
            this.SelectVendor(prm.Vendor);
        }

        ~Camera()
        {
            try
            {
                Executor.Close(); //关闭相机资源
            }
            catch (Exception)
            {

            }
        }
        private bool isTriggerMode = false;

        private int imageBufferIndex = 0;

        public ICameraExecutable Executor { get; private set; } 
        
        public CameraPrm Prm { get; private set; }

        public CameraPrm PrmBackUP { get; set; }

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => (this.Prm.Name == null || this.Prm.Name == string.Empty)? this.EntityName : this.Prm.Name;

        public int ExposureTimeMax { get; private set; }

        public int ExposureTimeMin { get; private set; }

        public int GainMax { get; private set; }

        public int GainMin { get; private set; }

        public bool IsInitDone { get; set; }

        public ConcurrentQueue<KeyValuePair<int,byte[]>> ImageByteBuffer { get; set; } = new ConcurrentQueue<KeyValuePair<int, byte[]>>();

        public short Open()
        {
            this.IsInitDone = true;
            try
            {
                short rtn = this.Executor.Open();
                if (rtn != 0)
                {
                    this.IsInitDone = false;
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.FatalCameraInit);
                }
                return rtn;
            }
            catch
            {
                this.IsInitDone = false;
                AlarmServer.Instance.Fire(this, AlarmInfoVision.FatalCameraInit);
                return -10;
            }
        }

        public short Init()
        {
            //获取曝光范围
            int minValue, maxValue;
            short rtn = this.Executor.GetExposureRange(out minValue, out maxValue);
            this.ExposureTimeMin = minValue;
            this.ExposureTimeMax = maxValue;

            //获取增益范围
            rtn = this.Executor.GetGainRange(out minValue, out maxValue);
            this.GainMin = minValue;
            this.GainMax = maxValue;

            //初始化硬件参数：获取图像大小
            rtn = this.Executor.Init();
            if (rtn != 0)
            {
                this.IsInitDone = false;
                Logger.DEFAULT.Error(LogCategory.LOADING, this.GetType().Name, "initialize failed, camera get hardware params failed");
                AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraGetHardPrm);
            }

            //设置图像
            this.SetReverseX(this.Prm.ReverseX);
            this.SetReverseY(this.Prm.ReverseY);
            //设置曝光
            this.SetExposure(this.Prm.Exposure);
            //设置增益
            this.SetGain(this.Prm.Gain);
            //启动采集
            rtn = this.Executor.StartGrabing();
            if (rtn != 0)
            {
                this.IsInitDone = false;
                Logger.DEFAULT.Error(LogCategory.LOADING, this.GetType().Name, "initialize failed, camera grab continue failed");
                AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraConti);
            }
            //绑定硬触发回调函数
            Logger.DEFAULT.Error(LogCategory.LOADING, this.GetType().Name, "camera initialize successfully");
            return rtn;
        }

        public short Close()
        {
            try
            {
                return this.Executor.Close();
            }
            catch (Exception)
            {
                return -10;
            }
        }

        public short SelectVendor(Vendor vendor)
        {
            this.Prm.Vendor = vendor;
            switch (vendor)
            {
                case Vendor.Basler:
                    this.Executor = new BaslerExecutor();
                    break;
                case Vendor.Hik:
                    this.Executor = new HikExecutor();
                    break;
            }
            this.Executor.BytesSaveBuffer += SaveImageBufferQueue;
            return 0;
        }

        public short SetExposure(int exposureTime)
        {
            try
            {
                this.Prm.Exposure = exposureTime;
                short rtn = this.Executor.SetExposure(exposureTime);
                if(rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraSetExposure);
                }
                return rtn;
            }
            catch
            {
                return -10;
            }
        }

        public short SetGain(int gain)
        {
            try
            {
                this.Prm.Gain = gain;
                if (gain > this.GainMax)
                {
                    gain = this.GainMax;
                }
                short rtn = this.Executor.SetGain(gain);
                if (rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraSetGain);
                }
                return rtn;
            }
            catch 
            {
                return -10;
            }
        }

        public short SetReverseX(bool value)
        {
            try
            {
                short rtn = this.Executor.SetReverseX(value);
                if(rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraReverseX);
                }
                this.Prm.ReverseX = value;
                return rtn;
            }
            catch 
            {
                return -10;
            }
        }

        public short SetReverseY(bool value)
        {
            try
            {
                short rtn = this.Executor.SetReverseY(value);
                if (rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraReverseY);
                }
                this.Prm.ReverseY = value;
                return rtn;
            }
            catch
            {
                return -10;
            }
        }
        /// <summary>
        /// 设置为连续模式
        /// </summary>
        /// <returns></returns>
        public short SetContinueMode()
        {
            try
            {
                isTriggerMode = false;
                short rtn = this.Executor.SetContinueMode();
                if (rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraConti);
                }
                return rtn;
            }
            catch
            {
                return -10;
            }
        }
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <returns></returns>
        public short StartGrabing()
        {
            try
            {
                short rtn = this.Executor.StartGrabing();
                if (rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraStartGrabing);
                }
                return rtn;
            }
            catch
            {
                return -10;
            }
        }
        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public short StopGrabing()
        {
            try
            {
                short rtn = this.Executor.StopGrabing();
                if (rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraStopGrabing);
                }
                return rtn;
            }
            catch
            {
                return -10;
            }
        }

        public short SetTriggerMode(bool isHardTrigger)
        {
            try
            {
                short rtn = this.Executor.SetTriggerMode();
                if (isHardTrigger)
                {
                    rtn = this.Executor.SetHardTriggerSource();
                    this.Prm.TrigSrc = TrigSrcType.Ex;
                    isTriggerMode = true;
                }
                else
                {
                   rtn = this.Executor.SetSoftTriggerSource();
                    this.Prm.TrigSrc = TrigSrcType.Soft;
                }
                if (rtn != 0)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoVision.ErrorCameraSetTrigger);
                }
                imageBufferIndex = 0;
                KeyValuePair<int, byte[]> imageBytes;
                //清空缓存队列
                while (ImageByteBuffer.TryDequeue(out imageBytes)){}
                return rtn;
            }
            catch
            {
                return -10;
            }
        }
        /// <summary>
        /// 硬触发缓存回调函数
        /// </summary>
        /// <param name="imageBytes"></param>
        public void SaveImageBufferQueue(byte[] imageBytes)
        {
            if (isTriggerMode)//没启用硬触发模式不缓存图像
            {
                this.ImageByteBuffer.Enqueue(new KeyValuePair<int, byte[]>(imageBufferIndex,imageBytes));
                Log.Dprint("Cache Image:" + imageBufferIndex);
                imageBufferIndex++;
            }
        }

        public PointD ToMachine(PointD imgp)
        {
            return this.ToMachine(imgp.X, imgp.Y);
        }

        public PointD ToMachine(double imgx, double imgy)
        {
            double x = 0, y = 0;
            CalibBy9d.ConvertImg2Phy(ref imgx, ref imgy, ref x, ref y);
            PointD rtn = new PointD(-x, -y);
            //if (this.Prm.ReverseX)
            //{
            //    rtn.X *= -1;
            //}
            //if (this.Prm.ReverseY)
            //{
            //    rtn.Y *= -1;
            //}
            return rtn;
        }

        public Bitmap TriggerAndGetBmp(TimeSpan timeout)
        {
            return TriggerAndReply(timeout).Item2;
        }

        public byte[] TriggerAndGetBytes(TimeSpan timeout)
        {
            return TriggerAndReply(timeout).Item1;
        }

        public Tuple<byte[], Bitmap> TriggerAndReply(TimeSpan timeout)
        {
            this.Executor.BmpGrabedEvnet.Reset();
            //触发拍照
            this.Executor.GrabOne();
            if (!this.Executor.BmpGrabedEvnet.WaitOne(timeout))
            {
                return null;
            }
            return new Tuple<byte[], Bitmap>(this.Executor.CurrentBytes, this.Executor.CurrentBmp);
        }
    }
}
