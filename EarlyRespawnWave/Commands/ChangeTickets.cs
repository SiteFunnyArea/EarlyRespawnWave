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
    class ChangeTickets : ICommand
    {
        public string Command { get; } = "changetickets";

        public string[] Aliases { get; } = { "edittickets","erwtickets" };

        public string Description { get; } = "A Command that changes the first wave tickets.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(sender is PlayerCommandSender pl)
            {
                if(arguments.Count != 2)
                {
                    response = $"Missing {2-arguments.Count} arguments (changetickets [Team] [Amount]).";
                    return false;
                }
                if (Plugin.Instance._handlers.Waves > 1)
                {
                    response = $"Missed spawnwave.";
                    return false;
                }

                if (!int.TryParse(arguments.At(1),out int input))
                {
                    response = $"Please use a number (integer).";
                    return false;
                }



                if(arguments.At(0).Equals("RRT"))
                {
                    int amount = int.Parse(arguments.At(1));

                    if(amount > 0 && (Plugin.Instance._handlers.IISTickets - amount) >= 0)
                    {
                        Plugin.Instance._handlers.RRTTickets += amount;
                        Plugin.Instance._handlers.IISTickets -= amount;

                        response = "Successfully changed tickets!";
                        return true;
                    }
                    else
                    {
                        response = "Could not change tickets.";
                        return false;
                    }
                }else if (arguments.At(0).Equals("IIS"))
                {
                    int amount = int.Parse(arguments.At(1));

                    if (amount > 0 && (Plugin.Instance._handlers.RRTTickets - amount) >= 0)
                    {
                        Plugin.Instance._handlers.RRTTickets -= amount;
                        Plugin.Instance._handlers.IISTickets += amount;

                        response = "Successfully changed tickets!";
                        return true;
                    }
                    else
                    {
                        response = "Could not change tickets.";
                        return false;
                    }
                }
                else
                {
                    response = $"Invalid team \"{arguments.At(0)}\" (valid teams: RRT, IIS).";
                    return false;
                }
            }

            response = "idfk.";
            return false;
        }
    }
}
