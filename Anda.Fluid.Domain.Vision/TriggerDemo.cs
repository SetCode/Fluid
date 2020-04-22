using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Vision
{
    public partial class TriggerDemo : Form
    {
        public TriggerDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Machine.Instance.Camera.Executor.SetContinueMode();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Machine.Instance.Camera.Executor.GrabOne();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Machine.Instance.Camera.Executor.StopGrabing();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Machine.Instance.Camera.Executor.ContinueToSoftShot();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Machine.Instance.Camera.Executor.SoftShotToContinue();
        }
    }
}
