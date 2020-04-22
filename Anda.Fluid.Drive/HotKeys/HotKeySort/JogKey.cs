using Anda.Fluid.Infrastructure.HotKeying;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Drive.HotKeys.HotKeySort
{
    public class JogKey : IHotKeySortable
    {
        private List<HotKey> keyList;
        private HotKey jogKeyXp = new HotKey(Keys.Right);
        private HotKey jogKeyXn = new HotKey(Keys.Left);
        private HotKey jogKeyYp = new HotKey(Keys.Up);
        private HotKey jogKeyYn = new HotKey(Keys.Down);
        private HotKey jogKeyZp = new HotKey(Keys.Home);
        private HotKey jogKeyZn = new HotKey(Keys.End);
        private HotKey jogKeyAp = new HotKey(Keys.Right);
        private HotKey jogKeyAn = new HotKey(Keys.Left);
        private HotKey jogKeyBp = new HotKey(Keys.Up);
        private HotKey jogKeyBn = new HotKey(Keys.Down);
        private HotKey jogKeyRp = new HotKey(Keys.PageUp);
        private HotKey jogKeyRn = new HotKey(Keys.PageDown);

        public bool Enable { get; set; }

        public List<HotKey> KeyList => this.keyList;

        public HotKeySortEnum SortName => HotKeySortEnum.JogKey;

        public JogKey()
        {
            this.Enable = true;
            this.keyList = new List<HotKey>();
            this.keyList.Add(this.jogKeyXp);
            this.keyList.Add(this.jogKeyXn);
            this.keyList.Add(this.jogKeyYp);
            this.keyList.Add(this.jogKeyYn);
            this.keyList.Add(this.jogKeyZp);
            this.keyList.Add(this.jogKeyZn);
            this.keyList.Add(this.jogKeyAp);
            this.keyList.Add(this.jogKeyAn);
            this.keyList.Add(this.jogKeyBp);
            this.keyList.Add(this.jogKeyBn);
            this.keyList.Add(this.jogKeyRp);
            this.keyList.Add(this.jogKeyRn);
        }

        public void OnKeyDownEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;

            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down
                || e.KeyData == Keys.Left || e.KeyData == Keys.Right
                || e.KeyData == Keys.Home || e.KeyData == Keys.End
                || e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown)
            {
                if (Machine.Instance.IsBusy)
                {
                    return;
                }
            }

            Axis axisR = null;
            if (e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown)
            {
                if(Machine.Instance.Setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZR)
                {
                    axisR = Machine.Instance.Robot.AxisR;
                }
                else if(Machine.Instance.Setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZU)
                {
                    axisR = Machine.Instance.Robot.AxisU;
                }
                else
                {
                    return;
                }
            }

            switch (e.KeyData)
            {
                #region Y轴
                case Keys.Up:
                    this.jogKeyYp.State.Update(true);
                    if (this.jogKeyYp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, true, false);
                        Debug.WriteLine("y+");
                    }
                    break;
                case Keys.Up | Keys.Control:
                    this.jogKeyYp.State.Update(true);
                    if (this.jogKeyYp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, true, true);
                        Debug.WriteLine("y++");
                    }
                    break;
                case Keys.Down:
                    this.jogKeyYn.State.Update(true);
                    if (this.jogKeyYn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, false, false);
                        Debug.WriteLine("y-");
                    }
                    break;
                case Keys.Down | Keys.Control:
                    this.jogKeyYn.State.Update(true);
                    if (this.jogKeyYn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, false, true);
                        Debug.WriteLine("y--");
                    }
                    break;
                #endregion

                #region X轴
                case Keys.Left:
                    this.jogKeyXn.State.Update(true);
                    if (this.jogKeyXn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, false, false);
                        Debug.WriteLine("x-");
                    }
                    break;
                case Keys.Left | Keys.Control:
                    this.jogKeyXn.State.Update(true);
                    if (this.jogKeyXn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, false, true);
                        Debug.WriteLine("x--");
                    }
                    break;
                case Keys.Right:
                    this.jogKeyXp.State.Update(true);
                    if (this.jogKeyXp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, true, false);
                        Debug.WriteLine("x+");
                    }
                    break;
                case Keys.Right | Keys.Control:
                    this.jogKeyXp.State.Update(true);
                    if (this.jogKeyXp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, true, true);
                        Debug.WriteLine("x++");
                    }
                    break;
                #endregion

                #region Z轴
                case Keys.Home:
                    this.jogKeyZp.State.Update(true);
                    if (this.jogKeyZp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, true, false);
                        Debug.WriteLine("z+");
                    }
                    break;
                case Keys.Home | Keys.Control:
                    this.jogKeyZp.State.Update(true);
                    if (this.jogKeyZp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, true, true);
                        Debug.WriteLine("z++");
                    }
                    break;
                case Keys.End:
                    this.jogKeyZn.State.Update(true);
                    if (this.jogKeyZn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, false, false);
                        Debug.WriteLine("z-");
                    }
                    break;
                case Keys.End | Keys.Control:
                    this.jogKeyZn.State.Update(true);
                    if (this.jogKeyZn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, false, true);
                        Debug.WriteLine("z--");
                    }
                    break;
                #endregion

                #region A轴
                case Keys.Left | Keys.Control | Keys.Shift:
                    this.jogKeyAn.State.Update(true);
                    if (this.jogKeyAn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisA, false, false);
                        Debug.WriteLine("A-");
                    }
                    break;
                case Keys.Right | Keys.Control | Keys.Shift:
                    this.jogKeyAp.State.Update(true);
                    if (this.jogKeyAp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisA, true, false);
                        Debug.WriteLine("A+");
                    }
                    break;
                #endregion

                #region B轴
                case Keys.Down | Keys.Control | Keys.Shift:
                    this.jogKeyBn.State.Update(true);
                    if (this.jogKeyBn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisB, false, false);
                        Debug.WriteLine("B-");
                    }
                    break;
                case Keys.Up | Keys.Control | Keys.Shift:
                    this.jogKeyBp.State.Update(true);
                    if (this.jogKeyBp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisB, true, false);
                        Debug.WriteLine("B+");
                    }
                    break;
                #endregion

                #region R轴
                case Keys.PageUp:
                    this.jogKeyRp.State.Update(true);
                    if (this.jogKeyRp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(axisR, true, false);
                        Debug.WriteLine("r+");
                    }
                    break;
                case Keys.PageUp | Keys.Control:
                    this.jogKeyRp.State.Update(true);
                    if (this.jogKeyRp.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(axisR, true, true);
                        Debug.WriteLine("r++");
                    }
                    break;
                case Keys.PageDown:
                    this.jogKeyRn.State.Update(true);
                    if (this.jogKeyRn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(axisR, false, false);
                        Debug.WriteLine("r-");
                    }
                    break;
                case Keys.PageDown | Keys.Control:
                    this.jogKeyRn.State.Update(true);
                    if (this.jogKeyRn.State.IsRising)
                    {
                        Machine.Instance.Robot.ManualMove(axisR, false, true);
                        Debug.WriteLine("r--");
                    }
                    break;
                    #endregion
            }
        }

        public void OnKeyUpEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;

            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down
                || e.KeyData == Keys.Left || e.KeyData == Keys.Right
                || e.KeyData == Keys.Home || e.KeyData == Keys.End
                || e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown)
            {
                if (Machine.Instance.IsBusy)
                {
                    return;
                }
            }

            Axis axisR = null;
            if (e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown)
            {
                if (Machine.Instance.Setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZR)
                {
                    axisR = Machine.Instance.Robot.AxisR;
                }
                else if (Machine.Instance.Setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZU)
                {
                    axisR = Machine.Instance.Robot.AxisU;
                }
                else
                {
                    return;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.jogKeyYp.State.Update(false);
                    if (this.jogKeyYp.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisY);
                    }
                    this.jogKeyBp.State.Update(false);
                    if (this.jogKeyBp.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisB);
                    }
                    break;
                case Keys.Down:
                    this.jogKeyYn.State.Update(false);
                    if (this.jogKeyYn.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisY);
                    }
                    this.jogKeyBn.State.Update(false);
                    if (this.jogKeyBn.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisB);
                    }
                    break;
                case Keys.Left:
                    this.jogKeyXn.State.Update(false);
                    if (this.jogKeyXn.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisX);
                    }
                    this.jogKeyAn.State.Update(false);
                    if (this.jogKeyAn.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisA);
                    }
                    break;
                case Keys.Right:
                    this.jogKeyXp.State.Update(false);
                    if (this.jogKeyXp.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisX);
                    }
                    this.jogKeyAp.State.Update(false);
                    if (this.jogKeyAp.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisA);
                    }
                    break;
                case Keys.Home:
                    this.jogKeyZp.State.Update(false);
                    if (this.jogKeyZp.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisZ);
                    }
                    break;
                case Keys.End:
                    this.jogKeyZn.State.Update(false);
                    if (this.jogKeyZn.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisZ);
                    }
                    break;
                case Keys.PageUp:
                    this.jogKeyRp.State.Update(false);
                    if (this.jogKeyRp.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(axisR);
                    }
                    break;
                case Keys.PageDown:
                    this.jogKeyRn.State.Update(false);
                    if (this.jogKeyRn.State.IsFalling)
                    {
                        Machine.Instance.Robot.ManualMoveStop(axisR);
                    }
                    break;
            }
        }
    }
}
