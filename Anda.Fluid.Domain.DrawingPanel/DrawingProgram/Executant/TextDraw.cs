
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Executant
{
    public class TextDraw : DirectiveDraw
    {
        private string content;
        private Color contentColor;
        private PointF position;
        private float size;
        public TextDraw (string content,PointF position, Color penColor,float size)
        {
            this.content = content;
            this.contentColor = penColor;
            this.size = size*(float)1.2;
            this.position = position;
            
        }
        public override void Execute()
        {
            //设置字体的尺寸和样式
            FontFamily fntFamily = new FontFamily("Calibri");
            StringFormat fontFormat = new StringFormat();
            fontFormat.Alignment = StringAlignment.Center;
            fontFormat.LineAlignment = StringAlignment.Center;
            float fontSize = (this.size / (float)1.2);
            Font fnt = new Font(fntFamily, fontSize,FontStyle.Bold);

            //用来限制字体位置的矩形
            RectangleF rect = new RectangleF((this.position.X - this.size / 2)  ,
               (this.position.Y - this.size / 2 ) ,
               this.size , this.size );

            //画刷
            Brush brush = new SolidBrush(this.contentColor);

            //绘图
            DrawProgram.Instance.Graphics.DrawString(this.content, fnt, brush, rect,fontFormat);

        }
        public override object Clone()
        {
            TextDraw text = MemberwiseClone() as TextDraw;
            text.content = content;
            text.contentColor = contentColor;
            text.position = position;
            text.size = size;
            return text;
        }

        public override bool IsHitter(PointF point)
        {
            throw new NotImplementedException();
        }

        public override bool IsContain(RectangleF rect)
        {
            throw new NotImplementedException();
        }
    }
}
