using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anda.Fluid.App.Common;
using System.Linq;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class AddPassBlockForm : FormEx, IMsgSender
    {
        private bool isCreating;
        private PassBlockCmdLine passBlockCmdLine;
        private PassBlockCmdLine passBlockCmdLineBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private AddPassBlockForm()
        {
            InitializeComponent();
        }
        public AddPassBlockForm(PassBlockCmdLine passBlockCmdLine)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            if (passBlockCmdLine == null)
            {
                this.Text = "AddPassBlockForm";
                this.isCreating = true;
                if (Properties.Settings.Default.passBlockStart < 1)
                {
                    Properties.Settings.Default.passBlockStart = 1;
                }
                if (Properties.Settings.Default.passBlockEnd < 2)
                {
                    Properties.Settings.Default.passBlockEnd = 2;
                }
                tbStartIndex.Text = Properties.Settings.Default.passBlockStart.ToString();
                tbEndIndex.Text = Properties.Settings.Default.passBlockEnd.ToString();
            }
            else
            {
                this.Text = "EditPassBlockForm";
                this.passBlockCmdLine = passBlockCmdLine;
                tbStartIndex.Text = passBlockCmdLine.StartIndex.ToString();
                tbEndIndex.Text = passBlockCmdLine.EndIndex.ToString();
            }
            this.ReadLanguageResources();
            if (passBlockCmdLine != null)
            {
                this.passBlockCmdLineBackUp = (PassBlockCmdLine)this.passBlockCmdLine.Clone();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbStartIndex.IsValid)
            {
                //MessageBox.Show("Please input integer number for start index.");
                MessageBox.Show("请输入起始值");
                return;
            }
            if (!tbEndIndex.IsValid)
            {
                //MessageBox.Show("Please input integer number for end index.");
                MessageBox.Show("请输入结束值");
                return;
            }
            if (tbStartIndex.Value > tbEndIndex.Value)
            {
                //MessageBox.Show("Start index can not be bigger than end index.");
                MessageBox.Show("起始值不可以大于结束值");
                return;
            }

            int startIndex = tbStartIndex.Value;
            int endIndex = tbEndIndex.Value;
            Properties.Settings.Default.passBlockStart = startIndex;
            Properties.Settings.Default.passBlockEnd = endIndex;

            if (isCreating)
            {
                this.passBlockCmdLine = new PassBlockCmdLine(startIndex, endIndex);
                List<CmdLine> cmdLineList = new List<CmdLine>();
                cmdLineList.Add(passBlockCmdLine);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    cmdLineList.Add(new StartPassCmdLine(i));
                    cmdLineList.Add(new EndPassCmdLine());
                }
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, cmdLineList.ToArray());
            }
            else
            {
                this.passBlockCmdLine.StartIndex = startIndex;
                this.passBlockCmdLine.EndIndex = endIndex;

                List<CmdLine> cmdLineList = this.passBlockCmdLine.CommandsModule.CmdLineList;
                //保存StartPassCmdLine的[passIndex, listIndex]
                Dictionary<int, int> startIndexes = new Dictionary<int, int>();
                //保存EndPassCmdLine的[passIndex, listIndex]
                Dictionary<int, int> endIndexes = new Dictionary<int, int>();
                int startIndexMin = 0;
                int startIndexMax = 0;

                //获取所有的StartPassCmdLine和EndPassCmdLine的[passIndex, listIndex]
                Action getStartEndIndexes = () =>
                {
                    startIndexes.Clear();
                    endIndexes.Clear();
                    int tempIndex = 0;
                    foreach (var item in cmdLineList)
                    {
                        if (item is StartPassCmdLine)
                        {
                            StartPassCmdLine startPassCmdLine = item as StartPassCmdLine;
                            startIndexes.Add(startPassCmdLine.Index, cmdLineList.IndexOf(item));
                            tempIndex = startPassCmdLine.Index;
                        }
                        else if (item is EndPassCmdLine)
                        {
                            EndPassCmdLine endPassCmdLine = item as EndPassCmdLine;
                            endIndexes.Add(tempIndex, cmdLineList.IndexOf(item));
                        }
                    }
                    startIndexMin = startIndexes.Keys.Min();
                    startIndexMax = startIndexes.Keys.Max();
                };

                getStartEndIndexes();
                if (startIndex < startIndexMin)
                {//起始index小于初始值
                    List<CmdLine> newLines = new List<CmdLine>();
                    for (int i = startIndex; i < startIndexMin; i++)
                    {
                        newLines.Add(new StartPassCmdLine(i));
                        newLines.Add(new EndPassCmdLine());
                    }
                    MsgCenter.Broadcast(Constants.MSG_FINISH_INSERTING_CMD_LINE, this, 1, newLines);
                    MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, this.passBlockCmdLine);
                }
                else if (startIndex > startIndexMin)
                {//起始index大于初始值
                    List<int> indexes = new List<int>();
                    for (int i = 1; i < startIndexes[startIndex]; i++)
                    {
                        indexes.Add(i);
                    }
                    MsgCenter.Broadcast(Constants.MSG_FINISH_DELETING_CMD_LINE, this, indexes[0], indexes.Count);
                    MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, this.passBlockCmdLine);
                }

                getStartEndIndexes();
                if (endIndex > startIndexMax)
                {//结束index大于初始值
                    List<CmdLine> newLines = new List<CmdLine>();
                    for (int i = startIndexMax + 1; i <= endIndex; i++)
                    {
                        newLines.Add(new StartPassCmdLine(i));
                        newLines.Add(new EndPassCmdLine());
                    }
                    MsgCenter.Broadcast(Constants.MSG_FINISH_INSERTING_CMD_LINE, this, endIndexes[startIndexMax] + 1, newLines);
                    MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, this.passBlockCmdLine);
                }
                else if (endIndex < startIndexMax)
                {//结束index小于初始值
                    List<int> indexes = new List<int>();
                    for (int i = endIndexes[endIndex] + 1; i <= endIndexes[startIndexMax]; i++)
                    {
                        indexes.Add(i);
                    }
                    MsgCenter.Broadcast(Constants.MSG_FINISH_DELETING_CMD_LINE, this, indexes[0], indexes.Count);
                    MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, this.passBlockCmdLine);
                }
            }
            Close();
            if (this.passBlockCmdLine!=null && this.passBlockCmdLineBackUp!=null)
            {
                CompareObj.CompareProperty(this.passBlockCmdLine, this.passBlockCmdLineBackUp, null, this.GetType().Name, true);
                CompareObj.CompareField(this.passBlockCmdLine, this.passBlockCmdLineBackUp, null, this.GetType().Name, true);
            }
           
        }

    }
}
