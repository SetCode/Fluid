using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Utils;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.SVO
{
    internal class DataSetting
    {
        public static DataSetting Default { get; private set; } = new DataSetting();

        public static void Save()
        {
            JsonUtil.Serialize(typeof(DataSetting).Name + ".ds", Default);
        }

        public static void Load()
        {
            Default = JsonUtil.Deserialize<DataSetting>(typeof(DataSetting).Name + ".ds");
            if (Default == null)
            {
                Default = new DataSetting();
            }
        }

        #region 十步矫正管理
        /// <summary>
        /// 十步校正步骤集合
        /// </summary>
        public List<string> SvoCorrects { get; set; }

        /// <summary>
        /// 十步校正完成步数
        /// </summary>
        public int SvoDoneStep { get; set; }

        /// <summary>
        /// 十步校正是否在软件打开时重新开始
        /// </summary>
        public bool SvoRestartOnSoftStart { get; set; }

        /// <summary>
        /// 十步校正是否在窗体打开时重新开始
        /// </summary>
        public bool SvoRestartOnFormStart { get; set; }
        #endregion

        #region 生产前准备管理
        /// <summary>
        /// 生产前准备校正步骤集合
        /// </summary>
        public List<string> PrepareCorrects { get; set; }

        /// <summary>
        /// 生产前准备校正完成步数
        /// </summary>
        public int PrepareDoneStep { get; set; }

        /// <summary>
        /// 生产前准备校正是否在软件打开时重新开始
        /// </summary>
        public bool PrepareRestartOnSoftStart { get; set; }

        /// <summary>
        /// 生产前准备校正是否在窗体打开时重新开始
        /// </summary>
        public bool PrepareRestartOnFormStart { get; set; }
        #endregion

        #region 自定义校正
        /// <summary>
        /// 自定义校正步骤集合
        /// </summary>
        public List<string> CustomCorrects { get; set; }

        /// <summary>
        /// 自定义校正完成步数
        /// </summary>
        public int CustomDoneStep { get; set; }

        /// <summary>
        /// 自定义校正是否在软件打开时重新开始
        /// </summary>
        public bool CustomRestartOnSoftStart { get; set; }

        /// <summary>
        /// 自定义校正是否在窗体打开时重新开始
        /// </summary>
        public bool CustomRestartOnFormStart { get; set; }
        #endregion

        /// <summary>
        /// 上一次完成的校准步数
        /// </summary>
        public int DoneStepCount { get; set; } = 0;
        public bool IsReStart { get; set; } = false;

        /// <summary>
        /// 机台有几个胶阀
        /// </summary>
        public int VavelNo { get; set; } = 2;

        #region NeedleToCamera

        public PointD Needle1Mark { get; set; } = new PointD();

        public PointD CameraNeedle1Mark { get; set; } = new PointD();

        public PointD Needle2Mark { get; set; } = new PointD();


        #endregion

        #region LaserToCamera

        public PointD LaserMark { get; set; } = new PointD();

        public PointD CameraLaserMark { get; set; } = new PointD();


        #endregion

        #region CameraToHeight

        public PointD HeightCenter { get; set; } = new PointD();

        public PointD HeightSensorCenter { get; set; } = new PointD();

        public PointD HeightCircumferenceP1 { get; set; } = new PointD();

        public PointD HeightCircumferenceP2 { get; set; } = new PointD();

        public PointD HeightCircumferenceP3 { get; set; } = new PointD();


        #endregion

        #region PurgeLocation

        public PointD PurgeCenter { get; set; } = new PointD();

        public PointD PurgeCircumferenceP1 { get; set; } = new PointD();

        public PointD PurgeCircumferenceP2 { get; set; } = new PointD();

        public PointD PurgeCircumferenceP3 { get; set; } = new PointD();
        public decimal PurgeZDistance { get; set; } = 10;

        #endregion

        #region PrimeLocaiton
        public PointD PrimeCenter { get; set; } = new PointD();
        public PointD PrimeCircumferenceP1 { get; set; } = new PointD();
        public PointD PrimeCircumferenceP2 { get; set; } = new PointD();
        public PointD PrimeCircumferenceP3 { get; set; } = new PointD();
        public decimal PrimeZDistance { get; set; } = 10;
        public bool SinglePointToSearch { get; set; } = false;
        #endregion

        #region ScaleLocation

        public PointD ScaleCenter { get; set; } = new PointD();

        public PointD ScaleCircumferenceP1 { get; set; } = new PointD();

        public PointD ScaleCircumferenceP2 { get; set; } = new PointD();

        public PointD ScaleCircumferenceP3 { get; set; } = new PointD();
        public decimal ScaleZDistance { get; set; } = 10;

        #endregion

        #region NeedleJet
        public PointD SubstrateCenter { get; set; } = new PointD();

        public double NeedleJetZ { get; set; } = 0;
        #endregion

        #region SlopreAmongVavel1AndVavel2

        /// <summary>
        /// 胶阀打点模式返回True，戳橡皮泥模式返回False
        /// </summary>
        public bool selectDispenseMode { get; set; } = false;

        public CorrectionAxis selectCorrectionAxis { get; set; } = CorrectionAxis.A轴校正;

        /// <summary>
        /// 戳橡皮泥方式下水平轴校正位置
        /// </summary>
        public PointD HorizontalPosInPlasticine { get; set; } = new PointD();

        /// <summary>
        /// 戳橡皮泥方式下垂直轴校正位置
        /// </summary>
        public PointD VerticalPosInPlasticine { get; set; } = new PointD();

        /// <summary>
        /// 点胶方式下水平轴校正位置
        /// </summary>
        public PointD HorizontalPosInDispense { get; set; } = new PointD();

        /// <summary>
        /// 点胶方式下垂直轴校正位置
        /// </summary>
        public PointD VerticalPosInDispense { get; set; } = new PointD();

        /// <summary>
        /// 戳橡皮泥方式下双轴校正位置
        /// </summary>
        public PointD DoubleAxesPosInPlasticine { get; set; } = new PointD();

        /// <summary>
        /// 点胶方式下双轴校正位置
        /// </summary>
        public PointD DoubleAxesPosInDispense { get; set; } = new PointD();

        public double HorizontalRatio { get; set; } = 1;

        public double VerticalRatio { get; set; } = 1;

        #endregion

        #region 针嘴校准
        public PointD CentorInCross { get; set; }
        public double ZUp;

        public PointD RPointInCross { get; set; }
        public double ZDown;

        #endregion

    }

    internal enum CorrectionAxis
    {
        A轴校正,
        B轴校正,
        A轴和B轴同时校正
    }
}
