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
    public partial class DialogComponentLibEdit : FormEx
    {
        private PatternInfo curPinfo;
        private ComponentLib curLib = new ComponentLib();
        private ComponentLib curUserLib = new ComponentLib();
        private ComponentEx curUserComp ;
        private ComponentEx curLibComp ;
        private bool canChange = false;
        private Technology tech = Technology.Adh;

        private string pathHelp = "Help\\HelpComponentLib.txt";
        private string[] textHelp;
        public DialogComponentLibEdit()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.rbtAdh.Checked = true;
            this.rbtSold.Checked = false;
            this.txtModel.ReadOnly = false;
            this.componentShape1.Setup(this.componentShape1.Width, this.componentShape1.Height);
            
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.SaveLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.SaveKeyValueToResources(this.gpbUsers.Name, this.gpbUsers.Text);
            this.SaveKeyValueToResources(this.btnLoad.Name, this.btnLoad.Text);
            this.SaveKeyValueToResources(this.btnNew.Name, this.btnNew.Text);

            this.SaveKeyValueToResources(this.btnMerge.Name, this.btnMerge.Text);
            this.SaveKeyValueToResources(this.btnSave.Name, this.btnSave.Text);
            this.SaveKeyValueToResources(this.btnSaveAs.Name, this.btnSaveAs.Text);
            this.SaveKeyValueToResources(this.btnok.Name, this.btnok.Text);
            this.SaveKeyValueToResources(this.btncancel.Name, this.btncancel.Text);
            this.SaveKeyValueToResources(this.toolStripStslbl1.Name, this.toolStripStslbl1.Text);
            this.SaveKeyValueToResources(this.gpbLib.Name, this.gpbLib.Text);
            this.SaveKeyValueToResources(this.btnLibLoad.Name, this.btnLibLoad.Text);
            this.SaveKeyValueToResources(this.toolStripStatusLabel1.Name, this.toolStripStatusLabel1.Text);

            this.SaveKeyValueToResources(this.gpbEdit.Name, this.gpbEdit.Text);
            this.SaveKeyValueToResources(this.lblModel.Name, this.lblModel.Text);
            this.SaveKeyValueToResources(this.btnAdd.Name, this.btnAdd.Text);
            this.SaveKeyValueToResources(this.lblWH.Name, this.lblWH.Text);
            this.SaveKeyValueToResources(this.btnDelete.Name, this.btnDelete.Text);
            this.SaveKeyValueToResources(this.rbtAdh.Name, this.rbtAdh.Text);
            this.SaveKeyValueToResources(this.rbtSold.Name, this.rbtSold.Text);

            this.SaveKeyValueToResources(this.gpbDot.Name, this.gpbDot.Text);
            this.SaveKeyValueToResources(this.lblXY.Name, this.lblXY.Text);
            this.SaveKeyValueToResources(this.btnDotAdd.Name, this.btnDotAdd.Text);
            this.SaveKeyValueToResources(this.lblWeight.Name, this.lblWeight.Text);
            this.SaveKeyValueToResources(this.btnDotDlt.Name, this.btnDotDlt.Text);           
            this.SaveKeyValueToResources(this.lblRadius.Name, this.lblRadius.Text);
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            this.gpbUsers.Text = this.ReadKeyValueFromResources(this.gpbUsers.Name);
            this.btnLoad.Text = this.ReadKeyValueFromResources(this.btnLoad.Name);
            this.btnNew.Text = this.ReadKeyValueFromResources(this.btnNew.Name);

            this.btnMerge.Text = this.ReadKeyValueFromResources(this.btnMerge.Name);
            this.btnSave.Text = this.ReadKeyValueFromResources(this.btnSave.Name);
            this.btnSaveAs.Text = this.ReadKeyValueFromResources(this.btnSaveAs.Name);
            this.btnok.Text = this.ReadKeyValueFromResources(this.btnok.Name);
            this.btncancel.Text = this.ReadKeyValueFromResources(this.btncancel.Name);
            this.toolStripStslbl1.Text = this.ReadKeyValueFromResources(this.toolStripStslbl1.Name);

            this.gpbLib.Text = this.ReadKeyValueFromResources(this.gpbLib.Name);
            this.btnLibLoad.Text = this.ReadKeyValueFromResources(this.btnLibLoad.Name);
            this.toolStripStatusLabel1.Text = this.ReadKeyValueFromResources(this.toolStripStatusLabel1.Name);

            this.gpbEdit.Text = this.ReadKeyValueFromResources(this.gpbEdit.Name);
            this.lblModel.Text = this.ReadKeyValueFromResources(this.lblModel.Name);
            this.btnAdd.Text = this.ReadKeyValueFromResources(this.btnAdd.Name);
            this.lblWH.Text = this.ReadKeyValueFromResources(this.lblWH.Name);
            this.btnDelete.Text = this.ReadKeyValueFromResources(this.btnDelete.Name);            
            this.rbtAdh.Text = this.ReadKeyValueFromResources(this.rbtAdh.Name);
            this.rbtSold.Text = this.ReadKeyValueFromResources(this.rbtSold.Name);

            this.gpbDot.Text = this.ReadKeyValueFromResources(this.gpbDot.Name);
            this.lblXY.Text = this.ReadKeyValueFromResources(this.lblXY.Name);
            this.btnDotAdd.Text = this.ReadKeyValueFromResources(this.btnDotAdd.Name);
            this.lblWeight.Text = this.ReadKeyValueFromResources(this.lblWeight.Name);
            this.btnDotDlt.Text = this.ReadKeyValueFromResources(this.btnDotDlt.Name);
            this.lblRadius.Text = this.ReadKeyValueFromResources(this.lblRadius.Name);

        }

        private void drawComponent(ComponentEx comp)
        {
            if (comp == null)
                return;
            this.componentShape1.AddRect((float)comp.component.Width, (float)comp.component.Height);

            //List<PointD> points = new List<PointD>();
            //foreach (var item in comp.component.GetPoints())
            //{
            //    points.Add(item.point);
            //}
            if (this.rbtAdh.Checked)
            {
                this.componentShape1.AddPoints(comp.component.GetPoints(Technology.Adh));
            }
            if (this.rbtSold.Checked)
            {
                this.componentShape1.AddPoints(comp.component.GetPoints(Technology.Adh));
            }
            
        
            this.componentShape1.AddOnePoint(this.dot);
            this.componentShape1.BeginPaint();

        }
        private Dictionary<string, int> lswDotLength = new Dictionary<string, int>();
        private void initialLsw()
        {
            this.lswUnEdited.View = View.Details;
            this.lswUnEdited.GridLines = true;
            this.lswUnEdited.MultiSelect = false;
            this.lswUnEdited.FullRowSelect = true;
            //添加列  
            this.lswUnEdited.Columns.Add("Index", 50, HorizontalAlignment.Center);            
            this.lswUnEdited.Columns.Add(HeadName.Comp.ToString(), 50, HorizontalAlignment.Center);
            this.lswUnEdited.Columns.Add("Width*Height",140, HorizontalAlignment.Center);
            this.lswUnEdited.Columns.Add("Tech", 50, HorizontalAlignment.Center);
            this.lswUnEdited.BeginUpdate();
            this.lswUnEdited.EndUpdate();

            this.lswLib.View = View.Details;
            this.lswLib.GridLines = true;
            this.lswLib.MultiSelect = false;
            this.lswLib.FullRowSelect = true;

            this.lswLib.Columns.Add("Index", 50, HorizontalAlignment.Left);
            this.lswLib.Columns.Add(HeadName.Comp.ToString(), 50, HorizontalAlignment.Center);
            this.lswLib.Columns.Add("Width*Height", 140, HorizontalAlignment.Center);
            this.lswLib.Columns.Add("Tech",50, HorizontalAlignment.Center);
            this.lswLib.BeginUpdate();
            this.lswLib.EndUpdate();

            this.lswDots.View = View.Details;
            this.lswDots.GridLines = true;
            this.lswDots.MultiSelect = false;
            this.lswDots.FullRowSelect = true;

            this.lswDotLength.Clear();          
            this.lswDotLength.Add("Index", 50);
            this.lswDotLength.Add("Offset", 60);
            this.lswDotLength.Add("Weight", 60);
            this.lswDotLength.Add("Radius", 65);
            this.lswDotLength.Add("WeightControl", 120);
            this.lswDots.Columns.Add("Index", 50, HorizontalAlignment.Center);
            this.lswDots.Columns.Add("Offset", 60, HorizontalAlignment.Center);
            this.lswDots.Columns.Add("Weight", 60,HorizontalAlignment.Center);
            this.lswDots.Columns.Add("Radius", 65, HorizontalAlignment.Center);
            this.lswDots.Columns.Add("WeightControl", 120, HorizontalAlignment.Center);
            
            this.lswDots.BeginUpdate();
            this.lswDots.EndUpdate();
        }

        public DialogComponentLibEdit Setup(PatternInfo pInfo)
        {
            this.curPinfo = pInfo;
            this.curUserLib = this.curPinfo.LibUser;
            return this;
        }
        public void lswUnEditedLoad()
        {
            this.lswUnEdited.BeginUpdate();
            this.lswUnEdited.Items.Clear();
            int count = 0;
            foreach (ComponentEx comp in this.curUserLib.FindAll())
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                item.SubItems.Add(comp.component.Name);
                item.SubItems.Add(comp.component.Width.ToString() + "*" + comp.component.Height.ToString());
                item.SubItems.Add(comp.component.Tech.ToString());
                this.lswUnEdited.Items.Add(item);
            }
            this.lswUnEdited.EndUpdate();
            this.lswUserShow();
            this.adjustLswUnEditWidth();
        }
        private void adjustLswUnEditWidth()
        {
            int width1 = 0;
            int widthBack = 0;
            int widthSum = 0;
            foreach (ColumnHeader item in this.lswUnEdited.Columns)
            {
                widthBack = item.Width;
                item.Width = -1;
                width1 = item.Width;
                if (width1 < widthBack)
                {
                    item.Width = widthBack;
                }
                else
                {
                    item.Width = width1;
                }
                widthSum += item.Width;
            }
            int columnCount = this.lswUnEdited.Columns.Count - 1;
            int widthEach = (this.lswUnEdited.Width - widthSum) / columnCount;
            if (widthEach >= 0)
            {
                for (int i = 1; i < this.lswUnEdited.Columns.Count; i++)
                {
                    this.lswUnEdited.Columns[i].Width += widthEach;
                }
            }

        }
        private void adjustLswLibWidth()
        {
            if (this.curLib.FindAll().Count > 0)
            {
                int width1 = 0;
                int widthBack = 0;
                int widthSum = 0;
                foreach (ColumnHeader item in this.lswLib.Columns)
                {
                    widthBack = item.Width;
                    item.Width = -1;
                    width1 = item.Width;
                    if (width1 < widthBack)
                    {
                        item.Width = widthBack;
                    }
                    else
                    {
                        item.Width = width1;
                    }
                    widthSum += item.Width;
                }
                int columnCount = this.lswLib.Columns.Count - 1;
                int widthEach = (this.lswLib.Width - widthSum) / columnCount;
                if (widthEach>=0)
                {
                    for (int i = 1; i < this.lswLib.Columns.Count; i++)
                    {
                        this.lswLib.Columns[i].Width += widthEach;
                    }
                }
                
            }           
            
        }

        public void lswLibLoad()
        {
            this.lswLib.BeginUpdate();
            this.lswLib.Items.Clear();
            int count = 0;
            foreach (ComponentEx comp in this.curLib.FindAll())
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                item.SubItems.Add(comp.component.Name);
                item.SubItems.Add(comp.component.Width.ToString()+"*"+comp.component.Height.ToString());
                item.SubItems.Add(comp.component.Tech.ToString());
                this.lswLib.Items.Add(item);
            }
            this.lswLib.EndUpdate();
            this.adjustLswLibWidth();

        }

        public void lswDotLoad(ComponentEx comp)
        {
            this.lswDots.BeginUpdate();
            this.lswDots.Items.Clear();
            int count = 0;
            foreach (GlueDot dot in comp.component.GetPoints(this.getTech()))
            {
                count++;
                ListViewItem item = new ListViewItem();
                item.Text = count.ToString();
                item.SubItems.Add("("+dot.point.X.ToString("F3")+","+dot.point.Y.ToString("F3")+")");
                item.SubItems.Add(dot.Weight.ToString("F3"));
                item.SubItems.Add(dot.Radius.ToString("F3"));
                item.SubItems.Add(dot.IsWeight.ToString());
                item.Tag = dot;
                dot.index = count;
                this.lswDots.Items.Add(item);
            }
            this.lswDots.EndUpdate();
            adjustLswDotWidth(comp);
        }
        public void adjustLswDotWidth(ComponentEx comp)
        {
            if (comp.component.GetPoints(this.getTech()).Count>0)
            {
                int width1 = 0;
                int widthBack = 0;
                int widthSum = 0;
                foreach (ColumnHeader item in this.lswDots.Columns)
                {
                    widthBack = item.Width;
                    item.Width = -1;
                    width1 = item.Width;
                    if (width1 < widthBack)
                    {
                        item.Width = widthBack;
                    }
                    else
                    {
                        item.Width = width1;
                    }
                    widthSum += item.Width;
                }
                int columnCount = this.lswDots.Columns.Count - 1;
                int widthAvg = (this.lswDots.Width - widthSum) / columnCount;
                for (int i = 1; i < this.lswDots.Columns.Count; i++)
                {
                    if (this.lswDots.Columns[i].Width < widthAvg)
                    {
                        this.lswDots.Columns[i].Width = widthAvg;
                    }
                }
                int widthEach = (this.lswUnEdited.Width - widthSum) / columnCount;
                if (widthEach >= 0)
                {
                    for (int i = 1; i < this.lswUnEdited.Columns.Count; i++)
                    {
                        this.lswUnEdited.Columns[i].Width += widthEach;
                    }
                }
            }
          
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.curUserLib.PathLib))
            {
                MessageBox.Show("Please save this current Component Library firstly,then create a new Component Library", "", MessageBoxButtons.OKCancel);
                return;
            }
            else
            {
                btnSave_Click(null, null);
            }
            this.curUserLib = new ComponentLib();
            this.toolStripStsLblName.Text = Path.GetFileName(this.curUserLib.PathLib);
            this.lswUnEditedLoad();                       
        }

        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.curUserLib.PathLib))
            {
                MessageBox.Show("Please save this current Component Library firstly,then load a new Component Library", "", MessageBoxButtons.OKCancel);
                return;
            }
            else
            {
                btnSave_Click(null, null);
            }
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.InitialDirectory = Application.StartupPath;
            openFileDlg.Filter = "LIB|*.lib";
            openFileDlg.FilterIndex = 0;
            openFileDlg.RestoreDirectory = true;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                CADImport.Instance.PathLib = openFileDlg.FileName;//完整路径

            }
            this.curUserLib = this.loadLib(CADImport.Instance.PathLib);
            this.toolStripStsLblName.Text = Path.GetFileName(this.curUserLib.PathLib);  
            this.lswUnEditedLoad();
        }


        private ComponentLib loadLib(string path)
        {
            ComponentLib lib = new ComponentLib();
            if (String.IsNullOrEmpty(path))
            {
                return lib;
            }
            lib.SetPath(path);
            lib.PathLib = path;
            lib.Load();
            return lib;
        }
        /// <summary>
        /// 将用户Lib融入到已有Lib
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMerge_Click(object sender, EventArgs e)
        {
            bool isFinded = false;
            if (string.IsNullOrEmpty(this.curLib.PathLib))
                return;
            ComponentLib lib = new ComponentLib();
            lib.Clear();
            lib.AddRange(this.curLib.FindAll());
            this.curLib.Save();
            int index = findMaxIndex(lib);
            foreach (var userComp in this.curUserLib.FindAll())
            {
                isFinded = false;
                foreach (var libComp in lib.FindAll())
                {
                    if (libComp.component.Name== userComp.component.Name)
                    {
                        isFinded = true;
                        continue;
                    }
                }
                if (isFinded==false)
                {
                    index++;
                    ComponentEx cmp = new ComponentEx(index);
                    cmp.component = userComp.component.DepCopy();
                    lib.Add(cmp);
                } 
            }
            string path =string.Format("{0}\\{1}.lib", Path.GetDirectoryName(this.curLib.GetPath()), Path.GetFileNameWithoutExtension(this.curLib.GetPath()));
            lib.SetPath(path);
            lib.PathLib = path;
            lib.Save();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.curUserLib.PathLib))
            {
                btnSaveAs_Click(null, null);
            }
            else
            {
                this.curUserLib.Save();
            }            
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog svDialog = new SaveFileDialog();
            svDialog.Filter = "LIB|*.lib";
            svDialog.FilterIndex = 0;
            svDialog.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
            svDialog.CheckPathExists = true;//检查目录 
            if (svDialog.ShowDialog() == DialogResult.OK)
            {
                CADImport.Instance.PathLib = svDialog.FileName;//完整的路径
                
            }
            if (String.IsNullOrEmpty(CADImport.Instance.PathLib))
            {
                return;
            }
            this.curUserLib.SetPath(CADImport.Instance.PathLib);
            this.curUserLib.PathLib = svDialog.FileName;
            this.toolStripStsLblName.Text = Path.GetFileName(this.curUserLib.PathLib);
            this.curUserLib.Save();
        }

        private void DialogComponentLibEdit_Load(object sender, EventArgs e)
        {
            FileUtils.ReadLines(pathHelp, out textHelp);
            this.curPinfo.GetUserComps();
            this.curPinfo.GetUserLib();
            this.initialLsw();
            this.lswUnEditedLoad();
            //this.loadLib(CADImport.Instance.PathLib);
            this.lswLibLoad();
            if(this.lswUnEdited.Items.Count>0)
                this.lswUnEdited.Items[0].Selected = true;
            this.colorListItem();       
        }


        #region Componet 
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = this.txtModel.Text;
            if (String.IsNullOrEmpty(name))
                return;
            bool isFind=checkComponent(name);
            if (isFind)
            {
                MessageBox.Show("the component is in the Lib");
                return;
            }
            int index = this.findMaxIndex(this.curUserLib);
            this.curUserComp = new ComponentEx(index+1);
            this.curUserComp.component.Name = name;
            this.compName = name;
            this.curUserComp.component.Width = (double)this.nudWidth.Value;            
            this.curUserComp.component.Height = (double)this.nudHeight.Value;            
            this.curUserLib.Add(this.curUserComp);
            this.canChange = false;
            this.lswUnEditedLoad();
            this.updateCompShow(this.curUserComp);
            this.drawComponent(this.curUserComp);
            this.canChange = true;
        }
        private bool checkComponent(string name)
        {
            foreach (ComponentEx item in this.curUserLib.FindAll())
            {
                if (item.component.Name==name)
                {
                    return true;
                }
            }
            return false;
        }
        private ComponentEx findCompByName(string name)
        {
            foreach (ComponentEx item in this.curUserLib.FindAll())
            {
                if (item.component.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = this.txtModel.Text;
            if (this.curPinfo.UserComps.Contains(name))
            {
                MessageBox.Show("can not delete the component");
                return;
            }
            ComponentEx comp = this.findCompByName(name);
            if (comp ==null)
                return;
            this.curUserLib.Remove(comp);
            if(this.curUserLib.FindAll().Count>0)
            {
                this.curUserComp = this.curUserLib.FindAll()[0];
                if (this.curUserComp.component.GetPoints(getTech()).Count > 0)
                    this.dot = this.curUserComp.component.GetPoints(getTech())[0];
            }
            this.canChange = false;
            this.updateCompShow(this.curUserComp);
            this.updateDotShow();
            this.lswUnEditedLoad();
            this.canChange = true;
        }

        private void btnCompModity_Click(object sender, EventArgs e)
        {
            if (this.curUserComp == null)
                return;
            string name = this.txtModel.Text;
            if (String.IsNullOrEmpty(name))
                return;
            if (this.curUserComp.component.Name != name)
            {
                MessageBox.Show("the component name is different,can not be modified");
                return;
            }
            this.curUserComp.component.Width = (double)this.nudWidth.Value;
            this.curUserComp.component.Height = (double)this.nudHeight.Value;
            this.curUserComp.component.Tech = this.getTech();          
            this.lswUnEditedLoad();           
            this.drawComponent(this.curUserComp);
        }
    
        private void btnClone_Click(object sender, EventArgs e)
        {           
            string name = this.txtModel.Text;
            if (String.IsNullOrEmpty(name))
                return;
            bool isFind = checkComponent(name);
            if (isFind)
            {
                MessageBox.Show("the component is in the Lib");
                return;
            }
            if (this.curUserComp == null)
            {
                return;
            }
            int index = findMaxIndex(this.curUserLib);
            ComponentEx cmp = new ComponentEx(index+1);
            cmp.component=this.curUserComp.component.DepCopy();           
            cmp.component.Name = name;
            this.curLib.Add(cmp);
            this.lswLibLoad();
            this.drawComponent(this.curUserComp);
        }
        private int findMaxIndex(ComponentLib lib)
        {
            int max = 0;
            foreach (ComponentEx item in lib.FindAll())
            {
                if (item.Key>max)
                {
                    max = item.Key;
                }
            }
            return max;
        }

        /// <summary>
        /// 修改 元器件宽 高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            if (!this.canChange)
            {
                return;
            }
            string name = this.txtModel.Text;
            if (String.IsNullOrEmpty(name))
                return;
            if (this.curUserComp.component.Name != name)
            {
                MessageBox.Show("the component name is different,can not be modified");
                return;
            }
            if (this.curUserComp != null)
            {
                this.curUserComp.component.Width = (double)this.nudWidth.Value;
                this.curUserComp.component.Height = (double)this.nudHeight.Value;
            }
            this.lswUnEditedLoad();
            //this.updateCompShow(this.curUserComp);
            this.drawComponent(this.curUserComp);
        }    

        private void nudComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isEnterClicked = true;
            }
        }

        private void nudComp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.isEnterClicked == true)
                {
                    this.isEnterClicked = false;
                    if (this.curUserComp == null)
                    {
                        return;
                    }
                    string name = this.txtModel.Text;
                    if (String.IsNullOrEmpty(name))
                        return;
                    if (this.curUserComp.component.Name != name)
                    {
                        MessageBox.Show("the component name is different,can not be modified");
                        return;
                    }
                    this.curUserComp.component.Width = (double)this.nudWidth.Value;
                    this.curUserComp.component.Height = (double)this.nudHeight.Value;
                    this.lswUnEditedLoad();                
                    this.drawComponent(this.curUserComp);
                    this.lswUserShow();

                }
            }
        }
        private void updateCompShow(ComponentEx comp)
        {
            this.txtModel.Text = comp.component.Name;
            this.nudWidth.Value = (decimal)comp.component.Width;
            this.nudHeight.Value = (decimal)comp.component.Height;            
            this.lswDotLoad(comp);

        }
        #endregion

        #region dot
        public int dotIndex = -1;
        private GlueDot dot;
        private void btnDotAdd_Click(object sender, EventArgs e)
        {
            if (this.curUserComp == null)
                return;
            GlueDot dot = new GlueDot();
            int cout = this.curUserComp.component.AdhPoints.Count;
            dot.index = cout + 1;
            dot.point = new PointD((double)this.nudOffsetX.Value, (double)this.nudOffsetY.Value);
            dot.Weight = (double)this.nudWeight.Value;
            dot.IsWeight = this.ckbWeight.Checked;
            dot.NunShots = (int)this.nudShots.Value;
            dot.Radius = (double)this.nudRadius.Value;
            this.curUserComp.component.AddPoint(dot, getTech());
            this.canChange = false;
            this.lswDotLoad(this.curUserComp);
            this.dot = dot;            
            this.updateDotShow();
            this.drawComponent(this.curUserComp);
            this.canChange = true;
        }
        private void btnDotDlt_Click(object sender, EventArgs e)
        {
            if (this.dot == null)
                return;
            this.curUserComp.component.GetPoints(getTech()).Remove(this.dot);
            this.drawComponent(this.curUserComp);
            this.lswDotLoad(this.curUserComp);
            if (this.curUserComp.component.GetPoints(getTech()).Count <= 0)
            {
                this.dot = null;
            }           
            else
            {
                this.dot = this.curUserComp.component.GetPoints(getTech())[0];
            }            
            this.canChange = false;            
            this.updateDotShow();            
            this.canChange = true;
        }
        private void btnDotModity_Click(object sender, EventArgs e)
        {
            if (this.dot == null)
                return;
            this.dot.point= new PointD((double)this.nudOffsetX.Value, (double)this.nudOffsetY.Value);
            this.dot.Weight = (double)this.nudWeight.Value;
            this.dot.IsWeight = this.ckbWeight.Checked;
            this.dot.NunShots = (int)this.nudShots.Value;
            this.dot.Radius = (double)this.nudRadius.Value;
            this.canChange = false;
            this.lswDotLoad(this.curUserComp);
            this.updateDotShow();
            this.drawComponent(this.curUserComp);
            this.canChange = true;
        }
       
        private void lswDots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lswDots.SelectedIndices.Count > 0)
            {  
                this.dot = (GlueDot)this.lswDots.SelectedItems[0].Tag;
                this.dotColor(this.lswDots.SelectedItems[0]);
                this.canChange = false;
                this.updateDotShow();
                this.drawComponent(this.curUserComp);
                this.canChange = true;
            }
        }
        private void lswDots_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lswDots.SelectedIndices.Count > 0)
            {                
                this.dot = (GlueDot)this.lswDots.SelectedItems[0].Tag;
                this.dotColor(this.lswDots.SelectedItems[0]);
                this.canChange = false;
                this.updateDotShow();
                this.drawComponent(this.curUserComp);
                this.canChange = true;
            }
        }
        private void updateDotShow()
        {
            if (this.dot == null)
            {
                return;
            }
            else
            {
                this.nudOffsetX.Value = (decimal)this.dot.point.X;
                this.nudOffsetY.Value = (decimal)this.dot.point.Y;
                this.nudWeight.Value = (decimal)this.dot.Weight;
                this.nudRadius.Value = (decimal)this.dot.Radius;
                this.nudShots.Value = (decimal)this.dot.NunShots;
                this.ckbWeight.Checked = this.dot.IsWeight;
            }
            
        }
   
        private bool isEnterClicked = false;
        private void nudDot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isEnterClicked = true;
            }

        }

        private void nudDot_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.isEnterClicked == true)
                {
                    this.isEnterClicked = false;
                    if (this.dot == null)
                        return;
                    if (this.dot.point.X != (double)this.nudOffsetX.Value)
                    {
                        this.dot.point.X = (double)this.nudOffsetX.Value;
                    }
                    if (this.dot.point.Y != (double)this.nudOffsetY.Value)
                    {
                        this.dot.point.Y = (double)this.nudOffsetY.Value;
                    }
                    if (this.dot.Weight != (double)this.nudWeight.Value)
                    {
                        this.dot.Weight = (double)this.nudWeight.Value;
                    }
                    if (this.dot.Radius != (double)this.nudRadius.Value)
                    {
                        this.dot.Radius = (double)this.nudRadius.Value;
                    }
                    if (this.dot.NunShots!=(int)this.nudShots.Value)
                    {
                        this.dot.NunShots = (int)this.nudShots.Value;
                    }
                    this.dot.IsWeight = this.ckbWeight.Checked;
                    this.lswDotLoad(this.curUserComp);
                    this.drawComponent(this.curUserComp);

                }
            }
        }
        private void ckbWeight_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.dot == null)
            //    return;
            //this.dot.IsWeight = this.ckbWeight.Checked;
            //this.lswDotLoad(this.curUserComp);
            
        }
        

        private void dotColor(ListViewItem itemSelected)
        {
            foreach (ListViewItem item in this.lswDots.Items)
            {
                item.ForeColor = Color.Black;
            }
            itemSelected.ForeColor = Color.Red;
        }
        #endregion dot

        #region LIB
        private string compName;

        /// <summary>
        /// 标准库加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLibLoad_Click(object sender, EventArgs e)
        {
            string pathLib = String.Empty;
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.InitialDirectory = Application.StartupPath;
            openFileDlg.Filter = "LIB|*.lib";
            openFileDlg.FilterIndex = 0;
            openFileDlg.RestoreDirectory = true;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                pathLib = openFileDlg.FileName;//完整路径

            }
            this.curLib = this.loadLib(pathLib);
            this.toolStripStsLblLibName.Text = Path.GetFileName(this.curLib.PathLib);
            this.lswLibLoad();
            if(this.lswLib.Items.Count>0)
                this.lswLib.Items[0].Selected = true;
        }
        private void lswLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (this.lswLib.SelectedIndices.Count > 0)
            {
                this.canChange = false;
                compName = this.lswLib.SelectedItems[0].SubItems[1].Text;
                this.txtModel.Text = compName;
                this.curLibComp = this.findComponentInLib(this.curLib,compName);
                this.curLibComp.component.Tech = this.getTech();
                this.updateCompShow(this.curLibComp);
                this.lswUserShow();
                this.drawComponent(this.curLibComp);
                this.lswLibShow();
                this.canChange = true;
            }
        }
      
        private void lswLib_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (this.lswLib.SelectedIndices.Count > 0)
            {
                this.canChange = false;
                compName = this.lswLib.SelectedItems[0].SubItems[1].Text;
                this.txtModel.Text = compName;
                this.curLibComp = this.findComponentInLib(this.curLib,compName);
                this.curLibComp.component.Tech = this.getTech();
                this.updateCompShow(this.curLibComp);
                this.lswUserShow();
                this.drawComponent(this.curLibComp);
                this.lswLibShow();
                this.canChange = true;
            }
        }
        private Technology getTech()
        {            
            if (this.rbtAdh.Checked)
            {
                return Technology.Adh;
            }
            if (this.rbtSold.Checked)
            {
                return Technology.SolderPaste;
            }
            return Technology.Adh;

        }
        private ComponentEx findComponentInLib(ComponentLib lib ,string name)
        {
            foreach (ComponentEx item in lib.FindAll())
            {
                if (item.component.Name == name)
                {
                    return item;
                }
            }
            return null;
        }


        private void lswUnEdited_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lswUnEdited.SelectedIndices.Count > 0)
            {
                this.canChange = false;
                compName = this.lswUnEdited.SelectedItems[0].SubItems[1].Text;
                this.txtModel.Text = compName;
                this.curUserComp = this.findComponentInLib(this.curUserLib, compName);
                if (this.curUserComp == null)
                    return;
                if (this.curUserComp.component.GetPoints(this.getTech()).Count > 0)
                    this.dot = this.curUserComp.component.GetPoints(getTech())[0];
                this.updateCompShow(this.curUserComp);
                this.updateDotShow();
                this.lswUserShow();
                this.drawComponent(this.curUserComp);
                this.canChange = true;
            }
        }
        private void lswUnEdited_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.lswUnEdited.SelectedIndices.Count > 0)
            {
                this.canChange = false;
                compName = this.lswUnEdited.SelectedItems[0].SubItems[1].Text;
                this.txtModel.Text = compName;
                this.curUserComp = this.findComponentInLib(this.curUserLib, compName);
                if(this.curUserComp.component.GetPoints(getTech()).Count>0)
                    this.dot = this.curUserComp.component.GetPoints(getTech())[0];
                this.updateCompShow(this.curUserComp);
                this.updateDotShow();
                this.lswUserShow();
                this.drawComponent(this.curUserComp);
                this.canChange = true;
            }
        }
    
       
        #endregion
        

        private void DialogComponentLibEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            CADImport.Instance.curLib = this.curUserLib;
        }

        private ComponentEx findCmpByName(string name)
        {
            foreach (var item in this.curUserLib.FindAll())
            {
                if (item.component.Name==name)
                {
                    return item;
                }
            }
            return null;
        }

        private void colorListItem()
        {
            foreach (ListViewItem item in this.lswUnEdited.Items)
            {
                ComponentEx cmp = findCmpByName(item.SubItems[1].Text);
                if (cmp != null && cmp.component.Width > 0 && cmp.component.Height > 0 && cmp.component.GetPoints(getTech()).Count > 0)
                {
                    item.ForeColor = Color.Black;
                }
                else
                {
                    item.ForeColor = Color.Red;
                }
            }
        }
        private void mapShow()
        {
            if (String.IsNullOrEmpty(this.txtModel.Text))
                return;
            
        }
        private void lswUserShow()
        {
            foreach (ListViewItem item in this.lswUnEdited.Items)
            {
                if (this.curUserComp!=null)
                {
                    if (item.SubItems[1].Text == this.curUserComp.component.Name)
                    {
                        item.BackColor = Color.DeepSkyBlue;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                    }
                }
               
            }
            this.colorListItem();
        }
        private void lswLibShow()
        {
            foreach (ListViewItem item in this.lswLib.Items)
            {
                if (this.curLibComp!=null)
                {
                    if (item.SubItems[1].Text == this.curLibComp.component.Name)
                    {
                        item.BackColor = Color.DeepSkyBlue;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                    }
                }
            }
        }
        
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.curUserLib.PathLib))
            {
                MessageBox.Show("Please save this current Component Library firstly,then load a new Component Library", "", MessageBoxButtons.OKCancel);
                return;
            }
            else
            {
                btnSave_Click(null, null);
            }
            if (this.isCompleted())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("元器件没有编辑完成！！！");
            }
           
            
        }
        private bool isCompleted()
        {
            bool isFinded=false;
            foreach (string name in this.curPinfo.UserComps)
            {
                isFinded = false;
                foreach (ComponentEx comp in this.curUserLib.FindAll())
                {
                    if (comp.component.Name== name && comp.component.GetPoints(getTech()).Count>0)
                    {
                        isFinded = true;
                        break;
                    }
                }
                if (isFinded == false)
                {
                    return false;
                }
                
            }
            return true;
        }
        
        
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.curLibComp == null || this.curUserComp == null)
            {
                MessageBox.Show("Please select a  component in the left and right listView control  ");
                return;
            }
                        
            string nameback = this.curUserComp.component.Name;
            this.curUserComp.component = this.curLibComp.component.DepCopy();
            this.curUserComp.component.Name = nameback;
            this.lswUnEditedLoad();
        }

        private HelpForm helpFrm;
        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (this.textHelp== null)
            {
                //MessageBox.Show("加载帮助文档失败");
                this.textHelp = new string[] { "no message" };
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
