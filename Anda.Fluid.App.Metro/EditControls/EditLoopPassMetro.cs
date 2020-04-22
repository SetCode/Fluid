using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditLoopPassMetro : MetroSetUserControl, IMsgSender
    {
        private bool isCreating;
        private LoopPassCmdLine loopPassCmdLine;
        private LoopPassCmdLine loopPassCmdLineBackUp;

        public EditLoopPassMetro() : this(null)
        {
        }

        public EditLoopPassMetro(LoopPassCmdLine loopPassCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();

            if (loopPassCmdLine == null)
            {
                isCreating = true;
                this.loopPassCmdLine = new LoopPassCmdLine(1, 3);
            }
            else
            {
                isCreating = false;
                this.loopPassCmdLine = loopPassCmdLine;
            }
            tbStart.Text = this.loopPassCmdLine.Start.ToString();
            tbEnd.Text = this.loopPassCmdLine.End.ToString();
            if (this.loopPassCmdLine != null)
            {
                this.loopPassCmdLineBackUp = (LoopPassCmdLine)this.loopPassCmdLine.Clone();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbStart.IsValid || !tbEnd.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MetroSetMessageBox.Show(this, "请输入正确的值");
                return;
            }
            if (tbStart.Value > tbEnd.Value)
            {
                //MessageBox.Show("Start value can not be bigger than end value.");
                MetroSetMessageBox.Show(this, "起始点不可以大于结束点.");
                return;
            }
            loopPassCmdLine.Start = tbStart.Value;
            loopPassCmdLine.End = tbEnd.Value;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, loopPassCmdLine, new NextLoopCmdLine());
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, loopPassCmdLine);
            }
            if (!this.isCreating)
            {
                if (this.loopPassCmdLine != null && this.loopPassCmdLineBackUp != null)
                {
                    CompareObj.CompareField(this.loopPassCmdLine, this.loopPassCmdLineBackUp, null, this.GetType().Name, true);
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
