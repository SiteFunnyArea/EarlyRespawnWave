using EarlyRespawnWave.Abilities;
using EarlyRespawnWave.Configurations;
using EarlyRespawnWave.Interfaces;
using EarlyRespawnWave.Roles;
using Exiled.API.Interfaces;
using System.ComponentModel;

namespace EarlyRespawnWave
{
    public class Config : IConfig
    {
        [Description("Indicates if the plugin is enabled or not")]
        public bool IsEnabled { get; set; } = true;
        [Description("Enables/disables debugging for this plugin")]
        public bool Debug { get; set; } = false;
        [Description("How many seconds should occur before a early respawn wave occurs.")]
        public float Seconds { get; set; } = 150f;
        [Description("Broadcast that will show while waiting to be respawned")]
        public Exiled.API.Features.Broadcast RespawnBroadcast { get; set; } = new()
        {
            Content = "<size=30><color=#FFEA00><i>Early Respawn Unit:</i></color><br>{SpawnWave} <color=#07633C>E</color><color=#076343>a</color><color=#07634A>r</color><color=#076351>l</color><color=#076358>y</color> <color=#076366>W</color><color=#07636D>a</color><color=#076374>v</color><color=#07637B>e</color> <color=#076389>w</color><color=#076390>i</color><color=#076397>l</color><color=#07639E>l</color> <color=#0763AC>c</color><color=#0763B3>o</color><color=#0763BA>m</color><color=#0763C1>m</color><color=#0763C8>e</color><color=#0763CF>n</color><color=#0763D6>c</color><color=#0763DD>e</color> <color=#0763EB>i</color><color=#0763F2>n</color><color=#0763F9>:</color><br><i>{TimeElapsed}</i></size>",
            Duration = 1,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
        [Description("the CASSIE messages that will play depending on the scenario")]
        public CassieAnnouncements CassieAnnouncements { get; set; } = new();
        [Description("The RapidResponseTeam role")]
        public RapidResponseTeam RapidResponseTeam { get; set; } = new();
        [Description("The InfiltrationInsurgencySquad role")]
        public InfiltrationInsurgencySquad InfiltrationInsurgencySquad { get; set; } = new();
        [Description("Serpents Hand Roles")]
        public SerpentsHandTeam SerpentsHand { get; set; } = new();
    }
}