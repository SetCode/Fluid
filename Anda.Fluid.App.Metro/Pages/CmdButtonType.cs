using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.Metro.Pages
{
    public enum CmdButtonType
    {
        None,

        New,
        Open,
        Save,
        Copy,
        Cut,
        Paste,
        Config,
        EditOrigin,
        Start,
        Step,
        Pause,
        Abort,
        ConveyorSelect,
        ConveyorWidth,
        Locations,
        Inspect,
        BatchUpdate,

        NewPattern,
        DoPattern,
        DoMultipass,
        Disable,
        Comments,
        Mark,
        AsvMark,
        BadMark,
        NozzleCheck,
        Height,
        Timer,
        LoopPass,
        PassBlock,
        MultipassArray,
        MultipassTimer,
        Purge,
        SvSpeed,
        MoveAbsXy,
        MoveAbsZ,
        MoveXy,
        MoveLoc,

        Dot,
        Line,
        Polyline,
        Arc,
        Circle,
        Snakeline,
        PatternArray,
        PatternsArray,
        FinishShot
    }
}
