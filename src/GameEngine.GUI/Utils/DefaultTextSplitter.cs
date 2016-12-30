using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameEngine.GUI.Utils
{
    public class DefaultTextSplitter : ITextSplitter
    {
        public IEnumerable<string> SplitText(int charsPerLine, string text)
        {
            var lines = new List<string>();

            if (!NeedsToSplit(text, charsPerLine))
                return lines;

            var remaining = text.Trim();

            while (remaining.Length > charsPerLine)
            {
                var match = Regex.Match(remaining.Substring(0, charsPerLine), @"\s", RegexOptions.RightToLeft);
                var length = match.Success ? match.Index : charsPerLine;


                lines.Add(remaining.Substring(0, length).Trim());
                remaining = remaining.Substring(length).TrimStart();
            }

            // remaining has no trailing whitespace, because we trimmed it at the beginning.
            if (remaining.Length != 0)
                lines.Add(remaining);

            return lines;
        }

        private static bool NeedsToSplit(string text, int charsPerLine)
        {
            return text != null && charsPerLine != 0;
        }

    }
}
