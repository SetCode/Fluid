using Anda.Fluid.Drive;
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

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal partial class TeachMeasureHeightAndMarkZ : TeachFormBase, IClickable
    {


        public TeachMeasureHeightAndMarkZ()
        {
            InitializeComponent();
            this.txtMeasureHeight.Text = Machine.Instance.Robot.CalibPrm.MeasureHeightZ.ToString("0.000");
            this.txtMarkZ.Text = Machine.Instance.Robot.CalibPrm.MarkZ.ToString("0.000");
            this.UpdateByFlag();
            this.ReadLanguageResources();
        }
    

        private int flag = 0;

        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            this.Close();
        }

        public void DoHelp()
        {
           
        }

        public void DoNext()
        {
            flag++;
            UpdateByFlag();
        }

        public void DoPrev()
        {
            flag--;
            UpdateByFlag();
        }

        public void DoTeach()
        {
            if (this.flag == 0)
            {
                //给设置文件中的SafeZ赋值
                Machine.Instance.Robot.CalibPrm.MeasureHeightZ = Machine.Instance.Robot.PosZ;
                this.txtMeasureHeight.Text = Machine.Instance.Robot.CalibPrm.MeasureHeightZ.ToString("0.000");
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = true;
                this.btnDone.Enabled = false;
                this.btnTeach.Enabled = true;
            }
            else if (this.flag == 1)
            {
                Machine.Instance.Robot.CalibPrm.MarkZ = Machine.Instance.Robot.PosZ;
                this.txtMarkZ.Text = Machine.Instance.Robot.CalibPrm.MarkZ.ToString("0.000");
                this.btnPrev.Enabled = true;
                this.btnNext.Enabled = false;
                this.btnDone.Enabled = true;
                this.btnTeach.Enabled = true;
            }
        }
        private void UpdateByFlag()
        {
            switch (this.flag)
            {
                case 0:
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    break;
                case 1:
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = false;
                    this.btnDone.Enabled = false;
                    this.btnTeach.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        #region 测高高度
        private void btnTeachMeausreHeight_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Robot.PosZ < Machine.Instance.Robot.CalibPrm.SafeZ)
            {
                return;
            }
            //给设置文件中的SafeZ赋值
            Machine.Instance.Robot.CalibPrm.MeasureHeightZ = Machine.Instance.Robot.PosZ;
            this.txtMeasureHeight.Text = Machine.Instance.Robot.CalibPrm.MeasureHeightZ.ToString("0.000");
        }

        private void btnGotoMH_Click(object sender, EventArgs e)
        {
            //移动到测高高度
            Machine.Instance.Robot.MoveMeasureHeightZAndReply();
        }

        private async void btnStatus_Click(object sender, EventArgs e)
        {
            bool b = false;
            double value;
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.MeasureHeightBefore();
                b = Machine.Instance.Laser.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value) >= 0;
                Machine.Instance.MeasureHeightAfter();
                //Result res = Machine.Instance.MeasureHeight(out value);
                //b = res.IsOk ? true : false;
            });
            if (b)
            {
                this.txtStatus.BackColor = Color.Green;
                this.txtStatus.Text = "通讯成功";
            }
            else
            {
                this.txtStatus.BackColor = Color.Red;
                this.txtStatus.Text = "通讯失败";
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.ReadValue();
        }



        private async void ReadValue()
        {
            double value = 0;
            int rtn = 0;
            await Task.Factory.StartNew(() =>
            {                                
                Machine.Instance.MeasureHeightBefore();
                rtn = Machine.Instance.Laser.Laserable.ReadValue(new TimeSpan(0, 0, 1), out value);
                Machine.Instance.MeasureHeightAfter();
            });

            if (rtn == 0)
            {
                this.txtRead.BackColor = Color.White;
                this.txtRead.Text = value.ToString("0.000");
            }
            else if (rtn > 0)
            {
                this.txtRead.BackColor = Color.Yellow;
                this.txtRead.Text = value.ToString("0.000");
            }
            else
            {
                this.txtRead.BackColor = Color.Red;
                this.txtRead.Text = "failed";
            }
        }

        #endregion 

        #region Mark高度
        private void btnTeachMark_Click(object sender, EventArgs e)
        {
            if(Machine.Instance.Robot.PosZ<Machine.Instance.Robot.CalibPrm.SafeZ)
            {
                return;
            }
            Machine.Instance.Robot.CalibPrm.MarkZ = Machine.Instance.Robot.PosZ;
            this.txtMarkZ.Text = Machine.Instance.Robot.CalibPrm.MarkZ.ToString("0.000");
        }

        private void btnGotoMark_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveToMarkZAndReply();
        }

        #endregion
    }
}
