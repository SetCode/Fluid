using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Cpk;
using Anda.Fluid.Infrastructure.Utils;
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
    public class AxisXCPK:CpkBase
    {
        public AxisXCPK(Sheet sheet, CpkPrm prm) : base(sheet, prm.AxisXSpec)
        {
            this.CpkPrm = prm;
            this.inspection = InspectionMgr.Instance.FindBy(0) as InspectionDot;
        }
        private PointD pointTrans= new PointD(0, 0);
        public PointD Start=new PointD(0, 0);
        public PointD End=new PointD(0,0);
        public double PosZ = 0;
        private bool isStop = false;
        private InspectionDot inspection;
        public override void Execute(CpkPrm prm)
        {

            this.CpkPrm = prm;
            this.Specf = prm.AxisXSpec;
            if (prm.XPointsNum < 30)
            {
                MessageBox.Show("输入的执行点数{0}小于30 !!!，请重新输入执行点数","", MessageBoxButtons.OKCancel);
                return;
            }
            
            this.moveAndRead();       
            if (dataInput.Count!=this.CpkPrm.XPointsNum)
            {
                return;
            }     
             this.SaveDataToExl();
        }
        public override void Stop()
        {
            this.isStop = true;
        }

        public PointD calPoint()
        {
            double xl = this.End.X - this.Start.X;
            double yl = this.End.Y - this.Start.Y;
            if (Math.Abs(yl) > 2)
            {
                return null;
            }          
            double distanceX = xl / 10;
            pointTrans.X = this.End.X - distanceX*3;
            pointTrans.Y = this.End.Y;
            return this.pointTrans;
        }

        private bool CaptruePos(out double pos)
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
            pos = Machine.Instance.Camera.ToMachine(outX, outY).X;
            return result;
        }

        private void moveAndRead()
        {
            Result res = Result.OK;
            double height;
            dataInput.Clear();
            this.isStop = false;
            for (int i=0;i<this.CpkPrm.XPointsNum;i++)
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
                if (this.CpkMeasureType == 0) // 通信千分表
                {
                    res = CpkMove.MoveToPosHigh(pointTrans.X, pointTrans.Y);
                    if (!res.IsOk)
                    {
                        return;
                    }
                    res = CpkMove.MoveToPosSlowly(End.X, End.Y);
                    if (!res.IsOk)
                    {
                        return;
                    }
                    Thread.Sleep(3000);
                    //读取高度
                    int retCode = Machine.Instance.DigitalGage.DigitalGagable.ReadHeight(out height);
                    if (retCode != 0)
                    {
                        return;
                    }
                }
                else //this.CpkMeasureType == 1 // 相机定位
                {
                    res = CpkMove.MoveToPosSlowly(End.X, End.Y);
                    if (!res.IsOk)
                    {
                        return;
                    }
                    double posInCamera;
                    bool result = this.CaptruePos(out posInCamera);
                    if (!result)
                    {
                        return;
                    }
                    height = Machine.Instance.Robot.PosX + posInCamera;
                }
                dataInput.Add(height);
            }
        }

       

        public override void SaveDataToExl()
        {
            ////CPK测试报告(X轴定位精度)
            sheet.SetOneCellValue(1, "A", "CPK测试报告(X轴定位精度)");

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
