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
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.Motion
{
    public partial class PositionVControl : UserControlEx, IControlUpdating
    {
        private double relativeOriginX = 0;
        private double relativeOriginY = 0;
        public PositionVControl()
        {
            InitializeComponent();

            this.lblx.Text = "0";
            this.lbly.Text = "0";
            this.lblz.Text = "0";

            ControlUpatingMgr.Add(this);
            this.ReadLanguageResources();
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(true, true, true, true);
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(true, true, true, true);
        }

        public void SetRelativeOrigin(PointD origin)
        {
            this.relativeOriginX = origin.X;
            this.relativeOriginY = origin.Y;
        }

        public void Updating()
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            if (this.ChkSwitch.Checked)
            {
                double x = Machine.Instance.Robot.PosX - this.relativeOriginX;
                double y = Machine.Instance.Robot.PosY - this.relativeOriginY;
                double z = Machine.Instance.Robot.PosZ;

                this.lblx.Text = x.ToString();
                this.lbly.Text = y.ToString();
                this.lblz.Text = z.ToString();
            }
            else
            {
                this.lblx.Text = Machine.Instance.Robot?.PosX.ToString();
                this.lbly.Text = Machine.Instance.Robot?.PosY.ToString();
                this.lblz.Text = Machine.Instance.Robot?.PosZ.ToString();
            }

        }
    }
}
