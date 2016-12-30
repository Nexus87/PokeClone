using System.Collections.Generic;

namespace GameEngine.GUI.Utils
{
    public interface ITextSplitter
    {
        IEnumerable<string> SplitText(int charsPerLine, string text);
    }
}
