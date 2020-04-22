using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Grammar;
using DrawingPanel.Data;
using DrawingPanel.DrawingProgram;
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.DrawingProgram.Grammar;
using DrawingPanel.Msg;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingPanel
{
    public partial class TestForm : Form
    {
        private Graphics g;
        public TestForm()
        {
            DrawingMsgCenter.Instance.RegisterReceiver(DrawProgram.Instance);        
            
            InitializeComponent();
            GlobalData.Instance.PanelWidth = this.drawingPanelControl1.Width;
            GlobalData.Instance.PanelHeight = this.drawingPanelControl1.Height;
            g = this.drawingPanelControl1.CreateGraphics();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DrawWorkPiece workPiece = new DrawWorkPiece(new PointF(50, 50));
            //DrawProgram.Instance.AddWorkPiece(workPiece);


            ////PointF startPoint1 = new PointF(0, 0);
            ////PointF endPoint1 = new PointF(100, 100);
            ////LineCmdLine lineCmdLine1 = new LineCmdLine(startPoint1, endPoint1, 1, Color.Green, true);
            ////workPiece.Add(lineCmdLine1);

            //DrawPattern pattern = new DrawPattern(new PointF(100, 100));            

            ////line
            //PointF startPoint = new PointF(0, 0);
            //PointF endPoint = new PointF(100, 100);
            //LineDrawCmd lineCmdLine = new LineDrawCmd(startPoint, endPoint, 1, Color.Green, true);
            //pattern.Add(lineCmdLine);

            ////mark
            //MarkDrawCmd markCmdLine = new MarkDrawCmd(new PointF(0, 100), 5, Color.Blue, Color.Red);
            //pattern.Add(markCmdLine);

            ////height
            //HeightDrawCmd heightCmdLine = new HeightDrawCmd(new PointF(100, 0), 5, Color.Violet, Color.Green);
            //pattern.Add(heightCmdLine);

            ////arc
            //ArcDrawCmd arcCmdLine = new ArcDrawCmd(new PointF(50, 50), new PointF(60, 50), 2, Color.Red, 90);
            //pattern.Add(arcCmdLine);

            ////circle
            //CircleDrawCmd circleCmdLine = new CircleDrawCmd(new PointF(150, 150), 10, 2, Color.Red);
            //pattern.Add(circleCmdLine);

            ////polyLine
            //PointF[] points1 = new PointF[] { new PointF(100, 0), new PointF(200, 0), new PointF(200, 50) };
            //PolyLineDrawCmd polyLineCmdLine = new PolyLineDrawCmd(points1, 2, Color.Green, true);
            //pattern.Add(polyLineCmdLine);

            ////lines
            //Line2Points line1 = new Line2Points(new PointF(100, 20), new PointF(150, 20));
            //Line2Points line2 = new Line2Points(new PointF(100, 40), new PointF(150, 40));
            //Line2Points line3 = new Line2Points(new PointF(100, 60), new PointF(150, 60));
            //Line2Points line4 = new Line2Points(new PointF(100, 80), new PointF(150, 80));
            //LinesDrawCmd linesCmdLine = new LinesDrawCmd(new Line2Points[] { line1, line2, line3, line4 }, 2, Color.Green, true);
            //pattern.Add(linesCmdLine);
            //pattern.Add(null);

            //pattern.SetupAll();

            //PointF p1 = new PointF(50, 50);
            //PointF p2 = new PointF(400, 0);
            //PointF p3 = new PointF(-400, 400);
            //PointF[] allPoint = new PointF[] { p1, p2, p3 };

            //ArrayDrawCmd arrayCmdLine = new ArrayDrawCmd(pattern, allPoint);

            //DrawProgram.Instance.WorkPiece.Add(arrayCmdLine);

            ////DoPatternCmdLine doPatternCmdLine1 = new DoPatternCmdLine(pattern, p1);
            ////DoPatternCmdLine doPatternCmdLine2 = new DoPatternCmdLine(pattern, p2);
            ////DoPatternCmdLine doPatternCmdLine3 = new DoPatternCmdLine(pattern, p3);
            ////DrawProgram.Instance.WorkPiece.Add(doPatternCmdLine1);
            ////DrawProgram.Instance.WorkPiece.Add(doPatternCmdLine2);
            ////DrawProgram.Instance.WorkPiece.Add(doPatternCmdLine3);

            //DrawProgram.Instance.WorkPiece.SetupAll();

            //DrawProgram.Instance.SetGraphics(this.g, 1);
            //DrawProgram.Instance.Drawing();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FluidProgram.Create("xiaoxu", 0, 0);
            DotCmdLine dot = new DotCmdLine();
            dot.Position.X =100;
            dot.Position.Y = 100;
            FluidProgram.CurrentOrDefault().Workpiece.AddCmdLine(dot);

            DoCmdLine doPattern = new DoCmdLine("1", 300, 300);
            FluidProgram.CurrentOrDefault().Workpiece.AddCmdLine(doPattern);

            Pattern pattern = new Pattern(FluidProgram.CurrentOrDefault(), "1", 200, 200);
            DotCmdLine dot1 = new DotCmdLine();
            dot1.Position.X = 200;
            dot1.Position.Y = 200;
            pattern.AddCmdLine(dot1);
            FluidProgram.CurrentOrDefault().AddPattern(pattern);

            

            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.需要更新绘图程序, FluidProgram.CurrentOrDefault());

            DrawProgram.Instance.SetGraphics(this.g, 1);
            DrawProgram.Instance.Drawing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.需要更新绘图程序, FluidProgram.CurrentOrDefault());
            DrawProgram.Instance.SetGraphics(this.g, 1);

            //DrawProgram.Instance.Drawing();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Pattern界面,0);
            DrawProgram.Instance.SetGraphics(this.g, 1);

            DrawProgram.Instance.Drawing();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Workpiece界面, 0);

            DrawProgram.Instance.SetGraphics(this.g, 1);
            DrawProgram.Instance.Drawing();
        }
    }
}
