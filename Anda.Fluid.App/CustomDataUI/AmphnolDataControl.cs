using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.App.CustomDataUI
{
    public partial class AmphnolDataControl : CustomControlBase
    {
        public AmphnolDataControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
        }

        public override void SetParam(FluidProgram program)
        {
            program.RuntimeSettings.CustomParam.AmphnolParam.DataMesPathDir = this.txtMesPath.Text;
            program.RuntimeSettings.CustomParam.AmphnolParam.DataLocalPathDir = this.txtLocalPath.Text;
            program.RuntimeSettings.CustomParam.AmphnolParam.DataEmarkPathDir = this.txtEmarkPath.Text;
            program.RuntimeSettings.CustomParam.AmphnolParam.EmarkUserName = this.txtUserName.Text;
            program.RuntimeSettings.CustomParam.AmphnolParam.EmarkPassword = this.txtPassword.Text;
        }

        public override void LoadParam(FluidProgram program)
        {
            this.txtMesPath.Text = program.RuntimeSettings.CustomParam?.AmphnolParam.DataMesPathDir;
            this.txtLocalPath.Text = program.RuntimeSettings.CustomParam?.AmphnolParam.DataLocalPathDir;
            this.txtEmarkPath.Text = program.RuntimeSettings.CustomParam?.AmphnolParam.DataEmarkPathDir;
            this.txtUserName.Text = program.RuntimeSettings.CustomParam?.AmphnolParam.EmarkUserName;
            this.txtPassword.Text = program.RuntimeSettings.CustomParam?.AmphnolParam.EmarkPassword;
        }

        private void btnMesPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDlg = new FolderBrowserDialog();

            if (fDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtMesPath.Text = fDlg.SelectedPath;
                this.txtMesPath.Focus();
                this.txtMesPath.Select(this.txtMesPath.TextLength, 0);
                this.txtMesPath.ScrollToCaret();
            }
        }

        private void btnLocalPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDlg = new FolderBrowserDialog();

            if (fDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtLocalPath.Text = fDlg.SelectedPath;
                this.txtLocalPath.Focus();
                this.txtLocalPath.Select(this.txtLocalPath.TextLength, 0);
                this.txtLocalPath.ScrollToCaret();
            }
        }

        private void btnEmarkPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDlg = new FolderBrowserDialog();

            if (fDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtEmarkPath.Text = fDlg.SelectedPath;
                this.txtEmarkPath.Focus();
                this.txtEmarkPath.Select(this.txtEmarkPath.TextLength, 0);
                this.txtEmarkPath.ScrollToCaret();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }
    }
}
