using Anda.Fluid.Drive.Sensors.Lighting;
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

namespace Anda.Fluid.App.Setting
{
    [Serializable]
    public class Inspection
    {
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
    }
}
