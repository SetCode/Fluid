using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using System.Threading;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive.Motion.ActiveItems;

namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    public partial class DotValveOffsetCtl : UserControl,ITestCtlable
    {
        private int index = 0;
        private DotParam dotParam = null;
        private DotStyle dotStyle = DotStyle.TYPE_1;
        private bool isDone = false;

        //传入的参数
        private TiltType tileType;
        private double valveAngle;
        private PointD valveCameraOffset;
        private double standardZ = 0;
        private double gap = 0;

        /// <summary>
        /// 四点打胶中心的相机位置
        /// </summary>
        private PointD dispensePos = new PointD();
        private PointD dotValveOffset = new PointD();

        public PointD DotValveOffset => this.dotValveOffset;

        public double Gap => this.gap;


        private double dot2TotalOffset = 0;
        private double dot3TotalOffset = 0;
        private double dot4TotalOffset = 0;
        public DotValveOffsetCtl()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                cbxDotStyle.Items.Add("Type " + (i + 1));
            }
            cbxDotStyle.SelectedIndex = 0;

            this.UpdateByIndex();
        }


        private void btnEditDot_Click(object sender, EventArgs e)
        {
            new EditDotParamsForm(FluidProgram.CurrentOrDefault().ProgramSettings.DotParamList).ShowDialog();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.isDone = false;
            if (this.index != 6)
            {
                this.index++;
            }
            this.UpdateByIndex();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.isDone = false;
            if (index == 4 || index == 5 || index == 6)
            {
                this.index = 2;
            }
            else
            {
                this.index--;
            }
            this.UpdateByIndex();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Reset();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.isDone = true;
        }

        private void UpdateByIndex()
        {

            switch (index)
            {
                case 0:
                    this.txtMsg.Text = "用相机寻找到打胶中心，按 [下一步]，将会在距离寻找到的中心半径5mm的四个方向打胶";
                    this.btnNext.Enabled = true;
                    break;
                case 1:
                    this.dispensePos = Machine.Instance.Robot.PosXY;
                    this.dotStyle = (DotStyle)this.cbxDotStyle.SelectedIndex;
                    this.dotParam = FluidProgram.CurrentOrDefault().ProgramSettings.GetDotParam(dotStyle);
                    this.gap = this.dotParam.DispenseGap;
                    Task.Factory.StartNew(new Action(() =>
                    {
                        bool b = this.MeasureHeightAndDot();
                        this.BeginInvoke(new Action(() =>
                        {
                            if (b)
                            {
                                this.txtMsg.Text = "点胶结束,按 [下一步]，将会移动到第一个点胶位置";
                            }
                            else
                            {
                                this.txtMsg.Text = "点胶失败";
                                this.btnNext.Enabled = false;
                            }
                        }));
                    }));
                    break;
                case 2:
                    this.txtMsg.Text = "找到第一个胶点的中心位置，按 [下一步]";
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //移动到点1附近
                    PointD p1 = new PointD(this.dispensePos.X - 5, this.dispensePos.Y + 5);
                    Machine.Instance.Robot.ManualMovePosXY(p1);

                    this.txtDot2Offset.BackColor = Color.White;
                    this.txtDot3Offset.BackColor = Color.White;
                    this.txtDot4Offset.BackColor = Color.White;
                    break;
                case 3:
                    //计算实际点胶中心和胶阀中心的差值
                    this.dotValveOffset = new PointD(Machine.Instance.Robot.PosX - (this.dispensePos.X - 5), Machine.Instance.Robot.PosY - (this.dispensePos.Y + 5));
                    this.txtMsg.Text = "找到第二个胶点的中心位置，按 [下一步]";
                    PointD p2 = new PointD(this.dispensePos.X + 5, this.dispensePos.Y + 5);
                    Machine.Instance.Robot.ManualMovePosXY(p2);
                    break;
                case 4:
                    //计算修正后,在点2时实际点胶中心和胶阀中心的差值
                    PointD dot2Offset = new PointD();
                    dot2Offset.X = Machine.Instance.Robot.PosX - (this.dispensePos.X + 5 + this.dotValveOffset.X);
                    dot2Offset.Y = Machine.Instance.Robot.PosY - (this.dispensePos.Y + 5 + this.dotValveOffset.Y);
                    this.dot2TotalOffset = Math.Sqrt(Math.Pow(dot2Offset.X, 2) + Math.Pow(dot2Offset.Y, 2));
                    this.txtDot2Offset.Text = dot2TotalOffset.ToString();

                    this.txtMsg.Text = "找到第三个胶点的中心位置，按 [下一步]";
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    PointD p3 = new PointD(this.dispensePos.X - 5, this.dispensePos.Y - 5);
                    Machine.Instance.Robot.ManualMovePosXY(p3);
                    break;
                case 5:
                    //计算修正后,在点2时实际点胶中心和胶阀中心的差值
                    PointD dot3Offset = new PointD();
                    dot3Offset.X = Machine.Instance.Robot.PosX - (this.dispensePos.X - 5 + this.dotValveOffset.X);
                    dot3Offset.Y = Machine.Instance.Robot.PosY - (this.dispensePos.Y - 5 + this.dotValveOffset.Y);
                    this.dot3TotalOffset = Math.Sqrt(Math.Pow(dot3Offset.X, 2) + Math.Pow(dot3Offset.Y, 2));
                    this.txtDot3Offset.Text = dot3TotalOffset.ToString();

                    this.txtMsg.Text = "找到第四个胶点的中心位置，按 [下一步]";
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    PointD p4 = new PointD(this.dispensePos.X + 5, this.dispensePos.Y - 5);
                    Machine.Instance.Robot.ManualMovePosXY(p4);
                    break;
                case 6:
                    //计算修正后,在点2时实际点胶中心和胶阀中心的差值
                    PointD dot4Offset = new PointD();
                    dot4Offset.X = Machine.Instance.Robot.PosX - (this.dispensePos.X + 5 + this.dotValveOffset.X);
                    dot4Offset.Y = Machine.Instance.Robot.PosY - (this.dispensePos.Y - 5 + this.dotValveOffset.Y);
                    this.dot4TotalOffset = Math.Sqrt(Math.Pow(dot4Offset.X, 2) + Math.Pow(dot4Offset.Y, 2));
                    this.txtDot4Offset.Text = dot4TotalOffset.ToString();

                    if (this.dot2TotalOffset > Convert.ToDouble(this.txtTolerance.Text))
                    {
                        this.rdoFail.Checked = true;
                        this.rdoFail.Enabled = true;
                        this.txtDot2Offset.BackColor = Color.DarkRed;
                    }
                    if (dot3TotalOffset > Convert.ToDouble(this.txtTolerance.Text))
                    {
                        this.rdoFail.Checked = true;
                        this.rdoFail.Enabled = true;
                        this.txtDot3Offset.BackColor = Color.DarkRed;
                    }
                    if (dot4TotalOffset > Convert.ToDouble(this.txtTolerance.Text))
                    {
                        this.rdoFail.Checked = true;
                        this.rdoFail.Enabled = true;
                        this.txtDot4Offset.BackColor = Color.DarkRed;
                    }
                    if ((dot2TotalOffset <= Convert.ToDouble(this.txtTolerance.Text))
                        && (dot3TotalOffset <= Convert.ToDouble(this.txtTolerance.Text))
                            && (dot4TotalOffset <= Convert.ToDouble(this.txtTolerance.Text)))
                    {
                        this.rdoOK.Checked = true;
                        this.rdoOK.Enabled = true;
                        this.txtDot2Offset.BackColor = Color.White;
                        this.txtDot3Offset.BackColor = Color.White;
                        this.txtDot4Offset.BackColor = Color.White;
                    }

                    this.txtMsg.Text = "四点校正完成";
                    break;
            }
        }

       
        private bool MeasureHeightAndDot()
        {
            int rst;
            Result result = Result.OK;
            //关闭光源
            Machine.Instance.Light.None();
            Machine.Instance.MeasureHeightBefore();
            //去点1处测高
            Machine.Instance.Robot.ManualMovePosXYAndReply(
                this.dispensePos.X - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                this.dispensePos.Y + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
            double height1;
            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height1);
            //Result res= Machine.Instance.MeasureHeight(out height1);
            //rst = res.IsOk ? 0 : -1;
            if (rst != 0)
            {
                MessageBox.Show("测高失败");
                return false;
            }
            double z1 = this.standardZ + (height1 - Machine.Instance.Robot.CalibPrm.StandardHeight);

            //去点2处测高
            Machine.Instance.Robot.ManualMovePosXYAndReply(
                this.dispensePos.X + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                this.dispensePos.Y + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
            double height2;
            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height2);
            //res = Machine.Instance.MeasureHeight(out height2);
            //rst = res.IsOk ? 0 : -1;
            if (rst != 0)
            {
                MessageBox.Show("测高失败");
                return false;
            }
            double z2 = this.standardZ + (height2 - Machine.Instance.Robot.CalibPrm.StandardHeight);

            //去点3处测高
            Machine.Instance.Robot.ManualMovePosXYAndReply(
                this.dispensePos.X + 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                this.dispensePos.Y - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
            double height3;
            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height3);
            //res = Machine.Instance.MeasureHeight(out height3);
            //rst = res.IsOk ? 0 : -1;
            if (rst != 0)
            {
                MessageBox.Show("测高失败");
                return false;
            }
            double z3 = this.standardZ + (height3 - Machine.Instance.Robot.CalibPrm.StandardHeight);

            //去点4处测高
            Machine.Instance.Robot.ManualMovePosXYAndReply(
                this.dispensePos.X - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                this.dispensePos.Y - 5 + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
            double height4;
            rst = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out height4);
            //res = Machine.Instance.MeasureHeight(out height4);
            //rst = res.IsOk ? 0 : -1;
            if (rst != 0)
            {
                MessageBox.Show("测高失败");
                return false;
            }
            double z4 = this.standardZ + (height4 - Machine.Instance.Robot.CalibPrm.StandardHeight);

            Machine.Instance.MeasureHeightAfter();
            //打开光源
            Machine.Instance.Light.ResetToLast();
            //

            //去清洗位置洗喷嘴(恢复垂直状态)
            Machine.Instance.Robot.MoveSafeZAndReply();
            Machine.Instance.Valve1.DoPurgeAndPrime();

            //变化胶阀角度
            Machine.Instance.Robot.MovePosU(this.valveAngle);
            Machine.Instance.Valve1.CurTilt = this.tileType;

            //到点胶位置1点胶
            this.BeginInvoke(new Action(() =>
            {
                this.txtMsg.Text = "到点胶位置1点胶";
            }));
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZAndReply();
            //移动到点胶位置1
            PointD p1 = new PointD(this.dispensePos.X - 5 + this.valveCameraOffset.X, this.dispensePos.Y + 5 + this.valveCameraOffset.Y);
            Machine.Instance.Robot.ManualMovePosXYAndReply(p1);
            //点胶
            result = this.DotByParam(z1);
            if (result != Result.OK)
            {
                return false;
            }

            //到点胶位置2点胶
            this.BeginInvoke(new Action(() =>
            {
                this.txtMsg.Text = "到点胶位置2点胶";
            }));
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZAndReply();
            //移动到点胶位置1
            PointD p2 = new PointD(this.dispensePos.X + 5 + this.valveCameraOffset.X, this.dispensePos.Y + 5 + this.valveCameraOffset.Y);
            Machine.Instance.Robot.ManualMovePosXYAndReply(p2);
            //点胶
            result = this.DotByParam(z2);
            if (result != Result.OK)
            {
                return false;
            }

            //到点胶位置3点胶
            this.BeginInvoke(new Action(() =>
            {
                this.txtMsg.Text = "到点胶位置3点胶";
            }));
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZAndReply();
            //移动到点胶位置1
            PointD p3 = new PointD(this.dispensePos.X - 5 + this.valveCameraOffset.X, this.dispensePos.Y - 5 + this.valveCameraOffset.Y);
            Machine.Instance.Robot.ManualMovePosXYAndReply(p3);
            //点胶
            result = this.DotByParam(z3);
            if (result != Result.OK)
            {
                return false;
            }

            //到点胶位置4点胶
            this.BeginInvoke(new Action(() =>
            {
                this.txtMsg.Text = "到点胶位置4点胶";
            }));
            //抬起到SafeZ
            Machine.Instance.Robot.MoveSafeZAndReply();
            //移动到点胶位置1
            PointD p4 = new PointD(this.dispensePos.X + 5 + this.valveCameraOffset.X, this.dispensePos.Y - 5 + this.valveCameraOffset.Y);
            Machine.Instance.Robot.ManualMovePosXYAndReply(p4);
            //点胶
            result = this.DotByParam(z4);
            if (result != Result.OK)
            {
                return false;
            }

            return true;
        }

        private Result DotByParam(double z)
        {
            // 下降到指定高度
            Result ret = Machine.Instance.Robot.MovePosZAndReply(z + dotParam.DispenseGap, dotParam.DownSpeed, dotParam.DownAccel);
            if (!ret.IsOk)
            {
                return ret;
            }
            Thread.Sleep(TimeSpan.FromSeconds(dotParam.SettlingTime));

            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                Machine.Instance.Valve1.SprayCycleAndWait((short)dotParam.NumShots);

                DateTime sprayEnd = DateTime.Now;

                // 等待一段时间 Dwell Time
                double ellapsed = (DateTime.Now - sprayEnd).TotalSeconds;
                double realDwellTime = dotParam.DwellTime - ellapsed;
                if (realDwellTime > 0)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(realDwellTime));
                }
            }

            // 抬高一段距离 Retract Distance
            if (dotParam.RetractDistance > 0)
            {
                ret = Machine.Instance.Robot.MoveIncZAndReply(dotParam.RetractDistance, dotParam.RetractSpeed, dotParam.RetractAccel);
            }

            return ret;
        }

        public void Setup(object[] objs)
        {
            dotParam = FluidProgram.CurrentOrDefault().ProgramSettings.GetDotParam(dotStyle);
            this.tileType = (TiltType)objs[0];
            this.valveAngle = (double)objs[1];
            this.valveCameraOffset = (PointD)objs[2];
            this.standardZ = (double)objs[3];
        }

        public void Reset()
        {
            this.isDone = false;
            this.index = 0;
            this.UpdateByIndex();
        }
        public bool IsDone()
        {
            if (this.isDone)
                return true;
            else
                return false;
        }
    }
}
