using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyStart.Configurations
{
    public class CassieAnnouncements
    {
        [Description("Announcement for when only IIS spawns")]
        public string IISOnlyCassie { get; set; } = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Unauthorized Military Personnel has been detected .g2 .g6 .g3 on Surface Zone";
        [Description("Announcement for when both IIS and RRT spawns")]
        public string BothCassie { get; set; } = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Authorized Military Personnel HASENTERED .g2 .g6 .g3 ChaosInsurgency threat detected at Surface Zone";
        [Description("Announcement for when only RRT spawns")]
        public string RRTOnlyCassie { get; set; } = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Authorized Military Personnel HASENTERED";

    }
}
