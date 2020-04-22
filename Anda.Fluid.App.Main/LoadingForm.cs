using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.Main
{
    public partial class LoadingForm : FormEx
    {
        public LoadingForm()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;

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
                if(obj)
                {
                    this.progressBar1.Value = 100;
                }
                this.Close();
            }));
        }

        private void Instance_Initing(Machine.Hardware obj)
        {
            if(!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.lblMessage.ForeColor = Color.Black;
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
                if(!arg2)
                {
                    this.lblMessage.ForeColor = Color.Red;
                    this.lblMessage.Text = string.Format("Init {0} Failed !!!", arg1);
                }
                else
                {
                    this.progressBar1.Value += arg3;
                    this.lblMessage.ForeColor = Color.Black;
                    this.lblMessage.Text = string.Format("Init {0} Done.", arg1);
                }
            }));
        }
    }
}
