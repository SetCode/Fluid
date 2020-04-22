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

namespace Anda.Fluid.App.Metro.Forms
{
    public partial class LoadingMetro : MetroSetForm
    {
        public LoadingMetro()
        {
            InitializeComponent();
            this.AllowResize = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.metroSetProgressBar1.Minimum = 0;
            this.metroSetProgressBar1.Maximum = 100;

            Machine.Instance.HardwareIniting += Instance_Initing;
            Machine.Instance.HardwareInited += Instance_InitedItem;
            Machine.Instance.AllHardwareInited += Instance_InitedAll;
        }

        private void Instance_InitedAll(bool obj)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (obj)
                {
                    this.metroSetProgressBar1.Value = 100;
                }
                this.Close();
            }));
        }

        private void Instance_Initing(Machine.Hardware obj)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.lblMessage.Style = this.styleManager1.Style;
                this.lblMessage.Text = string.Format("Initing {0} ...", obj);
            }));
        }

        private void Instance_InitedItem(Machine.Hardware arg1, bool arg2, int arg3)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (!arg2)
                {
                    this.lblMessage.ForeColor = Color.Red;
                    this.lblMessage.Text = string.Format("Init {0} Failed !!!", arg1);
                }
                else
                {
                    this.metroSetProgressBar1.Value += arg3;
                    this.lblMessage.Style = this.styleManager1.Style;
                    this.lblMessage.Text = string.Format("Init {0} Done.", arg1);
                }
            }));
        }
    }
}
