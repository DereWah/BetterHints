using BetterHints.Types;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterHints
{
    public static class API
    {
        /// <summary>
        /// Update the player's hint screen.
        /// </summary>
        /// <param name="player">The player to show the hint to.</param>
        public static void UpdateBetterHint(Player player)
        {
            Main.Singleton.Utilities.UpdateBetterHint(player);
        }

        /// <summary>
        /// Update the player's hint screen.
        /// </summary>
        /// <param name="player">The player to show the hint to.</param>
        /// <param name="hintElement">The hint to show to the player.</param>
        public static void AddBetterHint(Player player, HintElement hintElement)
        {
            Main.Singleton.Utilities.AddBetterHint(player, hintElement);
        }

        /// <summary>
        /// Removes a Hint from the player's screen.
        /// </summary>
        /// <param name="player">The player to remove the hint from.</param>
        /// <param name="hintElement">The hint to remove.</param>
        public static void RemoveBetterHint(Player player, HintElement hintElement)
        {
            Main.Singleton.Utilities.RemoveBetterHint(player, hintElement);
        }

        /// <summary>
        /// Update the player's hint screen.
        /// </summary>
        /// <param name="player">The player to show the hint to.</param>
        /// <param name="text">The text of the hint.</param>
        /// /// <param name="duration">The duration of the hint. Set to a negative value to make it permanent</param>
        /// /// <param name="voffset">The height relative to the baseline where this hint will be displayed.</param>
        public static void AddBetterHint(Player player, string text, float duration, int voffset)
        {
            HintElement hintElement = new HintElement(text, duration, voffset);
            Main.Singleton.Utilities.AddBetterHint(player, hintElement);
        }
    }
}
