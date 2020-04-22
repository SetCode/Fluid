using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision
{
    [Serializable]
    public abstract class MarkFindPrmBase : VisionFindPrmBase
    {
        /// <summary>
        /// Mark点图像坐标
        /// </summary>
        [NonSerialized]
        public PointD MarkInImg = new PointD();

        /// <summary>
        /// Mark点2图像坐标(ASV 非标mark输出的第二个点)
        /// </summary>
        [NonSerialized]
        public PointD MarkInImg2 = new PointD();

        /// <summary>
        /// 转换后的目标位置
        /// </summary>
        [NonSerialized]
        public PointD TargetInMachine = new PointD();

        /// <summary>
        /// 转换后的目标位置2
        /// </summary>
        [NonSerialized]
        public PointD TargetInMachine2 = new PointD();

        /// <summary>
        /// 角度，用于位置+角度的纠偏方式
        /// </summary>
        [NonSerialized]
        public double Angle;

        /// <summary>
        /// 用于判断是否是非标Mark
        /// </summary>
        public bool IsUnStandard = false;

        /// <summary>
        /// 判断非标mark输出结果类新星
        /// 0：1个点+角度输出
        /// 1：2个点输出
        /// </summary>
        public int UnStandardType = 0;

        /// <summary>
        /// 检测流程Key，用于非标Mark，从5开始
        /// </summary>
        public int InspectionKey = 5;

        /// <summary>
        /// 非标Mark参考角度(1点+角度模式)
        /// </summary>
        public double ReferenceA;

        /// <summary>
        /// 非标Mark参考点X1
        /// </summary>
        public double ReferenceX;

        /// <summary>
        /// 非标Mark参考点Y1
        /// </summary>
        public double ReferenceY;

        /// <summary>
        /// 非标Mark参考点X2
        /// </summary>
        public double ReferenceX2;

        /// <summary>
        /// 非标Mark参考点Y2
        /// </summary>
        public double ReferenceY2;

        /// <summary>
        /// 目标位置与注册位置偏差范围
        /// </summary>
        public double Tolerance { get; set; } = 2;

        public bool IsOutOfTolerance()
        {
            if(IsUnStandard)
            {
                return false;
            }
            else
            {
                if (PosInMachine.DistanceTo(TargetInMachine) > Tolerance)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public abstract bool Init();

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <returns></returns>
        public abstract override bool Execute();
    }
}
