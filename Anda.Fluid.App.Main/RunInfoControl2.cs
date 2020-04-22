using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Drive;

namespace Anda.Fluid.App.Main
{
    public partial class RunInfoControl2 : UserControlEx, IMsgReceiver
    {
        public RunInfoControl2()
        {
            InitializeComponent();
            this.txtDotWeight.ReadOnly = true;
            this.txtBarcode.ReadOnly = true;
            this.txtMeasureHeight.ReadOnly = true;
            this.ReadLanguageResources();
        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (msgName == Domain.MsgType.MSG_CURRENT_BARCODE)
            {
                if (args[0] != null)
                {
                    this.txtBarcode.Text = args[0] as string;
                }
            }
            else if (msgName == MachineMsg.SINGLEDROPWEIGHT_UPDATE)
            {
                if (args[0] != null)
                {
                    this.txtDotWeight.Text = ((double)args[0]).ToString();
                }
            }
            else if (msgName == Domain.MsgType.MSG_CURRENT_HEIGHT)
            {
                if (args[0] != null)
                {
                    this.txtMeasureHeight.Text = ((double)args[0]).ToString();
                }
            }
            else
            {

            }
        }
    }
}
