using System;
using System.Collections.Generic;
using GameEngine.GUI.Components;

namespace GameEngineTest.Graphics
{
    public class SplitterStub : ITextSplitter
    {
        public List<string> Strings = new List<string>();
        public Action SplitTextCallback = null;

        public IEnumerable<string> SplitText(int charsPerLine, string text)
        {
            SplitTextCallback?.Invoke();
            return Strings;
        }
    }
}