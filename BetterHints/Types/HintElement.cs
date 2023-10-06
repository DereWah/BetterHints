using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterHints.Types
{
    public class HintElement
    {
        public string RawHint { get; }
        public string Result { get; set; } = "";
        public string[] Lines { get; }

        public List<HintElement> Breaks { get; } = new List<HintElement>();

        public int VerticalOffset { get; }

        public int Duration { get; }

        public HintElement(string content, int duration, int voffset)
        {
            Duration = duration;
            RawHint = content;
            VerticalOffset = voffset;
            Lines = content.Split(new string[] { "\\n" }, StringSplitOptions.None);
            if (!Lines.Any())
            {
                Result = content;
                return;
            }
            int x = -1;
            foreach (string l in Lines)
            {
                x++;
                if (x == 0) continue;
                HintElement h = new HintElement(l, duration, voffset - x*35);
                Breaks.Add(h);
                
            }
            Result = Lines.First();
        }
    }
}
