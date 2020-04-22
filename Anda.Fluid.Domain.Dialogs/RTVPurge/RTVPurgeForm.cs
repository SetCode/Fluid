using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem.PurgeAndPrime.Purge;
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

namespace Anda.Fluid.Domain.Dialogs.RTVPurge
{
    public partial class RTVPurgeForm : Form
    {
        private PointD lineStart;
        private PointD lineEnd;

        private int arrayXDirection;
        private int arrayXCounts;
        private double arrayXInterval;
        private int arrayYDirection;
        private int arrayYCounts;
        private double arrayYInterval;

        private double vel;
        private int cycles;
        private int delay;

        private List<Tuple<PointD, PointD>> lines;
        public RTVPurgeForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.cbxArrayXDirection.Items.Add("X轴正向");
            this.cbxArrayXDirection.Items.Add("X轴负向");
            this.cbxArrayYDirection.Items.Add("Y轴正向");
            this.cbxArrayYDirection.Items.Add("Y轴负向");
        }

        public void Setup()
        {
            this.SetupDate();
            this.UpdateUI();
        }

        private void SetupDate()
        {
            this.lineStart = RTVPurgePrm.Instance.LineStart;
            this.lineEnd = RTVPurgePrm.Instance.LineEnd;

            this.arrayXDirection = RTVPurgePrm.Instance.ArrayXDirection;
            this.arrayXCounts = RTVPurgePrm.Instance.ArrayXCounts;
            this.arrayXInterval = RTVPurgePrm.Instance.ArrayXInterval;
            this.arrayYDirection = RTVPurgePrm.Instance.ArrayYDirection;
            this.arrayYCounts = RTVPurgePrm.Instance.ArrayYCounts;
            this.arrayYInterval = RTVPurgePrm.Instance.ArrayYInterval;

            this.vel = RTVPurgePrm.Instance.Vel;
            this.cycles = RTVPurgePrm.Instance.Cycles;
            this.delay = RTVPurgePrm.Instance.DispenseDelay;

            this.lines = RTVPurgePrm.Instance.Lines;
        }

        private void UpdateUI()
        {
            this.txtPrimeX.Text = Machine.Instance.Robot.CalibPrm.PrimeLoc.X.ToString();
            this.txtPrimeY.Text = Machine.Instance.Robot.CalibPrm.PrimeLoc.Y.ToString();
            this.txtPrimeZ.Text = Machine.Instance.Robot.CalibPrm.PrimeZ.ToString();

            this.txtLineStartX.Text = RTVPurgePrm.Instance.LineStart.X.ToString();
            this.txtLineStartY.Text = RTVPurgePrm.Instance.LineStart.Y.ToString();
            this.txtLineEndX.Text = RTVPurgePrm.Instance.LineEnd.X.ToString();
            this.txtLineEndY.Text = RTVPurgePrm.Instance.LineEnd.Y.ToString();

            this.cbxArrayXDirection.SelectedIndex = RTVPurgePrm.Instance.ArrayXDirection;
            this.txtArrayXCount.Text = RTVPurgePrm.Instance.ArrayXCounts.ToString();
            this.txtArrayXInterval.Text = RTVPurgePrm.Instance.ArrayXInterval.ToString();
            this.cbxArrayYDirection.SelectedIndex = RTVPurgePrm.Instance.ArrayYDirection;
            this.txtArrayYCount.Text = RTVPurgePrm.Instance.ArrayYCounts.ToString();
            this.txtArrayYInterval.Text = RTVPurgePrm.Instance.ArrayYInterval.ToString();

            this.txtVel.Text = RTVPurgePrm.Instance.Vel.ToString();
            this.txtCycles.Text = RTVPurgePrm.Instance.Cycles.ToString();
            this.txtDelay.Text = RTVPurgePrm.Instance.DispenseDelay.ToString();

            this.UpdateListView();
        }

        private void UpdateListView()
        {
            this.listView1.Clear();
            for (int i = 0; i < RTVPurgePrm.Instance.Lines.Count; i++)
            {
                string s = string.Format("Line{0}:{1},{2}", i + 1,
                    RTVPurgePrm.Instance.Lines[i].Item1.ToString(),
                    RTVPurgePrm.Instance.Lines[i].Item2.ToString());
                this.listView1.Items.Add(s);
            }
        }

