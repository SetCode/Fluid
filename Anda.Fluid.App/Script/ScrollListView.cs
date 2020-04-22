using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Anda.Fluid.App.Script
{
    public class ScrollListView : ListView
    {
        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;
        public event EventHandler HScroll;
        public event EventHandler VScroll;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HSCROLL)
            {
                HScroll?.Invoke(this, new EventArgs());
            }
            //else if (m.Msg == WM_VSCROLL)
            //{
            //    VScroll?.Invoke(this, new EventArgs());
            //}
            // 同 20 一样的还有：15 4140 49892
            else if (m.Msg == 15) // 鼠标滚动，上下键 控制屏幕滚动
            {
                VScroll?.Invoke(this, new EventArgs());
            }
            base.WndProc(ref m);
        }
    }
}
