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
    public partial class EditCommentsMetro : MetroSetUserControl, IMsgSender
    {
        private CommentCmdLine commentCmdLine;
        private CommentCmdLine commentCmdLineBackUp;
        private bool isCreating;

        public EditCommentsMetro(CommentCmdLine commentCmdLine)
        {
            InitializeComponent();

            if (commentCmdLine == null)
            {
                isCreating = true;
                this.commentCmdLine = new CommentCmdLine("");
            }
            else
            {
                isCreating = false;
                this.commentCmdLine = commentCmdLine;
                tbContent.Text = commentCmdLine.Content;
                tbContent.SelectAll();
            }
            //this.ReadLanguageResources();
            if (this.commentCmdLine != null)
            {
                this.commentCmdLineBackUp = (CommentCmdLine)this.commentCmdLine.Clone();
            }
        }

        public EditCommentsMetro() : this(null)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            commentCmdLine.Content = tbContent.Text;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, commentCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, commentCmdLine);
            }

            if (this.commentCmdLine != null && this.commentCmdLineBackUp != null)
            {
                CompareObj.CompareField(this.commentCmdLine, this.commentCmdLineBackUp, null, this.GetType().Name, true);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
