using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditDoMultipassForm1 : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private DoMultiPassCmdLine doMultipassCmdLine;
        private DoMultiPassCmdLine doMultipassCmdLineBackUp;
        private List<string> patternNameList = new List<string>();
        private string Tag = "EditDoMultipassForm1";
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditDoMultipassForm1()
        {
            InitializeComponent();
        }

        public EditDoMultipassForm1(Pattern pattern) : this(pattern, null)
        {
        }

        public EditDoMultipassForm1(Pattern pattern, DoMultiPassCmdLine doMultipassCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.cbxValveType.Items.Add(ValveType.Valve1);
            this.cbxValveType.Items.Add(ValveType.Valve2);
            this.cbxValveType.Items.Add(ValveType.Both);
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (doMultipassCmdLine == null)
            {
                isCreating = true;
                this.doMultipassCmdLine = new DoMultiPassCmdLine();
            }
            else
            {
                isCreating = false;
                this.doMultipassCmdLine = doMultipassCmdLine;
            }

            // patter list
            foreach (Pattern item in FluidProgram.Current.Patterns)
            {
                if (!item.HasPassBlocks)
                {
                    continue;
                }
                if (item.Name == this.pattern.Name) // 阵列指令所在拼版不显示，自循环引用防呆
                {
                    continue;
                }
                patternNameList.Add(item.Name);
            }
            foreach (string patternName in patternNameList)
            {
                listBoxPatterns.Items.Add(patternName);
            }
            if (isCreating)
            {
                // 新建时，默认选中第一条
                if (listBoxPatterns.Items.Count > 0)
                {
                    listBoxPatterns.SelectedIndex = 0;
                }
            }
            // 选中当前patter name
            else
            {
                int index = -1;
                for (int i = 0; i < patternNameList.Count; i++)
                {
                    if (patternNameList[i] == doMultipassCmdLine.PatternName)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > -1)
                {
                    listBoxPatterns.SelectedIndex = index;
                }
            }
            //系统坐标->机械坐标
            PointD p = this.pattern.MachineRel(this.doMultipassCmdLine.Origin);
            tbOriginX.Text = p.X.ToString("0.000");
            tbOriginY.Text = p.Y.ToString("0.000");
            this.cbxValveType.SelectedItem = this.doMultipassCmdLine.Valve;
            this.txtBoardNo.Text = this.doMultipassCmdLine.BoardNo.ToString();
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.lblValveType.Visible = false;
                this.cbxValveType.Visible = false;
            }
            if (this.doMultipassCmdLine != null)
            {
                this.doMultipassCmdLineBackUp = (DoMultiPassCmdLine)this.doMultipassCmdLine.Clone();
            }
            this.ReadLanguageResources();
        }

        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            tbOriginX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbOriginY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, System.EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbOriginX.Value, origin.Y + tbOriginY.Value);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (listBoxPatterns.Items.Count <= 0)
            {
                //MessageBox.Show("No pattern is available!");
                MessageBox.Show("没有合适的拼版");
                return;
            }
            if (listBoxPatterns.SelectedItem == null)
            {
                MessageBox.Show("没有选择拼版");
                return;
            }
            if (!tbOriginX.IsValid || !tbOriginY.IsValid)
            {
                MessageBox.Show("请输入正确的原点值");
                return;
            }
            doMultipassCmdLine.PatternName = patternNameList[listBoxPatterns.SelectedIndex];
            //机械坐标->系统坐标
            PointD p = this.pattern.SystemRel(tbOriginX.Value, tbOriginY.Value);
            doMultipassCmdLine.Origin.X = p.X;
            doMultipassCmdLine.Origin.Y = p.Y;
            doMultipassCmdLine.Valve = (ValveType)this.cbxValveType.SelectedItem;
            doMultipassCmdLine.BoardNo = this.txtBoardNo.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, doMultipassCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, doMultipassCmdLine);
            }
            if (!this.isCreating)
            {
                Close();
            }
            if (this.doMultipassCmdLine!=null && this.doMultipassCmdLineBackUp!=null)
            {
                CompareObj.CompareField(this.doMultipassCmdLine, this.doMultipassCmdLineBackUp, null, this.Tag, true);
                CompareObj.CompareProperty(this.doMultipassCmdLine, this.doMultipassCmdLineBackUp, null, this.Tag, true);
            }
            
        }
    }
}
