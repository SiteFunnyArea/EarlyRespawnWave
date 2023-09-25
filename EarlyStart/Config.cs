using Exiled.API.Interfaces;
using System.ComponentModel;

namespace EarlyStart
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
            Content = "<size=30><color=#FFEA00><i>Early Respawn Unit:</i></color><br><color=#076312>F</color><color=#076319>i</color><color=#076320>r</color><color=#076327>s</color><color=#07632E>t</color> <color=#07633C>E</color><color=#076343>a</color><color=#07634A>r</color><color=#076351>l</color><color=#076358>y</color> <color=#076366>W</color><color=#07636D>a</color><color=#076374>v</color><color=#07637B>e</color> <color=#076389>w</color><color=#076390>i</color><color=#076397>l</color><color=#07639E>l</color> <color=#0763AC>c</color><color=#0763B3>o</color><color=#0763BA>m</color><color=#0763C1>m</color><color=#0763C8>e</color><color=#0763CF>n</color><color=#0763D6>c</color><color=#0763DD>e</color> <color=#0763EB>i</color><color=#0763F2>n</color><color=#0763F9>:</color><br><i>{TimeElapsed}</i></size>",
            Duration = (ushort)1f,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
    }
}