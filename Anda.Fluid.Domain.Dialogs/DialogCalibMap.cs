using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogCalibMap : DialogBase, IOptional
    {
        private int flag;
        private int rowNum, colNum;
        private PointD origin = new PointD();
        private PointD hend = new PointD();
        private PointD vend = new PointD();
        private double interval;
        private int settlingTime;
        private int dwellTime;
        private bool stopping;
        private Rectangle targetRect = Rectangle.Empty;
        private Pen penGreen;
        private double outX = 0, outY = 0, outR = 0;
        private InspectionDot inspection;

        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        public DialogCalibMap()
        {
            InitializeComponent();
            lngResources.Add(btnTeachOrigin.Name, btnTeachOrigin.Text);
            lngResources.Add(btnGotoOrigin.Name, btnGotoOrigin.Text);
            lngResources.Add(label1.Name, label1.Text);

            lngResources.Add(label2.Name, label2.Text);
            lngResources.Add(btnTeachHend.Name, btnTeachHend.Text);
            lngResources.Add(btnGotoHend.Name, btnGotoHend.Text);

            lngResources.Add(label9.Name, label9.Text);
            lngResources.Add(btnTeachVend.Name, btnTeachVend.Text);
            lngResources.Add(btnGotoVend.Name, btnGotoVend.Text);

            lngResources.Add(label3.Name, label3.Text);
            lngResources.Add(label5.Name, label5.Text);
            lngResources.Add(label8.Name, label8.Text);
            this.Setup();
            this.ReadLanguageResources();
        }

        private void Setup()
        {
            this.nudInterval.Minimum = 1;
            this.nudInterval.Maximum = 100;
            this.nudInterval.DecimalPlaces = 1;
            this.nudInterval.Increment = 0.5M;
            this.nudInterval.Value = 5;

            this.nudSettlingTime.Minimum = 10;
            this.nudSettlingTime.Maximum = 1000;
            this.nudSettlingTime.Value = 50;

            this.nudDwellTime.Minimum = 10;
            this.nudDwellTime.Maximum = 1000;
            this.nudDwellTime.Value = 50;

            this.CamCtrl.Pbx.Paint += Pbx_Paint;
            this.penGreen = new Pen(Color.Green, 1);

            this.tbOriginX.Text = Properties.Settings.Default.mapStartX.ToString("0.000");
            this.tbOriginY.Text = Properties.Settings.Default.mapStartY.ToString("0.000");
            this.tbHendX.Text = Properties.Settings.Default.mapHEndX.ToString("0.000");
            this.tbHendY.Text = Properties.Settings.Default.mapHEndY.ToString("0.000");
            this.tbVendX.Text = Properties.Settings.Default.mapVEndX.ToString("0.000");
            this.tbVendY.Text = Properties.Settings.Default.mapVEndY.ToString("0.000");

            this.inspection = InspectionMgr.Instance.FindBy(0) as InspectionDot;

            this.UpdateByFlag();
        }

        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {

            this.SaveKeyValueToResources(btnTeachOrigin.Name, lngResources[btnTeachOrigin.Name]);
            this.SaveKeyValueToResources(btnGotoOrigin.Name, lngResources[btnGotoOrigin.Name]);
            this.SaveKeyValueToResources(label1.Name, lngResources[label1.Name]);
            this.SaveKeyValueToResources(label2.Name, lngResources[label2.Name]);
            this.SaveKeyValueToResources(btnTeachHend.Name, lngResources[btnTeachHend.Name]);
            this.SaveKeyValueToResources(btnGotoHend.Name, lngResources[btnGotoHend.Name]);
            this.SaveKeyValueToResources(label9.Name, lngResources[label9.Name]);
            this.SaveKeyValueToResources(btnTeachVend.Name, lngResources[btnTeachVend.Name]);
            this.SaveKeyValueToResources(btnGotoVend.Name, lngResources[btnGotoVend.Name]);
            this.SaveKeyValueToResources(label3.Name, lngResources[label3.Name]);
            this.SaveKeyValueToResources(label5.Name, lngResources[label5.Name]);
            this.SaveKeyValueToResources(label8.Name, lngResources[label8.Name]);
           
        }
        /// <summary>
        /// 更新控件显示文本
        /// </summary>
        /// <param name="skipButton"></param>
        /// <param name="skipRadioButton"></param>
        /// <param name="skipCheckBox"></param>
        /// <param name="skipLabel"></param>
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                lngResources[btnTeachOrigin.Name] = this.ReadKeyValueFromResources(btnTeachOrigin.Name);
                lngResources[btnGotoOrigin.Name] = this.ReadKeyValueFromResources(btnGotoOrigin.Name);
                lngResources[label1.Name] = this.ReadKeyValueFromResources(label1.Name);

                lngResources[label2.Name] = this.ReadKeyValueFromResources(label2.Name);
                lngResources[btnTeachHend.Name] = this.ReadKeyValueFromResources(btnTeachHend.Name);
                lngResources[btnGotoHend.Name] = this.ReadKeyValueFromResources(btnGotoHend.Name);

                lngResources[label9.Name] = this.ReadKeyValueFromResources(label9.Name);
                lngResources[btnTeachVend.Name] = this.ReadKeyValueFromResources(btnTeachVend.Name);
                lngResources[btnGotoVend.Name] = this.ReadKeyValueFromResources(btnGotoVend.Name);

                lngResources[label3.Name] = this.ReadKeyValueFromResources(label3.Name);
                lngResources[label5.Name] = this.ReadKeyValueFromResources(label5.Name);
                lngResources[label8.Name] = this.ReadKeyValueFromResources(label8.Name);
               
            }
            this.btnTeachOrigin.Text = lngResources[btnTeachOrigin.Name];
            this.btnGotoOrigin.Text = lngResources[btnGotoOrigin.Name];
            this.label1.Text = lngResources[label1.Name];

            this.label2.Text = lngResources[label2.Name];
            this.btnTeachHend.Text = lngResources[btnTeachHend.Name];
            this.btnGotoHend.Text = lngResources[btnGotoHend.Name];

            this.label9.Text = lngResources[label9.Name];
            this.btnTeachVend.Text = lngResources[btnTeachVend.Name];
            this.btnGotoVend.Text = lngResources[btnGotoVend.Name];

            this.label3.Text = lngResources[label3.Name];
            this.label5.Text = lngResources[label5.Name];
            this.label8.Text = lngResources[label8.Name];

        }

        private void Pbx_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                targetRect.Width = (int)(this.outR * 2 / this.CamCtrl.Radio);
                targetRect.Height = (int)(this.outR * 2 / this.CamCtrl.Radio);
                targetRect.X = this.CamCtrl.PbxCenter.X - (int)((this.CamCtrl.ImgCenter.X - this.outX + this.outR) / this.CamCtrl.Radio);
                targetRect.Y = this.CamCtrl.PbxCenter.Y - (int)((this.CamCtrl.ImgCenter.Y - this.outY + this.outR) / this.CamCtrl.Radio);
                e.Graphics.DrawEllipse(penGreen, targetRect);
            }
            catch (Exception)
            {

            }
        }

        public void DoCancel()
        {
            this.Close();
        }

        public void DoDone()
        {
            switch(this.flag)
            {
                case 3:
                    Machine.Instance.Robot.SaveStripMapPrm();
                    if (this.saveCSV(Machine.Instance.Robot.CalibMapPrm.Items) == DialogResult.OK)
                    {
                        Machine.Instance.Robot.IsMapValid = true;
                        this.flag++;
                        this.UpdateByFlag();
                    }
                    break;
                case 6:
                    this.saveCSV(Machine.Instance.Robot.CalibMapPrm.VerifyItems);
                    break;
            }

            Properties.Settings.Default.mapStartX = tbOriginX.Value;
            Properties.Settings.Default.mapStartY = tbOriginY.Value;
            Properties.Settings.Default.mapHEndX = tbHendX.Value;
            Properties.Settings.Default.mapHEndY = tbHendY.Value;
            Properties.Settings.Default.mapVEndX = tbVendX.Value;
            Properties.Settings.Default.mapVEndY = tbVendY.Value;
        }

        private DialogResult saveCSV(List<MapPoint> list)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.csv|*.*";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string fileName = sfd.FileName + ".csv";
                string head = "index_x,index_y,robot_x,robot_y,real_x,real_y,delta_x,delta_y";
                CsvUtil.WriteLine(fileName, head);
                foreach (var item in list)
                {
                    StringBuilder builder = new StringBuilder();
                    string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                        item.IndexX, item.IndexY, item.RobotX,Math.Round(item.RobotY, 3), Math.Round(item.RealX, 3),
                        Math.Round(item.RealY, 3), Math.Round(item.RealX - item.RobotX, 3), Math.Round(item.RealY - item.RobotY, 3));
                    CsvUtil.WriteLine(fileName, line);
                }
            }
            return dr;
        }

        public async void DoNext()
        {
            switch (this.flag)
            {
                case 0:
                    //设置参数
                    this.origin = new PointD(tbOriginX.Value, tbOriginY.Value);
                    this.hend = new PointD(tbHendX.Value, tbHendY.Value);
                    this.vend = new PointD(tbVendX.Value, tbVendY.Value);
                    this.interval = (double)this.nudInterval.Value;
                    if (this.origin.DistanceTo(this.hend) < this.interval || this.origin.DistanceTo(this.vend) < this.interval)
                    {
                        //MessageBox.Show("Please teach right Xend and Yend.");
                        MessageBox.Show("请正确的示教结束点.");
                        return;
                    }
                    this.settlingTime = (int)this.nudSettlingTime.Value;
                    this.dwellTime = (int)this.nudDwellTime.Value;
                    this.rowNum = (int)(this.vend.DistanceTo(this.origin) / this.interval) + 1;
                    this.colNum = (int)(this.hend.DistanceTo(this.origin) / this.interval) + 1;
                    break;
                case 1:
                    Machine.Instance.Robot.CalibMapPrm.RowNum = rowNum;
                    Machine.Instance.Robot.CalibMapPrm.ColNum = colNum;
                    Machine.Instance.Robot.CalibMapPrm.Interval = interval;
                    Machine.Instance.Robot.CalibMapPrm.Items.Clear();
                    Machine.Instance.Robot.IsMapValid = false;
                    this.listBox1.Items.Clear();
                    this.stopping = false;
                    await Task.Factory.StartNew(() =>
                    {
                        //拍照获取Xend和Yend的偏移
                        Machine.Instance.Robot.MoveSafeZAndReply();
                        Machine.Instance.Robot.MovePosXYAndReply(this.origin);
                        PointD delta0 = this.CaptrueDelta().Item1;
                        Machine.Instance.Robot.MovePosXYAndReply(this.hend.X, this.origin.Y);
                        PointD delta1 = this.CaptrueDelta().Item1;
                        //计算棋盘旋转矩阵
                        double sinth = (delta1.Y - delta0.Y) / ((int)(this.hend.DistanceTo(this.origin) / interval) * interval);
                        double baseX_x = interval * Math.Sqrt(1 - sinth * sinth);
                        double baseX_y = interval * sinth;
                        double baseY_x = -baseX_y;
                        double baseY_y = baseX_x;
                        //执行采样过程
                        for (int i = 0; i < this.rowNum; i++)
                        {
                            for (int j = 0; j < this.colNum; j++)
                            {
                                if (this.stopping)
                                {
                                    break;
                                }
                                Tuple<PointD, double> delta;
                                //计算棋盘拍照位置
                                double predX = j * baseX_x + i * baseY_x;
                                double predY = j * baseX_y + i * baseY_y;
                                PointD p = this.origin + delta0 + new PointD(predX, predY);
                                Machine.Instance.Robot.MovePosXYAndReply(p);
                                //拍照计算偏差
                                delta = this.CaptrueDelta();
                                double realX = p.X - delta.Item1.X;
                                double realY = p.Y - delta.Item1.Y;
                                //添加结果数据
                                MapPoint mp = new MapPoint()
                                {
                                    RealX = realX,
                                    RealY = realY,
                                    RobotX = p.X,
                                    RobotY = p.Y,
                                    IndexX = j,
                                    IndexY = i,
                                    R = delta.Item2,
                                    Ok = true
                                };
                                if (mp.R > 1.1 || mp.R < 0.9)
                                {
                                    mp.Ok = false;
                                }
                                Machine.Instance.Robot.CalibMapPrm.Items.Add(mp);
                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    this.listBox1.Items.Add(mp);
                                }));
                                //拍照后延时
                                Thread.Sleep(this.dwellTime);
                            }
                        }
                        Machine.Instance.Robot.MovePosXYAndReply(this.origin);
                    });
                    break;
                case 2:
                    this.LblTitle.Text = "Doing calibration...";
                    await Task.Factory.StartNew(() =>
                    {
                        //采用双线性插值算法
                        CalibMap.creatNewClbMap(colNum, rowNum, interval);
                        foreach (var item in Machine.Instance.Robot.CalibMapPrm.Items)
                        {
                            CalibMap.addPointData(item.RealX, item.RealY, item.RobotX, item.RobotY, item.IndexX, item.IndexY);
                        }
                        //采用神经网络算法
                        Machine.Instance.Robot.InitRBF2D();
                    });
                    break;
            }
            this.flag++;
            this.UpdateByFlag();
        }

        private Tuple<PointD, double> CaptrueDelta()
        {
            //拍照前延时
            Thread.Sleep(this.settlingTime);
            //拍照获取图像
            byte[] imgData = Machine.Instance.Camera.TriggerAndGetBytes(TimeSpan.FromSeconds(1)).DeepClone();
            //移动到拍照高度
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                Machine.Instance.Robot.MoveToMarkZAndReply();
            }
            //抓圆，得到像素坐标
            if (!this.chxAsv.Checked)
            {
                CalibMap.excuteCaliper(imgData, Machine.Instance.Camera.Executor.ImageWidth, Machine.Instance.Camera.Executor.ImageHeight,
                    ref outX, ref outY, ref outR);
            }
            else
            {
                this.inspection.Execute(imgData, Machine.Instance.Camera.Executor.ImageWidth, Machine.Instance.Camera.Executor.ImageHeight);
                outX = this.inspection.PixResultX;
                outY = this.inspection.PixResultY;
                outR = this.inspection.PixResultR;
            }
            //像素坐标转机械坐标
            double scale = 0;
            PointD p = Machine.Instance.Camera.ToMachine(outX, outY);
            CalibBy9d.GetScale(ref scale);
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.LblTitle.Text = string.Format("deltaX = {0}, deltaY = {1}.", Math.Round(p.X, 3), Math.Round(p.Y, 3));
            }));
            return new Tuple<PointD, double>(p, outR * scale);
        }

        public void DoPrev()
        {
            if (this.flag >= 3)
            {
                this.flag = 1;
            }
            else
            {
                this.flag--;
            }
            this.UpdateByFlag();
        }

        public void DoTeach()
        {
           
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.stopping = true;
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            if(this.flag == 6)
            {
                this.flag = 4;
            }

            PointD p00 = new PointD();
            Machine.Instance.Robot.CalibMapPrm.VerifyItems.Clear();
            this.listBox1.Items.Clear();
            Random random = new Random();
            //棋盘校正
            this.stopping = false;
            await Task.Factory.StartNew(() => {
                Machine.Instance.Robot.MoveSafeZAndReply();
                for (int i = 0; i < this.rowNum; i++)
                {
                    for (int j = 0; j < this.colNum; j++)
                    {
                        if (this.stopping)
                        {
                            break;
                        }

                        Tuple<PointD, double> delta;
                        MapPoint mp = Machine.Instance.Robot.CalibMapPrm.Items.Find(m => m.IndexX == j && m.IndexY == i);
                        double dx = random.Next(-100, 100) * 0.01;
                        double dy = random.Next(-100, 100) * 0.01;
                        //dx = 0;
                        //dy = 0;
                        double machX = 0, machY = 0;
                        if (mp != null)
                        {
                            if (!this.chxRbfd.Checked)
                            {
                                CalibMap.mapToMach(mp.RobotX + dx, mp.RobotY + dy, ref machX, ref machY);
                            }
                            else
                            {
                                CalibNet.calcCoordinate(mp.RobotX + dx, mp.RobotY + dy, ref machX, ref machY);
                            }
                            if (i == 0 && j == 0)
                            {
                                p00.X = machX;
                                p00.Y = machY;
                            }
                        }
                        Machine.Instance.Robot.MovePosXYAndReply(machX, machY);
                        delta = this.CaptrueDelta();

                        MapPoint mp2 = new MapPoint()
                        {
                            RealX = - delta.Item1.X,
                            RealY = - delta.Item1.Y,
                            RobotX = dx,
                            RobotY = dy,
                            IndexX = j,
                            IndexY = i,
                            R = delta.Item2,
                            Ok = true
                        };
                        Machine.Instance.Robot.CalibMapPrm.VerifyItems.Add(mp2);
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.listBox1.Items.Add(mp);
                        }));
                        //拍照后延时
                        Thread.Sleep(this.dwellTime);
                    }
                }
                Machine.Instance.Robot.MovePosXYAndReply(p00);
                this.flag++;
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    this.UpdateByFlag();
                }));
            });

            this.flag++;
            this.UpdateByFlag();
        }

        private void btnTeachOrigin_Click(object sender, EventArgs e)
        {
            this.tbOriginX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            this.tbOriginY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
        }

        private void btnGotoOrigin_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(this.tbOriginX.Value, this.tbOriginY.Value);
        }

        private void btnTeachHend_Click(object sender, EventArgs e)
        {
            this.tbHendX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            this.tbHendY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
        }

        private void btnGotoHend_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(this.tbHendX.Value, this.tbHendY.Value);

        }

        private void btnTeachVend_Click(object sender, EventArgs e)
        {
            this.tbVendX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            this.tbVendY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
        }

        private void btnGotoVend_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MoveSafeZ();
            Machine.Instance.Robot.ManualMovePosXY(this.tbVendX.Value, this.tbVendY.Value);
        }

        private void btnAsv_Click(object sender, EventArgs e)
        {
            this.inspection.SetImage(
               Machine.Instance.Camera.Executor.CurrentBytes,
               Machine.Instance.Camera.Executor.ImageWidth,
               Machine.Instance.Camera.Executor.ImageHeight);
            this.inspection.ShowEditWindow();
        }

        private void chxAsv_CheckedChanged(object sender, EventArgs e)
        {
            this.btnAsv.Enabled = this.chxAsv.Checked;
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
            {
                return;
            }
            MapPoint mp = this.listBox1.SelectedItem as MapPoint;
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Robot.MovePosXYAndReply(mp.RobotX, mp.RobotY);
            });
        }

        private void btnMapMove_Click(object sender, EventArgs e)
        {
            new JogMapForm().Show();
        }

        private void UpdateByFlag()
        {
            switch(this.flag)
            {
                case 0:
                    this.LblTitle.Text = "Input params and [Next].";
                    this.BtnPrev.Enabled = false;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.panel1.Enabled = true;
                    this.chxAsv.Enabled = true;
                    this.btnAsv.Enabled = this.chxAsv.Checked;
                    this.btnStop.Enabled = false;
                    this.btnGoto.Enabled = true;
                    this.btnFind.Enabled = true;
                    this.btnVerify.Enabled = false;
                    break;
                case 1:
                    this.LblTitle.Text = "Press [Next] to capture points.";
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.panel1.Enabled = false;
                    this.chxAsv.Enabled = true;
                    this.btnAsv.Enabled = this.chxAsv.Checked;
                    this.btnStop.Enabled = false;
                    this.btnGoto.Enabled = false;
                    this.btnFind.Enabled = false;
                    this.btnVerify.Enabled = false;
                    break;
                case 2:
                    this.LblTitle.Text = "Press [Next] to start callibration.";
                    this.BtnPrev.Enabled = false;
                    this.BtnNext.Enabled = true;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.panel1.Enabled = false;
                    this.chxAsv.Enabled = false;
                    this.btnAsv.Enabled = false;
                    this.btnStop.Enabled = true;
                    this.btnGoto.Enabled = false;
                    this.btnFind.Enabled = false;
                    this.btnVerify.Enabled = false;
                    break;
                case 3:
                    this.LblTitle.Text = "Press [Done] to accept results.";
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = true;
                    this.BtnCancel.Enabled = true;
                    this.panel1.Enabled = false;
                    this.chxAsv.Enabled = true;
                    this.btnAsv.Enabled = this.chxAsv.Checked;
                    this.btnStop.Enabled = false;
                    this.btnGoto.Enabled = true;
                    this.btnFind.Enabled = true;
                    this.btnVerify.Enabled = false;
                    break;
                case 4:
                    this.LblTitle.Text = "Press [Verify] to start verification.";
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = true;
                    this.panel1.Enabled = false;
                    this.chxAsv.Enabled = true;
                    this.btnAsv.Enabled = this.chxAsv.Checked;
                    this.btnStop.Enabled = false;
                    this.btnGoto.Enabled = true;
                    this.btnFind.Enabled = true;
                    this.btnVerify.Enabled = true;
                    break;
                case 5:
                    this.LblTitle.Text = "verifying...";
                    this.BtnPrev.Enabled = false;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = false;
                    this.BtnCancel.Enabled = false;
                    this.panel1.Enabled = false;
                    this.chxAsv.Enabled = false;
                    this.btnAsv.Enabled = false;
                    this.btnStop.Enabled = true;
                    this.btnGoto.Enabled = false;
                    this.btnFind.Enabled = false;
                    this.btnVerify.Enabled = false;
                    break;
                case 6:
                    this.LblTitle.Text = "Press [Done] to accept the results.";
                    this.BtnPrev.Enabled = true;
                    this.BtnNext.Enabled = false;
                    this.BtnTeach.Enabled = false;
                    this.BtnDone.Enabled = true;
                    this.BtnCancel.Enabled = true;
                    this.panel1.Enabled = false;
                    this.chxAsv.Enabled = true;
                    this.btnAsv.Enabled = this.chxAsv.Checked;
                    this.btnStop.Enabled = false;
                    this.btnGoto.Enabled = true;
                    this.btnFind.Enabled = true;
                    this.btnVerify.Enabled = true;
                    break;
            }
        }

        private async void btnGoto_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
            {
                return;
            }
            MapPoint mp = this.listBox1.SelectedItem as MapPoint;
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Robot.MovePosXYAndReply(mp.RobotX, mp.RobotY);
            });
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                this.CaptrueDelta();
            });
            this.cameraControl1.Pbx.Invalidate();
        }
    }

    public class CalibMapFrmPrm
    {
        [CompareAtt("CMP")]
        public PointD Origin = new PointD();
        [CompareAtt("CMP")]
        public PointD Hend = new PointD();
        [CompareAtt("CMP")]
        public PointD Vend = new PointD();
        [CompareAtt("CMP")]
        public double Interval = 5;
        [CompareAtt("CMP")]
        public int SettlingTime = 50;
        [CompareAtt("CMP")]
        public int DwellTime = 50;
    }
}
