using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.HotKeying
{
    public class FormHotKeyMgr
    {
        private static List<FormHotKey> hotKeys = new List<FormHotKey>();

        public static void AddHotKey(int id, Form form, Keys key, bool isCtrl, bool isShift, bool isAlt, Action actionKeyDown, Action actionKeyUp)
        {
            FormHotKey hotKey = new FormHotKey(id, key)
            {
                IsCtrl = isCtrl,
                IsShift = isShift,
                IsAlt = isAlt,
                ActionKeyDown = actionKeyDown,
                ActionKeyUp = actionKeyUp
            };

            hotKeys.RemoveAll(x => x.Id == id);
            hotKeys.Add(hotKey);

            form.KeyPreview = true;

            form.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (!hotKey.Enabled)
                {
                    return;
                }

                if(IsHotKey(e, hotKey))
                {
                    hotKey.State.Update(true);
                }

                if (hotKey.State.IsRising)
                {
                    hotKey.ActionKeyDown?.Invoke();
                }
            };

            form.KeyUp += delegate (object sender, KeyEventArgs e)
            {
                if (!hotKey.Enabled)
                {
                    return;
                }

                if (IsNotHotKey(e, hotKey))
                {
                    hotKey.State.Update(false);
                }

                if (hotKey.State.IsFalling)
                {
                    hotKey.ActionKeyUp?.Invoke();
                }
            };

        }

        public static void RemoveHotKey(int id)
        {
            hotKeys.RemoveAll(x => x.Id == id);
        }

        private static bool IsHotKey(KeyEventArgs e, FormHotKey hotKey)
        {
            return e.KeyCode == hotKey.KeyCode
                && e.Control == hotKey.IsCtrl
                && e.Shift == hotKey.IsShift
                && e.Alt == hotKey.IsAlt;
        }

        private static bool IsNotHotKey(KeyEventArgs e, FormHotKey hotKey)
        {
            return e.KeyCode == hotKey.KeyCode
                || e.Control != hotKey.IsCtrl
                || e.Shift != hotKey.IsShift
                || e.Alt != hotKey.IsAlt;
        }


    }
}
