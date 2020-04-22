using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{

    ///<summary>
    /// Description	:用于切换点胶轨迹编辑窗口的工具类
    /// Author  	:liyi
    /// Date		:2019/07/23
    ///</summary>   
    public class SwitchSymbolForm
    {
        private CommandsModule curModule;

        private static readonly SwitchSymbolForm instance = new SwitchSymbolForm();

        private SwitchSymbolForm() { }

        public static SwitchSymbolForm Instance => instance;

        ///<summary>
        /// Description	:是否还有下一条点胶指令
        /// Author  	:liyi
        /// Date		:2019/07/23
        ///</summary>   
        /// <param name="cmdline">当前要关闭的指令</param>
        /// <returns>返回-1表示没有，否则为CommandModule中当前指令的下一条点胶指令</returns>
        public int HasNextSymbol(CmdLine cmdLine)
        {
            if (this.curModule == null)
            {
                return -1;
            }
            int curIndex = GetCurCmdLineIndex(cmdLine);
            if (curIndex >= this.curModule.CmdLineList.Count - 1)
            {
                return -1;
            }
            for (int i = curIndex+1; i < this.curModule.CmdLineList.Count - 1; i++)
            {
                if (IsSymbolCmdLine(this.curModule.CmdLineList[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        ///<summary>
        /// Description	:是否还有上一条点胶指令
        /// Author  	:liyi
        /// Date		:2019/07/23
        ///</summary>   
        /// <param name="cmdline">当前要关闭的指令</param>
        /// <returns>返回-1表示没有，否则为CommandModule中当前指令的上一条点胶指令</returns>
        public int HasLastSymbol(CmdLine cmdLine)
        {
            if (this.curModule == null)
            {
                return -1;
            }
            int curIndex = GetCurCmdLineIndex(cmdLine);
            if (curIndex <= 0)
            {
                return -1;
            }
            for (int i = 0; i < curIndex; i++)
            {
                if (IsSymbolCmdLine(this.curModule.CmdLineList[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        ///<summary>
        /// Description	:切换下一条点胶轨迹的编辑窗口
        /// Author  	:liyi
        /// Date		:2019/07/23
        /// <param name="cmdline">当前要关闭的指令</param>
        ///</summary>   
        public void SwitchNextSymbol(CmdLine cmdLine,Form lastForm)
        {
            if (this.curModule == null)
            {
                return;
            }
            int nexIndex = HasNextSymbol(cmdLine);
            if (nexIndex == -1)
            {
                return;
            }
            else
            {
                ShowCmdLineEditForm(this.curModule.CmdLineList[nexIndex], lastForm);
            }
        }

        ///<summary>
        /// Description	:切换上一条点胶轨迹的编辑窗口
        /// Author  	:liyi
        /// Date		:2019/07/23
        /// <param name="cmdline">当前要关闭的指令</param>
        ///</summary>   
        public void SwitchLastSymbol(CmdLine cmdLine, Form lastForm)
        {
            if (this.curModule == null)
            {
                return;
            }
            int lastIndex = HasLastSymbol(cmdLine);
            if (lastIndex == -1)
            {
                return;
            }
            else
            {
                ShowCmdLineEditForm(this.curModule.CmdLineList[lastIndex], lastForm);
            }
        }

        ///<summary>
        /// Description	:设置打开的命令模块和指定某一行的指令
        /// Author  	:liyi
        /// Date		:2019/07/23
        ///</summary>   
        public void SetCurCommandsModule(CommandsModule module)
        {
            this.curModule = module;
        }

        ///<summary>
        /// Description	:获取指定轨迹在pattern中的索引位置
        /// Author  	:liyi
        /// Date		:2019/07/23
        ///</summary>   
        /// <param name="cmdLine"></param>
        /// <returns></returns>
        private int GetCurCmdLineIndex(CmdLine cmdLine)
        {
            if (this.curModule == null)
            {
                return -1;
            }
            int curIndex = -1;
            for (int i = 0; i < this.curModule.CmdLineList.Count; i++)
            {
                if (cmdLine.Equals(this.curModule.CmdLineList[i]))
                {
                    curIndex = i;
                }
            }
            return curIndex;
        }

        ///<summary>
        /// Description	:判断指令轨迹是否是点胶轨迹
        /// Author  	:liyi
        /// Date		:2019/07/23
        ///</summary>   
        /// <param name="cmdLine"></param>
        /// <returns></returns>
        private bool IsSymbolCmdLine(CmdLine cmdLine)
        {
            if (cmdLine is CircleCmdLine
                || cmdLine is ArcCmdLine
                || cmdLine is DotCmdLine
                || cmdLine is FinishShotCmdLine
                || cmdLine is SnakeLineCmdLine
                || cmdLine is LineCmdLine
                || cmdLine is NozzleCheckCmdLine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///<summary>
        /// Description	:显示指定指令的编辑窗口
        /// Author  	:liyi
        /// Date		:2019/07/23
        ///</summary>   
        /// <param name="cmdLine"></param>
        private void ShowCmdLineEditForm(CmdLine cmdLine,Form lastForm)
        {
            if (this.curModule == null)
            {
                return;
            }
            lastForm.Dispose();
            if (cmdLine is CircleCmdLine)
            {
                new EditCircleForm1(this.curModule as Pattern, cmdLine as CircleCmdLine).ShowDialog();
            }
            else if (cmdLine is ArcCmdLine)
            {
                new EditArcForm1(this.curModule as Pattern, cmdLine as ArcCmdLine).ShowDialog();
            }
            else if (cmdLine is DotCmdLine)
            {
                new EditDotForm1(this.curModule as Pattern, cmdLine as DotCmdLine).ShowDialog();
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                new EditFinishShotForm1(this.curModule as Pattern, cmdLine as FinishShotCmdLine).ShowDialog();
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                new EditSnakeLineForm1(this.curModule as Pattern, cmdLine as SnakeLineCmdLine).ShowDialog();
            }
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                switch (lineCmdLine.LineMethod)
                {
                    case Domain.FluProgram.Common.LineMethod.Multi:
                        new EditMultiLinesForm(this.curModule as Pattern, lineCmdLine).ShowDialog();
                        break;
                    case Domain.FluProgram.Common.LineMethod.Single:
                        new EditSingleLineForm(this.curModule as Pattern, lineCmdLine).ShowDialog();
                        break;
                    case Domain.FluProgram.Common.LineMethod.Poly:
                        new EditPolyLineForm(this.curModule as Pattern, lineCmdLine).ShowDialog();
                        break;
                }
            }
            else if (cmdLine is NozzleCheckCmdLine)
            {
                new EditNozzleCheckForm(this.curModule as Pattern, cmdLine as NozzleCheckCmdLine).ShowDialog();
            }
        }

        /// <summary>
        /// Description	:显示指定指令的编辑窗口到父级编辑窗口
        /// Author  	:Xuxixiao
        /// Date		:2019/08/29
        /// </summary>
        /// <param name="cmdLine"></param>
        private void ShowCmdLineEditForm(CmdLine cmdLine)
        {
            if (this.curModule == null)
            {
                return;
            }
            if (EditFormParent.Current == null || EditFormParent.Current.IsDisposed)
            {
                new EditFormParent(cmdLine.CommandsModule as Pattern).Show();
            }

            if (cmdLine is CircleCmdLine)
            {
                EditFormParent.Current.ChangeForm(new EditCircleForm1(this.curModule as Pattern, cmdLine as CircleCmdLine));
            }
            else if (cmdLine is ArcCmdLine)
            {
                EditFormParent.Current.ChangeForm(new EditArcForm1(this.curModule as Pattern, cmdLine as ArcCmdLine));
            }
            else if (cmdLine is DotCmdLine)
            {
                EditFormParent.Current.ChangeForm(new EditDotForm1(this.curModule as Pattern, cmdLine as DotCmdLine));
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                EditFormParent.Current.ChangeForm(new EditFinishShotForm1(this.curModule as Pattern, cmdLine as FinishShotCmdLine));
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                EditFormParent.Current.ChangeForm(new EditSnakeLineForm1(this.curModule as Pattern, cmdLine as SnakeLineCmdLine));
            }
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                switch (lineCmdLine.LineMethod)
                {
                    case Domain.FluProgram.Common.LineMethod.Multi:
                        EditFormParent.Current.ChangeForm(new EditMultiLinesForm(this.curModule as Pattern, lineCmdLine));
                        break;
                    case Domain.FluProgram.Common.LineMethod.Single:
                        EditFormParent.Current.ChangeForm(new EditSingleLineForm(this.curModule as Pattern, lineCmdLine));
                        break;
                    case Domain.FluProgram.Common.LineMethod.Poly:
                        EditFormParent.Current.ChangeForm(new EditPolyLineForm(this.curModule as Pattern, lineCmdLine));
                        break;
                }
            }
            else if (cmdLine is NozzleCheckCmdLine)
            {
                EditFormParent.Current.ChangeForm(new EditNozzleCheckForm(this.curModule as Pattern, cmdLine as NozzleCheckCmdLine));
            }
        }
    }
}
