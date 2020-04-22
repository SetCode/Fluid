using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal partial class NeedleCalibrationForm : TeachFormBase,IClickable
    {
        public NeedleCalibrationForm()
        {
            InitializeComponent();
            this.nudLmt.DecimalPlaces = 3;
            this.nudLmt.Minimum = 0;
            this.nudLmt.Maximum = 10;
            this.nudLmt.Increment = (decimal)0.001;
        }
        private int step = 0;
        /// <summary>
        /// 取消
        /// </summary>
        public void DoCancel()
        {
            this.Close();
        }
        /// <summary>
        /// 完成
        /// </summary>
        public void DoDone()
        {
            DataSetting.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void DoHelp()
        {
            
        }

        public void DoNext()
        {
            if (this.step < 4)
            {
                this.step++;
            }            
        }

        public void DoPrev()
        {
            if (this.step>0)
            {
                this.step--;
            }
            
        }

        public void DoTeach()
        {
            if (this.step<4)
            {
                this.step++;
            }
            
        }
        private void UpdateByFlag()
        {
            switch (this.step)
            {
                case 0:

                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;                    
                    break;
                case 1:
                    //移动到默认位置
                    Machine.Instance.Robot.MoveSafeZ();
                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.CentorInCross);
                    Machine.Instance.Robot.MovePosZ(DataSetting.Default.ZUp);

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 2:
                    DataSetting.Default.CentorInCross = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    DataSetting.Default.ZUp = Machine.Instance.Robot.PosZ;

                    Machine.Instance.Robot.ManualMovePosXY(DataSetting.Default.RPointInCross);

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 3:
                    DataSetting.Default.RPointInCross = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    DataSetting.Default.ZDown = Machine.Instance.Robot.PosZ;
                       
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 4:
                    //执行
                    this.Calibration();
                    break;

            }
        }

        public double vel = 5;
        public double acc = 1;

        private async void Calibration()
        {
            
            double r = DataSetting.Default.CentorInCross.DistanceTo(DataSetting.Default.RPointInCross);
            double delta = r*Math.Sin(Math.PI/4);            
            PointD[] pointResult1 = new PointD[4];
            PointD[] pointResult2 = new PointD[4];
            
            //MoveLnXY(PointD pos, double vel, double acc)
            PointD p1 = DataSetting.Default.CentorInCross+new PointD(delta, delta);
            PointD p2 = DataSetting.Default.CentorInCross + new PointD(-delta, delta);
            PointD p3 = DataSetting.Default.CentorInCross + new PointD(-delta, -delta);
            PointD p4 = DataSetting.Default.CentorInCross + new PointD(delta, -delta);
            await Task.Factory.StartNew(() => {
                //针嘴下面
                Machine.Instance.Robot.MovePosZ(DataSetting.Default.ZUp);
                Machine.Instance.Robot.MoveLnXY(DataSetting.Default.CentorInCross, this.vel, this.acc);
                Machine.Instance.Robot.MoveLnXY(p1, this.vel, this.acc);
                pointResult1[0] = this.MoveAndCapturePos(p2);
                pointResult1[1] = this.MoveAndCapturePos(p3);
                pointResult1[2] = this.MoveAndCapturePos(p4);
                pointResult1[3] = this.MoveAndCapturePos(p1);

                //针嘴上面
                Machine.Instance.Robot.MovePosZ(DataSetting.Default.ZDown);
                pointResult2[0] = this.MoveAndCapturePos(p2);
                pointResult2[1] = this.MoveAndCapturePos(p3);
                pointResult2[2] = this.MoveAndCapturePos(p4);
                pointResult2[3] = this.MoveAndCapturePos(p1);

                Machine.Instance.Robot.MoveSafeZ();
            });

            VectorD v21 = pointResult2[0] - pointResult2[2];
            VectorD v22 = pointResult2[1] - pointResult2[3];

            VectorD v11 = pointResult1[0] - pointResult1[2];
            VectorD v12 = pointResult1[1] - pointResult1[3];

            double dis1 = v11.CrossProduct(v21);
            double dis2 = v12.CrossProduct(v22);

            double lmt = (double)this.nudLmt.Value;
            double diff = Math.Sqrt(dis1 * dis1 + dis2 * dis2);
            this.lblInfo.Text = diff.ToString("0.000");
            if (diff > lmt)
            {
                this.lblInfo.ForeColor = Color.Red;
            }
            this.lblInfo.ForeColor = Color.Green;
        }

        private PointD MoveAndCapturePos(PointD point)
        {
            Machine.Instance.Robot.MoveLnXY(point, this.vel, this.acc);
            Result res = null;
                //Machine.Instance.Robot.WaitDIReply(0, true, TimeSpan.FromMilliseconds(10000));
            if (res.IsOk)
            {
                return new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
            }
            return null;
                
        }

       

    }
}
