using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_OPTControllerAPI;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    public class LightingOPT : ILighting
    {
        private OPTControllerAPI opt;
        /// <summary>
        /// OPT光源构造函数
        /// </summary>
        /// <param name="mode">0：串口连接，1：IP地址连接，2：SN连接</param>
        /// <param name="config">对应串口号，IP地址，SN序列号三种连接方式</param>
        public LightingOPT(int mode, string config)
        {
            opt = new OPTControllerAPI();
            this.Mode = mode;
            this.Config = config;
        }
        /// <summary>
        /// 连接模式
        /// </summary>
        public int Mode { get; private set; }
        /// <summary>
        /// 连接配置
        /// </summary>
        public string Config { get; private set; }

        /// <summary>
        /// 连接光源 0：串口连接，1：IP地址连接，2：SN连接
        /// </summary>
        public bool Connect(TimeSpan timeout)
        {
            int ret = 0;
            switch (this.Mode)
            {
                case 0:
                    ret = opt.InitSerialPort(this.Config);
                    break;
                case 1:
                    ret = opt.CreateEtheConnectionByIP(this.Config);
                    break;
                case 2:
                    ret = opt.CreateEtheConnectionBySN(this.Config);
                    break;
                default:
                    ret = opt.CreateEtheConnectionByIP(this.Config);
                    break;
            }
            return (ret == 0);
        }

        /// <summary>
        /// 断开连接，0：串口1：网口
        /// </summary>
        public void Disconnect()
        {
            int ret = 0;
            switch (this.Mode)
            {
                case 0:
                    ret = opt.ReleaseSerialPort();
                    break;
                case 1:
                    ret = opt.DestoryEtheConnect();
                    break;
                default:
                    ret = opt.ReleaseSerialPort();
                    break;
            }
        }
        /// <summary>
        /// 获取各个通道连接状态
        /// </summary>
        /// <param name="lightType">通道类型</param>
        /// <returns>-1：函数执行失败，0：已连接光源，1：没有连接光源，2：短路保护3：过压保护4：过流保护</returns>
        public int GetChannelState(LightChn lightType)
        {
            int i = 0;
            unsafe
            {
                int* p = &i;
                int ret = opt.GetChannelState(SwitchChannel(lightType), p);
                if (ret == 0)
                {
                    return i;
                }
                return -1;
            }
        }
        /// <summary>
        /// 切换通道
        /// </summary>
        /// <param name="lightType"></param>
        /// <returns></returns>
        private int SwitchChannel(LightChn lightType)
        {
            int channel = 0;
            switch (lightType)
            {
                case LightChn.Red:
                    channel = 1;
                    break;
                case LightChn.Green:
                    channel = 2;
                    break;
                case LightChn.Blue:
                    channel = 3;
                    break;
                default:
                    break;
            }
            return channel;
        }
        /// <summary>
        /// 打开单个通道
        /// </summary>
        /// <param name="lightType"></param>
        /// <returns></returns>
        public int TurnOnChannel(LightChn lightType)
        {
            return opt.TurnOnChannel(SwitchChannel(lightType));
        }

        public int TurnOnMultiChannel(int[] ChannelArray, int len)
        {
            return opt.TurnOnMultiChannel(ChannelArray, len);
        }
        /// <summary>
        /// 关闭单个通道
        /// </summary>
        /// <param name="lightType"></param>
        /// <returns></returns>
        public int TurnOffChannel(LightChn lightType)
        {
            return opt.TurnOffChannel(SwitchChannel(lightType));
        }
        /// <summary>
        /// 关闭多通道
        /// </summary>
        /// <param name="ChannelArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int TurnOffMultiChannel(int[] ChannelArray, int len)
        {
            return opt.TurnOffMultiChannel(ChannelArray, len);
        }
        /// <summary>
        /// 设置单个通道亮度
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetIntensity(LightChn lightType, int Value)
        {
            return opt.SetIntensity(SwitchChannel(lightType), Value);
        }
        /// <summary>
        /// 设置多通道亮度
        /// </summary>
        /// <param name="IntensityArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int SetMultiIntensity(Intensity[] IntensityArray, int len)
        {
            OPTControllerAPI.IntensityItem[] arr = new OPTControllerAPI.IntensityItem[len];
            for (int i = 0; i < IntensityArray.Length; i++)
            {
                arr[i].channel = IntensityArray[i].channel;
                arr[i].intensity = IntensityArray[i].intensity;
            }
            return opt.SetMultiIntensity(arr, len);
        }
        /// <summary>
        /// 读取某个通道亮度
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public int ReadIntensity(LightChn lightType)
        {
            int Value = 0;
            opt.ReadIntensity(SwitchChannel(lightType), ref Value);
            return Value;
        }
        /// <summary>
        /// 设置触发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetTriggerWidth(LightChn lightType, int Value)
        {
            return opt.SetTriggerWidth(SwitchChannel(lightType), Value);
        }
        /// <summary>
        /// 设置多个通道触发脉宽
        /// </summary>
        /// <param name="TriggerWidthArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int SetMultiTriggerWidth(TriggerWidth[] TriggerWidthArray, int len)
        {
            OPTControllerAPI.TriggerWidthItem[] arr = new OPTControllerAPI.TriggerWidthItem[len];
            for (int i = 0; i < TriggerWidthArray.Length; i++)
            {
                arr[i].channel = TriggerWidthArray[i].channel;
                arr[i].triggerWidth = TriggerWidthArray[i].triggerWidth;
            }
            return opt.SetMultiTriggerWidth(arr, len);
        }
        /// <summary>
        /// 读取触发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int ReadTriggerWidth(LightChn lightType, ref int Value)
        {
            return opt.ReadTriggerWidth(SwitchChannel(lightType), ref Value);
        }
        /// <summary>
        /// 设置高亮触发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetHBTriggerWidth(LightChn lightType, int Value)
        {
            return opt.SetHBTriggerWidth(SwitchChannel(lightType), Value);
        }
        /// <summary>
        /// 设置多个通道高亮触发脉宽
        /// </summary>
        /// <param name="HBTriggerWidthArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int SetMultiHBTriggerWidth(HBTriggerWidth[] HBTriggerWidthArray, int len)
        {
            OPTControllerAPI.HBTriggerWidthItem[] arr = new OPTControllerAPI.HBTriggerWidthItem[len];
            for (int i = 0; i < HBTriggerWidthArray.Length; i++)
            {
                arr[i].channel = HBTriggerWidthArray[i].channel;
                arr[i].HBTriggerWidth = HBTriggerWidthArray[i].Width;
            }
            return opt.SetMultiHBTriggerWidth(arr, len);
        }
        /// <summary>
        /// 读取单个通道高亮触发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int ReadHBTriggerWidth(LightChn lightType, ref int Value)
        {
            return opt.ReadHBTriggerWidth(SwitchChannel(lightType), ref Value);
        }
        /// <summary>
        /// 是否函数有返回值
        /// </summary>
        /// <param name="isResponse"></param>
        /// <returns></returns>
        public int EnableResponse(bool isResponse)
        {
            return opt.EnableResponse(isResponse);
        }
        /// <summary>
        /// 是否允许断电备份
        /// </summary>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public int EnablePowerOffBackup(bool isSave)
        {
            return opt.EnablePowerOffBackup(isSave);
        }
        /// <summary>
        /// 读取设备序列号
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        public int ReadSN(StringBuilder SN)
        {
            return opt.ReadSN(SN);
        }
        /// <summary>
        /// 读取IP配置
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="subnetMask"></param>
        /// <param name="defaultGateway"></param>
        /// <returns></returns>
        public int ReadIPConfig(StringBuilder IP, StringBuilder subnetMask, StringBuilder defaultGateway)
        {
            return opt.ReadIPConfig(IP, subnetMask, defaultGateway);
        }
        /// <summary>
        /// 设置单个通道最大电流
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetMaxCurrent(LightChn lightType, int Value)
        {
            return opt.SetMaxCurrent(SwitchChannel(lightType), Value);
        }
        /// <summary>
        /// 设置多个通道最大电流
        /// </summary>
        /// <param name="maxCurrentArray"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int SetMultiMaxCurrent(MaxCurrent[] maxCurrentArray, int length)
        {
            OPTControllerAPI.TriggerWidthItem[] arr = new OPTControllerAPI.TriggerWidthItem[length];
            for (int i = 0; i < maxCurrentArray.Length; i++)
            {
                arr[i].channel = maxCurrentArray[i].channel;
                arr[i].triggerWidth = maxCurrentArray[i].SoftwareTriggerTime;
            }
            return opt.SetMultiTriggerWidth(arr, length);
        }
        /// <summary>
        /// 设置触发极性
        /// </summary>
        /// <param name="triggerActivation"></param>
        /// <returns></returns>
        public int SetTriggerActivation(int triggerActivation)
        {
            return opt.SetTriggerActivation(triggerActivation);
        }
        /// <summary>
        /// 读取触发极性
        /// </summary>
        /// <param name="triggerActivation"></param>
        /// <returns></returns>
        public int ReadTriggerActivation(ref int triggerActivation)
        {
            return opt.ReadTriggerActivation(ref triggerActivation);
        }
        /// <summary>
        /// 设置工作模式
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public int SetWorkMode(int workMode)
        {
            return opt.SetWorkMode(workMode);
        }
        /// <summary>
        /// 读取工作模式
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public int ReadWorkMode(ref int workMode)
        {
            return opt.ReadWorkMode(ref workMode);
        }
        /// <summary>
        /// 软触发
        /// </summary>
        /// <param name="channelIdx"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int SoftwareTrigger(LightChn lightType, int time)
        {
            return opt.SoftwareTrigger(SwitchChannel(lightType), time);
        }
        /// <summary>
        /// 多通道软触发
        /// </summary>
        /// <param name="softwareTriggerArray"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int MultiSoftwareTrigger(MaxCurrent[] softwareTriggerArray, int length)
        {
            OPTControllerAPI.TriggerWidthItem[] arr = new OPTControllerAPI.TriggerWidthItem[length];
            for (int i = 0; i < softwareTriggerArray.Length; i++)
            {
                arr[i].channel = softwareTriggerArray[i].channel;
                arr[i].triggerWidth = softwareTriggerArray[i].SoftwareTriggerTime;
            }
            return opt.SetMultiTriggerWidth(arr, length);
        }
    }
}
