using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Drive;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.App.Common;
using System.IO;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.App.Main
{
    public partial class RunInfoControl : UserControlEx, IMsgReceiver,IMsgI18N
    {

        private string[] msgText = new string[]
        {
            "确认清楚当前数据？",
        };

        public RunInfoControl()
        {
            InitializeComponent();

            this.txtProgram.ReadOnly = true;
            this.txtBoardCount.ReadOnly = true;
            this.txtPassCount.ReadOnly = true;
            this.txtFailedCount.ReadOnly = true;
            this.txtStartDate.ReadOnly = true;
            this.txtStartTime.ReadOnly = true;
            this.txtCycleTime.ReadOnly = true;
            this.txtConveyor1Program.ReadOnly = true;
            this.txtConveyor2Program.ReadOnly = true;

            this.cbxRunMode.Items.Add(ValveRunMode.Wet);
            this.cbxRunMode.Items.Add(ValveRunMode.Dry);
            this.cbxRunMode.Items.Add(ValveRunMode.Look);
            this.cbxRunMode.SelectedIndexChanged += CbxRunMode_SelectedIndexChanged;
            this.cbxRunMode.SelectedIndex = (int)Machine.Instance.Valve1.RunMode;

            this.cbxRunMode.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;

            this.nudSetNum.Minimum = 0;
            this.nudSetNum.Maximum = 1000000;
            this.nudSetNum.ValueChanged += NudSetNum_ValueChanged;
            this.ReadLanguageResources();

            this.SetConveyorProgram();
            this.UpdateConveyorProgramSetting();
        }

        public void UpdateUI()
        {
            this.ReadLanguageResources();
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.SaveKeyValueToResources("RunModeWet", ValveRunMode.Wet.ToString());
            this.SaveKeyValueToResources("RunModeDry", ValveRunMode.Dry.ToString());
            this.SaveKeyValueToResources("RunModeLook", ValveRunMode.Look.ToString());
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
                this.cbxRunMode.Items[0] = this.ReadKeyValueFromResources("RunModeWet");
                this.cbxRunMode.Items[1] = this.ReadKeyValueFromResources("RunModeDry");
                this.cbxRunMode.Items[2] = this.ReadKeyValueFromResources("RunModeLook");
            }
            this.ReadMsgLanguageResource();
        }
        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (msgName == MsgType.IDLE)
            {
                this.Enabled = true;
                this.cbxRunMode.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
                this.nudSetNum.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
            }
            else if (msgName == MsgType.RUNNING || msgName == MsgType.BUSY || msgName == MsgType.PAUSED)
            {
                this.Enabled = false;
            }
            else if(msgName == MsgType.RUNINFO_PROGRAM || msgName == Constants.MSG_NEW_PROGRAM)
            {
                string programName = args[0] as string;
                this.txtProgram.Text = programName;
            }
            else if(msgName == MsgType.RUNINFO_START_DATETIME)
            {
                DateTime dateTime = (DateTime)args[0];
                this.txtStartDate.Text = dateTime.ToString("yyyy/MM/dd");
                this.txtStartTime.Text = dateTime.ToString("HH:mm:ss");
            }
            else if(msgName == MsgType.RUNINFO_RESULT)
            {
                int boardCount = (int)args[0];
                int failedCount = (int)args[1];
                double cycleTime = (double)args[2];
                this.SetInfo(boardCount, failedCount, cycleTime);
            }
            else if (msgName == LngMsg.SWITCH_LNG)
            {
                this.UpdateUI();
            }
            else if (msgName == MsgConstants.MODIFY_ACCESS || msgName == MsgConstants.SWITCH_USER)
            {
                this.cbxRunMode.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
                this.nudSetNum.Enabled = RoleMgr.Instance.CurrentRole.MainFormAccess.CanUseCbxRunMode;
            }
        }

        private void NudSetNum_ValueChanged(object sender, EventArgs e)
        {
            Executor.Instance.Cycle  = (int)this.nudSetNum.Value;
        }


        private void CbxRunMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cbxRunMode.SelectedIndex < 0)
            {
                return;
            }
            Machine.Instance.Valve1.RunMode = (ValveRunMode)this.cbxRunMode.SelectedIndex;
        }


        private void SetInfo(int boardCount, int failedCount, double cycleTime)
        {
            this.txtPassCount.Text = boardCount.ToString();
            this.txtFailedCount.Text = failedCount.ToString();
            this.txtBoardCount.Text = (boardCount + failedCount).ToString();
            this.txtCycleTime.Text = cycleTime.ToString();
        }

        private void SetConveyorProgram()
        {
            this.LoadConveyor1Program(Properties.Settings.Default.Conveyor1ProgramPath);
            this.LoadConveyor2Program(Properties.Settings.Default.Conveyor2ProgramPath);
        }

        private void btnConveyor1Program_Click(object sender, EventArgs e)
        {
            this.OpenAndLoadFile(0);
        }

        private void btnConveyor2Program_Click(object sender, EventArgs e)
        {
            this.OpenAndLoadFile(1);
        }

        /// <summary>
        /// 为轨道打开并加载点胶脚本程序
        /// </summary>
        /// <param name="conveyorNo"></param>
        private void OpenAndLoadFile(int conveyorNo)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "";
            ofd.Filter = "|*.flu";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (conveyorNo == 0)
                {
                    this.LoadConveyor1Program(ofd.FileName);
                }
                else
                {
                    this.LoadConveyor2Program(ofd.FileName);
                }
            }
        }

        /// <summary>
        /// 加载轨道1点胶脚本程序
        /// </summary>
        /// <param name="programPath"></param>
        private void LoadConveyor1Program(string programPath)
        {
            Executor.Instance.Conveyor1Program = this.GetProgram(programPath);
            if (Executor.Instance.Conveyor1Program != null && Executor.Instance.Conveyor1Program.Name != null)
            {
                this.txtConveyor1Program.Text = Executor.Instance.Conveyor1Program.Name;
                Properties.Settings.Default.Conveyor1ProgramPath = programPath;
            }
            else
            {
                this.txtConveyor1Program.Text = "";
            }
        }

        /// <summary>
        /// 加载轨道2点胶脚本程序
        /// </summary>
        /// <param name="programPath"></param>
        private void LoadConveyor2Program(string programPath)
        {
            Executor.Instance.Conveyor2Program = this.GetProgram(programPath);
            if (Executor.Instance.Conveyor2Program != null && Executor.Instance.Conveyor2Program.Name != null)
            {
                this.txtConveyor2Program.Text = Executor.Instance.Conveyor2Program.Name;
                Properties.Settings.Default.Conveyor2ProgramPath = programPath;
            }
            else
            {
                this.txtConveyor2Program.Text = "";
            }
        }

        /// <summary>
        /// 根据路径得到脚本程序
        /// </summary>
        /// <param name="programPath"></param>
        /// <returns></returns>
        private FluidProgram GetProgram(string programPath)
        {
            if (programPath == null)
                return null;

            Stream fstream = null;
            try
            {
                FluidProgram program = FluidProgram.GetProgram(fstream, programPath);

                //如果不在空闲时，则返回
                if (Executor.Instance.CurrProgramState != Executor.ProgramOuterState.IDLE
                    && Executor.Instance.CurrProgramState != Executor.ProgramOuterState.ABORTED)
                {
                    return null;
                }

                //进行语法检查
                Result rst = program.Parse();
                if (!rst.IsOk)
                {
                    MessageBox.Show("该程序存在语法错误，无法加载！");
                    return null;
                }            
                return program;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (fstream != null)
                {
                    fstream.Close();
                }
            }                 
        }

        private void rdoSingleProgram_CheckedChanged(object sender, EventArgs e)
        {
            Machine.Instance.Setting.DoubleProgram = false;
            Machine.Instance.Setting.Save();
            this.UpdateConveyorProgramSetting();
        }

        private void rdoDoubleProgram_CheckedChanged(object sender, EventArgs e)
        {
            Machine.Instance.Setting.DoubleProgram = true;
            Machine.Instance.Setting.Save();
            this.UpdateConveyorProgramSetting();
        }

        /// <summary>
        /// 更新轨道程序设置显示
        /// </summary>
        public void UpdateConveyorProgramSetting()
        {
            if (Machine.Instance.Setting.ConveyorSelect == ConveyorSelection.单轨)
            {
                this.rdoSingleProgram.Enabled = false;
                this.rdoDoubleProgram.Enabled = false;
                this.txtConveyor1Program.Enabled = false;
                this.txtConveyor2Program.Enabled = false;
                this.btnConveyor1Program.Enabled = false;
                this.btnConveyor2Program.Enabled = false;
            }
            else
            {
                if (this.rdoSingleProgram.Checked)
                {
                    this.rdoSingleProgram.Enabled = true;
                    this.rdoDoubleProgram.Enabled = true;
                    this.txtConveyor1Program.Enabled = false;
                    this.txtConveyor2Program.Enabled = false;
                    this.btnConveyor1Program.Enabled = false;
                    this.btnConveyor2Program.Enabled = false;
                }
                else
                {
                    this.rdoSingleProgram.Enabled = true;
                    this.rdoDoubleProgram.Enabled = true;
                    this.txtConveyor1Program.Enabled = true;
                    this.txtConveyor2Program.Enabled = true;
                    this.btnConveyor1Program.Enabled = true;
                    this.btnConveyor2Program.Enabled = true;
                }
            }
        }

        public void SaveMsgLanguageResource()
        {
            for (int i = 0; i < msgText.Length; i++)
            {
                LanguageHelper.Instance.SaveMsgLngResource(this.GetType().Name, i, msgText[i]);
            }
        }

        public void ReadMsgLanguageResource()
        {
            for (int i = 0; i < msgText.Length; i++)
            {
                string temp = LanguageHelper.Instance.ReadMsgLngResource(this.GetType().Name, i);
                if (!temp.Equals(""))
                {
                    msgText[i] = temp;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(msgText[0], "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.txtBoardCount.Text = "0";
                this.txtFailedCount.Text = "0";
                this.txtPassCount.Text = "0";
                Executor.Instance.IsClearAllCount = true;
            }
        }
    }
}
