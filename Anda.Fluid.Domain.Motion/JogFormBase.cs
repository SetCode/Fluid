using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.HotKeying;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.Domain.Motion
{
    public class JogFormBase : FormEx
    {
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;

        //private HotKey hotKeyXp;
        //private HotKey hotKeyXn;
        //private HotKey hotKeyYp;
        //private HotKey hotKeyYn;
        //private HotKey hotKeyZp;
        //private HotKey hotKeyZn;
        //private HotKey hotKeyAp;
        //private HotKey hotKeyAn;
        //private HotKey hotKeyBp;
        //private HotKey hotKeyBn;


        public JogFormBase()
        {
            //this.KeyPreview = true;
            //this.hotKeyXp = new HotKey(0, Keys.Right);
            //this.hotKeyXn = new HotKey(1, Keys.Left);
            //this.hotKeyYp = new HotKey(2, Keys.Up);
            //this.hotKeyYn = new HotKey(3, Keys.Down);
            //this.hotKeyZp = new HotKey(4, Keys.Home);
            //this.hotKeyZn = new HotKey(5, Keys.End);
            //this.hotKeyAp = new HotKey(6, Keys.Right);
            //this.hotKeyAn = new HotKey(7, Keys.Left);
            //this.hotKeyBp = new HotKey(8, Keys.Up);
            //this.hotKeyBn = new HotKey(9, Keys.Down);

            this.Load += JogFormBase_Load;
            this.FormClosed += JogFormBase_FormClosed;
        }

        private void JogFormBase_Load(object sender, EventArgs e)
        {
            HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, true);
        }

        private void JogFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.Up || keyData == Keys.Down
                    || keyData == Keys.Left || keyData == Keys.Right
                    || keyData == Keys.Home || keyData == Keys.End
                    || keyData == (Keys.Up | Keys.Control) || keyData == (Keys.Down | Keys.Control)
                    || keyData == (Keys.Left | Keys.Control) || keyData == (Keys.Right | Keys.Control)
                    || keyData == (Keys.Home | Keys.Control) || keyData == (Keys.End | Keys.Control))
                {
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (msg.Msg == WM_KEYDOWN)
        //    {
        //        if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Home || keyData == Keys.End)
        //        {
        //            if (Machine.Instance.IsBusy)
        //            {
        //                return true;
        //            }
        //        }
        //        switch (keyData)
        //        {
        //            #region Y轴
        //            case Keys.Up:
        //                this.hotKeyYp.StsKeyDown.Update(true);
        //                if (this.hotKeyYp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, true, false);
        //                    Debug.WriteLine("y+");
        //                }
        //                break;
        //            case Keys.Up | Keys.Control:
        //                this.hotKeyYp.StsKeyDown.Update(true);
        //                if (this.hotKeyYp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, true, true);
        //                    Debug.WriteLine("y++");
        //                }
        //                break;
        //            case Keys.Down:
        //                this.hotKeyYn.StsKeyDown.Update(true);
        //                if (this.hotKeyYn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, false, false);
        //                    Debug.WriteLine("y-");
        //                }
        //                break;
        //            case Keys.Down | Keys.Control:
        //                this.hotKeyYn.StsKeyDown.Update(true);
        //                if (this.hotKeyYn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, false, true);
        //                    Debug.WriteLine("y--");
        //                }
        //                break;
        //            #endregion

        //            #region X轴
        //            case Keys.Left:
        //                this.hotKeyXn.StsKeyDown.Update(true);
        //                if (this.hotKeyXn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, false, false);
        //                    Debug.WriteLine("x-");
        //                }
        //                break;
        //            case Keys.Left | Keys.Control:
        //                this.hotKeyXn.StsKeyDown.Update(true);
        //                if (this.hotKeyXn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, false, true);
        //                    Debug.WriteLine("x--");
        //                }
        //                break;
        //            case Keys.Right:
        //                this.hotKeyXp.StsKeyDown.Update(true);
        //                if (this.hotKeyXp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, true, false);
        //                    Debug.WriteLine("x+");
        //                }
        //                break;
        //            case Keys.Right | Keys.Control:
        //                this.hotKeyXp.StsKeyDown.Update(true);
        //                if (this.hotKeyXp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, true, true);
        //                    Debug.WriteLine("x++");
        //                }
        //                break;
        //            #endregion

        //            #region Z轴
        //            case Keys.Home:
        //                this.hotKeyZp.StsKeyDown.Update(true);
        //                if (this.hotKeyZp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, true, false);
        //                    Debug.WriteLine("z+");
        //                }
        //                break;
        //            case Keys.Home | Keys.Control:
        //                this.hotKeyZp.StsKeyDown.Update(true);
        //                if (this.hotKeyZp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, true, true);
        //                    Debug.WriteLine("z++");
        //                }
        //                break;
        //            case Keys.End:
        //                this.hotKeyZn.StsKeyDown.Update(true);
        //                if (this.hotKeyZn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, false, false);
        //                    Debug.WriteLine("z-");
        //                }
        //                break;
        //            case Keys.End | Keys.Control:
        //                this.hotKeyZn.StsKeyDown.Update(true);
        //                if (this.hotKeyZn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, false, true);
        //                    Debug.WriteLine("z--");
        //                }
        //                break;
        //            #endregion

        //            #region A轴
        //            case Keys.Left | Keys.Control | Keys.Shift:
        //                this.hotKeyAn.StsKeyDown.Update(true);
        //                if (this.hotKeyAn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisA, false, false);
        //                    Debug.WriteLine("A-");
        //                }
        //                break;
        //            case Keys.Right | Keys.Control | Keys.Shift:
        //                this.hotKeyAp.StsKeyDown.Update(true);
        //                if (this.hotKeyAp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisA, true, false);
        //                    Debug.WriteLine("A+");
        //                }
        //                break;
        //            #endregion

        //            #region B轴
        //            case Keys.Down | Keys.Control | Keys.Shift:
        //                this.hotKeyBn.StsKeyDown.Update(true);
        //                if (this.hotKeyBn.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisB, false, false);
        //                    Debug.WriteLine("B-");
        //                }
        //                break;
        //            case Keys.Up | Keys.Control | Keys.Shift:
        //                this.hotKeyBp.StsKeyDown.Update(true);
        //                if (this.hotKeyBp.StsKeyDown.IsRising)
        //                {
        //                    Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisB, true, false);
        //                    Debug.WriteLine("B+");
        //                }
        //                break;
        //            #endregion
        //            default:
        //                return base.ProcessCmdKey(ref msg, keyData);
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return base.ProcessCmdKey(ref msg, keyData);
        //    }
        //}

        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
        //    {
        //        if (Machine.Instance.IsBusy)
        //        {
        //            base.OnKeyUp(e);
        //            return;
        //        }
        //    }

        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up:
        //            this.hotKeyYp.StsKeyDown.Update(false);
        //            if(this.hotKeyYp.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisY);
        //            }
        //            this.hotKeyBp.StsKeyDown.Update(false);
        //            if (this.hotKeyBp.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisB);
        //            }
        //            break;
        //        case Keys.Down:
        //            this.hotKeyYn.StsKeyDown.Update(false);
        //            if (this.hotKeyYn.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisY);
        //            }
        //            this.hotKeyBn.StsKeyDown.Update(false);
        //            if (this.hotKeyBn.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisB);
        //            }
        //            break;
        //        case Keys.Left:
        //            this.hotKeyXn.StsKeyDown.Update(false);
        //            if (this.hotKeyXn.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisX);
        //            }
        //            this.hotKeyAn.StsKeyDown.Update(false);
        //            if (this.hotKeyAn.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisA);
        //            }
        //            break;
        //        case Keys.Right:
        //            this.hotKeyXp.StsKeyDown.Update(false);
        //            if (this.hotKeyXp.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisX);
        //            }
        //            this.hotKeyAp.StsKeyDown.Update(false);
        //            if (this.hotKeyAp.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisA);
        //            }
        //            break;
        //        case Keys.Home:
        //            this.hotKeyZp.StsKeyDown.Update(false);
        //            if (this.hotKeyZp.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisZ);
        //            }
        //            break;
        //        case Keys.End:
        //            this.hotKeyZn.StsKeyDown.Update(false);
        //            if (this.hotKeyZn.StsKeyDown.IsFalling)
        //            {
        //                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisZ);
        //            }
        //            break;
        //    }
        //    base.OnKeyUp(e);
        //}
    }
}
