using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Conveyor.LeadShine;

namespace Anda.Fluid.Domain.Conveyor.Forms
{

    public partial class ConveyorSettingForm : FormEx
    {
        private int conveyorCount;
        private ConveyorSettingControl conveyor1SettingControl;
        private ConveyorSettingControl conveyor2SettingControl;
        public ConveyorSettingForm(int conveyorCount)
        {
            this.conveyorCount = conveyorCount;
            this.Init();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.rdoConveyor1.Checked = true;
            this.ReadLanguageResources();
            this.UpdateUI();

            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                this.btnRTV.Visible = true;
            }
            else
            {
                this.btnRTV.Visible = false;
            }
            this.ReadLanguageResources();
        }
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private ConveyorSettingForm()
        {
            InitializeComponent();
        }
        private void Init()
        {
            this.conveyor1SettingControl = new ConveyorSettingControl();
            this.conveyor1SettingControl.SetUp(0);
            this.conveyor1SettingControl.Parent = this;
            this.conveyor1SettingControl.Location = new Point(5, 50);

            if (this.conveyorCount == 1)
            {
                this.rdoConveyor2.Enabled = false;                
            }
            else
            {
                this.conveyor2SettingControl = new ConveyorSettingControl();
                this.conveyor2SettingControl.SetUp(1);
                this.conveyor2SettingControl.Parent = this;
                this.conveyor2SettingControl.Location = new Point(5, 50);
            }
        }
        private void UpdateChecked(object sender,EventArgs e)
        {
            this.UpdateUI();
        }
        private void UpdateUI()
        {
            if (this.rdoConveyor1.Checked)
            {
                this.conveyor2SettingControl.Hide();
                this.conveyor1SettingControl.Show();
            }
            else if (this.rdoConveyor2.Checked)
            {
                this.conveyor1SettingControl.Hide();
                this.conveyor2SettingControl.Show();
            }
        }

        private void btnConnectCheck_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                if (ConveyorMachine.Instance.Enable)
                {
                    this.txtConnectCheck.BackColor = Color.Green;
                    this.txtConnectCheck.Text = "连接成功";
                }
                else
                {
                    this.txtConnectCheck.BackColor = Color.Red;
                    this.txtConnectCheck.Text = "连接失败";
                }
            }
            else
            {
                if (SensorMgr.Instance.Conveyor1.Connected)
                {
                    this.txtConnectCheck.BackColor = Color.Green;
                    this.txtConnectCheck.Text = "连接成功";
                }
                else
                {
                    this.txtConnectCheck.BackColor = Color.Red;
                    this.txtConnectCheck.Text = "连接失败";
                }
            }
        }

        private void btnRTV_Click(object sender, EventArgs e)
        {
            new RTVSettingForm().ShowDialog();
        }
    }
}
