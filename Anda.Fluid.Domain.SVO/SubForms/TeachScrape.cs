using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.Locations;
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
    internal partial class TeachScrape : JogFormBase
    {
        public TeachScrape()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.txtLocation.ReadOnly = true;

            if (Machine.Instance.Robot != null)
            {
                this.ShowLocation();
            }
            this.ReadLanguageResources();
        }

        private void ShowLocation()
        {
            this.txtLocation.Text = string.Format("{0},{1},{2}", Machine.Instance.Robot.CalibPrm.ScrapeLocation.X+ Machine.Instance.Robot.CalibPrm.NeedleCamera1.X, Machine.Instance.Robot.CalibPrm.ScrapeLocation.Y + Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y, Machine.Instance.Robot.CalibPrm.ScrapeLocation.Z); 
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            Location loc = new Location();
            loc.X = Machine.Instance.Robot.CalibPrm.ScrapeLocation.X;
            loc.Y = Machine.Instance.Robot.CalibPrm.ScrapeLocation.Y;
            loc.Z = Machine.Instance.Robot.CalibPrm.ScrapeLocation.Z;
            Machine.Instance.Robot.MoveToLocAndReply(loc.ToNeedle(0));
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.CalibPrm.ScrapeLocation.X = Machine.Instance.Robot.PosX - Machine.Instance.Robot.CalibPrm.NeedleCamera1.X;
            Machine.Instance.Robot.CalibPrm.ScrapeLocation.Y = Machine.Instance.Robot.PosY - Machine.Instance.Robot.CalibPrm.NeedleCamera1.Y;
            Machine.Instance.Robot.CalibPrm.ScrapeLocation.Z = Machine.Instance.Robot.PosZ;

            Machine.Instance.Robot.SystemLocations.ScrapeLoc.X = Machine.Instance.Robot.CalibPrm.ScrapeLocation.X;
            Machine.Instance.Robot.SystemLocations.ScrapeLoc.Y = Machine.Instance.Robot.CalibPrm.ScrapeLocation.Y;
            Machine.Instance.Robot.SystemLocations.ScrapeLoc.Z = Machine.Instance.Robot.CalibPrm.ScrapeLocation.Z;
            this.ShowLocation();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();

            if (DataSetting.Default.DoneStepCount <= 11)
            {
                DataSetting.Default.DoneStepCount = 11;
            }
            Machine.Instance.Robot.CalibPrm.SavedItem = 11;
            StepStateMgr.Instance.FindBy(10).IsDone = true;
            StepStateMgr.Instance.FindBy(10).IsChecked();

            DataSetting.Save();
            Machine.Instance.Robot.CalibPrm.SavedTime = DateTime.Now;
            Machine.Instance.Robot.SaveCalibPrm();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
