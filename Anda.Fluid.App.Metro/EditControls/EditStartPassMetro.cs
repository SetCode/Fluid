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
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.App.Common;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditStartPassMetro : MetroSetUserControl, IMsgSender
    {
        private StartPassCmdLine startPassCmdLine;
        private StartPassCmdLine startPassCmdLineBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditStartPassMetro()
        {
            InitializeComponent();
        }

        public EditStartPassMetro(StartPassCmdLine startPassCmdLine)
        {
            InitializeComponent();
            //this.ReadLanguageResources();

            if (startPassCmdLine == null)
            {
                throw new Exception("start pass cmd line is null.");
            }
            this.startPassCmdLine = startPassCmdLine;
            tbIndex.Text = this.startPassCmdLine.Index.ToString();
            tbIndex.SelectAll();
            if (this.startPassCmdLine != null)
            {
                this.startPassCmdLineBackUp = (StartPassCmdLine)this.startPassCmdLine.Clone();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!tbIndex.IsValid)
            {
                //MessageBox.Show("Please input valid value.");
                MetroSetMessageBox.Show(this, "请输入正确的值.");
                return;
            }
            startPassCmdLine.Index = tbIndex.Value;
            MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, startPassCmdLine);
            if (this.startPassCmdLine != null && this.startPassCmdLineBackUp != null)
            {
                CompareObj.CompareField(this.startPassCmdLine, this.startPassCmdLineBackUp, null, this.GetType().Name, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
