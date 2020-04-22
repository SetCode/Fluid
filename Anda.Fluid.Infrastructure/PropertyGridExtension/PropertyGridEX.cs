using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.PropertyGridExtension
{
    
    public class PropertyGridEx : PropertyGrid
    {
        public int Width=200;
        protected override void OnLayout(LayoutEventArgs e)
        {            
            Control propertyGridView = this.Controls[2];
            Type propertyGridViewType = propertyGridView.GetType();

            propertyGridViewType.InvokeMember("MoveSplitterTo",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                null, propertyGridView, new object[] { Width });

            base.OnLayout(e);
        }
    }
}
