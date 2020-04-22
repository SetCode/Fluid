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
using Anda.Fluid.Drive.Motion.ActiveItems;

namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    public partial class ValveCameraOffsetCtl : UserControl,ITestCtlable
    {
        private int index = 0;
        private TiltType tiltType;
        private double valveAngle;
        private PointD valvePos = new PointD();
        private PointD cameraPos = new PointD();
        private bool isDone = false;

        public PointD ValvePos => this.valvePos;
        public PointD CameraPos => this.cameraPos;
        public ValveCameraOffsetCtl()
        {
            InitializeComponent();
            this.UpdateByIndex();
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.index == 2)
            {
                
            }
            else
            {
                this.index++;
            }
            this.UpdateByIndex();
        }

        private void UpdateByIndex()
        {
            switch (this.index)
            {
                case 0:
                    this.txtMsg.Text = "用胶枪在橡皮泥上压出痕迹，完成后按下 [确认] 。";
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //TODO 根据阀倾斜姿态调整胶阀

                    Machine.Instance.Robot.MovePosU(this.valveAngle);
                    break;
                case 1:
                    this.valvePos = Machine.Instance.Robot.PosXY;
                    Machine.Instance.Robot.MoveSafeZ();
                    this.txtMsg.Text = "用相机在橡皮泥上找到压痕中心，完成后按下 [确认] 。";
                    break;
                case 2:
                    this.cameraPos = Machine.Instance.Robot.PosXY;
                    this.isDone = true;
                    break;
            }
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            this.Reset();
        }

        public void Setup(object[] objs)
        {
            this.tiltType = (TiltType)objs[0];
            this.valveAngle = (double)objs[1];
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
