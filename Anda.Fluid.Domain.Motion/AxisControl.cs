using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.International;
using System.Reflection;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Motion
{
    public partial class AxisControl : UserControlEx
    {
        private Axis axis;
        private AxisPrm PrmBackUp;

        public AxisControl()
        {
            InitializeComponent();

            this.btnClrSts.Click += BtnClrSts_Click;
            this.btnServo.Click += BtnServo_Click;
            this.btnZero.Click += BtnZero_Click;
            this.btnHome.Click += BtnHome_Click;
            this.propertyGrid1.PropertySort = PropertySort.Categorized;

            this.ReadLanguageResources();
        }

        public Axis Axis => this.axis;

        public event Action<Axis> HomeClicked;

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveProportyGridLngText(new AxisPrm(999));
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            if (this.axis == null)
            {
                return;
            }
            this.HomeClicked?.Invoke(this.axis);
        }

        private void BtnZero_Click(object sender, EventArgs e)
        {
            if (this.axis == null)
            {
                return;
            }
            this.axis.ZeroPos();
        }

        private void BtnServo_Click(object sender, EventArgs e)
        {
            if (this.axis == null)
            {
                return;
            }
            if(this.axis.IsServoOn.Value)
            {
                this.axis.Servo(false);
            }
            else
            {
                this.axis.Servo(true);
            }
        }

        private void BtnClrSts_Click(object sender, EventArgs e)
        {
            if(this.axis == null)
            {
                return;
            }
            this.axis.ClrSts();
        }

        public AxisControl Setup(Axis axis)
        {
            this.axis = axis;
            this.lblKey.Text = this.axis.Key.ToString();
            this.lblId.Text = this.axis.AxisId.ToString();
            this.lblName.Text = this.axis.Name;
            this.PrmBackUp = (AxisPrm)this.axis.Prm.Clone();
            LngPropertyProxyTypeDescriptor proxyObj = new LngPropertyProxyTypeDescriptor(axis.Prm, this.GetType().Name);
            this.propertyGrid1.SelectedObject = proxyObj;
            return this;
        }

        public void UpdateAxis()
        {
            if(this.axis == null)
            {
                return;
            }
            this.lblEncPos.Text = this.axis.Pos.ToString("0.000");
            this.lblAlarm.Text = (this.axis.IsAlarm.Value) ? "1" : "0";
            this.lblAstop.Text = (this.axis.IsAbruptStop.Value) ? "1" : "0";
            this.lblError.Text = (this.axis.IsError.Value) ? "1" : "0";
            this.lblMoving.Text = (this.axis.IsMoving.Value) ? "1" : "0";
            this.lblNLmt.Text = (this.axis.IsNLmt.Value) ? "1" : "0";
            this.lblPLmt.Text = (this.axis.IsPLmt.Value) ? "1" : "0";
            this.lblServo.Text = (this.axis.IsServoOn.Value) ? "1" : "0";
            this.lblSstop.Text = (this.axis.IsSmoothStop.Value) ? "1" : "0";
        }

        public void Save()
        {
            if (this.axis == null)
            {
                return;
            }
            AxisPrmMgr.Instance.Save();
            CompareObj.CompareProperty(this.axis.Prm, this.PrmBackUp, null, this.GetType().Name);
        }

        public void Reset()
        {
            if (this.axis == null)
            {
                return;
            }
            //if (MessageBox.Show(string.Format("reset axis {0} setting to default?", this.axis.Name), "reset",
            //MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            if (MessageBox.Show(string.Format("将轴 {0} 的参数设置为默认?", this.axis.Name), "重置",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                MotionUtil.ResetAxisPrm(this.axis);   
                this.propertyGrid1.SelectedObject = this.axis.Prm;
                CompareObj.CompareProperty(this.axis.Prm, this.PrmBackUp);
            }
        }
    }
}
