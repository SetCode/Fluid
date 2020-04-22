using Anda.Fluid.Infrastructure.Cpk;
using NPOI.HSSF.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ASV
{
    public class ArrayDataSave
    {
        public ArrayList dataInput { get; set; }
        private WorkBook workBook;
        private Sheet sheet;
        private string sheetName = "ArrayFrame";

        string path = @"ArrayFrame.xls";
        public ArrayDataSave(ArrayList dataInput)
        {
            this.dataInput = dataInput;
            workBook = new WorkBook();
            workBook.AddSheet(sheetName);

            sheet = workBook.FindSheetByName(sheetName);

        }

        public void SaveDataToExl()
        {
            ////自喷自检阵列框模式分析结果
            sheet.SetOneCellValue(1, "A", "自喷自检阵列框模式分析结果");

            sheet.SetCellStyle(1, "A", new FontStyle()
            {
                color = HSSFColor.Black.Index,
                size = 20,
                Align = Alignments.中中

            });
            sheet.CreateRegion(1, "A", 4, "K");

            ////记录数据            
            Location recordLoc = new Location(32, "B");
            recordLoc.ExlIndexToNpIndex();
            ////数据
            Location dataLoc = new Location();
            dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            dataLoc.NpIndexToExlIndex();
            this.AddRecord(recordLoc, dataInput);

            /***********************计算结果*********************************/
            //Location resLoc = new Location(13, "A");
            //this.AddFormula(resLoc, dataLoc);
            //sheet.SetColumnWidth("A", 15);
            /**************************************************************/
           
            this.sheet.VirWorkBook.Save(path);
        }
        public void AddRecord(Location recordLoc, ArrayList dataInput)
        {
            recordLoc.ExlIndexToNpIndex();

            //记录数据

            sheet.SetOneCellValue(recordLoc.NPRowIndex - 4, recordLoc.NPColIndex - 1, "记录数据");
            sheet.SetCellStyle(recordLoc.NPRowIndex - 4, recordLoc.NPColIndex - 1, new FontStyle()
            {
                size = 16,
                Align = Alignments.中中
            });
            sheet.CreateRegion(recordLoc.NPRowIndex - 4, recordLoc.NPColIndex - 1, recordLoc.NPRowIndex - 2, recordLoc.NPColIndex + 9);

            //Title
            //Location recordLoc = new Location(32, "B");
            //recordLoc.ExlIndexToNpIndex();


            sheet.SetOneCellValue(recordLoc.NPRowIndex, recordLoc.NPColIndex, "序号");

            sheet.SetOneCellValue(recordLoc.NPRowIndex, recordLoc.NPColIndex + 4, "定位值");

            sheet.SetRowStyle(recordLoc.NPRowIndex, recordLoc.NPColIndex, 5, new FontStyle()
            {
                size = 13,
                Align = Alignments.中中
            });
            //序号
            ArrayList dataIndex = new ArrayList(30);
            for (int i = 0; i < 30; i++)
            {
                dataIndex.Add(i + 1);
            }

            sheet.SetColumValue(recordLoc.NPRowIndex + 1, recordLoc.NPColIndex, dataIndex, new FontStyle()
            {
                Align = Alignments.右中
            });

            //数据
            //Location dataLoc = new Location();
            //dataLoc.NPRowIndex = recordLoc.NPRowIndex + 1;
            //dataLoc.NPColIndex = recordLoc.NPColIndex + 4;
            //dataLoc.NpIndexToExlIndex();

            int dataLocR = recordLoc.NPRowIndex + 1;
            int dataLocC = recordLoc.NPColIndex + 4;
            sheet.SetColStyle(dataLocR, dataLocC, 30, new FontStyle()
            {
                Align = Alignments.右中,
                dataFormat = "0.0000"
            });

            sheet.SetColumValue(dataLocR, dataLocC, dataInput, new FontStyle()
            {
                Align = Alignments.右中
            });


            //最后行
            sheet.SetOneCellValue(recordLoc.NPRowIndex + 31, recordLoc.NPColIndex, "制表：");
            sheet.SetOneCellValue(recordLoc.NPRowIndex + 31, recordLoc.NPColIndex + 3, "测试技术员：");
            sheet.SetOneCellValue(recordLoc.NPRowIndex + 31, recordLoc.NPColIndex + 6, "日期:");
        }
        
    }
}
