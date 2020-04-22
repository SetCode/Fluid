using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Anda.Fluid.Infrastructure.Common.CommonDelegates;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    /// <summary>
    /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
    /// </summary>
    public sealed class PatternCorrector : IMsgSender
    {
        private static readonly string TAG = "PatternCorrector";
        private static readonly PatternCorrector instance = new PatternCorrector();
        public static PatternCorrector Instance
        {
            get { return instance; }
        }

        public ManualResetEvent WaitManualMarkDone { get; private set; } = new ManualResetEvent(false);

        /// <summary>
        /// Load程序后，第一次加载显示Pattern内容后，拍摄Mark点校正Pattern原点及轨迹命令坐标
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="onStart"></param>
        /// <param name="onFinished"></param>
        /// <param name="onError"></param>
        /// <param name="mustCorrect">必须校正，即使NeedMarkCorrect为false</param>
        /// <returns>-1：执行失败，1：不需要校正，0：校正成功</returns>
        public int Correct(Pattern pattern)
        {
            if (pattern == null)
            {
                Log.Print(TAG, "pattern is null.");
                return -1;
            }

            //if (!pattern.NeedMarkCorrect)
            //{
            //    Log.Print(TAG, "pattern NeedMarkCorrect : false");
            //    return 1;
            //}
            var tuple = this.getMarksRecursive(pattern);
            var markCmdLines = tuple.Item2;
            if (markCmdLines.Count <= 0)
            {
                return 1;
            }
            //if (pattern.GetMarkCmdLines().Count <= 0)
            //{
            //    Log.Print(TAG, "pattern marks count : 0");
            //    return 1;
            //}

            // 拍摄Mark点
            Result ret = Result.OK;
            foreach (MarkCmdLine markCmdLine in markCmdLines)
            {
                ret = executeMark(tuple.Item1, markCmdLine);
                if (!ret.IsOk)
                {
                    return -1;
                }
            }

            // 生成坐标校正器
            CoordinateTransformer coordinateTransformer = new CoordinateTransformer();
            if (markCmdLines.Count == 1)
            {
                if (markCmdLines[0].ModelFindPrm.IsUnStandard)
                {
                    PointD p = new PointD(markCmdLines[0].ModelFindPrm.ReferenceX, markCmdLines[0].ModelFindPrm.ReferenceY);
                    PointD refXY = (p + tuple.Item1.GetOriginSys()).ToMachine();

                    if (markCmdLines[0].ModelFindPrm.UnStandardType == 0)
                    {
                        coordinateTransformer.SetMarkPoint(refXY, markCmdLines[0].ModelFindPrm.ReferenceA,
                            markCmdLines[0].ModelFindPrm.TargetInMachine, markCmdLines[0].ModelFindPrm.Angle);
                    }
                    else
                    {
                        PointD p2 = new PointD(markCmdLines[0].ModelFindPrm.ReferenceX2, markCmdLines[0].ModelFindPrm.ReferenceY2);
                        PointD refXY2 = (p2 + tuple.Item1.GetOriginSys()).ToMachine();
                        coordinateTransformer.SetMarkPoint(refXY, refXY2,
                            markCmdLines[0].ModelFindPrm.TargetInMachine, markCmdLines[0].ModelFindPrm.TargetInMachine2);
                    }
                }
                else
                {
                    coordinateTransformer.SetMarkPoint(
                        markCmdLines[0].ModelFindPrm.PosInMachine,
                        markCmdLines[0].ModelFindPrm.TargetInMachine);
                }

            }
            else if (markCmdLines.Count == 2)
            {
                coordinateTransformer.SetMarkPoint(
                    markCmdLines[0].ModelFindPrm.PosInMachine,
                    markCmdLines[1].ModelFindPrm.PosInMachine,
                    markCmdLines[0].ModelFindPrm.TargetInMachine,
                    markCmdLines[1].ModelFindPrm.TargetInMachine);
            }


            // 校正Pattern原点
            PointD patternOldOrigin = new PointD(pattern.GetOriginPos());
            PointD patternNewOrigin = coordinateTransformer.Transform(patternOldOrigin);
            if (pattern is Workpiece)
            {//workpiece原点为机械坐标
                Workpiece w = pattern as Workpiece;
                w.OriginPos.X = patternNewOrigin.X;
                w.OriginPos.Y = patternNewOrigin.Y;
                FluidProgram.Current.GetWorkPieceCmdLine().Origin.X = w.OriginPos.X;
                FluidProgram.Current.GetWorkPieceCmdLine().Origin.Y = w.OriginPos.Y;
            }
            else
            {//Pattern原点为相对workpiece的系统坐标
                VectorD v = patternNewOrigin.ToSystem() - pattern.Program.Workpiece.GetOriginPos().ToSystem();
                pattern.Origin.X = v.X;
                pattern.Origin.Y = v.Y;
            }

            //// 校正轨迹命令坐标
            //foreach (CmdLine cmdLine in pattern.CmdLineList)
            //{
            //    cmdLine.Correct(patternOldOrigin, coordinateTransformer, patternNewOrigin);
            //}

            //// 校正子Pattern的原点
            //foreach (var item in pattern.Children)
            //{
            //    PointD newOriginPos = coordinateTransformer.Transform(item.GetOriginPos());
            //    // Pattern原点为相对Workpiece的系统坐标
            //    item.Origin.X = (newOriginPos.ToSystem() - pattern.Program.Workpiece.GetOriginSys()).X;
            //    item.Origin.Y = (newOriginPos.ToSystem() - pattern.Program.Workpiece.GetOriginSys()).Y;
            //}

            correctPatternRecursive(pattern, patternOldOrigin, patternNewOrigin, coordinateTransformer);

            //pattern.NeedMarkCorrect = false;
            return 0;
        }

        private Result executeMark(Pattern pattern, MarkCmdLine markCmdLine)
        {
            Log.Print("begin to execute Mark cmdline");
            //系统坐标->机械坐标
            markCmdLine.ModelFindPrm.PosInMachine = pattern.MachineAbs(markCmdLine.PosInPattern);
            //(markCmdLine.PosInPattern + pattern.GetOriginSys()).ToMachine();
            Log.Print("move to position : " + markCmdLine.ModelFindPrm.PosInMachine);
            // 移动到安全高度
            Result ret = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return ret;
            }
            // 移动到拍照位置
            ret = Machine.Instance.Robot.MovePosXYAndReply(markCmdLine.ModelFindPrm.PosInMachine,
                FluidProgram.Current.MotionSettings.VelXY,
                FluidProgram.Current.MotionSettings.AccXY);
            if (!ret.IsOk)
            {
                return ret;
            }
            Log.Print("capture mark");
            // 拍照并获取mark点位置
            ret = Machine.Instance.CaptureMark(markCmdLine.ModelFindPrm);
            if (!ret.IsOk)
            {
                return ret;
            }
            Log.Print("mark real position : " + markCmdLine.ModelFindPrm.TargetInMachine);
            return ret;
        }

        /// <summary>
        /// 如果本层没有Mark，层层查找，直到找到含Mark的Pattern进行校正
        /// </summary>
        private Tuple<Pattern, List<MarkCmdLine>> getMarksRecursive(Pattern pattern)
        {
            if (pattern == null)
            {
                return new Tuple<Pattern, List<MarkCmdLine>>(pattern, new List<MarkCmdLine>());
            }
            List<MarkCmdLine> markList = pattern.GetMarkCmdLines();
            Tuple<Pattern, List<MarkCmdLine>> tuple = new Tuple<Pattern, List<MarkCmdLine>>(pattern, markList);
            if (markList.Count <= 0)
            {
                tuple = getMarksRecursive(pattern.ParentPattern);
            }
            return tuple;
        }

        /// <summary>
        /// 校正本级Pattern，且递归校正子Pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="patternOldOrigin"></param>
        /// <param name="patternNewOrigin"></param>
        /// <param name="coordinateTransformer"></param>
        public void correctPatternRecursive(Pattern pattern, PointD patternOldOrigin, PointD patternNewOrigin, CoordinateTransformer coordinateTransformer)
        {
            // 校正轨迹命令坐标
            foreach (CmdLine cmdLine in pattern.CmdLineList)
            {
                cmdLine.Correct(patternOldOrigin, coordinateTransformer, patternNewOrigin);
            }

            // 校正子Pattern的原点
            foreach (var child in pattern.getChildren())
            {
                PointD childOldOriginPos = new PointD(child.GetOriginPos());
                PointD childNewOriginPos = coordinateTransformer.Transform(childOldOriginPos);
                // Pattern原点为相对Workpiece的系统坐标
                child.Origin.X = (childNewOriginPos.ToSystem() - pattern.Program.Workpiece.GetOriginSys()).X;
                child.Origin.Y = (childNewOriginPos.ToSystem() - pattern.Program.Workpiece.GetOriginSys()).Y;
                List<MarkCmdLine> childMarks = child.GetMarkCmdLines();
                // 有Mark的拼版只校准拼版原点位置和拼版Mark的拍照位置
                if (childMarks.Count > 0)
                {
                    foreach (MarkCmdLine item in childMarks)
                    {
                        PointD oldPosInMachine = item.PosInPattern + childOldOriginPos;
                        PointD newPosInMachine = coordinateTransformer.Transform(oldPosInMachine);
                        VectorD v = newPosInMachine - childNewOriginPos;
                        item.PosInPattern.X = v.X;
                        item.PosInPattern.Y = v.Y;
                    }
                }
                else
                {
                    // 递归校正子Pattern
                    correctPatternRecursive(child, childOldOriginPos, childNewOriginPos, coordinateTransformer);
                }
            }
        }


    }
}
