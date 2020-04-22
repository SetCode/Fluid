using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Domain.SoftFunction.LotControl
{
    /// <summary>
    /// Lot Control功能实体类
    /// 读取产品信息，记录产品信息，提供给其他模块的回调函数
    /// </summary>
    public class LotController
    {
        private FluidProgram fluidProgram;
        public LotController(FluidProgram fluidProgram)
        {
            this.fluidProgram = fluidProgram;
        }

        public string ProductionId { get; set; } = "";

        public bool ProductionIdisValid { get; set; } = false;
        /// <summary>
        /// 记录生产信息
        /// </summary>
        public void SaveProductionInfo()
        {
            //todo 保存产品信息
        }
        /// <summary>
        /// 条码读取回调函数
        /// </summary>
        public void ReadProductionId()
        {
            string str;
            Machine.Instance.BarcodeSanncer1.BarcodeScannable.ReadValue(new TimeSpan(0,0,1),out str);
            this.ProductionId = str;
            if (this.fluidProgram.RuntimeSettings.IsStartLotById)
            {
                string temp = StringUtil.GetSubString(this.ProductionId, this.fluidProgram.RuntimeSettings.LotIdStartPos, this.fluidProgram.RuntimeSettings.LotIdEndPos);
                if (temp.Contains(this.fluidProgram.RuntimeSettings.LotId))
                {
                    this.ProductionIdisValid = true;
                }
                else
                {
                    this.ProductionIdisValid = false;
                }
            }
        }
    }
}
