using Anda.Fluid.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CpkPrm
    {
        private const string CategoryValve1 = "阀1点胶量CPK规格参数";
        private const string CategoryValve2 = "阀2点胶量CPK规格参数";
        private const string CategoryAxisX = "轴X定位精度CPK规格参数";
        private const string CategoryAxisY = "轴Y定位精度CPK规格参数";
        private const string CategoryAxisXY = "XY轴联动定位精度CPK规格参数";
        private const string CategoryAxisZ = "轴Z定位精度CPK规格参数";
        private const string CategoryAxisVel = "轴CPK运行速度";


        [JsonProperty]
        [Category("\t阀点胶量")]
        [DisplayName("\t单组点胶点数")]
        [Description("mg")]
        [DefaultValue(100)]
        public short Cycles { get; set; }

        [JsonProperty]
        [Category("\t阀点胶量")]
        [DisplayName("\t\t测试组数")]
        [Description("mg")]
        [DefaultValue(30)]
        public int MeasureTimes { get; set; }

        [JsonProperty]
        [Category("\t阀点胶量")]
        [DisplayName("\t\t两次清洗间隔组数")]
        [Description("进行指定组数的点胶后清洗")]
        [DefaultValue(5)]
        public int Interval { get; set;}


        [JsonProperty]
        [Category("\t阀点胶量")]
        [DisplayName(CategoryValve1)]
        [Description("mg")]
        [DefaultValue(0)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Specifications Valve1Spec { get; set; } = new Specifications(typeof(Valve1WeightCPK).Name);


        [JsonProperty]
        [Category("\t阀点胶量")]
        [DisplayName(CategoryValve2)]
        [Description("mg")]
        [DefaultValue(0)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Specifications Valve2Spec { get; set; } = new Specifications(typeof(Valve2WeightCPK).Name);


        [JsonProperty]
        [Category("X轴定位精度CPK参数")]
        [DisplayName("执行次数")]
        [DefaultValue(30)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int XPointsNum { get; set; }

        [JsonProperty]
        [Category("X轴定位精度CPK参数")]
        [DisplayName(CategoryAxisX)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Specifications AxisXSpec { get; set; } = new Specifications(typeof(AxisXCPK).Name);

        [JsonProperty]
        [Category("Y轴定位精度CPK参数")]
        [DisplayName("执行次数")]
        [DefaultValue(30)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int YPointsNum { get; set; }

        [JsonProperty]
        [Category("Y轴定位精度CPK参数")]
        [DisplayName(CategoryAxisY)]       
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Specifications AxisYSpec { get; set; } = new Specifications(typeof(AxisYCPK).Name);

        [JsonProperty]
        [Category("XY轴联动定位精度CPK参数")]
        [DisplayName("执行次数")]
        [DefaultValue(30)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int XYPointsNum { get; set; }

        [JsonProperty]
        [Category("XY轴联动定位精度CPK参数")]
        [DisplayName(CategoryAxisXY)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Specifications AxisXYSpec { get; set; } = new Specifications(typeof(AxisXYCPK).Name);

        [JsonProperty]
        [Category("Z轴定位精度CPK参数")]
        [DisplayName("执行次数")]
        [DefaultValue(30)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int ZPointsNum { get; set; }

        [JsonProperty]
        [Category("Z轴定位精度CPK参数")]
        [DisplayName(CategoryAxisZ)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]

        public Specifications AxisZSpec { get; set; } = new Specifications(typeof(AxisZCPK).Name);


        [JsonProperty]
        [Category(CategoryAxisVel)]       
        [DisplayName("XY轴高速")]
        [Description("mm/s")]
        [DefaultValue(50)]        
        public double CPKVelXYH { get; set; }

        [JsonProperty]
        [Category(CategoryAxisVel)]
        [DisplayName("XY轴低速")]
        [Description("mm/s")]
        [DefaultValue(5)]
        public double CPKVelXYL { get; set; }


        [JsonProperty]
        [Category(CategoryAxisVel)]
        [DisplayName("Z轴高速")]
        [Description("mm/s")]
        [DefaultValue(50)]
        public double CPKVelZH { get; set; }

        [JsonProperty]
        [Category(CategoryAxisVel)]
        [DisplayName("Z轴低速")]
        [Description("mm/s")]
        [DefaultValue(5)]
        public double CPKVelZL { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
    }

    
    public class Specifications
    {
        public string Name;
        public Specifications(string name)
        {
            this.Name = name;
        }
        [DisplayName("规格上限")]
        [Description("大于规格下限，阀点胶量单位为mg，轴定位精度单位为mm")]
        public double USL { get; set; }

        [DisplayName("规格中心")]
        [Description("处于规格下限和上限之间，阀点胶量单位为mg，轴定位精度单位为mm")]
        public double Center { get; set; }
        
        [DisplayName("规格下限")]
        [Description("小于规格上限，阀点胶量单位为mg，轴定位精度单位为mm")]
        public double LSL { get; set; }

        public override string ToString()
        {
            return "";
        }

    }
  


}
