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
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Domain.Custom;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    public class Measure : Directive,IAlarmSenderable
    {      
        public MeasurePrm MeasurePrm { get; private set; }

        public MeasureCmd MeasureCmd { get; protected set; }
        public string SavePath = string.Empty;

        private PointD position;

        //public bool NeedMeasureHeight = false;
        //0: 集中处理  1：执行时处理
        public List<MeasureHeight> MeasureHeights;
        public MeasureContents MeasureContent = MeasureContents.None;

        public object Obj => this;


        public string Name => this.GetType().Name;
       

        public Measure()
        {

        }
        public Measure(MeasureCmd measureCmd, CoordinateCorrector coordinateCorrector)
        {
            this.MeasureCmd = measureCmd;
            this.MeasurePrm = measureCmd.MeasurePrm;
            this.position=coordinateCorrector.Correct(measureCmd.RunnableModule, measureCmd.Position, Executor.Instance.Program.ExecutantOriginOffset);
            Program = measureCmd.RunnableModule.CommandsModule.Program;
            this.SavePath = measureCmd.SavePath;
            //this.NeedMeasureHeight = measureCmd.NeedMeasureHeight;
            this.MeasureContent = measureCmd.MeasureContent;
            if (measureCmd.MeasureContent.HasFlag(MeasureContents.GlueHeight) && measureCmd.MeasureHeightCmds != null && measureCmd.MeasureHeightCmds.Count == 2)
            {
                this.MeasurePrm.ToleranceMax = measureCmd.ToleranceMax;
                this.MeasurePrm.ToleranceMin = measureCmd.ToleranceMin;
                this.MeasureHeights = new List<MeasureHeight>();
                foreach (MeasureHeightCmd item in measureCmd.MeasureHeightCmds)
                {
                    this.MeasureHeights.Add(new MeasureHeight(item, coordinateCorrector));
                }
            }
        }
        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }

            this.MeasurePrm.ResHeight = 0;
            this.MeasurePrm.PhyResult = 0;
            //edit by shawn
            string[] result = new string[6] { "0.00", "0.00", "0.00", "0.00", "0.00", "0.00" };        

            Result ret = Result.OK;
            if (this.MeasureContent.HasFlag(MeasureContents.LineWidth))
            {
                Logger.DEFAULT.Info("开始检测，移动到点： " + this.position);
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
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
                ret = Machine.Instance.CaptureAndMeasure(this.MeasurePrm, out bmp);
                Logger.DEFAULT.Info("Measure Result is :  {0}" + ret.IsOk.ToString());

                //Executor.Instance.GetCustom().AppendRecored(this.MeasurePrm.measureType.ToString(), Math.Round(this.MeasurePrm.PhyResult, 3).ToString("0.000"));
                result[0] = this.MeasurePrm.PhyResult.ToString("0.00");
                result[1] = this.MeasurePrm.Upper.ToString("0.00");
                result[2] = this.MeasurePrm.Lower.ToString("0.00");

                //保存图片
                if (this.Program.RuntimeSettings.SaveMeasureMentImages)
                {
                    bmp?.SaveMarkImage(this.Program.Name, "MeasureMents", "measure");
                }
            }
            
            //测高 第二个是在指令里面执行
            if (this.MeasureContent.HasFlag(MeasureContents.GlueHeight) && this.MeasureHeights != null && this.MeasureHeights.Count == 2)
            {
               
                double height;              
                ret = this.MeasureHeights[1].Execute();
                if (!ret.IsOk)
                {
                    //AlarmServer.Instance.Fire(this, AlarmInfoDomain.InfoMeasuredGlueValue);
                    //检测失败，高度设置为-1
                    this.MeasurePrm.ResHeight = -1;
                    //Executor.Instance.GetCustom().AppendRecored(MeasureType.胶高.ToString(), Math.Round(-1.0, 3).ToString("0.000"));
                    return ret;
                }
                //胶高
                height = Math.Abs(this.MeasureHeights[1].MeasureHeightCmd.RealHtValue - this.MeasureHeights[0].MeasureHeightCmd.RealHtValue);
                //超出范围 也要记录
                if (height<this.MeasurePrm.ToleranceMin || height>this.MeasurePrm.ToleranceMax)
                {
                    //AlarmServer.Instance.Fire(this, AlarmInfoDomain.InfoMeasuredGlueValue);
                    //ret = Result.FAILED;                   
                }

                result[3] = height.ToString("0.00");
                result[4] = this.MeasurePrm.ToleranceMax.ToString("0.00");
                result[5] = this.MeasurePrm.ToleranceMin.ToString("0.00");
                //Executor.Instance.GetCustom().AppendRecored(MeasureType.胶高.ToString(), Math.Round(height, 3).ToString("0.000"));
                this.MeasurePrm.ResHeight = height;
            }

            if (Executor.Instance.GetCustom() is CustomRTV)
            {
                CustomRTV rtv = (CustomRTV)Executor.Instance.GetCustom();
                rtv.AddResult(result);
                MsgCenter.Broadcast(LngMsg.MSG_WidthAndHeight_Info, this, result);
            }

            return ret;
        }
    }
}
