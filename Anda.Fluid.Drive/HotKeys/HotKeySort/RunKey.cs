using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.HotKeying;

namespace Anda.Fluid.Drive.HotKeys.HotKeySort
{
    public class RunKey : IHotKeySortable
    {
        public Action OnRun;
        public Action OnPause;
        public Action OnStep;
        public Action OnAbort;
        public Action OnStop;

        private HotKey runKeyRun = new HotKey(Keys.F7);
        private HotKey runKeyPause = new HotKey(Keys.F8);
        private HotKey runKeyStep = new HotKey(Keys.F9);
        private HotKey runKeyAbort = new HotKey(Keys.F10);
        private HotKey runKeyStop = new HotKey(Keys.F11);
        private List<HotKey> keyList;

        public bool Enable { get; set; }

        public List<HotKey> KeyList => this.keyList;

        public HotKeySortEnum SortName => HotKeySortEnum.RunKey;

        public RunKey()
        {
            this.Enable = true;
            this.keyList = new List<HotKey>();
            this.keyList.Add(this.runKeyRun);
            this.keyList.Add(this.runKeyPause);
            this.keyList.Add(this.runKeyStep);
            this.keyList.Add(this.runKeyAbort);
            this.keyList.Add(this.runKeyStop);
        }

        public void OnKeyDownEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;
            switch (e.KeyData)
            {
                case Keys.F7:
                    runKeyRun.State.Update(true);
                    if (runKeyRun.State.IsRising)
                    {
                        this.OnRun?.Invoke();
                    }
                    break;
                case Keys.F8:
                    runKeyPause.State.Update(true);
                    if (runKeyPause.State.IsRising)
                    {
                        this.OnPause?.Invoke();
                    }
                    break;
                case Keys.F9:
                    runKeyStep.State.Update(true);
                    if (runKeyStep.State.IsRising)
                    {
                        this.OnStep?.Invoke();
                    }
                    break;
                case Keys.F10:
                    runKeyAbort.State.Update(true);
                    if (runKeyAbort.State.IsRising)
                    {
                        this.OnAbort?.Invoke();
                    }
                    break;
                case Keys.F11:
                    runKeyStop.State.Update(true);
                    if (runKeyStop.State.IsRising)
                    {
                        this.OnStop?.Invoke();
                    }
                    break;
            }
        }

        public void OnKeyUpEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;
            switch (e.KeyData)
            {
                case Keys.F7:
                    runKeyRun.State.Update(false);
                    break;
                case Keys.F8:
                    runKeyPause.State.Update(false);
                    break;
                case Keys.F9:
                    runKeyStep.State.Update(false);
                    break;
                case Keys.F10:
                    runKeyAbort.State.Update(false);
                    break;
                case Keys.F11:
                    runKeyStop.State.Update(false);
                    break;
            }
        }
    }
}
