using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.Motion
{
    public partial class DOControl : UserControlEx
    {
        private DO d = null;
        private string HeightLevel = "高电平";
        private string LowLevel = "低电平";

        public DOControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.txtId.ReadOnly = true;
            this.txtName.ReadOnly = true;
            this.button1.Click += Button1_Click;
            this.comboBoxValidValue.Items.Add(HeightLevel);
            this.comboBoxValidValue.Items.Add(LowLevel);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(this.d == null)
            {
                return;
            }

            if(this.d.Status.Value)
            {
                this.d.Set(false);
            }
            else
            {
                this.d.Set(true);
            }
       
        }

        public void Setup(DO d,string name)
        {
            this.d = d;
            this.txtId.Text = d.DoId.ToString();
            this.txtName.Text = d.Name;
            this.comboBoxValidValue.SelectedIndex = d.Prm.HeightLevelValid ? 0 : 1;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources("HeightLevel", this.HeightLevel);
            this.SaveKeyValueToResources("LowLevel", this.LowLevel);
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.HeightLevel = this.ReadKeyValueFromResources("HeightLevel");
            this.LowLevel = this.ReadKeyValueFromResources("LowLevel");
        }

        public void UpdateDO()
        {
            if(this.d == null)
            {
                return;
            }
            if (this.d.Status.Value)
            {
                this.pictureBox1.BackColor = Color.Green;
            }
            else
            {
                this.pictureBox1.BackColor = Color.Gray;
            }
        }

        private void comboBoxValidValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == 0)
            {
                d.Prm.HeightLevelValid = true;
            }
            else if(cb.SelectedIndex == 1)
            {
                d.Prm.HeightLevelValid = false;
            }
        }
    }
}
