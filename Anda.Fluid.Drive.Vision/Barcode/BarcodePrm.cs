using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Anda.Fluid.Drive.Vision.Barcode
{
    [Serializable]
    public class BarcodePrm : VisionFindPrmBase,ICloneable
    {
        //[NonSerialized]
        //public PointD TargetInMachine;

        /// <summary>
        /// 条码识别从10开始  ASV
        /// </summary>
        public int InspectionKey = 15;

        public int MinLength = 8;

        public int MaxLength = 32;

        public FindBarCodeType FindBarCodeType = FindBarCodeType.ASV;

        [NonSerialized]
        public string Text = string.Empty;

        public override bool Execute()
        {
            bool flag = false;
            if (this.FindBarCodeType == FindBarCodeType.ASV)
            {
                Inspection inspection = InspectionMgr.Instance.FindBy(this.InspectionKey);
                if (inspection == null)
                {
                    return false;
                }
                inspection.Execute(this.ImgData, this.ImgWidth, this.ImgHeight);
                if (!inspection.IsCurrResultOk)
                {
                    return false;
                }
                this.Text = inspection.CurrResultStr;
                if (this.Text.Length > this.MaxLength || this.Text.Length < this.MinLength)
                {
                    return false;
                }
                flag=true;
            }
            else if (this.FindBarCodeType == FindBarCodeType.AFM)
            {
                BarcodeHalcon barcode = new BarcodeHalcon();
                flag= barcode.Execute(this.Bmp, out this.Text);
            }
            return flag;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
    public enum FindBarCodeType
    {
        AFM,
        ASV
    }
    

}
