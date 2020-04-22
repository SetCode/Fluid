using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.HotKeying;

namespace Anda.Fluid.Drive.HotKeys.HotKeySort
{
    public class ValveKey : IHotKeySortable
    {
        private List<HotKey> keyList;
        private HotKey valveKey1 = new HotKey(Keys.F5);
        private HotKey valveKey2 = new HotKey(Keys.F6);
        public bool Enable { get; set; }

        public List<HotKey> KeyList => this.keyList;

        public HotKeySortEnum SortName => HotKeySortEnum.ValveKey;

        public ValveKey()
        {
            this.Enable = false;
            this.keyList = new List<HotKey>();
            this.keyList.Add(this.valveKey1);
            this.keyList.Add(this.valveKey2);
        }

        public void OnKeyDownEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;
            switch (e.KeyCode)
            {
                case Keys.F5:
                    valveKey1.State.Reverse();
                    if (valveKey1.State.Value)
                    {
                        Machine.Instance.Valve1.StartManualSpray(0);
                    }
                    else
                    {
                        Machine.Instance.Valve1.StopManualSpray();
                    }
                    break;
                case Keys.F6:
                    valveKey2.State.Reverse();
                    if (valveKey2.State.Value)
                    {
                        Machine.Instance.Valve2.StartManualSpray(0);
                    }
                    else
                    {
                        Machine.Instance.Valve2.StopManualSpray();
                    }
                    break;
            }
        }

        public void OnKeyUpEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;
            switch (e.KeyCode)
            {
                case Keys.F5:
                    valveKey1.State.Update(false);
                    if (valveKey1.State.IsFalling)
                    {
                        Machine.Instance.Valve1.StopManualSpray();
                    }
                    break;
                case Keys.F6:
                    valveKey2.State.Update(false);
                    if (valveKey2.State.IsFalling)
                    {
                        Machine.Instance.Valve2.StopManualSpray();
                    }
                    break;
            }
        }
    }
}
