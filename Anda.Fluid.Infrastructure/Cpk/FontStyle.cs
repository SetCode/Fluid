using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Cpk
{
    public class FontStyle
    {
        /// <summary>
        /// 字体颜色 HSSFColor.Black.Index
        /// </summary>
        public short color = HSSFColor.Black.Index;
        /// <summary>
        /// 字体大小
        /// </summary>
        public double size=12;
        /// <summary>
        /// 字体 "宋体";
        /// </summary>
        public string fontType="宋体";

        public string dataFormat= string.Empty;

        public Alignments Align=Alignments.None;

        public FontStyle Default()
        {
            this.color= HSSFColor.Black.Index;
            this.size = 12;
            this.fontType= "宋体";
            this.Align= Alignments.None;
            this.dataFormat = string.Empty;
            return this;
        }


    }

 

    public enum Alignments
    {
        左上,
        左中,
        左下,
        中上,
        中中,
        中下,
        右上,
        右中,
        右下,
        None
    }
}
