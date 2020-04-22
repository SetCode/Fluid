using Anda.Fluid.App.LoadTrajectory;
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
    public partial class DialogCADImport : Form
    {
        public DialogCADImport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.rtxInfo.ReadOnly = true;
        }
        private int step = 0;
        private bool laststepStatus = true;

        private void btnPrev_Click(object sender, EventArgs e)
        {            
            if (this.step <= 0)
            {
                this.step = 0;
            }
            else
            {
                this.step--;
            }
            this.process();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.laststepStatus == true)
            {
                step++;
            }
            else
            {
                step += 0;
            }
            this.process();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void process()
        {
            switch (this.step)
            {
                case 0:
                    this.showRunInfo("Please click the next key to load the trajectory file");
                    this.laststepStatus = true;
                    break;
                case 1:
                    this.loadTrajectory();
                    break;
                case 2:
                    this.componentLibEdit();
                    break;
                case 3:
                    this.editTrajetory();
                    break;
                case 4:
                    this.Done();
                    break;                    
                default:
                    break;

            }

        }

        private void DialogCADImport_Load(object sender, EventArgs e)
        {
            this.rtxInfo.Text = "Please click the next key to load the trajectory file";
        }

        private void loadTrajectory()
        {
            this.showRunInfo("Start loading ...");
            if (new DialogImport().ShowDialog() == DialogResult.OK)
            {
                PatternInfo pattern = CADImport.Instance.GetCurPatternInfo();
                if (pattern != null && pattern.CompListStanded!=null && pattern.CompListStanded.Count>=0)
                {
                    this.laststepStatus = true;
                    this.showRunInfo("Successfully open the trajectory file,Please click the next key to edit Component Library");
                    return;
                }
                
            }
            this.showRunInfo("failed to load trajectory file,Please click the next key to load the trajectory file again");
            this.laststepStatus = false;
            
            
        }

        private void showRunInfo(string info)
        {
            this.rtxInfo.Text = info;
        }

        private void componentLibEdit()
        {
            this.showRunInfo("Start editing Component Library ...");
            PatternInfo pattern = CADImport.Instance.GetCurPatternInfo();
            if (new DialogComponentLibEdit().Setup(pattern).ShowDialog() == DialogResult.OK)
            {
                this.laststepStatus = true;
                this.showRunInfo("Successfully edit the Component Library,Please click the next key to edit the trajectory");
            }
            else
            {
                this.laststepStatus = false;
                this.showRunInfo("failed to edit the Component Library,Please click the next key to edit Component Library again");
            }
        }

        private void editTrajetory()
        {
            this.showRunInfo("Start editing the trajetory and create the pattern ...");
            PatternInfo pattern = CADImport.Instance.GetCurPatternInfo();
            if (new TrajectoryDialog02().ShowDialog() == DialogResult.OK)
            {
                this.laststepStatus = true;
                this.btnNext.Text = "Done";
                this.showRunInfo("Successfully edit the trajectory,Please click the Done key to create the Pattern");
            }
            else
            {
                this.laststepStatus = false;
            }
        }
        private void Done()
        {
            this.step = 0;
            this.laststepStatus = true;
            this.Close();
        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            new DialogLibConvert().ShowDialog();
        }
    }
}
