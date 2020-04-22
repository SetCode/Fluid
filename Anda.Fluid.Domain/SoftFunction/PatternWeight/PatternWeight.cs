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

namespace Anda.Fluid.Domain.SoftFunction.PatternWeight
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
            DirUtils.CreateDir(string.Format("mes\\{0}", DateTime.Today.ToString("yyyy-MM-dd")));
            PatternWeightSettings.FilePathDotWeight =
                string.Format("mes\\{0}\\DotWeight-{0}.csv",
                DateTime.Today.ToString("yyyy-MM-dd"));
            PatternWeightSettings.FilePathMatrixWeight = string.Format("mes\\{0}\\MatrixWeight-{0}.csv",
                DateTime.Today.ToString("yyyy-MM-dd"));
            if (!string.IsNullOrEmpty(PatternWeightSettings.FileDirUser))
            {
                DirUtils.CreateDir(string.Format("{0}\\{1}", PatternWeightSettings.FileDirUser,DateTime.Today.ToString("yyyy-MM-dd")));
            }
            if (!string.IsNullOrEmpty(PatternWeightSettings.FileDirUser))
            {
                PatternWeightSettings.FilePathDotWeightUser = string.Format("{0}\\{1}\\DotWeight-{1}.csv", PatternWeightSettings.FileDirUser, DateTime.Today.ToString("yyyy-MM-dd"));
                PatternWeightSettings.FilePathMatrixWeightUser = string.Format("{0}\\{1}\\MatrixWeight-{1}.csv", PatternWeightSettings.FileDirUser, DateTime.Today.ToString("yyyy-MM-dd"));
            }          
            //this.recdPatternWeight();
            //this.recDotWeight();
            Executor.Instance.ShotNums = 0;
            if (String.IsNullOrEmpty(patterName))
                return;
            //if (this.runnableModule==null)
            //    return;
            Machine.Instance.Robot.MoveSafeZAndReply();
            this.Valve.MoveToScaleLoc();
            Valve.DoWeight(spray);
            this.shotNums = Executor.Instance.ShotNums;
            this.recdPatternWeight();
            this.recDotWeight();
        }
        private void spray()
        {
            Executor.Instance.executePatternWeight(this.Valve,this.runnableModule.CmdList, DateUtils.CurrTimeInMills);
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
            if (!File.Exists(PatternWeightSettings.FilePathMatrixWeight))
            {
                //时间	型号	 实际重量(mg)	重量范围(mg)	状态	NG原因                
                CsvUtil.WriteLine(PatternWeightSettings.FilePathMatrixWeight, strheadline);
            }
            else
            {
                line = String.Format("{0},{1},{2},{3},{4}", DateTime.Now.ToString(), FluidProgram.Current.Name, this.Valve.weightPrm.DifferWeight, String.Format("{0}-{1}", (getPatWgt(this.shotNums) * (1 - this.Valve.weightPrm.WeightOffset / 100.0d)), (getPatWgt(this.shotNums) * (1 + this.Valve.weightPrm.WeightOffset / 100.0d))), this.CaliPatWgtResult());
                CsvUtil.WriteLine(PatternWeightSettings.FilePathMatrixWeight, line);
            }
            if (!String.IsNullOrEmpty(PatternWeightSettings.FilePathMatrixWeightUser))
            {
                if (!File.Exists(PatternWeightSettings.FilePathMatrixWeightUser))
                {
                    CsvUtil.WriteLine(PatternWeightSettings.FilePathMatrixWeightUser, strheadline);
                }
                else
                {
                    CsvUtil.WriteLine(PatternWeightSettings.FilePathMatrixWeightUser, line);
                }

            }
        }
        //记录单点重量CSV
        private void recDotWeight()
        {
            //Time	NozzalNumber	DotWeight
            string strheadline = "Time,NozzalNumber,DotWeight";
            string line = String.Empty;
            if (!File.Exists(PatternWeightSettings.FilePathDotWeight))
            {
                //时间	型号	 实际重量(mg)	重量范围(mg)	状态	NG原因                
                CsvUtil.WriteLine(PatternWeightSettings.FilePathDotWeight, strheadline);
            }
            else
            {
                line = String.Format("{0},{1},{2}", DateTime.Now.ToString(), ((ValueType)this.Valve.Key).ToString(), this.Valve.weightPrm.SingleDotWeight);
                CsvUtil.WriteLine(PatternWeightSettings.FilePathDotWeight, line);
            }
            if (!String.IsNullOrEmpty(PatternWeightSettings.FilePathDotWeightUser))
            {
                if (!File.Exists(PatternWeightSettings.FilePathDotWeightUser))
                {
                    CsvUtil.WriteLine(PatternWeightSettings.FilePathDotWeightUser, strheadline);
                }
                else
                {
                    CsvUtil.WriteLine(PatternWeightSettings.FilePathDotWeightUser, line);
                }

            }
        }

        public  string CaliPatWgtResult()
        {
            if (this.getPatWgt(shotNums) < this.Valve.weightPrm.StandardWeight * (1 - this.Valve.weightPrm.WeightOffset / 100.0d) || this.getPatWgt(shotNums) > this.Valve.weightPrm.StandardWeight * (1 + this.Valve.weightPrm.WeightOffset / 100.0d))
            {
                return "NG";
            }
            return "OK";
        }
    }
}
