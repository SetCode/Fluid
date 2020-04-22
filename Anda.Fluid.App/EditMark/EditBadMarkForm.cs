using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
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

namespace Anda.Fluid.App.EditMark
{
    public partial class EditBadMarkForm : JogFormBase, IMsgSender
    {
        private Pattern pattern;
        private bool isCreating;
        private BadMarkCmdLine badMarkCmdLine;
        private BadMarkCmdLine badMarkCmdLineBackUp;
        private EditModelFindForm editModelFindForm;
        private EditGrayCheckForm editGrayCheckForm;
        

        #region 语言切换变量
        private string[] messageTip = new string[2]
        {
            "Model is no edit.",
            "Gray parameter is no edit"
        };
        #endregion

        public EditBadMarkForm(Pattern pattern, BadMarkCmdLine badMarkCmdLine)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.pattern = pattern;
            if (badMarkCmdLine == null)
            {
                this.isCreating = true;
                badMarkCmdLine = new BadMarkCmdLine();
            }
            this.badMarkCmdLine = badMarkCmdLine;

            if (this.badMarkCmdLine.FindType == BadMarkCmdLine.BadMarkType.ModelFind)
            {
                rbModelFind.Checked = true;
            }
            else
            {
                rbGrayScale.Checked = true;
            }

            if (this.badMarkCmdLine.IsOkSkip)
            {
                rbOkSkip.Checked = true;
            }
            else
            {
                rbNgSkip.Checked = true;
            }

            this.ReadLanguageResources();
            if (this.badMarkCmdLine != null)
                this.badMarkCmdLineBackUp = (BadMarkCmdLine)this.badMarkCmdLine.Clone();
        }

        public EditBadMarkForm(Pattern pattern)
            : this(pattern, null)
        {

        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditBadMarkForm()
        {
            InitializeComponent();
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            for (int i = 0; i < messageTip.Length; i++)
            {
                string temp = "";
                temp = this.ReadKeyValueFromResources(string.Format("messageTip{0}", i));
                if (temp != "")
                {
                    messageTip[i] = temp;
                }
            }
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            for (int i = 0; i < messageTip.Length; i++)
            {
                this.SaveKeyValueToResources(string.Format("messageTip{0}",i),messageTip[i]);
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbModelFind.Checked)
            {
                badMarkCmdLine.FindType = BadMarkCmdLine.BadMarkType.ModelFind;
                if (this.editModelFindForm != null)
                {
                    if (this.editModelFindForm.DialogResult == DialogResult.OK)
                    {
                        badMarkCmdLine.Position.X = badMarkCmdLine.ModelFindPrm.PosInPattern.X;
                        badMarkCmdLine.Position.Y = badMarkCmdLine.ModelFindPrm.PosInPattern.Y;
                    }
                }
                else if (isCreating)
                {
                    MessageBox.Show(messageTip[0]);
                    return;
                }
            }
            else
            {
                badMarkCmdLine.FindType = BadMarkCmdLine.BadMarkType.GrayScale;
                if (this.editGrayCheckForm != null)
                {
                    if (this.editGrayCheckForm.DialogResult == DialogResult.OK)
                    {
                        badMarkCmdLine.Position.X = badMarkCmdLine.GrayCheckPrm.PosInPattern.X;
                        badMarkCmdLine.Position.Y = badMarkCmdLine.GrayCheckPrm.PosInPattern.Y;
                    }
                }
                else if (isCreating)
                {
                    MessageBox.Show(messageTip[1]);
                    return;
                }
            }

            if (rbNgSkip.Checked)
            {
                badMarkCmdLine.IsOkSkip = false;
            }
            else
            {
                badMarkCmdLine.IsOkSkip = true;
            }

            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, badMarkCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, badMarkCmdLine);
            }
            Close();
            CompareObj.CompareMember(this.badMarkCmdLine, this.badMarkCmdLineBackUp, null, this.GetType().Name, true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Machine.Instance.Robot.MoveSafeZ();
            PointD pos = (pattern.GetOriginSys() + badMarkCmdLine.Position).ToMachine();
            if (rbModelFind.Checked)
            {
                if (this.badMarkCmdLine.ModelFindPrm.ModelId != 0)
                {
                    Machine.Instance.Robot.ManualMovePosXY(pos);
                }
                this.editModelFindForm = new EditModelFindForm(this.badMarkCmdLine.ModelFindPrm, pattern);
                this.editModelFindForm.TopLevel = false;
                this.editModelFindForm.Parent = this.panel1;
                this.editModelFindForm.FormBorderStyle = FormBorderStyle.None;
                this.editModelFindForm.StartPosition = FormStartPosition.CenterParent;
                this.editModelFindForm.Show();
            }
            else if (rbGrayScale.Checked)
            {
                if (this.badMarkCmdLine.GrayCheckPrm.IsCreated)
                {
                    Machine.Instance.Robot.ManualMovePosXY(pos);
                }
                this.editGrayCheckForm = new EditGrayCheckForm(this.badMarkCmdLine.GrayCheckPrm, pattern);
                this.editGrayCheckForm.TopLevel = false;
                this.editGrayCheckForm.Parent = this.panel1;
                this.editGrayCheckForm.FormBorderStyle = FormBorderStyle.None;
                this.editGrayCheckForm.StartPosition = FormStartPosition.CenterParent;
                this.editGrayCheckForm.Show();

            }
        }
    }
}
