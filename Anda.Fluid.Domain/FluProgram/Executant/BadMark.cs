using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Drive.Vision.GrayFind;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Anda.Fluid.Domain.FluProgram.Grammar.BadMarkCmdLine;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    [Serializable]
    public class BadMark : Directive
    {
        /// <summary>
        /// Mark点模板匹配参数
        /// </summary>
        public ModelFindPrm ModelFindPrm { get; private set; }

        public GrayCheckPrm GrayCheckPrm { get; private set; }

        public BadMarkType FindType { get; set; }

        public bool IsOkSkip { get; set; }

        /// <summary>
        /// 拍照位置
        /// </summary>
        public PointD Position { get; private set; }

        public BadMarkCmd BadMarkCmd { get; private set; }

        public BadMark()
        {
        }

        public BadMark(BadMarkCmd badMarkCmd, CoordinateCorrector coordinateCorrector)
        {
            this.BadMarkCmd = badMarkCmd;
            this.Position = coordinateCorrector.Correct(badMarkCmd.RunnableModule,
                badMarkCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            this.FindType = badMarkCmd.FindType;
            this.IsOkSkip = badMarkCmd.IsOkSkip;
            this.ModelFindPrm = badMarkCmd.ModelFindPrm;
            this.GrayCheckPrm = badMarkCmd.GrayCheckPrm;
            this.RunnableModule = badMarkCmd.RunnableModule;
            Program = badMarkCmd.RunnableModule.CommandsModule.Program;
        }

        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Log.Dprint("begin to execute Bad Mark");
            Log.Dprint("move to position : " + Position);
            // 移动到拍照位置
            Result ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }
            ret = Machine.Instance.Robot.MovePosXYAndReply(Position,
                this.Program.MotionSettings.VelXYMark,
                this.Program.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }

            Log.Dprint("capture Bad mark");
            VisionFindPrmBase visionFindPrm = null;
            switch (this.FindType)
            {
                case BadMarkType.ModelFind:
                    visionFindPrm = this.ModelFindPrm;
                    break;
                case BadMarkType.GrayScale:
                    visionFindPrm = this.GrayCheckPrm;
                    break;
            }
            visionFindPrm.PosInMachine = new PointD(Position.X, Position.Y);
            //Machine.Instance.Light.SetLight(visionFindPrm.LightType);
            Machine.Instance.Light.SetLight(visionFindPrm.ExecutePrm);
            Machine.Instance.Camera.SetExposure(visionFindPrm.ExposureTime);
            Machine.Instance.Camera.SetGain(visionFindPrm.Gain);
            Thread.Sleep(visionFindPrm.SettlingTime);
            byte[] bytes = Machine.Instance.Camera.TriggerAndGetBytes(TimeSpan.FromSeconds(1)).DeepClone();
            if (bytes == null)
            {
                return Result.FAILED;
            }

            Result result = Result.OK;
            //移动到拍照高度
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                Machine.Instance.Robot.MoveToMarkZAndReply();
            }
            if (this.FindType == BadMarkType.ModelFind)
            {
                ModelFindPrm.ImgData = bytes;
                ModelFindPrm.ImgWidth = Machine.Instance.Camera.Executor.ImageWidth;
                ModelFindPrm.ImgHeight = Machine.Instance.Camera.Executor.ImageHeight;
                if (!ModelFindPrm.Execute())
                {
                    result = Result.FAILED;
                }

            }
            else if (this.FindType == BadMarkType.GrayScale)
            {
                GrayCheckPrm.CheckData = GrayCheckPrm.GetROI(bytes, Machine.Instance.Camera.Executor.ImageWidth, Machine.Instance.Camera.Executor.ImageHeight);
                GrayCheckPrm.CheckWidth = GrayCheckPrm.ModelWidth;
                GrayCheckPrm.CheckHeight = GrayCheckPrm.ModelHeight;
                if (!GrayCheckPrm.Execute())
                {
                    result = Result.FAILED;
                }
            }
            Log.Dprint(string.Format("bad mark result : {0}", result.IsOk));
            
            // save mark image
            if(this.Program.RuntimeSettings.SaveBadMarkImages)
            {
                bytes?.SaveMarkImage(
                    Machine.Instance.Camera.Executor.ImageWidth,
                    Machine.Instance.Camera.Executor.ImageHeight, 
                    this.Program.Name, "BadMarks", "badmark");
            }

            ////如果OK跳过则结果取反
            //if (this.IsOkSkip)
            //{
            //    result = result.IsOk ? Result.FAILED : Result.OK;
            //}
            return result;
        }
    }
}
