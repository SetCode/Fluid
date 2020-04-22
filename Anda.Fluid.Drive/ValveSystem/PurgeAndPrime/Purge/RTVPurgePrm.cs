using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge
{
    public class RTVPurgePrm
    {
        private static RTVPurgePrm instance = new RTVPurgePrm();

        private static string pathRTVPurgePrm = SettingsPath.PathMachine + "\\" + typeof(RTVPurgePrm).Name;
        public static RTVPurgePrm Instance => instance;

        private RTVPurgePrm() { }

        public static void Save()
        {
            JsonUtil.Serialize(pathRTVPurgePrm, instance);
        }

        public static void Load()
        {
            instance = JsonUtil.Deserialize<RTVPurgePrm>(pathRTVPurgePrm);
            if (instance == null)
            {
                instance = new RTVPurgePrm();
            }
        }

        /// <summary>
        /// 用以作为清洗矩阵的线(起点和终点)集合
        /// </summary>
        public List<Tuple<PointD, PointD>> Lines { get; set; } = new List<Tuple<PointD, PointD>>();

        /// <summary>
        /// 每条清洗线重复执行的次数
        /// </summary>
        public int Cycles { get; set; } = 1;

        /// <summary>
        /// 第一条清洗线的起点
        /// </summary>
        public PointD LineStart { get; set; } = new PointD();

        /// <summary>
        /// 第一条清洗线的终点
        /// </summary>
        public PointD LineEnd { get; set; } = new PointD();

        /// <summary>
        /// 阵列x方向(0:X轴正向，1：X轴负向)
        /// </summary>
        public int ArrayXDirection { get; set; } = 0;

        /// <summary>
        /// 阵列x方向(0:Y轴正向，1：Y轴负向)
        /// </summary>
        public int ArrayYDirection { get; set; } = 0;

        /// <summary>
        /// 阵列X方向间隔
        /// </summary>
        public double ArrayXInterval { get; set; } = 1;

        /// <summary>
        /// 阵列Y方向间隔
        /// </summary>
        public double ArrayYInterval { get; set; } = 1;

        /// <summary>
        /// 阵列X方向清洗线数量
        /// </summary>
        public int ArrayXCounts { get; set; } = 1;

        /// <summary>
        /// 阵列Y方向清洗线数量
        /// </summary>
        public int ArrayYCounts { get; set; } = 1;

        /// <summary>
        /// 清洗时的速度
        /// </summary>
        public double Vel { get; set; } = 5;

        /// <summary>
        /// 清洗时的高度
        /// </summary>
        public double posZ { get; set; } = 0;

        /// <summary>
        /// 到位开胶延时（ms）
        /// </summary>
        public int DispenseDelay { get; set; } = 0;

        /// <summary>
        /// 当前清洗线在矩阵中的编号
        /// </summary>
        public int CurrLineIndex { get; set; } = 0;

        /// <summary>
        /// 当前清洗线的重复执行次数
        /// </summary> 
        public int CurrLineCycle { get; set; } = 0;

    }

}
