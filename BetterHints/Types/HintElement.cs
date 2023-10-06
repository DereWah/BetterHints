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

        /// <summary>
        /// Gets the raw content of the hint.
        /// </summary>
        public string RawHint { get; }
        /// <summary>
        /// Gets the parsed version of the hint, to fit the correct height.
        /// </summary>
        public string Result { get; } = "";

        /// <summary>
        /// Gets the multiple HintElements the original hint will be split into if it has multiple lines.
        /// </summary>
        public List<HintElement> Breaks { get; }

        /// <summary>
        /// Gets the vertical position of the hint from the baseline.
        /// </summary>
        public int VerticalOffset { get; }

        /// <summary>
        /// Gets the vertical line height used for newlines in this hint.
        /// </summary>
        public int NewLineHeight { get; }

        /// <summary>
        /// Gets the horizontal origin (pixels) of the hint from the center of the screen.
        /// </summary>
        public int HorizontalOffset { get; }

        /// <summary>
        /// Gets the duration of the hint display.
        /// </summary>
        public float Duration { get; }

        /// <summary>
        /// Creates a HintElement instance.
        /// </summary>
        /// <param name="content">The text of the hint.</param>
        /// <param name="duration">The duration of the hint.</param>
        /// <param name="vOffset">The vertical offset of the hint.</param>
        /// /// <param name="hOffset">The vertical offset of the hint.</param>
        /// /// <param name="newLineHeight">The height of new lines in this hint.</param>
        public HintElement(string content, float duration, int vOffset, int hOffset = 0, int newLineHeight = 35)
        {
            Duration = duration;
            RawHint = content;
            VerticalOffset = vOffset;
            HorizontalOffset = hOffset;
            NewLineHeight = newLineHeight;
            string[] Lines = content.Split(new string[] { "\\n" }, StringSplitOptions.None);

            if (Lines.Any())
            {
                Breaks = new List<HintElement>();
                //If the content is split across multiple lines, we create multiple hints and offset them below the line above.
                for (int i = 1; i < Lines.Length; i++)
                {
                    HintElement h = new HintElement(Lines[i], duration, vOffset - i * newLineHeight);
                    Breaks.Add(h);
                }
                Result = Lines.First();
            }
            else
            {
                Result = content;
            }
            Result = $"<align=left><pos={hOffset}px>" + Result + "</pos></align>";
        }
    }
}
