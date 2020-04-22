using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.ActiveItems;

namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    public partial class GapMeasureCtl : UserControl,ITestCtlable
    {
        private TiltType tiltType;
        private double posZ = 0;
        private double angle = 0;
        private PointD offset = new PointD();
        private PointD heightPoint = new PointD();
        private int index = 0;
        private double lineStartHeight = 0;
        private double standardHeight = 0;
        private double standardZ = 0;
        private double gap = 0;
        private bool isDone;

        public double PosZ => this.posZ;

        public double StandardZ => this.standardZ;
        public GapMeasureCtl()
        {
            InitializeComponent();
            this.UpdateByIndex();
        }

        private void btnPress_Click(object sender, EventArgs e)
        {
            this.index = 1;
            this.UpdateByIndex();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Reset();
        }



        private void UpdateByIndex()
        {
            switch (index)
            {
                case 0:
                    this.isDone = false;
                    this.btnPress.Enabled = true;
                    this.txtMsg.Text = "确认下压参数后，按下 [执行下压] 。";
                    break;
                case 1:
                    this.isDone = false;
                    this.btnPress.Enabled = false;
                    Task.Factory.StartNew(new Action(() =>
                    {
                        bool b = this.BeginZAxisMeasurement();
                        this.BeginInvoke(new Action(() =>
                        {
                            if (b)
                            {
                                this.btnPress.Enabled = false;
                                this.txtMsg.Text = "可进行下一阶段";
                                this.isDone = true;
                            }
                            else
                            {
                                this.btnPress.Enabled = true;
                            }
                        }));
                    }));
                    break;
            }
        }


        private bool BeginZAxisMeasurement()
        {
            int cycles = (int)this.nudCycles.Value;
            double totalZ = 0;
            double sumTwiceZ = 0;
            double centerH;

            for (int i = 0; i < cycles; i++)
            {
                //关闭光源
                Machine.Instance.Light.None();
                //
                //抬起到SafeZ
                Machine.Instance.Robot.MoveSafeZAndReply();

                //将激光移动到圆盘中心
                Result moveResult1 = Machine.Instance.Robot.ManualMovePosXYAndReply(Machine.Instance.Robot.CalibPrm.HeightSensorCenter.X + Machine.Instance.Robot.CalibPrm.HeightCamera.X,
                    Machine.Instance.Robot.CalibPrm.HeightSensorCenter.Y + Machine.Instance.Robot.CalibPrm.HeightCamera.Y);
                if (!moveResult1.IsOk)
                {
                    MessageBox.Show("将激光移动到圆盘中心失败");
                    return false;
                }

                //              
                //记录激光测高数据
                int result = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out centerH);
                //Result res = Machine.Instance.MeasureHeight(out centerH);
                //int result = res.IsOk ? 0 : -1;
                //判断激光能否使用
                if (result != 0)
                {
                    MessageBox.Show("激光不可用");
                    return false;
                }

                totalZ += centerH;

                //打开光源
                Machine.Instance.Light.ResetToLast();
                //

                //TODO 根据阀倾斜姿态调整胶阀

                //调整阀的角度
                Machine.Instance.Robot.MovePosU(this.angle);

                //将阀移动至圆盘中心
                Result moveResult2 = Machine.Instance.Robot.ManualMovePosXYAndReply(Machine.Instance.Robot.CalibPrm.HeightSensorCenter.X + this.offset.X,
                 Machine.Instance.Robot.CalibPrm.HeightSensorCenter.Y + this.offset.Y);
                if (!moveResult2.IsOk)
                {
                    MessageBox.Show("将阀组移动到圆盘中心失败");
                    return false;
                }               

                //反复下压两次
                for (int j = 0; j < 2; j++)
                {
                    Result moveResult4 = Machine.Instance.Robot.MoveZOnDIAndReply(Convert.ToDouble(this.nudVelocity.Value),
                        (int)DiType.对刀仪, Infrastructure.StsType.High);
                    if (!moveResult4.IsOk)
                    {
                        MessageBox.Show("下压失败");
                        return false;
                    }

                    sumTwiceZ += Machine.Instance.Robot.PosZ; //下降到底时Z坐标
                    Machine.Instance.Robot.MoveSafeZAndReply();
                }

            }
            //结果计算            
            this.standardHeight = totalZ / cycles;
            this.standardZ = sumTwiceZ / (cycles * 2);
            this.BeginInvoke(new Action(() =>
            {
                this.txtStandardZ.Text = this.standardZ.ToString();
            }));
            return true;
        }

        public void Setup(object[] objs)
        {
            this.tiltType = (TiltType)objs[0];
            this.posZ = (double)objs[1];
            this.angle = (double)objs[2];
            this.offset = (PointD)objs[3];
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
