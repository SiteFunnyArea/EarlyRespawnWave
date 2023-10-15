using EarlyRespawnWave.Enums;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Core;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp0492;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Scp173;
using Exiled.Events.EventArgs.Scp939;
using Exiled.Events.EventArgs.Server;
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
        public abstract List<ItemType> Inventory { get; set; }
        public virtual List<uint> CustomItemInventory { get; set; } = new();
        public abstract Dictionary<AmmoType, ushort> Ammo { get; set; }
        public virtual bool KeycardBypass { get; set; } = false;
        public abstract UnityEngine.Vector3 SpawnLocation { get; set; }
        public virtual bool IsGodMode { get; set; } = false;
        public virtual void RoleAdded(Player p) { PluginAPI.Core.Log.Debug("Gave role " + Name + " to " + p.Nickname + ". Role team " + Team.ToString()); }
        public virtual void RoleRemoved(Player p) { PluginAPI.Core.Log.Debug("Removed role " + Name + " from " + p.Nickname); }
        public virtual void SubscribeEvent()
        {
            if (Team == Teams.SerpentsHand)
            {

                Exiled.Events.Handlers.Server.EndingRound += OnEndingRound;
                Exiled.Events.Handlers.Scp049.Attacking += OnAttack049;
                Exiled.Events.Handlers.Scp0492.ConsumingCorpse += ConsumeCorpse;
                Exiled.Events.Handlers.Scp096.AddingTarget += OnAddingTarget;
                Exiled.Events.Handlers.Scp106.Attacking += OnAttack106;
                //Exiled.Events.Handlers.Scp173.Blinking += Blink;
                Exiled.Events.Handlers.Scp939.Lunging += Lunging;
                Exiled.Events.Handlers.Player.Shot += OnShot;
                Exiled.Events.Handlers.Player.Hurting += OnHurting;
                Exiled.Events.Handlers.Player.Dying += OnKillingPlayer;
                Exiled.Events.Handlers.Player.ChangingRole += OnPlayerChangeRole;
            }
        }
        public virtual void UnsubscribeEvent()
        {
            if (Team == Teams.SerpentsHand)
            { 
                Exiled.Events.Handlers.Server.EndingRound -= OnEndingRound;
                Exiled.Events.Handlers.Scp049.Attacking -= OnAttack049;
                Exiled.Events.Handlers.Scp0492.ConsumingCorpse -= ConsumeCorpse;
                Exiled.Events.Handlers.Scp096.AddingTarget -= OnAddingTarget;
                Exiled.Events.Handlers.Scp106.Attacking -= OnAttack106;
                //Exiled.Events.Handlers.Scp173.Blinking -= Blink;
                Exiled.Events.Handlers.Scp939.Lunging -= Lunging;
                Exiled.Events.Handlers.Player.Shot -= OnShot;
                Exiled.Events.Handlers.Player.Hurting -= OnHurting;
                Exiled.Events.Handlers.Player.Dying -= OnKillingPlayer;
                Exiled.Events.Handlers.Player.ChangingRole -= OnPlayerChangeRole;
            }

        }

        public virtual void ConsumeCorpse(ConsumingCorpseEventArgs ev)
        {
            if(Check(ev.Player) && Team == Teams.SerpentsHand) {
                ev.IsAllowed = false;
            }
            else
            {
                ev.IsAllowed = true; 
            }
        }

        //public virtual void Blink(BlinkingEventArgs ev)
        //{
        //    foreach (Player p in ev.Targets)
        //    {
        //        if (Check(p) && Team == Teams.SerpentsHand)
        //        {
        //            ev.Targets.Remove(p);
        //        }
        //    }
        //}
        public virtual void Lunging(LungingEventArgs ev)
        {
            if (Check(ev.Player) && Team == Teams.SerpentsHand)
            {
                ev.Player.Health = ev.Player.Health;
            }
        }

        public virtual void OnEndingRound(EndingRoundEventArgs e)
        {

            if (Player.List.Count(t => Check(t)) > 0 && (Player.List.Count(p => p.IsNTF || p.IsCHI) > 0))
            {
                e.IsRoundEnded = false;
            }


        }
        public virtual void OnPlayerChangeRole(ChangingRoleEventArgs ev)
        {
            if (Check(ev.Player))
            {
                Plugin.Instance.sM.RemoveRole(this, ev.Player);
            }
        }

        public virtual void OnKillingPlayer(DyingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                Plugin.Instance.sM.RemoveRole(this, ev.Player);
            }
        }


        public virtual void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (Check(ev.Target) && Team == Teams.SerpentsHand)
            {
                ev.IsAllowed = false;
            }
            else
            {
                ev.IsAllowed = true;
            }
        }
        public virtual void OnAttack049(Exiled.Events.EventArgs.Scp049.AttackingEventArgs ev)
        {
            if (Check(ev.Target) && Team == Teams.SerpentsHand)
            {
                ev.IsAllowed = false;
            }
            else
            {
                ev.IsAllowed = true;
            }
        }
        public virtual void OnAttack106(Exiled.Events.EventArgs.Scp106.AttackingEventArgs ev)
        {
            if (Check(ev.Target) && Team == Teams.SerpentsHand)
            {
                ev.IsAllowed = false;
            }
            else
            {
                ev.IsAllowed = true;
            }
        }

        public virtual void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != null && ev.Attacker != null)
            {
                if (Check(ev.Player) && Team == Teams.SerpentsHand)
                {
                    if (ev.Attacker.Role.Side == Side.Scp)
                        ev.IsAllowed = false;
                }else if(Check(ev.Attacker) && Team == Teams.SerpentsHand)
                {
                    if (ev.Player.Role.Side == Side.Scp)
                        ev.IsAllowed = false;
                }
                else
                {
                    ev.IsAllowed = true;
                }
            }
        }
        public virtual void OnShot(ShotEventArgs ev)
        {
            if (ev.Player != null && ev.Target != null)
            {
                if (Check(ev.Player) && Team == Teams.SerpentsHand)
                {
                    if(ev.Target.Role.Side == Side.Scp)
                        ev.CanHurt = false;
                    
                }
                else if (Check(ev.Target) && Team == Teams.SerpentsHand)
                {
                    if (ev.Player.Role.Side == Side.Scp)
                        ev.CanHurt = false;
                }
                else
                {
                    ev.CanHurt = true;
                }
            }
        }

    }
}
