using EarlyRespawnWave.Interfaces;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyRespawnWave.Managers
{
    public class SpawnManager
    {
        public void SpawnClass(ICustomRole role, Player player)
        {
            // Set players role
            player.RoleManager.ServerSetRole(role.Role, RoleChangeReason.None);
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
            //if (role.CustomItemInventory.Count() > 0)
            //{
            //    foreach (uint ItemId in role.CustomItemInventory)
            //    {
            //        if (!player.IsInventoryFull)
            //        {
            //            CustomItem.TryGive(player, ItemId);
            //        }
            //    }
            //}

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

            // Sets players Custom Info.
            player.CustomInfo = role.CustomInfo;
            player.UniqueRole = role.Name + "-" + role.Team.ToString();
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

        }

        public void RemoveRole(Player p)
        {
            ICustomRole? role = CheckPlayerForRole(p);
            if(role != null)
            {
                p.UniqueRole = "";
                p.CustomInfo = "";
                //if(role.Abilities.Count > 0)
                //{
                //    foreach(IAbility i in role.Abilities)
                //    {
                //        if(i.Enabled == true)
                //        {
                //            i.AbilityRemoved(p);
                //            i.UnsubscribeToEvents();
                //        }
                //    }
                //}
            }
        }

        public ICustomRole? CheckPlayerForRole(Player p)
        {
            if(p.UniqueRole.Contains("Rapid Response Team")){
                return Plugin.Instance.Config.RapidResponseTeam;
            }
            if (p.UniqueRole.Contains("Infiltration Insurgency Squad"))
            {
                return Plugin.Instance.Config.InfiltrationInsurgencySquad;
            }

            return null;
        }

        public bool PlayerHasACustomRole(Player p)
        {
            if(CheckPlayerForRole(p) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
