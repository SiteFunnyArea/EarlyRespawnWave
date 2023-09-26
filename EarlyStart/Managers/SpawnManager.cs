using EarlyStart.Interfaces;
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

namespace EarlyStart.Managers
{
    public class SpawnManager
    {
        public void SpawnClass(ICustomRole role, Player player)
        {
            // Set players role
            player.RoleManager.ServerSetRole(role.Role, RoleChangeReason.None);
            
            player.ClearInventory();

            // Adds player inventory
            if(role.Inventory.Count > 0 ) {
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
                    player.AddAmmo(Ammo.Key, Ammo.Value);
                }
            }
            // Gives custom items
            if (role.CustomItemInventory.Count() > 0)
            {
                foreach (uint ItemId in role.CustomItemInventory)
                {
                    if (!player.IsInventoryFull)
                    {
                        CustomItem.Get(ItemId).Give(player);
                    }
                }
            }

            if(role.Effects.Count > 0)
            {
                foreach(Effect e in role.Effects)
                {
                    player.EnableEffect(e);
                }
            }
            // Sets players health and max health
            player.MaxHealth = role.MaxHealth;
            player.Health = role.Health;

            // Sets players postition
            player.Transform.position = role.SpawnLocation;
            // Sets players Custom Info.
            player.CustomInfo = role.CustomInfo;
            player.UniqueRole = role.Name + "-" + role.Team.ToString();

            player.Broadcast(role.Broadcast);
        }
    }
}
