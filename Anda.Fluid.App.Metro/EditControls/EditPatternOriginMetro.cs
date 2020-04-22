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
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.EditControls
{
    public partial class EditPatternOriginMetro : MetroSetUserControl, IMsgSender, ICanSelectButton
    {
        private Pattern pattern;
        private Workpiece workpiece;
        private PointD patternOrgBackUp = new PointD();
        private PointD workPieceOrgBackUp = new PointD();
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditPatternOriginMetro()
        {
            InitializeComponent();
        }
        public EditPatternOriginMetro(Pattern pattern, Workpiece workpiece)
        {
            InitializeComponent();
            //this.ReadLanguageResources();
            this.pattern = pattern;
            this.workpiece = workpiece;
            tbPatternName.Text = this.pattern.Name;
            tbPatternName.Enabled = false;

            if (pattern is Workpiece)
            {
                tbOriginX.Text = this.pattern.GetOriginPos().X.ToString("0.000");
                tbOriginY.Text = this.pattern.GetOriginPos().Y.ToString("0.000");
            }
            else
            {
                //系统坐标->机械坐标
                PointD p = workpiece.MachineRel(this.pattern.Origin);
                tbOriginX.Text = p.X.ToString("0.000");
                tbOriginY.Text = p.Y.ToString("0.000");
            }
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (pattern is Workpiece)
            {
                tbOriginX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
                tbOriginY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
            }
            else
            {
                tbOriginX.Text = (Machine.Instance.Robot.PosX - pattern.Program.Workpiece.GetOriginPos().X).ToString("0.000");
                tbOriginY.Text = (Machine.Instance.Robot.PosY - pattern.Program.Workpiece.GetOriginPos().Y).ToString("0.000");
            }
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            if (pattern is Workpiece)
            {
                //Machine.Instance.Robot.MovePosXY(tbOriginX.Value, tbOriginY.Value);
                Machine.Instance.Robot.ManualMovePosXY(tbOriginX.Value, tbOriginY.Value);
            }
            else
            {
                //Machine.Instance.Robot.MovePosXY(pattern.Program.Workpiece.GetOriginPos().X + tbOriginX.Value, pattern.Program.Workpiece.GetOriginPos().Y + tbOriginY.Value);
                Machine.Instance.Robot.ManualMovePosXY(pattern.Program.Workpiece.GetOriginPos().X + tbOriginX.Value, pattern.Program.Workpiece.GetOriginPos().Y + tbOriginY.Value);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string patternName = tbPatternName.Text.Trim();

            if (!tbOriginX.IsValid || !tbOriginY.IsValid)
            {
                //MessageBox.Show("Please input valid origin values.");
                MetroSetMessageBox.Show(this, "请输入正确的原点坐标.");
                return;
            }

            if (pattern is Workpiece)
            {
                Workpiece w = pattern as Workpiece;
                w.OriginPos.X = tbOriginX.Value;
                w.OriginPos.Y = tbOriginY.Value;
                FluidProgram.Current.GetWorkPieceCmdLine().Origin.X = tbOriginX.Value;
                FluidProgram.Current.GetWorkPieceCmdLine().Origin.Y = tbOriginY.Value;
                CompareObj.CompareField(w.OriginPos, workPieceOrgBackUp, null, this.GetType().Name, true);
            }
            else
            {
                //机械坐标->系统坐标
                PointD p = this.workpiece.SystemRel(tbOriginX.Value, tbOriginY.Value);
                pattern.Origin.X = p.X;
                pattern.Origin.Y = p.Y;
                CompareObj.CompareField(p, patternOrgBackUp, null, this.GetType().Name, true);
            }

            MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_PATTREN_ORIGIN, null, null);
            MsgCenter.Broadcast(MsgDef.MSG_PARAMPAGE_CLEAR, null);

            Log.Print("change pattern origin to : " + pattern.Origin);
            // 移动到新原点位置
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(pattern.GetOriginPos());
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            Pattern p = pattern.ReversePattern();
            MsgCenter.Broadcast(Constants.MSG_ADD_PATTERN, this, p);
        }

        private void EditPattern_Load(object sender, EventArgs e)
        {
            if (pattern is Workpiece)
            {
                Workpiece w = pattern as Workpiece;
                workPieceOrgBackUp = (PointD)w.OriginPos.Clone();
            }
            else
            {
                patternOrgBackUp = (PointD)pattern.Origin.Clone();
            }
        }

        public void SetSelectButtons()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(this.btnSelect);
            buttons.Add(this.btnOk);
            HookHotKeyMgr.Instance.GetSelectKey().SetButtons(buttons);
        }
    }
}
