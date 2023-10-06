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



        private readonly Main plugin;

        public Utilities() => plugin = Main.Singleton;

        public float hintRate = 0.55f;
        public bool isRateLimited = false;
        public bool shouldUpdate = false;


        public void AddBetterHint(Player player, HintElement hintElement)
        {
            if (!plugin.PlayerHints.TryGetValue(player.UserId, out List<HintElement> CurrentHints))
            {
                CurrentHints = new List<HintElement>();
                plugin.PlayerHints.Add(player.UserId, CurrentHints);
            }
            if (hintElement.Breaks.Any())
            {

                foreach (HintElement he in hintElement.Breaks) AddBetterHint(player, he);
            }
            CurrentHints.Add(hintElement);
            ShowBetterHint(player);
            Timing.CallDelayed(hintElement.Duration, () => {
                if (CurrentHints.Contains(hintElement))
                {
                    CurrentHints.Remove(hintElement);
                    foreach (HintElement he in hintElement.Breaks) CurrentHints.Remove(he);
                    ShowBetterHint(player);
                }
            });
        }

        public void ShowBetterHint(Player player)
        {
            if (!plugin.PlayerHints.TryGetValue(player.UserId, out List<HintElement> CurrentHints))
            {
                CurrentHints = new List<HintElement>();
                plugin.PlayerHints.Add(player.UserId, CurrentHints);
            }

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

        public string BuildResult(List<HintElement> CurrentHints)
        {
            string result = string.Empty;
            foreach (HintElement he in CurrentHints)
            {
                result += $"<line-height=0px>\n</line-height><voffset={he.VerticalOffset}px>" + he.Result + "</voffset>";
            }
            return result;
        }

        public void OnRateLimitFinished(Player player)
        {
            isRateLimited = false;
            if (shouldUpdate)
            {
                shouldUpdate = false;
                ShowBetterHint(player);
            }
        }

    }
}
