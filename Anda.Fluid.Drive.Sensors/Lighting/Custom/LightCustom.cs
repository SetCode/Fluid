using Anda.Fluid.Infrastructure.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Lighting.Custom
{
    public class LightCustom : LightingCom,ILightingController, ILighting
    {
        //控制器
        public LightVendor lightVendor { get; } = LightVendor.Custom;
       public LightCustom(EasySerialPort easySerialPort):base(easySerialPort)
        {

        }

        public ExecutePrmCustom CurPrm { get; private set; }
     
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public override bool Connect(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public override void Disconnect()
        {
            throw new NotImplementedException();
        }

        public int EnablePowerOffBackup(bool isSave)
        {
            throw new NotImplementedException();
        }

        public int EnableResponse(bool isResponse)
        {
            throw new NotImplementedException();
        }

        public int GetChannelState(LightChn lightType)
        {
            throw new NotImplementedException();
        }

        public int MultiSoftwareTrigger(MaxCurrent[] softwareTriggerArray, int length)
        {
            throw new NotImplementedException();
        }

       

        public int ReadHBTriggerWidth(LightChn lightType, ref int Value)
        {
            throw new NotImplementedException();
        }

        public int ReadIntensity(LightChn lightType)
        {
            throw new NotImplementedException();
        }

        public int ReadIPConfig(StringBuilder IP, StringBuilder subnetMask, StringBuilder defaultGateway)
        {
            throw new NotImplementedException();
        }

        public int ReadSN(StringBuilder SN)
        {
            throw new NotImplementedException();
        }

        public int ReadTriggerActivation(ref int triggerActivation)
        {
            throw new NotImplementedException();
        }

        public int ReadTriggerWidth(LightChn lightType, ref int Value)
        {
            throw new NotImplementedException();
        }

        public int ReadWorkMode(ref int workMode)
        {
            throw new NotImplementedException();
        }

       

        public int SetHBTriggerWidth(LightChn lightType, int Value)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置单个通道亮度
        /// </summary>
        /// <param name="lightType"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetIntensity(LightChn lightType, int Value)
        {
            throw new NotImplementedException();
        }

        public int SetMaxCurrent(LightChn lightType, int Value)
        {
            throw new NotImplementedException();
        }

        public int SetMultiHBTriggerWidth(HBTriggerWidth[] HBTriggerWidthArray, int len)
        {
            throw new NotImplementedException();
        }

        public int SetMultiIntensity(Intensity[] IntensityArray, int len)
        {
            throw new NotImplementedException();
        }

        public int SetMultiMaxCurrent(MaxCurrent[] maxCurrentArray, int length)
        {
            throw new NotImplementedException();
        }

        public int SetMultiTriggerWidth(TriggerWidth[] TriggerWidthArray, int len)
        {
            throw new NotImplementedException();
        }

        public int SetTriggerActivation(int triggerActivation)
        {
            throw new NotImplementedException();
        }

        public int SetTriggerWidth(LightChn lightType, int Value)
        {
            throw new NotImplementedException();
        }

        public int SetWorkMode(int workMode)
        {
            throw new NotImplementedException();
        }

        public int SoftwareTrigger(LightChn lightType, int time)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 关闭单通道
        /// </summary>
        /// <param name="lightType"></param>
        /// <returns></returns>
        public int TurnOffChannel(LightChn lightType)
        {
            throw new NotImplementedException();
        }

        public int TurnOffMultiChannel(int[] ChannelArray, int len)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 打开单通道
        /// </summary>
        /// <param name="lightType"></param>
        /// <returns></returns>
        public int TurnOnChannel(LightChn lightType)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 打开多通道
        /// </summary>
        /// <param name="ChannelArray"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int TurnOnMultiChannel(int[] ChannelArray, int len)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 关闭所有通道
        /// </summary>
        public void None()
        {
            
            throw new NotImplementedException();
        }

        public void ResetToLast()
        {
            return;
            //this.SetLight(this.CurPrm);
        }
        public void SetLight(ExecutePrm prm)
        {
            //this.CurPrm = prm.PrmCustom;
            return;

            //foreach (var item in executePrm.ChanelDic.Keys)
            //{
            //    if (executePrm.ChanelDic[item].On)
            //    {

            //    }
            //}
            
            //this.CurPrm = executePrm;
        }

        private void setLight()
        {

        }

        public void SetChanel(Chanels chanel,bool on, int value)
        {
                   

            //设置亮度
        }

    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class ExecutePrmCustom
    {
        [JsonProperty]
        public Dictionary<Chanels, Itensity> ChanelDic { get; private set; } = new Dictionary<Chanels, Itensity>();
        public ExecutePrmCustom()
        {
            Itensity itensity = new Itensity() { On = false, Value = 0 };
           
            this.ChanelDic.Add(Chanels.Chanel1, itensity);
            this.ChanelDic.Add(Chanels.Chanel2, itensity);
            this.ChanelDic.Add(Chanels.Chanel3, itensity);
            this.ChanelDic.Add(Chanels.Chanel4, itensity);
        }

        public void SetChanel(Chanels chanel, bool on, int value)
        {
            if (this.ChanelDic.Keys.Contains(chanel))
            {
                Itensity itensity = new Itensity() { On=on,Value=value};                
                this.ChanelDic[chanel] = itensity;
            }
        }
    }

    public enum Chanels
    {
        None=-1,
        Chanel1 = 1,
        Chanel2,
        Chanel3,
        Chanel4
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class ItensityClass:ICloneable
    {
        [JsonProperty]
        public Chanels Chanel;
        [JsonProperty]
        public bool On;
        [JsonProperty]
        public int Value;
        public object Clone()
        {
            return (ItensityClass)this.MemberwiseClone();
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public struct Itensity
    {
        [JsonProperty]
        public Chanels Chanel;
        [JsonProperty]
        public bool On;
        [JsonProperty]
        public int Value;
       
    }

}
