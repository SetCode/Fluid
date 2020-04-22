using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
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

namespace Anda.Fluid.Domain.SoftFunction.PatternWeight
{
    public partial class DialogPatternWeight : FormEx
    {
        private Valve valve;
        private PatternWeight pWeight = new PatternWeight();
        public DialogPatternWeight()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }


        private void btnDotSave_Click(object sender, EventArgs e)
        {
            if (this.fdgPath.ShowDialog() == DialogResult.OK)
            {
                string path = fdgPath.SelectedPath;//完整的路径D:\\mes
                PatternWeightSettings.FileDirUser = path;
              
                this.txtDotPath.Text = path;
                this.txtDotPath.Focus();
                this.txtDotPath.Select(this.txtDotPath.TextLength, 0);
                this.txtDotPath.ScrollToCaret();
            }
        }
        

        private void btnWeight_Click(object sender, EventArgs e)
        {
            this.pWeight.Valve = this.valve;
            this.pWeight.DoPatternWeight();
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
            this.lstPattern.Items.Clear();
            if (FluidProgram.Current == null)
                return;
            this.lstPattern.Items.Add(FluidProgram.Current.Workpiece.Name);
            foreach (Pattern item in FluidProgram.Current.Patterns)
            {
                this.lstPattern.Items.Add(item.Name);
            }
            
        }

        /// <summary>
        /// 选择要称重的pattern，找到对应的RunnableMoudle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstPattern.SelectedIndex>-1)
            {
                pWeight.PatternName = this.lstPattern.SelectedItem.ToString();
            }
            pWeight.GetRunnableMoudleByPattern();
        }
    }
}
