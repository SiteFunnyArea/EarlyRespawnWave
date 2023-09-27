using EarlyRespawnWave.Managers;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.Handlers;
using MEC;
using PluginAPI.Core;

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
        SpawnManager spawn;

        public void OnRoundStarted()
        {
            SpawnManager spawn = Plugin.Instance.sM;
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
                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.BothCassie;
                    Subtitles = "Attention, all personnel: Rapid Response Team has entered the Facility. Chaos Insurgency threat has also been detected, leave the facility with caution.";
                }
                else if(RRTSpawn > 0 && IISSpawn <= 0) {
                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie;
                    Subtitles = "Attention, all personnel: Rapid Response Team has entered the Facility. They will escort Foundation personnel out shortly.";
                }
                else if (IISSpawn > 0 && RRTSpawn <= 0)
                {
                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie;
                    Subtitles = "Attention, all personnel: Chaos Insurgency has been detected on Surface Zone. Please head to a nearby evacuation zone nearest to you.";
                }

                if (PreferredAnnounement.Count() > 0)
                    Exiled.API.Features.Cassie.MessageTranslated(PreferredAnnounement, Subtitles, false, true, true);
                
            });
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
                PluginAPI.Core.Log.Debug(ev.Player.UniqueRole);
            }
        }

        public IEnumerator<float> TimerCoroutine()
        {
            while (true)
            {
                if(TimeElapsed == 0)
                    break;
                if (Exiled.API.Features.Round.IsEnded)
                    break;
                if (Plugin.Instance.TimeOver == true)
                    break;
                foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator))
                {
                    Exiled.API.Features.Broadcast b = new();
                    b.Content = Plugin.Instance.Config.RespawnBroadcast.Content.Replace("{TimeElapsed}", TimeElapsed.ToString());
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