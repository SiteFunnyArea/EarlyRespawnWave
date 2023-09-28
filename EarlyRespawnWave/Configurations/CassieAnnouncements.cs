using EarlyRespawnWave.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyRespawnWave.Configurations
{
    public class CassieAnnouncements
    {
        [Description("Announcement for when only Infiltration Insurgency Squad spawns")]
        public CASSIEAnnouncer IISOnlyCassie { get; set; } = new() { 
            CassieAnnouncement = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Unauthorized Military Personnel has been detected .g2 .g6 .g3 on Surface Zone",
            CassieSubtitle = "Attention, all personnel: Chaos Insurgency has been detected on Surface Zone. Please head to a nearby evacuation zone nearest to you."
        };
        [Description("Announcement for when both Infiltration Insurgency Squad and Rapid Response Team spawns")]
        public CASSIEAnnouncer BothCassie { get; set; } = new() { 
            CassieAnnouncement = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Authorized Military Personnel HASENTERED .g2 .g6 .g3 ChaosInsurgency threat detected at Surface Zone",
            CassieSubtitle = "Attention, all personnel: Rapid Response Team has entered the Facility. Chaos Insurgency threat has also been detected, leave the facility with caution."
        };
        [Description("Announcement for when only Rapid Response Team spawns")]
        public CASSIEAnnouncer RRTOnlyCassie { get; set; } = new()
        {
            CassieAnnouncement = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Authorized Military Personnel HASENTERED",
            CassieSubtitle = "Attention, all personnel: Rapid Response Team has entered the Facility. They will escort Foundation personnel out shortly."
        };
        [Description("Announcement for when only Serpents Hand spawns")]
        public CASSIEAnnouncer SHCassie { get; set; } = new()
        {
            CassieAnnouncement = "pitch_0.5 .g5 .g5 pitch_1.0 Attention, all personnel. Authorized Military Personnel HASENTERED",
            CassieSubtitle = "Attention, all personnel: Rapid Response Team has entered the Facility. They will escort Foundation personnel out shortly."
        };
    }
}
