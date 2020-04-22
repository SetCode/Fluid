using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive.Vision.GrayFind;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{

    ///<summary>
    /// Description	:胶阀出胶检测指令
    /// Author  	:liyi
    /// Date		:2019/07/18
    ///</summary>   
    [Serializable]
    public class NozzleCheckCmd : SupportDirectiveCmd
    {
        /// <summary>
        /// 用于判定是全局检测还是每个拼版检测
        /// </summary>
        public bool isGlobal { get; set; } = true;

        private NozzleCheckStyle nozzleStyle = NozzleCheckStyle.Valve1;
        /// <summary>
        /// 胶阀检测类型（valve1、valve2、both）
        /// </summary>
        public NozzleCheckStyle NozzleStyle { get { return nozzleStyle; } }

        private ModelFindPrm modelFindPrm;
        /// <summary>
        /// 模板匹配参数
        /// </summary>
        public ModelFindPrm ModelFindPrm
        {
            get { return modelFindPrm; }
        }

        public GrayCheckPrm GrayCheckPrm { get; private set; }

        public CheckThm CheckThm { get; private set; }

        public bool IsOkAlarm { get; private set; }

        private PointD position;
        /// <summary>
        /// 点位置坐标
        /// </summary>
        public PointD Position
        {
            get { return position; }
        }

        /// <summary>
        /// 所使用的点参数类型
        /// </summary>
        public DotStyle DotStyle = DotStyle.TYPE_1;

        /// <summary>
        /// 是否开启重量控制
        /// </summary>
        public bool IsWeightControl = false;

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
        [NonSerialized]
        private MeasureHeightCmd associatedMeasureHeightCmd = null;
        public MeasureHeightCmd AssociatedMeasureHeightCmd { get { return this.associatedMeasureHeightCmd; } }

        public NozzleCheckCmd(RunnableModule runnableModule, NozzleCheckCmdLine nozzleCheckCmdLine, MeasureHeightCmd mhCmd) : base(runnableModule, nozzleCheckCmdLine)
        {
            // 转换成机械坐标
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            position = structure.ToMachine(runnableModule, nozzleCheckCmdLine.Position);
            DotStyle = nozzleCheckCmdLine.DotStyle;
            modelFindPrm = nozzleCheckCmdLine.ModelFindPrm;
            GrayCheckPrm = nozzleCheckCmdLine.GrayCheckPrm;
            CheckThm = nozzleCheckCmdLine.CheckThm;
            IsOkAlarm = nozzleCheckCmdLine.IsOkAlarm;
            this.associatedMeasureHeightCmd = mhCmd;
            IsWeightControl = nozzleCheckCmdLine.IsWeightControl;
            Weight = nozzleCheckCmdLine.Weight;
            this.nozzleStyle = nozzleCheckCmdLine.NozzleStyle;
            this.isGlobal = nozzleCheckCmdLine.isGlobal;
        }
        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new NozzleCheck(this,coordinateCorrector);
        }

        public Result SpecialExcute(CoordinateCorrector coordinateCorrector)
        {
            return new NozzleCheck(this, coordinateCorrector).CheckExecute();
        }
    }
}
