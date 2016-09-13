using System;
using System.Collections.Generic;
using GameEngine.Utils;

namespace GameEngineTest.Graphics
{
    public class SplitterStub : ITextSplitter
    {
        public List<string> Strings = new List<string>();
        public Action SplitTextCallback = null;
        public string GetString(int index)
        {
            return index < Strings.Count ? Strings[index] : "";
        }

        public int Count
        {
            get { return Strings.Count; }
        }

        public void SplitText(int charsPerLine, string text)
        {
            if (SplitTextCallback != null)
                SplitTextCallback();
        }
    }
}