using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Reflection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Anda.Fluid.Drive.Vision.ASV
{
    [Serializable]
    [XmlInclude(typeof(InspectionDot))]
    [XmlInclude(typeof(InspectionLine))]
    public class Inspection : ICloneable
    {
        public Inspection(int key)
        {
            this.Key = key;
        }

        public Inspection() 
        {

        }

        [CompareAtt("CMP")]
        public int Key;

        [CompareAtt("CMP")]
        public PointD PosInMachine = new PointD();

        [CompareAtt("CMP")]
        public int SettlingTime  = 100;

        [CompareAtt("CMP")]
        public int DwellTime  = 100;

        [CompareAtt("CMP")]
        public int Gain = 300;

        [CompareAtt("CMP")]
        public int ExposureTime = 5000;

        [CompareAtt("CMP")]
        //public LightType LightType = LightType.Coax;
        public ExecutePrm ExecutePrm = new ExecutePrm();

        [NonSerialized]
        public byte[] CurrImgData;

        [NonSerialized]
        public int CurrImgWidth;

        [NonSerialized]
        public int CurrImgHeight;

        [NonSerialized]
        public string CurrResultStr;

        [NonSerialized]
        public bool IsCurrResultOk;
        

        public void ShowEditWindow()
        {
            try
            {
                ASVCore.ShowEditWindow(this.Key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public virtual bool Execute(byte[] imgData, int imgWidth, int imgHeight)
        {
            this.CurrResultStr = string.Empty;
            this.IsCurrResultOk = false;
            byte[] resultBytes = new byte[1024];
            ASVCore.Excute(this.Key, imgData, imgHeight, imgWidth, resultBytes);
            this.CurrImgData = imgData;
            this.CurrImgWidth = imgWidth;
            this.CurrImgHeight = imgHeight;
            this.CurrResultStr = Encoding.Default.GetString(resultBytes);
            try
            {
                if (this.CurrResultStr.Substring(0, 2) == "NG")
                {
                    return false;
                }
                this.IsCurrResultOk = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SetImage(byte[] currImgData, int imgWidth, int imgHeight)
        {
            ASVCore.SetImage(Key, currImgData, imgHeight, imgWidth);
            this.CurrImgData = currImgData;
            this.CurrImgWidth = imgWidth;
            this.CurrImgHeight = imgHeight;
            this.CurrResultStr = string.Empty;
            this.IsCurrResultOk = false;         
        }


        public object Clone()
        {
            Inspection inspection = (Inspection)this.MemberwiseClone();
            inspection.PosInMachine = (PointD)this.PosInMachine.Clone();
            return inspection;
        }
    }
}
