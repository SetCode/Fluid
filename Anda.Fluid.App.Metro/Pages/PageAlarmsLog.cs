using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageAlarmsLog : MetroSetUserControl
    {
        public PageAlarmsLog()
        {
            InitializeComponent();
            this.ShowBorder = false;
            AlarmServer.Instance.Register(this.alarmControl1);
        }
    }
}
