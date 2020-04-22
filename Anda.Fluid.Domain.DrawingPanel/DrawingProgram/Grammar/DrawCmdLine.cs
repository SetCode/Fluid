
using DrawingPanel.EntitySelect.EntityHitter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public abstract class DrawCmdLine : ICloneable
    {
        private Graphics graphics = null;
        public Graphics Graphics
        {
            get { return this.graphics; }
            set { this.graphics = value; }
        }

        /// <summary>
        /// 轨迹组成的最大外接矩形
        /// </summary>
        public abstract RectangleF Rect { get; }


        public abstract bool IsClick{ get; set; }


        public bool IsSelected { get; set; }

        public abstract bool IsHitter(PointF point);

        public abstract bool IsContain(RectangleF rect);

        /// <summary>
        /// 构建具体指令
        /// </summary>
        /// <param name="origin"></param>
        public abstract void Setup(PointF origin);
        public abstract void SetupDisable(PointF origin);

        protected abstract void SetTrack(PointF ModelOrigin, Color backColor);

        /// <summary>
        /// 绘图
        /// </summary>
        public abstract void DrawingPanel();
        public abstract object Clone();
    }
}