        private void CalculateLines()
        {
            RTVPurgePrm.Instance.Lines.Clear();

            double lineXLength = Math.Abs(RTVPurgePrm.Instance.LineEnd.X - RTVPurgePrm.Instance.LineStart.X);
            double lineYLength = Math.Abs(RTVPurgePrm.Instance.LineEnd.Y - RTVPurgePrm.Instance.LineStart.Y);

            //先构造X方向
            for (int i = 0; i < RTVPurgePrm.Instance.ArrayXCounts; i++)
            {
                //如果是X轴正向
                if (RTVPurgePrm.Instance.ArrayXDirection == 0)
                {
                    PointD singleLineStart = new PointD();
                    PointD singleLineEnd = new PointD();
                    singleLineStart.X = RTVPurgePrm.Instance.LineStart.X + i * lineXLength + i * RTVPurgePrm.Instance.ArrayXInterval;
                    singleLineStart.Y = RTVPurgePrm.Instance.LineStart.Y;
                    singleLineEnd.X = RTVPurgePrm.Instance.LineEnd.X + i * lineXLength + i * RTVPurgePrm.Instance.ArrayXInterval;
                    singleLineEnd.Y = RTVPurgePrm.Instance.LineEnd.Y;
                    RTVPurgePrm.Instance.Lines.Add(new Tuple<PointD, PointD>(singleLineStart, singleLineEnd));
                }
                //X轴负向
                else
                {
                    PointD singleLineStart = new PointD();
                    PointD singleLineEnd = new PointD();
                    singleLineStart.X = RTVPurgePrm.Instance.LineStart.X - i * lineXLength - i * RTVPurgePrm.Instance.ArrayXInterval;
                    singleLineStart.Y = RTVPurgePrm.Instance.LineStart.Y;
                    singleLineEnd.X = RTVPurgePrm.Instance.LineEnd.X - i * lineXLength - i * RTVPurgePrm.Instance.ArrayXInterval;
                    singleLineEnd.Y = RTVPurgePrm.Instance.LineEnd.Y;
                    RTVPurgePrm.Instance.Lines.Add(new Tuple<PointD, PointD>(singleLineStart, singleLineEnd));
                }
            }

            //再构造Y方向
            for (int i = 1; i < RTVPurgePrm.Instance.ArrayYCounts; i++)
            {
                for (int j = 0; j < RTVPurgePrm.Instance.ArrayXCounts; j++)
                {
                    //如果是Y轴正向
                    if (RTVPurgePrm.Instance.ArrayYDirection == 0)
                    {
                        PointD singleLineStart = new PointD();
                        PointD singleLineEnd = new PointD();
                        singleLineStart.X = RTVPurgePrm.Instance.Lines[j].Item1.X;
                        singleLineStart.Y = RTVPurgePrm.Instance.Lines[j].Item1.Y + i * lineYLength + i * RTVPurgePrm.Instance.ArrayYInterval;
                        singleLineEnd.X = RTVPurgePrm.Instance.Lines[j].Item2.X;
                        singleLineEnd.Y = RTVPurgePrm.Instance.Lines[j].Item2.Y + i * lineYLength + i * RTVPurgePrm.Instance.ArrayYInterval;
                        RTVPurgePrm.Instance.Lines.Add(new Tuple<PointD, PointD>(singleLineStart, singleLineEnd));
                    }
                    //Y轴负向
                    else
                    {
                        PointD singleLineStart = new PointD();
                        PointD singleLineEnd = new PointD();
                        singleLineStart.X = RTVPurgePrm.Instance.Lines[j].Item1.X;
                        singleLineStart.Y = RTVPurgePrm.Instance.Lines[j].Item1.Y - i * lineYLength - i * RTVPurgePrm.Instance.ArrayYInterval;
                        singleLineEnd.X = RTVPurgePrm.Instance.Lines[j].Item2.X;
                        singleLineEnd.Y = RTVPurgePrm.Instance.Lines[j].Item2.Y - i * lineYLength - i * RTVPurgePrm.Instance.ArrayYInterval;
                        RTVPurgePrm.Instance.Lines.Add(new Tuple<PointD, PointD>(singleLineStart, singleLineEnd));
                    }
                }
            }
        }

