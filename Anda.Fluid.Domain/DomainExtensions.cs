using Anda.Fluid.Infrastructure.Data;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain
{
    public static class DomainExtensions
    {
        public static void SaveMarkImage(this Bitmap bmp, string programName, string markDir, string markTag)
        {
            if (bmp == null)
            {
                return;
            }
            string filePath = RecordPathDef.Program_Marks_Date(programName, markDir)
                + "\\" + markTag + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            DataServer.Instance.Fire(() =>
            {
                try
                {
                    bmp.Save(filePath, ImageFormat.Jpeg);
                }
                catch (Exception e)
                {
                    Log.Dprint("Mark Image Save error :" + e.Message);
                }
            });
        }

        public static void SaveMarkImage(this byte[] bytes, int width, int height, string programName, string markDir, string markTag)
        {
            if(bytes == null)
            {
                return;
            }
            bytes.ToBitmap(width, height).SaveMarkImage(programName, markDir, markTag);
        }
    }
}
