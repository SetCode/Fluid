using Anda.Fluid.Drive.Motion.Locations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RobotSystemLoc
    {
        public static readonly string Key_Mechanical_Zero = "Mechanical Zero";
        //public static readonly string Key_WorkSpace_Zero = "WorkSpace Zero";
        //public static readonly string Key_Post_Run_Park_Loc = "Post Run Park Loc";
        public static readonly string Key_Purge_Loc = "Purge Loc";
        public static readonly string Key_Scale_Loc = "Scale Loc";
        public static readonly string Key_Prime_Loc = "Prime Loc";
        public static readonly string Key_Scrape_Loc = "Scrape Loc";
        public static readonly string Key_Soak_Loc = "Soak Loc";
        public RobotSystemLoc()
        {
            All.Add(MechanicalZero);
            //All.Add(WorkSpaceZero);
            //All.Add(PostRunParkLoc);
            All.Add(PurgeLoc);
            All.Add(ScaleLoc);
            All.Add(PrimeLoc);
            All.Add(ScrapeLoc);
        }

        public List<Location> All { get; private set; } = new List<Location>();

        [JsonProperty]
        public Location MechanicalZero { get; private set; } = new Location(Key_Mechanical_Zero, true);

        //[JsonProperty]
        //public Location WorkSpaceZero { get; private set; } = new Location(Key_WorkSpace_Zero, true);

        //[JsonProperty]
        //public Location PostRunParkLoc { get; private set; } = new Location(Key_Post_Run_Park_Loc, true);

        [JsonProperty]
        public Location PurgeLoc { get; private set; } = new Location(Key_Purge_Loc, true);

        [JsonProperty]
        public Location ScaleLoc { get; private set; } = new Location(Key_Scale_Loc, true);

        [JsonProperty]
        public Location PrimeLoc { get; private set; } = new Location(Key_Prime_Loc, true);

        [JsonProperty]
        public Location ScrapeLoc { get; private set; } = new Location(Key_Scrape_Loc, true);

        [JsonProperty]
        public Location SoakLoc { get; private set; } = new Location(Key_Soak_Loc, true);
    }
}
