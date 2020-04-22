using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.PropertyGridExtension;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class ScaleDialog : FormEx
    {
        private PropertyGrid prgScalePrm = new PropertyGrid();
        private ScalePrm prm;
        private ScalePrm prmBackUp;
        private IScalable scale;
        private double weight;
        public ScaleDialog()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.MinimizeBox = false;

            this.prgScalePrm.Width = 160;
            this.prgScalePrm.Parent = this.pnlPrm;
            this.prgScalePrm.Dock = DockStyle.Fill;
            this.prgScalePrm.CategoryForeColor = Color.Black;
            this.prgScalePrm.PropertySort = PropertySort.Categorized;
        }

        public ScaleDialog Setup(IScalable scale)
        {
            this.scale = scale;
            this.prm = scale.Prm;
            this.prmBackUp = (ScalePrm)scale.Prm.Clone();
            this.prgScalePrm.SelectedObject = this.prm;
            this.RefreshPrg();
            return this;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            //Show(string text, string caption, MessageBoxButtons buttons)
            if (MessageBox.Show("加载默认参数，结果参数会清空！是否继续？","提示",MessageBoxButtons.OKCancel) ==DialogResult.OK)
            {
                Machine.Instance.ResetWeightSetting();
                this.RefreshPrg();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {           
            scale.SavePrm();            
            this.Close();
        }

        private void RefreshPrg()
        {
            this.scale.Prm.SetReadWeight(this.weight);
            this.prgScalePrm.ExpandAllGridItems();
            this.prgScalePrm.Refresh();
        }
        //清零
        private async void btnZero_Click(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(new Action(()=> {
                this.scale.Zero();
                //this.ReadAndShow();
                Console.WriteLine("btnZero ");
                
            }));
            this.RefreshPrg();

        }
        //重新启动
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(()=> {
                this.scale.ResetScale();
                //this.ReadAndShow();
                Console.WriteLine("btnReset ");

            }));
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            new CalibrateDialog().Setup(this.scale).ShowDialog();
        }
        //外部校准
        private void button1_Click(object sender, EventArgs e)
        {
            this.scale.ExternalCali();            
            this.RefreshPrg();
        }
        //读取
        private void button2_Click(object sender, EventArgs e)
        {
            double value = this.getResult();           
            this.weight = value;
            this.RefreshPrg();
        }
        private void ReadAndShow()
        {            
            this.scale.ReadWeight(out this.weight);           
        }
        private double getResult()
        {
            //string valueStr;
            //this.scale.Print(TimeSpan.FromMilliseconds(this.scale.Prm.SingleReadTimeOut), out valueStr);            
            //double value;
            //double.TryParse(valueStr, out value);
            double value;
            this.scale.ReadWeight(out value);
            return value;
        }

        private void ScaleDialog_Load(object sender, EventArgs e)
        {
            double value = this.getResult();           
            this.weight = value;
            this.RefreshPrg();
        }

        private void ScaleDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //参数修改记录
            CompareObj.CompareProperty(scale.Prm, this.prmBackUp);
        }
    }


}
