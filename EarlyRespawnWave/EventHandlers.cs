using EarlyRespawnWave.Interfaces;
using EarlyRespawnWave.Managers;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.Handlers;
using MEC;
using PluginAPI.Core;
using Utils.NonAllocLINQ;

namespace EarlyRespawnWave
{
    public class EventHandlers
    {

        public float TimeElapsed = Plugin.Instance.Config.Seconds;
        private static CoroutineHandle _timerCoroutine;
        public int Waves;

        public int IISSpawn;
        public int RRTSpawn;
        public string PreferredAnnounement = "";
        public string Subtitles = "";
        public List<ICustomRole> SHQueue;
        SpawnManager spawn;

        public void OnRoundStarted()
        {
            SHQueue = new List<ICustomRole>();
            SpawnManager spawn = Plugin.Instance.sM;
            Waves += 1;
            _timerCoroutine = Timing.RunCoroutine(TimerCoroutine());
            Timing.CallDelayed(Plugin.Instance.Config.Seconds, () =>
            {
                Plugin.Instance.TimeOver = true;
                if (_timerCoroutine.IsRunning)
                    Timing.KillCoroutines(_timerCoroutine);

                foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator)){

                    //PluginAPI.Core.Log.Debug("user detected " + p.Nickname + " with info " + p.UniqueRole);
                    if (p.UniqueRole.Contains("-SpawnAs"))
                    {
                        if (p.UniqueRole.Contains("RRT"))
                        {
                            spawn.SpawnClass(Plugin.Instance.Config.RapidResponseTeam, p);
                            RRTSpawn+= 1;
                        }else if (p.UniqueRole.Contains("IIS"))
                        {
                            spawn.SpawnClass(Plugin.Instance.Config.InfiltrationInsurgencySquad, p);
                            IISSpawn += 1;

                        }else if (p.UniqueRole.Contains("None"))
                        {
                            int chance = UnityEngine.Random.Range(0, 100);
                            if(chance <= 50) {
                                spawn.SpawnClass(Plugin.Instance.Config.InfiltrationInsurgencySquad, p);
                                RRTSpawn += 1;
                            } else
                            {
                                spawn.SpawnClass(Plugin.Instance.Config.RapidResponseTeam, p);
                                IISSpawn += 1;
                            }
                        }
                    }
                 }

                if(IISSpawn > 0 && RRTSpawn > 0) {
                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.BothCassie.CassieAnnouncement;
                    Subtitles = Plugin.Instance.Config.CassieAnnouncements.BothCassie.CassieSubtitle;
                }
                else if(RRTSpawn > 0 && IISSpawn <= 0) {
                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieAnnouncement;
                    Subtitles = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieSubtitle;
                }
                else if (IISSpawn > 0 && RRTSpawn <= 0)
                {
                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieAnnouncement;
                    Subtitles = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieSubtitle;
                }

                if (PreferredAnnounement.Count() > 0)
                    Exiled.API.Features.Cassie.MessageTranslated(PreferredAnnounement, Subtitles, false, true, true);
                
            });
        }
        
        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            if(Waves < 2)
            {
                Plugin.Instance.TimeOver = false;
                Waves += 1;
                TimeElapsed = Plugin.Instance.Config.Seconds;
                _timerCoroutine = Timing.RunCoroutine(TimerCoroutine());
                Timing.CallDelayed(Plugin.Instance.Config.Seconds, () => {
                    Plugin.Instance.TimeOver = true;
                    if (_timerCoroutine.IsRunning)
                        Timing.KillCoroutines(_timerCoroutine);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHLeader);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHSilencer);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHEngineer);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHPhantom);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHSavage);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHCollector);
                    SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHDestroyer);
                    

                        foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator))
                        {
                            ICustomRole? i = SHQueue[0];
                            if(i != null)
                            {
                                spawn.SpawnClass(i, p);
                                SHQueue.Remove(i);
                            }
                        }
                    
                    if(SHQueue.Count > 0)
                    {
                        SHQueue.Clear();
                    }
                });
            }
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {

            if (ev.Player.UniqueRole.Contains("-SpawnAs") && ev.NewRole != PlayerRoles.RoleTypeId.Spectator)
            {
                ev.Player.UniqueRole = "";
                ev.Player.ClearBroadcasts();
            }

        }

        public void OnRoundRestart()
        {

            Plugin.Instance.TimeOver = false;
            TimeElapsed = Plugin.Instance.Config.Seconds;
            IISSpawn = 0;
            RRTSpawn = 0;
            SHQueue = null;
            PreferredAnnounement = "";
            Subtitles = "";
            if (_timerCoroutine.IsRunning)
                Timing.KillCoroutines(_timerCoroutine);
        }

        public void OnJoined(JoinedEventArgs ev)
        {
            if(Plugin.Instance.TimeOver == false)
            {
                ev.Player.UniqueRole = ev.Player.UniqueRole + "-SpawnAs None";
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            if(Plugin.Instance.TimeOver == false)
            {
                if(ev.Player.Role.Team == PlayerRoles.Team.FoundationForces || ev.Player.Role.Team == PlayerRoles.Team.Scientists)
                {
                    ev.Player.UniqueRole = ev.Player.UniqueRole + "-SpawnAs RRT";
                }else if (ev.Player.Role.Team == PlayerRoles.Team.ClassD || ev.Player.Role.Team == PlayerRoles.Team.SCPs)
                {
                    ev.Player.UniqueRole = ev.Player.UniqueRole + "-SpawnAs IIS";
                }
            }

            //if (spawn.CheckPlayerForRole(ev.Player) != null)
            //{
            //    ev.Player.UniqueRole = "";
            //    ev.Player.CustomInfo = "";
            //    spawn.RemoveRole(ev.Player);
            //}
        }

        public IEnumerator<float> TimerCoroutine()
        {
            while (true)
            {
                if (Waves > 2)
                    break;
                if(TimeElapsed == 0)
                    break;
                if (Exiled.API.Features.Round.IsEnded)
                    break;
                if (Plugin.Instance.TimeOver == true)
                    break;
                foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator))
                {
                    string Prefix = "";
                    if (Waves == 1) Prefix = "<color=#076312>F</color><color=#076319>i</color><color=#076320>r</color><color=#076327>s</color><color=#07632E>t</color>";
                    if (Waves == 2) Prefix = "<color=#076312>S</color><color=#076316>e</color><color=#07631A>c</color><color=#07631E>o</color><color=#076322>n</color><color=#076326>d</color>";

                    Exiled.API.Features.Broadcast b = new();
                    b.Content = Plugin.Instance.Config.RespawnBroadcast.Content.Replace("{TimeElapsed}", (TimeElapsed-2).ToString()).Replace("{SpawnWave}", Prefix);
                    b.Duration = Plugin.Instance.Config.RespawnBroadcast.Duration;
                    b.Show = Plugin.Instance.Config.RespawnBroadcast.Show;
                    b.Type = Plugin.Instance.Config.RespawnBroadcast.Type;
                    p.Broadcast(b);
                }
                yield return Timing.WaitForSeconds(1f);
                
                TimeElapsed--;
            }

        }

    }
}