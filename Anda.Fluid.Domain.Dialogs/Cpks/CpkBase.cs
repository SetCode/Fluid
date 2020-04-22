using Anda.Fluid.Infrastructure.Cpk;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    public abstract class CpkBase:ICPKable
    {

        public  CpkPrm CpkPrm { get; set; }
        public ArrayList dataInput { get; set; } = new ArrayList();
        public ArrayList SpecfArr { get; set; } = new ArrayList(3);
        public Specifications Specf { get; set; }

        public int CpkMeasureType { get; set; } = 0;        

        public CpkBase()
        { }
        public CpkBase(Sheet sheet)
        {
            this.sheet = sheet;
        }
        public CpkBase(Sheet sheet,Specifications specf):this(sheet)
        {
            this.Specf = specf;
        }
       
        public CpkBase(double[] sample,double usl, double lsl)
        {
            this.sample = sample;
            this.usl = usl;
            this.lsl = lsl;
            this.sl = (usl + lsl) / 2;
        }

       
        //样本
        private double[] sample;

        //规格上限
        private double usl;
        public double USL => usl;

        //规格下限
        private double lsl;
        public double LSL => lsl;


        //规格中心
        private double sl;
        //public double SL { get { return } }
        
        //平均值
        public double MeanX;//mean
        
        
        //样本标准差
        public double Sigma;

        //Ca
        private double ca;
        public double Ca => ca;
        //Cp
        private double cp;
        public double Cp => cp;

        //Cpk
        private double cpk;
        public double Cpk => cpk;


        protected Sheet sheet;

        public abstract void Execute(CpkPrm prm);
        public abstract void Stop();

        public abstract void SaveDataToExl(); 
       
        public void PrmSynchronization()
        {
            if (this.Specf!=null)
            {

            }
            this.SpecfArr.Clear();                   
            this.SpecfArr.Add(this.Specf.Center);
            this.SpecfArr.Add(this.Specf.USL);
            this.SpecfArr.Add(this.Specf.LSL);
        }
        private int count=0;
        public void AddRecord(Location recordLoc, ArrayList dataInput)
        {
            this.count = dataInput.Count;
            if (this.count <= 0)
            {
                this.count = 0;
            }
            recordLoc.ExlIndexToNpIndex();

            //记录数据

            sheet.SetOneCellValue(recordLoc.NPRowIndex - 4, recordLoc.NPColIndex - 1, "记录数据");
            sheet.SetCellStyle(recordLoc.NPRowIndex - 4, recordLoc.NPColIndex - 1, new FontStyle()
            {
                size = 16,
                Align = Alignments.中中
            });
            sheet.CreateRegion(recordLoc.NPRowIndex - 4, recordLoc.NPColIndex - 1, recordLoc.NPRowIndex - 2, recordLoc.NPColIndex + 9);

            //Title
            //Location recordLoc = new Location(32, "B");
            //recordLoc.ExlIndexToNpIndex();


            sheet.SetOneCellValue(recordLoc.NPRowIndex, recordLoc.NPColIndex, "序号");

            sheet.SetOneCellValue(recordLoc.NPRowIndex, recordLoc.NPColIndex + 4, "定位值");

            sheet.SetRowStyle(recordLoc.NPRowIndex, recordLoc.NPColIndex, 5, new FontStyle()
            {
                size = 13,
                Align = Alignments.中中
            });
            //序号
            ArrayList dataIndex = new ArrayList(this.count);
            for (int i = 0; i < this.count; i++)
            {
                dataIndex.Add(i + 1);
            }
            //清空以前数据
            this.sheet.NewColumn(recordLoc.NPRowIndex + 1, recordLoc.NPColIndex, this.count);
            sheet.SetColumValue(recordLoc.NPRowIndex + 1, recordLoc.NPColIndex, dataIndex, new FontStyle()
            {
                Align = Alignments.右中
            });

            //数据
            //Location dataLoc = new Location();
            //dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            //dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            //dataLoc.NpIndexToExlIndex();

            int dataLocR= recordLoc.NPRowIndex + 1;
            int dataLocC= recordLoc.NPColIndex + 4;
            sheet.SetColStyle(dataLocR, dataLocC, this.count, new FontStyle()
            {
                Align = Alignments.右中,
                dataFormat = "0.0000"
            });
            //设置一列数据
            sheet.SetColumValue(dataLocR, dataLocC, dataInput, new FontStyle()
            {
                Align = Alignments.右中
            });


            //最后行
            sheet.SetOneCellValue(recordLoc.NPRowIndex + this.count + 1, recordLoc.NPColIndex, "制表：");
            sheet.SetOneCellValue(recordLoc.NPRowIndex + this.count + 1, recordLoc.NPColIndex + 3, "测试技术员：");
            sheet.SetOneCellValue(recordLoc.NPRowIndex + this.count + 1, recordLoc.NPColIndex + 6, "日期:");
        }

       
        public void AddFormula(Location resLoc, Location dataLoc)
        {
            //将规格类转化为规格参数数组
            this.PrmSynchronization();
            resLoc.ExlIndexToNpIndex();
            ArrayList specName = new ArrayList();
            specName.AddRange(new string[] { "Nominal", "UPPER LIMIT", "LOWER LIMIT" });
            sheet.SetColumValue(resLoc.NPRowIndex, resLoc.NPColIndex, specName, new FontStyle()
            {
                size = 8,
                fontType = "Arial",
                Align = Alignments.右中
            });
            

            sheet.SetColumValue(resLoc.NPRowIndex, resLoc.NPColIndex + 3, this.SpecfArr, new FontStyle()
            {
                size = 12,
                fontType = "宋体",
                Align = Alignments.右中,
                dataFormat = "0.0000"
            });
            

            ArrayList arrRes = new ArrayList();
            arrRes.AddRange(new string[] { "AVERAGE", "STD", "", "Cp", "", "Cpku", "CpkL", "Cpk", "", "MIN", "MAX" });

            sheet.SetColumValue(resLoc.NPRowIndex + 4, resLoc.NPColIndex, arrRes, new FontStyle()
            {

                Align = Alignments.右中
            });

            Location fomulaLoc = new Location(resLoc.NPRowIndex + 4, resLoc.NPColIndex + 3);
            //Console.WriteLine(resLoc.NPRowIndex.ToString() + resLoc.NPColIndex.ToString());
            //"AVERAGE(F32:F61)" dataLoc
            sheet.SetColStyle(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex, 11, new FontStyle()
            {
                Align = Alignments.右中
            });
            //平均值
            //string[] formulaStr = new string[] { "AVERAGE(F32:F61)", "STDEV(F32:F61)" , ""};
            string averageStr = string.Format("AVERAGE({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + this.count-1, dataLoc.NPColIndex));
            //Console.WriteLine(averageStr);

            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex, averageStr);

            //方差 
            string stdStr = string.Format("STDEV({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + this.count - 1, dataLoc.NPColIndex));
            //Console.WriteLine(averageStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex, stdStr);

            //CPK
            //string cpStr = "+((D13-D14)/(6*D17))";            
            string cpStr = string.Format("+(({0}-{1})/(6*{2}))", Location.GetRC(fomulaLoc.NPRowIndex - 3, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex - 2, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex));
            //Console.WriteLine(cpStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 3, fomulaLoc.NPColIndex, cpStr);
            //CPK U
            //string cpkuStr = "(D13-D16)/(3*D17)";
            string cpkuStr = string.Format("(({0}-{1})/(3*{2}))", Location.GetRC(fomulaLoc.NPRowIndex - 3, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex));
            //Console.WriteLine(cpkuStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 5, fomulaLoc.NPColIndex, cpkuStr);

            //CPK L
            //rowoff = 6;
            //string cpklStr = "(D16-D14)/(3*D17)";
            string cpklStr = string.Format("(({0}-{1})/(3*{2}))", Location.GetRC(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex - 2, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex));
            //Console.WriteLine(cpklStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 6, fomulaLoc.NPColIndex, cpklStr);

            //CPK U CPK L 的最小值
            //string cpkStr = "MIN(D21:D22)";

            string cpkStr = string.Format("MIN({0}:{1})", Location.GetRC(fomulaLoc.NPRowIndex + 5, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 6, fomulaLoc.NPColIndex));
            //Console.WriteLine(cpkStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 7, fomulaLoc.NPColIndex, cpkStr);

            //最小值
            //string[] minmaxStr = new string[] { "MIN(F32:F61)", "MAX(F32:F61)" };
            string minStr = string.Format("MIN({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + this.count - 1, dataLoc.NPColIndex));
            //Console.WriteLine(minStr);

            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 9, fomulaLoc.NPColIndex, minStr);
            //最大值
            string maxStr = string.Format("MAX({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + this.count - 1, dataLoc.NPColIndex));
            //Console.WriteLine(maxStr);

            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 10, fomulaLoc.NPColIndex, maxStr);
        }


    }
}
