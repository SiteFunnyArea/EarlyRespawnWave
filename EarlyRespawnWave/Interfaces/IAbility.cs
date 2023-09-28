using EarlyRespawnWave.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace EarlyRespawnWave.Interfaces
{
    public abstract class IAbility
    {
        public IAbility()
        {
        }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract AbilityType Type { get; set; }
        public abstract bool Enabled { get; set; }
        public virtual void AbilityAdded(Player p)
        {
        }

        public virtual void AbilityRemoved(Player p)
        {
        }

        public virtual void SubscribeToEvents()
        {
        }

        public virtual void UnsubscribeToEvents()
        {
        }
    }
}
