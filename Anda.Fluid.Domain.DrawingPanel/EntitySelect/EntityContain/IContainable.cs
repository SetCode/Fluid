using DrawingPanel.DrawingProgram.Executant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.EntitySelect.EntityContain
{
    /// <summary>
    /// 轨迹被框选判断
    /// </summary>
    public interface IContainable
    {
        bool IsContain(RectangleF mouseRect, DirectiveDraw entity);
    }
}
