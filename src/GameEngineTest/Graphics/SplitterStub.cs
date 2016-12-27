using System;
using System.Collections.Generic;
using GameEngine.GUI.Utils;

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

        public int Count => Strings.Count;

        public void SplitText(int charsPerLine, string text)
        {
            SplitTextCallback?.Invoke();
        }
    }
}