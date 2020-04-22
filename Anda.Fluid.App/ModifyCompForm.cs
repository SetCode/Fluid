using Anda.Fluid.App.Common;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App
{
    /// <summary>
    /// 修改元器件窗体(不改元器件库，只是更改点胶程序坐标)
    /// Author : 肖旭
    /// date : 2019/12/26
    /// </summary>
    public partial class ModifyCompForm : Form,IMsgSender
    {
        /// <summary>
        /// 当前显示的pattern程序
        /// </summary>
        private CommandsModule commandsModule;

        /// <summary>
        /// 当前选定的元器件集合
        /// </summary>
        private List<Component> comps = new List<Component>();

        

        public ModifyCompForm(CommandsModule commandsModule)
        {
            InitializeComponent();
            this.commandsModule = commandsModule;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnFindComp_Click(object sender, EventArgs e)
        {
            this.nudRotation.Value = 0;
            this.comps.Clear();
            if (this.rdoComponent.Checked)
            {
                this.CreatComponents(this.txtCompName.Text);
                this.pictureBox1.Invalidate();

                if (this.comps.Count == 0)
                {
                    //再加上双引号查找一次
                    this.CreatComponents(string.Format("\"{0}\"", this.txtCompName.Text));
                    this.pictureBox1.Invalidate();
                    if (this.comps.Count == 0)
                    {
                        MessageBox.Show("未找到该元器件");
                    }
                }
            }
            else if(this.rdoDesign.Checked)
            {
                bool res=this.getComponentByDesign(this.txtCompName.Text);
                this.pictureBox1.Invalidate();
                if (!res)
                {
                    //再加上双引号查找一次
                    this.getComponentByDesign(string.Format("\"{0}\"", this.txtCompName.Text));
                    this.pictureBox1.Invalidate();
                    if (this.comps.Count == 0)
                    {
                        MessageBox.Show("未找到该元器件");
                    }
                }
                    
            }
            
        }
        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void btnRotate_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Invalidate();           
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.comps.Count == 0)
                return;

            foreach (var item in this.comps)
            {
                item.ModifyRotation((double)this.nudRotation.Value);
            }
            MsgCenter.Broadcast(Constants.MSG_PATTERN_EDITED, this, null);
        }

        /// <summary>
        /// 根据元器件名称，构建当前的元器件集合
        /// </summary>
        /// <param name="compName"></param>
        private void CreatComponents(string compName)
        {
            this.comps.Clear();

            //得到所有元器件名称相同的点按设计编号名称分类的集合
            List<List<DotCmdLine>> classficationDots = this.GetClassficationDots(compName);

            //根绝分类添加元器件
            foreach (var item in classficationDots)
            {
                this.comps.Add(new Component(item));
            }
        }
        private bool getComponentByDesign(string design)
        {
            List<DotCmdLine> dots = new List<DotCmdLine>();
            foreach (var item in this.commandsModule.CmdLineList)
            {
                //如果不是点指令，则终止此次循环
                if (item is DotCmdLine == false)
                    continue;
                DotCmdLine dotCmdLine = item as DotCmdLine;
                if (dotCmdLine.TrackNumber != null && dotCmdLine.TrackNumber.Equals(design))
                {
                    dots.Add(dotCmdLine);
                }
            }
            if (dots==null || dots.Count<=0)
            {
                return false;
            }
            string compName = dots[0].Comp;
            foreach (var item in dots)
            {
                if (!item.Comp.Equals(compName))
                {
                    return false;
                }
            }
            this.comps.Clear();
            this.comps.Add(new Component(dots));
            return true;        
        }
        /// <summary>
        /// 将元器件名称相同的点指令按照设计名称进行分类
        /// </summary>
        /// <returns></returns>
        private List<List<DotCmdLine>> GetClassficationDots(string compName)
        {
            List<List<DotCmdLine>> classficationDots = new List<List<DotCmdLine>>();
            foreach (var item in this.commandsModule.CmdLineList)
            {
                //如果不是点指令，则终止此次循环
                if (item is DotCmdLine == false)
                    continue;

                DotCmdLine dotCmdLine = item as DotCmdLine;
                //如果元器件名称相同
                if (dotCmdLine.Comp != null && dotCmdLine.TrackNumber != null && dotCmdLine.Comp.Equals(compName))
                {
                    //是否找到同样的设计名称
                    bool findDesign = false;

                    //遍历已分类的集合                      
                    foreach (var dotList in classficationDots)
                    {
                        //如果该点的设计名称和某个集合的设计名称一致，则添加到该集合中
                        if (dotCmdLine.TrackNumber.Equals(dotList.First().TrackNumber))
                        {
                            dotList.Add(dotCmdLine);
                            findDesign = true;
                            break;
                        }
                    }

                    //如果没找到，则重新增加一个点集合
                    if (!findDesign)
                    {
                        List<DotCmdLine> dots = new List<DotCmdLine>();
                        dots.Add(dotCmdLine);
                        classficationDots.Add(dots);
                    }
                }
            }

            return classficationDots;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (this.comps.Count == 0)
                return;

            //画出绿色十字线
            Pen greenPen = new Pen(Color.Green);
            e.Graphics.DrawLine(greenPen, new PointF(0, this.pictureBox1.Height / 2)
                , new PointF(this.pictureBox1.Width, this.pictureBox1.Height / 2));
            e.Graphics.DrawLine(greenPen, new PointF(this.pictureBox1.Width / 2, 0)
                , new PointF(this.pictureBox1.Width / 2, this.pictureBox1.Height));

            //得到元器件在指定角度下的中心、胶点坐标、X和Y方向上的最大距离(用以自适应图像大小)
            Tuple<PointD, List<PointD>> tuple = this.comps.First().GetPosition((double)this.nudRotation.Value);
            PointF rotateCenter = new PointF((Single)tuple.Item1.X, (Single)tuple.Item1.Y);
            List<PointF> points = new List<PointF>();
            double longestDis = 0.1;
            foreach (var item in tuple.Item2)
            {
                points.Add(new PointF((Single)item.X, (Single)item.Y));
                double disX = Math.Abs(item.X - tuple.Item1.X);
                double disY = Math.Abs(item.Y - tuple.Item1.Y);
                if (disX > longestDis)
                {
                    longestDis = disX;
                }
                if (disY > longestDis)
                {
                    longestDis = disY;
                }
            }

            //得到元器件的中心相对于显示控件中心的偏差值
            Single offsetX = this.pictureBox1.Width / 2 - rotateCenter.X;
            Single offsetY = this.pictureBox1.Height / 2 - rotateCenter.Y;

            //得到自适应比例
            Single ratio = (this.pictureBox1.Width / 8) / (Single)longestDis;

            //得到胶点在画布中的坐标
            for (int i = 0; i < points.Count; i++)
            {
                Single x = (points[i].X - rotateCenter.X) * ratio + this.pictureBox1.Width / 2;
                Single y = -(points[i].Y - rotateCenter.Y) * ratio + this.pictureBox1.Height / 2;
                points[i] = new PointF(x, y);
            }

            //绘制胶点
            foreach (var item in points)
            {
                //圆的中心坐标
                PointF centerPosition = new PointF(item.X, item.Y);

                //圆半径
                float radius = 10;

                //圆外接四边形的左上点坐标
                PointF panelPosition = new PointF(centerPosition.X - radius, centerPosition.Y - radius);

                //圆外接四边形尺寸
                SizeF arcSize = new SizeF(radius * 2, radius * 2);
                RectangleF rect = new RectangleF(panelPosition, arcSize);

                //画刷和画笔
                Brush brush = new SolidBrush(Color.Red);

                //执行绘图
                e.Graphics.FillEllipse(brush, rect);
            }
        }

    }

    /// <summary>
    /// 单个元器件
    /// Author : 肖旭
    /// Data : 2019/12/27
    /// </summary>
    public class Component
    {
        /// <summary>
        /// 元器件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元器件在PCB板上的设计名称
        /// Example:元器件名称同样为Resistance(电阻),在PCB板上共有3个，则设计名称分别为R1、R2、R3.
        /// </summary>
        public string Design { get; set; }

        /// <summary>
        /// 元器件在PCB板上的旋转角度
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// 该元器件对应的点胶指令集合
        /// </summary>
        public List<DotCmdLine> DotCmdLines { get; set; } = new List<DotCmdLine>();

        public Component(List<DotCmdLine> dotCmdLines)
        {
            this.Name = dotCmdLines[0].Comp;
            this.Design = dotCmdLines[0].TrackNumber;
            this.Rotation = Convert.ToDouble(dotCmdLines[0].Rotation);
            this.DotCmdLines = dotCmdLines;
        }

        /// <summary>
        /// 得到元器件在指定旋转角度时的中心坐标和各个胶点的坐标
        /// </summary>
        public Tuple<PointD,List<PointD>> GetPosition(double rotation)
        {
            List<PointD> points = new List<PointD>();

            //得到元器件的旋转中心
            List<PointD> compPoints = new List<PointD>();
            foreach (var item in this.DotCmdLines)
            {
                compPoints.Add((PointD)item.Position.Clone());
            }
            PointD rotateCenter = this.GetCenter(compPoints);

            ////先将元器件上的各个胶点旋转到0°
            //foreach (var item in DotCmdLines)
            //{
            //    PointD rotate0Point = item.Position.Rotate(rotateCenter, 0);
            //    points.Add(rotate0Point);
            //}

            ////再将各个胶点旋转到指定角度
            //for (int i = 0; i < points.Count; i++)
            //{
            //    points[i] = points[i].Rotate(rotateCenter, rotation);
            //}
            for (int i = 0; i < compPoints.Count; i++)
            {
                compPoints[i] = compPoints[i].Rotate(rotateCenter, rotation);
            }
            return new Tuple<PointD, List<PointD>>(rotateCenter, compPoints);
        }

        /// <summary>
        /// 将当前元器件中对应的点指令的坐标相对于元器件中心旋转指定角度
        /// </summary>
        /// <param name="rotation"></param>
        public void ModifyRotation(double rotation)
        {
            //得到旋转中心
            List<PointD> points = new List<PointD>();
            List<DotCmdLine> Dots = this.DotCmdLines;
            foreach (var item in this.DotCmdLines)
            {
                points.Add(item.Position);
            }
            PointD center = this.GetCenter(points);

            //进行旋转
            for (int i = 0; i < this.DotCmdLines.Count; i++)
            {
                DotCmdLines[i].Position = DotCmdLines[i].Position.Rotate(center,rotation);
            }
        }

        /// <summary>
        /// 得到传入的点集合的中心
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private PointD GetCenter(List<PointD> points)
        {
            PointD center=new PointD();
            //1个点时，就是其自身
            if (points.Count == 1)
            {
                center = new PointD(points[0].X, points[0].Y);
            }
            //2个点时，是两点组成的直线的中间点
            else if (points.Count == 2)
            {
                double middleX = (points[0].X + points[1].X) / 2;
                double middleY = (points[0].Y + points[1].Y) / 2;
                center = new PointD(middleX, middleY);
            }
            //如果大于2个点时，则是多点组成的圆的圆心（取前三个点）
            else
            {
                //center = MathUtils.CalculateCircleCenter(points[0], points[1], points[2]);
                center=this.CalcuDotsCenter(DotCmdLines);
            }
            return center;
        }

        private PointD CalcuDotsCenter(List<DotCmdLine> dotCmdLines)
        {
            //double 
            if (dotCmdLines.Count<2)
            {
                return null;
            }
            PointD center = new PointD();
            PointD org1 = dotCmdLines[0].OrgOffset;
            PointD real1 = dotCmdLines[0].Position;
            PointD org2 = dotCmdLines[1].OrgOffset;
            PointD real2 = dotCmdLines[1].Position;

            CoordinateTransformer transformer = new CoordinateTransformer();
            transformer.SetMarkPoint(org1,org2,real1,real2);
            center=transformer.Transform(new PointD(0,0));
            return center;
        }
    }
}
