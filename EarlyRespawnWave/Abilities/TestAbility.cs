using EarlyRespawnWave.Enums;
using EarlyRespawnWave.Interfaces;
using Exiled.API.Features;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyRespawnWave.Abilities
{
    public class TestAbility : IAbility
    {
        public override string Name { get; set; } = "Test";
        public override string Description { get; set; } = "Test";
        public override AbilityType Type { get; set; } = AbilityType.AlwaysActive;
        public override bool Enabled { get; set; } = true;
        public override void AbilityAdded(Exiled.API.Features.Player p)
        {
            PluginAPI.Core.Log.Debug("Player " + p.Nickname + " has an ability.");
            base.AbilityAdded(p);
        }

        public override void AbilityRemoved(Exiled.API.Features.Player p)
        {
            PluginAPI.Core.Log.Debug("Player " + p.Nickname + " no longer has an ability.");
            base.AbilityRemoved(p);
        }

    }
}
