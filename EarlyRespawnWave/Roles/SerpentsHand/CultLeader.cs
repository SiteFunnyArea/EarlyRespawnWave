﻿using EarlyRespawnWave.Enums;
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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EarlyRespawnWave.Roles
{
    public class CultLeader : ICustomRole
    {
        public override string Name { get; set; } = "Serpents Hand Cult Leader";
        public override Teams Team { get; set; } = Teams.SerpentsHand;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        public SHTeamPersonnel Personnel { get; set; } = SHTeamPersonnel.Leader;
        public override float Health { get; set; } = 100;
        public override float MaxHealth { get; set; } = 100;
        public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new()
        {
            Content = "You have spawned as <color=#55da71>Serpents</color> <color=#f02d0f>Hand</color> from early spawn.<br>Your objective is to terminate any personnel, including <color=#ffa300>Class D Personnel</color> and <color=#0e9017>Chaos Insurgency</color> to save the <color=#bd0606>SCPs</color>.<br>Work with the other <color=#bd0606>SCPs</color> to dominate the site!",
            Duration = 10,
            Show = true,
            Type = global::Broadcast.BroadcastFlags.Normal,
        };
        public override string CustomInfo { get; set; } = "Serpents Hand Cult Leader";
        public override List<ItemType> Inventory { get; set; } = new()
        {
            ItemType.Medkit,
            ItemType.SCP500,
            ItemType.ArmorHeavy,
            ItemType.Radio,
            ItemType.SCP2176
        };
        public override List<uint> CustomItemInventory { get; set; } = new()
        {
            200,
            201
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
        {
            {
                AmmoType.Nato556,
                200
            }
        };
        public override void SubscribeEvent()
        {
            Exiled.Events.Handlers.Scp049.Attacking += OnAttack049;
            Exiled.Events.Handlers.Scp096.AddingTarget += OnAddingTarget;
            Exiled.Events.Handlers.Scp106.Attacking += OnAttack106;
            Exiled.Events.Handlers.Scp173.Blinking += Blink;
            Exiled.Events.Handlers.Player.Shot += OnShot;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.Dying += OnKillingPlayer;
            base.SubscribeEvent();

        }

        public override void UnsubscribeEvent()
        {
            Exiled.Events.Handlers.Scp049.Attacking -= OnAttack049;
            Exiled.Events.Handlers.Scp096.AddingTarget -= OnAddingTarget;
            Exiled.Events.Handlers.Scp106.Attacking -= OnAttack106;
            Exiled.Events.Handlers.Scp173.Blinking -= Blink;
            Exiled.Events.Handlers.Player.Shot -= OnShot;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.Dying -= OnKillingPlayer;
            base.UnsubscribeEvent();

        }

        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (Check(ev.Target))
            {
                ev.IsAllowed = false;
            }
        }
        public void OnAttack049(Exiled.Events.EventArgs.Scp049.AttackingEventArgs ev)
        {
            if (Check(ev.Target))
            {
                ev.IsAllowed = false;
            }
        }
        public void OnAttack106(Exiled.Events.EventArgs.Scp106.AttackingEventArgs ev)
        {
            if (Check(ev.Target))
            {
                ev.IsAllowed = false;
            }
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != null && ev.Player.Role != null && ev.Attacker != null && ev.Attacker.Role != null)
            {
                if (Check(ev.Player) && ev.Attacker.Role.Side == Exiled.API.Enums.Side.Scp || ev.Player.Role.Side == Side.Scp && Check(ev.Attacker))
                {
                    ev.IsAllowed = false;
                }
                else
                {
                    ev.IsAllowed = true;
                }
            }
        }
        public void OnShot(ShotEventArgs ev)
        {
            if (ev.Player != null && ev.Player.Role != null && ev.Target != null && ev.Target.Role != null)
            {
                if (Check(ev.Player) && ev.Target.Role.Side == Exiled.API.Enums.Side.Scp)
                {
                    ev.CanHurt = false;
                }
                else
                {
                    ev.CanHurt = true;
                }
            }
        }

        public void Blink(BlinkingEventArgs ev)
        {
            foreach (Player p in ev.Targets)
            {
                if (Check(p))
                {
                    ev.Targets.Remove(p);
                }
            }
        }

        public override void OnKillingPlayer(DyingEventArgs ev)
        {
            if (ev.Attacker != null && Check(ev.Attacker) && ev.Player.Role.Type != RoleTypeId.Scp0492)
            {
                ev.IsAllowed = false;
                ev.Player.RoleManager.ServerSetRole(RoleTypeId.Scp0492, RoleChangeReason.Respawn);
            }

            base.OnKillingPlayer(ev);
        }

        public override Vector3 SpawnLocation { get; set; } = new(62.777f, 991.648f, -50.397f);
    }
}
