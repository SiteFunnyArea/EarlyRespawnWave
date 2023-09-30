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
    [CommandHandler(typeof(ClientCommandHandler))]
    class HideTimer : ICommand
    {
        public string Command { get; } = "hidetimer";

        public string[] Aliases { get; } = { "erwhide" };

        public string Description { get; } = "A Command that hides the ERW respawn timer.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string userId = Player.Get(sender).UserId;

            if (!Plugin.TimerHidden.Remove(userId))
            {
                Plugin.TimerHidden.Add(userId);

                response = "ERW Respawn Timer has been hidden.";
                return true;
            }

            response = "ERW Respawn Timer has been shown.";
            return true;
        }
    }
}
