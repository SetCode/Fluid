using Anda.Fluid.Drive;
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

namespace Anda.Fluid.Domain.Vision
{
    public partial class CameraForm : FormEx
    {
        public CameraForm()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterParent;
            this.Activated += CameraForm_Activated;
        }

        private void CameraForm_Activated(object sender, EventArgs e)
        {
            this.UpdateUI();
        }

        public void UpdateUI()
        {
            this.cameraControl1.UpdateUI();
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {          
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void CameraForm_Load(object sender, EventArgs e)
        {
            this.ReadLanguageResources();
        }
    }
}
