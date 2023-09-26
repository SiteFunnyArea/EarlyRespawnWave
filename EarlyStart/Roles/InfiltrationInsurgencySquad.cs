using EarlyStart.Enums;
using EarlyStart.Interfaces;
using Exiled.API.Enums;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EarlyStart.Roles
{
    public class InfiltrationInsurgencySquad : ICustomRole
    {
        public override string Name { get; set; } = "Infiltration Insurgency Squad";
        public override Teams Team { get; set; } = Teams.InfiltrationInsurgencySquad;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosConscript;
        public override float Health { get; set; } = 100;
        public override float MaxHealth { get; set; } = 100;
        public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new()
        {
            Content = "You have spawned as <color=#0e9017>Infiltration Insurgency Squad</color> from early spawn.<br>Your objective is to terminate any <color=#f7e808>Science Personnel</color> and <color=#949a91>Facility Guards</color>.<br>Save any <color=#ffa300>Class D Personnel</color> to safety. Terminating <color=#bd0606>SCPs</color> is a last resort.",
            Duration = 10,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
        public override string CustomInfo { get; set; } = "<color=#228B22>Infiltration Insurgency Squad</color>";
        public override List<ItemType> Inventory { get; set; } = new()
        {
            ItemType.GunAK,
            ItemType.KeycardChaosInsurgency,
            ItemType.Medkit,
            ItemType.Painkillers,
            ItemType.GrenadeFlash,
            ItemType.ArmorCombat
        };
        public override List<uint> CustomItemInventory { get; set; } = new()
        {
            14
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
        {
            {
                AmmoType.Nato762,
                50
            }
        };
        public override Vector3 SpawnLocation { get; set; } = new(29.579f, 991.885f, -25.604f);
    }
}
