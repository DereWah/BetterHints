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
        public string[] Breaks { get; }

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
        /// Get the total vertical length this hint occupies.
        /// </summary>
        /// <returns></returns>
        public int getHintHeight()
        {
            return (Breaks.Length - 1) * NewLineHeight;
        }

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
            VerticalOffset = -vOffset;
            HorizontalOffset = hOffset;
            NewLineHeight = newLineHeight;
            Breaks = content.Split(new string[] { "\\n" }, StringSplitOptions.None);

            if (Breaks.Any())
            {
                Result = string.Join($"<line-height={newLineHeight}px>\n", Breaks);
            }
            else
            {
                Result = content;
            }
            if (hOffset != 0) Result = $"<align=left><pos={hOffset}px>" + Result + "</pos></align>";
        }
    }
}
