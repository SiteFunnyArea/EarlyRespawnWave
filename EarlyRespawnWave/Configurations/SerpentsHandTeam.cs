using EarlyRespawnWave.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyRespawnWave.Configurations
{
    public class SerpentsHandTeam
    {
        public CultLeader SHLeader { get; set; } = new CultLeader();
        public CultSilencer SHSilencer { get; set; } = new CultSilencer();
        public CultEngineer SHEngineer { get; set; } = new CultEngineer();
        public CultPhantom SHPhantom { get; set; } = new CultPhantom();
        public CultSavage SHSavage { get; set; } = new CultSavage();
        public CultCollector SHCollector { get; set; } = new CultCollector();
        public CultDestroyer SHDestroyer { get; set; } = new CultDestroyer();
        public CultConscript SHConscript { get; set; } = new CultConscript();

    }
}
