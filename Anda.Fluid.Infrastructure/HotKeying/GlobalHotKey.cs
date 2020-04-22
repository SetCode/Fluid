using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.HotKeying
{
    public class GlobalHotKey : HotKey
    {
        /// <summary>
        /// 窗口消息：热键
        /// </summary>
        public const int WM_HOTKEY = 0x312;
        /// <summary>
        /// 窗口消息：创建
        /// </summary>
        public const int WM_CREATE = 0x1;
        /// <summary>
        /// 窗口消息：销毁
        /// </summary>
        public const int WM_DESTROY = 0x2;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;

        public GlobalHotKey(int id, KeyModifiers keyModifiers, Keys keyCode)
            : base(id, keyCode)
        {
            this.KeyModifiers = keyModifiers;
        }

        public KeyModifiers KeyModifiers { get; set; }

    }
}
