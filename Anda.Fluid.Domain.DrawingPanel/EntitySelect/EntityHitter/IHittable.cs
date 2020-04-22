using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.DrawingProgram.Grammar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.EntitySelect.EntityHitter
{
    /// <summary>
    /// 轨迹点击判断
    /// </summary>
    public interface IHittable
    {
        /// <summary>
        /// 判断轨迹是否被鼠标点中
        /// </summary>
        bool IsHitting(PointF mouseLocationInModel, DirectiveDraw entity);
    }
}
