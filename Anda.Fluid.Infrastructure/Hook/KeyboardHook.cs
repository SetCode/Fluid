using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Hook
{
    public class KeyboardHook
    {
        private int hHook;

        private Win32Api.HookProc KeyboardHookDelegate;

        public event KeyEventHandler OnKeyDownEvent;

        public event KeyEventHandler OnKeyUpEvent;

        public event KeyPressEventHandler OnKeyPressEvent;

        public KeyboardHook() { }

        public void SetupHook()
        {
            KeyboardHookDelegate = new Win32Api.HookProc(KeyboardHookProc);
            Process cProcess = Process.GetCurrentProcess();
            ProcessModule cModule = cProcess.MainModule;
            var mh = Win32Api.GetModuleHandle(cModule.ModuleName);
            hHook = Win32Api.SetWindowsHookEx(Win32Api.WH_KEYBOARD_LL, KeyboardHookDelegate, mh, 0);
        }

        public void UnHook()
        {
            Win32Api.UnhookWindowsHookEx(hHook);
        }

        /// <summary>
        /// 存放被按下的控制键，用来生成具体的键
        /// </summary>
        private List<Keys> preKeysList = new List<Keys>();

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //如果该消息被丢弃（nCode<0）或者没有事件绑定处理程序则不会触发事件
            if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
            {
                Win32Api.KeyboardHookStruct KeyDataFromHook = (Win32Api.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.KeyboardHookStruct));
                Keys keyData = (Keys)KeyDataFromHook.vkCode;

                //按下控制键
                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                {
                    if (preKeysList.IndexOf(keyData) == -1)
                    {
                        preKeysList.Add(keyData);
                    }
                }

                //WM_KEYDOWN和WM_SYSKEYDOWN消息，将会引发OnKeyDownEvent事件
                if (OnKeyDownEvent != null && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                {
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                    OnKeyDownEvent(this, e);
                }

                ////WM_KEYDOWN消息将引发OnKeyPressEvent 
                //if (OnKeyPressEvent != null && wParam == Win32Api.WM_KEYDOWN)
                //{
                //    byte[] keyState = new byte[256];
                //    Win32Api.GetKeyboardState(keyState);
                //    byte[] inBuffer = new byte[2];
                //    if (Win32Api.ToAscii(KeyDataFromHook.vkCode, KeyDataFromHook.scanCode, keyState, inBuffer, KeyDataFromHook.flags) == 1)
                //    {
                //        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                //        OnKeyPressEvent(this, e);
                //    }
                //}

                //松开控制键
                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                {
                    for (int i = preKeysList.Count - 1; i >= 0; i--)
                    {
                        if (preKeysList[i] == keyData)
                        {
                            preKeysList.RemoveAt(i);
                        }
                    }
                }

                //WM_KEYUP和WM_SYSKEYUP消息，将引发OnKeyUpEvent事件           
                if (OnKeyUpEvent != null && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                {
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    OnKeyUpEvent(this, e);
                }
            }
            return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 根据已经按下的控制键生成key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Keys GetDownKeys(Keys key)
        {
            Keys rtnKey = Keys.None;
            foreach (Keys k in preKeysList)
            {
                if (k == Keys.LControlKey || k == Keys.RControlKey)
                {
                    rtnKey = rtnKey | Keys.Control;
                }
                if (k == Keys.LMenu || k == Keys.RMenu)
                {
                    rtnKey = rtnKey | Keys.Alt;
                }
                if (k == Keys.LShiftKey || k == Keys.RShiftKey)
                {
                    rtnKey = rtnKey | Keys.Shift;
                }
            }
            return rtnKey | key;
        }

        private Boolean IsCtrlAltShiftKeys(Keys key)
        {
            if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.LMenu || key == Keys.RMenu || key == Keys.LShiftKey || key == Keys.RShiftKey)
            {
                return true;
            }
            return false;
        }

    }
}
