using EarlyRespawnWave.Enums;
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

        public int IISTickets = 50;
        public int RRTTickets = 50;
        public string PreferredAnnounement = "";
        public string Subtitles = "";
        public Teams PreferredSpawn;
        //public int SHTeamRespawnCount;
        //public List<ICustomRole> SHQueue;
        SpawnManager spawn;
        public int AmountSpawned = 0;
        public void OnRoundStarted()
        {
            //SHTeamRespawnCount = 0;
            //SHQueue = new List<ICustomRole>();
            spawn = Plugin.Instance.sM;
            Waves += 1;
            _timerCoroutine = Timing.RunCoroutine(TimerCoroutine());
            Timing.CallDelayed(Plugin.Instance.Config.Seconds, () =>
            {
                Plugin.Instance.TimeOver = true;
                if (_timerCoroutine.IsRunning)
                    Timing.KillCoroutines(_timerCoroutine);

                if (IISTickets == RRTTickets)
                {
                    int chance = UnityEngine.Random.Range(0, 100);
                    if (chance <= 50)
                    {
                        PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieAnnouncement;
                        Subtitles = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieSubtitle;

                        PreferredSpawn = Teams.InfiltrationInsurgencySquad;
                    }
                    else
                    {
                        PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieAnnouncement;
                        Subtitles = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieSubtitle;

                        PreferredSpawn = Teams.RapidResponseTeam;
                    }
                }
                else if (RRTTickets > IISTickets)
                {
                    PreferredSpawn = Teams.RapidResponseTeam;

                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieAnnouncement;
                    Subtitles = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieSubtitle;
                }
                else if (RRTTickets < IISTickets)
                {
                    PreferredSpawn = Teams.InfiltrationInsurgencySquad;

                    PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieAnnouncement;
                    Subtitles = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieSubtitle;
                }


                foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator)){
                    //PluginAPI.Core.Log.Debug("user detected " + p.Nickname + " with info " + p.UniqueRole);
                    AmountSpawned += 1;
                        if (PreferredSpawn == Teams.RapidResponseTeam)
                        {
                            spawn.SpawnClass(Plugin.Instance.Config.RapidResponseTeam, p);
                        }
                        else if (PreferredSpawn == Teams.InfiltrationInsurgencySquad)
                        {
                            spawn.SpawnClass(Plugin.Instance.Config.InfiltrationInsurgencySquad, p);
                        }
              
                 }


                if (PreferredAnnounement.Count() > 0 && AmountSpawned > 0)
                {
                    Exiled.API.Features.Cassie.MessageTranslated(PreferredAnnounement, Subtitles, false, true, true);
                }

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
                Timing.CallDelayed(Plugin.Instance.Config.Seconds, () =>
                {
                    Plugin.Instance.TimeOver = true;
                    if (_timerCoroutine.IsRunning)
                        Timing.KillCoroutines(_timerCoroutine);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHLeader);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHSilencer);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHEngineer);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHPhantom);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHSavage);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHCollector);
                    //SHQueue.Add(Plugin.Instance.Config.SerpentsHand.SHDestroyer);

                    if (IISTickets == RRTTickets)
                    {
                        int chance = UnityEngine.Random.Range(0, 100);
                        if (chance <= 50)
                        {
                            PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieAnnouncement;
                            Subtitles = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieSubtitle;

                            PreferredSpawn = Teams.InfiltrationInsurgencySquad;
                        }
                        else
                        {
                            PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieAnnouncement;
                            Subtitles = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieSubtitle;

                            PreferredSpawn = Teams.RapidResponseTeam;
                        }
                    }
                    else if (RRTTickets > IISTickets)
                    {
                        PreferredSpawn = Teams.RapidResponseTeam;

                        PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieAnnouncement;
                        Subtitles = Plugin.Instance.Config.CassieAnnouncements.RRTOnlyCassie.CassieSubtitle;
                    }
                    else if (RRTTickets < IISTickets)
                    {
                        PreferredSpawn = Teams.InfiltrationInsurgencySquad;

                        PreferredAnnounement = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieAnnouncement;
                        Subtitles = Plugin.Instance.Config.CassieAnnouncements.IISOnlyCassie.CassieSubtitle;
                    }


                    foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator))
                    {
                        //PluginAPI.Core.Log.Debug("user detected " + p.Nickname + " with info " + p.UniqueRole);
                        AmountSpawned += 1;
                        if (PreferredSpawn == Teams.RapidResponseTeam)
                        {
                            spawn.SpawnClass(Plugin.Instance.Config.RapidResponseTeam, p);
                        }
                        else if (PreferredSpawn == Teams.InfiltrationInsurgencySquad)
                        {
                            spawn.SpawnClass(Plugin.Instance.Config.InfiltrationInsurgencySquad, p);
                        }

                    }


                    if (PreferredAnnounement.Count() > 0 && AmountSpawned > 0)
                    {
                        Exiled.API.Features.Cassie.MessageTranslated(PreferredAnnounement, Subtitles, false, true, true);
                    }

                    //foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator))
                    //{
                        //    if(SHTeamRespawnCount < 7)
                        //{
                        //        ICustomRole? ICR = SHQueue[0];
                        //        PluginAPI.Core.Log.Debug(ICR.Name + " will be given to " + p.Nickname);
                        //        spawn.SpawnClass(ICR, p);

                        //        SHQueue.Remove(ICR);
                        //        SHTeamRespawnCount++;
                        //    }

                        // }

                        //if (Exiled.API.Features.Player.List.Count(t => t.CustomInfo.Contains("Serpents Hand")) > 0)
                        //    {
                        //        Exiled.API.Features.Cassie.MessageTranslated(Plugin.Instance.Config.CassieAnnouncements.SHCassie.CassieAnnouncement, Plugin.Instance.Config.CassieAnnouncements.SHCassie.CassieSubtitle);
                        //    }


                        //if(SHQueue.Count > 0)
                        //{
                        //    SHQueue.Clear();
                        //}
                    //}
                });
            }
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            ev.Player.UniqueRole = "";
                    
            ev.Player.ClearBroadcasts();

            //if (spawn.CheckPlayerForRole(ev.Player) != null)
            //{
            //    spawn.RemoveRole(ev.Player);
            //}
        }

        public void OnRoundRestart()
        {

            Plugin.Instance.TimeOver = false;
            TimeElapsed = Plugin.Instance.Config.Seconds;
            IISTickets = 50;
            RRTTickets = 50;
            //SHQueue = null;
            Waves = 0;
            AmountSpawned = 0;
            //SHTeamRespawnCount = 0;
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
                    if (ev.DamageHandler.Attacker == null)
                    {
                        RRTTickets -= 1;
                        IISTickets += 1;
                    }

                    if (ev.Attacker.Role.Team == PlayerRoles.Team.SCPs || ev.Attacker.Role.Team == PlayerRoles.Team.ClassD || ev.Attacker.Role.Team == PlayerRoles.Team.ChaosInsurgency)
                    {
                        RRTTickets += 1;
                        IISTickets -= 1;
                    }
                }else if (ev.Player.Role.Team == PlayerRoles.Team.ClassD || ev.Player.Role.Team == PlayerRoles.Team.SCPs)
                {
                    if (ev.DamageHandler.Attacker == null)
                    {
                        RRTTickets += 1;
                        IISTickets -= 1;
                    }

                    if (ev.Attacker.Role.Team == PlayerRoles.Team.FoundationForces || ev.Attacker.Role.Team == PlayerRoles.Team.Scientists)
                    {
                        RRTTickets -= 1;
                        IISTickets += 1;
                    } 
                }
            }

            if (spawn.CheckPlayerForRole(ev.Player) != null)
            {
                spawn.RemoveRole(ev.Player);
            }
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
                    if (Plugin.TimerHidden.Contains(p.UserId))
                        continue;

                    string Prefix = "";
                    string OfficialText = "";
                    if (Waves == 1) {
                        Prefix = "<color=#076312>F</color><color=#076319>i</color><color=#076320>r</color><color=#076327>s</color><color=#07632E>t</color>";
                        OfficialText = Plugin.Instance.Config.RespawnBroadcast.Content + "<br></i></size><size=20><color=#FF0000>RRT:</color> <color=white>{RRT}</color> - <color=#07633C>IIS:</color> <color=white>{IIS}</color></size>";
                    }
                    if (Waves == 2) {
                        Prefix = "<color=#076312>S</color><color=#076316>e</color><color=#07631A>c</color><color=#07631E>o</color><color=#076322>n</color><color=#076326>d</color>";
                        OfficialText = Plugin.Instance.Config.RespawnBroadcast.Content + "<br></i></size><size=20><color=#FF0000>RRT:</color> <color=white>{RRT}</color> - <color=#07633C>IIS:</color> <color=white>{IIS}</color></size>";
                    } 

                    Exiled.API.Features.Broadcast b = new();
                    b.Content = OfficialText.Replace("{TimeElapsed}", (TimeElapsed-1).ToString()).Replace("{SpawnWave}", Prefix).Replace("{RRT}",RRTTickets.ToString()).Replace("{IIS}", IISTickets.ToString());
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