        private void btnLineStartTeach_Click(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.LineStart = Machine.Instance.Robot.PosXY;
            RTVPurgePrm.Instance.posZ = Machine.Instance.Robot.PosZ;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void btnTeachEnd_Click(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.LineEnd = Machine.Instance.Robot.PosXY;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void cbxArrayXDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.ArrayXDirection = this.cbxArrayXDirection.SelectedIndex;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void cbxArrayYDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.ArrayYDirection = this.cbxArrayYDirection.SelectedIndex;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtArrayXCount_TextChanged(object sender, EventArgs e)
        {
            if (this.txtArrayXCount.Value == 0)
            {
                MessageBox.Show("最小值为1");
                this.txtArrayXCount.Text = "1";
                RTVPurgePrm.Instance.ArrayXCounts = 1;
            }
            else
            {
                RTVPurgePrm.Instance.ArrayXCounts = this.txtArrayXCount.Value;
            }
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtArrayYCount_TextChanged(object sender, EventArgs e)
        {
            if (this.txtArrayYCount.Value == 0)
            {
                MessageBox.Show("最小值为1");
                this.txtArrayYCount.Text = "1";
                RTVPurgePrm.Instance.ArrayYCounts = 1;
            }
            else
            {
                RTVPurgePrm.Instance.ArrayYCounts = this.txtArrayYCount.Value;
            }
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtArrayXInterval_TextChanged(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.ArrayXInterval = this.txtArrayXInterval.Value;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtArrayYInterval_TextChanged(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.ArrayYInterval = this.txtArrayYInterval.Value;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtVel_TextChanged(object sender, EventArgs e)
        {
            if (this.txtVel.Value == 0)
            {
                MessageBox.Show("速度不能为0");
                this.txtVel.Text = "10";
                RTVPurgePrm.Instance.Vel = 10;
            }
            else
            {
                RTVPurgePrm.Instance.Vel = this.txtVel.Value;
            }
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtCycles_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCycles.Value == 0)
            {
                MessageBox.Show("最小值为1");
                this.txtCycles.Text = "1";
                RTVPurgePrm.Instance.Cycles = 1;
            }
            else
            {
                RTVPurgePrm.Instance.Cycles = this.txtCycles.Value;
            }
            this.CalculateLines();
            this.UpdateUI();
        }

        private void txtDelay_TextChanged(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.DispenseDelay = this.txtDelay.Value;
            this.CalculateLines();
            this.UpdateUI();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.CurrLineIndex = 0;
            RTVPurgePrm.Instance.CurrLineCycle = 0;
            RTVPurgePrm.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RTVPurgePrm.Instance.LineStart = this.lineStart;
            RTVPurgePrm.Instance.LineEnd = this.lineEnd;

            RTVPurgePrm.Instance.ArrayXDirection = this.arrayXDirection;
            RTVPurgePrm.Instance.ArrayXCounts = this.arrayXCounts;
            RTVPurgePrm.Instance.ArrayXInterval = this.arrayXInterval;
            RTVPurgePrm.Instance.ArrayYDirection = this.arrayYDirection;
            RTVPurgePrm.Instance.ArrayYCounts = this.arrayYCounts;
            RTVPurgePrm.Instance.ArrayYInterval = this.arrayYInterval;

            RTVPurgePrm.Instance.Vel = this.vel;
            RTVPurgePrm.Instance.Cycles = this.cycles;
            RTVPurgePrm.Instance.DispenseDelay = this.delay;

            RTVPurgePrm.Instance.Lines = this.lines;
            RTVPurgePrm.Save();

            this.Close();
        }

        private void btnGotoStart_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();
                Machine.Instance.Robot.MovePosXYAndReply(RTVPurgePrm.Instance.LineStart);
                Machine.Instance.Robot.MovePosZ(RTVPurgePrm.Instance.posZ);
            }));
        }

        private void btnGotoEnd_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();
                Machine.Instance.Robot.MovePosXYAndReply(RTVPurgePrm.Instance.LineEnd);
                Machine.Instance.Robot.MovePosZ(RTVPurgePrm.Instance.posZ);
            }));
        }

        private void btnGotoPrime_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                Machine.Instance.Robot.MoveSafeZAndReply();
                Machine.Instance.Robot.MovePosXYAndReply(Machine.Instance.Robot.CalibPrm.PrimeLoc);
                Machine.Instance.Robot.MovePosZ(Machine.Instance.Robot.CalibPrm.PrimeZ);
            }));
        }

        private void btnTeachPrime_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.CalibPrm.PrimeLoc = Machine.Instance.Robot.PosXY;
            Machine.Instance.Robot.CalibPrm.PrimeZ = Machine.Instance.Robot.PosZ;

            this.UpdateUI();
            Machine.Instance.UpdateLocations();
        }
    }
}
