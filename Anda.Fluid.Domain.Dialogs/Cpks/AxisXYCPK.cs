using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Cpk;
using Anda.Fluid.Infrastructure.Utils;
using NPOI.HSSF.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{
    public class AxisXYCPK : CpkBase
    {
        public AxisXYCPK(Sheet sheet, CpkPrm prm) : base(sheet, prm.AxisXYSpec)
        {
            this.CpkPrm = prm;
            this.inspection = InspectionMgr.Instance.FindBy(0) as InspectionDot;
        }

        private PointD pointTrans = new PointD(0, 0);
        public PointD Start = new PointD(0, 0);
        public PointD End = new PointD(0, 0);
        public double PosZ = 0;
        private bool isStop = false;
        private InspectionDot inspection;

        public ArrayList dataInput2 { get; set; } = new ArrayList();

        public override void Execute(CpkPrm prm)
        {
            this.CpkPrm = prm;
            this.Specf = prm.AxisXYSpec;
            if (prm.XYPointsNum < 30)
            {
                MessageBox.Show("输入的执行点数{0}小于30 !!!，请重新输入执行点数", "", MessageBoxButtons.OKCancel);
                return;
            }

            this.moveAndRead();
            if (dataInput.Count != this.CpkPrm.XYPointsNum || dataInput2.Count != this.CpkPrm.XYPointsNum)
            {
                return;
            }
            this.SaveDataToExl();
        }


        public override void SaveDataToExl()
        {
            ////CPK测试报告(X轴定位精度)
            sheet.SetOneCellValue(1, "A", "CPK测试报告(XY轴联动定位精度)");

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
            sheet.SetOneCellValue(11, "B", "千分尺");

            sheet.CreateRegion(11, "B", 11, "C");

            ////记录数据            
            Location recordLoc = new Location(31, "B");
            recordLoc.ExlIndexToNpIndex();
            ////X数据
            Location dataLoc = new Location();
            dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            dataLoc.NpIndexToExlIndex();

            this.AddRecord(recordLoc, dataInput);

            ////Y数据
            Location dataLoc2 = new Location();
            dataLoc2.NPRowIndex = recordLoc.NPRowIndex + 1;
            dataLoc2.NPColIndex = recordLoc.NPColIndex + 5;
            dataLoc2.NpIndexToExlIndex();

            this.AddRecord2(recordLoc, dataInput2);

            /***********************计算结果*********************************/
            Location resLoc = new Location(12, "A");

            this.AddFormula(resLoc, dataLoc);
            this.AddFormula2(resLoc, dataLoc2);

            sheet.SetColumnWidth("A", 15);
            /**************************************************************/
            string path = @"CPK.xls";

            this.sheet.VirWorkBook.Save(path);
        }

        public override void Stop()
        {
            this.isStop = true;
        }

        private bool CaptruePos(out double posX,out double posY)
        {
            //拍照前延时
            Thread.Sleep(1000);
            //拍照获取图像
            byte[] imgData = Machine.Instance.Camera.TriggerAndGetBytes(TimeSpan.FromSeconds(1)).DeepClone();
            //抓圆，得到像素坐标
            bool result = this.inspection.Execute(imgData, Machine.Instance.Camera.Executor.ImageWidth, Machine.Instance.Camera.Executor.ImageHeight);
            double outX = this.inspection.PixResultX;
            double outY = this.inspection.PixResultY;
            double outR = this.inspection.PixResultR;
            //像素坐标转机械坐标
            posX = Machine.Instance.Camera.ToMachine(outX, outY).X;
            posY = Machine.Instance.Camera.ToMachine(outX, outY).Y;
            return result;
        }

        public void AddFormula2(Location resLoc, Location dataLoc)
        {
            //将规格类转化为规格参数数组
            this.PrmSynchronization();
            resLoc.ExlIndexToNpIndex();
            ArrayList specName = new ArrayList();

            sheet.SetColumValue(resLoc.NPRowIndex, resLoc.NPColIndex + 4, this.SpecfArr, new FontStyle()
            {
                size = 12,
                fontType = "宋体",
                Align = Alignments.右中,
                dataFormat = "0.0000"
            });

            Location fomulaLoc = new Location(resLoc.NPRowIndex + 4, resLoc.NPColIndex + 4);
            //Console.WriteLine(resLoc.NPRowIndex.ToString() + resLoc.NPColIndex.ToString());
            //"AVERAGE(F32:F61)" dataLoc
            sheet.SetColStyle(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex, 11, new FontStyle()
            {
                Align = Alignments.右中
            });
            string averageStr = string.Format("AVERAGE({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + 31, dataLoc.NPColIndex));
            Console.WriteLine(averageStr);

            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex, averageStr);



            //string[] formulaStr = new string[] { "AVERAGE(F32:F61)", "STDEV(F32:F61)" , ""};


            string stdStr = string.Format("STDEV({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + 31, dataLoc.NPColIndex));
            Console.WriteLine(averageStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex, stdStr);

            //string cpStr = "+((D13-D14)/(6*D17))";


            string cpStr = string.Format("+(({0}-{1})/(6*{2}))", Location.GetRC(fomulaLoc.NPRowIndex - 3, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex - 2, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex));
            Console.WriteLine(cpStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 3, fomulaLoc.NPColIndex, cpStr);

            //string cpkuStr = "(D13-D16)/(3*D17)";

            string cpkuStr = string.Format("(({0}-{1})/(3*{2}))", Location.GetRC(fomulaLoc.NPRowIndex - 3, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex));
            Console.WriteLine(cpkuStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 5, fomulaLoc.NPColIndex, cpkuStr);
            //rowoff = 6;
            //string cpklStr = "(D16-D14)/(3*D17)";

            string cpklStr = string.Format("(({0}-{1})/(3*{2}))", Location.GetRC(fomulaLoc.NPRowIndex, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex - 2, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 1, fomulaLoc.NPColIndex));
            Console.WriteLine(cpklStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 6, fomulaLoc.NPColIndex, cpklStr);

            //string cpkStr = "MIN(D21:D22)";

            string cpkStr = string.Format("MIN({0}:{1})", Location.GetRC(fomulaLoc.NPRowIndex + 5, fomulaLoc.NPColIndex), Location.GetRC(fomulaLoc.NPRowIndex + 6, fomulaLoc.NPColIndex));
            Console.WriteLine(cpkStr);
            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 7, fomulaLoc.NPColIndex, cpkStr);

            //string[] minmaxStr = new string[] { "MIN(F32:F61)", "MAX(F32:F61)" };

            string minStr = string.Format("MIN({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + 29, dataLoc.NPColIndex));
            Console.WriteLine(minStr);

            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 9, fomulaLoc.NPColIndex, minStr);

            string maxStr = string.Format("MAX({0}:{1})", Location.GetRC(dataLoc.NPRowIndex, dataLoc.NPColIndex), Location.GetRC(dataLoc.NPRowIndex + 29, dataLoc.NPColIndex));
            Console.WriteLine(maxStr);

            sheet.SetOneCellFormula(fomulaLoc.NPRowIndex + 10, fomulaLoc.NPColIndex, maxStr);
        }

        private void moveAndRead()
        {
            Result res = Result.OK;
            double height,height2;
            dataInput.Clear();
            dataInput2.Clear();
            this.isStop = false;
            for (int i = 0; i < this.CpkPrm.XYPointsNum; i++)
            {
                res = CpkMove.MoveToPosHigh(Start.X, Start.Y);
                if (!res.IsOk)
                {
                    return;
                }
                if (this.isStop)
                {
                    this.isStop = false;
                    break;
                }
                res = CpkMove.MoveToPosSlowly(End.X, End.Y);
                if (!res.IsOk)
                {
                    return;
                }
                double posInCameraX,posInCameraY;
                bool result = this.CaptruePos(out posInCameraX,out posInCameraY);
                if (!result)
                {
                    return;
                }
                height = Machine.Instance.Robot.PosX + posInCameraX;
                height2 = Machine.Instance.Robot.PosY + posInCameraY;
                dataInput.Add(height);
                dataInput2.Add(height2);
            }
        }

        public void AddRecord2(Location recordLoc, ArrayList dataInput)
        {
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
            ArrayList dataIndex = new ArrayList(30);
            for (int i = 0; i < 30; i++)
            {
                dataIndex.Add(i + 1);
            }
            //清空以前数据
            //this.sheet.NewColumn(recordLoc.NPRowIndex + 1, recordLoc.NPColIndex, 30);
            //sheet.SetColumValue(recordLoc.NPRowIndex + 1, recordLoc.NPColIndex, dataIndex, new FontStyle()
            //{
            //    Align = Alignments.右中
            //});

            //数据
            //Location dataLoc = new Location();
            //dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            //dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            //dataLoc.NpIndexToExlIndex();

            int dataLocR = recordLoc.NPRowIndex + 1;
            int dataLocC = recordLoc.NPColIndex + 5;
            sheet.SetColStyle(dataLocR, dataLocC, 30, new FontStyle()
            {
                Align = Alignments.右中,
                dataFormat = "0.0000"
            });

            sheet.SetColumValue(dataLocR, dataLocC, dataInput, new FontStyle()
            {
                Align = Alignments.右中
            });


            //最后行
            sheet.SetOneCellValue(recordLoc.NPRowIndex + 31, recordLoc.NPColIndex, "制表：");
            sheet.SetOneCellValue(recordLoc.NPRowIndex + 31, recordLoc.NPColIndex + 3, "测试技术员：");
            sheet.SetOneCellValue(recordLoc.NPRowIndex + 31, recordLoc.NPColIndex + 6, "日期:");
        }
    }
}
