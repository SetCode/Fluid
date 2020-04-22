using Anda.Fluid.Drive.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    public interface ILightingController 
    {
        /// <summary>
        /// 获取通道状态
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        int GetChannelState(LightChn lightType);

        /// <summary>
        /// 打开单通道
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        int TurnOnChannel(LightChn lightType);
        /// <summary>
        /// 打开多通道
        /// </summary>
        /// <param name="ChannelArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        int TurnOnMultiChannel(int[] ChannelArray, int len);
        /// <summary>
        /// 关闭单通道
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        int TurnOffChannel(LightChn lightType);
        /// <summary>
        /// 关闭多通道
        /// </summary>
        /// <param name="ChannelArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        int TurnOffMultiChannel(int[] ChannelArray, int len);
        /// <summary>
        /// 设置单通道亮度
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        int SetIntensity(LightChn lightType, int Value);
        /// <summary>
        /// 设置多通道亮度
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        int SetMultiIntensity(Intensity[] IntensityArray, int len);
        /// <summary>
        /// 读取亮度
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns></returns>
        int ReadIntensity(LightChn lightType);
        /// <summary>
        /// 设置出发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        int SetTriggerWidth(LightChn lightType, int Value);
        /// <summary>
        /// 设置多通道触发脉宽
        /// </summary>
        /// <param name="TriggerWidthArray"></param>
        /// <param name="len"></param>
        int SetMultiTriggerWidth(TriggerWidth[] TriggerWidthArray, int len);
        /// <summary>
        /// 读取触发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        int ReadTriggerWidth(LightChn lightType, ref int Value);
        /// <summary>
        /// 设置高亮触发脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        int SetHBTriggerWidth(LightChn lightType, int Value);
        /// <summary>
        /// 设置多通道高亮触发脉宽
        /// </summary>
        /// <param name="HBTriggerWidthArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        int SetMultiHBTriggerWidth(HBTriggerWidth[] HBTriggerWidthArray, int len);
        /// <summary>
        /// 读取脉宽
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        int ReadHBTriggerWidth(LightChn lightType, ref int Value);
        /// <summary>
        /// 是否需要返回值
        /// </summary>
        /// <param name="isResponse"></param>
        /// <returns></returns>
        int EnableResponse(bool isResponse);
        /// <summary>
        /// 是否需要断电备份
        /// </summary>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int EnablePowerOffBackup(bool isSave);
        /// <summary>
        /// 读取序列号
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        int ReadSN(StringBuilder SN);
        /// <summary>
        /// 读取IP配置
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="subnetMask"></param>
        /// <param name="defaultGateway"></param>
        /// <returns></returns>
        int ReadIPConfig(StringBuilder IP, StringBuilder subnetMask, StringBuilder defaultGateway);
        /// <summary>
        /// 设置最大电流
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        int SetMaxCurrent(LightChn lightType, int Value);
        /// <summary>
        /// 设置多通道最大电流
        /// </summary>
        /// <param name="maxCurrentArray"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int SetMultiMaxCurrent(MaxCurrent[] maxCurrentArray, int length);
        /// <summary>
        /// 设置触发模式
        /// </summary>
        /// <param name="triggerActivation"></param>
        /// <returns></returns>
        int SetTriggerActivation(int triggerActivation);
        /// <summary>
        /// 读取触发模式
        /// </summary>
        /// <param name="triggerActivation"></param>
        /// <returns></returns>
        int ReadTriggerActivation(ref int triggerActivation);
        /// <summary>
        /// 设置工作模式
        /// </summary>
        /// <param name="workMode"></param>
        int SetWorkMode(int workMode);
        /// <summary>
        /// 读取工作模式
        /// </summary>
        /// <param name="workMode"></param>
        int ReadWorkMode(ref int workMode);
        /// <summary>
        /// 软触发
        /// </summary>
        /// <param name="channelIdx"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        int SoftwareTrigger(LightChn lightType, int time);
        /// <summary>
        /// 多通道软触发
        /// </summary>
        /// <param name="softwareTriggerArray"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int MultiSoftwareTrigger(MaxCurrent[] softwareTriggerArray, int length);
    }

    public struct Intensity
    {
        public int channel;
        public int intensity;
    }
    public struct TriggerWidth
    {
        public int channel;
        public int triggerWidth;

    }
    public struct HBTriggerWidth
    {
        public int channel;
        public int Width;
    }
    public struct MaxCurrent
    {
        public int channel;
        public int SoftwareTriggerTime;
    }
}
