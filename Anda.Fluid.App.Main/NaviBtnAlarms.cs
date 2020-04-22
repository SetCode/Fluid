using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.International;
namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnAlarms : UserControl
    {
        private AlarmControl alarmControl;
        public NaviBtnAlarms()
        {
            InitializeComponent();
            this.btnAlarms.Click += BtnAlarms_Click;
            UserControlEx UserControlEx = new UserControlEx();
            this.btnAlarms.MouseMove += UserControlEx.ReadDisplayTip;
            this.btnAlarms.MouseLeave += UserControlEx.DisopTip;
        }

        public void SetAlarmControl(AlarmControl alarmControl)
        {
            this.alarmControl = alarmControl;
        }

        public void ShowAlarms(bool reShow = false)
        {
            if(reShow)
            {
                FormMgr.GetForm<AlarmForm>().Visible = false;
            }
            if(FormMgr.GetForm<AlarmForm>().Visible)
            {
                return;
            }
            FormMgr.GetForm<AlarmForm>().AddControl(this.alarmControl).Show(this);
        }

        private void BtnAlarms_Click(object sender, EventArgs e)
        {
            //ShowAlarms(true);
        }
    }
}
