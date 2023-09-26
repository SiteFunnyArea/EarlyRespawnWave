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
    public class RapidResponseTeam : ICustomRole
    {
        public override string Name { get; set; } = "Rapid Response Team";
        public override Teams Team { get; set; } = Teams.RapidResponseTeam;
        public override RoleTypeId Role { get; set; } = RoleTypeId.NtfPrivate;
        public override float Health { get; set; } = 100;
        public override float MaxHealth { get; set; } = 100;
        public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new()
        {
            Content = "You have spawned as <color=#ff393f>Rapid Response Team</color> from early spawn.<br>Your objective is to terminate <color=#bd0606>SCPs</color> and <color=#0e9017>Chaos Insurgency</color> to secure the site.<br>Also escorting any <color=#f7e808>Science Personnel</color> and complying <color=#ffa300>Class D Personnel</color> to safety.",
            Duration = 10,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
        public override string CustomInfo { get; set; } = "<color=#e50000>Rapid Response Team</color>";
        public override List<ItemType> Inventory { get; set; } = new()
        {
            ItemType.GunCrossvec,
            ItemType.KeycardMTFOperative,
            ItemType.Radio,
            ItemType.ArmorCombat,
            ItemType.Medkit,
            ItemType.GrenadeFlash
        };
        public override List<uint> CustomItemInventory { get; set; }
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
        {
            {
                AmmoType.Nato9,
                50
            }
        };
        public override Vector3 SpawnLocation { get; set; } = new(123.751f, 988.762f, 21.153f);
    }
}
