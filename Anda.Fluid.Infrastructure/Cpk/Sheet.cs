using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NPOI.HSSF.UserModel;
using System.IO;

using System.Collections;
using NPOI.SS.Util;
using System.Windows.Forms;
using NPOI.HSSF.Util;

namespace Anda.Fluid.Infrastructure.Cpk
{
    public class Sheet
    {
        public WorkBook VirWorkBook;
        private HSSFWorkbook workBook;
        ISheet sheet;
        public Sheet(HSSFWorkbook workBook, ISheet sheet)
        {            
            this.workBook = workBook;
            this.sheet = sheet;
        }


        #region 单元格设置内容
        //double
        public void SetRowValue(int rowStart, string colStart, ArrayList data,FontStyle fontStyle=null)
        {
            if (data == null)
            {
                return;
            }
            if (fontStyle==null)
            {
                fontStyle = new FontStyle().Default();
            }
          
            int nrowStart, ncolStart;
            ExlIndexToNpIndex(rowStart, colStart, out nrowStart, out ncolStart);

            SetRowValue(nrowStart, ncolStart, data, fontStyle);
        }
        /// <summary>
        /// 设置一行数据
        /// </summary>
        /// <param name="npRowStart"></param>
        /// <param name="npColStart"></param>
        /// <param name="data"></param>
        /// <param name="fontStyle"></param>
        public void SetRowValue(int npRowStart, int npColStart, ArrayList data, FontStyle fontStyle = null)
        {
            if (data == null)
            {
                return;
            }
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }
            int cols = data.Count;
            IRow row = this.sheet.GetRow(npRowStart);

            if (row == null)
            {
                row = this.sheet.CreateRow(npRowStart);
            }

            for (int colIndex = 0; colIndex < cols; colIndex++)
            {

                if (data[0] is int || data[0] is double)
                {
                    
                    row.CreateCell(npColStart + colIndex).SetCellValue(Convert.ToDouble(data[colIndex]));


                }
                else if (data[0] is string)
                {
                    row.CreateCell(npColStart + colIndex).SetCellValue(data[colIndex].ToString());

                }
                SetCellStyle(npRowStart, npColStart + colIndex, fontStyle);
            }
        }


        //一个单元格
        public void SetOneCellValue( int rowStart, string colStart, object data, FontStyle fontStyle = null)
        {
            ArrayList arr = new ArrayList(1);
            arr.Add(data);
            this.SetRowValue( rowStart, colStart, arr);
        }
        /// <summary>
        /// 设置一个单元格内容
        /// </summary>
        /// <param name="nprowStart"></param>
        /// <param name="npcolStart"></param>
        /// <param name="data"></param>
        /// <param name="fontStyle"></param>
        public void SetOneCellValue(int nprowStart, int npcolStart, object data, FontStyle fontStyle = null)
        {
            ArrayList arr = new ArrayList(1);
            arr.Add(data);
            this.SetRowValue(nprowStart, npcolStart, arr);
        }

        public void SetColumValue(int rowStart, string colStart, ArrayList data, FontStyle fontStyle = null)
        {
            if (data == null)
            {
                return;
            }
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }
           
            int nrowStart, ncolStart;
            ExlIndexToNpIndex(rowStart, colStart, out nrowStart, out ncolStart);

