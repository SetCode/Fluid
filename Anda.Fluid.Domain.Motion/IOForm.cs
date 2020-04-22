using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.Motion
{
    public partial class IOForm : FormEx
    {
        private Timer timer;
        private List<DIControl> lstDICtl = new List<DIControl>();
        private List<DOControl> lstDOCtl = new List<DOControl>();

        public IOForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = this.Size;
            this.FormClosing += IOForm_FormClosing;
        
            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
            this.ReadLanguageResources();
            this.Setup();
        }

        private void IOForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //IOPrmMgr.Instance.Save();
            Machine.Instance.IOSetupable.SaveIOPrm();
        }
        public override void SaveLanguageResources(bool skipButton = true, bool skipRadioButton = true, bool skipCheckBox = true, bool skipLabel = true)
        {
            foreach (var item in DIMgr.Instance.FindAll())
            {
                this.SaveKeyValueToResources(item.Name, item.Name);
            }
            foreach (var item in DOMgr.Instance.FindAll())
            {
                this.SaveKeyValueToResources(item.Name, item.Name);
            }
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }
        private IOForm Setup()
        {
            int i = 0;
            int orgDIx = this.gbxDI.Location.X+10;
            int orgDIy = this.gbxDI.Location.Y+15;
            int deltaDIX = 295;
            int deltaDIY = 35;
            foreach (var item in DIMgr.Instance.FindAll())
            {
                string name = this.ReadKeyValueFromResources(item.Name);
                DIControl c = new DIControl();            
                c.Setup(item, name);
                //int x = 10;
                //int y = i * 30 + 15;
                int x = 0;
                int y = i* deltaDIY;
                if (i > 13)
                {                    
                    //x = 226;
                    //y = (i - 14) * 30 + 15;
                     x = deltaDIX;
                     y = (i - 14) * deltaDIY;
                    //this.gbxDI.Size = new Size(this.gbxDI.Size.Width + 236, this.gbxDI.Size.Height);
                    this.gbxDI.Size = new Size(orgDIx + x + deltaDIX + 10, this.gbxDI.Size.Height);
                }
                c.Location = new Point(orgDIx + x, orgDIy + y);
                this.lstDICtl.Add(c);
                this.gbxDI.Controls.Add(c);
                DIControl di = this.lstDICtl[0];
                i++;
            }
            Size s = this.gbxDI.Size;
            if (DIMgr.Instance.Count > 13)
            {
                //this.gbxDO.Location = new Point(this.gbxDO.Location.X + 276, this.gbxDO.Location.Y);
                //this.Size = new Size(this.Size.Width + 256, this.Size.Height);
                this.gbxDO.Location = new Point(this.gbxDI.Location.X + this.gbxDI.Size.Width + 10, this.gbxDO.Location.Y);
                //this.Size = new Size(this.Size.Width + 670, this.Size.Height);
            }
            i = 0;
            int orgDox = 10;
            int orgDoy = 15;
            int deltaDoX = 335;
            int deltaDoY = 35;
            foreach (var item in DOMgr.Instance.FindAll())
            {
                string name = this.ReadKeyValueFromResources(item.Name);
                DOControl c = new DOControl();
                c.Setup(item, name);
                int x = 0;
                int y = i * deltaDoY;
                if (i > 13)
                {
                    x = deltaDoX;
                    y = (i - 14) * deltaDoY;
                    this.gbxDO.Size = new Size(x + deltaDoX + 10, this.gbxDO.Size.Height);
                    this.Size = new Size(this.gbxDO.Location.X + this.gbxDO.Size.Width + 10, this.Size.Height);
                }
                c.Location = new Point(orgDox+x, orgDoy+y);
                this.lstDOCtl.Add(c);
                this.gbxDO.Controls.Add(c);
                i++;
            }
            if (!RoleMgr.Instance.CurrentRole.OtherFormAccess.CanUseOutputButton)
            {
                this.gbxDO.Enabled = false;
                this.gbxDI.Enabled = false;
            }

            return this;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var item in this.lstDICtl)
            {
                item.UpdateDI();
            }

            foreach (var item in this.lstDOCtl)
            {
                item.UpdateDO();
            }
        }

    }
}
