using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.Handlers;
using MEC;
using PluginAPI.Core;

namespace EarlyStart
{
    public class EventHandlers
    {

        public float TimeElapsed = Plugin.Instance.Config.Seconds;
        private static CoroutineHandle _timerCoroutine;

        public void OnRoundStarted()
        {

            PluginAPI.Core.Log.Debug($"{Plugin.Instance.Name} Round started.");
            _timerCoroutine = Timing.RunCoroutine(TimerCoroutine());
            Timing.CallDelayed(Plugin.Instance.Config.Seconds, () =>
            {
                 Plugin.Instance.TimeOver = true;
                 foreach(Exiled.API.Features.Player p in Exiled.API.Features.Player.Get(PlayerRoles.RoleTypeId.Spectator)){
                    PluginAPI.Core.Log.Debug("user detected " + p.Nickname + " with info " + p.UniqueRole);
                 }
            });
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.UniqueRole.Contains("-SpawnAs") && ev.NewRole != PlayerRoles.RoleTypeId.Spectator)
            {
                ev.Player.UniqueRole = "";
            }
        }

        public void OnRoundEnded(RoundEndedEventArgs ev) {
            
            Plugin.Instance.TimeOver = false;
            TimeElapsed = Plugin.Instance.Config.Seconds;
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
                    ev.Player.UniqueRole = ev.Player.UniqueRole + "-SpawnAs CIS";
                }
                PluginAPI.Core.Log.Debug(ev.Player.UniqueRole);
                Hint h = new();
                h.Content = $"You have died, you will respawn in less than {TimeElapsed.ToString()} seconds.";
                h.Duration = 1;

                ev.Player.ShowHint(h);
            }
        }

        public IEnumerator<float> TimerCoroutine()
        {
            yield return Timing.WaitForSeconds(1f);

            while (true)
            {
                if(TimeElapsed == 0)
                    break;
                if (Exiled.API.Features.Round.IsEnded)
                    break;
                yield return Timing.WaitForSeconds(1f);
                
                TimeElapsed--;
                PluginAPI.Core.Log.Debug(TimeElapsed.ToString());


                
            }

        }

    }
}