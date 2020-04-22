using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive.Vision.CameraFramework;
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
    public partial class EditBoardHeightForm : EditFormBase
    {
        private FluidProgram fluidProgram;
        private PointD origin;
        private RuntimeSettings runtimeSettingsBackUp;
        public EditBoardHeightForm(FluidProgram fluidProgram)
        {
            InitializeComponent();
            this.fluidProgram = fluidProgram;
            this.origin = fluidProgram.Workpiece.OriginPos;
            tbX.Text = fluidProgram.RuntimeSettings.HeightPosX.ToString("0.000");
            tbY.Text = fluidProgram.RuntimeSettings.HeightPosY.ToString("0.000");
            this.heightControl1.SetupFluidProgram(this.fluidProgram);
            this.heightControl1.LaserControl.MeasureStarting += HeightControl1_MeasureStarting;

            if (this.fluidProgram.RuntimeSettings != null)
            {
                this.runtimeSettingsBackUp = (RuntimeSettings)this.fluidProgram.RuntimeSettings.Clone();
            }
            this.ReadLanguageResources();
        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditBoardHeightForm()
        {
            InitializeComponent();
        }

        private void HeightControl1_MeasureStarting(PointD obj)
        {
            obj.X = origin.X + tbX.Value;
            obj.Y = origin.Y + tbY.Value;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbX.Value, origin.Y + tbY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbX.Value, origin.Y + tbY.Value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入合理的值");
                return;
            }
            this.fluidProgram.RuntimeSettings.HeightPosX = tbX.Value;
            this.fluidProgram.RuntimeSettings.HeightPosY = tbY.Value;
            this.fluidProgram.RuntimeSettings.StandardBoardHeight = this.heightControl1.BoardHeight;
            this.fluidProgram.RuntimeSettings.MaxTolerance = this.heightControl1.MaxTolerance;
            this.fluidProgram.RuntimeSettings.MinTolerance = this.heightControl1.MinTolerance;
            this.DialogResult = DialogResult.OK;
            this.Close();
            if (FluidProgram.Current.RuntimeSettings!=null && this.runtimeSettingsBackUp!=null)
            {
                CompareObj.CompareProperty(FluidProgram.Current.RuntimeSettings, this.runtimeSettingsBackUp, false);
                CompareObj.CompareField(FluidProgram.Current.RuntimeSettings, this.runtimeSettingsBackUp, null, this.GetType().Name, false);
            }
           
           
        }
    }
}
