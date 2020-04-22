using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using System.ComponentModel;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    public class RobotXYZPrm 
    {
        public double OrgX { get; set; }

        public double OrgY { get; set; }

        /// <summary>
        /// Z轴安全高度
        /// </summary>
        [DisplayName("Z轴安全高度")]
        public double SafeZ { get; set; }

        /// <summary>
        /// X轴回零参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("X轴回零参数")]
        public MoveHomePrm HomePrmX { get; set; }

        /// <summary>
        /// Y轴回零参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("Y轴回零参数")]
        public MoveHomePrm HomePrmY { get; set; }

        /// <summary>
        /// Z轴回零参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("Z轴回零参数")]
        public MoveHomePrm HomePrmZ { get; set; }

        /// <summary>
        /// Z轴回零离开正限位的参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("Z轴回零离开正限位的参数")]
        public EscapeLmtPrm EscapeLmtPrmZ { get; set; }

        /// <summary>
        /// X轴Jog运动参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("X轴Jog运动参数")]
        public MoveJogPrm JogPrmX { get; set; }

        /// <summary>
        /// Y轴Jog运动参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("Y轴Jog运动参数")]
        public MoveJogPrm JogPrmY { get; set; }

        /// <summary>
        /// Z轴Jog运动参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("Z轴Jog运动参数")]
        public MoveJogPrm JogPrmZ { get; set; }

        /// <summary>
        /// 连续差补初始化参数
        /// </summary>
        [Browsable(false)]
        [DisplayName("连续差补初始化参数")]
        public MoveTrcPrm TrcPrmXY { get; set; }

        /// <summary>
        /// 联动加速度pulse/ms2
        /// </summary>
        [DefaultValue(3)]
        [DisplayName("联动加速度pulse/ms2")]
        public double AccXY { get; set; }

        /// <summary>
        /// 胶量模式最大限速mm/s
        /// </summary>
        [DefaultValue(50)]
        [DisplayName("胶量模式最大限速mm/s")]
        public double WeightMaxVel { get; set; }

        /// <summary>
        /// 胶量模式加速度
        /// </summary>
        [DefaultValue(5)]
        [DisplayName("胶量模式加速度")]
        public double WeightAcc { get; set; }

        /// <summary>
        /// 速度模式下工作速度
        /// </summary>
        [DefaultValue(200)]
        [DisplayName("速度模式下工作速度")]
        public double WorkVel { get; set; }

        /// <summary>
        /// 速度模式下空行程速度
        /// </summary>
        [DefaultValue(200)]
        [DisplayName("速度模式下空行程速度")]
        public double EmptyVel { get; set; }

        /// <summary>
        /// 喷胶电磁阀相应时间ms
        /// </summary>
        [DefaultValue(1.5)]
        [DisplayName("喷胶电磁阀响应时间ms")]
        public double DispensingSpan { get; set; }

        /// <summary>
        /// 2D比较误差带
        /// </summary>
        [DefaultValue(200)]
        [DisplayName("2D比较误差带")]
        public int Tolerance2DCmp { get; set; }

        public RobotXYZPrm Default()
        {
            //axis x move home param
            this.HomePrmX = new MoveHomePrm()
            {
                Mode = HomeMode.Limit_Index,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 20,
                Acc = 100,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 20
            };
            //axis y move home param
            this.HomePrmY = new MoveHomePrm()
            {
                Mode = HomeMode.Limit_Index,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 20,
                Acc = 100,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 20
            };
            //axis z move home param
            this.HomePrmZ = new MoveHomePrm()
            {
                Mode = HomeMode.Limit_Index,
                MoveDir = 1,
                IndexDir = -1,
                VelHigh = 100,
                VelLow = 20,
                Acc = 100,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 20
            };
            //axis z escape positive limit param before move home
            this.EscapeLmtPrmZ = new EscapeLmtPrm()
            {
                Lmt = LmtType.Positive,
                Step = -20,
                Vel = 50,
                Acc = 100
            };
            
            //axes move jog param
            this.JogPrmX = new MoveJogPrm() { Vel = 50, Acc = 100 };
            this.JogPrmY = new MoveJogPrm() { Vel = 50, Acc = 100 };
            this.JogPrmZ = new MoveJogPrm() { Vel = 20, Acc = 100 };

            //axes move pos param
            

            return this;
        }
    }

}
