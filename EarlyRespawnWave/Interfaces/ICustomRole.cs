﻿using EarlyRespawnWave.Enums;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Core;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace EarlyRespawnWave.Interfaces
{
    public abstract class ICustomRole
    {
        public ICustomRole()
        {
            GetCR = this;
        }

        [YamlIgnore]
        public virtual List<Player> PlayersWhoHaveRole { get; set; } = new();

        public virtual bool Check(Player p) {
            if (PlayersWhoHaveRole.Contains(p))
                return true;
            else
                return false;
        }

        [YamlIgnore]
        public virtual ICustomRole GetCR { get; set; }
        public abstract string Name { get; set; }
        public virtual string Description { get; set; }
        public abstract Teams Team { get; set; } 
        public abstract RoleTypeId Role { get; set; }
        public abstract float Health { get; set; }
        public abstract float MaxHealth { get; set; }
        //public virtual List<IAbility> Abilities { get; set; } = new();
        public virtual Exiled.API.Features.Broadcast Broadcast { get; set; } = new();
        public virtual List<Effect> Effects { get; set; } = new();
        public abstract string CustomInfo { get; set; }
        public abstract List<ItemType> Inventory { get;set; }
        public virtual List<uint> CustomItemInventory { get; set; } = new();
        public abstract Dictionary<AmmoType, ushort> Ammo { get; set; }
        public virtual bool KeycardBypass { get; set; } = false;
        public abstract UnityEngine.Vector3 SpawnLocation { get; set; }
        public virtual bool IsGodMode { get; set; } = false;
        public virtual void RoleAdded(Player p) { PluginAPI.Core.Log.Debug("Gave role " + Name + " to " + p.Nickname); }
        public virtual void RoleRemoved(Player p) { PluginAPI.Core.Log.Debug("Removed role " + Name + " from " + p.Nickname); }
        public virtual void SubscribeEvent() { }
        public virtual void UnsubscribeEvent() { }

    }
}