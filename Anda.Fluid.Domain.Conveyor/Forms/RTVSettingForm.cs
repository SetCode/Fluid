using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Conveyor.Forms
{
    public partial class RTVSettingForm : Form
    {
        public RTVSettingForm()
        {
            InitializeComponent();
            DoType.上层轨道调宽刹车.Set(true);
            DoType.下层轨道调宽刹车.Set(true);
            this.txtSpeed.Text = "5";
        }

        private void btnUpConveyorForward_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnUpConveyorForward")
            {
                AxisType.Axis6.MoveJog(this.txtSpeed.Value);
            }
            else if (btn.Name == "btnUpConveyorBack")
            {
                AxisType.Axis6.MoveJog(-this.txtSpeed.Value);
            }
            else if (btn.Name == "btnDownConveyorForward")
            {
                AxisType.Axis8.MoveJog(this.txtSpeed.Value);
            }
            else if (btn.Name == "btnDownConveyorBack")
            {
                AxisType.Axis8.MoveJog(-this.txtSpeed.Value);
            }
        }

        private void btnUpConveyorForward_MouseUp(object sender, MouseEventArgs e)
        {
            AxisType.Axis6.MoveStop();
            AxisType.Axis8.MoveStop();
        }

        private void RTVSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DoType.上层轨道调宽刹车.Set(false);
            DoType.下层轨道调宽刹车.Set(false);
        }
    }
}
