using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
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

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditLineWeightForm : FormEx
    {
        private LineCmdLine lineCmdLine;
        private List<LineCoordinate> lineCache;
        private double sumLength;
        private List<NumericUpDown> nudList = new List<NumericUpDown>();
        private List<ComboBox> cbxList = new List<ComboBox>();
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditLineWeightForm()
        {
            InitializeComponent();
        }

        public EditLineWeightForm(LineCmdLine lineCmdLine, List<LineCoordinate> lineCache)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.panel1.AutoScroll = true;

            this.nudWhole.Increment = 0.001M;
            this.nudWhole.DecimalPlaces = 3;
            this.nudWhole.Minimum = 0;
            this.nudWhole.Maximum = 10000000;
            this.nudEach.Increment = 0.001M;
            this.nudEach.DecimalPlaces = 3;
            this.nudEach.Minimum = 0;
            this.nudEach.Maximum = 10000000;

            this.rdoWhole.Checked = true;
            this.rdoEach.Checked = false;

            this.lineCmdLine = lineCmdLine;
            this.lineCache = lineCache;

            int index = 0;
            foreach (var item in this.lineCache)
            {
                this.sumLength += item.CalculateDistance();
                int y = 25 * index + 5;
                int x = 0;

                Label lblId = new Label();
                lblId.Text = string.Format("{0}:",index);
                lblId.Width = 20;
                lblId.Location = new Point(5, y);
                x = 25;
                panel1.Controls.Add(lblId);

                ComboBox cbxLineType = new ComboBox();
                cbxLineType.Location = new Point(x + 5, y);
                cbxLineType.Width = 90;
                x += 95;
                panel1.Controls.Add(cbxLineType);
                for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
                {
                    cbxLineType.Items.Add("Type " + (i + 1));
                }
                this.cbxList.Add(cbxLineType);

                Button btn = new Button();
                btn.Text = "edit";
                btn.Location = new Point(x + 5, y);
                btn.Width = 70;
                x += 80;
                btn.Click += this.btnEditLineStyle_Click;
                this.panel1.Controls.Add(btn);

                NumericUpDown nud = new NumericUpDown();
                nud.Increment = 0.001M;
                nud.DecimalPlaces = 3;
                nud.Minimum = 0;
                nud.Maximum = 10000000;
                nud.Location = new Point(x + 5, y);
                nud.Width = 100;
                x += 105;
                nud.Value = (decimal)item.Weight;
                this.nudList.Add(nud);
                panel1.Controls.Add(nud);

                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                lbl.Width = 1000;
                lbl.Text = string.Format("mg {0}", item);
                panel1.Controls.Add(lbl);
                index++;
            }

            for (int i = 0; i < FluidProgram.Current.ProgramSettings.LineParamList.Count; i++)
            {
                this.cbxAllLineStyle.Items.Add("Type " + (i + 1));
            }

            this.rdoWhole.Checked = this.lineCmdLine.IsWholeWtMode;
            this.rdoEach.Checked = !this.lineCmdLine.IsWholeWtMode;
            this.nudWhole.Value = (decimal)this.lineCmdLine.WholeWeight;
            this.nudEach.Value = (decimal)this.lineCmdLine.EachWeight;
            for (int i = 0; i < this.lineCache.Count; i++)
            {
                this.nudList[i].Value = (decimal)this.lineCache[i].Weight;
                this.cbxList[i].SelectedIndex = (int)this.lineCache[i].LineStyle;
            }
            this.cbxAllLineStyle.SelectedIndex = (int)this.lineCmdLine.LineStyle;
            this.cbxAllLineStyle.SelectedIndexChanged += CbxAllLineStyle_SelectedIndexChanged;
        }

        private void CbxAllLineStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in this.cbxList)
            {
                item.SelectedIndex = this.cbxAllLineStyle.SelectedIndex;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if(this.rdoWhole.Checked)
            {
                double wholeWt = (double)this.nudWhole.Value;
                for (int i = 0; i < this.lineCache.Count; i++)
                {
                    this.nudList[i].Value = (decimal)(this.lineCache[i].CalculateDistance() / this.sumLength * wholeWt);
                }
            }
            else
            {
                double eachWt = (double)this.nudEach.Value;
                for (int i = 0; i < this.lineCache.Count; i++)
                {
                    this.nudList[i].Value = (decimal)eachWt;
                }
            }
        }

        private void rBtnWhole_CheckedChanged(object sender, EventArgs e)
        {
            this.nudWhole.Enabled = this.rdoWhole.Checked;
            this.nudEach.Enabled = !this.rdoWhole.Checked;
        }

        private void rBtnEach_CheckedChanged(object sender, EventArgs e)
        {
            this.nudWhole.Enabled = !this.rdoEach.Checked;
            this.nudEach.Enabled = this.rdoEach.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.lineCmdLine.IsWholeWtMode = this.rdoWhole.Checked;
            this.lineCmdLine.WholeWeight = (double)this.nudWhole.Value;
            this.lineCmdLine.EachWeight = (double)this.nudEach.Value;
            this.lineCmdLine.LineStyle = (LineStyle)this.cbxAllLineStyle.SelectedIndex;
            for (int i = 0; i < this.lineCache.Count; i++)
            {
                this.lineCache[i].Weight = (double)this.nudList[i].Value;
                this.lineCache[i].LineStyle = (LineStyle)this.cbxList[i].SelectedIndex;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnEditLineStyle_Click(object sender, EventArgs e)
        {
            new EditLineParamsForm(FluidProgram.Current.ProgramSettings.LineParamList).ShowDialog();
        }
    }
}
