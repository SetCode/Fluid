using Anda.Fluid.APP.AngleHeightPoseCorrect.TestType;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.ActiveItems;
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

namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{
    public partial class TestTypeCoreectAngleForm : Form
    {
        private ITestCtlable dispenseLineCtl;
        private ITestCtlable valveCameraOffsetCtl;
        private ITestCtlable gapMeasureCtl;
        private ITestCtlable dotValveOffsetCtl;
        private List<ITestCtlable> ctlList;
        private int index = 0;

        private AngleHeightPosOffset angleHeightPosOffset;
        #region 阶段一生成的其他阶段会用到的参数
        private double posZ;
        #endregion
        public TestTypeCoreectAngleForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.angleHeightPosOffset = new AngleHeightPosOffset();

            this.Init();
            this.UpdateByIndex();
            this.FormClosed += TestTypeCoreectAngleForm_FormClosed;
        }

        private void TestTypeCoreectAngleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //校准完之后胶阀复位
            Result res = Machine.Instance.Robot.MoveSafeZAndReply();
            if (res.IsOk)
            {
                Machine.Instance.Valve1.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU, Machine.Instance.Robot.AxisU.Prm.Acc);
            }
        }

        private void Init()
        {
            this.ctlList = new List<ITestCtlable>();
            
            //添加四个阶段控件
            this.dispenseLineCtl = new TiltTypeCtl();
            this.valveCameraOffsetCtl = new ValveCameraOffsetCtl();
            this.gapMeasureCtl = new GapMeasureCtl();
            this.dotValveOffsetCtl = new DotValveOffsetCtl();
            this.ctlList.Add(this.dispenseLineCtl);
            this.ctlList.Add(this.valveCameraOffsetCtl);
            this.ctlList.Add(this.gapMeasureCtl);
            this.ctlList.Add(this.dotValveOffsetCtl);

            this.index = 0;         
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            //如果当前阶段没有完成,则直接返回
            if (!this.ctlList[this.index].IsDone())
                return;
            //当前阶段完成，则更新校正结果
            else
            {
                this.GetResult(this.index);
            }

            //如果不是第四阶段，则前进一个阶段
            if (this.index != 3)
            {
                this.index++;
                this.UpdateByIndex();
            }
            //如果是第四阶段，则提示可以添加结果
            else 
            {
                this.btnOk.Enabled = true;
            }
        }

        private void btnPreStep_Click(object sender, EventArgs e)
        {
            if (this.index != 0)
            {
                this.index--;
                this.UpdateByIndex();
            }
        }

        private void UpdateByIndex()
        {
            this.btnOk.Enabled = false;
            this.btnContinue.Enabled = false;
            object[] objs;
            switch (index)
            {
                case 0:
                    this.ctlList[0].Setup(null);
                    this.ShowCtl(0);
                    break;
                case 1:
                    objs = new object[2];
                    objs[0] = this.angleHeightPosOffset.TiltType;
                    objs[1] = this.angleHeightPosOffset.ValveAngle;
                    this.ctlList[1].Setup(objs);
                    this.ShowCtl(1);
                    break;
                case 2:
                    objs = new object[4];
                    objs[0] = this.angleHeightPosOffset.TiltType;
                    objs[1] = (this.gapMeasureCtl as GapMeasureCtl).PosZ;
                    objs[2] = this.angleHeightPosOffset.ValveAngle;
                    objs[3] = this.angleHeightPosOffset.ValveCameraOffset;
                    this.ctlList[2].Setup(objs);
                    this.ShowCtl(2);
                    break;
                case 3:
                    objs = new object[4];
                    objs[0] = this.angleHeightPosOffset.TiltType;
                    objs[1] = this.angleHeightPosOffset.ValveAngle;
                    objs[2] = this.angleHeightPosOffset.ValveCameraOffset;
                    objs[3] = this.angleHeightPosOffset.StandardZ;
                    this.ctlList[3].Setup(objs);
                    this.ShowCtl(3);
                    break;

            }
        }

        private void ShowCtl(int ctlNo)
        {
            this.ctlList[ctlNo].Reset();
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(this.ctlList[ctlNo] as Control);
            (this.ctlList[ctlNo] as Control).Show();
        }

        private void  GetResult(int ctlNo)
        {
            switch (ctlNo)
            {
                case 0:
                    this.angleHeightPosOffset.TiltType = (this.ctlList[ctlNo] as TiltTypeCtl).TiltType;
                    this.angleHeightPosOffset.ValveAngle = (this.ctlList[ctlNo] as TiltTypeCtl).Angle;
                    this.txtPosture.Text = this.angleHeightPosOffset.TiltType.ToString();
                    this.txtAngle.Text = this.angleHeightPosOffset.ValveAngle.ToString();
                    break;
                case 1:
                    //阀组位置-相机位置
                    PointD valvePos = (this.ctlList[ctlNo] as ValveCameraOffsetCtl).ValvePos;
                    PointD cameraPos = (this.ctlList[ctlNo] as ValveCameraOffsetCtl).CameraPos;
                    this.angleHeightPosOffset.ValveCameraOffset = new PointD(valvePos.X - cameraPos.X, valvePos.Y - cameraPos.Y);
                    this.txtValveCameraOffset.Text = this.angleHeightPosOffset.ValveCameraOffset.ToString();
                    break;
                case 2:
                    double standardZ = (this.ctlList[ctlNo] as GapMeasureCtl).StandardZ;
                    this.angleHeightPosOffset.StandardZ = standardZ;
                    this.txtStandardZ.Text = this.angleHeightPosOffset.StandardZ.ToString();
                    break;
                case 3:
                    PointD dotValveOffset = (this.ctlList[ctlNo] as DotValveOffsetCtl).DotValveOffset;
                    double gap = (this.ctlList[ctlNo] as DotValveOffsetCtl).Gap;
                    this.angleHeightPosOffset.DispenseOffset = dotValveOffset;
                    this.txtDispenseValveOffset.Text = this.angleHeightPosOffset.DispenseOffset.ToString();
                    this.angleHeightPosOffset.Gap = gap;
                    this.txtGap.Text = this.angleHeightPosOffset.Gap.ToString();
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool needAdd = true;
            for (int i = 0; i < Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Count; i++)
            {
                if (Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i].TiltType == this.angleHeightPosOffset.TiltType)
                {
                    Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList[i] = this.angleHeightPosOffset;
                    needAdd = false;
                }
            }
            if (needAdd)
            {
                Machine.Instance.Robot.CalibPrm.AngleHeightPosOffsetList.Add(this.angleHeightPosOffset);
            }
            Result res = Machine.Instance.Robot.MoveSafeZAndReply();
            if (res.IsOk)
            {
                Machine.Instance.Valve1.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU,Machine.Instance.Robot.AxisU.Prm.Acc);
            }
            this.btnContinue.Enabled = true;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.index = 0;
            this.UpdateByIndex();
            this.ClearTxtValue();
            Result res = Machine.Instance.Robot.MoveSafeZAndReply();
            if (res.IsOk)
            {
                Machine.Instance.Valve1.ResetValveTilt(Machine.Instance.Robot.DefaultPrm.VelU, Machine.Instance.Robot.AxisU.Prm.Acc);
            }
        }

        private void ClearTxtValue()
        {
            this.txtAngle.Text = "";
            this.txtValveCameraOffset.Text = "";
            this.txtStandardZ.Text = "";
            this.txtGap.Text = "";
            this.txtDispenseValveOffset.Text = "";
        }
    }
}
