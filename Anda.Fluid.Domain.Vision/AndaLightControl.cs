using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Utils;

namespace Anda.Fluid.Domain.Vision
{
    public partial class AndaLightControl : UserControl
    {
        public AndaLightControl()
        {
            InitializeComponent();

            this.tbrExposure.Minimum = 0;
            this.tbrExposure.Maximum = 10000;
            this.tbrExposure.SmallChange = 5;
            this.tbrExposure.TickFrequency = 100;
            this.tbrExposure.ValueChanged += TbrExposure_ValueChanged;

            this.tbrGain.Minimum = 300;
            this.tbrGain.Maximum = 850;
            this.tbrGain.TickFrequency = 100;
            this.tbrGain.SmallChange = 5;
            this.tbrGain.ValueChanged += TbrGain_ValueChanged;

            this.cbxLight.Items.Add(LightType.None);
            this.cbxLight.Items.Add(LightType.Coax);
            this.cbxLight.Items.Add(LightType.Ring);
            this.cbxLight.Items.Add(LightType.Both);
            this.cbxLight.SelectedIndexChanged += CbxLight_SelectedIndexChanged;
        }

        public int ExposureTime => this.tbrExposure.Value;

        public int Gain => this.tbrGain.Value;

        public LightType LightType => (LightType)this.cbxLight.SelectedItem;

        public void Setup(int exposureTime, int gain, LightType lightType)
        {
            this.tbrExposure.Value = MathUtils.Limit(exposureTime, this.tbrExposure.Minimum, this.tbrExposure.Maximum);
            this.tbrGain.Value = MathUtils.Limit(gain, this.tbrGain.Minimum, this.tbrGain.Maximum);
            this.cbxLight.SelectedItem = lightType;
        }

        private void TbrExposure_ValueChanged(object sender, EventArgs e)
        {
            this.lblExpo.Text = this.tbrExposure.Value.ToString();

            if (Machine.Instance.Camera == null)
            {
                return;
            }

            if (Machine.Instance.Camera.SetExposure((int)this.tbrExposure.Value) != 0)
            {
                return;
            }
            Properties.Settings.Default.exposureTime = (int)this.tbrExposure.Value;
        }

        private void TbrGain_ValueChanged(object sender, EventArgs e)
        {
            this.lblGain.Text = this.tbrGain.Value.ToString();

            if (Machine.Instance.Camera == null)
            {
                return;
            }

            if (Machine.Instance.Camera.SetGain((int)this.tbrGain.Value) != 0)
            {
                return;
            }
            Properties.Settings.Default.gain = (int)this.tbrGain.Value;
        }

        private void CbxLight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Machine.Instance.Camera == null || Machine.Instance.Light == null)
            {
                return;
            }
            if(Machine.Instance.Light.ExecutePrm == null)
            {
                Machine.Instance.Light.ExecutePrm = new ExecutePrm();
            }
            Machine.Instance.Light.ExecutePrm.LightType = (LightType)this.cbxLight.SelectedIndex;
            Machine.Instance.Light.SetLight(Machine.Instance.Light.ExecutePrm);
            Properties.Settings.Default.ligthType = this.cbxLight.SelectedIndex;
        }
    }
}
