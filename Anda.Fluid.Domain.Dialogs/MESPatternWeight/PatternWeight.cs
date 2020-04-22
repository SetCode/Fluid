using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.MESPatternWeight
{
    public class PatternWeight
    {
        public  int shotNums = 0;
        public  Valve Valve;
        private string patterName;
        public string PatternName
        {
            get { return patterName; }
            set { this.patterName = value; }
        }
        private RunnableModule runnableModule;

        public void DoPatternWeight()
        {
            Executor.Instance.ShotNums = 0;
            if (String.IsNullOrEmpty(patterName))
                return;
            if (this.runnableModule==null)
                return;
            Machine.Instance.Robot.MoveSafeZAndReply();
            this.Valve.MoveToScaleLoc();
            Valve.DoWeight(spray);
            this.shotNums = Executor.Instance.ShotNums;
        }
        private void spray()
        {
            Executor.Instance.executePatWgt(this.Valve,this.runnableModule.CmdList, DateUtils.CurrTimeInMills);
        }
        /// <summary>
        /// 
        /// </summary>
        public RunnableModule GetRunnableMoudleByPattern()
        {   
            foreach (RunnableModule rb in FluidProgram.Current.ModuleStructure.GetAllRunnableModules())
            {
                if (rb.CommandsModule.Name==this.patterName)
                {
                    this.runnableModule = rb;
                    return rb;
                }
            }
            return this.runnableModule=null;           
        }

        private double getPatWgt(int shotNums)
        {
            return shotNums * FluidProgram.Current.RuntimeSettings.SingleDropWeight;
        }

        private void recdPatternWeight()
        {
            string strheadline = "时间,型号,实际重量(mg),重量范围(mg),状态,NG原因";
            string line = String.Empty;
            //记录拼版重量CSV
            if (!File.Exists(MesSettings.FilePathMatrixWeight))
            {
                //时间	型号	 实际重量(mg)	重量范围(mg)	状态	NG原因                
                CsvUtil.WriteLine(MesSettings.FilePathDotWeight, strheadline);
            }
            else
            {
                //line = String.Format("{0},{1},{2},{3},{4}", DateTime.Now.ToString(), FluidProgram.Current.Name, this.Valve.weightPrm.DifferWeight, (getPatWgt(this.shotNums)*(1- this.Valve.weightPrm.WeightOffset/100.0d)).ToString()+"-"+ (getPatWgt(this.shotNums) * (1 + this.Valve.weightPrm.WeightOffset / 100.0d)).ToString(), this.Valve.CaliPatWgtResult());
                //CsvUtil.WriteLine(MesSettings.FilePathDotWeight, line);
            }
            if (!String.IsNullOrEmpty(MesSettings.FilePathMatrixWeightUser))
            {
                if (!File.Exists(MesSettings.FilePathMatrixWeightUser))
                {
                    CsvUtil.WriteLine(MesSettings.FilePathMatrixWeightUser, strheadline);
                }
                else
                {
                    CsvUtil.WriteLine(MesSettings.FilePathDotWeight, line);
                }

            }
        }
        //记录单点重量CSV
        private void recDotWeight()
        {
            //Time	NozzalNumber	DotWeight
            string strheadline = "Time,NozzalNumber,DotWeight";
            string line = String.Empty;
            if (!File.Exists(MesSettings.FilePathDotWeight))
            {
                //时间	型号	 实际重量(mg)	重量范围(mg)	状态	NG原因                
                CsvUtil.WriteLine(MesSettings.FilePathDotWeight, strheadline);
            }
            else
            {
                line = String.Format("{0},{1},{2}}", DateTime.Now.ToString(), ((ValueType)this.Valve.Key).ToString(), this.Valve.weightPrm.SingleDotWeight);
                CsvUtil.WriteLine(MesSettings.FilePathDotWeight, line);
            }
            if (!String.IsNullOrEmpty(MesSettings.FilePathDotWeightUser))
            {
                if (!File.Exists(MesSettings.FilePathDotWeightUser))
                {
                    CsvUtil.WriteLine(MesSettings.FilePathDotWeightUser, strheadline);
                }
                else
                {
                    CsvUtil.WriteLine(MesSettings.FilePathDotWeightUser, line);
                }

            }
        }
    }
}
