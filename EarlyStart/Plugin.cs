using Exiled.API.Features;

namespace EarlyStart
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "SiteFunnyArea";

        public override string Name => "EarlyStart";

        public override string Prefix => Name;

        public override Version RequiredExiledVersion => new Version(8,0,0);

        public override Version Version => new Version(1,0,0);

        public static Plugin Instance;

        private EventHandlers _handlers;

        public bool TimeOver;

        public override void OnEnabled()
        {
            Instance = this;
            TimeOver = false;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;
            TimeOver = false;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();

            Exiled.Events.Handlers.Server.RoundStarted += _handlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound += _handlers.OnRoundRestart;
            Exiled.Events.Handlers.Player.Joined += _handlers.OnJoined;
            Exiled.Events.Handlers.Player.Dying += _handlers.OnDying;
            Exiled.Events.Handlers.Player.ChangingRole += _handlers.OnChangingRole;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= _handlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound -= _handlers.OnRoundRestart;
            Exiled.Events.Handlers.Player.Joined -= _handlers.OnJoined;
            Exiled.Events.Handlers.Player.Dying -= _handlers.OnDying;
            Exiled.Events.Handlers.Player.ChangingRole += _handlers.OnChangingRole;

            _handlers = null;
        }
    }
}