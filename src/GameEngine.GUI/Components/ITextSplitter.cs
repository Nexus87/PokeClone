using System.Collections.Generic;

namespace GameEngine.GUI.Components
{
    public interface ITextSplitter
    {
        IEnumerable<string> SplitText(int charsPerLine, string text);
    }
}
