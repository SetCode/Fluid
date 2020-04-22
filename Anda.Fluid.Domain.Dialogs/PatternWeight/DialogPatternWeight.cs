using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogPatternWeight : Form
    {
        private Valve valve;
        public DialogPatternWeight()
        {
            InitializeComponent();
            
        }

        private void btnDotSave_Click(object sender, EventArgs e)
        {
            if (this.fdgPath.ShowDialog() == DialogResult.OK)
            {
                string path = fdgPath.SelectedPath;//完整的路径D:\\mes
                MesSettings.FilePathDotWeightUser = path + string.Format("\\DotWeight-{0}.csv",
                DateTime.Today.ToString("yyyy-MM-dd"));
                this.txtDotPath.Text = path + string.Format("\\DotWeight-{0}.csv",
                DateTime.Today.ToString("yyyy-MM-dd")); 
                this.txtDotPath.Focus();
                this.txtDotPath.Select(this.txtDotPath.TextLength, 0);
                this.txtDotPath.ScrollToCaret();
            }


        }

        private void btnMatrixSave_Click(object sender, EventArgs e)
        {           
            if (this.fdgPath.ShowDialog() == DialogResult.OK)
            {
                string path = fdgPath.SelectedPath;//完整的路径D:\\mes
                MesSettings.FilePathMatrixWeightUser = path + string.Format("\\MatrixWeight-{0}.csv",
                DateTime.Today.ToString("yyyy-MM-dd"));
                this.txtMatrixPath.Text = path + string.Format("\\MatrixWeight-{0}.csv",
                DateTime.Today.ToString("yyyy-MM-dd"));              
                this.txtMatrixPath.Focus();
                this.txtMatrixPath.Select(this.txtMatrixPath.TextLength, 0);
                this.txtMatrixPath.ScrollToCaret();
            }
        }

        private void btnWeight_Click(object sender, EventArgs e)
        {
            

        }
        


        private void cmbValves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Machine.Instance.Setting.ValveSelect==ValveSelection.单阀)
            {
                valve = Machine.Instance.Valve1;
                return;
            }   
            if (this.cmbValves.SelectedIndex>0)
            {
                if ((ValveType)this.cmbValves.SelectedItem == ValveType.Valve1)
                {
                    valve = Machine.Instance.Valve1;
                }
                else
                {
                    valve = Machine.Instance.Valve2;
                }
            }
        }

        private void DialogMESGlue_Load(object sender, EventArgs e)
        {
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.cmbValves.Items.Add(ValveType.Valve1);
                valve = Machine.Instance.Valve1;
            }
            else if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                this.cmbValves.Items.Add(ValveType.Valve1);
                this.cmbValves.Items.Add(ValveType.Valve2);
                valve = Machine.Instance.Valve1;
            }
        }

        private void getRunnableMoudleByPattern()
        {

        }
    }
}
