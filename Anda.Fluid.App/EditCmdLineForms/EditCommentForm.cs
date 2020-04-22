using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditCommentForm : FormEx, IMsgSender
    {
        private CommentCmdLine commentCmdLine;
        private CommentCmdLine commentCmdLineBackUp;
        private bool isCreating;

        public EditCommentForm(CommentCmdLine commentCmdLine)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

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
            this.ReadLanguageResources();
            if (this.commentCmdLine != null)
            {
                this.commentCmdLineBackUp = (CommentCmdLine)this.commentCmdLine.Clone();
            }
        }

        public EditCommentForm() : this(null)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            Close();
            if (this.commentCmdLine!=null && this.commentCmdLineBackUp!=null)
            {
                CompareObj.CompareField(this.commentCmdLine, this.commentCmdLineBackUp, null, this.GetType().Name, true);
            }
            
        }
    }
}
