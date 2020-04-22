using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using System;
using System.Collections.Generic;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Infrastructure.Algo;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.FluProgram.Common;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    /// <summary>
    /// 语法解析器
    /// </summary>
    public class GrammarParser
    {
        private FluidProgram program;

        public GrammarParser(FluidProgram program)
        {
            if (program == null)
            {
                throw new Exception("program is null.");
            }
            this.program = program;
        }

        /// <summary>
        /// 解析程序语法
        /// </summary>
        public Result Parse()
        {
            Log.Print("GrammarParser", "begin to parse...");
            program.ModuleStructure.Clear();
            // 解析前重置Pattern层级引用关系
            program.Workpiece.ParentPattern = null;
            program.Workpiece.getChildren().Clear();
            foreach (var item in program.Patterns)
            {
                item.ParentPattern = null;
                item.getChildren().Clear();
            }
            Result ret = parse(program, new PointD(), null, 0, 0);
            //program.ModuleStructure.Print();
            //MARK 和测高点  轨迹优化          
            this.program.ModuleStructure.MarksSorted = this.arrangeMarkList();
            this.program.ModuleStructure.MHCmdsSorted = this.arrangeMeasureHeightList();
            return ret;
        }

        /// <summary>
        /// 解析指令
        /// 注意： 传递的实际原点坐标值，是引用当前模块的指令（DO, DO MULTIPASS)里面的坐标值；如果当前模块是FluidProgram，则原点值为(0, 0)
        /// </summary>
        /// <param name="origin">当前模块实际原点坐标</param>
        /// <param name="parent">包含当前模块的外层模块, 一个模块解析后对应的是RunnableModule类型，所以parent类型是RunnableModule</param>
        /// <param name="level">当前模块所处的层级，Program为0,Workpiece为1，以此类推</param>
        /// <returns>解析结果. 如果解析成功，携带对应的RunnableModule；如果解析失败，携带出错行CmdLine、出错描述信息</returns>
        private Result parse(CommandsModule commandsModule, PointD origin, RunnableModule parent, int level,int boardNo)
        {
            Pattern currPattern = commandsModule as Pattern;
            RunnableModule runnableModule = new RunnableModule(commandsModule, origin);
            runnableModule.BoardNo = boardNo;
            // workpiece下面所有拼版根据因引用的Do指令或DoMultiPass指令的Valve设定拼版
            if (level == 2)
            {
                runnableModule.Valve = (commandsModule as Pattern).Valve;
                if (runnableModule.Valve == Drive.ValveSystem.ValveType.Valve1)
                {
                    runnableModule.Mode = ModuleMode.AssignMode1;
                    runnableModule.SaveMode = ModuleMode.AssignMode1;
                }
                else if (runnableModule.Valve == Drive.ValveSystem.ValveType.Valve2)
                {
                    runnableModule.Mode = ModuleMode.AssignMode2;
                    runnableModule.SaveMode = ModuleMode.AssignMode2;
                }
                else if (runnableModule.Valve == Drive.ValveSystem.ValveType.Both)
                {
                    runnableModule.Mode = ModuleMode.DualFallow;
                    runnableModule.SaveMode = ModuleMode.DualFallow;
                }
            }
            if (level > 2)
            {
                runnableModule.Valve = parent.Valve;
                runnableModule.Mode = parent.Mode;
                runnableModule.SaveMode = parent.SaveMode;
            }
            program.ModuleStructure.AddModule(runnableModule, parent, level);
            // 是否已经遇到END:
            bool ended = false;
            // 记录当前处在什么类型的子句中，例如：loop block, pass block
            BlockState blockState = new BlockState();
            // 开始解析：
            List<CmdLine> cmdLineList;
            cmdLineList = new List<CmdLine>(commandsModule.CmdLineList);
            CmdLine cmdLine = null;
            bool hasPassBlock = false;
            int lastPassblockIndex = -1;
            MeasureHeightCmd curMeasureHeightCmd = null;

            //增加螺杆阀的速度、重量键值对
            Dictionary<int, double> svValvespeedDic = new Dictionary<int, double>();
            //TODO 将5换为阀参数设置的默认值,10换为默认值
            svValvespeedDic.Add(5, 10);

            for (int i = 0; i < cmdLineList.Count; i++)
            {
                cmdLine = cmdLineList[i];

                // 忽略被禁用的命令
                if (!cmdLine.Enabled)
                {
                    continue;
                }

                // END: 后面不允许添加任何命令
                if (ended)
                {
                    return new Result(false, cmdLine, "No commands is allowed to be added after END");
                }

                if (cmdLine is SetHeightSenseModeCmdLine)
                {
                    runnableModule.AddCommand(new SetHeightSenseModeCmd(runnableModule, cmdLine as SetHeightSenseModeCmdLine));
                }
                else if (cmdLine is MarkCmdLine)
                {
                    // Mark点命令必须处在最前面
                    if (i > 0)
                    {
                        // 寻找Mark点前面的Enable==true且不是注释的命令
                        CmdLine lastestCmdLine = getLastestEnabledCmdLine(cmdLineList, i - 1);
                        if (lastestCmdLine != null && !(lastestCmdLine is MarkCmdLine))
                        {
                            return new Result(false, cmdLine, "MARK must be added before the other command types.");
                        }
                    }
                    if (findMarkCmdLineIndex(cmdLineList, cmdLine as MarkCmdLine) > 1)
                    {
                        return new Result(false, cmdLine, "MARK number can not be more than 2.");
                    }
                    MarkCmd markCmd = new MarkCmd(runnableModule, cmdLine as MarkCmdLine);
                    runnableModule.AddCommand(markCmd);
                    program.ModuleStructure.RecordMarkPoint(runnableModule, markCmd);
                }
                else if (cmdLine is BadMarkCmdLine)
                {
                    //// 屏蔽原因 : BadMark模式由每个拼版只有一个 改为 测高指令那种模式
                    //// BadMark点命令必须处在非Mark命令的前面
                    //if (i > 0)
                    //{
                    //    // 寻找Bad Mark点前面的Enable==true且不是注释的命令
                    //    CmdLine lastestCmdLine = getLastestEnabledCmdLine(cmdLineList, i - 1);
                    //    if (lastestCmdLine != null && !(lastestCmdLine is MarkCmdLine))
                    //    {
                    //        // 当前指令前面有非Mark指令，判断是否是BadMark，每个pattern只能添加一个BadMark
                    //        if (lastestCmdLine is BadMarkCmdLine)
                    //        {
                    //            return new Result(false, cmdLine, "Only one BADMARK command can be added per pattern");
                    //        }
                    //        else
                    //        {
                    //            return new Result(false, cmdLine, "BADMARK must be added before the non-mark command types.");
                    //        }
                    //    }
                    //}
                    BadMarkCmd badMarkCmd = new BadMarkCmd(runnableModule, cmdLine as BadMarkCmdLine);
                    runnableModule.AddCommand(badMarkCmd);
                    program.ModuleStructure.RecordBadMarkPoint(runnableModule, badMarkCmd);
                }
                else if (cmdLine is MeasureCmdLine)
                {
                    MeasureCmd measureCmd = new MeasureCmd(runnableModule, cmdLine as MeasureCmdLine);
                    runnableModule.AddCommand(measureCmd);
                    //记录检测指令里的测高
                    program.ModuleStructure.RecordMeasureGlueHTCmds(runnableModule, measureCmd);   
                }
                else if (cmdLine is BlobsCmdLine)
                {
                    BlobsCmd blobsCmd = new BlobsCmd(runnableModule, cmdLine as BlobsCmdLine);
                    runnableModule.AddCommand(blobsCmd);
                    program.ModuleStructure.RecordBlobsCmds(runnableModule, blobsCmd);
                }
                else if (cmdLine is BarcodeCmdLine)
                {
                    BarcodeCmd barcodeCmd = new BarcodeCmd(runnableModule, cmdLine as BarcodeCmdLine);
                    runnableModule.AddCommand(barcodeCmd);
                    program.ModuleStructure.RecordBarcodeCmds(runnableModule, barcodeCmd);
                }
                else if (cmdLine is ConveyorBarcodeCmdLine)
                {
                    ConveyorBarcodeCmd conveyorBarcodeCmd = new ConveyorBarcodeCmd(runnableModule, cmdLine as ConveyorBarcodeCmdLine);
                    runnableModule.AddCommand(conveyorBarcodeCmd);
                }
                else if (cmdLine is MeasureHeightCmdLine)
                {
                    MeasureHeightCmdLine measureHeightCmdLine = cmdLine as MeasureHeightCmdLine;
                    curMeasureHeightCmd = new MeasureHeightCmd(runnableModule, measureHeightCmdLine);
                    runnableModule.AddCommand(curMeasureHeightCmd);
                    program.ModuleStructure.RecordMeasureHeightPoint(runnableModule, curMeasureHeightCmd);
                }
                else if (cmdLine is NozzleCheckCmdLine)
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new NozzleCheckCmd(runnableModule, cmdLine as NozzleCheckCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is SymbolLinesCmdLine)
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    SymbolLinesCmdLine symbolLinesCmdLine = cmdLine as SymbolLinesCmdLine;
                    SymbolLinesCmd symbolLinesCmd = new SymbolLinesCmd(runnableModule, symbolLinesCmdLine);
                    runnableModule.AddCommand(symbolLinesCmd);
                    foreach (MeasureHeightCmd item in symbolLinesCmd.GetAllMeasureCmdLineList())
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        program.ModuleStructure.RecordMeasureHeightPoint(runnableModule, item);
                    }
                    program.ModuleStructure.RecordSymbolLinesCmd(runnableModule, symbolLinesCmd);
                }
                else if (cmdLine is MultiTracesCmdLine)
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new MultiTracesCmd(runnableModule, cmdLine as MultiTracesCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is DotCmdLine)
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new DotCmd(runnableModule, cmdLine as DotCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is FinishShotCmdLine)
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new FinishShotCmd(runnableModule, cmdLine as FinishShotCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is LineCmdLine) // 包含了 cmdLine is SnakeLineCmdLine
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new LineCmd(runnableModule, cmdLine as LineCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is ArcCmdLine) // 包含了 CircleCmdLine
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new ArcCmd(runnableModule, cmdLine as ArcCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is CircleCmdLine)
                {
                    if (level > 1)
                    {
                        cmdLine.Valve = runnableModule.Valve;
                    }
                    runnableModule.AddCommand(new ArcCmd(runnableModule, cmdLine as CircleCmdLine, curMeasureHeightCmd));
                }
                else if (cmdLine is MoveXyCmdLine)
                {
                    runnableModule.AddCommand(new MoveXyCmd(runnableModule, cmdLine as MoveXyCmdLine));
                }
                else if (cmdLine is MoveAbsXyCmdLine)
                {
                    runnableModule.AddCommand(new MoveAbsXyCmd(runnableModule, cmdLine as MoveAbsXyCmdLine));
                }
                else if (cmdLine is MoveAbsZCmdLine)
                {
                    runnableModule.AddCommand(new MoveAbsZCmd(runnableModule, cmdLine as MoveAbsZCmdLine));
                }
                else if (cmdLine is MoveToLocationCmdLine)
                {
                    MoveToLocationCmdLine moveToLocationCmdLine = cmdLine as MoveToLocationCmdLine;
                    // 检测系统预定义坐标名称是否存在
                    if (runnableModule.CommandsModule.program.UserPositions.Find(x => x.Name == moveToLocationCmdLine.PositionName) == null)
                    {
                        return new Result(false, cmdLine, "\'" + moveToLocationCmdLine.PositionName + "\' is not defined in system.");
                    }
                    runnableModule.AddCommand(new MoveToLocationCmd(runnableModule, moveToLocationCmdLine));
                }
                else if (cmdLine is NormalTimerCmdLine)
                {
                    // TIMER不能放在pass block里面
                    if (blockState.Type == BlockType.PASS_BLOCK)
                    {
                        return new Result(false, cmdLine, "TIMER must not be added in pass block");
                    }
                    runnableModule.AddCommand(new NormalTimerCmd(runnableModule, cmdLine as NormalTimerCmdLine));
                }
                else if (cmdLine is TimerCmdLine)
                {
                    // TIMER必须在pass block里面
                    if (blockState.Type != BlockType.PASS_BLOCK)
                    {
                        return new Result(false, cmdLine, "TIMER must be added in pass block");
                    }
                    // TIMER必须是END PASS的前一个命令行，也就是说TIMER必须是分组内的最后一个命令行
                    if (i >= cmdLineList.Count - 1 || !(cmdLineList[i + 1] is EndPassCmdLine))
                    {
                        return new Result(false, cmdLine, "TIMER must be added before END PASS.");
                    }
                    //// TIMER不能放在最后一个分组中，无意义
                    //if (i + 2 <= cmdLineList.Count - 1 && !(cmdLineList[i + 2] is StartPassCmdLine))
                    //{
                    //    return new Result(false, cmdLine, "TIMER is no need to be added in the last pass block.");
                    //}
                    runnableModule.AddCommand(new TimerCmd(runnableModule, cmdLine as TimerCmdLine));
                }
                else if (cmdLine is StepAndRepeatCmdLine)
                {
                    StepAndRepeatCmdLine stepAndRepeatCmdLine = cmdLine as StepAndRepeatCmdLine;
                    Pattern pattern = program.GetPatternByName(stepAndRepeatCmdLine.PatternName);
                    if (pattern == null)
                    {
                        return new Result(false, cmdLine, "Pattern '" + stepAndRepeatCmdLine.PatternName + "' is not found.");
                    }
                    if (pattern.HasPassBlocks)
                    {
                        return new Result(false, cmdLine, "Pattern '" + pattern.Name + "' can not contain pass blocks.");
                    }
                    if (pattern.IsContainItself)
                    {
                        return new Result(false, cmdLine, "Pattern '" + pattern.Name + "' can not contain itself.");
                    }
                    cmdLineList.InsertRange(i + 1, (cmdLine as StepAndRepeatCmdLine).DoCmdLineList);
                }
                else if (cmdLine is DoCmdLine)
                {
                    DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                    Pattern pattern = program.GetPatternByName(doCmdLine.PatternName);
                    if (pattern == null)
                    {
                        return new Result(false, cmdLine, "Pattern '" + doCmdLine.PatternName + "' is not found.");
                    }
                    //设置父级Pattern，建立Pattern的引用关系
                    pattern.ParentPattern = currPattern;
                    currPattern?.AddChild(pattern);
                    Logger.DEFAULT.Debug(this.GetType().Name, string.Format("{0}'s parent pattern is {1}", pattern.Name, currPattern?.Name));
                    
                    if (pattern.HasPassBlocks)
                    {
                        return new Result(false, cmdLine, "Pattern '" + pattern.Name + "' can not contain pass blocks.");
                    }
                    if (pattern.Name == commandsModule.Name)
                    {
                        return new Result(false, cmdLine, "Pattern '" + pattern.Name + "' can not contain itself.");
                    }
                    if (checkExitInAncestorNode(pattern, runnableModule))
                    {
                        return new Result(false, cmdLine, "Pattern '" + pattern.Name + "' can not contain itself.");
                    }
                    pattern.Valve = doCmdLine.Valve;
                    //倒序
                    if (doCmdLine.Reverse)
                    {
                        pattern = pattern.ReversePattern();
                    }
                    if (level > 0)
                    {
                        if (runnableModule.BoardNo != 0)
                        {
                            doCmdLine.BoardNo = runnableModule.BoardNo;
                        }
                        program.ModuleStructure.AddBoardNo(doCmdLine.BoardNo);
                    }
                    Result result = parse(pattern, doCmdLine.Origin, runnableModule, level + 1, doCmdLine.BoardNo);
                    if (!result.IsOk)
                    {
                        return result;
                    }
                    // 拼版穴位号逻辑
                    (result.Param as RunnableModule).BoardNo = doCmdLine.BoardNo;
                    runnableModule.AddCommand(new DoCmd(runnableModule, result.Param as RunnableModule, curMeasureHeightCmd));
                }
                else if (cmdLine is DoMultiPassCmdLine)
                {
                    if (blockState.Type != BlockType.LOOP_BLOCK)
                    {
                        return new Result(false, cmdLine, "DO MULTIPASS must be added in loop block.");
                    }
                    DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                    Pattern pattern = program.GetPatternByName(doMultiPassCmdLine.PatternName);
                    //设置父级Pattern，建立Pattern的引用关系
                    pattern.ParentPattern = currPattern;
                    if (pattern == null)
                    {
                        return new Result(false, cmdLine, "Pattern '" + doMultiPassCmdLine.PatternName + "' is not found.");
                    }
                    currPattern?.AddChild(pattern);
                    Logger.DEFAULT.Debug(this.GetType().Name, string.Format("{0}'s parent pattern is {1}", pattern.Name, currPattern?.Name));
                    
                    if (pattern.IsContainItself)
                    {
                        return new Result(false, cmdLine, "Pattern '" + pattern.Name + "' can not contain itself.");
                    }
                    pattern.Valve = doMultiPassCmdLine.Valve;
                    if (level > 0)
                    {
                        if (runnableModule.BoardNo != 0)
                        {
                            doMultiPassCmdLine.BoardNo = runnableModule.BoardNo;
                        }
                        program.ModuleStructure.AddBoardNo(doMultiPassCmdLine.BoardNo);
                    }
                    Result result = parse(pattern, doMultiPassCmdLine.Origin, runnableModule, level + 1, doMultiPassCmdLine.BoardNo);
                    if (!result.IsOk)
                    {
                        return result;
                    }
                    if (!pattern.HasPassBlocks)
                    {
                        return new Result(false, cmdLine, "Pattern '" + doMultiPassCmdLine.PatternName + "' has no pass blocks.");
                    }
                    // 拼版穴位号逻辑
                    (result.Param as RunnableModule).BoardNo = doMultiPassCmdLine.BoardNo;
                    // 此处先暂时添加在cmdList中，在结尾处，对处于子句中的语句，集中处理。
                    runnableModule.AddCommand(new DoMultipassCmd(runnableModule, result.Param as RunnableModule, curMeasureHeightCmd));
                }
                else if (cmdLine is LoopPassCmdLine)
                {
                    if (blockState.Type != BlockType.NONE)
                    {
                        return new Result(false, cmdLine, blockState.NoEndDescription);
                    }
                    blockState.Type = BlockType.LOOP_BLOCK;
                    blockState.BlockCmdObj = new LoopBlockCmd(runnableModule, cmdLine as LoopPassCmdLine);
                }
                else if (cmdLine is NextLoopCmdLine)
                {
                    if (blockState.Type != BlockType.LOOP_BLOCK)
                    {
                        return new Result(false, cmdLine, "LOOP PASS is not found for the loop block.");
                    }
                    //不同拼版的同一个分组只需要一个计时器，不需要每个拼版都记一次时间
                    LoopBlockCmd loopBlockCmd = blockState.BlockCmdObj as LoopBlockCmd;
                    for (int doMultiPassCmdIndex = 1; doMultiPassCmdIndex < loopBlockCmd.DoMultipassCmdList.Count; doMultiPassCmdIndex++)
                    {
                        RunnableModule tempModule = loopBlockCmd.DoMultipassCmdList[doMultiPassCmdIndex].AssociatedRunnableModule;
                        foreach (Command item in tempModule.CmdList)
                        {
                            if (item is PassBlockCmd)
                            {
                                (item as PassBlockCmd).CmdList.RemoveAll(data => data is TimerCmd);
                            }
                        }
                    }
                    runnableModule.AddCommand(blockState.BlockCmdObj as LoopBlockCmd);
                    blockState.Reset();
                }
                else if (cmdLine is StartPassCmdLine)
                {
                    if (blockState.Type != BlockType.NONE)
                    {
                        return new Result(false, cmdLine, blockState.NoEndDescription);
                    }
                    // 当前pass block序号必须比上一个pass block序号大
                    if ((cmdLine as StartPassCmdLine).Index <= lastPassblockIndex)
                    {
                        return new Result(false, cmdLine, "Current pass block index must be bigger than the previous.");
                    }
                    // 相邻pass block之间不允许添加指令
                    if (hasPassBlock && !(cmdLineList[i - 1] is EndPassCmdLine))
                    {
                        return new Result(false, cmdLine, "No commands is allowed to be added between pass blocks.");
                    }
                    blockState.Type = BlockType.PASS_BLOCK;
                    blockState.BlockCmdObj = new PassBlockCmd(runnableModule, cmdLine as StartPassCmdLine);
                }
                else if (cmdLine is EndPassCmdLine)
                {
                    if (blockState.Type != BlockType.PASS_BLOCK)
                    {
                        return new Result(false, cmdLine, "START PASS is not found for the pass block");
                    }
                    PassBlockCmd passBlockCmd = blockState.BlockCmdObj as PassBlockCmd;
                    runnableModule.AddCommand(passBlockCmd);
                    hasPassBlock = true;
                    lastPassblockIndex = passBlockCmd.Index;
                    blockState.Reset();
                }
                else if (cmdLine is EndCmdLine)
                {
                    ended = true;
                }
                else if (cmdLine is PurgeCmdLine)
                {
                    if (commandsModule is Pattern)
                    {
                        cmdLine.Valve = (commandsModule as Pattern).Valve;
                    }
                    runnableModule.AddCommand(new PurgeCmd(runnableModule, cmdLine as PurgeCmdLine));
                }
                else if (cmdLine is ChangeSpeedCmdLine)
                {
                    ChangeSpeedCmdLine changeSpeedCmdLine = cmdLine as ChangeSpeedCmdLine;

                    if (svValvespeedDic.ContainsKey(changeSpeedCmdLine.Speed))
                    {

                    }
                    else if (this.program.RuntimeSettings.VavelSpeedDic.ContainsKey(changeSpeedCmdLine.Speed))
                    {
                        double value;
                        this.program.RuntimeSettings.VavelSpeedDic.TryGetValue(changeSpeedCmdLine.Speed, out value);
                        svValvespeedDic.Add(changeSpeedCmdLine.Speed, value);
                    }
                    else
                    {
                        //TODO 将10替换为默认参数
                        svValvespeedDic.Add(changeSpeedCmdLine.Speed, 10);
                    }

                    runnableModule.AddCommand(new ChangeSpeedCmd(runnableModule, changeSpeedCmdLine));

                    //传递程序中的速度重量键值对
                    SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic = FluidProgram.CurrentOrDefault().RuntimeSettings.VavelSpeedDic;
                }

                // 处理包含在子句中的语句：
                if (blockState.Type != BlockType.NONE && runnableModule.CmdList.Count > 0)
                {
                    switch (blockState.Type)
                    {
                        case BlockType.LOOP_BLOCK:
                            if (!(cmdLine is LoopPassCmdLine))
                            {
                                if (!(cmdLine is DoMultiPassCmdLine))
                                {
                                    return new Result(false, cmdLine, "Only DO MULTIPASS can be added in loop block.");
                                }
                                (blockState.BlockCmdObj as LoopBlockCmd).DoMultipassCmdList.Add(runnableModule.GetCommandAtTail() as DoMultipassCmd);
                                runnableModule.RemoveCommandAtTail();
                            }
                            break;
                        case BlockType.PASS_BLOCK:
                            if (!(cmdLine is StartPassCmdLine))
                            {
                                (blockState.BlockCmdObj as PassBlockCmd).CmdList.Add(runnableModule.GetCommandAtTail());
                                runnableModule.RemoveCommandAtTail();
                            }
                            break;
                    }
                }
            }

            //替换Program中的SvValveDic
            this.program.RuntimeSettings.VavelSpeedDic = svValvespeedDic;

            var lastCmdLine = getLastestEnabledCmdLine(cmdLineList, cmdLineList.Count - 1);
            // 检查子句命令是否完成
            if (blockState.Type != BlockType.NONE)
            {
                return new Result(false, lastCmdLine, blockState.NoEndDescription);
            }
            // 必须以END命令结尾
            if (!ended)
            {
                return new Result(false, lastCmdLine, "END is not found.");
            }         
           
            return new Result(true, runnableModule);
        }

        /// <summary>
        /// mark点拍照路径优化
        /// </summary>
        /// <param name="markList"></param>
        private List<MarkCmd> arrangeMarkList()
        {
            List<MarkCmd> markList = this.program.ModuleStructure.GetAllMarkPoints();
            if (markList.Count <= 0)
            {
                return markList;
            }
            if (!this.program.RuntimeSettings.MarksSort)
            {
                return markList;
            }
            List<PointD> points = new List<PointD>();
            List<PointD> pointListOptimized = new List<PointD>();
            // 计算离workpiece远点最近的mark点，并将其移动到列表头部
            double min = double.MaxValue;
            double distance = 0;
            int nearestIndex = -1;
            //PointD currMachinePosition = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);            
            PointD workpieceOrg = this.program.Workpiece.OriginPos;
            MarkCmd mark = null;
            for (int i = 0; i < markList.Count; i++)
            {
                mark = markList[i];
                distance = MathUtils.Distance(workpieceOrg, mark.Position);
                if (distance < min)
                {
                    nearestIndex = i;
                    min = distance;
                }
            }
            if (nearestIndex > 0)
            {
                MarkCmd nearestMark = markList[nearestIndex];
                markList.RemoveAt(nearestIndex);
                markList.Insert(0, nearestMark);
            }
            if (markList.Count <= 2)
            {
                return markList;
            }
            foreach (MarkCmd item in markList)
            {
                points.Add(item.Position);
            }
            double[] data = new double[points.Count * 2];
            for (int i = 0; i < points.Count; i++)
            {
                data[i * 2] = points[i].X;
                data[i * 2 + 1] = points[i].Y;
            }
            int[] routeIndexArr = new int[points.Count];
            OptimalRoute.initializeAll();
            OptimalRoute.autoRunAntColonyx86(data, points.Count, routeIndexArr);
            pointListOptimized.Clear();
            for (int i = 0; i < routeIndexArr.Length; i++)
            {
                pointListOptimized.Add(points[routeIndexArr[i]]);
            }
            
            markList= this.sortPoints(markList, pointListOptimized);
            return markList;
            //return route;
        }

        /// <summary>
        /// 根据点相等将对应排序
        /// </summary>
        /// <param name="points"></param>
        private List<MarkCmd> sortPoints(List<MarkCmd> markList, List<PointD> points)
        {
            List<MarkCmd> res = new List<MarkCmd>();
            res.Clear();
            foreach (PointD p in points)
            {
                foreach (var item in markList)
                {
                    if (item.Position == p)
                    {
                        res.Add(item);
                    }
                }
            }
            return res;
        }
    
        /// <summary>
        /// 测高点路径优化
        /// </summary>
        /// <param name="markList"></param>
        private List<MeasureHeightCmd> arrangeMeasureHeightList()
        {
            List<MeasureHeightCmd> measureHeightCmdList = this.program.ModuleStructure.GetAllMeasureHeightCmds();
            if (measureHeightCmdList.Count <= 0)
            {
                return measureHeightCmdList;
            }
            //不排序
            if (!this.program.RuntimeSettings.MeasureCmdsSort)
            {
                return measureHeightCmdList;
            }
            List<PointD> pointsUnsorted = new List<PointD>();
            List<PointD> pointListOptimized = new List<PointD>();
            // 计算离workpiece远点最近的测高点，并将其移动到列表头部
            double min = double.MaxValue;
            double distance = 0;
            int nearestIndex = -1;                     
            PointD workpieceOrg = this.program.Workpiece.OriginPos;
            MeasureHeightCmd measureHeightCmd = null;
            for (int i = 0; i < measureHeightCmdList.Count; i++)
            {
                measureHeightCmd = measureHeightCmdList[i];
                distance = MathUtils.Distance(workpieceOrg, measureHeightCmd.Position);
                if (distance < min)
                {
                    nearestIndex = i;
                    min = distance;
                }
            }
            if (nearestIndex > 0)
            {
                MeasureHeightCmd nearestMeasureHeightCmd = measureHeightCmdList[nearestIndex];
                measureHeightCmdList.RemoveAt(nearestIndex);
                measureHeightCmdList.Insert(0, nearestMeasureHeightCmd);
            }
            if (measureHeightCmdList.Count <= 2)
            {
                return measureHeightCmdList;
            }
            foreach (MeasureHeightCmd item in measureHeightCmdList)
            {
                pointsUnsorted.Add(item.Position);
            }
            double[] data = new double[pointsUnsorted.Count * 2];
            for (int i = 0; i < pointsUnsorted.Count; i++)
            {
                data[i * 2] = pointsUnsorted[i].X;
                data[i * 2 + 1] = pointsUnsorted[i].Y;
            }
            int[] routeIndexArr = new int[pointsUnsorted.Count];
            OptimalRoute.initializeAll();
            OptimalRoute.autoRunAntColonyx86(data, pointsUnsorted.Count, routeIndexArr);
            pointListOptimized.Clear();
            for (int i = 0; i < routeIndexArr.Length; i++)
            {
                pointListOptimized.Add(pointsUnsorted[routeIndexArr[i]]);
            }

            measureHeightCmdList = this.sortMeasureHeightCmd(measureHeightCmdList, pointListOptimized);
            return measureHeightCmdList;
            
        }

        /// <summary>
        /// 侧高点排序
        /// </summary>
        /// <param name="measureHeightCmdList"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<MeasureHeightCmd> sortMeasureHeightCmd(List<MeasureHeightCmd> measureHeightCmdList, List<PointD> points)
        {
            List<MeasureHeightCmd> res = new List<MeasureHeightCmd>();
            res.Clear();
            foreach (PointD p in points)
            {
                foreach (var item in measureHeightCmdList)
                {
                    if (item.Position == p)
                    {
                        res.Add(item);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 寻找mark点命令在所有启用（Enabled==true）且不是注释的命令行中所处的索引位置
        /// </summary>
        /// <param name="markCmdLine"></param>
        /// <returns></returns>
        private int findMarkCmdLineIndex(IEnumerable<CmdLine> cmdLineList, MarkCmdLine markCmdLine)
        {
            int index = 0;
            foreach (CmdLine cmdLine in cmdLineList)
            {
                if (cmdLine.Enabled)
                {
                    if (cmdLine is MarkCmdLine)
                    {
                        if (cmdLine == markCmdLine)
                        {
                            return index;
                        }
                        else
                        {
                            index++;
                        }
                    }
                    else if (!(cmdLine is CommentCmdLine))
                    {
                        // Mark点命令被限制添加在最前面，所以如果遇到其他命令，则后续不必再判断
                        break;
                    }
                }
            }

            return index;
        }

        /// <summary>
        /// 从指定索引位置开始，往前查找第一个启用的非注释的命令
        /// </summary>
        /// <param name="cmdLine"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private CmdLine getLastestEnabledCmdLine(List<CmdLine> cmdLineList, int startIndex)
        {
            if (cmdLineList == null || cmdLineList.Count <= 0 || startIndex < 0)
            {
                return null;
            }
            startIndex = startIndex > cmdLineList.Count - 1 ? cmdLineList.Count - 1 : startIndex;
            CmdLine cmdLine = null;
            for (int i = startIndex; i >= 0; i--)
            {
                cmdLine = cmdLineList[i];
                if (cmdLine.Enabled && !(cmdLine is CommentCmdLine))
                {
                    return cmdLine;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查父辈级链中是否已经引用了要引用的pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="parentModule"></param>
        /// <returns></returns>
        public bool checkExitInAncestorNode(CommandsModule pattern,RunnableModule parentModule)
        {
            string nameStr = "";
            RunnableModule currentModule = parentModule;
            while(currentModule != null)
            {
                nameStr = currentModule.CommandsModule.Name;
                if (nameStr.Equals(pattern.Name))
                {
                    return true;
                }
                currentModule = program.ModuleStructure.GetParentModule(currentModule);
            }

            return false;
        }

        /// <summary>
        /// 语法解析时记录当前所处的子句类型、相关对象
        /// </summary>
        private class BlockState
        {
            internal BlockType Type = BlockType.NONE;

            internal object BlockCmdObj;

            internal void Reset()
            {
                Type = BlockType.NONE;
                BlockCmdObj = null;
            }

            /// <summary>
            /// 子句缺少结尾命令时的错误描述
            /// </summary>
            internal string NoEndDescription
            {
                get
                {
                    string desc = null;
                    switch (Type)
                    {
                        case BlockType.NONE:
                            desc = "no blocks";
                            break;
                        case BlockType.LOOP_BLOCK:
                            desc = "NEXT LOOP is not found for the loop block.";
                            break;
                        case BlockType.PASS_BLOCK:
                            desc = "END PASS is not found for the pass block";
                            break;
                    }
                    return desc;
                }
            }
        }

        /// <summary>
        /// 子句类型
        /// </summary>
        private enum BlockType
        {
            NONE,
            LOOP_BLOCK,
            PASS_BLOCK
        }
    }
}
