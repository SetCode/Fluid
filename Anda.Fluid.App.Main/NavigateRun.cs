using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.App.Main.EventBroker;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.HotKeys.HotKeySort;
using Anda.Fluid.Drive.HotKeys;

namespace Anda.Fluid.App.Main
{
    public partial class NavigateRun : UserControl, IMsgSender, IMsgReceiver
    {
        public NavigateRun()
        {
            InitializeComponent();
            Executor.Instance.OnStateChanged += Instance_OnStateChanged;
            Executor.Instance.OnWorkStateChanged += Instance_OnWorkStateChanged;
            UserControlEx UserControl = new UserControlEx();
            foreach (Control controls in this.Controls)
            {
                controls.MouseMove += UserControl.ReadDisplayTip;
                controls.MouseLeave += UserControl.DisopTip;
            }
        }

        private void Instance_OnWorkStateChanged(Executor.WorkState obj)
        {
            if(!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (obj)
                {
                    case Executor.WorkState.Idle:
                        btnStart.Enabled = true;
                        btnStep.Enabled = true;
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnAbort.Enabled = false;
                        break;
                    case Executor.WorkState.Preparing:
                        btnStart.Enabled = false;
                        btnStep.Enabled = false;
                        btnPause.Enabled = false;
                        btnStop.Enabled = true;
                        btnAbort.Enabled = true;
                        break;
                    case Executor.WorkState.WaitingForBoard:
                        btnStart.Enabled = false;
                        btnStep.Enabled = false;
                        btnPause.Enabled = false;
                        btnStop.Enabled = true;
                        btnAbort.Enabled = true;
                        break;
                    case Executor.WorkState.Programing:
                        //to do
                        break;
                    case Executor.WorkState.Stopping:
                        btnStop.Enabled = false;
                        break;
                }
            }));
        }

        private void Instance_OnStateChanged(Executor.ProgramOuterState oldState, Executor.ProgramOuterState newState)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            // 根据程序执行状态的变化更新按钮状态
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (newState)
                {
                    case Executor.ProgramOuterState.IDLE:
                        HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, true);
                        btnStart.Enabled = true;
                        btnStep.Enabled = true;
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnAbort.Enabled = false;
                        btnPause.Image = Properties.Resources.Pause_30px;
                        break;
                    case Executor.ProgramOuterState.RUNNING:
                        HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, false);
                        btnStart.Enabled = false;
                        btnStep.Enabled = false;
                        btnPause.Enabled = true;
                        btnStop.Enabled = true;
                        btnAbort.Enabled = true;
                        btnPause.Image = Properties.Resources.Pause_30px;
                        break;
                    case Executor.ProgramOuterState.PAUSING:
                        HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, false);
                        btnStart.Enabled = false;
                        btnStep.Enabled = false;
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnAbort.Enabled = false;
                        break;
                    case Executor.ProgramOuterState.PAUSED:
                        HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, false);
                        btnStart.Enabled = false;
                        btnStep.Enabled = true;
                        btnPause.Enabled = true;
                        btnStop.Enabled = false;
                        btnAbort.Enabled = true;
                        btnPause.Image = Properties.Resources.Resume_Button_30px;
                        break;
                    case Executor.ProgramOuterState.ABORTING:
                        HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, false);
                        btnStart.Enabled = false;
                        btnStep.Enabled = false;
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnAbort.Enabled = false;
                        break;
                    case Executor.ProgramOuterState.ABORTED:
                        HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, true);
                        btnStart.Enabled = true;
                        btnStep.Enabled = true;
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                        btnAbort.Enabled = false;
                        btnPause.Image = Properties.Resources.Pause_30px;
                        break;
                }
            }));
        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            //如果是双轨双程序，则要进行检查
            if(Machine.Instance.Setting.ConveyorSelect== ConveyorSelection.双轨
                && Machine.Instance.Setting.DoubleProgram)
            {
                if(Executor.Instance.Conveyor1Program == null || Executor.Instance.Conveyor2Program == null
                    || !Executor.Instance.Conveyor1Program.Parse().IsOk
                    || !Executor.Instance.Conveyor1Program.Parse().IsOk)
                {
                    MessageBox.Show("无法正确加载双轨程序，请检查后重试！");
                    return;
                }
            }

            RunBroker.Instance.StartWork();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            Executor.Instance.SingleStep();
        }

        public void btnPause_Click(object sender, EventArgs e)
        {
            Executor.Instance.PauseResume();
        }

        public void btnAbort_Click(object sender, EventArgs e)
        {
            //if (this.InvokeRequired)
            //{
            //    DialogResult res = DialogResult.No;
            //    this.BeginInvoke(new Action(() =>
            //    {
            //        res = new AbortOrRunForm().ShowDialog();
            //        //DialogResult.Yes  Abort 后继续 完成当前板
            //        if (res == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            Executor.Instance.PauseResume();
            //            return;
            //        }
            //        else
            //        {
            //            Executor.Instance.Abort();
            //        }
            //    }));

            //}
            //else
            //{
            //    Executor.Instance.Abort();
            //}

            Executor.Instance.Abort();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Executor.Instance.Stop();
        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if(msgName == MsgType.IDLE)
            {
                btnStart.Enabled = true;
                btnStep.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
                btnAbort.Enabled = false;
                naviBtnInitAll1.Enabled = true;
                naviBtnInitItems1.Enabled = true;
            }
            else if(msgName == MsgType.BUSY)
            {
                btnStart.Enabled = false;
                btnStep.Enabled = false;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
                btnAbort.Enabled = false;
                naviBtnInitAll1.Enabled = false;
                naviBtnInitItems1.Enabled = false;
            }
        }
    }
}
