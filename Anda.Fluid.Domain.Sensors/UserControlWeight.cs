using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Anda.Fluid.Drive.ScaleSystem;
using Anda.Fluid.Drive;
using System.Threading;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.ValveSystem;
using Newtonsoft.Json;
using System.Reflection;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.PropertyGridExtension;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class UserControlWeight : UserControlEx
    {
        private PropertyGrid valve1Prg = new PropertyGrid();
        private PropertyGrid valve2Prg = new PropertyGrid();

        private ValveSprayResultPrm vsrPrm1;
        private ValveSprayResultPrm vsrPrm2;

        private ValveSelection valveSelect = ValveSelection.单阀;
        public UserControlWeight()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            InitialPrgs();
            //this.rdoValve1.Checked = true;
        }

        private void InitialPrgs()
        {
            this.valve1Prg.Width = 130;
            this.valve1Prg.Parent = this.pnlValve1;
            this.valve1Prg.Dock = DockStyle.Fill;
            this.valve1Prg.CategoryForeColor = Color.Black;

            this.valve1Prg.PropertySort = PropertySort.Categorized;

            this.valve2Prg.Width = 130;
            this.valve2Prg.Parent = this.pnlValve2;
            this.valve2Prg.Dock = DockStyle.Fill;
            this.valve2Prg.CategoryForeColor = Color.Black;

            this.valve2Prg.PropertySort = PropertySort.Categorized;

        }

        private void Prg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.btnVal1AutoRun.Enabled = false;

        }

        public void Setup()
        {

            this.valveSelect = Machine.Instance.Setting.ValveSelect;

            this.BindPrm();
            this.UpdateUI();
            this.RefreshPrgs();
        }
        private void UpdateUI()
        {
            if (valveSelect == ValveSelection.单阀)
            {
                this.grpValve2.Enabled = false;
            }
            else
            {
                this.grpValve2.Enabled = true;
            }
        }

        private void BindPrm()
        {
            if (Machine.Instance.Scale == null && Machine.Instance.Valve1 == null)
            {
                Log.Print("Valve1 or Scale Err ....");
                return;
            }
            vsrPrm1 = new ValveSprayResultPrm(Machine.Instance.Valve1.weightPrm);
            LngPropertyProxyTypeDescriptor proxyObj1 = new LngPropertyProxyTypeDescriptor(vsrPrm1,this.GetType().Name);
            this.valve1Prg.SelectedObject = proxyObj1;
            if (Machine.Instance.Scale == null && Machine.Instance.Valve2 == null)
            {
                Log.Print("Valve2 or Scale Err ....");
                return;
            }
            vsrPrm2 = new ValveSprayResultPrm(Machine.Instance.Valve2.weightPrm);
            LngPropertyProxyTypeDescriptor proxyObj2 = new LngPropertyProxyTypeDescriptor(vsrPrm2, this.GetType().Name);
            this.valve2Prg.SelectedObject = proxyObj2;

        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveProportyGridLngText(new ValveSprayResultPrm(Machine.Instance.Valve1.weightPrm));
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
        }

        private void RefreshPrgs()
        {
            this.valve1Prg.ExpandAllGridItems();
            this.valve1Prg.Refresh();

            this.valve2Prg.ExpandAllGridItems();
            this.valve2Prg.Refresh();
        }

        private void EnableSingleRunBtn()
        {
            this.btnVal1AutoRun.Enabled = true;
        }

        private void EnableButton(Button button = null)
        {
            this.Enabled = true;
        }
        private void DisableButton(Button button = null)
        {
            this.Enabled = false;
        }
        private void setButton(ControlCollection controls, bool on)
        {
            foreach (var control in controls)
            {
                if (control.GetType() == typeof(Button))//如果类型是button
                {
                    Button btn = control as Button;

                    btn.Enabled = on;
                }
            }
        }

        //自动运行
        private async void btnAutoRun_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            this.DisableButton();
            await Task.Factory.StartNew(() =>
            {


                if (button.Name == "btnVal1AutoRun")
                {
                    Machine.Instance.Valve1.AutoRunWeighingWithPurge();

                }
                else if (button.Name == "btnVal2AutoRun")
                {
                    Machine.Instance.Valve2.AutoRunWeighingWithPurge();

                }
            });
            this.RefreshPrgs();
            this.EnableButton();

        }

        //单步运行
        private async void SingleRun2_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                this.DisableButton(button);
                await Task.Factory.StartNew(() =>
                {
                    switch (button.Name)
                    {
                        //btnVal1Purge btnVal1Discharge btnVal1Weight
                        //btnWeight
                        case "btnVal1Purge":
                            Machine.Instance.Valve1.DoPurge();
                            break;
                        case "btnVal2Purge":
                            Machine.Instance.Valve2.DoPurge();
                            break;
                        case "btnVal1Prime":
                            Machine.Instance.Valve1.DoPrime();
                            Debug.WriteLine("btnVal1Prime");
                            break;
                        case "btnVal2Prime":
                            Machine.Instance.Valve2.DoPrime();
                            Debug.WriteLine("btnVal2Prime");
                            break;
                        case "btnVal1Weight":
                            Machine.Instance.Valve1.DoWeight();
                            break;
                        case "btnVal2Weight":
                            Machine.Instance.Valve2.DoWeight();
                            break;
                        default:
                            break;
                    }

                });
                this.RefreshPrgs();
                this.EnableButton(button);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVal1Edit_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            new SettingWeightForm().Setup(Machine.Instance.Valve1.weightPrm).ShowDialog();
            this.Enabled = true;
        }
        private void btnVal2Edit_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            new SettingWeightForm().Setup(Machine.Instance.Valve2.weightPrm).ShowDialog();
            this.Enabled = true;
        }

        //重新加载默认参数
        private void btnResetPrm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("加载默认参数，结果参数会清空！是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Machine.Instance.ResetWeightSetting();
                Machine.Instance.ResetValveWeightSettings();

                this.RefreshPrgs();
                this.EnableSingleRunBtn();
            }
        }

        private void btnSavePrm_Click(object sender, EventArgs e)
        {
            if (valveSelect == ValveSelection.单阀)
            {
                if (!Machine.Instance.Valve1.CalibWeight())
                {
                    MessageBox.Show("本次打胶称重超出标准范围，请检查！");
                }
            }
            else
            {
                if ((!Machine.Instance.Valve1.CalibWeight() || !Machine.Instance.Valve2.CalibWeight()))
                {
                    MessageBox.Show("本次打胶称重超出标准范围，请检查！");
                }
            }

            Machine.Instance.Scale.Scalable.SavePrm();            
            if (ValveWeightPrmMgr.Instance.Save())
            {
                this.EnableSingleRunBtn();
            }
            else
            {
                MessageBox.Show("保存参数失败！");

            }
        }

        private void btnScaleEdit_Click(object sender, EventArgs e)
        {
            new ScaleDialog().Setup(Machine.Instance.Scale.Scalable).ShowDialog();
        }


    }

        
    public class ValveSprayResultPrm
    {
        public ValveSprayResultPrm(ValveWeightPrm valWtPrm)
        {
            this.valWtPrm = valWtPrm;
        }


        private ValveWeightPrm valWtPrm;

        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("天平读数")]
        [Description("结果参数,天平读数（mg）")]
        [DefaultValue(0)]
       
        public double CurrentWeight { get { return this.valWtPrm.CurrentWeight; } private set { } }


        private const string CategoryResult = "\t结果参数";
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("打胶前后读数差")]
        [Description("打胶前后读数差(mg)")]
        [DefaultValue(0)]
        
        public double DifferWeight { get { return this.valWtPrm.DifferWeight; } private set { } }



        /// <summary>
        /// 单点重量
        /// </summary>
        [JsonProperty]
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("单点重量")]
        [Description("结果参数,单点重量(mg/dot)")]
        [DefaultValue(3)]
        public double SingleDotWeight
        { get { return this.valWtPrm.SingleDotWeight; } private set { } }
    }





}
