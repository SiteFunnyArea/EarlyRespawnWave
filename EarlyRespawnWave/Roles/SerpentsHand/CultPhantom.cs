using EarlyRespawnWave.Enums;
using EarlyRespawnWave.Interfaces;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Scp173;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EarlyRespawnWave.Roles
{
    public class CultPhantom : ICustomRole
    {
        public override string Name { get; set; } = "Serpents Hand Cult Phantom";
        public override Teams Team { get; set; } = Teams.SerpentsHand;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        public SHTeamPersonnel Personnel { get; set; } = SHTeamPersonnel.Phantom;
        public override float Health { get; set; } = 100;
        public override float MaxHealth { get; set; } = 100;
        public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new()
        {
            Content = "You have spawned as <color=#55da71>Serpents</color> <color=#f02d0f>Hand</color> from early spawn.<br>Your objective is to terminate any personnel, including <color=#ffa300>Class D Personnel</color> and <color=#0e9017>Chaos Insurgency</color> to save the <color=#bd0606>SCPs</color>.<br>Work with the other <color=#bd0606>SCPs</color> to dominate the site!",
            Duration = 10,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
        public override string CustomInfo { get; set; } = "Serpents Hand Cult Phantom";
        public override List<ItemType> Inventory { get; set; } = new()
        {
            ItemType.GunShotgun,
            ItemType.GunRevolver,
            ItemType.Medkit,
            ItemType.Radio,
            ItemType.ArmorCombat,
            ItemType.KeycardChaosInsurgency,
        };
        public override List<uint> CustomItemInventory { get; set; } = new()
        {
            203
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
        {
            {
                AmmoType.Ammo12Gauge,
                24
            }
        };

        public override void SubscribeEvent()
        {

            base.SubscribeEvent();
        }

        public override void UnsubscribeEvent()
        {

            base.UnsubscribeEvent();

        }

        public override void OnKillingPlayer(DyingEventArgs ev)
        {
            if (ev.Attacker != null && Check(ev.Attacker))
            {
                ev.Attacker.EnableEffect(EffectType.Invisible, 5);
            }

            base.OnKillingPlayer(ev);
        }
        public override Vector3 SpawnLocation { get; set; } = new(62.777f, 991.648f, -50.397f);
    }
}
