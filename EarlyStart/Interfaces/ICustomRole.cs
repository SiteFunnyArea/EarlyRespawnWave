using EarlyStart.Enums;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace EarlyStart.Interfaces
{
    public abstract class ICustomRole
    {
        public abstract string Name { get; set; }
        public virtual string Description { get; set; }
        public abstract Teams Team { get; set; }
        public abstract RoleTypeId Role { get; set; }
        public abstract float Health { get; set; }
        public abstract float MaxHealth { get; set; }
        public virtual Exiled.API.Features.Broadcast Broadcast { get; set; }
        public virtual List<Effect> Effects { get; set; }
        public abstract string CustomInfo { get; set; }
        public abstract List<ItemType> Inventory { get;set; }
        public abstract List<uint> CustomItemInventory { get; set; }
        public abstract Dictionary<AmmoType, ushort> Ammo { get; set; }
        public abstract UnityEngine.Vector3 SpawnLocation { get; set; }

    }
}
