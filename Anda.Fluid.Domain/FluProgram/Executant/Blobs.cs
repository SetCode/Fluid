using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.Halcon;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    public class Blobs : Directive
    {
        public BlobsCmd BlobsCmd { get; private set; }

        public BlobsTool BlobsTool { get; private set; } 

        public string SavePath { get; set; } 

        public PointD Position { get; private set; }

        public Blobs(BlobsCmd blobsCmd, CoordinateCorrector coordinateCorrector)
        {
            this.BlobsCmd = blobsCmd;
            this.BlobsTool = blobsCmd.BlobsTool;
            this.Position = coordinateCorrector.Correct(blobsCmd.RunnableModule, blobsCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            Program = blobsCmd.RunnableModule.CommandsModule.Program;
            this.SavePath = blobsCmd.SavePath;
        }

        public override Result Execute()
        {
            //移动到拍照位置
            Result result = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!result.IsOk)
            {
                return result;
            }
            result = Machine.Instance.Robot.MovePosXYAndReply(this.Position,
                this.Program.MotionSettings.VelXYMark,
                this.Program.MotionSettings.AccXY);
            if (!result.IsOk)
            {
                return result;
            }
            //设置光源参数
            Machine.Instance.Light.SetLight(this.BlobsTool.ExecutePrm);
            Machine.Instance.Camera.SetExposure(this.BlobsTool.ExposureTime);
            Machine.Instance.Camera.SetGain(this.BlobsTool.Gain);
            //拍照前延时
            System.Threading.Thread.Sleep(this.BlobsTool.SettlingTime);
            //拍照获取当前图像
            byte[] bytes = Machine.Instance.Camera.TriggerAndGetBytes(TimeSpan.FromSeconds(1));
            this.BlobsTool.CurrentHImage = bytes.ToHImage(Machine.Instance.Camera.Executor.ImageWidth, Machine.Instance.Camera.Executor.ImageHeight); ;
            //执行Blobs检测
            if (this.BlobsTool.Execute())
            {
                return Result.OK;
            }
            return Result.FAILED;
        }
    }
}
