using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive.Conveyor.LeadShine;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;
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
    public partial class ConveyorWidthForm : FormEx
    {
        public ConveyorWidthForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            if (!ConveyorMachine.Instance.AxisYHomeIsDone)
            {
                this.btnAxisYDown.Enabled = false;
                this.btnAxisYUp.Enabled = false;
                this.btnStop.Enabled = false;
                this.btnTeach.Enabled = false;
                this.btnGo.Enabled = false;
            }
            this.ReadLanguageResources();
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorMachine.Instance.MoveHome(10);
                this.BeginInvoke(new Action(() =>
                {
                    this.btnAxisYDown.Enabled = true;
                    this.btnAxisYUp.Enabled = true;
                    this.btnStop.Enabled = true;
                    this.btnTeach.Enabled = true;
                    this.btnGo.Enabled = true;
                }));
            }));           
        }
        private double ConveyorWidthBackUp = 0;
        private void btnTeach_Click(object sender, EventArgs e)
        {
            FluidProgram.CurrentOrDefault().ConveyorWidth = double.Parse(this.txtLiveLocation.Text);
            string msg = string.Format("ConveyorWidth:{0}->{1}", ConveyorWidthBackUp, double.Parse(this.txtLiveLocation.Text));
            Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
            this.ConveyorWidthBackUp = FluidProgram.CurrentOrDefault().ConveyorWidth;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double pos = ConveyorMachine.Instance.GetYPos() ;
            this.txtLiveLocation.Text = pos.ToString();
        }


        private void btnAxisYUp_MouseUp(object sender, MouseEventArgs e)
        {
            ConveyorMachine.Instance.AxisYStop();
        }

        private void btnAxisYDown_MouseDown(object sender, MouseEventArgs e)
        {
            ConveyorMachine.Instance.AxisYJogBackMove(ConveyorPrmMgr.Instance.FindBy(0).Speed, ConveyorPrmMgr.Instance.FindBy(0).AccTime);
        }

        private void btnAxisYDown_MouseUp(object sender, MouseEventArgs e)
        {
            ConveyorMachine.Instance.AxisYStop();
        }

        private void btnAxisYUp_MouseDown(object sender, MouseEventArgs e)
        {
            ConveyorMachine.Instance.AxisYJogForwardMove(ConveyorPrmMgr.Instance.FindBy(0).Speed, ConveyorPrmMgr.Instance.FindBy(0).AccTime);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ConveyorMachine.Instance.AxisYStop();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.btnGo.Enabled = false;
            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorMachine.Instance.AxisYMovePos(FluidProgram.Current.ConveyorWidth,
                ConveyorPrmMgr.Instance.FindBy(0).Speed, ConveyorPrmMgr.Instance.FindBy(0).AccTime);
                this.BeginInvoke(new Action(() =>
                {
                    this.btnGo.Enabled = true;
                }));
            }));
            
        }
    }
}
