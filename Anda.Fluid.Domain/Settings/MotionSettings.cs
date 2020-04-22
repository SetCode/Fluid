using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Settings
{
    [Serializable]
    public class MotionSettings
    {
        [Category("XY")]
        [DisplayName("XY联动速度")]
        [Description("mm/s")]
        public double VelXY { get; set; }

        [Category("XY")]
        [DisplayName("XY联动加速度")]
        [Description("pulse/ms^2")]
        public double AccXY { get; set; }

        [Category("XY")]
        [DisplayName("测高速度")]
        [Description("mm/s")]
        public double VelXYHeight { get; set; }

        [Category("XY")]
        [DisplayName("拍照速度")]
        [Description("mm/s")]
        public double VelXYMark { get; set; }

        [Category("Z")]
        [DisplayName("Z轴速度")]
        [Description("mm/s")]
        public double VelZ { get; set; }

        [Category("Z")]
        [DisplayName("Z轴加速度")]
        [Description("pulse/ms^2")]
        public double AccZ { get; set; }

        [Category("XYAB")]
        [DisplayName("XYAB联动速度")]
        [Description("mm/s")]
        public double VelXYAB { get; set; }

        [Category("XYAB")]
        [DisplayName("XYAB联动加速度")]
        [Description("pulse/ms^2")]
        public double AccXYAB { get; set; }

        [Category("胶量模式")]
        [DisplayName("最大速度")]
        [Description("mm/s")]
        public double WeightMaxVel { get; set; }

        [Category("胶量模式")]
        [DisplayName("加速度")]
        [Description("pulse/ms^2")]
        public double WeightAcc { get; set; }

        [Category("速度模式")]
        [DisplayName("最大速度")]
        [Description("mm/s")]
        [DefaultValue(500)]
        public double WorkMaxVel { get; set; }

        [Category("U")]
        [DisplayName("U轴速度")]
        [Description("角度/s")]
        public double VelU { get; set; }

        [Category("U")]
        [DisplayName("U轴加速度")]
        [Description("角度/s^2")]
        public double AccU { get; set; }

        public MotionSettings Default()
        {
            this.VelXY = Machine.Instance.Robot.DefaultPrm.VelXY;
            this.AccXY = Machine.Instance.Robot.DefaultPrm.AccXY;
            this.VelXYHeight = this.VelXY;
            this.VelXYMark = this.VelXY;
            this.VelZ = Machine.Instance.Robot.DefaultPrm.VelZ;
            this.AccZ = Machine.Instance.Robot.AxisZ.Prm.Acc;
            this.VelXYAB = Machine.Instance.Robot.DefaultPrm.VelXYAB;
            this.AccXYAB = Machine.Instance.Robot.DefaultPrm.AccXYAB;
            this.WeightMaxVel = Machine.Instance.Robot.DefaultPrm.WeightMaxVel;
            this.WeightAcc = Machine.Instance.Robot.DefaultPrm.WeightAcc;
            this.WorkMaxVel = Machine.Instance.Robot.DefaultPrm.WorkMaxVel;
            if (Machine.Instance.Setting.AxesStyle == Drive.Motion.ActiveItems.RobotAxesStyle.XYZU
                || Machine.Instance.Setting.AxesStyle == Drive.Motion.ActiveItems.RobotAxesStyle.XYZUV)
            {
                this.VelU = Machine.Instance.Robot.DefaultPrm.VelU;
                this.AccU = Machine.Instance.Robot.AxisU.Prm.Acc;
            }
            return this;
        }
    }

}
