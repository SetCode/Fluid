using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Lighting.Custom;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Domain.Vision
{
    public partial class LightSettingControl : UserControl
    {
        
        public ExecutePrm ExecutePrm = new ExecutePrm();
        private int min = 0;
        private int max = 20;
       
        public LightSettingControl()
        {
            InitializeComponent();
            int tickFrequency = (max - min) / 10;
            this.tbRed.Maximum = max;
            this.tbRed.Minimum = min;
            this.tbRed.SmallChange = 1;
            this.tbRed.LargeChange = 5;
            this.tbRed.TickFrequency = tickFrequency;

            this.tbGreen.Maximum = max;
            this.tbGreen.Minimum = min;
            this.tbGreen.SmallChange = 1;
            this.tbGreen.LargeChange = 5;
            this.tbGreen.TickFrequency = tickFrequency;

            this.tbBlue.Maximum = max;
            this.tbBlue.Minimum = min;
            this.tbBlue.SmallChange = 1;
            this.tbBlue.LargeChange = 5;
            this.tbBlue.TickFrequency = tickFrequency;
            if(Machine.Instance.Light==null || Machine.Instance.Light.ExecutePrm==null)
            {
                return;
            }
            this.SetupLight(Machine.Instance.Light.ExecutePrm);
        }
        private bool isSetup = false;
        public LightSettingControl SetupLight(ExecutePrm prm)
        {
            if (prm!=null)
            {
                this.ExecutePrm = (ExecutePrm)prm.Clone();
            
            }
            this.isSetup = true;      
            updateSetting();            
            Machine.Instance.Light.SetLight(this.ExecutePrm);
            this.isSetup = false;
            return this;
        }


        private void updateSetting()
        {

            ItensityClass itensityCls;
            itensityCls = this.ExecutePrm.PrmOPT.FindChn(LightChn.Red);
            if (itensityCls != null)
            {
                if ((int)itensityCls.Chanel == LightingOPT.SwitchChannel(LightChn.Red))
                {
                    
                    this.tbRed.Value = MathUtils.Limit(itensityCls.Value, this.min, this.max);
                    this.lblChn1.Text = this.tbRed.Value.ToString();
                }
            }
            itensityCls = this.ExecutePrm.PrmOPT.FindChn(LightChn.Green);
            if (itensityCls != null)
            {
                if ((int)itensityCls.Chanel == LightingOPT.SwitchChannel(LightChn.Green))
                {                    
                    this.tbGreen.Value = MathUtils.Limit(itensityCls.Value, this.min, this.max);
                    this.lblChn2.Text = this.tbGreen.Value.ToString();
                }
            }
            itensityCls = this.ExecutePrm.PrmOPT.FindChn(LightChn.Blue);
            if (itensityCls != null)
            {

                if ((int)itensityCls.Chanel == LightingOPT.SwitchChannel(LightChn.Blue))
                {
                    this.tbBlue.Value = MathUtils.Limit(itensityCls.Value, this.min, this.max);
                    this.lblChn3.Text = this.tbBlue.Value.ToString();
                }
            }
        }



        private void tbRed_ValueChanged(object sender, EventArgs e)
        {
            setChannels();          
        }

        private void tbGreen_ValueChanged(object sender, EventArgs e)
        {
            setChannels();           
        }

        private void tbBlue_ValueChanged(object sender, EventArgs e)
        {
            setChannels();          
        }

        private void setChannels()
        {
            if (this.isSetup)
            {
                return;
            }
            this.lblChn1.Text = this.tbRed.Value.ToString();
            this.lblChn2.Text = this.tbGreen.Value.ToString();
            this.lblChn3.Text = this.tbBlue.Value.ToString();
            //this.lblChn4.Text=this.
            this.ExecutePrm.PrmOPT.AddRedChn(this.tbRed.Value);
            this.ExecutePrm.PrmOPT.AddGreenChn(this.tbGreen.Value);
            this.ExecutePrm.PrmOPT.AddBlueChn(this.tbBlue.Value);

            Machine.Instance.Light.SetLight(this.ExecutePrm);
        }
    }
}
