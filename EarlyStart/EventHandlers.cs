using Exiled.API.Features;
using MEC;

namespace EarlyStart
{
    public class EventHandlers
    {
        public void OnRoundStarted()
        {
            Log.Debug($"{Plugin.Instance.Name} Round started.");
            Timing.CallDelayed(Plugin.Instance.Config.Seconds, () =>
            {
                Log.Debug($"{Plugin.Instance.Name} Message recieved.");
            });
        }
    }
}