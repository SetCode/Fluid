using Anda.Fluid.Infrastructure.HotKeying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Drive.HotKeys.HotKeySort
{
    public interface IHotKeySortable
    {
        /// <summary>
        /// 热键类别名称
        /// </summary>
        HotKeySortEnum SortName { get;}

        /// <summary>
        /// 热键集合
        /// </summary>
        List<HotKey> KeyList { get; }

        /// <summary>
        /// 是否启用此类别热键
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// 当热键按下时发生
        /// </summary>
        /// <param name="e"></param>
        void OnKeyDownEvent(KeyEventArgs e);

        /// <summary>
        /// 当热键按下后抬起时发生
        /// </summary>
        /// <param name="e"></param>
        void OnKeyUpEvent(KeyEventArgs e);
    }

    public enum HotKeySortEnum
    {
        JogKey,
        ValveKey,
        RunKey,
        SelectKey,
    }
}
