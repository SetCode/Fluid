using Anda.Fluid.Domain.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO
{
    internal partial class TeachFormBase : JogFormBase
    {
        private bool isFill = false;
        private IClickable clickable;
        public TeachFormBase()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            clickable = this as IClickable;
            this.ReadLanguageResources();
            
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.clickable.DoPrev();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.clickable.DoNext();
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            this.clickable.DoTeach();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.clickable.DoDone();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            this.clickable.DoHelp();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clickable.DoCancel();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
           
        }

        private void change()
        {
            if (!this.isFill)
            {
                this.grpOperation.Visible = false;
                this.grpLighting.Visible = false;
                this.grpMotion.Visible = false;
                this.grpResultTest.Visible = false;
                this.positionVControl1.Visible = false;
                this.grpDisplay.Visible = false;
                this.grpDisplay.Controls.Remove(this.pnlDisplay);
                this.Controls.Add(this.pnlDisplay);
                this.pnlDisplay.Dock = DockStyle.Fill;
                this.isFill = true;
            }
            else
            {
                this.grpOperation.Visible = true;
                this.grpLighting.Visible = true;
                this.grpMotion.Visible = true;
                this.grpResultTest.Visible = true;
                this.positionVControl1.Visible = true;
                this.grpDisplay.Visible = true;
                this.Controls.Remove(this.pnlDisplay);
                this.grpDisplay.Controls.Add(this.pnlDisplay);
                this.pnlDisplay.Dock = DockStyle.Fill;
                this.isFill = false;
            }
        }

        private void TeachFormBase_Load(object sender, EventArgs e)
        {

        }

        private void TeachFormBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && e.Alt)
            {
                this.change();
                return;
            }
            base.OnKeyDown(e);
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            this.change();
        }
    }
}
