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

namespace Anda.Fluid.Domain.Motion
{
    public partial class ValveControl : UserControl
    {
        private int vavelNo;
        public ValveControl()
        {
            InitializeComponent();
            this.vavelNo = 1;

            this.nudValve1.Minimum = 0;
            this.nudValve1.Maximum = 32760;

            this.cbxValve1.Checked = false;
            this.cbxValve1.CheckedChanged += CbxValve1_CheckedChanged;
        }
        public void Setup(int vavelNo)
        {
            this.vavelNo = vavelNo;
            this.cbxValve1.Text = string.Format("Vavel{0}", this.vavelNo);
        }
        private void CbxValve1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxValve1.Checked)
            {
                if (this.nudValve1.Value == 0)
                {
                    if (this.vavelNo == 1)
                    {
                        Machine.Instance.Valve1.Spraying();
                    }
                    else if (this.vavelNo == 2)
                    {
                        Machine.Instance.Valve2.Spraying();
                    }                 
                }
                else
                {
                    if (this.vavelNo == 1)
                    {
                        Machine.Instance.Valve2.SprayCycle((short)this.nudValve1.Value);
                    }
                    else if (this.vavelNo == 2)
                    {
                        Machine.Instance.Valve2.SprayCycle((short)this.nudValve1.Value);
                    }                    
                }
                this.nudValve1.Enabled = false;
            }
            else
            {
                if (this.vavelNo == 1)
                {
                    Machine.Instance.Valve1.SprayOff();
                }
                else if (this.vavelNo == 2)
                {
                    Machine.Instance.Valve2.SprayOff();
                }                
                this.nudValve1.Enabled = true;
            }
        }
    }
}
