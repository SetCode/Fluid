using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using System.Drawing;
using Anda.Fluid.Drive.Vision.Barcode;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    public class Barcode : Directive
    {      
        public BarcodePrm BarcodePrm { get; private set; }

        public BarcodeCmd BarcodeCmd { get; protected set; }

        private PointD position;
        public Barcode()
        {

        }
        public Barcode(BarcodeCmd barcodeCmd, CoordinateCorrector coordinateCorrector)
        {
            this.BarcodeCmd = barcodeCmd;
            this.BarcodePrm = barcodeCmd.BarcodePrm;
            this.position=coordinateCorrector.Correct(barcodeCmd.RunnableModule, barcodeCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            Program = barcodeCmd.RunnableModule.CommandsModule.Program;
            this.RunnableModule = barcodeCmd.RunnableModule;
        }
        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Logger.DEFAULT.Info("开始检测，移动到点： "+this.position);
            Result ret=Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }
            ret = Machine.Instance.Robot.MovePosXYAndReply(this.position);
            if (!ret.IsOk)
            {
                return ret;
            }
            //执行检测
            Bitmap bmp;
            ret = Machine.Instance.CaptureAndBarcode(this.BarcodePrm, out bmp);
            Logger.DEFAULT.Info("Barcode Result is :  {0}" + ret.IsOk.ToString());

            //保存结果

            //保存图片
           
            return ret;
        }
    }
}
