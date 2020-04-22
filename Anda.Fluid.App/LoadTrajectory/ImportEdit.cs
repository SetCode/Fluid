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

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class ImportEdit : Form
    {

        public ImportEdit()
        {
            InitializeComponent();
            this.TopLevel = true;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.cmbHead.Items.Add(HeadName.Design);
            this.cmbHead.Items.Add(HeadName.Comp);
            this.cmbHead.Items.Add(HeadName.X);
            this.cmbHead.Items.Add(HeadName.Y);
            this.cmbHead.Items.Add(HeadName.Rot);
            this.cmbHead.Items.Add(HeadName.LayOut);
            this.cmbHead.SelectedIndex = 0;

            foreach (string str in seperatorStrs)
            {
                this.cmbSeperators.Items.Add(str);
            }
            this.cmbSeperators.SelectedIndex = 0;
        }
        public ImportEdit(Action loadDataEvent):this()
        {
            this.loadDataEvent = loadDataEvent;
        }
        private int selectedHeadIndex;
       
        private string[] heads;
        private List<Head> headSelected = new List<Head>();
        private List<string> headSelNames = new List<string>();
        private HeadName standName;
        private List<char> seperators = new List<char>() { ' ','\t',','};
        private List<string> seperatorStrs = new List<string> { "space", "\\t", "," };

        private PatternInfo Currinfo;
        private Action loadDataEvent;
        private bool isLoadFirst;
        public ImportEdit Setup(PatternInfo pinfo)
        {
            this.Currinfo = pinfo;
            return this;
        }

        private void ImportEdit_Load(object sender, EventArgs e)
        {
            this.isLoadFirst = true;
            if (this.Currinfo == null)
            {
                MessageBox.Show("指定轨迹不存在");
                return;
            }                      
            this.UpdateTxtSeperator();
            this.showHeadslst();
            this.UpdatelstHeadSelected();
            this.updateCmbLayOut();
            this.isLoadFirst = false;
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {                      
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// cancel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region 添加head
        private void showHeadslst()
        {
            //如果heads的个数小于等于0，不能添加
            if (this.Currinfo.Heads == null || this.Currinfo.Heads.Length == 0)
            {               
                return;
            }
            this.heads = this.Currinfo.Heads;        
            foreach (string head in Currinfo.Heads)
            {
                this.lstHead.Items.Add(head);
            }
        }
        /// <summary>
        /// 左侧文件列头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstHead.SelectedIndex != -1)
            {
                selectedHeadIndex = this.lstHead.SelectedIndex;
            }

        }
        /// <summary>
        /// 添加head 将左侧listBox中的列头加载到右边listBox中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string headName = this.heads[selectedHeadIndex];           
            foreach (Head h in headSelected)
            {
                if (h.Text== headName || h.StandName == this.standName)
                {
                    string msg = String.Format("{0}或{1}已经存在了", headName, this.standName);
                    MessageBox.Show(msg);
                    return;
                }
            }
            Head head = new Head() { Text = headName, Index = this.selectedHeadIndex, StandName = this.standName };  
            this.headSelected.Add(head);            
            this.UpdatelstHeadSelected();
        }
        /// <summary>
        /// 中间标准列头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbHead.SelectedIndex >= 0)
            {
                this.standName = (HeadName)cmbHead.SelectedItem;
            }
            else
            {
                this.standName = (HeadName)cmbHead.Items[0];
            }
        }
        /// <summary>
        /// 刷新右边listBox 
        /// </summary>
        private void UpdatelstHeadSelected()
        {
            this.lstHeadSelected.Items.Clear();
            if (isLoadFirst)
            {
                if (this.Currinfo.HeadSelected == null )
                    return;
                if (this.Currinfo.HeadSelected.Count > 0)
                {
                    this.headSelected = this.Currinfo.HeadSelected;
                    foreach (Head head in headSelected)
                    {
                        this.lstHeadSelected.Items.Add(String.Format("{0}=>{1}", head.Text, head.StandName));
                    }
                }            
            }
            else
            {
                foreach (Head head in headSelected)
                {
                    this.lstHeadSelected.Items.Add(String.Format("{0}=>{1}", head.Text, head.StandName));
                }
            }         
            
        }
        
        /// <summary>
        /// 从已选择的用户head中删除指定项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lstHeadSelected.SelectedIndex<0)
            {
                return;
            }
            string itemName = this.lstHeadSelected.SelectedItem.ToString();
            foreach (Head head in headSelected)
            {
                if (head.Text==itemName)
                {
                    this.headSelected.Remove(head);                   
                    break; 
                }
            }
            this.UpdatelstHeadSelected();
        }
        /// <summary>
        /// Apply button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {            
            this.Currinfo.HeadSelected = this.headSelected;
            this.Currinfo.Load();
            this.updateCmbLayOut();
            this.loadDataEvent?.Invoke();
        }

        private void updateCmbLayOut()
        {
            try
            {
                this.cmbLayout.Items.Clear();
                foreach (string layout in this.Currinfo.layOuts)
                {
                    this.cmbLayout.Items.Add(layout);
                }
                if(this.Currinfo.layOuts.Count>0)
                    this.cmbLayout.SelectedIndex = 0;
            }
            catch
            {
                return;
            }
                   
        }
        
        #endregion 

        private void btnAddSep_Click(object sender, EventArgs e)
        {
            if (this.cmbSeperators.SelectedIndex < 0)
            {
                return;
            }
            int sepIndex = this.cmbSeperators.SelectedIndex;            
            this.Currinfo.AddSeperator(this.seperators[sepIndex]);
            this.Currinfo.AddSeperatorShow(this.seperatorStrs[sepIndex]);
            this.UpdateTxtSeperator();
        }
        private void btnSepeDel_Click(object sender, EventArgs e)
        {
            if (this.cmbSeperators.SelectedIndex < 0)
            {
                return;
            }
            int sepIndex = this.cmbSeperators.SelectedIndex;
            this.Currinfo.DelSeperator(this.seperators[sepIndex]);
            this.Currinfo.DelSeperatorShow(this.seperatorStrs[sepIndex]);
            this.UpdateTxtSeperator();
        }
        private void UpdateTxtSeperator()
        {
            List<string> seperator = this.Currinfo.GetSeperatorShow();
            StringBuilder sb=new StringBuilder();     
            foreach (string sep in seperator)
            {
                sb.Append(sep+"|");
            }
            this.txtSeperators.Text = sb.ToString();
        }

        private void cmbSeperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSeperators.SelectedIndex<0)
            {
                return;
            }
            int  sepIndex = this.cmbSeperators.SelectedIndex;
        }
        /// <summary>
        /// 分隔符确定后Apply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSepApply_Click(object sender, EventArgs e)
        {
            if (this.Currinfo.GetHead() != Result.OK)
                return;
            this.showHeadslst();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {           
            this.Currinfo.SelectelayOut= this.cmbLayout.SelectedItem.ToString();
            this.Currinfo.GetLayoutComp();
            this.loadDataEvent?.Invoke();
        }
    }
}
