using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Vision.Barcode;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.App.EditMark
{
    public partial class InputBarcodeForm : FormEx
    {
        private BarcodePrm barcodePrm;
        private ConveyorBarcode conveyorBarcode;

        public InputBarcodeForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ReadLanguageResources();
        }

        public InputBarcodeForm(BarcodePrm barcodePrm)
            : this()
        {
            this.barcodePrm = barcodePrm;
        }

        public InputBarcodeForm(ConveyorBarcode conveyorBarcode)
            : this()
        {
            this.conveyorBarcode = conveyorBarcode;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.barcodePrm != null)
            {
                this.barcodePrm.Text = this.textBox1.Text;
            }
            if (this.conveyorBarcode != null)
            {
                this.conveyorBarcode.Text = this.textBox1.Text;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
