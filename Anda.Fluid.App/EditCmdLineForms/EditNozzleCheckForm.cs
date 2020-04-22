using Anda.Fluid.App.Common;
using Anda.Fluid.App.EditMark;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditNozzleCheckForm : EditFormBase,IMsgSender
    {
        private Pattern pattern;
        private PointD origin;
        private bool isCreating;
        private NozzleCheckCmdLine nozzleCheckCmdLine;
        private NozzleCheckCmdLine nozzleCheckCmdLineBackUp;
        /// <summary>
        /// 图像模板是否已经成功创建
        /// </summary>
        private bool modelIsCreated = false;

        #region 语言提示文本
        //private string msgModelIsNull = "image model is not create";
        private string msgModelIsNull = "没有创建图像模板";
        #endregion

        /// <summary>
        /// 只用于语言文件生成
        /// </summary>
        private EditNozzleCheckForm()
        {
            InitializeComponent();
        }

        public EditNozzleCheckForm(Pattern pattern) : this(pattern,null){}

        public EditNozzleCheckForm(Pattern pattern,NozzleCheckCmdLine nozzleCheckCmdLine) : base(pattern.GetOriginPos())
        {
            InitializeComponent();
            this.cbWeightControl.CheckedChanged += CbWeightControl_CheckedChanged;
            this.pattern = pattern;
            this.origin = pattern.GetOriginPos();
            if (nozzleCheckCmdLine == null)
            {
                isCreating = true;
                this.nozzleCheckCmdLine = new NozzleCheckCmdLine();
                this.nozzleCheckCmdLine.Position.X = Properties.Settings.Default.DotX;
                this.nozzleCheckCmdLine.Position.Y = Properties.Settings.Default.DotY;
                this.nozzleCheckCmdLine.DotStyle = (DotStyle)Properties.Settings.Default.DotStyle;
                this.nozzleCheckCmdLine.IsWeightControl = Properties.Settings.Default.DotIsWt;
                this.nozzleCheckCmdLine.Weight = Properties.Settings.Default.DotWt;
                this.nozzleCheckCmdLine.CheckThm = CheckThm.GrayScale;
                this.nozzleCheckCmdLine.IsOkAlarm = false;
                this.modelIsCreated = false;
            }
            else
            {
                isCreating = false;
                this.nozzleCheckCmdLine = nozzleCheckCmdLine;
                this.modelIsCreated = true;
            }
            //系统坐标->机械坐标
            PointD p = this.pattern.MachineRel(this.nozzleCheckCmdLine.Position);
            tbLocationX.Text = p.X.ToString("0.000");
            tbLocationY.Text = p.Y.ToString("0.000");
            for (int i = 0; i < 10; i++)
            {
                comboBoxDotType.Items.Add("Type " + (i + 1));
            }
            comboBoxDotType.SelectedIndex = (int)this.nozzleCheckCmdLine.DotStyle;
            cbWeightControl.Checked = this.nozzleCheckCmdLine.IsWeightControl;
            cbxIsGlobal.Checked = this.nozzleCheckCmdLine.isGlobal;
            tbWeight.Text = this.nozzleCheckCmdLine.Weight.ToString("0.000");
            if(this.nozzleCheckCmdLine.CheckThm == CheckThm.GrayScale)
            {
                this.rbnGrayScale.Checked = true;
            }
            else
            {
                this.rbnModelFind.Checked = true;
            }
            if(this.nozzleCheckCmdLine.IsOkAlarm)
            {
                this.rbnOkAlarm.Checked = true;
            }
            else
            {
                this.rbnNgAlarm.Checked = true;
            }
            if (this.nozzleCheckCmdLine != null)
            {
                this.nozzleCheckCmdLineBackUp = (NozzleCheckCmdLine)this.nozzleCheckCmdLine.Clone();
            }
            this.ReadLanguageResources();
        }

        private void CbWeightControl_CheckedChanged(object sender, EventArgs e)
        {
            this.tbWeight.Enabled = this.cbWeightControl.Checked;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbLocationX.Text = (Machine.Instance.Robot.PosX - origin.X).ToString("0.000");
            tbLocationY.Text = (Machine.Instance.Robot.PosY - origin.Y).ToString("0.000");
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZAndReply();
            Machine.Instance.Robot.ManualMovePosXY(origin.X + tbLocationX.Value, origin.Y + tbLocationY.Value);
        }

        private void btnEditDotParams_Click(object sender, EventArgs e)
        {
            new EditDotParamsForm(FluidProgram.Current.ProgramSettings.DotParamList).ShowDialog();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbLocationX.IsValid || !tbLocationY.IsValid || !tbWeight.IsValid)
            {
                //MessageBox.Show("Please input valid values.");
                MessageBox.Show("请输入正确的值");
                //"请输入正确的值"
                return;
            }
            if (this.nozzleCheckCmdLine.ModelFindPrm == null || !this.modelIsCreated)
            {
                //MessageBox.Show(msgModelIsNull);
                MessageBox.Show(msgModelIsNull);
                return;
            }
            //机械坐标->系统坐标
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            nozzleCheckCmdLine.Position.X = p.X;
            nozzleCheckCmdLine.Position.Y = p.Y;
            nozzleCheckCmdLine.DotStyle = (DotStyle)comboBoxDotType.SelectedIndex;
            nozzleCheckCmdLine.IsWeightControl = cbWeightControl.Checked;
            nozzleCheckCmdLine.Weight = tbWeight.Value;
            nozzleCheckCmdLine.isGlobal = cbxIsGlobal.Checked;
            if(this.rbnGrayScale.Checked)
            {
                nozzleCheckCmdLine.CheckThm = CheckThm.GrayScale;
            }
            else if(this.rbnModelFind.Checked)
            {
                nozzleCheckCmdLine.CheckThm = CheckThm.ModelFind;
            }
            nozzleCheckCmdLine.IsOkAlarm = this.rbnOkAlarm.Checked ? true : false;
            if (isCreating)
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, nozzleCheckCmdLine);
            }
            else
            {
                MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, nozzleCheckCmdLine);
            }
            Properties.Settings.Default.DotX = nozzleCheckCmdLine.Position.X;
            Properties.Settings.Default.DotY = nozzleCheckCmdLine.Position.Y;
            Properties.Settings.Default.DotStyle = (int)nozzleCheckCmdLine.DotStyle;
            Properties.Settings.Default.DotIsWt = nozzleCheckCmdLine.IsWeightControl;
            Properties.Settings.Default.DotWt = nozzleCheckCmdLine.Weight;
            if (!this.isCreating)
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            if (this.nozzleCheckCmdLine!=null && this.nozzleCheckCmdLineBackUp!=null)
            {
                CompareObj.CompareMember(this.nozzleCheckCmdLine, this.nozzleCheckCmdLineBackUp, null, this.GetType().Name, true);
            }
            
        }

        private void btnModelEdit_Click(object sender, EventArgs e)
        {
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            Machine.Instance.Robot.MoveSafeZAndReply();
            PointD pos = (pattern.GetOriginSys() + p).ToMachine();
            if(rbnGrayScale.Checked)
            {
                if(this.nozzleCheckCmdLine.GrayCheckPrm.IsCreated)
                {
                    Machine.Instance.Robot.ManualMovePosXY(pos);
                }
                if(new EditGrayCheckForm(this.nozzleCheckCmdLine.GrayCheckPrm, pattern).ShowDialog() == DialogResult.OK)
                {
                    this.modelIsCreated = true;
                }
            }
            else if(rbnModelFind.Checked)
            {
                if (this.nozzleCheckCmdLine.ModelFindPrm.ModelId != 0)
                {
                    Machine.Instance.Robot.ManualMovePosXY(pos);
                }
                if (new EditModelFindForm(this.nozzleCheckCmdLine.ModelFindPrm, pattern).ShowDialog() == DialogResult.OK)
                {
                    this.modelIsCreated = true;
                }
            }
        }

        private void btnAutoDispense_Click(object sender, EventArgs e)
        {
            Machine.Instance.Light.None();
            PointD p = this.pattern.SystemRel(tbLocationX.Value, tbLocationY.Value);
            Machine.Instance.Robot.MoveSafeZAndReply();
            PointD pos = (pattern.GetOriginSys() + p).ToMachine();
            DotParam param = FluidProgram.Current.ProgramSettings.DotParamList[comboBoxDotType.SelectedIndex];
            Result ret = Result.OK;
            double targZ;
            if (Machine.Instance.Laser.Laserable.Vendor == Drive.Sensors.HeightMeasure.Laser.Vendor.Disable)
            {
                targZ = FluidProgram.Current.RuntimeSettings.BoardZValue + param.DispenseGap;
            }
            else
            {
                double curMeasureHeightValue = 0;
                //到对应点位测高
                //ret = Machine.Instance.Robot.MovePosXYAndReply(pos.ToLaser());
                ret = Machine.Instance.Robot.ManualMovePosXYAndReply(pos.ToLaser());
                if (!ret.IsOk)
                {
                    return;
                }
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(true);
                //}
                Machine.Instance.MeasureHeightBefore();
                ////激光尺读数
                Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 2), out curMeasureHeightValue);
                //if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                //{
                //    DoType.测高阀.Set(false);
                //}
                //Result res = Machine.Instance.MeasureHeight(out curMeasureHeightValue);
                Machine.Instance.MeasureHeightAfter();

                if (curMeasureHeightValue == 0)
                {
                    return;
                }
                targZ = Converter.NeedleBoard2Z(param.DispenseGap, curMeasureHeightValue);
            }

            // 喷嘴移动到指定位置
            //ret = Machine.Instance.Robot.MovePosXYAndReply(pos.ToNeedle(Drive.ValveSystem.ValveType.Valve1));
            ret = Machine.Instance.Robot.ManualMovePosXYAndReply(pos.ToNeedle(Drive.ValveSystem.ValveType.Valve1));
            if (!ret.IsOk)
            {
                return;
            }
            // 下降z轴
            ret = Machine.Instance.Robot.MovePosZByToleranceAndReply(targZ, param.DownSpeed, param.DownAccel);
            if (!ret.IsOk)
            {
                return;
            }
            // 开胶
            Machine.Instance.Valve1.SprayOneAndWait((int)this.nudSprayTime.Value);
            //回到安全高度
            Machine.Instance.Robot.MoveSafeZAndReply();
            if (!ret.IsOk)
            {
                return;
            }
            //相机到点胶位置
            //ret = Machine.Instance.Robot.MovePosXYAndReply(pos);
            ret = Machine.Instance.Robot.ManualMovePosXYAndReply(pos);
            if (!ret.IsOk)
            {
                return;
            }
            Machine.Instance.Light.ResetToLast();
        }
    }
}
