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
using Anda.Fluid.Domain.FluProgram.Executant.Fluider;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Domain.FluProgram.Executant.Fluider.ValveTracker.JtValveTracker;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.App.AngleHeightPoseCorrect.TestType;

namespace Anda.Fluid.APP.AngleHeightPoseCorrect.TestType
{
    public partial class TiltTypeCtl : UserControl,ITestCtlable
    {
        private LineStyle lineStyle = LineStyle.TYPE_1;
        private TiltType tiltType = TiltType.NoTilt;
        private bool isDone = false;

        /// <summary>
        /// 倾斜方向
        /// </summary>
        public TiltType TiltType => this.tiltType;

        /// <summary>
        /// Z轴角度
        /// </summary>
        public double Angle { get; private set; }


        public TiltTypeCtl()
        {
            InitializeComponent();

            this.cbxPosture.SelectedIndex = 0;
        }

        public void Setup(object[] objs)
        {
            this.isDone = false;
        }
        public void Reset()
        {
            this.isDone = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.isDone = true;
        }

        public bool IsDone()
        {
            if (this.isDone)
                return true;
            else
                return false;
        }

        private void btnChangePosture_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            //TODO 根据选择切换气缸
            switch (this.cbxPosture.SelectedIndex)
            {
                case 0:
                    this.tiltType = TiltType.NoTilt;
                    break;
                case 1:
                    this.tiltType = TiltType.LTilt;
                    break;
                case 2:
                    this.tiltType = TiltType.RTilt;
                    break;
                case 3:
                    this.tiltType = TiltType.FTilt;
                    break;
                case 4:
                    this.tiltType = TiltType.BTilt;
                    break;                                                  
            }

            this.Angle = this.txtAngle.Value;
            Machine.Instance.Robot.MovePosU(this.txtAngle.Value);
            Machine.Instance.Valve1.CurTilt = this.tiltType;
        }
    }
}
