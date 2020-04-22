using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Motion
{
    public partial class SettingAxesForm : SettingFormBase
    {
        private AxisControl axisControl;
        private Timer timer;

        public SettingAxesForm()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
            this.SetupTree();

            this.OnSaveClicked += axisControl.Save;
            this.OnResetClicked += axisControl.Reset;
            this.OnResetAllClicked += SettingAxesForm_OnResetAllClicked;

            this.ReadLanguageResources();

        }

        private void SetupTree()
        {
            this.axisControl = new AxisControl();
            this.axisControl.Dock = DockStyle.Fill;
            this.axisControl.HomeClicked += AxisControl_HomeClicked;
            this.gbxContent.Controls.Clear();
            this.gbxContent.Controls.Add(axisControl);

            bool isLoad = false;
            foreach (var item in AxisMgr.Instance.FindAll())
            {
                TreeNode node = new TreeNode(item.Name);
                node.Tag = item;
                if (!isLoad)
                {
                    this.axisControl.Setup(item);
                    isLoad = true;
                }  
                this.tvwList.Nodes.Add(node);   
            }
            this.tvwList.NodeMouseClick += TvwList_NodeMouseClick;

            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        private void AxisControl_HomeClicked(Axis obj)
        {
            AxisType type = (AxisType)obj.Key;
            switch(type)
            {
                case AxisType.X轴:
                    Machine.Instance.Robot.MoveHomeX();
                    break;
                case AxisType.Y轴:
                    Machine.Instance.Robot.MoveHomeY();
                    break;
                case AxisType.Z轴:
                    Machine.Instance.Robot.MoveHomeZ();
                    break;
                case AxisType.A轴:
                    Machine.Instance.Robot.MoveHomeA();
                    break;
                case AxisType.B轴:
                    Machine.Instance.Robot.MoveHomeB();
                    break;
                case AxisType.R轴:
                    Machine.Instance.Robot.MoveHomeR();
                    break;
                case AxisType.U轴:
                    Machine.Instance.Robot.MoveHomeU();
                    break;
                
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.axisControl?.UpdateAxis();
        }

        private void TvwList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            Axis axis = e.Node.Tag as Axis;
            this.axisControl.Setup(axis);
        }

        private void SettingAxesForm_OnResetAllClicked()
        {
            //if (MessageBox.Show(string.Format("reset all settings to default?"), "reset",
            //MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            if (MessageBox.Show(string.Format("将所有的参数设置为默认值?"), "重置",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                foreach (var item in AxisMgr.Instance.FindAll())
                {
                    MotionUtil.ResetAxisPrm(item);
                }
            }
        }

    }
}
