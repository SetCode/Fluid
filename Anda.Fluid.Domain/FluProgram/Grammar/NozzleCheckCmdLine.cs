using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive.Vision.GrayFind;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    ///<summary>
    /// Description	:喷嘴出胶检测语法指令
    /// Author  	:liyi
    /// Date		:2019/07/17
    ///</summary>   
    [Serializable]
    public class NozzleCheckCmdLine : CmdLine
    {
        /// <summary>
        /// 用于判定是全局检测还是每个拼版检测
        /// </summary>
        [CompareAtt("CMP")]
        public bool isGlobal { get; set; } = true;

        [CompareAtt("CMP")]
        private NozzleCheckStyle nozzleStyle = NozzleCheckStyle.Valve1;
        /// <summary>
        /// 胶阀检测类型（valve1、valve2、both）
        /// </summary>
        [CompareAtt("CMP")]
        public NozzleCheckStyle NozzleStyle { get { return nozzleStyle; } }
        /// <summary>
        /// 所使用的点参数类型
        /// </summary>
        [CompareAtt("CMP")]
        public DotStyle DotStyle = DotStyle.TYPE_1;

        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        [CompareAtt("CMP")]
        public bool IsWeightControl = false;
        [CompareAtt("CMP")]
        private double weight = 0;
        /// <summary>
        /// 如果开启了重量控制，该参数指定重量值，单位：mg
        /// </summary>
        public double Weight
        {
            set
            {
                weight = value < 0 ? 0 : value;
            }
            get
            {
                return weight;
            }
        }

        public ModelFindPrm ModelFindPrm = new ModelFindPrm();

        public GrayCheckPrm GrayCheckPrm = new GrayCheckPrm();
        [CompareAtt("CMP")]
        public CheckThm CheckThm;
        [CompareAtt("CMP")]
        public bool IsOkAlarm;
        [CompareAtt("CMP")]
        public PointD Position { get; private set; } = new PointD();

        public NozzleCheckCmdLine(NozzleCheckStyle Style = NozzleCheckStyle.Valve1) : base(true)
        {
            this.nozzleStyle = Style;
        }
        public override object Clone()
        {
            NozzleCheckCmdLine nozzleCheckCmdLine = MemberwiseClone() as NozzleCheckCmdLine;
            nozzleCheckCmdLine.Position = Position.Clone() as PointD;
            return nozzleCheckCmdLine;
        }
        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="patternOldOrigin">Pattern原点被校正前的位置</param>
        /// <param name="coordinateTransformer">根据Mark点拍摄结果生成的坐标校正器</param>
        /// <param name="patternNewOrigin">Pattern原点被校正后的位置</param>
        public override void Correct(PointD patternOldOrigin, CoordinateTransformer coordinateTransformer, PointD patternNewOrigin)
        {
            // 校正前的机械坐标
            PointD p = (patternOldOrigin.ToSystem() + Position).ToMachine();
            // 校正后的机械坐标
            p = coordinateTransformer.Transform(p);
            // 相对系统坐标
            Position.X = (p.ToSystem() - patternNewOrigin.ToSystem()).X;
            Position.Y = (p.ToSystem() - patternNewOrigin.ToSystem()).Y;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("Nozzle Check : ").Append(MachineRel(Position));
            string temp;
            if (IsOkAlarm)
            {
                temp = " OK Alarm";
            }
            else
            {
                temp = " NG Alarm";
            }
            return sb.Append(temp).ToString();
        }
    }
}
