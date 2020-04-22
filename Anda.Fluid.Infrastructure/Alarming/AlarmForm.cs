using Anda.Fluid.Infrastructure.International;
using System;
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
    public partial class AlarmForm : FormEx
    {
        private int buttonWidth = 75;
        private int buttonHeight = 30;
        private int widthInterval = 0;
        private Button[] buttons;
        private Dictionary<DialogResult, Action> dicAction;
        private List<DialogResult> dialogList;
        private Action action;
        public AlarmForm(Dictionary<DialogResult,Action> dic,List<string> alarmInfo)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.dicAction = dic;
            
            buttons = new Button[dicAction.Count];
            dialogList = dicAction.Keys.ToList();
           
            for (int i = 0; i < dic.Count; i++)
            {
                buttons[i] = new Button();
                buttons[i].Width = buttonWidth;
                buttons[i].Height = buttonHeight;
                //buttons[i].Text = dialogList[i].ToString();
                buttons[i].Name = dialogList[i].ToString();
                Action action;
                dic.TryGetValue(dialogList[i], out action);
                buttons[i].Click += this.ClickButtons;
                if (dic.Count == 1)
                {
                    buttons[i].Location = new Point(10 + this.richTextBox1.Width - this.buttonWidth, this.richTextBox1.Height+20);
                }
                else
                {
                    this.widthInterval = (this.richTextBox1.Width - dic.Count * this.buttonWidth) / (dic.Count - 1);
                    buttons[i].Location = new Point(10 + i * (this.buttonWidth + this.widthInterval), this.richTextBox1.Height + 20);
                }             
                buttons[i].Show();
                buttons[i].Parent = this;
            }

            for (int i = 0; i < alarmInfo.Count; i++)
            {
                this.richTextBox1.AppendText(alarmInfo[i] + "\r\n");
            }

            this.ReadLanguageResources();

        }

        private void ClickButtons(object sender,EventArgs e)
        {
            Button button = (Button)sender;
            Alarming.AlarmServer.Instance.StopVoice();
            foreach (var item in this.dialogList)
            {
                if (button.Name.Equals(item.ToString()))
                {
                    this.Close();                                    
                    this.dicAction.TryGetValue(item, out action);
                    this.DialogResult = item;               
                }
            }
        }

        private void btnStopLightTower_Click(object sender, EventArgs e)
        {
            Alarming.AlarmServer.Instance.StopVoice();
        }

        private void AlarmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            action?.Invoke();
        }

    }
}
