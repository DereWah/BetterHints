using BetterHints.Types;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterHints.Utils
{
    public class Utilities
    {

        

        public Utilities() => plugin = Main.Singleton;
        /// <summary>
        /// Minimum time between Hint updates. Prevent rate limits.
        /// </summary>
        public float hintRate = 0.55f;

        private bool isRateLimited = false;
        private bool shouldUpdate = false;
        private readonly Main plugin;

        /// <summary>
        /// Add a HintElement to a player's screen.
        /// </summary>
        /// <param name="player">The player to show the hint to.</param>
        /// <param name="hintElement">The hint to show to the player.</param>
        public void AddBetterHint(Player player, HintElement hintElement)
        {
            List<HintElement> currentHints = GetPlayerHints(player);

            currentHints.Add(hintElement);
            UpdateBetterHint(player);
            if(hintElement.Duration >= 0)
            {
                Timing.CallDelayed(hintElement.Duration, () => {
                    if (currentHints.Contains(hintElement))
                    {
                        currentHints.Remove(hintElement);
                        UpdateBetterHint(player);
                    }
                });
            }
        }

        /// <summary>
        /// Removes a Hint from the player's screen.
        /// </summary>
        /// <param name="player">The player to remove the hint from.</param>
        /// <param name="hintElement">The hint to remove.</param>
        public void RemoveBetterHint(Player player, HintElement hintElement)
        {
            List<HintElement> currentHints = GetPlayerHints(player);
            currentHints.Remove(hintElement);
            UpdateBetterHint(player);
        }

        /// <summary>
        /// Update the player's hint screen.
        /// </summary>
        /// <param name="player">The player to show the hint to.</param>
        /// <param name="hintElement">The hint to show to the player.</param>
        public void UpdateBetterHint(Player player)
        {
            List<HintElement> CurrentHints = GetPlayerHints(player);
            string result = BuildResult(CurrentHints);

            if (!isRateLimited)
            {
                isRateLimited = true;
                player.ShowHint(result, 99999);
                Timing.CallDelayed(hintRate, () => { OnRateLimitFinished(player); });
            }
            else
            {
                shouldUpdate = true;
            }
        }
        /// <summary>
        /// Build the final string for the BetterHint system.
        /// </summary>
        /// <param name="CurrentHints">A list of the hints to merge on the screen.</param>
        /// <returns></returns>
        public string BuildResult(List<HintElement> CurrentHints)
        {
            string result = string.Empty;
            foreach (HintElement he in CurrentHints)
            {
                //The space at the end of the string is SUPER IMPORTANT! If removed, TMP will skip completely
                result += $"<line-height={he.VerticalOffset}>\n"
                    + he.Result
                    + $"<line-height={-(he.VerticalOffset + he.getHintHeight())}>\n"
                    + " ";
            }
            Log.Info(result);
            return result;
        }

        /// <summary>
        /// Gets all the active hints of a player.
        /// </summary>
        /// <param name="player">The player whose hints will be retrieved</param>
        /// <returns></returns>
        public List<HintElement> GetPlayerHints(Player player)
        {
            if (!plugin.PlayerHints.TryGetValue(player.UserId, out List<HintElement> CurrentHints))
            {
                CurrentHints = new List<HintElement>();
                plugin.PlayerHints.Add(player.UserId, CurrentHints);
            }
            return CurrentHints;
        }

        public void OnRateLimitFinished(Player player)
        {
            isRateLimited = false;
            if (shouldUpdate)
            {
                shouldUpdate = false;
                UpdateBetterHint(player);
            }
        }

    }
}
