using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using System.Threading;
using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class HeightControl : UserControlEx
    {
        private FluidProgram fluidProgram;
        private PointD measuredPos = new PointD();
        public bool canMeasure = true;

        public HeightControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.nudMaxTolerance.Minimum = 0;
            this.nudMaxTolerance.Maximum = 20;
            this.nudMaxTolerance.Increment = 0.001M;
            this.nudMaxTolerance.DecimalPlaces = 3;

            this.nudMinTolerance.Minimum = -20;
            this.nudMinTolerance.Maximum = 0;
            this.nudMinTolerance.Increment = 0.001M;
            this.nudMinTolerance.DecimalPlaces = 3;

            this.laserControl1.ValueReaded = v =>
            {
                if (v > this.txtBoardHeight.Value + (double)this.nudMaxTolerance.Value
                    || v < this.txtBoardHeight.Value + (double)this.nudMinTolerance.Value)
                {
                    this.laserControl1.TxtRead.BackColor = Color.Yellow;
                }
            };
        }

        public LaserControl LaserControl => this.laserControl1;

        public double MaxTolerance => (double)this.nudMaxTolerance.Value;

        public double MinTolerance => (double)this.nudMinTolerance.Value;

        public double BoardHeight
        {
            get
            {
                try
                {
                    return txtBoardHeight.Value;
                }
                catch
                {
                    return Machine.Instance.Robot.CalibPrm.StandardHeight;
                }
            }
        }

        public void canMeasure_way()
        {
            this.laserControl1.canMeasure = this.canMeasure;
            this.laserControl1.canMeasure_way();
        }


        public void SetupCmdLine(MeasureHeightCmdLine cmdLine)
        {
            this.txtBoardHeight.Text = cmdLine.StandardHt.ToString("0.000");
            this.nudMaxTolerance.Value = (decimal)cmdLine.ToleranceMax;
            this.nudMinTolerance.Value = (decimal)cmdLine.ToleranceMin;
        }

        public void SetupFluidProgram(FluidProgram fluidProgram)
        {
            this.fluidProgram = fluidProgram;
            this.txtBoardHeight.Text = this.fluidProgram.RuntimeSettings.StandardBoardHeight.ToString("0.000");
            this.nudMaxTolerance.Value = (decimal)this.fluidProgram.RuntimeSettings.MaxTolerance;
            this.nudMinTolerance.Value = (decimal)this.fluidProgram.RuntimeSettings.MinTolerance;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                double value = double.Parse(this.laserControl1.TxtRead.Text);
                this.txtBoardHeight.Text = value.ToString("0.000");
            }
            catch
            {

            }
        }
    }
}
