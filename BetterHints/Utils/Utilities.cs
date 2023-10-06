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
            List<HintElement> CurrentHints = GetPlayerHints(player);

            if (hintElement.Breaks != null)
            {
                foreach (HintElement he in hintElement.Breaks) AddBetterHint(player, he);
            }

            CurrentHints.Add(hintElement);
            UpdateBetterHint(player);
            Timing.CallDelayed(hintElement.Duration, () => {
                if (CurrentHints.Contains(hintElement))
                {
                    CurrentHints.Remove(hintElement);
                    if(hintElement.Breaks != null)
                    {
                        foreach (HintElement he in hintElement.Breaks) CurrentHints.Remove(he);
                    }
                    UpdateBetterHint(player);
                }
            });
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
                result += $"<line-height=0px>\n</line-height><voffset={he.VerticalOffset}px>" + he.Result + "</voffset>";
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
