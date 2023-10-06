using BetterHints.Types;
using BetterHints.Utils;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BetterHints
{
    public class Main : Plugin<Config>
    {
        public override string Author => "@derewah";
        public override string Name => "BetterHints";
        public override string Prefix => "BetterHints";
        public override PluginPriority Priority => PluginPriority.Medium;

        public static Main Singleton;

        public Dictionary<string, List<HintElement>> PlayerHints = new Dictionary<string, List<HintElement>>();
        public Utilities Utilities;

        public override void OnEnabled()
        {
            base.OnEnabled();
            Singleton = this;
            Utilities = new Utilities();
        }
    }
}
