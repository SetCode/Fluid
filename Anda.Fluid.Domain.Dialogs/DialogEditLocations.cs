using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.Locations;
using Anda.Fluid.Drive.ValveSystem;
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

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogEditLocations : DialogBase, IOptional
    {
        private string selectedKey = string.Empty;
        private Location selectedLoc = null;
        private Location selectedLocBackUp = null;
        public DialogEditLocations()
        {
            InitializeComponent();
            this.GbxOption.Enabled = false;
            this.btnTeachLoc.Click += BtnTeachLoc_Click;
            this.btnGo.Click += BtnGo_Click;
            this.btnNew.Click += BtnNew_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            this.ReadLanguageResources();
            this.rdoTeachXY.Checked = true;
            this.rdoValve1Goto.Checked = true;
           
        }

        public void DoCancel()
        {
            throw new NotImplementedException();
        }

        public void DoDone()
        {
            throw new NotImplementedException();
        }

        public void DoNext()
        {
            throw new NotImplementedException();
        }

        public void DoPrev()
        {
            throw new NotImplementedException();
        }

        public void DoTeach()
        {
            throw new NotImplementedException();
        }

        private void BtnTeachLoc_Click(object sender, EventArgs e)
        {
            if (this.selectedLoc == null)
            {
                return;
            }
            if (this.selectedLoc.IsSystemLoc)
            {
                return;
            }
            if (this.rdoTeachXY.Checked)
            {
                this.selectedLoc.X = Machine.Instance.Robot.PosX;
                this.selectedLoc.Y = Machine.Instance.Robot.PosY;
            }
            if (this.rdoTeachZ.Checked)
            {
                this.selectedLoc.Z = Machine.Instance.Robot.PosZ;
            }

            LocationMgr.Instance.Save();
            
            this.UpdateSelectedLocText();
            if (this.selectedLoc!=null && this.selectedLocBackUp!=null)
            {
                CompareObj.CompareProperty(this.selectedLoc, this.selectedLocBackUp, null, this.GetType().Name);
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedKey = this.listBox1.SelectedItem.ToString();
            this.selectedLoc = LocationMgr.Instance.FindBy(this.selectedKey);
            this.selectedLocBackUp = (Location)this.selectedLoc.Clone();
            this.UpdateSelectedLocText();
        }

        private void UpdateSelectedLocText()
        {
            this.txtx.Text = selectedLoc?.X.ToString();
            this.txty.Text = selectedLoc?.Y.ToString();
            this.txtz.Text = selectedLoc?.Z.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedLoc == null)
            {
                return;
            }
            if (selectedLoc.IsSystemLoc)
            {
                return;
            }

            LocationMgr.Instance.Remove(this.selectedKey);
            this.UpdateListBox();
            string msg = string.Format("删除功能位 {0}成功！", this.selectedKey);
            Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            string s = this.txtName.Text;
            if (s == string.Empty || s == "")
            {
                return;
            }

            if (LocationMgr.Instance.FindBy(s) == null)
            {
                Location loc = new Location(s)
                {
                    X = Machine.Instance.Robot.PosX,
                    Y = Machine.Instance.Robot.PosY,
                    Z = Machine.Instance.Robot.PosZ
                };
                LocationMgr.Instance.Add(loc);
                LocationMgr.Instance.Save();

                this.UpdateListBox();
                string msg = string.Format("添加功能位 {0}:[{1},{2},{3}] 成功！", s, loc.X, loc.Y, loc.Z);
                Logger.DEFAULT.Info(LogCategory.MANUAL, this.GetType().Name, msg);
            }
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            Location loc = LocationMgr.Instance.FindBy(this.listBox1.SelectedItem.ToString());
            ValveType valve = ValveType.Valve1;
            if (this.rdoValve1Goto.Checked)
            {
                valve = ValveType.Valve1;
            }
            else
            {
                valve = ValveType.Valve2;
            }
            if (loc != null)
            {
                Machine.Instance.Robot.MoveToLoc(loc.ToNeedle(valve));
            }
        }

        private void UpdateListBox()
        {
            this.listBox1.Items.Clear();
            foreach (var item in LocationMgr.Instance.FindAll())
            {
                this.listBox1.Items.Add(item.Key);

            }
        }

        public DialogEditLocations Setup()
        {
            this.UpdateListBox();
            return this;
        }
    }
}
