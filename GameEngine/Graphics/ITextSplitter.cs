using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public interface ITextSplitter
    {
        string GetString(int index);
        int Count { get; }
        void SplitText(int charsPerLine, string text);
    }
}
