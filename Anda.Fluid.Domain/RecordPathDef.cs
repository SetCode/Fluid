using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain
{
    public static class RecordPathDef
    {
        private const string ROOT = "E:\\Record";

        public static string Root()
        {
            return createDir(ROOT);
        }

        public static string ProgramPath(string programName)
        {
            return createDir(Root() + "\\" + programName);
        }

        public static string Program_Marks(string programName, string markDir)
        {
            return createDir(ProgramPath(programName) + "\\" + markDir);
        }

        public static string Program_Marks_Date(string programName, string markDir)
        {
            return createDir(Program_Marks(programName, markDir) + "\\" + DateTime.Today.ToString("yyyyMMdd"));
        }

        public static string Program_Barcodes(string programName)
        {
            return createDir(ProgramPath(programName) + "\\Barcodes");
        }

        private static string createDir(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch(Exception ex)
            {
                Logger.DEFAULT.Crash(ex);
            }
            return dir;
        }
    }
}
