using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    public class RobotDefaultPrm : ICloneable
    {
        //private const string CategoryWeightMode = "Weight Mode";
        //private const string CategoryVelMode = "Speed Mode";
        //private const string CategoryGlobal = "Global";
        //private const string CategoryManulMode = "Manual";
        private const string CategoryWeightMode = "胶量模式";
        private const string CategoryVelMode = "速度模式";
        private const string CategoryGlobal = "全局参数";
        private const string CategoryManulMode = "手动参数";

        #region Global

        [ReadOnly(true)]
        [Category(CategoryGlobal)]
        [DisplayName("Z轴最低位置")]
        [Description("mm")]
        [DefaultValue(-5)]
        public double MinZ { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("联动最大速度")]
        [Description("mm/s")]
        [DefaultValue(1000)]
        public double MaxVelXY { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("联动最大加速度")]
        [Description("pulse/ms2")]
        [DefaultValue(10)]
        public double MaxAccXY { get; set; }

        /// <summary>
        /// 联动速度
        /// </summary>
        [Category(CategoryGlobal)]
        [DisplayName("XY联动速度")]
        [Description("mm/s")]
        [DefaultValue(200)]
        public double VelXY { get; set; }

        /// <summary>
        /// 联动加速度
        /// </summary>
        [Category(CategoryGlobal)]
        [DisplayName("XY联动加速度")]
        [Description("pulse/ms2")]
        [DefaultValue(5)]
        public double AccXY { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("AB联动速度")]
        [Description("mm/s")]
        [DefaultValue(50)]
        public double VelAB { get; set; }

        /// <summary>
        /// 联动加速度
        /// </summary>
        [Category(CategoryGlobal)]
        [DisplayName("AB联动加速度")]
        [Description("pulse/ms2")]
        [DefaultValue(3)]
        public double AccAB { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("XYAB联动速度")]
        [Description("mm/s")]
        [DefaultValue(50)]
        public double VelXYAB { get; set; }

        /// <summary>
        /// 联动加速度
        /// </summary>
        [Category(CategoryGlobal)]
        [DisplayName("XYAB联动加速度")]
        [Description("pulse/ms2")]
        [DefaultValue(3)]
        public double AccXYAB { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("Z轴速度")]
        [Description("mm/s")]
        [DefaultValue(100)]
        public double VelZ { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("R轴速度")]
        [Description("角度/s")]
        [DefaultValue(100)]
        public double VelR { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("U轴速度")]
        [Description("角度/s")]
        [DefaultValue(100)]
        public double VelU { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("启用到位信号")]
        [Description("INP")]
        [DefaultValue(false)]
        public bool EnableINP { get; set; }

        [Category(CategoryGlobal)]
        [DisplayName("启用棋盘校正")]
        [Description("INP")]
        [DefaultValue(false)]
        public bool EnableMap { get; set; }
        
      

        #endregion


        #region Weight Mode

        /// <summary>
        /// 胶量模式最大限速
        /// </summary>
        [Category(CategoryWeightMode)]
        [DisplayName("最大速度")]
        [Description("mm/s")]
        [DefaultValue(50)]
        public double WeightMaxVel { get; set; }

        /// <summary>
        /// 胶量模式加速度
        /// </summary>
        [Category(CategoryWeightMode)]
        [DisplayName("加速度")]
        [Description("pulse/ms2")]
        [DefaultValue(5)]
        public double WeightAcc { get; set; }

        #endregion


        #region Speed Mode

        [Category(CategoryVelMode)]
        [DisplayName("最大速度")]
        [Description("mm/s")]
        [DefaultValue(500)]
        public double WorkMaxVel { get; set; }

        /// <summary>
        /// 速度模式工作速度
        /// </summary>
        [Browsable(false)]
        [Category(CategoryVelMode)]
        [DisplayName("工作速度")]
        [Description("mm/s")]
        [DefaultValue(200)]
        public double WorkVel { get; set; }

        /// <summary>
        /// 速度模式空行程速度
        /// </summary>
        [Browsable(false)]
        [Category(CategoryVelMode)]
        [DisplayName("空行程速度")]
        [Description("mm/s")]
        [DefaultValue(200)]
        public double EmptyVel { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region ManulMode

        [Category(CategoryManulMode)]
        [DisplayName("XY手动联动加速度")]
        [Description("pulse/ms2")]
        [DefaultValue(3)]
        public double ManulAccXY { get; set; }
        

        [Category(CategoryManulMode)]
        [DisplayName("XY手动联动速度")]
        [Description("mm/s")]
        [DefaultValue(50)]
        public double ManualVelXY { get; set; }


        #endregion
    }
}
