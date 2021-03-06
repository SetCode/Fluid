﻿using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditMeasureHeightForm1 : EditFormBase, IMsgSender
    {
        private Pattern pattern;
        private bool isCreating;
        private PointD origin;
        private MeasureHeightCmdLine measureHeightCmdLine;
        private MeasureHeightCmdLine measureHeightCmdLineBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditMeasureHeightForm1()
        {
            InitializeComponent();
        }
        public EditMeasureHeightForm1(Pattern pattern) : this(pattern, null)
        {

        }

        public EditMeasureHeightForm1(Pattern pattern, MeasureHeightCmdLine measureHeightCmdLine)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (measureHeightCmdLine == null)
            {
                isCreating = true;
                this.measureHeightCmdLine = new MeasureHeightCmdLine();
                this.heightControl1.SetupFluidProgram(FluidProgram.Current);
                this.measureHeightCmdLine.Position.X = Properties.Settings.Default.laserX;
                this.measureHeightCmdLine.Position.Y = Properties.Settings.Default.laserY;
            }
            else
            {
                isCreating = false;
                this.measureHeightCmdLine = measureHeightCmdLine;
                this.heightControl1.SetupCmdLine(measureHeightCmdLine);
            }
            PointD laserMachine = this.pattern.MachineRel(this.measureHeightCmdLine.Position);
            tbX.Text = laserMachine.X.ToString("0.000");
            tbY.Text = laserMachine.Y.ToString("0.000");
            this.heightControl1.LaserControl.MeasureStarting += HeightControl1_MeasureStarting;
            if (this.measureHeightCmdLine != null)
            {
                this.measureHeightCmdLineBackUp = (MeasureHeightCmdLine)this.measureHeightCmdLine.Clone();
            }
        }

        private void HeightControl1_MeasureStarting(PointD obj)
        {
            obj.X = origin.X + tbX.Value;
            obj.Y = origin.Y + tbY.Value;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            //Machine.Instance.Robot.MovePosXY(origin.X + tbX.Value, origin.Y + tbY.Value);
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbX.Value, origin.Y + tbY.Value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbX.IsValid || !tbY.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值.");
                return;
            }
            PointD laserMap = this.pattern.SystemRel(tbX.Value, tbY.Value);
            measureHeightCmdLine.Position.X = laserMap.X;
            measureHeightCmdLine.Position.Y = laserMap.Y;
            measureHeightCmdLine.StandardHt = this.heightControl1.BoardHeight;
            measureHeightCmdLine.ToleranceMax = this.heightControl1.MaxTolerance;
            measureHeightCmdLine.ToleranceMin = this.heightControl1.MinTolerance;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, measureHeightCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, measureHeightCmdLine);
            }
            Properties.Settings.Default.laserX = measureHeightCmdLine.Position.X;
            Properties.Settings.Default.laserY = measureHeightCmdLine.Position.Y;
            if (!this.isCreating)
            {
                Close();
                if (this.measureHeightCmdLine!=null && this.measureHeightCmdLineBackUp!=null)
                {
                    CompareObj.CompareProperty(this.measureHeightCmdLine, this.measureHeightCmdLineBackUp, null, this.GetType().Name, true);
                    CompareObj.CompareField(this.measureHeightCmdLine, this.measureHeightCmdLineBackUp, null, this.GetType().Name, true);
                }
               
            }
        }
    }
}
