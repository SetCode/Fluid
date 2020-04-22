using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Alarming
{
    public partial class AlarmTransparencyForm : Form, IRealTimeAlarmObservable
    {
        private ConcurrentDictionary<string, Tuple<IAlarmSenderable, AlarmInfo>> alarmDic = new ConcurrentDictionary<string, Tuple<IAlarmSenderable, AlarmInfo>>();
        private IWin32Window owner;

        private bool changeColor = true;
        private Image imgFatal = Properties.Resources.Flash_Bang_16px;
        private Image imgError = Properties.Resources.Cancel_16px;
        private Image imgWarn = Properties.Resources.Error_16px;
        private Image imgInfo = Properties.Resources.Info_16px;
        private Image imgDebug = Properties.Resources.Bug_16px;
        private Image imgAlarm = Properties.Resources.Alarm_96px;
        public AlarmTransparencyForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        public void SetOwner(IWin32Window owner)
        {
            this.owner = owner;
        }

        public void HandleRealTimeAlarm(ConcurrentDictionary<string, Tuple<IAlarmSenderable, AlarmInfo>> dic)
        {
            if (this.alarmDic.Equals(dic)) 
            {
                return;
            }
            else
            {
                this.alarmDic = dic;                
            }
        }
        private void UpdateData()
        {
            this.BeginInvoke(new Action(() =>
            {
                if(changeColor)
                {
                    this.richTextBox1.ForeColor = Color.Black;
                    this.richTextBox1.BackColor = Color.Red;
                    changeColor = false;
                    this.pictureBox1.BackgroundImage = this.imgAlarm;
                    this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                }
                else
                {
                    this.richTextBox1.ForeColor = Color.White;
                    this.richTextBox1.BackColor = Color.Black;
                    changeColor = true;
                    this.pictureBox1.BackgroundImage = null;
                }

                this.richTextBox1.Clear();

                foreach (var item in alarmDic)
                {
                    this.richTextBox1.AppendText(string.Format("{0}:{1}\r\n", item.Value.Item1.Name, item.Value.Item2.Message));
                }

            }));
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.alarmDic.Count > 0)
            {
                if(!this.Visible)
                {
                    if (this.owner != null)
                    {
                        this.Show(this.owner);
                    }
                    else
                    {
                        this.Show();
                    }
                }
                this.UpdateData();
            }
            else
            {
                if (this.IsHandleCreated && this.Visible )
                {
                    this.Hide();
                }              
            }
        }

    }
}
