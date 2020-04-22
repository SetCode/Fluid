using Anda.Fluid.Domain.FluProgram.Common;
using System;
using System.Collections.Generic;

namespace Anda.Fluid.Domain.Settings
{
    /// <summary>
    /// 需要存储在program文件中的配置参数
    /// </summary>
    [Serializable]
    public class ProgramSettings : ICloneable
    {
        private List<DotParam> dotParamList;
        /// <summary>
        /// 点参数，固定的10个
        /// </summary>
        public IReadOnlyList<DotParam> DotParamList
        {
            get
            {
                return dotParamList;
            }
        }

        private List<LineParam> lineParamList;
        /// <summary>
        /// 线参数，固定的10个
        /// </summary>
        public IReadOnlyList<LineParam> LineParamList
        {
            get { return lineParamList; }
        }

        public ProgramSettings()
        {
            // 预置固定的10个Dot Type
            dotParamList = new List<DotParam>(10);
            for (int i = 0; i < 10; i++)
            {
                dotParamList.Add(new DotParam());
            }
            // 预置固定的10个Line Type
            lineParamList = new List<LineParam>(10);
            for (int i = 0; i < 10; i++)
            {
                lineParamList.Add(new LineParam());
            }
        }

        /// <summary>
        /// 获取点参数
        /// </summary>
        /// <param name="dotStyle"></param>
        /// <returns></returns>
        public DotParam GetDotParam(DotStyle dotStyle)
        {
            return dotParamList[((int)dotStyle)];
        }

        /// <summary>
        /// 获取线参数
        /// </summary>
        /// <param name="lineStyle"></param>
        /// <returns></returns>
        public LineParam GetLineParam(LineStyle lineStyle)
        {
            return lineParamList[((int)lineStyle)];
        }
        public object Clone()
        {
            ProgramSettings programSettings = new ProgramSettings();
            programSettings.dotParamList.Clear();
            programSettings.lineParamList.Clear();
            for (int i = 0; i < this.dotParamList.Count; i++)
            {
                programSettings.dotParamList.Add((DotParam)this.dotParamList[i].Clone());
            }
            for (int i = 0; i < this.lineParamList.Count; i++)
            {
                programSettings.lineParamList.Add((LineParam)this.lineParamList[i].Clone());
            }
            return programSettings;
        }
    }

    [Serializable]
    public enum FluidMoveMode
    {
        普通,
        连续,
        //连续前瞻
    }
}
