﻿using EarlyRespawnWave.Interfaces;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.Handlers;
using PlayerRoles;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = PluginAPI.Core.Log;

namespace EarlyRespawnWave.Managers
{
    public class SpawnManager
    {
        public void SpawnClass(ICustomRole role, Exiled.API.Features.Player player)
        {
            // Set players role

            player.RoleManager.ServerSetRole(role.Role, RoleChangeReason.None);
            player.Role.Set(role.Role);
            Log.Debug("Player should have role " + player.Role.Name);

            player.ClearInventory();
            // Adds player inventory
            if (role.Inventory.Count > 0)
            {
                foreach (ItemType it in role.Inventory)
                {
                    player.AddItem(it);
                }
            }
            // Adds players ammo
            if (role.Ammo.Count() > 0)
            {
                foreach (KeyValuePair<AmmoType, ushort> Ammo in role.Ammo)
                {
                    player.SetAmmo(Ammo.Key, Ammo.Value);
                }
            }
            // Gives custom items
            if (role.CustomItemInventory.Count() > 0)
            {
                foreach (uint ItemId in role.CustomItemInventory)
                {
                    if (!player.IsInventoryFull)
                    {
                        CustomItem.TryGet(ItemId, out CustomItem? ci);
                        if(ci != null)
                        {
                            ci.Give(player);
                        }
                        
                    }
                }
            }

            if (role.Effects.Count > 0)
            {
                foreach (Effect e in role.Effects)
                {
                    player.EnableEffect(e);
                }
            }

            // Sets players health and max health
            player.MaxHealth = role.MaxHealth;
            player.Health = role.Health;
            
            player.IsGodModeEnabled = role.IsGodMode;
            Log.Debug("Player should have health " + player.Health + "/" + player.MaxHealth);
            Log.Debug("Is God Mode? " + player.IsGodModeEnabled);

            // Sets players Custom Info.
            player.CustomInfo = role.CustomInfo;
            player.UniqueRole = role.Name + " - " + role.Team.ToString();
            Log.Debug("Player should have CI " + player.CustomInfo + " with UR being " + player.UniqueRole);

            player.IsBypassModeEnabled = role.KeycardBypass;

            //if(role.Abilities.Count > 0)
            //{
            //    foreach(IAbility i in role.Abilities)
            //    {
            //        if(i.Enabled == true)
            //        {
            //            i.AbilityAdded(player);
            //            i.SubscribeToEvents();
            //        }
            //    }
            //}

            player.Broadcast(role.Broadcast);

            // Sets players postition
            player.Transform.position = role.SpawnLocation;


            role.PlayersWhoHaveRole.Add(player);
            
            role.RoleAdded(player);
            role.SubscribeEvent();

            

        }

        public void RemoveRole(Exiled.API.Features.Player p)
        {
            ICustomRole? role = CheckPlayerForRole(p);
            if(role != null)
            {
                p.UniqueRole = "";
                p.CustomInfo = "";
                p.IsBypassModeEnabled = false;
                role.RoleRemoved(p);
                role.UnsubscribeEvent();
                role.PlayersWhoHaveRole.Remove(p);
            }
        }

        public void RemoveRole(ICustomRole role,Exiled.API.Features.Player p)
        {
                p.UniqueRole = "";
                p.CustomInfo = "";
                p.IsBypassModeEnabled = false;
                role.RoleRemoved(p);
                role.UnsubscribeEvent();
                role.PlayersWhoHaveRole.Remove(p);
        }

        public ICustomRole? CheckPlayerForRole(Exiled.API.Features.Player p)
        {
            if(Plugin.Instance.Config.RapidResponseTeam.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.RapidResponseTeam;   
            else if (Plugin.Instance.Config.InfiltrationInsurgencySquad.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.InfiltrationInsurgencySquad;
            else if (Plugin.Instance.Config.SerpentsHand.SHLeader.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHLeader;
            else if (Plugin.Instance.Config.SerpentsHand.SHSilencer.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHSilencer;
            else if (Plugin.Instance.Config.SerpentsHand.SHEngineer.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHEngineer;
            else if (Plugin.Instance.Config.SerpentsHand.SHPhantom.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHPhantom;
            else if (Plugin.Instance.Config.SerpentsHand.SHSavage.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHSavage;
            else if (Plugin.Instance.Config.SerpentsHand.SHCollector.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHCollector;
            else if (Plugin.Instance.Config.SerpentsHand.SHDestroyer.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHDestroyer;
            else if (Plugin.Instance.Config.SerpentsHand.SHConscript.PlayersWhoHaveRole.Contains(p))
                return Plugin.Instance.Config.SerpentsHand.SHConscript;
            else
                return null;
        }

        public bool DoesPlayerHaveRole(Exiled.API.Features.Player p)
        {
            if (Plugin.Instance.Config.RapidResponseTeam.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.InfiltrationInsurgencySquad.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHLeader.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHSilencer.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHEngineer.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHPhantom.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHSavage.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHCollector.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHDestroyer.PlayersWhoHaveRole.Contains(p))
                return true;
            else if (Plugin.Instance.Config.SerpentsHand.SHConscript.PlayersWhoHaveRole.Contains(p))
                return true;
            else
                return false;
        }
    }
}
