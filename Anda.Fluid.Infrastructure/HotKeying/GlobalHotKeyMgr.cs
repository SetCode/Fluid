using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.HotKeying
{
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8
    }

    public class GlobalHotKeyMgr
    {

        #region DllImport

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）          
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );

        #endregion


        private static List<GlobalHotKey> hotKeys = new List<GlobalHotKey>();

        public static event Action<string> RegisterHotKeyFailed;

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        /// <returns>0:注册成功，-1:热键被占用，-2:注册热键失败，-3:其他原因</returns>
        private static short RegisterKey(IntPtr hwnd, int hotKey_id, KeyModifiers keyModifiers, Keys key)
        {
            try
            {
                if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
                {
                    if (Marshal.GetLastWin32Error() == 1409)
                    {
                        RegisterHotKeyFailed?.Invoke("The hotKey is used");
                        return -1;
                    }
                    else
                    {
                        RegisterHotKeyFailed?.Invoke("Register hotKey failed");
                        return -2;
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                RegisterHotKeyFailed?.Invoke("Register hotKey failed");
                return -3;
            }
        }

        public static short RegKey(IntPtr hwnd, GlobalHotKey hotKey)
        {
            short rtn = RegisterKey(hwnd, hotKey.Id, hotKey.KeyModifiers, hotKey.KeyCode);
            if (rtn != 0)
            {
                return rtn; 
            }
            hotKeys.RemoveAll(x => x.Id == hotKey.Id);
            hotKeys.Add(hotKey);
            return 0;
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        public static short UnRegKey(IntPtr hwnd, int hotKey_id)
        {
            //注销Id号为hotKey_id的热键设定
            if (UnregisterHotKey(hwnd, hotKey_id))
            {
                hotKeys.RemoveAll(x => x.Id == hotKey_id);
                return 0;
            }
            return -1;
        }

        public static short UnRegKey(IntPtr hwnd, GlobalHotKey globalHotkey)
        {
            return UnRegKey(hwnd, globalHotkey.Id);
        }

        public static GlobalHotKey FindHotKey(int id)
        {
            foreach (var item in hotKeys)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static List<GlobalHotKey> FindAll()
        {
            return hotKeys;
        }

    }
}
