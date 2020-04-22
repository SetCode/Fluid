using Anda.Fluid.Drive;
using Anda.Fluid.Drive.GlueManage;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.GlueManage
{
    public partial class GlueManageForm : FormEx
    {

        private GlueManagePrm[] prm = new GlueManagePrm[2] { new GlueManagePrm(0), new GlueManagePrm(1) };
        public GlueManageForm()
        {
            InitializeComponent();
            this.Load += GlueManageForm_Load;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void GlueManageForm_Load(object sender, EventArgs e)
        {
            this.cbxValve.Items.Add(ValveType.Valve1);
            this.cbxValve.Items.Add(ValveType.Valve2);
            this.cbxValve.SelectedIndex = 0;
            this.cbxManageType.Items.Add("本地管控");
            this.cbxManageType.Items.Add("联网管控");
            this.cbxManageType.SelectedIndex = 0;
            GlueManagePrm tempPrm = null;
            for (int i = 0; i < GlueManagePrmMgr.Instance.Count; i++)
            {
                tempPrm = GlueManagePrmMgr.Instance.FindBy(i);
                if (tempPrm != null)
                {
                    prm[i] = (GlueManagePrm)tempPrm.Clone();
                }
            }
            this.UpdateUI();
            this.UpdateFormData(0);
            this.cbxValve.SelectedIndexChanged += CbxValve_SelectedIndexChanged;
            this.tbSNLength.TextChanged += TbSNLength_TextChanged;
            this.cbUseGlueManage.CheckedChanged += CbUseGlueManage_CheckedChanged;
            this.ReadLanguageResources();
            this.tbDeliverTime.Enabled = false;
            this.tbGlueSN.Enabled = false;
            this.cbxValve.Enabled = true;
        }

        private void CbUseGlueManage_CheckedChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].UseGlueManage = this.cbUseGlueManage.Checked;
            this.UpdateUI();
        }

        private void TbSNLength_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueSNLength = this.tbSNLength.Value;
            return;
        }

        private void CbxValve_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateFormData(this.cbxValve.SelectedIndex);
            this.UpdateUI();
        }
        /// <summary>
        /// 主副阀胶水参数切换
        /// </summary>
        /// <param name="key"></param>
        private void UpdateFormData(int key)
        {
            if (key > 1)
            {
                key = 0;
            }
            this.cbUseGlueManage.Checked = prm[key].UseGlueManage;
            this.cbxManageType.SelectedIndex = prm[key].ManageType;
            this.tbTotalWeight.Text = prm[key].TotalWeight.ToString("0.00");
            this.tbRemainWeight.Text = prm[key].RemainWeight.ToString("0.00");
            this.tbWarnWeight.Text = prm[key].WarningPercentage.ToString("0.0");
            this.tbGlueLife.Text = (prm[key].GlueLife / 60.0).ToString("0.00");
            this.tbRemainLife.Text = (prm[key].GlueRemainLife / 60.0).ToString("0.00");
            this.tbThawTime.Text = (prm[key].GlueThawTime / 60.0).ToString("0.00");
            this.tbLifeWarnTime.Text = (prm[key].LifeWarningTime / 60.0).ToString("0.00");
            this.tbDeliverTime.Text = prm[key].GlueDeliverTime.ToString();
            this.tbSNLength.Text = prm[key].GlueSNLength.ToString();
            this.tbGlueType.Text = prm[key].GlueType;
            this.tbGlueSN.Text = prm[key].GlueSN;
            this.cbTypeCheck.Checked = prm[key].UseGlueType;
        }
        /// <summary>
        /// 控件状态更新
        /// </summary>
        private void UpdateUI()
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            this.cbUseGlueManage.Checked = prm[key].UseGlueManage;
            if (!prm[key].UseGlueManage)
            {
                UpdateAllPrmControl(false);
            }
            else
            {
                UpdateAllPrmControl(true);
            }
            
            this.tbGlueType.Enabled = prm[key].UseGlueType && prm[key].UseGlueManage;
            this.btnQueryTime.Enabled = prm[key].ManageType == 1 && prm[key].UseGlueManage;
        }

        private void UpdateAllPrmControl(bool enable)
        {
            this.cbxManageType.Enabled = enable;
            this.tbTotalWeight.Enabled = enable;
            this.tbRemainWeight.Enabled = enable;
            this.tbWarnWeight.Enabled = enable;
            this.tbGlueLife.Enabled = enable;
            this.tbRemainLife.Enabled = enable;
            this.tbThawTime.Enabled = enable;
            this.tbLifeWarnTime.Enabled = enable;
            this.tbGlueType.Enabled = enable;
            this.tbSNLength.Enabled = enable;
            this.tbGlueSN.Enabled = enable;
            this.btnChangeGlue.Enabled = enable;
            this.btnWarnCancel.Enabled = enable;
            this.btnReScan.Enabled = enable;
        }
        /// <summary>
        /// 简单检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (GlueManagerMgr.Instance.IsChangeGlue && prm[key].UseGlueManage)
            {
                if (this.tbGlueSN.Text == "")
                {
                    MessageBox.Show(this, "胶水SN为空，请检查");
                    return;
                }
                if (prm[key].GlueSN.Length != prm[key].GlueSNLength)
                {
                    MessageBox.Show(this, "胶水SN长度不符，请检查");
                    return;
                }
                if (prm[key].TotalWeight <= 0)
                {
                    MessageBox.Show(this, "请输入正常胶水总重量");
                    return;
                }
                if (prm[key].UseGlueType)
                {
                    if (this.tbGlueType.Text == "")
                    {
                        MessageBox.Show(this, "胶水类型为空，请检查");
                        return;
                    }
                    if (!prm[key].GlueSN.Contains(prm[key].GlueType))
                    {
                        MessageBox.Show(this, "胶水型号错误");
                        return;
                    }
                }
                Log.Dprint("glue change success，SN : " + prm[key].GlueSN);
                GlueManagerMgr.Instance.IsChangeGlue = false;
                this.btnCancel.Enabled = true;
                this.cbxValve.Enabled = true;
            }
            GlueManagePrmMgr.Instance.Add((GlueManagePrm)prm[key].Clone());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 如果正在换胶水，不允许关闭窗口
            if (GlueManagerMgr.Instance.IsChangeGlue)
            {
                return;
            }
            this.Close();
        }

        private void tbTotalWeight_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].TotalWeight = this.tbTotalWeight.Value;
        }

        private void tbRemainWeight_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].RemainWeight = this.tbRemainWeight.Value;
        }

        private void tbWarnWeight_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].WarningPercentage = this.tbWarnWeight.Value;
        }

        private void tbGlueLife_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueLife = this.tbGlueLife.Value * 60.0;
        }

        private void tbRemainLife_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueRemainLife = this.tbRemainLife.Value * 60.0;
        }

        private void tbThawTime_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueThawTime = this.tbThawTime.Value * 60.0;
        }

        private void tbLifeWarnTime_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].LifeWarningTime = this.tbLifeWarnTime.Value * 60.0;
        }

        private void tbGlueType_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueType = this.tbGlueType.Text.Trim().ToUpper();
        }

        private void tbGlueSN_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueSN = this.tbGlueSN.Text.Trim().ToUpper();
        }

        private void btnChangeGlue_Click(object sender, EventArgs e)
        {
            // todo 检测机器是否在运行
            if (Machine.Instance.IsProducting)
            {
                MessageBox.Show(this, "检测到机器正在运行中.请先停止!");
                return;
            }
            string strMsg = "胶阀1 确定是否执行开始换胶？";
            if (this.cbxValve.SelectedIndex != 0)
            {
                strMsg = "胶阀1 确定是否执行开始换胶？";
            }
            if (MessageBox.Show(this, strMsg, "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            GlueManagerMgr.Instance.IsChangeGlue = true;
            this.tbGlueSN.Enabled = true;
            int key = this.cbxValve.SelectedIndex;
            prm[key].TotalWeight = this.tbTotalWeight.Value;
            this.tbRemainWeight.Text = this.tbTotalWeight.Text;
            prm[key].RemainWeight = this.tbRemainWeight.Value;
            prm[key].GlueLife = this.tbGlueLife.Value * 60;
            this.tbRemainLife.Text = this.tbGlueLife.Text;
            prm[key].GlueRemainLife = this.tbRemainLife.Value * 60;
            if (this.cbxManageType.SelectedIndex == 0)// 本地管控使用当前系统时间
            {
                prm[key].GlueDeliverTime = DateTime.Now;
                this.tbDeliverTime.Text = prm[key].GlueDeliverTime.ToString();
            }
            this.tbGlueSN.Text = "";
            this.tbGlueSN.Focus();
            //开始换胶后不允许取消和切换胶阀
            this.btnCancel.Enabled = false;
            this.cbxValve.Enabled = false;
        }

        private void btnWarnCancel_Click(object sender, EventArgs e)
        {
            //关闭预警信息
        }

        private void tbSNLength_TextChanged(object sender, EventArgs e)
        {
            int key = this.cbxValve.SelectedIndex;
            if (key > 1 || key < 0)
            {
                key = 0;
            }
            prm[key].GlueSNLength = this.tbSNLength.Value;
        }
    }
}
