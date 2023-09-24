using Exiled.API.Features;

namespace EarlyStart
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "SiteFunnyArea";

        public override string Name => "EarlyStart";

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();
        }

        private void UnregisterEvents()
        {
            _handlers = null;
        }
    }
}