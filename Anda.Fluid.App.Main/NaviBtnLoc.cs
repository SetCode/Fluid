using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.Locations;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Infrastructure.International;
namespace Anda.Fluid.App.Main
{
    public partial class NaviBtnLoc : UserControl
    {
        private ContextMenuStrip cms;

        public NaviBtnLoc()
        {
            InitializeComponent();

            this.cms = new ContextMenuStrip();
            if (Machine.Instance.Robot != null)
            {
                foreach (var item in Machine.Instance.Robot.SystemLocations.All)
                {
                    this.cms.Items.Add(item.Key);
                }
            }
            this.cms.ItemClicked += Cms_ItemClicked;
            this.btnInit.Click += BtnInit_Click;
            UserControlEx UserControlEx = new UserControlEx();
            this.btnInit.MouseMove += UserControlEx.ReadDisplayTip;
            this.btnInit.MouseLeave += UserControlEx.DisopTip;
      
        }

        private void BtnInit_Click(object sender, EventArgs e)
        {
            this.cms.Show(this, 0, 0);
        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            Location loc = LocationMgr.Instance.FindBy(itemText);
            if(loc == null)
            {
                return;
            }
            Machine.Instance.Robot.MoveToLoc(loc);
        }
    }
}
