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
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.App.Common;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class NewProgramMetro : MetroSetUserControl
    {
        public NewProgramMetro()
        {
            InitializeComponent();
            //this.ReadLanguageResources();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            tbY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(tbX.Value, tbY.Value);
            Machine.Instance.Robot.ManualMovePosXY(tbX.Value, tbY.Value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string programName = tbName.Text.Trim();
            if (string.IsNullOrWhiteSpace(programName))
            {
                //MessageBox.Show("Please input program name.");
                MetroSetMessageBox.Show(this, "请输入程序名称.");
                return;
            }
            if (!tbX.IsValid || !tbY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MetroSetMessageBox.Show(this, "请输入正确的参数.");
                return;
            }
            MsgCenter.Broadcast(Constants.MSG_NEW_PROGRAM, null, programName, tbX.Value, tbY.Value);
            string msg = string.Format("new a praogram{0} at [{1},{2}]", programName, tbX.Value, tbY.Value);
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }
    }
}
