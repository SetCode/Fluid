using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Main
{
    public partial class TabPanel : UserControl
    { 
        public TabPanel()
        {
            InitializeComponent();

            // Initialize content/index tab control
            TabControl = new VerticalTabControl();
            TabControl.Vertical = false;
            TabControl.Dock = DockStyle.Fill;
            TabControl.BackColor = Color.White;
            TabControl.BorderColor = Color.FromArgb(191, 191, 191);
            TabControl.Font = new Font("Verdana", 8, FontStyle.Bold);
            // Add tab control into the panel
            this.Controls.Add(TabControl);
        }

        public VerticalTabControl TabControl { get; private set; }

        public void AddPages(params string[] pages)
        {
            foreach (var item in pages)
            {
                // Add Content page
                TabControl.TabPages.Add(item);
            }
        }

        public VerticalTabPage GetPage(int index)
        {
            try
            {
                return TabControl.TabPages[index];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetPagesCount()
        {
            try
            {
                return TabControl.TabPages.Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