            SetColumValue(nrowStart, ncolStart, data, fontStyle);
        }
        public void NewColumn(int nprowStart, int npcolStart, int rows)
        {
            IRow row;
            ICell cell;
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                row = this.sheet.CreateRow(nprowStart + rowIndex);
            }
        }
        public void SetColumValue(int nprowStart, int npcolStart, ArrayList data, FontStyle fontStyle = null)
        {
            if (data == null)
            {
                return;
            }
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }
            int rows = data.Count;

            IRow row;
            ICell cell;
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                row = this.sheet.GetRow(nprowStart + rowIndex);
                if (row == null)
                {
                    row = this.sheet.CreateRow(nprowStart + rowIndex);
                }
                 cell = row.GetCell(npcolStart);
                if (cell==null)
                {
                    cell = row.CreateCell(npcolStart);
                }
                if (data[0] is int || data[0] is double)
                {

                    cell.SetCellValue(Convert.ToDouble(data[rowIndex]));

                }
                else if (data[0] is string)
                {
                    cell.SetCellValue(data[rowIndex].ToString());

                }
                SetCellStyle(nprowStart + rowIndex, npcolStart, fontStyle);

            }
        }

        #endregion

        
        public void DelRow(int rowStart,int rowEnd)
        {
            int nprowStart,nprowEnd;
            ExlRIndexToNpRIndex(rowStart, out nprowStart);
            ExlRIndexToNpRIndex(rowEnd, out nprowEnd);

            this.SetOneCellValue(rowStart, "A", 10);
            
            this.sheet.ShiftRows(nprowStart, nprowEnd, -1);
        }
        public void HideRow(int erowStart)
        {
            int nprowStart;
            ExlRIndexToNpRIndex(erowStart, out nprowStart);
            IRow row;
            row = this.sheet.GetRow(nprowStart);
            if (row == null)
            {
                row = this.sheet.CreateRow(nprowStart);
            }
            row.ZeroHeight = true;
        }
        public void HideRows(int erowStart,int rows)
        {
            for (int i=0;i<rows;i++)
            {
                this.HideRow(erowStart+i);
            }
            
        }
      

        #region Style

        //单元格合并
        public void CreateRegion(int rowStart, string colStart, int rowEnd, string colEnd)
        {
            int nrowStart, ncolStart, nrowEnd, ncolEnd;

            ExlIndexToNpIndex(rowStart, colStart, out nrowStart, out ncolStart);
            ExlIndexToNpIndex(rowEnd, colEnd, out nrowEnd, out ncolEnd);

            CreateRegion(nrowStart, ncolStart,nrowEnd,  ncolEnd);

        }

        public void CreateRegion(int nprowStart, int npcolStart, int nprowEnd, int npcolEnd)
        {
            int nprowS, npcolS, nprowE, npcolE;
            nprowS = nprowStart;
            npcolS = npcolStart;
            nprowE = nprowEnd;
            npcolE = npcolEnd;
            CellRangeAddress cellAddr = new CellRangeAddress(nprowS, nprowE, npcolS, npcolE);
            this.sheet.AddMergedRegion(cellAddr);
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

        //对齐
        public ICellStyle SetAlignment(ICellStyle style, Alignments align=Alignments.None)
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

        

        public void SetCellStyle(int rowIndex, string colIndex, FontStyle fontstyle)
        {
            int nrowIndex, ncolIndex;

            ExlIndexToNpIndex(rowIndex, colIndex, out nrowIndex, out ncolIndex);
            this.SetCellStyle(nrowIndex, ncolIndex, fontstyle);
        }
        
        /// <summary>
        /// nrowIndex ncolIndex 是NPOI 可操作的索引，设置单格的格式
        /// </summary>
        /// <param name="nprowIndex"></param>
        /// <param name="npcolIndex"></param>
        /// <param name="fontstyle"></param>
        public void SetCellStyle(int nprowIndex, int npcolIndex, FontStyle fontstyle)
        {
            //ExlIndexToNpIndex(rowIndex, colIndex, out nrowIndex, out ncolIndex);
            ICellStyle style;
            //style = workBook.CreateCellStyle();
            //style = SetAlignment(style, fontstyle.Align);
            //style.SetFont(SetFont(fontstyle.color, fontstyle.size, fontstyle.fontType));

            //SetDataFormat(style, fontstyle.dataFormat);
            //
            style = VirWorkBook.CreateCellStyle(fontstyle);

            IRow row = this.sheet.GetRow(nprowIndex);
            if (row == null)
            {
                row = this.sheet.CreateRow(nprowIndex);
            }
            ICell cell = row.GetCell(npcolIndex);
            if (cell == null)
            {
                cell = row.CreateCell(npcolIndex);
            }

            cell.CellStyle = style;
        }


        //设置一列的格式
        public void SetColStyle(int rowStart, string colStart, int rows,FontStyle fontStyle)
        {            
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }          
            int nrowStart, ncolStart;
            ExlIndexToNpIndex(rowStart, colStart, out nrowStart, out ncolStart);
            SetColStyle(nrowStart, ncolStart, rows, fontStyle);
        }

        public void SetColStyle(int nprowStart, int  npcolStart, int rows, FontStyle fontStyle)
        {
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {                            
                SetCellStyle(nprowStart + rowIndex, npcolStart, fontStyle);
            }
        }

        public void SetRowStyle(int rowStart, string colStart, int cols, FontStyle fontStyle)
        {
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }
            int nrowStart, ncolStart;
            ExlIndexToNpIndex(rowStart, colStart, out nrowStart, out ncolStart);
        }
        public void SetRowStyle(int nprowStart, int npcolStart, int cols, FontStyle fontStyle)
        {
            if (fontStyle == null)
            {
                fontStyle = new FontStyle().Default();
            }
            
            for (int colIndex = 0; colIndex < cols; colIndex++)
            {                              
                SetCellStyle(nprowStart, npcolStart+ colIndex, fontStyle);
            }
        }


        /// <summary>
        /// 设置单元格模式
        /// </summary>
        /// <param name="style"></param>
        /// <param name="format">例如"0.0000"</param>
        public void  SetDataFormat(ICellStyle style, string formatStyle)
        {
            if (formatStyle==string.Empty||style==null)
            {
                return;
            }
            IDataFormat format = workBook.CreateDataFormat();
            //style.DataFormat  = HSSFDataFormat.GetBuiltinFormat("0.00");
            style.DataFormat = format.GetFormat(formatStyle);
        }

        //public void SetDataFormat()
        //{
        //    IDataFormat format = workBook.CreateDataFormat();
        //    ICell cell = sheet.CreateRow(32).CreateCell(6);
        //    cell.SetCellValue(1.2);
        //    ICellStyle cellStyle = workBook.CreateCellStyle();
        //    //cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.0000");
        //    cellStyle.DataFormat = format.GetFormat("0.0000");
        //    cell.CellStyle = cellStyle;
        //}

        public void SetColumnWidth(string colIndex, int nChar)
        {
            int  ncolIndex;
            
            Sheet.ExlCIndexToNpCIndex(colIndex, out ncolIndex);
            SetColumnWidth(ncolIndex, nChar);


        }
        public void SetColumnWidth(int npcolIndex, int nChar)
        {
            int width;
            width = nChar * 256;
            this.sheet.SetColumnWidth(npcolIndex, width);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="height">设置的高度</param>
        public void SetRowWidthByEIndex(int rowIndex, int height)
        {
            int nprowIndex;
            
            Sheet.ExlRIndexToNpRIndex(rowIndex, out nprowIndex);
            //this.sheet.GetRow(nrowIndex).Height = height * 20;
            this.sheet.GetRow(nprowIndex).HeightInPoints = height;
        }
        public void SetRowWidthByNpIndex(int nprowIndex, int height)
        {
            this.sheet.GetRow(nprowIndex).HeightInPoints = height;
        }

        #endregion

        # region 公式
        public void SetOneCellFormula(int rLoc, string cLoc, string formulaStr,FontStyle style=null)
        {
            int nrLoc, ncLoc;
            if (formulaStr==string.Empty)
            {
                return;
            }
            ExlIndexToNpIndex(rLoc, cLoc, out nrLoc, out ncLoc);
            SetOneCellFormula(nrLoc, ncLoc, formulaStr, style);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="npRLoc">NPOI 行索引</param>
        /// <param name="npCLoc">NPOI 列索引</param>
        /// <param name="formulaStr"></param>
        /// <param name="style"></param>
        public void SetOneCellFormula(int npRLoc, int npCLoc, string formulaStr, FontStyle style = null)
        {
            IRow row;
            ICell cell;
            row = this.sheet.GetRow(npRLoc);
            if (row == null)
            {
                row = this.sheet.CreateRow(npRLoc);
            }
            cell = row.GetCell(npCLoc);
            if (cell==null)
            {
                cell = row.CreateCell(npCLoc);
            }
            cell.SetCellFormula(formulaStr);
            FontStyle styleTemp = style;
            if (styleTemp == null)
            {
                styleTemp = new FontStyle().Default();
            }
            SetCellStyle(npRLoc, npCLoc, styleTemp);
        }

        public void SetColFormaula(int npRLoc, int npCLoc, string[] formulaStr, FontStyle style = null)
        {
            int length = formulaStr.Length;
            for (int rowIndex=0;rowIndex<length;rowIndex++)
            {
                SetOneCellFormula(npRLoc + rowIndex, npCLoc, formulaStr[rowIndex], style);
            }
            
        }
        //public void FomatFormula(string fun, string spli, string firstLoc, string secondLoc)
        //{
        //    string formulaStr = string.Format("{}{}{}");
        //}

        #endregion

        #region 转换
        /// <summary>
        /// 将A AA 转换为1 27
        /// </summary>
        /// <param name="rowStr">A开始</param>
        /// <returns>从1 开始</returns>
        public static int Str2Int(string rowStr)
        {
            int length = rowStr.Length;
            byte[] arr = new byte[length];
            arr = System.Text.Encoding.ASCII.GetBytes(rowStr);


            int res = 0;
            for (int i = 0; i < length; i++)
            {
                //65->A | 90->Z
                if (!(arr[i] >= 65 && arr[i] <= 90))
                {
                    MessageBox.Show("输入的字符串错误: {0}", rowStr);
                    return -1;
                }

                res += Convert.ToInt32((arr[i] - 65 + 1) * Math.Pow(26, length - 1 - i));
            }


            return res;

        }

        public void ExlIndexToNpoiIndex(int erow, int eindex, out int nrow, out int ncol)
        {

            nrow = erow - 1;
            ncol = eindex - 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="erow">从1 开始</param>
        /// <param name="ecol">从A 开始</param>
        /// <param name="nrow">从0 开始</param>
        /// <param name="ncol"></param>
        public static void ExlIndexToNpIndex(int erow, string ecol, out int nrow, out int ncol)
        {
            nrow = erow - 1;
            ncol = Sheet.Str2Int(ecol) - 1;

        }
        public static void ExlRIndexToNpRIndex(int erow, out int nrow)
        {
            nrow = erow - 1;
        }
        public static void ExlCIndexToNpCIndex(string ecol, out int ncol)
        {
            ncol = Sheet.Str2Int(ecol) - 1;
        }
        #endregion 

    }


}
