using System;
using Exiled.API.Features;
using HarmonyLib;

namespace Exiled.Template.Patches
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "Your name";

        public override string Name => "Exiled.Template.Patches";

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        private Harmony _harmony;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnpatchAll();

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

        private void PatchAll()
        {
            _harmony = new Harmony($"Exiled.Template.Patches.{DateTime.UtcNow.Ticks}");
            _harmony.PatchAll();
        }

        private void UnpatchAll()
        {
            _harmony?.UnpatchAll(_harmony.Id);
            _harmony = null;
        }
    }
}