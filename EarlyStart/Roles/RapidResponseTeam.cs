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
            Content = "<size=25><b>You have spawned as</b></size> <size=25><color=#0e9017><b>Infiltration Insurgency Squad</b></color></size> <size=15><b>from early spawn</b></size>.<br><size=15>Your objective is to terminate any </size><size=15><color=#f7e808>Science Personnel</color></size> <size=15>and</size> <size=15><color=#949a91>Facility Guards</color></size>.<br><size=15>Save any </size><size=15><color=#ffa300>Class D Personnel</color></size> <size=15>to safety. Terminating </size><size=15><color=#bd0606>SCPs</color></size> <size=15>is a last resort.</size>",
            Duration = 10,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
        public override string CustomInfo { get; set; } = "<color=#960018>Rapid Response Team</color>";
        public override List<ItemType> Inventory { get; set; } = new()
        {
            ItemType.GunCrossvec,
            ItemType.KeycardMTFOperative,
            ItemType.Radio,
            ItemType.ArmorCombat,
            ItemType.Medkit,
            ItemType.GrenadeFlash
        };
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
