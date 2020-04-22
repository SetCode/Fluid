using Anda.Fluid.Infrastructure.Utils;
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

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class ShowTimerForm : FormEx
    {
        private long currMills;
        private int waitMills;
        private Timer timer;
        /// <summary>
        /// 仅用于语言文本生成
        /// </summary>
        public ShowTimerForm()
        {
            InitializeComponent();
        }
        public ShowTimerForm(long currMills, int waitMills)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.currMills = currMills;
            this.waitMills = waitMills;

            this.timer = new Timer();
            this.timer.Interval = 100;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int sleepMills = (int)(currMills + waitMills - DateUtils.CurrTimeInMills);
            if (sleepMills <= 0)
            {
                this.Close();
                return;
            }
            this.lblSleepTime.Text = string.Format("{0} s", (sleepMills / 1000.0).ToString("0.0"));
        }
    }
}
