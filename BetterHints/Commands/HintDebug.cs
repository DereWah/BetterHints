using BetterHints.Types;
using BetterHints.Utils;
using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterHints.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class HintDebug : ICommand
    {
        public string Command { get; } = "HintDebug";

        public string[] Aliases { get; } = new[] { "hd" };

        public string Description { get; } = "Display a hint on your screen, using formatting.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Utilities Utilities = Main.Singleton.Utilities;
            Player player = Player.Get(sender);

            Utilities.UpdateBetterHint(player);


            List<string> args = arguments.Array.ToList<string>();
            HintElement hintElement = new HintElement(args[1], 10, int.Parse(args[2]), newLineHeight: int.Parse(args[3]));

            Utilities.AddBetterHint(player, hintElement);

            Utilities.UpdateBetterHint(player);

            response = $"Executing the command... ({args[1]} {args[2]})";
            
            return true;
        }
    }
}
