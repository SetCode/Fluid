using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.App.Main
{

    ///<summary>
    /// Description	:主界面硬件快捷读取操作窗体：激光尺、条码枪
    /// Author  	:liyi
    /// Date		:2019/07/11
    ///</summary>   
    public partial class SensorsReadValueForm : FormEx
    {
        private string scannerTitle = "Scanner";
        private string laserTitle = "Laser";

        public int type { get; private set; }

        ///<summary>
        /// Description	:构造函数
        /// Author  	:liyi
        /// Date		:2019/07/11
        ///</summary>   
        /// <param name="type">硬件类型
        ///     0:条码枪
        ///     1：激光尺
        /// </param>
        public SensorsReadValueForm(int type)
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.type = type;
            if (this.type == 0)
            {
                this.Text = this.scannerTitle;
            }
            else if (this.type == 1)
            {
                this.Text = this.laserTitle;
            }
        }

        public SensorsReadValueForm Setup(int type)
        {
            this.type = type;
            if (this.type == 0)
            {
                this.Text = this.scannerTitle;
            }
            else if (this.type == 1)
            {
                this.Text = this.laserTitle;
            }
            return this;
        }

        private SensorsReadValueForm()
        { InitializeComponent(); }
        /// <summary>
        /// 控件语言文本读取
        /// </summary>
        /// <param name="skipButton"></param>
        /// <param name="skipRadioButton"></param>
        /// <param name="skipCheckBox"></param>
        /// <param name="skipLabel"></param>
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            string text = this.ReadKeyValueFromResources("scannerTitle");
            if (!text.Equals(""))
            {
                this.scannerTitle = text;
            }
            text = this.ReadKeyValueFromResources("laserTitle");
            if (!text.Equals(""))
            {
                this.laserTitle = text;
            }
        }
        /// <summary>
        /// 控件语言文本保存
        /// </summary>
        /// <param name="skipButton"></param>
        /// <param name="skipRadioButton"></param>
        /// <param name="skipCheckBox"></param>
        /// <param name="skipLabel"></param>
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources("scannerTitle", this.scannerTitle);
            this.SaveKeyValueToResources("laserTitle", this.laserTitle);
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private async void btnReadValue_Click(object sender, EventArgs e)
        {
            if (this.type == 0)
            {
                string res = "";
                int rtn = 0;
                if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && (Machine.Instance.Setting.ConveyorSelect == ConveyorSelection.双轨))
                {
                    await Task.Factory.StartNew(() =>
                    {
                        rtn = Machine.Instance.BarcodeSanncer2.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(1), out res);
                    });
                }
                else
                {
                    await Task.Factory.StartNew(() =>
                    {
                        rtn = Machine.Instance.BarcodeSanncer1.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(1), out res);
                    });
                }
                

                if (rtn >= 0 && !res.Equals(""))
                {

                    this.tbxValue.Text = res;
                }
                else
                {
                    this.tbxValue.Text = "读取失败";
                }
            }
            else if (this.type == 1)
            {
                double value = 0;
                int rtn = 0;
                await Task.Factory.StartNew(() =>
                {
                    Machine.Instance.MeasureHeightBefore();
                    rtn = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value);
                    if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
                    {
                        DoType.测高阀.Set(false);
                    }
                    Machine.Instance.MeasureHeightAfter();
                    //Result res = Machine.Instance.MeasureHeight(out value);
                    //rtn = res.IsOk ? 0 : -1;
                });

                if (rtn == 0)
                {
                    this.tbxValue.Text = value.ToString("0.000");
                }
                else if (rtn > 0)
                {
                    this.tbxValue.Text = value.ToString("0.000");
                }
                else
                {
                    this.tbxValue.Text = "读取失败";
                }
            }
        }
    }
}

