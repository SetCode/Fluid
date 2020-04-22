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
    public partial class DIControl : UserControlEx
    {
        private DI d = null;
        private string HeightLevel = "高电平";
        private string LowLevel = "低电平";
        public DIControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.txtId.ReadOnly = true;
            this.txtName.ReadOnly = true;
            this.comboBoxValidValue.Items.Add(HeightLevel);
            this.comboBoxValidValue.Items.Add(LowLevel);
            
        }

        public void Setup(DI d,string name)
        {
            this.d = d;
            this.txtId.Text = d.DiId.ToString();
            this.txtName.Text = d.Name;
            this.comboBoxValidValue.SelectedIndex = d.Prm.HeightLevelValid ? 0 : 1;
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources("HeightLevel",this.HeightLevel);
            this.SaveKeyValueToResources("LowLevel", this.LowLevel);
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = true, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.HeightLevel = this.ReadKeyValueFromResources("HeightLevel");
            this.LowLevel = this.ReadKeyValueFromResources("LowLevel");
        }

        public void UpdateDI()
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
            else if (cb.SelectedIndex == 1)
            {
                d.Prm.HeightLevelValid = false;
            }
        }
    }
}
