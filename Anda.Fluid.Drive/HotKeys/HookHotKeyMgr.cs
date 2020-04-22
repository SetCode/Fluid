using Anda.Fluid.Drive;
using Anda.Fluid.Drive.HotKeys.HotKeySort;
using Anda.Fluid.Infrastructure.Hook;
using Anda.Fluid.Infrastructure.HotKeying;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*****************************************************************************
 * 当要拓展热键时，请遵循如下规则：
 * 1.在HotKeySort文件夹下添加新的热键分类，并继承IHotKeySortable接口。
 * 2.在HotKeySortEnum中添加新热键分类的名称。
 * 3.在HookHotKeyMgr的构造函数中,为hotKeySorts字段添加新的热键分类对象。
 * ***************************************************************************/


namespace Anda.Fluid.Drive.HotKeys
{
    public class HookHotKeyMgr
    {
        private readonly static HookHotKeyMgr instance = new HookHotKeyMgr();
        private HookHotKeyMgr()
        {
            this.hotKeySorts.Add(new JogKey());
            this.hotKeySorts.Add(new ValveKey());
            this.hotKeySorts.Add(new RunKey());
            this.hotKeySorts.Add(new SelectKey());
        }
        public static HookHotKeyMgr Instance => instance;

        private KeyboardHook keyboardHook;

        private List<IHotKeySortable> hotKeySorts = new List<IHotKeySortable>();

        public void Setup()
        {
            keyboardHook = new KeyboardHook();
            keyboardHook.SetupHook();
            keyboardHook.OnKeyDownEvent += KeyboardHook_OnKeyDownEvent;
            keyboardHook.OnKeyUpEvent += KeyboardHook_OnKeyUpEvent;
        }

        public void Unload()
        {
            keyboardHook.UnHook();
        }

        /// <summary>
        /// 设置某个类别的热键是否启动
        /// </summary>
        /// <param name="hotKeySort"></param>
        /// <param name="enable"></param>
        public void SetEnable(HotKeySortEnum hotKeySort,bool enable)
        {
            foreach (var item in this.hotKeySorts)
            {
                if (item.SortName == hotKeySort)
                    item.Enable = enable;
            }
        }

        /// <summary>
        /// 获得Runkey热键，没有的话返回null
        /// </summary>
        /// <returns></returns>
        public RunKey GetRunKey()
        {
            foreach (var item in this.hotKeySorts)
            {
                if (item is RunKey)
                    return item as RunKey;
            }
            return null;
        }

        /// <summary>
        /// 获得SelectKey热键，没有的话返回null
        /// </summary>
        /// <returns></returns>
        public SelectKey GetSelectKey()
        {
            foreach (var item in this.hotKeySorts)
            {
                if (item is SelectKey)
                    return item as SelectKey;
            }
            return null;
        }

        private void KeyboardHook_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            foreach (var item in this.hotKeySorts)
            {
                item.OnKeyDownEvent(e);
            }
        }

        private void KeyboardHook_OnKeyUpEvent(object sender, KeyEventArgs e)
        {
            foreach (var item in this.hotKeySorts)
            {
                item.OnKeyUpEvent(e);
            }
        }

    }

}
