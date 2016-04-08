using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class DefaultTextSplitter : ITextSplitter
    {
        List<string> lines = new List<string>();

        string lastSplittedText = null;
        public void SplitText(int charsPerLine, string text)
        {
            if (EqualsLastText(text))
                return;

            lines.Clear();
            lastSplittedText = text;

            if (!NeedsToSplit(text, charsPerLine))
                return;

            lastSplittedText = text.Trim();
            var remaining = lastSplittedText;


            while (remaining.Length > charsPerLine)
            {
                var match = Regex.Match(remaining.Substring(0, charsPerLine), @"\s", RegexOptions.RightToLeft);
                int length = match.Success ? match.Index : charsPerLine;


                lines.Add(remaining.Substring(0, length).Trim());
                remaining = remaining.Substring(length).TrimStart();
            }

            // remaining has no trailing whitespace, because we trimmed it at the beginning.
            if (remaining.Length != 0)
                lines.Add(remaining);
        }

        private bool NeedsToSplit(string text, int charsPerLine)
        {
            return text != null && charsPerLine != 0;
        }

        private bool EqualsLastText(string text)
        {
            if (text != null)
                text = text.Trim();

            return Object.Equals(text, lastSplittedText);
        }

        public string GetString(int index)
        {
            if (index < Count)
                return lines[index];

            return "";
        }

        public int Count
        {
            get { return lines.Count; }
        }
    }
}
