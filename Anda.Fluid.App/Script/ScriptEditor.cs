using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using DrawingPanel.Msg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Anda.Fluid.App.Script
{
    public partial class ScriptEditor : UserControlEx,ISingleEditDrawCmdable
    {
        private const int WM_KEYDOWN = 0x100;

        #region 命令行数据
        private CommandsModule commandsModule;
        /// <summary>
        /// 当前加载的命令行模块 
        /// </summary>
        public CommandsModule CurrCommandsModule
        {
            get { return commandsModule; }
        }
        #endregion

        #region 行标记相关
        // 需要标记显示的命令行索引位置，用于指示语法出错位置、程序运行暂停的位置
        public enum MarkType { NONE, COMPILE_ERROR, RUNNING_PAUSED_POSITION }
        // 被标记行所在的命令模块
        private CommandsModule markCommandsModule;
        // 无效的行索引位置
        private const int LINE_INDEX_INVALID = -1;
        // 被标记行的索引位置
        private int markLineIndex = LINE_INDEX_INVALID;
        // 标记类型
        private MarkType markType = MarkType.NONE;
        #endregion

        #region 剪贴板
        private static List<CmdLine> clipBoard = new List<CmdLine>();
        /// <summary>
        /// 剪贴板，存储复制、剪切操作的命令行对象集合
        /// </summary>
        public static List<CmdLine> ClipBoard
        {
            get { return new List<CmdLine>(clipBoard); }
        }
        #endregion

        #region 回调
        public delegate void OnEditCmdLine(CommandsModule commandsModule, CmdLine cmdLine);
        /// <summary>
        /// 用户鼠标双击某一行开始编辑命令
        /// </summary>
        public event OnEditCmdLine OnEditCmdLineEvent;

        public delegate void OnCommandsModuleLoaded(CommandsModule commandsModule);
        /// <summary>
        /// 加载CommandsModule完成
        /// </summary>
        public event OnCommandsModuleLoaded OnCommandsModuleLoadedEvent;
        #endregion

        private bool enable = true;
        private List<int> lastSelectedIndices = new List<int>();
        private String trackNumber = "";

        public ScriptEditor()
        {
            InitializeComponent();
            listView1.VScroll += (sender, e) => updateLineNumbers();
            listView1.Columns.Add("command", -1, HorizontalAlignment.Left);
            this.ReadLanguageResources();
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(this.tsrAllEnable.Name, this.tsrAllEnable.Text);
            this.SaveKeyValueToResources(this.tsrBackDisable.Name, this.tsrBackDisable.Text);
            this.SaveKeyValueToResources(this.tsrOthersDisable.Name, this.tsrOthersDisable.Text);
            this.SaveKeyValueToResources(this.tsrPreDisable.Name, this.tsrPreDisable.Text);
            this.SaveKeyValueToResources(this.tsrThisDisable.Name, this.tsrThisDisable.Text);
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            string temp = "";
            temp = this.ReadKeyValueFromResources(this.tsrAllEnable.Name);
            if (temp != "")
            {
                this.tsrAllEnable.Text = temp;
            }
            temp = this.ReadKeyValueFromResources(this.tsrBackDisable.Name);
            if (temp != "")
            {
                this.tsrBackDisable.Text = temp;
            }
            temp = this.ReadKeyValueFromResources(this.tsrOthersDisable.Name);
            if (temp != "")
            {
                this.tsrOthersDisable.Text = temp;
            }
            temp = this.ReadKeyValueFromResources(this.tsrPreDisable.Name);
            if (temp != "")
            {
                this.tsrPreDisable.Text = temp;
            }
            temp = this.ReadKeyValueFromResources(this.tsrThisDisable.Name);
            if (temp != "")
            {
                this.tsrThisDisable.Text = temp;
            }
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        /// <summary>
        /// 当enable为false的时候，对listview中的点击无效
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnable(bool enable)
        {
            if(enable)
            {
                this.enable = true;
            }
            else
            {
                this.enable = false;
            }
        }

        /// <summary>
        /// 单击选中行时，变色处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_Click(object sender,EventArgs e)
        {
            this.ClickCmdLine();
        }

        private void highLightSelectedIndices()
        {
            foreach (var i in lastSelectedIndices)
            {
                if (i > listView1.Items.Count - 1)
                {
                    continue;
                }
                listView1.Items[i].BackColor = Color.FromArgb(30, 30, 30);// Color.White;
            }
            lastSelectedIndices.Clear();
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }

            this.CompareTrackNumber(this.trackNumber);

            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                int ii = listView1.SelectedIndices[i];
                listView1.Items[listView1.SelectedIndices[i]].BackColor = Color.SkyBlue;
                lastSelectedIndices.Add(listView1.SelectedIndices[i]);
            }
        }

        /// <summary>
        /// 鼠标双击命令行弹出命令编辑界面；如果命令行不可编辑，则无动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.enable)
                return;
            var selectedItems = listView1.SelectedItems;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                OnEditCmdLineEvent?.Invoke(commandsModule, selectedItems[0].Tag as CmdLine);
            }
        }

        ///<summary>
        /// Description	:用于返回当前选中的指令的下一指令，如果选中多条返回第一条的下一条指令
        /// Author  	:liyi
        /// Date		:2019/07/06
        ///</summary>   
        /// <param name="curCmdLineIsEnd">判断当前指令所有节点是否遍历完成</param>
        /// <returns></returns>
        public CmdLine GetNextCmdLine(bool curCmdLineIsEnd)
        {
            if (!this.enable)
                return null;
            var selectedItems = listView1.SelectedItems;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                int index = MathUtils.Limit(selectedItems[0].Index + 1, 0, listView1.Items.Count -1);
                if (index == selectedItems[0].Index)
                {
                    return null;
                }
                else
                {
                    //当前指令未结束，返回当前指令
                    if (!curCmdLineIsEnd)
                    {
                        return selectedItems[0].Tag as CmdLine;
                    }
                    //当前指令已到末尾，返回下一条指令
                    this.listView1.SelectedItems.Clear();
                    this.listView1.Items[index].Selected = true;
                    ClickCmdLine();
                    return this.listView1.Items[index].Tag as CmdLine;
                }
            }
            else
            {
                if (this.listView1.Items.Count > 0)
                {
                    return this.listView1.Items[0].Tag as CmdLine;
                }
                else
                {
                    return null;
                }
            }
        }

        public CmdLine GetLastCmdline(bool curCmdLineIsEnd)
        {
            if (!this.enable)
                return null;
            var selectedItems = listView1.SelectedItems;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                int index = MathUtils.Limit(selectedItems[0].Index - 1, 0, listView1.Items.Count);
                if (index == selectedItems[0].Index)
                {
                    return null;
                }
                else
                {
                    //当前指令未结束，返回当前指令
                    if (!curCmdLineIsEnd)
                    {
                        return selectedItems[0].Tag as CmdLine;
                    }
                    //当前指令已到开头，返回上一条指令
                    this.listView1.SelectedItems.Clear();
                    this.listView1.Items[index].Selected = true;
                    ClickCmdLine();
                    return this.listView1.Items[index].Tag as CmdLine;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 用户在外部完成对命令行的编辑后，调用该接口完成编辑
        /// </summary>
        /// <param name="cmdLine">被编辑的命令行</param>
        public void OnFinishEditingCmdLine(CmdLine cmdLine)
        {
            if (commandsModule == null)
            {
                return;
            }
            int index = -1;
            for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
            {
                if (cmdLine == commandsModule.CmdLineList[i])
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
            {
                return;
            }

            listView1.Items[index] = createItem(cmdLine);

            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (i != index)
                {
                    this.listView1.Items[i].BackColor = Color.FromArgb(30, 30, 30);// Color.White;
                }
                else
                {
                    this.listView1.Items[i].BackColor = Color.SkyBlue;
                }
            }
            
            this.UpdateDrawPanel();

            this.EditCmdLine(index);

        }

        /// <summary>
        /// 用户在外部添加命令完成参数编辑后，调用该接口完成添加
        /// </summary>
        /// <param name="cmdLines"></param>
        public void OnFinishAddingCmdLines(List<CmdLine> cmdLineList, bool isCopy)
        {
            add(cmdLineList,isCopy);
        }

        public void OnFinishInsertingCmdLines(int index, List<CmdLine> cmdLineList)
        {
            insert(index, cmdLineList,false);
        }

        public void OnFinishDeletingCmdLines(int index, int count)
        {
            delete(index, count);
        }

        /// <summary>
        /// 加载命令行
        /// </summary>
        /// <param name="cmdLines"></param>
        /// <param name="scrollToIndex">加载完成后自动滚动到指定行的索引位置</param>
        public void LoadData(CommandsModule commandsModule, int scrollToIndex = 0)
        {
            if (commandsModule == null)
            {
                throw new Exception("commandsModule can not be null.");
            }
            if (this.commandsModule == commandsModule)
            {
                scrollTo(scrollToIndex);
                Log.Dprint("update LineNumbers...");
                updateLineNumbers(true);
                Log.Dprint("update LineNumbers done!");
                return;
            }
            this.commandsModule = commandsModule;
            Log.Dprint("update listView...");
            listView1.BeginUpdate();
            listView1.Items.Clear();
            if (commandsModule.CmdLineList != null && commandsModule.CmdLineList.Count > 0)
            {
                for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
                {
                    insertListViewItem(i, commandsModule.CmdLineList[i]);
                }
            }
            resizeCmdColumnWidth();
            scrollTo(scrollToIndex);
            listView1.EndUpdate();
            Log.Dprint("update listView done!");
            Log.Dprint("update LineNumbers...");
            updateLineNumbers(true);
            Log.Dprint("update LineNumbers done!");
            OnCommandsModuleLoadedEvent?.Invoke(commandsModule);

            this.UpdateDrawPanel();
        }

        /// <summary>
        /// 刷新所有命令行的显示
        /// </summary>
        public void UpdateCmdLines()
        {
            if (commandsModule == null)
            {
                return;
            }
            int scrollToIndex = getFirstVisibleIndex();
            listView1.BeginUpdate();
            listView1.Items.Clear();
            if (commandsModule.CmdLineList != null && commandsModule.CmdLineList.Count > 0)
            {
                for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
                {
                    insertListViewItem(i, commandsModule.CmdLineList[i]);
                }
            }
            resizeCmdColumnWidth();
            scrollTo(scrollToIndex);
            listView1.EndUpdate();
            updateLineNumbers(true);

            this.UpdateDrawPanel();
        }

        /// <summary>
        /// 滚动到指定行索引位置
        /// </summary>
        ///// <param name="index"></param>
        private void scrollTo(int index)
        {
            if (listView1.Items.Count > 0)
            {
                index = MathUtils.Limit(index, 0, listView1.Items.Count);
                listView1.TopItem = listView1.Items[index];
            }
        }

        private void insertListViewItem(int index, CmdLine cmdLine)
        {
            listView1.Items.Insert(index, createItem(cmdLine));
        }

        private ListViewItem createItem(CmdLine cmdLine)
        {
            // 创建Item
            ListViewItem item = new ListViewItem();
            item.Text = cmdLine.ToString();
            if (!cmdLine.Enabled)
            {
                item.ForeColor = Color.Red;
            }
            // CommentCmdLine 永远是Enabled，所以用else if 
            else if (cmdLine is CommentCmdLine)
            {
                item.ForeColor = Color.Green;
            }
            item.Tag = cmdLine;
            return item;
        }

        #region 记录/恢复 滚动位置

        /// <summary>
        /// 获取可视范围内第一行CmdLine的索引位置
        /// </summary>
        /// <returns></returns>
        private int getFirstVisibleIndex()
        {
            if (listView1.TopItem == null)
            {
                return 0;
            }
            CmdLine cmdLine = listView1.TopItem.Tag as CmdLine;
            return commandsModule.FindCmdLineIndex(cmdLine);
        }

        //private int firstVisibleIndex = 0;
        //private CommandsModule lastCommandsModule;
        //private void recordScrollOffset()
        //{
        //    if (listView1.TopItem == null)
        //    {
        //        firstVisibleIndex = 0;
        //        return;
        //    }
        //    CmdLine cmdLine = listView1.TopItem.Tag as CmdLine;
        //    firstVisibleIndex = commandsModule.FindCmdLineIndex(cmdLine);
        //    lastCommandsModule = commandsModule;
        //    Log.Print("record first visible index=" + firstVisibleIndex);
        //}

        //private void restoreScrollOffset()
        //{
        //    if (lastCommandsModule != commandsModule)
        //    {
        //        lastCommandsModule = null;
        //        return;
        //    }
        //    lastCommandsModule = null;
        //    if (listView1.Items.Count <= 0)
        //    {
        //        return;
        //    }
        //    firstVisibleIndex = MathUtils.Limit(firstVisibleIndex, 0, listView1.Items.Count - 1);
        //    listView1.TopItem = listView1.Items[firstVisibleIndex];
        //    Log.Print("restore first visible index=" + firstVisibleIndex);
        //}
        #endregion

        /// <summary>
        /// 复制
        public void Copy()
        {
            if (!canOperate())
            {
                return;
            }
            var selectedItems = listView1.SelectedItems;
            // END 命令不可以复制
            if (selectedItems[selectedItems.Count - 1].Tag is EndCmdLine)
            {
                //MessageBox.Show("END can not be selected to copy.");
                MessageBox.Show("END指令不可以复制.");
                return;
            }
            clipBoard.Clear();
            foreach (ListViewItem item in selectedItems)
            {
                clipBoard.Add(item.Tag as CmdLine);
            }

        }

        /// <summary>
        /// 剪切
        /// </summary>
        public void Cut()
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;
            // END 命令不可以剪切
            if (commandsModule.CmdLineList[selectedIndexs[selectedIndexs.Count - 1]] is EndCmdLine)
            {
                //MessageBox.Show("END can not be selected to cut.");
                MessageBox.Show("END 指令不可以剪切.");
                return;
            }
            clipBoard.Clear();
            int oldLineNums = commandsModule.CmdLineList.Count;
            listView1.BeginUpdate();
            int i = 0;
            foreach (int index in selectedIndexs)
            {
                clipBoard.Add(commandsModule.CmdLineList[index - i]);
                commandsModule.CmdLineList.RemoveAt(index - i);
                listView1.Items.RemoveAt(index - i);
                i++;
            }
            resizeCmdColumnWidth();
            listView1.EndUpdate();
            updateLineNumbers();

            this.UpdateDrawPanel();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        public void Paste()
        {
            if (clipBoard.Count <= 0)
            {
                return;
            }
            if (!canOperate())
            {
                return;
            }
            add(clipBoard,true);

            this.UpdateDrawPanel();
        }

        public void MovePrev()
        {
            if (!canOperate())
            {
                return;
            }
            // 获取选中行及上一行
            var selectedIndexs = listView1.SelectedIndices;
            if (selectedIndexs.Count < 0) return;
            var prevIndex = selectedIndexs[0] - 1;
            if (prevIndex < 0) return;
            // END 命令不可以剪切
            if (commandsModule.CmdLineList[selectedIndexs[selectedIndexs.Count - 1]] is EndCmdLine)
            {
                return;
            }
            Dictionary<CmdLine, int> selectedCmdLines = new Dictionary<CmdLine, int>();
            int oldLineNums = commandsModule.CmdLineList.Count;
            listView1.BeginUpdate();
            int i = 0;
            foreach (int index in selectedIndexs)
            {
                selectedCmdLines.Add(commandsModule.CmdLineList[index - i], index - 1);
                commandsModule.CmdLineList.RemoveAt(index - i);
                listView1.Items.RemoveAt(index - i);
                i++;
            }
            resizeCmdColumnWidth();
            listView1.EndUpdate();
            updateLineNumbers();
            //向上粘贴选中行
            this.insert(prevIndex, selectedCmdLines.Keys.ToList(), true);
            foreach (var item in selectedCmdLines)
            {
                this.listView1.SelectedIndices.Add(item.Value);
            }
            this.UpdateDrawPanel();
        }

        public void MoveNext()
        {
            if (!canOperate())
            {
                return;
            }
            // 获取选中行及上一行
            var selectedIndexs = listView1.SelectedIndices;
            if (selectedIndexs.Count < 0) return;
            var nextIndex = selectedIndexs[selectedIndexs.Count - 1] + 2;
            if (nextIndex > listView1.Items.Count - 1) return;
            // END 命令不可以剪切
            if (commandsModule.CmdLineList[selectedIndexs[selectedIndexs.Count - 1]] is EndCmdLine)
            {
                return;
            }
            List<CmdLine> selectedCmdLines = new List<CmdLine>();
            List<int> indexes = new List<int>();
            foreach (int index in selectedIndexs)
            {
                indexes.Add(index);
            }
            int oldLineNums = commandsModule.CmdLineList.Count;
            listView1.BeginUpdate();
            int i = 0;
            foreach (int index in selectedIndexs)
            {
                selectedCmdLines.Add(commandsModule.CmdLineList[index - i]);
                commandsModule.CmdLineList.RemoveAt(index - i);
                listView1.Items.RemoveAt(index - i);
                i++;
            }
            resizeCmdColumnWidth();
            listView1.EndUpdate();
            updateLineNumbers();
            //向上粘贴选中行
            this.insert(nextIndex - selectedCmdLines.Count, selectedCmdLines, true);
            foreach (var item in indexes)
            {
                this.listView1.SelectedIndices.Add(item + 1);
            }
            this.UpdateDrawPanel();
        } 

        /// <summary>
        /// 根据比较传入的轨迹编号，对现有命令行进行处理
        /// </summary>
        /// <param name="trackNumber"></param>
        public void CompareTrackNumber(string trackNumber)
        {
            this.trackNumber = trackNumber;

            for (int i = 0; i < this.commandsModule.CmdLineList.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(trackNumber))
                {
                    this.listView1.Items[i].BackColor = Color.FromArgb(30, 30, 30);// SystemColors.Window;
                    continue;
                }

                if (this.commandsModule.CmdLineList[i].TrackNumber != null
                        && this.commandsModule.CmdLineList[i].TrackNumber.Contains(trackNumber))
                {
                    this.listView1.Items[i].BackColor = Color.Yellow;
                }
                else
                {
                    this.listView1.Items[i].BackColor = Color.FromArgb(30, 30, 30);// SystemColors.Window;
                }
            }

        }

        public List<CmdLine> GetSelectCmdLines()
        {
            List<CmdLine> selectCmdLines = new List<CmdLine>();
            if (!canOperate())
            {
                return selectCmdLines;
            }
            var selectedIndexs = listView1.SelectedIndices;
            foreach (int index in selectedIndexs)
            {
                selectCmdLines.Add(commandsModule.CmdLineList[index]);
            }
            return selectCmdLines;
        }

        /// <summary>
        /// 禁用/取消禁用选中命令
        /// </summary>
        public void Disable()
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;
            // END 命令不可以DISABLE
            if (commandsModule.CmdLineList[selectedIndexs[selectedIndexs.Count - 1]] is EndCmdLine)
            {
                //MessageBox.Show("END can not be disabled.");
                MessageBox.Show("END指令不可以禁用.");
                return;
            }
            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;
            foreach (int index in selectedIndexs)
            {
                cmdLine = commandsModule.CmdLineList[index];
                cmdLine.Enabled = !cmdLine.Enabled;
                cmdLineList.Add(index, cmdLine);
            }
            update(cmdLineList,true);

            this.UpdateDrawPanel();
        }

        /// <summary>
        /// 更新一组命令行
        /// </summary>
        /// <param name="cmdLineList"></param>
        private void update(Dictionary<int, CmdLine> cmdLineList,bool isSelected)
        {
            if (cmdLineList == null || cmdLineList.Count <= 0)
            {
                return;
            }
            listView1.BeginUpdate();
            CmdLine cmdLine = null;
            foreach (int index in cmdLineList.Keys)
            {
                cmdLine = cmdLineList[index].Clone() as CmdLine;
                commandsModule.RemoveCmdLineAt(index);
                commandsModule.InsertCmdLine(index, cmdLine);
                var item = createItem(cmdLine);
                if (isSelected)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
                listView1.Items[index] = item;
            }
            resizeCmdColumnWidth();
            listView1.EndUpdate();
        }

        /// <summary>
        /// 更新命令行
        /// </summary>
        /// <param name="cmdLine"></param>
        private void update(int index, CmdLine cmdLine)
        {
            cmdLine = cmdLine.Clone() as CmdLine;
            commandsModule.RemoveCmdLineAt(index);
            commandsModule.InsertCmdLine(index, cmdLine);
            listView1.BeginUpdate();
            var item = createItem(cmdLine);
            item.Selected = true;
            listView1.Items[index] = item;
            resizeCmdColumnWidth();
            listView1.EndUpdate();
        }

        private void insert(int index, List<CmdLine> cmdLineList,bool isCopy)
        {
            if (cmdLineList == null || cmdLineList.Count <= 0)
            {
                return;
            }

            CmdLine cmdLine = null;
            listView1.BeginUpdate();
            for (int i = 0; i < cmdLineList.Count; i++)
            {
                cmdLine = cmdLineList[i].Clone() as CmdLine;
                if (isCopy)
                {
                    cmdLine.IdCode = cmdLineList[i].IdCode;
                }
                commandsModule.InsertCmdLine(index + i, cmdLine);
                insertListViewItem(index + i, cmdLine);
            }
            for (int i = 0; i < lastSelectedIndices.Count; i++)
            {
                if (lastSelectedIndices[i]>=index)
                {
                    lastSelectedIndices[i] += cmdLineList.Count;
                }
            }
            resizeCmdColumnWidth();
            listView1.EndUpdate();
            updateLineNumbers();
        }

        /// <summary>
        /// 添加命令行
        /// </summary>
        /// <param name="cmdLineList"></param>
        private void add(List<CmdLine> cmdLineList,bool isCopy)
        {
            int index;
            if (cmdLineList == null || cmdLineList.Count <= 0 || !canOperate(out index))
            {
                return;
            }
            //int index = listView1.SelectedIndices[0];
            this.insert(index, cmdLineList,isCopy);

            this.UpdateDrawPanel();
        }

        /// <summary>
        /// 删除命令行
        /// </summary>
        /// <param name="indexes"></param>
        private void delete(int index, int count)
        {
            listView1.BeginUpdate();
            commandsModule.CmdLineList.RemoveRange(index, count);
            for (int i = 0; i < count; i++)
            {
                listView1.Items.RemoveAt(index);
            }
            resizeCmdColumnWidth();
            listView1.EndUpdate();
            updateLineNumbers();

            this.UpdateDrawPanel();
        }

        /// <summary>
        /// 检测是否可以操作，1. 当前已加载命令模块  2. 有选中行
        /// </summary>
        /// <returns></returns>
        private bool canOperate()
        {
            return commandsModule != null
                && listView1.SelectedIndices != null
                && listView1.SelectedIndices.Count > 0;
        }

        private bool canOperate(out int index)
        {
            index = 0;

            if (commandsModule == null
                || listView1.SelectedIndices == null)
            {
                return false;
            }

            if (listView1.SelectedIndices.Count == 0)
            {
                index = listView1.Items.Count - 1;
            }
            else
            {
                index = listView1.SelectedIndices[0];
            }
            return true;
        }

        /// <summary>
        /// 根据命令行文本内容调整列宽
        /// </summary>
        private void resizeCmdColumnWidth()
        {
            listView1.Columns[0].Width = -1;
        }

        /// <summary>
        /// 根据当前commandsModule更新绘图程序内容
        /// </summary>
        private void UpdateDrawPanel()
        {
            //绘图相关逻辑
            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.需要更新绘图程序, FluidProgram.CurrentOrDefault());

            if (commandsModule is Workpiece)
            {
                DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Workpiece界面);
            }
            else if (commandsModule is Pattern)
            {
                int patternNo = 0;
                Pattern pattern = commandsModule as Pattern;
                for (int i = 0; i < FluidProgram.CurrentOrDefault().Patterns.Count; i++)
                {
                    if (FluidProgram.CurrentOrDefault().Patterns[i].Name == pattern.Name)
                    {
                        patternNo = i;
                    }
                }
                DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Pattern界面, patternNo);
            }
        }

        /// <summary>
        /// 更改命令行背景色
        /// </summary>
        private void ClickCmdLine()
        {
            bool inWorkpiece;
            int patternNo = 0;
            if (commandsModule is FluidProgram)
            {
                return;
            }
            else if (commandsModule is Workpiece)
            {
                inWorkpiece = true;
            }
            else
            {
                inWorkpiece = false;

                Pattern pattern = commandsModule as Pattern;
                for (int i = 0; i < FluidProgram.CurrentOrDefault().Patterns.Count; i++)
                {
                    if (FluidProgram.CurrentOrDefault().Patterns[i].Name == pattern.Name)
                    {
                        patternNo = i;
                    }
                }
            }
            int[] cmdLiensIndex = new int[this.listView1.SelectedItems.Count];
            for (int i = 0; i < this.listView1.SelectedItems.Count; i++)
            {
                cmdLiensIndex[i] = this.listView1.SelectedItems[i].Index;
            }

            this.highLightSelectedIndices();

            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.点击了一个绘图命令, inWorkpiece, patternNo, cmdLiensIndex);
        }

        private void EditCmdLine(int index)
        {
            bool inWorkpiece;
            int patternNo = 0;
            if (commandsModule is FluidProgram)
            {
                return;
            }
            else if (commandsModule is Workpiece)
            {
                inWorkpiece = true;
            }
            else
            {
                inWorkpiece = false;

                Pattern pattern = commandsModule as Pattern;
                for (int i = 0; i < FluidProgram.CurrentOrDefault().Patterns.Count; i++)
                {
                    if (FluidProgram.CurrentOrDefault().Patterns[i].Name == pattern.Name)
                    {
                        patternNo = i;
                    }
                }
            }

            int[] cmdLiensIndex = new int[1];
            cmdLiensIndex[0] = index;

            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.点击了一个绘图命令, inWorkpiece, patternNo, cmdLiensIndex);
        }

        #region 行号显示

        /// <summary>
        /// 在指定的行位置标记编译出错
        /// </summary>
        /// <param name="index"></param>
        public void MarkCompileError(CommandsModule commandsModule, int index)
        {
            markLineAt(commandsModule, MarkType.COMPILE_ERROR, index);
        }

        /// <summary>
        /// 在指定的行位置标记程序运行暂停的位置
        /// </summary>
        /// <param name="index"></param>
        public void MarkRunningPausedPosition(CommandsModule commandsModule, int index)
        {
            markLineAt(commandsModule, MarkType.RUNNING_PAUSED_POSITION, index);
        }

        /// <summary>
        /// 清除对指定行位置的标记
        /// </summary>
        public void cancelMark()
        {
            markLineAt(null, MarkType.NONE, LINE_INDEX_INVALID);
        }

        private void markLineAt(CommandsModule commandsModule, MarkType markType, int index)
        {
            markCommandsModule = commandsModule;
            this.markType = markType;
            markLineIndex = index;
            if (commandsModule != null)
            {
                if (this.markType == MarkType.COMPILE_ERROR)
                {
                    // 如果是语法错误，不自动跳转到其他CommandsModule，
                    // 因为检测到出错的位置，和当前正在编辑的位置有可能不在同一个CommandsModule
                    if (CurrCommandsModule == markCommandsModule)
                    {
                        updateLineNumbers(true);
                    }
                }
                else
                {
                    LoadData(commandsModule, index);
                }
                //LoadData(commandsModule, index);
            }
            else
            {
                updateLineNumbers(true);
            }
        }

        private const int LINE_NUMBER_DEFAULT_WIDTH = 14;
        private int lineNumberWidth = LINE_NUMBER_DEFAULT_WIDTH;
        private const int LINE_NUMBER_PADDING = 3;
        private int lastFirstVisibleIndex;
        private int lastFirstVisibleItemY;
        private int lastEndVisibleItemY;
        private int lastVisibleItemsCount = -1;
        /// <summary>
        /// 刷新行号
        /// </summary>
        private void updateLineNumbers(bool force = false)
        {
            if (force)
            {
                panel1.Invalidate();
                return;
            }
            Graphics g = panel1.CreateGraphics();
            if (commandsModule == null)
            {
                lineNumberWidth = LINE_NUMBER_DEFAULT_WIDTH;
            }
            else
            {
                lineNumberWidth = (int)g.MeasureString(commandsModule.CmdLineList.Count.ToString(), panel1.Font).Width;
            }
            g.Dispose();
            int w = lineNumberWidth + LINE_NUMBER_PADDING * 2;
            if (panel1.Width != w)
            {
                // 调整尺寸时，会触发重绘，故无需再调用 panel1.Invalidate(); 
                panel1.Width = w;
                return;
            }
            // 判断是否需要强制重绘，刷新行号显示
            bool needInvalidate = false;
            // 之前从未重绘过
            if (lastVisibleItemsCount < 0)
            {
                needInvalidate = true;
            }
            // 上次无内容
            else if (lastVisibleItemsCount == 0)
            {
                // 本次有内容，需要重绘
                if (listView1.Items.Count > 0)
                {
                    needInvalidate = true;
                }
            }
            // 上次有内容
            else if (lastVisibleItemsCount > 0)
            {
                // 本次无内容，需要重绘
                if (listView1.Items.Count <= 0)
                {
                    needInvalidate = true;
                }
                // 本次有内容
                else
                {
                    // 需要判断可视范围内，第一行的行号索引/位置、最后一行的行号位置、行数的值是否发生变化
                    // 如果发生变化，则需要重绘
                    // 寻找第一行的位置
                    var topItem = listView1.TopItem;
                    int index = -1;
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (listView1.Items[i] == topItem)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index < 0)
                    {
                        return;
                    }
                    if (lastFirstVisibleIndex != index || lastFirstVisibleItemY != listView1.GetItemRect(index).Y)
                    {
                        needInvalidate = true;
                    }
                    else
                    {
                        // 统计可视行数
                        int count = 0;
                        // 最后一行的Y坐标
                        int lastEndY = 0;
                        // 列表可视范围的高度
                        int visibleHeight = listView1.ClientRectangle.Height;
                        // 从可视范围内的第一行开始顺序遍历
                        for (int i = index; i < listView1.Items.Count; i++)
                        {
                            var rect = listView1.GetItemRect(i);
                            // 当前行已经超过列表的可视范围：
                            if (rect.Y > visibleHeight)
                            {
                                break;
                            }
                            // 记录最后一行的Y坐标
                            lastEndY = rect.Y;
                            count++;
                        }
                        if (lastVisibleItemsCount != count || lastEndVisibleItemY != lastEndY)
                        {
                            needInvalidate = true;
                        }
                    }
                }
            }
            if (needInvalidate)
            {
                panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            if (listView1.Items.Count <= 0)
            {
                return;
            }
            Graphics g = e.Graphics;
            // 寻找第一行的位置
            var topItem = listView1.TopItem;
            int index = -1;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i] == topItem)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
            {
                Log.Print("ScriptEditor error : Find topItem in listView.Items failed.");
                return;
            }
            // 记录起始行的索引位置
            lastFirstVisibleIndex = index;
            // 记录起始行的Y坐标
            lastFirstVisibleItemY = listView1.GetItemRect(index).Y;
            // 统计可视行数
            int count = 0; 
            // 列表可视范围的高度
            int visibleHeight = listView1.ClientRectangle.Height;
            // 从可视范围内的第一行开始顺序遍历
            for (int i = index; i < listView1.Items.Count; i++)
            {
                var rect = listView1.GetItemRect(i);
                // 当前行已经超过列表的可视范围：
                if (rect.Y > visibleHeight)
                {
                    break;
                }
                // 对于需要标记的行绘制背景色
                if (i == markLineIndex && markCommandsModule == commandsModule)
                {
                    switch (markType)
                    {
                        case MarkType.COMPILE_ERROR:
                            // 指示程序编译出错的位置，绘制红色背景色
                            g.FillRectangle(Brushes.Red, 0, rect.Y, panel1.Width, rect.Height);
                            break;
                        case MarkType.RUNNING_PAUSED_POSITION:
                            // 指示程序运行暂停位置，绘制橙色背景色
                            g.FillRectangle(Brushes.Orange, 0, rect.Y, panel1.Width, rect.Height);
                            break;
                    }
                }
                // 右对齐绘制行号
                string str = (i + 1).ToString();
                SizeF size = g.MeasureString(str, panel1.Font);
                float x = panel1.Width - LINE_NUMBER_PADDING - size.Width;
                float y = rect.Y + (rect.Height - size.Height) / 2f;
                g.DrawString((i + 1).ToString(), panel1.Font, Brushes.White, x, y);
                // 记录最后一行的Y坐标
                lastEndVisibleItemY = rect.Y;
                count++;
            } // end of for
            // 记录最近一次重绘时，可视行数
            lastVisibleItemsCount = count;
        } // end of panel1_Paint

        #endregion

        #region 快捷键操作
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                switch (keyData)
                {
                    //case Keys.C | Keys.Control:
                    //    this.Copy();
                    //    break;
                    //case Keys.V | Keys.Control:
                    //    this.Paste();
                    //    break;
                    case Keys.F | Keys.Control:
                        this.FindSameCmdLine();
                        break;
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region 右键菜单
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.Items.Count == 0)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                this.cmsDisableOperation.Show(new Point(PointToScreen(e.Location).X + 20,
                    PointToScreen(e.Location).Y + 10));
            }
        }
        private void tsrPreDisable_Click(object sender, EventArgs e)
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;
            
            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;

            for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
            {
                if (i < selectedIndexs[0] && (commandsModule.CmdLineList[i] is EndCmdLine == false)) 
                {
                    cmdLine = commandsModule.CmdLineList[i];
                    cmdLine.Enabled = false;
                    cmdLineList.Add(i, cmdLine);
                }
            }

            update(cmdLineList,false);

            FluidProgram.Current.Parse();
            this.UpdateDrawPanel();
        }

        private void tsrBackDisable_Click(object sender, EventArgs e)
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;

            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;

            for (int i = 0; i < commandsModule.CmdLineList.Count - 1; i++)
            {
                if ((i > selectedIndexs[selectedIndexs.Count - 1]) 
                    && commandsModule.CmdLineList[i] is EndCmdLine == false)
                {
                    cmdLine = commandsModule.CmdLineList[i];
                    cmdLine.Enabled = false;
                    cmdLineList.Add(i, cmdLine);
                }
            }

            update(cmdLineList,false);
            FluidProgram.Current.Parse();
            this.UpdateDrawPanel();
        }

        private void tsrAllEnable_Click(object sender, EventArgs e)
        {
            if (!canOperate())
            {
                return;
            }

            List<int> selected = new List<int>();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                selected.Add(listView1.SelectedIndices[i]);
            }

            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;

            for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
            {
                cmdLine = commandsModule.CmdLineList[i];
                if (commandsModule.CmdLineList[i] is EndCmdLine == false)
                {
                    cmdLine.Enabled = true;
                }
                cmdLineList.Add(i, cmdLine);
            }

            update(cmdLineList,false);

            for (int i = 0; i < selected.Count; i++)
            {
                this.listView1.Items[selected[i]].Selected = true;
            }
            FluidProgram.Current.Parse();
            this.UpdateDrawPanel();
        }

        private void tsrOthersDisable_Click(object sender, EventArgs e)
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;
            List<int> selected = new List<int>();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                selected.Add(listView1.SelectedIndices[i]);
            }

            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;

            for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
            {                
                cmdLine = commandsModule.CmdLineList[i];
                if (commandsModule.CmdLineList[i] is EndCmdLine == false)
                {
                    cmdLine.Enabled = false;
                }
                cmdLineList.Add(i, cmdLine);
            }

            for (int i = 0; i < selectedIndexs.Count; i++)
            {
                if (cmdLineList[selectedIndexs[i]] is EndCmdLine == false)
                {
                    cmdLineList[selectedIndexs[i]].Enabled = true;
                }        
            }

            update(cmdLineList,false);

            for (int i = 0; i < selected.Count; i++)
            {
                this.listView1.Items[selected[i]].Selected = true;
            }

            FluidProgram.Current.Parse();
            this.UpdateDrawPanel();
        }

        private void tsrThisDisable_Click(object sender, EventArgs e)
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;
            List<int> selected = new List<int>();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                selected.Add(listView1.SelectedIndices[i]);
            }

            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;

            for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
            {
                cmdLine = commandsModule.CmdLineList[i];
                if (commandsModule.CmdLineList[i] is EndCmdLine == false)
                {
                    cmdLine.Enabled = true;
                }                    
                cmdLineList.Add(i, cmdLine);
            }

            for (int i = 0; i < selectedIndexs.Count; i++)
            {
                if (cmdLineList[selectedIndexs[i]] is EndCmdLine == false)
                {
                    cmdLineList[selectedIndexs[i]].Enabled = false;
                }
            }

            update(cmdLineList,false);

            for (int i = 0; i < selected.Count; i++)
            {
                this.listView1.Items[selected[i]].Selected = true;
            }

            FluidProgram.Current.Parse();
            this.UpdateDrawPanel();
        }

        private void tsiReverse_Click(object sender, EventArgs e)
        {
            if (!canOperate())
            {
                return;
            }
            var selectedIndexs = listView1.SelectedIndices;
            List<int> selected = new List<int>();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                selected.Add(listView1.SelectedIndices[i]);
            }

            Dictionary<int, CmdLine> cmdLineList = new Dictionary<int, CmdLine>();
            CmdLine cmdLine = null;

            for (int i = 0; i < commandsModule.CmdLineList.Count; i++)
            {
                cmdLine = commandsModule.CmdLineList[i];
                if (commandsModule.CmdLineList[i] is EndCmdLine == false)
                {
                    cmdLine.Enabled = true;
                }
                cmdLineList.Add(i, cmdLine);
            }

            for (int i = 0; i < selectedIndexs.Count; i++)
            {
                if (cmdLineList[selectedIndexs[i]] is DoCmdLine)
                {
                    DoCmdLine doCmdLine = cmdLineList[selectedIndexs[i]] as DoCmdLine;
                    doCmdLine.Reverse = true;
                }
            }

            update(cmdLineList, false);

            for (int i = 0; i < selected.Count; i++)
            {
                this.listView1.Items[selected[i]].Selected = true;
            }

            FluidProgram.Current.Parse();
            this.UpdateDrawPanel();
        }

        #endregion

        #region 级联筛选操作

        /// <summary>
        /// 找到ID相同的命令，并且背景色强调显示
        /// </summary>
        private void FindSameCmdLine()
        {
            if (this.listView1.SelectedItems == null)
                return;
            for (int i = 0; i < this.commandsModule.CmdLineList.Count; i++)
            {
                if (this.commandsModule.CmdLineList[i].IdCode 
                    == this.commandsModule.CmdLineList[this.listView1.SelectedIndices[0]].IdCode)
                {
                    this.listView1.Items[i].BackColor = Color.SkyBlue;
                    this.listView1.Items[i].Selected = true;
                }
                else
                {
                    this.listView1.Items[i].BackColor = Color.FromArgb(30, 30, 30);// SystemColors.Window;
                    this.listView1.Items[i].Selected = false;
                }
            }
        }
        #endregion
        public void EditSingleDrawCmd(int cmdLineNo)
        {
            if (!this.enable && this.Parent.Parent == null) 
                return;
            OnEditCmdLineEvent?.Invoke(commandsModule, listView1.Items[cmdLineNo].Tag as CmdLine);
        }
    }
}
