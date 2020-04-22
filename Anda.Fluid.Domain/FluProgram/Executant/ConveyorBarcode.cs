using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.Custom;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    [Serializable]
    public class ConveyorBarcode : Directive
    {
        public ConveyorBarcodeCmd ConveyorBarcodeCmd { get; private set; }

        public string Text;

        public ConveyorBarcode(ConveyorBarcodeCmd conveyorBarcodeCmd)
        {
            this.ConveyorBarcodeCmd = conveyorBarcodeCmd;
            this.Program = conveyorBarcodeCmd.RunnableModule.CommandsModule.Program;
            this.RunnableModule = conveyorBarcodeCmd.RunnableModule;
        }

        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            //判断当前是轨道1还是轨道2
            if (Executor.Instance.Program.ExecutantOriginOffset.X > 0.1 && Executor.Instance.Program.ExecutantOriginOffset.Y > 0.1)
            {
                if (Machine.Instance.BarcodeSanncer2.BarcodeScannable == null)
                {
                    return Result.FAILED;
                }
                Machine.Instance.BarcodeSanncer2.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(3), out this.Text);
            }
            else
            {
                if (Machine.Instance.BarcodeSanncer1.BarcodeScannable == null)
                {
                    return Result.FAILED;
                }
                Machine.Instance.BarcodeSanncer1.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(3), out this.Text);
            }
            
            // 记录条码到文件
            //this.RecordBarcode(); 
            if(string.IsNullOrEmpty(this.Text))
            {
                return Result.FAILED;
            }
            return Result.OK;
        }

        public void RecordBarcode()
        {
            //string fileName = RecordPathDef.Program_Barcodes(this.Program.Name) + "\\Conveyor_Barcodes_" + DateTime.Today.ToString("yyyyMMdd") + ".csv";
            //string text = DateTime.Now.ToString("HH:mm:ss") + "," + this.Text;
            //FileUtils.AppendTextln(fileName, text);
            //Executor.Instance.GetCustom().FileName = this.Text;   
            if (this.Program.RuntimeSettings.CustomParam.RTVParam.IsSaveCode)
            {
                Executor.Instance.GetCustom().AppendRecored("BarCode ",this.Text);           
            }
        }
    }
}
