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
using System.Threading;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive.DeviceType;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class LaserControl : UserControlEx
    {
        private PointD measuredPos = new PointD();
        public bool canMeasure = true;

        public LaserControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
        }

        public Action<double> ValueReaded;

        public Action<PointD> MeasureStarting;

        public TextBox TxtRead => this.txtRead;

        private async void btnStatus_Click(object sender, EventArgs e)
        {
            bool b = false;
            await Task.Factory.StartNew(() =>
            {
                double value;         
                Machine.Instance.MeasureHeightBefore();
                b = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value) >= 0;
                Machine.Instance.MeasureHeightAfter();
               
            });
            if (b)
            {
                this.txtStatus.BackColor = Color.Green;
                this.txtStatus.Text = "通讯成功";
            }
            else
            {
                this.txtStatus.BackColor = Color.Red;
                this.txtStatus.Text = "通讯失败";
            }
        }

        public void canMeasure_way()
        {
            this.btnMeasure.Enabled = canMeasure;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.ReadValue();
        }

        private async void btnMeasure_Click(object sender, EventArgs e)
        {
            MeasureStarting?.Invoke(measuredPos);
            Machine.Instance.Light.None();
            await Task.Factory.StartNew(() =>
            {
                Result rtn = Machine.Instance.Robot.MoveSafeZAndReply();
                if (rtn != Result.OK)
                {
                    return;
                }
                PointD pos = new PointD();
                pos.X = this.measuredPos.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X;
                pos.Y = this.measuredPos.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y;
                //rtn = Machine.Instance.Robot.MovePosXYAndReply(pos);
                rtn = Machine.Instance.Robot.ManualMovePosXYAndReply(pos);
                if (rtn != Result.OK)
                {
                    return;
                }
                Thread.Sleep(100);
            });
            this.ReadValue();
            Machine.Instance.Light.ResetToLast();
        }

        private async void ReadValue()
        {
            double value = 0;
            int rtn = 0;
            await Task.Factory.StartNew(() =>
            {
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(true);
                //}
                Machine.Instance.MeasureHeightBefore();
                rtn = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value);
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(false);
                //}
                Machine.Instance.MeasureHeightAfter();
                //Result res = Machine.Instance.MeasureHeight(out value);
                //rtn = res.IsOk ? 0 : -1;
            });

            if (rtn == 0)
            {
                this.txtRead.BackColor = Color.White;
                this.txtRead.Text = value.ToString("0.000");
            }
            else if (rtn > 0)
            {
                this.txtRead.BackColor = Color.Yellow;
                this.txtRead.Text = value.ToString("0.000");
            }
            else
            {
                this.txtRead.BackColor = Color.Red;
                this.txtRead.Text = "读取失败";
            }
            this.ValueReaded?.Invoke(value);
        }
    }
}
