using Anda.Fluid.Drive.Conveyor.LeadShine.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Forms.IOForms
{
    public partial class IOForm : Form
    {
        private List<LeadShineDiCtl> listDiCtls = new List<LeadShineDiCtl>();
        private List<LeadShineDoCtl> listDoCtls = new List<LeadShineDoCtl>();

        public IOForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.SetUp();
        }

        private void SetUp()
        {
            for (int i = 1; i < InPutMgr.Instance.Counts+1; i++)
            {
                LeadShineDiCtl diCtl = new LeadShineDiCtl();
                diCtl.SetUp(InPutMgr.Instance.FindBy((DiEnum)i));

                this.listDiCtls.Add(diCtl);

                if (i < 17)
                {
                    diCtl.Location = new Point(10, i * 30 - 15);
                }
                else
                {
                    diCtl.Location = new Point(200, (i-16) * 30 - 15);
                }
                this.grpDi.Controls.Add(diCtl);
            }

            for (int i = 1; i < OutPutMgr.Instance.Counts+1; i++)
            {
                LeadShineDoCtl doCtl = new LeadShineDoCtl();
                doCtl.SetUp(OutPutMgr.Instance.FindBy((DoEnum)i));

                this.listDoCtls.Add(doCtl);

                if (i < 17)
                {
                    doCtl.Location = new Point(10, i * 30 - 15);
                }
                else
                {
                    doCtl.Location = new Point(215, (i - 16) * 30 - 15);
                }
                this.grpDo.Controls.Add(doCtl);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            foreach (var item in this.listDiCtls)
            {
                item.UpDateDi();
            }

            foreach (var item in this.listDoCtls)
            {
                item.UpdateDo();
            }
        }
    }
}
