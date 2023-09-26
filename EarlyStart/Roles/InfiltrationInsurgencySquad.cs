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
            Content = "<size=25><b>You have spawned as</b></size> <size=25><color=#ff393f><b>Rapid Response Team</b></color></size> <size=25><b>from early spawn.</b></size><br><size=15>Your objective is to terminate </size><size=15><color=#bd0606>SCPs</color></size> <size=15>and</size> <size=15><color=#0e9017>Chaos Insurgency</color></size> <size=15>to secure the site.</size><br><size=15>Also escorting any</size> <size=15><color=#f7e808>Science Personnel</color></size> <size=15>and complying</size> <size=15><color=#ffa300>Class D Personnel</color></size> <size=15>to safety.</size>",
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
