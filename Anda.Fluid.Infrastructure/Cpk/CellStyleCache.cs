using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Cpk
{
    public class CellStyleCache
    {
        private Dictionary<FontStyle, ICellStyle> CellStyleMap = new Dictionary<FontStyle, ICellStyle>();

        public ICellStyle GetCellStyle(FontStyle style)
        {
            foreach (object o in CellStyleMap.Keys)
            {
                FontStyle outStyle = o as FontStyle;
                if(      outStyle.Align == style.Align
                        && outStyle.color == style.color
                        && outStyle.fontType == style.fontType
                         && outStyle.size == style.size
                         && outStyle.dataFormat == style.dataFormat
                    )
                {
                    return CellStyleMap[outStyle];
                }
            }
            return null;

        }

        public void AddCellStyle(FontStyle style, ICellStyle cellStyle)
        {
            ICellStyle Cstyle = this.GetCellStyle(style);
            if (Cstyle==null)
            {
                this.CellStyleMap.Add(style,cellStyle);
            }
        }


    }
}
