using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
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

namespace Anda.Fluid.App
{
    public partial class EditConveyor2Origin : EditFormBase
    {
        private PointD Conveyor2OriginOffsetBackUp = new PointD();
        public EditConveyor2Origin()
        {
            InitializeComponent();
            if (FluidProgram.Current!=null && FluidProgram.Current.Conveyor2OriginOffset!=null)
            {
                tbX.Text = (FluidProgram.Current.Conveyor2OriginOffset.X + FluidProgram.Current.Workpiece.OriginPos.X).ToString();
                tbY.Text = (FluidProgram.Current.Conveyor2OriginOffset.Y + FluidProgram.Current.Workpiece.OriginPos.Y).ToString();
            }            
            this.ReadLanguageResources();
        }

        private void btnTeach_Click(object sender, EventArgs e)
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
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的参数.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            Close();
            MsgCenter.Broadcast(Constants.MSG_TEACH_CONVEYOR2_ORIGIN, null, tbX.Value, tbY.Value);
            
            CompareObj.CompareField(new PointD(tbX.Value, tbY.Value), Conveyor2OriginOffsetBackUp,null ,this.GetType().Name, true);
        }

        private void EditConveyor2Origin_Load(object sender, EventArgs e)
        {
            if (FluidProgram.Current != null)
            {
                this.Conveyor2OriginOffsetBackUp = FluidProgram.Current.Conveyor2OriginOffset;

            }
        }
    }
}
