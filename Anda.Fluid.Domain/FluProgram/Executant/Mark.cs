using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using System.Drawing;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.ASV;
using System.Threading;
using System.Drawing.Imaging;
using Anda.Fluid.Infrastructure.Data;
using Anda.Fluid.Domain.Custom.DataCentor;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 拍Mark点命令
    /// </summary>
    [Serializable]
    public class Mark : Directive
    {
        /// <summary>
        /// Mark点模板匹配参数
        /// </summary>
        public ModelFindPrm ModelFindPrm { get; protected set; }
        public bool IsFromFile { get; private set; }
        /// <summary>
        /// 拍照位置
        /// </summary>
        public PointD Position { get; protected set; }

        public MarkCmd MarkCmd { get; protected set; }

        public Mark()
        {
        }

        public Mark(MarkCmd markCmd, CoordinateCorrector coordinateCorrector)
        {
            this.MarkCmd = markCmd;
            this.Position = coordinateCorrector.Correct(markCmd.RunnableModule,
                markCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            this.ModelFindPrm = markCmd.ModelFindPrm;
            this.RunnableModule = markCmd.RunnableModule;
            Program = markCmd.RunnableModule.CommandsModule.Program;
            this.IsFromFile = markCmd.IsFromFile;
        }

        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Log.Dprint("begin to execute Mark");
            Log.Dprint("move to position : " + this.Position);
            Result ret = Result.OK;
            if (this.IsFromFile)
            {                
                this.ModelFindPrm.PosInMachine = new PointD(this.Position.X, this.Position.Y);

                //从指定的位置获取当前mark
                Custom.DataCentor.DataBase data = new DataAmphnol();               
               
                data.MarkInMachine= new PointD(this.Position.X, this.Position.Y);

                data = Executor.Instance.GetCustom().GetData(data);
                if (!data.IsOk)
                {
                    ret = Result.FAILED;
                }
                this.ModelFindPrm.TargetInMachine = data.MarkFromFile;    
            }
            else
            {
                // 移动到拍照位置
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return ret;
                }
                ret = Machine.Instance.Robot.MovePosXYAndReply(this.Position,
                    this.Program.MotionSettings.VelXYMark,
                    this.Program.MotionSettings.AccXY);
                if (!ret.IsOk)
                {
                    return ret;
                }
                Log.Dprint("capture mark");
                // 拍照并获取mark点位置
                this.ModelFindPrm.PosInMachine = new PointD(this.Position.X, this.Position.Y);
                Bitmap bmp;
                ret = Machine.Instance.CaptureMark(this.ModelFindPrm, out bmp);
                if (ret.IsOk)
                {
                    Log.Dprint("mark real position : " + this.ModelFindPrm.TargetInMachine);
                }
                if (this.Program.RuntimeSettings.SaveMarkImages)
                {
                    bmp?.SaveMarkImage(this.Program.Name, "Marks", "mark");
                    //if (bmp != null)
                    //{
                    //    string filePath = RecordPathDef.Program_Marks_Date(this.Program.Name, "Marks")
                    //        + "\\mark_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                    //    DataServer.Instance.Fire(() =>
                    //    {
                    //        try
                    //        {
                    //            bmp.Save(filePath, ImageFormat.Jpeg);
                    //        }
                    //        catch (Exception e)
                    //        {
                    //            Log.Dprint("Mark Image Save error :" + e.Message);
                    //        }
                    //    });
                    //}
                }
            }
           
            return ret;
        }
    }
}