using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class DialogImport : FormEx
    {
        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strIndex = "Index";
        private string path = "Path";

        private int leftWidth = 0;
        private int formWidth = 0;       
        public DialogImport()
        {
            InitializeComponent();
            lngResources.Add(strIndex, strIndex);
            lngResources.Add(HeadName.Design.ToString(), HeadName.Design.ToString());
            lngResources.Add(HeadName.Comp.ToString(), HeadName.Comp.ToString());
            lngResources.Add(HeadName.X.ToString(), HeadName.X.ToString());
            lngResources.Add(HeadName.Y.ToString(), HeadName.Y.ToString());
            lngResources.Add(HeadName.Rot.ToString(), HeadName.Rot.ToString());
            lngResources.Add(HeadName.LayOut.ToString(), HeadName.LayOut.ToString());
            lngResources.Add(path, path);

            this.ReadLanguageResources();
            this.TopLevel = true;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;           

            foreach (string str in seperatorStrs)
            {
                this.cmbSeperators.Items.Add(str);
            }
            this.cmbSeperators.SelectedIndex = 0;
            this.cmbUnit.Items.Add("mm");
            this.cmbUnit.Items.Add("mil");
            this.cmbUnit.SelectedIndex = 0;

            this.ckbUnit.Checked = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.leftWidth = this.splitContainer1.Width;
            this.formWidth = this.Width;

            this.lstStdHead.Items.Add(HeadName.Design);
            this.lstStdHead.Items.Add(HeadName.Comp);
            this.lstStdHead.Items.Add(HeadName.X);
            this.lstStdHead.Items.Add(HeadName.Y);
            this.lstStdHead.Items.Add(HeadName.Rot);
            this.lstStdHead.Items.Add(HeadName.LayOut);

        }
        public DialogImport(Action loadDataEvent):this()
        {
            this.loadDataEvent = loadDataEvent;
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.SaveKeyValueToResources(this.gpbLoad.Name, this.gpbLoad.Text);
            this.SaveKeyValueToResources(this.btnLoad.Name,this.btnLoad.Text);
            this.SaveKeyValueToResources(this.ckbLoadDefault.Name, this.ckbLoadDefault.Text);

            this.SaveKeyValueToResources(this.gpbSeperator.Name, this.gpbSeperator.Text);
            this.SaveKeyValueToResources(this.btnSepeDel.Name, this.btnSepeDel.Text);
            this.SaveKeyValueToResources(this.btnSepApply.Name, this.btnSepApply.Text);

            this.SaveKeyValueToResources(this.gpbHeadSel.Name, this.gpbHeadSel.Text);
            this.SaveKeyValueToResources(this.btnApply.Name, this.btnApply.Text);
            this.SaveKeyValueToResources(this.lblLay.Name, this.lblLay.Text);
            this.SaveKeyValueToResources(this.btnSave.Name, this.btnSave.Text);
            this.SaveKeyValueToResources(this.btnDelete.Name, this.btnDelete.Text);

            this.SaveKeyValueToResources(this.btnOk.Name, this.btnOk.Text);
            this.SaveKeyValueToResources(this.btnCancel.Name, this.btnCancel.Text);

            this.SaveKeyValueToResources(strIndex, lngResources[strIndex]);
            this.SaveKeyValueToResources(HeadName.Design.ToString(), lngResources[HeadName.Design.ToString()]);
            this.SaveKeyValueToResources(HeadName.Comp.ToString(), lngResources[HeadName.Comp.ToString()]);
            this.SaveKeyValueToResources(HeadName.X.ToString(), lngResources[HeadName.X.ToString()]);
            this.SaveKeyValueToResources(HeadName.Y.ToString(), lngResources[HeadName.Y.ToString()]);
            this.SaveKeyValueToResources(HeadName.Rot.ToString(), lngResources[HeadName.Rot.ToString()]);
            this.SaveKeyValueToResources(HeadName.LayOut.ToString(), lngResources[HeadName.LayOut.ToString()]);
            this.SaveKeyValueToResources(path, lngResources[path]);
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.gpbLoad.Text = this.ReadKeyValueFromResources(this.gpbLoad.Name);
            this.btnLoad.Text = this.ReadKeyValueFromResources(this.btnLoad.Name);
            this.ckbLoadDefault.Text = this.ReadKeyValueFromResources(this.ckbLoadDefault.Name);

            this.gpbSeperator.Text = this.ReadKeyValueFromResources(this.gpbSeperator.Name);
            this.btnSepeDel.Text = this.ReadKeyValueFromResources(this.btnSepeDel.Name);
            this.btnSepApply.Text = this.ReadKeyValueFromResources(this.btnSepApply.Name);

            this.gpbHeadSel.Text = this.ReadKeyValueFromResources(this.gpbHeadSel.Name);
            this.btnApply.Text = this.ReadKeyValueFromResources(this.btnApply.Name);
            this.lblLay.Text = this.ReadKeyValueFromResources(this.lblLay.Name);
            this.btnSave.Text = this.ReadKeyValueFromResources(this.btnSave.Name);
            this.btnDelete.Text = this.ReadKeyValueFromResources(this.btnDelete.Name);

            this.btnOk.Text = this.ReadKeyValueFromResources(this.btnOk.Name);
            this.btnCancel.Text = this.ReadKeyValueFromResources(this.btnCancel.Name);

            if (this.HasLngResources())
            {
                lngResources[strIndex] = this.ReadKeyValueFromResources(strIndex);
                lngResources[HeadName.Design.ToString()] = this.ReadKeyValueFromResources(HeadName.Design.ToString());
                lngResources[HeadName.Comp.ToString()] = this.ReadKeyValueFromResources(HeadName.Comp.ToString());
                lngResources[HeadName.X.ToString()] = this.ReadKeyValueFromResources(HeadName.X.ToString());
                lngResources[HeadName.Y.ToString()] = this.ReadKeyValueFromResources(HeadName.Y.ToString());
                lngResources[HeadName.Rot.ToString()] = this.ReadKeyValueFromResources(HeadName.Rot.ToString());
                lngResources[HeadName.LayOut.ToString()] = this.ReadKeyValueFromResources(HeadName.LayOut.ToString());
                lngResources[path] = this.ReadKeyValueFromResources(path);

            }

        }
        private int selectedHeadIndex;
       
       
        private List<Head> headSelected = new List<Head>();
        private List<string> headSelNames = new List<string>();
        private HeadName standName;
        private List<char> seperators = new List<char>() { ' ','\t',','};
        private List<string> seperatorStrs = new List<string> { "space", "\\t", "," };

        private PatternInfo currPinfo;
        private Action loadDataEvent;

        private string currPath;

        private string pathHelp = "Help\\HelpImport.txt";
        private string[] textHelp;
        private void ImportEdit_Load(object sender, EventArgs e)
        {           
            this.initLswPath();
            this.lswPathLoad();
            this.lstPathColor();
            //FileUtils.ReadLines(pathHelp, out textHelp);           
            if (CADImport.Instance.GetCurPatternInfo() == null)
                return;
            this.currPinfo = CADImport.Instance.GetCurPatternInfo();
            this.UpdateTxtSeperator();
            this.showHeadslst();
            this.updateHeadSelFirst();
            this.updateCmbLayOut();

        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.currPinfo == null)
            {
                MessageBox.Show("The current pattern is null,please load trajectory firstly");
                return;
            }
            else
            {
                if (this.currPinfo.CompListStanded == null)
                    return;
                if (this.currPinfo.CompListStanded.Count <= 0)
                {
                    MessageBox.Show("please load trajectory firstly ,then select the heads");
                    return;
                }                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }               
            
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
        #region  选择head
        private void showHeadslst()
        {
            this.lstHead.Items.Clear();
            //如果heads的个数小于等于0，不能添加
            if (this.currPinfo.Heads == null || this.currPinfo.Heads.Length == 0)
            {               
                return;
            }     
            foreach (string head in currPinfo.Heads)
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
            if (this.currPinfo == null)
                return;
            if (this.currPinfo.Heads.Length <= 0)
                return;
            if (selectedHeadIndex < 0)
                return;
            string headName = this.currPinfo.Heads[selectedHeadIndex];           
            foreach (Head h in this.currPinfo.HeadSelected)
            {
                if (h.Text== headName || h.StandName == this.standName)
                {
                    string msg = String.Format("{0}或{1}已经存在了", headName, this.standName);
                    MessageBox.Show(msg);
                    return;
                }
            }
            Head head = new Head() { Text = headName, Index = this.selectedHeadIndex, StandName = this.standName };
            this.currPinfo.HeadSelected.Add(head);            
            this.UpdatelstHeadSelected();
        }
        /// <summary>
        /// 中间标准列头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void lstStdHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstStdHead.SelectedIndex!=-1)
            {
                this.standName = (HeadName)this.lstStdHead.SelectedItem;
            }
        }
        /// <summary>
        /// 刷新右边listBox 
        /// </summary>
        private void UpdatelstHeadSelected()
        {
            this.lstHeadSelected.Items.Clear();
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                this.lstHeadSelected.Items.Add(String.Format("{0}=>{1}", head.Text, head.StandName));
            }   
        }
        private void updateHeadSelFirst()
        {
            this.lstHeadSelected.Items.Clear();
            if (this.currPinfo.HeadSelected == null || this.currPinfo.HeadSelected.Count <= 0)
                return;
            //this.headSelected = this.currPinfo.HeadSelected;
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                this.lstHeadSelected.Items.Add(String.Format("{0}=>{1}", head.Text, head.StandName));
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
            //string itemName = this.lstHeadSelected.SelectedItem.ToString();
            int index = this.lstHeadSelected.SelectedIndex;
            this.currPinfo.HeadSelected.RemoveAt(index);
            //foreach (Head head in headSelected)
            //{
            //    if (head.Text==itemName)
            //    {
            //        this.headSelected.Remove(head);                   
            //        break; 
            //    }
            //}
            this.UpdatelstHeadSelected();
        }
        /// <summary>
        /// Apply button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.currPinfo == null)
                return;
            //this.currPinfo.HeadSelected = this.headSelected;
            this.currPinfo.unitFind = this.ckbUnit.Checked;
            this.currPinfo.Load();
            this.updateCmbLayOut();
            this.listViewLoadData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.currPinfo == null)
                return;
            this.currPinfo.SaveComponent();
        }
        private void updateCmbLayOut()
        {
            try
            {
                this.cmbLayout.Items.Clear();
                foreach (string layout in this.currPinfo.layOuts)
                {
                    this.cmbLayout.Items.Add(layout);
                }
                if (this.currPinfo.layOuts.Count > 0)
                    this.cmbLayout.SelectedIndex = 0;
            }
            catch
            {
                return;
            }
                   
        }

        #endregion

        #region 分隔符
        private void btnAddSep_Click(object sender, EventArgs e)
        {
            if (this.cmbSeperators.SelectedIndex < 0)
            {
                return;
            }
            int sepIndex = this.cmbSeperators.SelectedIndex;
            if (this.currPinfo == null)
                return;
            this.currPinfo.AddSeperator(this.seperators[sepIndex]);
            this.currPinfo.AddSeperatorShow(this.seperatorStrs[sepIndex]);
            this.UpdateTxtSeperator();
        }
        private void btnSepeDel_Click(object sender, EventArgs e)
        {
            if (this.cmbSeperators.SelectedIndex < 0)
            {
                return;
            }
            int sepIndex = this.cmbSeperators.SelectedIndex;
            if (this.currPinfo == null)
                return;
            this.currPinfo.DelSeperator(this.seperators[sepIndex]);
            this.currPinfo.DelSeperatorShow(this.seperatorStrs[sepIndex]);
            this.UpdateTxtSeperator();
        }
        private void UpdateTxtSeperator()
        {
            if (this.currPinfo == null)
                return;
            List<string> seperator = this.currPinfo.GetSeperatorShow();
            StringBuilder sb=new StringBuilder();            
            foreach (string sep in seperator)
            {
                sb.Append(sep+" ");
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
            if (this.currPinfo == null)
                return;
            if (this.currPinfo.GetHead() != Result.OK)
                return;
            this.showHeadslst();
            this.updateHeadSelFirst();
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {           
            this.currPinfo.SelectelayOut= this.cmbLayout.SelectedItem.ToString();
            this.currPinfo.GetLayoutComp();
            this.listViewLoadData();
        }
        #region 加载文件
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.currPath = openFileDialog.FileName;              
               
            }
            if (String.IsNullOrEmpty(currPath))
                return;
            CADImport.Instance.AddFilePath(this.currPath);
            this.lswPathLoad();
            if (this.ckbLoadDefault.Checked)
            {
                this.currPinfo = CADImport.Instance.GetPatternInfo(this.currPath);
                //this.currPinfo.GetText();
                this.currPinfo.LoadDefault();
                this.updateCmbLayOut();
                this.listViewLoadData();
            }
            else
            {
                this.currPinfo = CADImport.Instance.GetPatternInfo(this.currPath);
                this.UpdateTxtSeperator();                
            }
            this.lstPathColor();

        }
        private void btnPathDelete_Click(object sender, EventArgs e)
        {
            CADImport.Instance.RemovePatternInfo(this.currPath);
            this.lswPathLoad();
            if (CADImport.Instance.PathPatternDic.Keys.Count > 0)
            {
                this.lswPath.Items[0].Selected = true;
                this.lswPath_SelectedIndexChanged(null, null);
                this.lstPathColor();
            }
            else
            {
                this.clear();
            }
        }
        private void initLswPath()
        {
            this.lswPath.View = View.Details;
            this.lswPath.GridLines = true;
            this.lswPath.MultiSelect = false;
            this.lswPath.FullRowSelect = true;
            this.lswPath.Columns.Add(lngResources[path], this.lswPath.Width, HorizontalAlignment.Left);
            this.lswPath.BeginUpdate();
            this.lswPath.EndUpdate();
        }
        private void lswPathLoad()
        {
            this.lswPath.BeginUpdate();
            this.lswPath.Items.Clear();
            foreach (string path in CADImport.Instance.PathPatternDic.Keys)
            {
                ListViewItem item = new ListViewItem();
                item.Text = path;
                this.lswPath.Items.Add(item);
            }
            this.lswPath.EndUpdate();
            this.adjlswPathWidth();
            
        }

        private void lswPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currPath = String.Empty;
            if (this.lswPath.SelectedIndices.Count > 0)
            {
                if (this.lswPath.SelectedIndices[0] > -1)
                {
                    this.currPath = this.lswPath.SelectedItems[0].Text;
                }
                else
                {
                    return;
                }
            }
            if (String.IsNullOrEmpty(currPath))
            {
                return;
            }
            CADImport.Instance.CurrTrajPath = currPath;
            this.currPinfo = CADImport.Instance.GetCurPatternInfo();
            if (this.currPinfo == null)
            {
                MessageBox.Show("指定轨迹不存在");
                return;
            }
            this.UpdateTxtSeperator();
            this.showHeadslst();
            //this.UpdatelstHeadSelected();
            this.updateHeadSelFirst();
            this.updateCmbLayOut();
            this.listViewLoadData();
            this.lstPathColor();

        }
        private void lstPathColor()
        {

            foreach (ListViewItem item in lswPath.Items)
            {
                PatternInfo p = CADImport.Instance.GetPatternInfo(item.Text);
                if (!p.IsPatternCreated)
                {
                    item.ForeColor = Color.Red;
                }
                else
                {
                    item.ForeColor = Color.Black;
                }
                if (item.Text == this.currPath)
                {
                    item.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    item.BackColor = Color.White;
                }
            }
            
        }

        private void lswPath_MouseLeave(object sender, EventArgs e)
        {
            lstPathColor();
        }
        #endregion 加载文件
        private void initLswComponents()
        {
            lswComponents.View = View.Details;
            lswComponents.GridLines = true;
            this.lswComponents.MultiSelect = false;
            this.lswComponents.FullRowSelect = true;

            //添加列  
            lswComponents.Columns.Add(lngResources[strIndex], 30, HorizontalAlignment.Center);
            lswComponents.Columns.Add(lngResources[HeadName.Design.ToString()], 50, HorizontalAlignment.Center);
            lswComponents.Columns.Add(lngResources[HeadName.Comp.ToString()], 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add(lngResources[HeadName.X.ToString()], 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add(lngResources[HeadName.Y.ToString()], 100, HorizontalAlignment.Center);
            lswComponents.Columns.Add(lngResources[HeadName.Rot.ToString()], 80, HorizontalAlignment.Center);
            lswComponents.Columns.Add(lngResources[HeadName.LayOut.ToString()], 50, HorizontalAlignment.Center);
            lswComponents.BeginUpdate();
            lswComponents.EndUpdate();           
        }
        private void initLswComponents2()
        {
            lswComponents.View = View.Details;
            lswComponents.GridLines = true;
            this.lswComponents.MultiSelect = false;
            this.lswComponents.FullRowSelect = true;
            this.lswComponents.Columns.Clear();

            //添加列  
            lswComponents.Columns.Add(lngResources[strIndex], 50, HorizontalAlignment.Center);
            
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                if (head.StandName == HeadName.Design)
                {                    
                    lswComponents.Columns.Add(lngResources[HeadName.Design.ToString()], 60, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                if (head.StandName == HeadName.Comp)
                {
                    lswComponents.Columns.Add(lngResources[HeadName.Comp.ToString()], 80, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                if (head.StandName == HeadName.X)
                {
                    lswComponents.Columns.Add(lngResources[HeadName.X.ToString()], 20, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                if (head.StandName == HeadName.Y)
                {
                    lswComponents.Columns.Add(lngResources[HeadName.Y.ToString()], 20, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                if (head.StandName == HeadName.Rot)
                {
                    lswComponents.Columns.Add(lngResources[HeadName.Rot.ToString()], 50, HorizontalAlignment.Center);
                }
            }
            foreach (Head head in this.currPinfo.HeadSelected)
            {
                if (head.StandName == HeadName.LayOut)
                {
                    lswComponents.Columns.Add(lngResources[HeadName.LayOut.ToString()], 60, HorizontalAlignment.Center);
                }
            }            
            lswComponents.BeginUpdate();
            lswComponents.EndUpdate();
        }

       
        /// <summary>
        /// lswComponents中加载数据
        /// </summary>
        private void listViewLoadData()
        {
            this.initLswComponents2();
            PatternInfo patInfor = this.currPinfo;
            lswComponents.BeginUpdate();
            this.lswComponents.Items.Clear();
            int count = 0;
            foreach (CompProperty comp in patInfor.CompListStanded)
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();   
                foreach (ColumnHeader column in this.lswComponents.Columns)
                {
                    if (column.Text== HeadName.Design.ToString())
                    {
                        item.SubItems.Add(comp.Desig);
                        continue;
                    }
                    if (column.Text == HeadName.Comp.ToString())
                    {
                        item.SubItems.Add(comp.Comp);
                        continue;
                    }
                    if (column.Text == HeadName.X.ToString())
                    {
                        item.SubItems.Add(comp.Mid.X.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.Y.ToString())
                    {
                        item.SubItems.Add(comp.Mid.Y.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.Rot.ToString())
                    {
                        item.SubItems.Add(comp.Rotation.ToString("F3"));
                        continue;
                    }
                    if (column.Text == HeadName.LayOut.ToString())
                    {
                        item.SubItems.Add(comp.LayOut);
                        continue;
                    }
                }              
                
                lswComponents.Items.Add(item);
            }            
            lswComponents.EndUpdate();
            this.adjustColumnWidth();
        }
        /// <summary>
        /// 调整lswComponents的宽度
        /// </summary>
        private void adjustColumnWidth()
        {
            if (this.currPinfo.CompListStanded.Count > 0)
            {
                int width1 = 0;
                int width2 = 0;
                foreach (ColumnHeader item in this.lswComponents.Columns)
                {
                    width2 = item.Width;
                    item.Width = -1;
                    width1 = item.Width;
                    if (width1 < width2)
                    {
                        item.Width = width2;
                    }
                    else
                    {
                        item.Width = width1;
                    }
                }
                int columnCount = this.lswComponents.Columns.Count - 1;
                int widthAvg = (this.lswComponents.Width - this.lswComponents.Columns[0].Width) / columnCount;
                for (int i = 1; i < this.lswComponents.Columns.Count; i++)
                {
                    if (this.lswComponents.Columns[i].Width < widthAvg)
                    {
                        this.lswComponents.Columns[i].Width = widthAvg;
                    }
                }
            }
        }
        private void adjlswPathWidth()
        {
            foreach (ColumnHeader ch in this.lswPath.Columns)
            {
                ch.Width = -1;
                if (ch.Width <= this.lswPath.Width)
                {
                    ch.Width = this.lswPath.Width;
                }
            }
        }
        private void clear()
        {
            this.lstHead.Items.Clear();
            this.lstHeadSelected.Items.Clear();
            this.txtSeperators.Clear();
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currPinfo == null)
                return;
            if (this.cmbUnit.SelectedIndex > -1)
            {
                if (this.cmbUnit.SelectedItem.ToString() == "mm")
                {                   
                    this.currPinfo.UnitScale = 1;
                }
                else if (this.cmbUnit.SelectedItem.ToString() == "mil")
                {                   
                    this.currPinfo.UnitScale = 0.0254;
                }
            }
            
        }

        private  HelpForm helpFrm;
        private void btnHelp_Click(object sender, EventArgs e)
        {
            FileUtils.ReadLines(pathHelp, out textHelp);
            if (this.textHelp == null)
            {
                //MessageBox.Show("加载帮助文档失败");
                this.textHelp = new string[] { "no message"};
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in textHelp)
            {
                sb.AppendLine(item);
            }
            if (helpFrm == null || helpFrm.IsDisposed)
            {
                helpFrm = new HelpForm().SetUp(sb.ToString(), this);
                helpFrm.Show(this);
            }       
            

        }

        
    }
}
