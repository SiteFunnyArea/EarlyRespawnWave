using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NorthwoodLib;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyRespawnWave.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class SetSubclass : ICommand
    {
        public string Command { get; } = "setsubclass";

        public string[] Aliases { get; } = { "erwclass", "setclass" };

        public string Description { get; } = "A Command that sets your role.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender pl)
            {
                if (arguments.Count != 2)
                {
                    response = $"Missing {2 - arguments.Count} arguments (setsubclass [Class] [UserId]).";
                    return false;
                }
                if (arguments.Count >= 3)
                {
                    response = $"Too many arugments, only needs two, you provided {arguments.Count} (setsubclass [Class] [UserId]).";
                    return false;
                }
                if (!int.TryParse(arguments.At(1), out int input))
                {
                    response = $"{arguments.At(1)} is not a valid number.";
                    return false;
                }
                if(!Player.TryGet(int.Parse(arguments.At(1)),out Player p))
                {
                    response = $"{arguments.At(1)} isn't a valid player.";
                    return false;
                }

                if (arguments.At(0).Equals("RapidResponseTeam"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.RapidResponseTeam, player);
                    response = $"Gave subclass {Plugin.Instance.Config.RapidResponseTeam.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("InfiltrationInsurgencySquad"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.InfiltrationInsurgencySquad, player);
                    response = $"Gave subclass {Plugin.Instance.Config.InfiltrationInsurgencySquad.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultLeader"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHLeader, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHLeader.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultSilencer"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHSilencer, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHSilencer.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultEngineer"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHEngineer, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHEngineer.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultPhantom"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHPhantom, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHPhantom.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultSavage"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHSavage, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHSavage.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultCollector"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHCollector, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHCollector.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultDestroyer"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHDestroyer, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHDestroyer.Name} to {player.Nickname}.";
                    return true;
                }
                else if (arguments.At(0).Equals("CultConscript"))
                {
                    Player player = Player.Get(pl.PlayerId);
                    Plugin.Instance.sM.SpawnClass(Plugin.Instance.Config.SerpentsHand.SHConscript, player);
                    response = $"Gave subclass {Plugin.Instance.Config.SerpentsHand.SHConscript.Name} to {player.Nickname}.";
                    return true;
                }
                else
                {
                    response = $"Invalid subclass \"{arguments.At(0)}\" (use the listsubclasses command).";
                    return false;
                }
            }

            response = "idfk.";
            return false;
        }
    }
}
