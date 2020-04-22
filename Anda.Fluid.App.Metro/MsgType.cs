using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.Metro
{
    class MsgType
    {
        public const string IDLE = "idle";
        public const string RUNNING = "running";
        public const string PAUSED = "paused";
        public const string BUSY = "busy";

        public const string ENTER_EDIT = "enter_edit";
        public const string ENTER_MAIN = "enter_main";
        public const string EXIT = "exit";

        public const string RUNINFO_PROGRAM = "runinfo_program";
        public const string RUNINFO_START_DATETIME = "runinfo_start_datetime";
        public const string RUNINFO_RESULT = "runinfo_result";

        public const string MANUAL_PURGE = "manual purge";
        public const string MANUAL_PRIMER = "manual primer";
        public const string MANUAL_SCALE = "manual scale";
        public const string MANUAL_HEATER = "manual heater";
    }
}
