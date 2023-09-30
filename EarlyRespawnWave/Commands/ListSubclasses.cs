using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyRespawnWave.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class ListSubclasses : ICommand
    {
        public string Command { get; } = "listsubclasses";

        public string[] Aliases { get; } = { "classlist", "erwlist" };

        public string Description { get; } = "A Command that lists all the subclasses with ERW.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            response = "All Valid Classes:\nRapidResponseTeam\nInfiltrationInsurgencySquad\nCultLeader\nCultSilencer\nCultEngineer\nCultPhantom\nCultSavage\nCultCollector\nCultDestroyer\nCultConscript";
            return true;

        }
    }
}
