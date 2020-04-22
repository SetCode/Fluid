using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Domain.Settings
{
    /// <summary>
    /// 运行时生成并使用的配置参数
    /// </summary>
    [Serializable]
    public class RuntimeSettings : ICloneable
    {
        #region 清洗相关参数

        /// <summary>
        /// 程序开始运行时是否要先清洗阀
        /// </summary>
        public bool PurgeBeforeStart { get; set; }

        /// <summary>
        /// 是否按板数自动清洗
        /// </summary>
        public bool IsAutoPurgeCount { get; set; } 

        /// <summary>
        /// 自动清洗：生产一定数量产品
        /// </summary>
        public int AutoPurgeCount { get; set; } = 10;

        /// <summary>
        /// 是否按时间自动清洗
        /// </summary>
        public bool IsAutoPurgeSpan { get; set; } 

        /// <summary>
        /// 自动清洗：间隔一定时间
        /// </summary>
        public TimeSpan AutoPurgeSpan { get; set; } = TimeSpan.FromMinutes(15);

        #endregion


        #region 称重相关参数

        /// <summary>
        /// 程序开始运行时是否要先称重
        /// </summary>
        public bool ScaleBeforeStart { get; set; }

        /// <summary>
        /// 单滴胶水的重量
        /// </summary>
        public double SingleDropWeight { get; set; }
        /// <summary>
        /// 胶量点数计算是否使用四舍五入计算方式
        /// </summary>
        public bool isHalfAdjust { get; set; } = false;
		
        /// <summary>
        /// 是否同步单点
        /// </summary>
        public bool IsSyncSingleDropWeight { get; set; } = true;
        

        /// <summary>
        /// 是否按板数自动称重
        /// </summary>
        public bool IsAutoScaleCount { get; set; }

        /// <summary>
        /// 自动称重：生产一定数量产品
        /// </summary>
        public int AutoScaleCount { get; set; } = 10;

        /// <summary>
        /// 是否按时间自动称重
        /// </summary>
        public bool IsAutoScaleSpan { get; set; }

        /// <summary>
        /// 自动称重：间隔一定时间
        /// </summary>
        public TimeSpan AutoScaleSpan { get; set; } = TimeSpan.FromMinutes(15);

        #endregion

        #region 浸泡相关参数
        /// <summary>
        /// 是否按时间间隔浸泡
        /// </summary>
        public bool IsAutoSoakSpan { get; set; } = false;

        /// <summary>
        /// 自动浸泡：空闲状态下，在指定的时间内没有来板就去浸泡
        /// </summary>
        public TimeSpan AutoSoakSpan { get; set; } = TimeSpan.FromMinutes(1);
        #endregion

        #region 测高相关参数

        private double maxTolerance = 4;
        private double minTolerance = -4;

        /// <summary>
        /// 测量基板高度时的激光测高值，默认值为系统设置中的值
        /// </summary>
        public double StandardBoardHeight { get; set; }

        /// <summary>
        /// 激光测高距板高度上限
        /// </summary>
        public double MaxTolerance
        {
            get { return this.maxTolerance; }
            set
            {
                if (this.maxTolerance < 0)
                {
                    throw new Exception("The Max Board Delta must bigger than 0.");
                }
                this.maxTolerance = value;
            }
        }

        /// <summary>
        /// 激光测高距板高度下限
        /// </summary>
        public double MinTolerance
        {
            get { return this.minTolerance; }
            set
            {
                if (this.minTolerance > 0)
                {
                    throw new Exception("The Min Board Delta must smaller than 0.");
                }
                this.minTolerance = value;
            }
        }

        /// <summary>
        /// 测高位置X
        /// </summary>
        public double HeightPosX { get; set; }

        /// <summary>
        /// 测高位置Y
        /// </summary>
        public double HeightPosY { get; set; }

        /// <summary>
        /// 阀接触到板时的Z轴绝对坐标， 当激光尺disable时，使用该参数
        /// </summary>
        public double BoardZValue { get; set; } = 0;

        #endregion


        #region 硬件相关参数

        private int airPressure = 100;
        private int airPressure2 = 100;
        private double temperature = 50.0;
        private double temperature2 = 50.0;
        private double simulDistence = 44;//现有双阀机构最小间距37左右

        /// <summary>
        /// 阀1气压
        /// </summary>
        public int AirPressure
        {
            get { return this.airPressure; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("The Air Pressure must bigger than 0.");
                }
                this.airPressure = value;
            }
        }
        /// <summary>
        /// 阀2气压
        /// </summar
        public int AirPressure2
        {
            get { return this.airPressure2; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("The Air Pressure must bigger than 0.");
                }
                this.airPressure2 = value;
            }
        }
        /// <summary>
        /// 阀1温度
        /// </summary>
        public double Valve1Temperature
        {
            get { return this.temperature; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("The Temperature must bigger than 0.");
                }
                this.temperature = value;
            }
        }

        /// <summary>
        /// 阀2温度
        /// </summary>
        public double Valve2Temperature
        {
            get { return this.temperature2; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("The Temperature must bigger than 0.");
                }
                this.temperature2 = value;
            }
        }

        /// <summary>
        /// 双阀间距
        /// </summary>
        public double SimulDistence
        {
            get { return simulDistence; }
            set { simulDistence = value; }
        }

        public double SimulOffsetX { get; set; }

        public double SimulOffsetY { get; set; }

        /// <summary>
        /// 双阀模式，暂时用MachineSetting中的
        /// </summary>
        [Obsolete]
        public DualValveMode DualMode { get; set; }

        #endregion


        #region 运行时参数（不需要保存）

        /// <summary>
        /// 检测生产的csv文件路径
        /// </summary>
        [NonSerialized]
        public string FilePathInspectDot;

        [NonSerialized]
        public string FilePathInspectRect;

        #endregion


        #region 飞拍参数
        /// <summary>
        /// 是否调整飞拍偏移
        /// (勾选且在look模式下，自动定拍+飞拍获取偏移量)
        /// </summary>
        [NonSerialized]
        public bool isAdjustFlyOffset  = false;
        /// <summary>
        /// 是否启用飞拍
        /// </summary>
        public bool isFlyMarks { get; set; } = false;
        /// <summary>
        /// 飞行速度
        /// </summary>
        public double FlySpeed { get; set; } = 200;
        /// <summary>
        /// 飞行加速度
        /// </summary>
        public double FlyAcc { get; set; } = 5;
        /// <summary>
        /// 飞行拐弯系数
        /// </summary>
        public double FlyCornerSpeed { get; set; } = 200;
        /// <summary>
        /// 飞行默认行优先
        /// </summary>
        public bool FlyIsRowFirst { get; set; } = true;
        /// <summary>
        /// 飞行空点距离触发点的方向距离
        /// </summary>
        public double FlyPreDistance { get; set; } = 10;
        /// <summary>
        /// 是否使用Mark原始坐标点位飞行，默认使用平滑直线飞行
        /// </summary>
        public bool FlyOriginPos { get; set; } = false;

        public int DisposeThreadCount { get; set; } = 1;
        /// <summary>
        /// 飞行校准参数数组，程序加载后赋值给每个MarkCmd
        /// </summary>
        public List<VectorD> FlyOffsetList = new List<VectorD>();
        /// <summary>
        /// 飞行校准参数是否有效
        /// </summary>
        public bool FlyOffsetIsValid { get; set; } = false;
        /// <summary>
        /// 飞拍中失败的Mark是否使用定拍补拍Mark
        /// </summary>
        public bool IsNGReshoot { get; set; } = false;
        #endregion


        #region Lot Control参数

        ///<summary>
        /// Description	:计划生产总量
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>
        public int LotCount { get; set; } = 0;


        ///<summary>
        /// Description	:Lot管控是否启用ID管控
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public bool IsStartLotById { get; set; } = false;

        ///<summary>
        /// Description	:生产工单号
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public string LotId { get; set; } = "";
        ///<summary>
        /// Description	:当前工单生产者
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>
        public string LotOperator { get; set; } = "";

        ///<summary>
        /// Description	:当前生产产品ID
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public string ProductionId { get; set; } = "";

        ///<summary>
        /// Description	:LotId在productionId中的起始位置
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public int LotIdStartPos { get; set; } = 0;

        ///<summary>
        /// Description	:LotId在productionId中的结束位置
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public int LotIdEndPos { get; set; } = 0;

        ///<summary>
        /// Description	:判断运行状态是否处于LotControl状态
        /// Author  	:liyi
        /// Date		:2019/06/26
        ///</summary>   
        public bool LotControlStart { get; set; } = false;
        #endregion


        #region 螺杆阀参数
        /// <summary>
        /// 螺杆阀或齿轮泵阀的当前转速(r/s)
        /// </summary>
        [NonSerialized]
        public int SvOrGearValveCurrSpeed;

        /// <summary>
        /// 螺杆阀或齿轮泵阀的不同速度对应的胶量
        /// <example>
        /// Tkey = 1(r/s),TValue = 1(mg).
        /// </example>
        /// </summary>
        public Dictionary<int, double> VavelSpeedDic { get; set; } = new Dictionary<int, double>();

        #endregion


        #region Program执行参数

        /// <summary>
        /// 编辑界面点运行时，程序的执行次数
        /// </summary>
        [NonSerialized]
        public int OfflineCycle = 0;

        public bool SaveMarkImages { get; set; }

        public bool SaveBadMarkImages { get; set; }
                
        public bool SaveMeasureMentImages { get; set; }

        public bool AutoSkipNgMarks { get; set; }

        public bool MarksSort { get; set; }

        public bool Back2WorkpieceOrigin { get; set; } = true;

        public bool MeasureCmdsSort = false;

        public Custom.CustomEnum Custom { get; set; } = Domain.Custom.CustomEnum.Default;

        public Custom.CustomParam CustomParam { get; set; } = new Domain.Custom.CustomParam();

        #endregion


        #region 连续前瞻

        public FluidMoveMode FluidMoveMode { get; set; }

        public double LookTime { get; set; } = 3;

        public double LookAccMax { get; set; } = 2;

        public int LookCount { get; set; } = 10;

        #endregion



        public object Clone()
        {
            RuntimeSettings setting = (RuntimeSettings)this.MemberwiseClone();
            setting.AutoPurgeSpan = TimeSpan.FromMilliseconds(this.AutoPurgeSpan.TotalMilliseconds);
            setting.AutoScaleSpan = TimeSpan.FromMilliseconds(this.AutoScaleSpan.TotalMilliseconds);
            setting.FlyOffsetList.Clear();
            foreach (var item in this.FlyOffsetList)
            {
                setting.FlyOffsetList.Add((VectorD)item.Clone());
            }
            setting.VavelSpeedDic.Clear();
            foreach (var item in this.VavelSpeedDic.Keys)
            {
                setting.VavelSpeedDic.Add(item, this.VavelSpeedDic[item]);
            }
            return setting;
        }
    }
}
