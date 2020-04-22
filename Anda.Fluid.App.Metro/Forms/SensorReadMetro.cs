using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.App.Metro.Forms
{
    public partial class SensorReadMetro : MetroSetForm
    {
        private string scannerTitle = "Scanner";
        private string laserTitle = "Laser";

        public int type { get; private set; }

        ///<summary>
        /// Description	:构造函数
        /// Author  	:liyi
        /// Date		:2019/07/11
        ///</summary>   
        /// <param name="type">硬件类型
        ///     0:条码枪
        ///     1：激光尺
        /// </param>
        public SensorReadMetro(int type)
        {
            InitializeComponent();
            this.AllowResize = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.ReadLanguageResources();
            this.type = type;
            if (this.type == 0)
            {
                this.Text = this.scannerTitle;
            }
            else if (this.type == 1)
            {
                this.Text = this.laserTitle;
            }
        }

        private SensorReadMetro()
        {
            InitializeComponent();
        }

        public SensorReadMetro Setup(int type)
        {
            this.type = type;
            if (this.type == 0)
            {
                this.Text = this.scannerTitle;
            }
            else if (this.type == 1)
            {
                this.Text = this.laserTitle;
            }
            return this;
        }

        private async void btnReadValue_Click(object sender, EventArgs e)
        {
            if (this.type == 0)
            {
                string res = "";
                int rtn = 0;
                if (((Control.ModifierKeys & Keys.ShiftKey) == Keys.ShiftKey) && (Machine.Instance.Setting.ConveyorSelect == ConveyorSelection.双轨))
                {
                    await Task.Factory.StartNew(() =>
                    {
                        rtn = Machine.Instance.BarcodeSanncer2.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(1), out res);
                    });
                }
                else
                {
                    await Task.Factory.StartNew(() =>
                    {
                        rtn = Machine.Instance.BarcodeSanncer1.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(1), out res);
                    });
                }

                if (rtn >= 0 && !res.Equals(""))
                {

                    this.txtValue.Text = res;
                }
                else
                {
                    this.txtValue.Text = "failed";
                }
            }
            else if (this.type == 1)
            {
                double value = 0;
                int rtn = 0;
                await Task.Factory.StartNew(() =>
                {
                    Machine.Instance.MeasureHeightBefore();
                    rtn = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value);
                    Machine.Instance.MeasureHeightAfter();
                    //Result res = Machine.Instance.MeasureHeight(out value);
                    //rtn = res.IsOk ? 0 : -1;
                });

                if (rtn == 0)
                {
                    this.txtValue.Text = value.ToString("0.000");
                }
                else if (rtn > 0)
                {
                    this.txtValue.Text = value.ToString("0.000");
                }
                else
                {
                    this.txtValue.Text = "failed";
                }
            }
        }
    }
}
