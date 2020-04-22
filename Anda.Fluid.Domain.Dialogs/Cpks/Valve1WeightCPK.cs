

using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Cpk;
using NPOI.HSSF.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    public class Valve1WeightCPK:CpkBase
    {
        public Valve1WeightCPK(Sheet sheet, CpkPrm prm) : base(sheet, prm.Valve1Spec)
        {
            this.CpkPrm = prm;
		}

        private double[] weight;

        public override void Execute(CpkPrm prm)
        {
            this.CpkPrm = prm;
            this.Specf = prm.Valve1Spec;
            Machine.Instance.Valve1.WeightCpk(this.CpkPrm.MeasureTimes, this.CpkPrm.Cycles, this.CpkPrm.Interval, out weight);
            dataInput.Clear();
            dataInput.AddRange(weight);
            #region 测CPK bug
            //Random r = new Random();
            //double value = 0;
            //dataInput.Clear();
            //for (int i = 0; i < 32; i++)
            //{
            //    value = r.NextDouble();
            //    dataInput.Add(10 + value);
            //}
            #endregion 
            Thread.Sleep(1000);            
            SaveDataToExl();
        }

        public override void Stop()
        {
            Machine.Instance.Valve1.WeightCpkStop();
        }
        public override void SaveDataToExl()
        {
            ////CPK测试报告(喷射阀点胶量)
            sheet.SetOneCellValue(1, "A", "CPK测试报告(喷射阀1点胶量)");

            sheet.SetCellStyle(1, "A", new FontStyle()
            {
                color = HSSFColor.Black.Index,
                size = 20,
                Align = Alignments.中中

            });
          
            sheet.CreateRegion(1, "A", 4, "K");

            ////测试对象:HD-XXX(XXX）
            sheet.SetOneCellValue(7, "A", "测试对象:HD-XXX(喷射阀）");
            sheet.CreateRegion(7, "A", 7, "E");

            //////机身编码：XXXX
            sheet.SetOneCellValue(7, "F", "机身编码：XXXX");
            sheet.CreateRegion(7, "F", 7, "K");
            sheet.HideRows(5,2);

            ////测试方法:	在所有参数固定的情况下重复测试Y轴定位精度,来检查设备的稳定性		
            sheet.SetOneCellValue(9, "A", "测试方法:");
            sheet.SetOneCellValue(9, "B", "在所有参数固定的情况下重复测试点胶量,以100个点为准，单位为mg	");
            sheet.CreateRegion(9, "B", 9, "K");

            sheet.SetOneCellValue(10, "A", "测试参数:");
            sheet.SetOneCellValue(10, "B", "喷射阀参数:(开阀气压:XXXXMPa 开胶时间： Xms  关胶时间： Xms  单点次数：X ） 胶筒气压: XMpa");
            sheet.CreateRegion(10, "B", 10, "K");
            
            //胶水
            sheet.SetOneCellValue(11, "A", "胶水温度：X°");
            sheet.SetOneCellValue(11, "C", "胶水型号：XXXXX");
            sheet.CreateRegion(11,"A", 11, "B");
            sheet.CreateRegion(11,"C", 11, "D");

            //测量仪量:
            sheet.SetOneCellValue(12, "A", "测量仪量:");
            sheet.SetOneCellValue(12, "B", "千分尺");
            sheet.CreateRegion(12, "B", 12, "C");
            
            ////记录数据            
            Location recordLoc = new Location(32, "B");
            recordLoc.ExlIndexToNpIndex();
            ////数据
            Location dataLoc = new Location();
            dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            dataLoc.NpIndexToExlIndex();

            this.AddRecord(recordLoc, dataInput);

            /***********************计算结果*********************************/
            Location resLoc = new Location(13, "A");
            this.AddFormula(resLoc, dataLoc);
            sheet.SetColumnWidth("A", 15);
            /**************************************************************/
            string path = @"CPK.xls";
            this.sheet.VirWorkBook.Save(path);
        }

    }
}
