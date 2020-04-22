using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogNeedleAngle : JogFormBase
    {
        #region Fields
        private int step = 0;
        private PointD needleStartPos = new PointD();
        private PointD needleEndPos = new PointD();
        private double needlePosZUp = 0;
        private double needlePosZDown = 0;
        private double angleLine = 0;
        private double angleRotated = 0;

        private PointD interStart = null;
        private PointD interEnd = null;
        private PointD interMiddle = null;      

        public double moveVel = 5;
        public double moveAcc = 1;
        #endregion 

        public DialogNeedleAngle()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterParent;

            this.txtP1X.ReadOnly = true;
            this.txtP1Y.ReadOnly = true;
            this.txtP2X.ReadOnly = true;
            this.txtP2Y.ReadOnly = true;
            this.txtPosZUp.ReadOnly = true;

            this.txtInter1X.ReadOnly = true;
            this.txtInter1Y.ReadOnly = true;
            this.txtInter2X.ReadOnly = true;
            this.txtInter2Y.ReadOnly = true;
            this.txtAngle.ReadOnly = true;
            this.txtRotated.ReadOnly = true;

        
            this.nudTolerance.Increment = (decimal)0.001;           
            this.nudTolerance.Minimum = 0;
            this.nudTolerance.Maximum = 1;
            this.nudTolerance.DecimalPlaces = 3;

            this.ReadLanguageResources();
        }
        public DialogNeedleAngle Setup()
        {
            this.needleStartPos = Machine.Instance.Robot.CalibPrm.NeedleStartPos;
            this.needleEndPos = Machine.Instance.Robot.CalibPrm.NeedleEndPos;
            this.needlePosZUp=Machine.Instance.Robot.CalibPrm.NeedlePosZUp;
            this.needlePosZDown = Machine.Instance.Robot.CalibPrm.NeedlePosZDown;
            return this;
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {            
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.step > 0)
            {
                this.step--;
                this.UpdateByFlag();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.step < 6)
            {
                this.step++;
                this.UpdateByFlag();
            }
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            this.teachPosition();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.CalibPrm.NeedlePosZUp = this.needlePosZUp;
            Machine.Instance.Robot.CalibPrm.NeedlePosZDown = this.needlePosZDown;
            Machine.Instance.Robot.CalibPrm.NeedleStartPos = this.needleStartPos;
            Machine.Instance.Robot.CalibPrm.NeedleEndPos = this.needleEndPos;
            Machine.Instance.Robot.CalibPrm.NeedleRotated = this.angleRotated;
            Machine.Instance.Robot.CalibPrm.AngleLine = this.angleLine;
            Machine.Instance.Robot.SaveCalibPrm();

            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    Machine.Instance.Robot.ManualMovePosXY(this.needleStartPos);
                    Machine.Instance.Robot.MovePosZ(this.needlePosZUp);

                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 2:                  
                    Machine.Instance.Robot.ManualMovePosXY(this.needleEndPos);
                    Machine.Instance.Robot.MovePosZ(this.needlePosZDown);
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnTeach.Enabled = true;
                    this.btnDone.Enabled = false;
                    break;
                case 3:
                    //执行，得到光纤角度
                    this.getopticalFiberAngle();
                    break;
                case 4:                    
                    //确保针嘴的垂直
                    this.checkVerticality();
                    break;
                case 5:
                    this.findGap();
                    break;

            }
        }
        private double scaleLine2X = 1;
        
        private void teachPosition()
        {
            switch (this.step)
            {
                case 1:
                    this.needleStartPos = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                    this.needlePosZUp = Machine.Instance.Robot.PosZ;                   
                    this.txtP1X.Text = Machine.Instance.Robot.PosX.ToString("0.000");
                    this.txtP1Y.Text = Machine.Instance.Robot.PosY.ToString("0.000");
                    this.txtPosZUp.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnTeach.Enabled = false;
                    this.btnDone.Enabled = false;
                    break;
                case 2:
                    if (Machine.Instance.Robot.PosX > this.needleStartPos.X && Machine.Instance.Robot.PosY > this.needleStartPos.Y)
                    {
                        this.needleEndPos = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
                        this.needlePosZDown= Machine.Instance.Robot.PosZ;
                        this.txtP2X.Text = Machine.Instance.Robot.PosX.ToString("0.000");
                        this.txtP2Y.Text = Machine.Instance.Robot.PosY.ToString("0.000");
                        this.txtPosZDown.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
                        this.btnPrev.Enabled = true;
                        this.btnNext.Enabled = true;
                        this.btnTeach.Enabled = false;
                        this.btnDone.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("请确保终点在起点的右上");                        
                    }                    
                    break;

            }

        }
        


        private int diKey = (int)DiType.对刀仪;
        private async void checkVerticality()
        {
            Result res;
            double dyUp1 = 0, dyUp2 = 0;
            double dyDown1 = 0, dyDown2 = 0;
            double dx = this.needleEndPos.X - this.needleStartPos.X;
            double dy = this.needleEndPos.Y - this.needleStartPos.Y;
            double posy = 0;
            await Task.Factory.StartNew(() =>
            {
                //上面运动
                Machine.Instance.Robot.ManualMovePosXYAndReply(this.needleStartPos);
                Machine.Instance.Robot.MovePosZAndReply(this.needlePosZUp);
                Machine.Instance.Robot.MovePosRAndReply(0);
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(this.needleStartPos.Y + dy, this.diKey, StsType.IsRising, out posy);
                if (!res.IsOk)
                {
                    MessageBox.Show("针嘴没有和光纤接触，请确保示教点位是否有问题");
                    return;
                }
                dyUp1 = posy*this.scaleLine2X;

                Machine.Instance.Robot.ManualMovePosXYAndReply(this.needleStartPos);
                Machine.Instance.Robot.MovePosRAndReply(90);
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(this.needleStartPos.Y + dy, this.diKey, StsType.IsRising, out posy);
                if (!res.IsOk)
                {
                    MessageBox.Show("针嘴没有和光纤接触，请确保示教点位是否有问题");
                    return;
                }
                dyUp2 = posy*this.scaleLine2X;
                //下面运动
                Machine.Instance.Robot.ManualMovePosXYAndReply(this.needleStartPos);
                Machine.Instance.Robot.MovePosZAndReply(this.needlePosZDown);
                Machine.Instance.Robot.MovePosRAndReply(0);
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(this.needleStartPos.Y + dy, this.diKey, StsType.IsRising, out posy);
                if (!res.IsOk)
                {
                    MessageBox.Show("针嘴没有和光纤接触，请确保示教点位是否有问题");
                    return;
                }
                dyDown1 = posy* this.scaleLine2X;

                Machine.Instance.Robot.ManualMovePosXYAndReply(this.needleStartPos);
                Machine.Instance.Robot.MovePosRAndReply(90);
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(this.needleStartPos.Y + dy, this.diKey, StsType.IsRising, out posy);
                if (!res.IsOk)
                {
                    MessageBox.Show("针嘴没有和光纤接触，请确保示教点位是否有问题");
                    return;
                }
                dyDown2 = posy* this.scaleLine2X;
            });
            double offset = Math.Sqrt((dyUp1 - dyDown1) * (dyUp1 - dyDown1) + (dyUp2 - dyDown2) * (dyUp2 - dyDown2));
            this.txtNeedleOffset.Text = offset.ToString("0.000");
            if (offset > (double)this.nudTolerance.Value)
            {
                MessageBox.Show("针嘴同心度超限");
            }
            else
            {
                MessageBox.Show("针嘴同心度合格");
            }

        }
        private async void getopticalFiberAngle()
        {
            double dx = this.needleEndPos.X - this.needleStartPos.X;
            double dy = this.needleEndPos.Y - this.needleStartPos.Y;
            PointD[] pointResult1 = new PointD[4];

            //MoveLnXY(PointD pos, double vel, double acc)
            PointD p1 = this.needleStartPos;
            PointD p2 = this.needleStartPos + new PointD(0, dy);
            PointD p3 = this.needleEndPos + new PointD(0, -dy);
            PointD p4 = this.needleEndPos;
            await Task.Factory.StartNew(() => {
                Result res;
                //针嘴下面
                Machine.Instance.Robot.MovePosZ(this.needlePosZUp);
                //得到第一个交点 
                Machine.Instance.Robot.ManualMovePosXYAndReply(p1);
                double posy = 0;
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(p1.Y + dy, this.diKey, StsType.IsRising, out posy);
                //res = Machine.Instance.Robot.ManualMovePosXYAndReply(p1.X,p1.Y+dy);
                if (!res.IsOk)
                {
                    return;
                }
                this.interStart = new PointD(this.needleStartPos.X, posy);
                //得到第二个交点
                Machine.Instance.Robot.ManualMovePosXYAndReply(p3);
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(p3.Y + dy, this.diKey, StsType.IsRising, out posy);
                //res = Machine.Instance.Robot.ManualMovePosXYAndReply(p3.X, p3.Y + dy);
                if (!res.IsOk)
                {
                    return;
                }
                this.interEnd = new PointD(this.needleEndPos.X, posy);
                //Machine.Instance.Robot.MoveSafeZ();
            });
            if (this.interStart == null || this.interEnd == null)
            {
                MessageBox.Show("针嘴和光纤没交点，请确认示教点是否有问题");
            }
            double x = (this.interStart + this.interEnd).X;
            double y = (this.interStart + this.interEnd).Y;
            this.interMiddle = new PointD(x / 2, y / 2);

            this.txtInter1X.Text = this.interStart.X.ToString("0.000");
            this.txtInter1Y.Text = this.interStart.Y.ToString("0.000");
            this.txtInter2X.Text = this.interEnd.X.ToString("0.000");
            this.txtInter2Y.Text = this.interEnd.Y.ToString("0.000");

            this.angleLine = MathUtils.CalculateArc(this.interStart, this.interEnd);
            this.scaleLine2X = Math.Cos(this.angleLine/180*Math.PI);
            this.txtAngle.Text = this.angleLine.ToString("0.000");
        }


        private  async void findGap()
        {
            double angleSum = 0;
            double deltaY = 10;
            PointD pFront = new PointD(this.interMiddle.X,this.interMiddle.Y- deltaY);
            PointD pBehind = new PointD(this.interMiddle.X, this.interMiddle.Y + deltaY);
            double posy = 0, posYFront=0, posYBehind=0;
            await Task.Factory.StartNew(()=>
            {
                Result res;
                Machine.Instance.Robot.ManualMovePosXYAndReply(pFront);
                res=Machine.Instance.Robot.MoveAxisYOnDIPosBack(pFront.Y + deltaY, this.diKey, StsType.IsRising, out posy);
                if (!res.IsOk)
                {
                    return;
                }
                posYFront = posy;
                Machine.Instance.Robot.ManualMovePosXYAndReply(pBehind);
                res = Machine.Instance.Robot.MoveAxisYOnDIPosBack(pFront.Y - deltaY, this.diKey, StsType.IsRising, out posy);
                if (!res.IsOk)
                {
                    return;
                }
                posYBehind = posy;
                Machine.Instance.Robot.ManualMovePosXYAndReply(new PointD(this.interMiddle.X, (posYBehind+ posYFront)/2));
                if (DIMgr.Instance.FindBy(this.diKey).Status.Is(StsType.Low))
                {
                    MessageBox.Show("确保针嘴在光纤光路上");
                    return;
                }
                //在光路上,移动z轴直到光纤没遮住
                double posZ = 0;
                res=Machine.Instance.Robot.MoveAxisZOnDIPosBack(this.needlePosZDown+30, this.diKey, StsType.Low, out posZ);
                if (!res.IsOk)
                {
                    return;
                }
                bool isBottom = false;
                //z向下移动0.5mm，确保针嘴的位置
                Machine.Instance.Robot.MovePosZAndReply(posZ+0.5);
                //判断是否为缺口,旋转90度，如果还是低，说明到底端了，如果为高 说明是缺口
                Machine.Instance.Robot.MovePosRAndReply(90);
                if (DIMgr.Instance.FindBy(this.diKey).Status.Is(StsType.Low))
                {
                    isBottom = true;
                    Logger.DEFAULT.Info("底端了");
                }
                else
                {
                    isBottom = false;
                    Logger.DEFAULT.Info("是缺口");
                }
                //确保光纤的高度在缺口高度范围内
                if (isBottom)
                {
                    //是底部，针嘴向下移动0.5
                    Machine.Instance.Robot.MovePosZAndReply(posZ - 0.5);
                }
                else
                {
                    //是缺口，针嘴向下移动0.5
                    Machine.Instance.Robot.MovePosZAndReply(posZ +0.5);
                }

                //转到0度
                Machine.Instance.Robot.MovePosRAndReply(0);
                bool isPrepared = true;
                //确保针嘴把光纤挡住了，才可以进行下一步 
                //没遮挡为高电平 常为高电平 遮挡了为电平
                //没遮挡 1、正好是缺口 2、没移动到光纤上 
                //确保信号Status为_一_   
                //MovePosR(double angle, double vel)
                if (DIMgr.Instance.FindBy(this.diKey).Status.Is(StsType.Low))
                {
                    //转90° 再次检查
                    Machine.Instance.Robot.MovePosRAndReply(90);
                    //如果状态没变化
                    if (DIMgr.Instance.FindBy(this.diKey).Status.Is(StsType.Low))
                    {
                        isPrepared = false;
                    }
                    else if(DIMgr.Instance.FindBy(this.diKey).Status.Is(StsType.High))
                    {
                        isPrepared = true;
                    }
                }
                isPrepared = true;
                if (isPrepared)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Result res1, res2;
                        //转到0度
                        Machine.Instance.Robot.MovePosRAndReply(0);
                        //如果在
                        double angle1 = 0, angle2 = 0;
                        if (DIMgr.Instance.FindBy(this.diKey).Status.Is(StsType.Low))
                        {
                            angle1 = Machine.Instance.Robot.PosR;
                            res1 = Result.OK;
                        }
                        else
                        {
                            res1 = Machine.Instance.Robot.MoveAxisROnDIPosBack(360, this.diKey, StsType.IsFalling, out angle1);
                        }
                        res2 = Machine.Instance.Robot.MoveAxisROnDIPosBack(360, this.diKey, StsType.IsRising, out angle2);
                        if (!res1.IsOk || !res2.IsOk)
                        {
                            MessageBox.Show("针嘴的位置不对，不在光纤光路上");
                        }
                        angleSum += (angle1 + angle2) / 2;
                    }
                }
                                
            } 
            );
            this.angleRotated=angleSum / 3;
            this.txtRotated.Text = this.angleRotated.ToString("0.000");       
        }

        private void DialogNeedleAngle_Load(object sender, EventArgs e)
        {
            this.UpdateByFlag();
        }
    }
}
