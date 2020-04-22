using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.App.CustomDataUI
{
    public partial class RTVDataControl : CustomControlBase
    {
        public RTVDataControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
        }

        public override void LoadParam(FluidProgram program)
        {
            this.txtMesPath.Text = program.RuntimeSettings.CustomParam.RTVParam.DataMesPathDir;
            this.txtLocalPath.Text = program.RuntimeSettings.CustomParam.RTVParam.DataLocalPathDir;
            this.ckbIsSaveCode.Checked = program.RuntimeSettings.CustomParam.RTVParam.IsSaveCode;
            this.txtPartInfo.Text = program.RuntimeSettings.CustomParam.RTVParam.Depart;
            this.txtComputerInfo.Text = program.RuntimeSettings.CustomParam.RTVParam.ComputerInfo;
            this.txtMachineInfo.Text = program.RuntimeSettings.CustomParam.RTVParam.MachineInfo;
            this.txtProductLineInfo.Text = program.RuntimeSettings.CustomParam.RTVParam.ProductLineInfo;
            this.txtOwk.Text = program.RuntimeSettings.CustomParam.RTVParam.OwkInfo;
            this.txtUserInfo.Text = program.RuntimeSettings.CustomParam.RTVParam.UserInfo;
        }

        public override void SetParam(FluidProgram program)
        {
            program.RuntimeSettings.CustomParam.RTVParam.DataMesPathDir = this.txtMesPath.Text;
            program.RuntimeSettings.CustomParam.RTVParam.DataLocalPathDir = this.txtLocalPath.Text;
            program.RuntimeSettings.CustomParam.RTVParam.IsSaveCode = this.ckbIsSaveCode.Checked;
            program.RuntimeSettings.CustomParam.RTVParam.Depart = this.txtPartInfo.Text;
            program.RuntimeSettings.CustomParam.RTVParam.ComputerInfo = this.txtComputerInfo.Text;
            program.RuntimeSettings.CustomParam.RTVParam.MachineInfo = this.txtMachineInfo.Text;
            program.RuntimeSettings.CustomParam.RTVParam.ProductLineInfo = this.txtProductLineInfo.Text;
            program.RuntimeSettings.CustomParam.RTVParam.OwkInfo = this.txtOwk.Text;
            program.RuntimeSettings.CustomParam.RTVParam.UserInfo = this.txtUserInfo.Text;
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
    }
}
