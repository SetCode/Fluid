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

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class ComponentEditForm : Form
    {
        private Graphics devcolor;
        private bool drawFlag = false;
        private List<int> drawLines = new List<int>();
        private List<int> drawLinesBack = new List<int>();

        private int topIndex = 0;
        private int lastIndex = 0;
        public ComponentEditForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            
        }
     
        /// <summary>
        /// ok 按钮,保存用户和标准元器件对应表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            CADImport.Instance.SaveComponentMap();
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComponentEditForm_Load(object sender, EventArgs e)
        {
            this.lstUserComps.DrawMode = DrawMode.OwnerDrawFixed;

            devcolor = lstUserComps.CreateGraphics();
            if (CADImport.Instance.TypeModeMap.Count >0)
            {
                foreach (string key in CADImport.Instance.TypeModeMap.Keys)
                {
                    this.cmbType.Items.Add(key);
                }
                this.cmbType.SelectedIndex = 0;
            }
            CADImport.Instance.GetUserComps();            
            if (CADImport.Instance.UserComps.Count > 0)
            {
                this.lstUserComps.Items.Clear();
                foreach (string comp in CADImport.Instance.UserComps)
                {
                    this.lstUserComps.Items.Add(comp);
                }
                this.lstUserComps.SelectedIndex = 0;
            }
            CADImport.Instance.LoadComponentMap();
            this.updateLstStand();
            
            this.drawLines.Clear();
            for (int i = 0; i < this.lstUserComps.Items.Count; i++)
            {
                bool foundFlag = false;
                foreach (List<string> list in CADImport.Instance.TypeModeMap.Values)
                {
                    if (list.Contains(this.lstUserComps.Items[i]))
                    {
                        foundFlag = true;
                        break;                        
                    }                    
                }
                if (foundFlag==false)
                {
                    this.drawLines.Add(i);
                }

            }
            this.drawLinesBack.AddRange(this.drawLines);
            this.updateDrawLines();
            this.drawFlag = true;
        }
        private void drawLstLine(int index)
        {
            this.drawFlag = true;
            this.lstUserComps.SelectedIndex = index;
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbModel.Items.Clear();
            if (CADImport.Instance.TypeModeMap.Count > 0)
            {
                foreach (string value in CADImport.Instance.TypeModeMap[this.cmbType.SelectedItem.ToString()])
                {
                    this.cmbModel.Items.Add(value);
                }
                this.cmbModel.SelectedIndex = 0;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.lstUserComps.SelectedIndex<0)
            {
                return;
            }
            CADImport.Instance.AddComponent(this.lstUserComps.SelectedItem.ToString(),this.cmbModel.SelectedItem.ToString());
            this.updateLstStand();

            this.updateDrawLines();            
        }
        private void updateLstStand()
        {
            this.lstStandComps.Items.Clear();
            foreach (string user in CADImport.Instance.ComponetMap.Keys)
            {
                this.lstStandComps.Items.Add(user + "=>"+CADImport.Instance.ComponetMap[user]);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lstStandComps.SelectedIndex < 0)
            {
                return;
            }
            string userComp = this.lstStandComps.SelectedItem.ToString().Split(new char[] { '=', '>' }, StringSplitOptions.RemoveEmptyEntries)[0];
            CADImport.Instance.RemoveComponent(userComp);
            this.updateLstStand();
            this.updateDrawLines();
        }

        private void updateDrawLines()
        {
            this.drawLines.Clear();
            for (int i=0;i< this.drawLinesBack.Count;i++)
            {
                if(CADImport.Instance.ComponetMap.Keys.Contains(CADImport.Instance.UserComps[this.drawLinesBack[i]]))
                {
                    continue;
                }
                this.drawLines.Add(this.drawLinesBack[i]);
            }
            this.drawFlag = true;
            this.lstUserComps.Invalidate();
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            CADImport.Instance.LoadComponentMap();
            this.updateLstStand();
            this.updateDrawLines();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CADImport.Instance.SaveComponentMap();
        }
        
        private void lstUserComps_DrawItem(object sender, DrawItemEventArgs e)
        {            
            if (this.drawFlag)
            {
                for (int i = 0; i < this.lstUserComps.Items.Count; i++)
                {
                    if (this.drawLines.Contains(i))
                    {
                        this.drawList(i, Color.OrangeRed);
                    }
                    else
                    {
                        this.drawList(i, Color.Green);
                    }


                }
                this.drawFlag = false;
            }
            if (this.lstUserComps.TopIndex!= topIndex)
            {
                this.topIndex = this.lstUserComps.TopIndex;
                for (int i = 0; i < this.lstUserComps.Items.Count; i++)
                {
                    if (this.drawLines.Contains(i))
                    {
                        this.drawList(i, Color.OrangeRed);
                    }
                    else
                    {
                        this.drawList(i, Color.Green);
                    }

                }
            }           
            return;                 
        }

        private void drawList(int index,Color color)
        {
            Color vColor = color;
            this.devcolor.FillRectangle(new SolidBrush(vColor), this.lstUserComps.GetItemRectangle(index));
            this.devcolor.DrawString(this.lstUserComps.Items[index].ToString(), this.lstUserComps.Font,
                new SolidBrush(this.lstUserComps.ForeColor), this.lstUserComps.GetItemRectangle(index));
        }
        
        private void lstUserComps_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drawList(this.lstUserComps.SelectedIndex, Color.LightYellow);
            if (this.lstUserComps.SelectedIndex != lastIndex)
            {
                if (this.drawLines.Contains(this.lastIndex))
                {
                    this.drawList(this.lastIndex, Color.OrangeRed);
                }
                else
                {
                    this.drawList(this.lastIndex, Color.Green);
                   
                }
                //this.drawFlag = true;
                //this.lstUserComps.Invalidate();
                this.lastIndex = this.lstUserComps.SelectedIndex;
            }
            
        }

        
    }
}
