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
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Msg;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class NewPatternMetro : MetroSetUserControl, IMsgSender
    {
        private Workpiece workpiece;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private NewPatternMetro()
        {
            InitializeComponent();
        }
        public NewPatternMetro(Workpiece workpiece)
        {
            InitializeComponent();
            //this.Activated += AddPattern1_Activated;
            //this.ReadLanguageResources();
            this.workpiece = workpiece;
        }

        //private void AddPattern1_Activated(object sender, EventArgs e)
        //{
        //    this.tbPatternName.Focus();
        //}

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbOriginX.Text = (Machine.Instance.Robot.PosX - workpiece.OriginPos.X).ToString("0.000");
            tbOriginY.Text = (Machine.Instance.Robot.PosY - workpiece.OriginPos.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(workpiece.OriginPos.X + tbOriginX.Value, workpiece.OriginPos.Y + tbOriginY.Value);
            Machine.Instance.Robot.ManualMovePosXY(workpiece.OriginPos.X + tbOriginX.Value, workpiece.OriginPos.Y + tbOriginY.Value);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string patternName = tbPatternName.Text.Trim();
            if (string.IsNullOrWhiteSpace(patternName))
            {
                //MessageBox.Show("Pattern name can not be empty or white space.");
                MetroSetMessageBox.Show(this, "请输入拼版名称");
                return;
            }

            if (FluidProgram.Current.GetPatternByName(patternName) != null)
            {
                //MessageBox.Show("Pattern " + patternName + " is already existed.");
                MetroSetMessageBox.Show(this, "拼版 " + patternName + " 已经存在");
                return;
            }

            if (FluidProgram.Current.Name == patternName)
            {
                //MessageBox.Show("Pattern name can not be same with program name.");
                MetroSetMessageBox.Show(this, "拼版名称不能和程序名称相同.");
                return;
            }

            if (!tbOriginX.IsValid || !tbOriginY.IsValid)
            {
                //MessageBox.Show("Please input valid origin values.");
                MetroSetMessageBox.Show(this, "请输入合理的原点值");
                return;
            }
            //机械坐标->系统坐标
            PointD p = this.workpiece.SystemRel(tbOriginX.Value, tbOriginY.Value);
            Pattern pattern = new Pattern(FluidProgram.Current, tbPatternName.Text.Trim(), p.X, p.Y);
            MsgCenter.Broadcast(Constants.MSG_ADD_PATTERN, this, pattern);

            string msg = string.Format("Add pattern {0} at [{1},{2}]", tbPatternName.Text.Trim(), p.X, p.Y);
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
        }
    }
}
