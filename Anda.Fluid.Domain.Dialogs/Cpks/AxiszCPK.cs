
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Cpk;
using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    public class AxisZCPK:CpkBase
    {
        public AxisZCPK(Sheet sheet, CpkPrm prm) : base(sheet, prm.AxisZSpec)
        {
            this.CpkPrm = prm;
        }
      
        private double pointTrans;
        public double ZStart;
        public double ZEnd;
        public PointD Pos = new PointD(0, 0);
        public PointD SafePos = new PointD(0, 0);
        private bool isStop = false;
        public override void Execute(CpkPrm prm)
        {
            this.CpkPrm = prm;
            this.Specf = prm.AxisZSpec;
            if (prm.ZPointsNum < 30)
            {
                MessageBox.Show("输入的执行点数{0}小于30 !!!，请重新输入执行点数", "", MessageBoxButtons.OKCancel);
                return;
            }
           
            this.moveAndRead();
            if (dataInput.Count != this.CpkPrm.YPointsNum)
            {
                return;
            }
            SaveDataToExl();
        }
        public override void Stop()
        {
            this.isStop = true;
        }

        public double calPoint()
        {
            double zl = this.ZEnd - this.ZStart;            
            double distanceZ = zl / 10;
            pointTrans = this.ZEnd - distanceZ*5;
            return pointTrans;
        }

       
        private void moveAndRead()
        {
            Result res = Result.OK;
            double height;
            this.isStop = false;
            dataInput.Clear();
            for (int i = 0; i < this.CpkPrm.ZPointsNum; i++)
            {
                
                res = CpkMove.MoveZHigh(ZStart);
                if (res != Result.OK)
                {
                    return;
                }
                if (this.isStop)
                {
                    this.isStop = false;
                    break;
                }
                res = CpkMove.MoveZHigh(pointTrans);
                if (!res.IsOk)
                {
                    return;
                }
                res = CpkMove.MoveZSlowly(ZEnd);
                if (!res.IsOk)
                {
                    return;
                }
                Thread.Sleep(3000);
                if (this.CpkMeasureType == 1)
                {
                    //读取高度
                    Result result = Machine.Instance.MeasureHeight(out height);
                    if (!result.IsOk)
                    {
                        return;
                    }
                }
                else
                {
                    //读取高度
                    int retCode = Machine.Instance.DigitalGage.DigitalGagable.ReadHeight(out height);
                    if (retCode != 0)
                    {
                        return;
                    }
                }
                dataInput.Add(height);
            }
        }

        public override void SaveDataToExl()
        {
            ////CPK测试报告(X轴定位精度)
            sheet.SetOneCellValue(1, "A", "CPK测试报告(Z轴定位精度)");
            sheet.SetCellStyle(1, "A", new FontStyle()
            {
                color = HSSFColor.Black.Index,
                size = 20,
                Align = Alignments.中中

            });

            sheet.CreateRegion(1, "A", 4, "K");
            sheet.HideRows(5, 2);
            ////测试对象:HD-XXX(XXX）
            sheet.SetOneCellValue(7, "A", "测试对象:HD-XXX");
            sheet.CreateRegion(7, "A", 7, "E");

            //////机身编码：XXXX
            sheet.SetOneCellValue(7, "F", "机身编码：XXXX");
            sheet.CreateRegion(7, "F", 7, "K");

            ////测试方法:	在所有参数固定的情况下重复测试Y轴定位精度,来检查设备的稳定性	
            sheet.SetOneCellValue(9, "A", "测试方法:");
            sheet.SetOneCellValue(9, "B", "在所有参数固定的情况下重复测试X轴定位精度,来检查设备的稳定性	");
            sheet.CreateRegion(9, "B", 9, "K");

            sheet.SetOneCellValue(10, "A", "测试参数:");
            sheet.SetOneCellValue(10, "B", "空行程速度1000mm/s   运行速度:800mm/s   加速度:6000000P/P/s");
            sheet.CreateRegion(10, "B", 10, "K");

            //测量仪量:
            sheet.SetOneCellValue(11, "A", "测量仪量:");
            sheet.SetOneCellValue(11, "B", "激光测高");
            sheet.CreateRegion(11, "B", 11, "C");

            ////记录数据            
            Location recordLoc = new Location(31, "B");
            recordLoc.ExlIndexToNpIndex();
            ////数据
            Location dataLoc = new Location();
            dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            dataLoc.NpIndexToExlIndex();

            this.AddRecord(recordLoc, dataInput);
            /***********************计算结果*********************************/
            Location resLoc = new Location(12, "A");
            this.AddFormula(resLoc, dataLoc);
            sheet.SetColumnWidth("A", 15);
            /**************************************************************/
            string path = @"CPK.xls";
            this.sheet.VirWorkBook.Save(path);
        }
    }
}
