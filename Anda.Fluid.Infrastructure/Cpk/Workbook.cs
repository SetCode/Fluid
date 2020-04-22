using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

namespace Anda.Fluid.Infrastructure.Cpk
{

    public enum SheetName
    {
        Valve1Weight,
        Valve2Weight,
        X轴,
        Y轴,
        XY轴联动,
        Z轴
    }

    public class WorkBook
    {
        public WorkBook()
        {
            this.workBook = new HSSFWorkbook();
        }
        //样式缓冲
        private CellStyleCache StyleCache = new CellStyleCache();

        private HSSFWorkbook workBook;

        private Dictionary<string, Sheet> sheets = new Dictionary<string, Sheet>();

        public ICellStyle CreateCellStyle(FontStyle fontstyle)
        {
            ICellStyle newStyle = this.StyleCache.GetCellStyle(fontstyle);
            if (newStyle == null)
            {
                //新建一个CellStyle
                ICellStyle cStyle;
                cStyle = workBook.CreateCellStyle();
                cStyle = SetAlignment(cStyle, fontstyle.Align);
                cStyle.SetFont(this.SetFont(fontstyle.color, fontstyle.size, fontstyle.fontType));
                SetDataFormat(cStyle, fontstyle.dataFormat);
                newStyle = cStyle;

                this.StyleCache.AddCellStyle(fontstyle, newStyle);;
            }            
            return newStyle;
        }

        
        public void AddSheet(string sheetName)
        {
            if (sheets.ContainsKey(sheetName))
            {
                return;
            }
            ISheet sheet = (HSSFSheet)workBook.CreateSheet(sheetName);
            Sheet sheetTmp = new Sheet(this.workBook, sheet);
            sheetTmp.VirWorkBook = this;
            sheets.Add(sheetName, sheetTmp);
        }
        public void RemoveSheet(string sheetName)
        {
            if (sheets.ContainsKey(sheetName))
            {
                workBook.RemoveName(sheetName);
            }
        }
        public Sheet FindSheetByName(string sheetName)
        {
            if (sheets.ContainsKey(sheetName))
            {
                return sheets[sheetName];
            }
            return null;
        }

        public void Save(string path)
        {
            FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            workBook.Write(file);
            file.Close();
        }
        #region CellStyle
        //对齐
        public ICellStyle SetAlignment(ICellStyle style, Alignments align = Alignments.None)
        {

            style = workBook.CreateCellStyle();

            switch (align)
            {
                case Alignments.左上:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    style.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case Alignments.左中:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case Alignments.左下:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    style.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case Alignments.中上:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case Alignments.中中:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case Alignments.中下:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case Alignments.右上:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    style.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case Alignments.右中:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case Alignments.右下:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    style.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                default:
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    break;

            }

            return style;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">例如HSSFColor.Black.Index</param>
        /// <param name="size"></param>
        /// <param name="fontType"></param>
        public IFont SetFont(short color, double size, string fontType)
        {
            IFont font = workBook.CreateFont();
            font.Color = color;//字体颜色
            font.FontHeightInPoints = size;//大小 字号
            font.FontName = fontType;
            return font;
        }

        /// <summary>
        /// 设置单元格模式
        /// </summary>
        /// <param name="style"></param>
        /// <param name="format">例如"0.0000"</param>
        public void SetDataFormat(ICellStyle style, string formatStyle)
        {
            if (formatStyle == string.Empty || style == null)
            {
                return;
            }
            IDataFormat format = workBook.CreateDataFormat();
            //style.DataFormat  = HSSFDataFormat.GetBuiltinFormat("0.00");
            style.DataFormat = format.GetFormat(formatStyle);
        }
        #endregion 

    }
}
