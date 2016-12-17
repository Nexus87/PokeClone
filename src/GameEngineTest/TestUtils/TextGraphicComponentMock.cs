using System;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicComponentMock : GraphicComponentMock, ITextGraphicComponent
    {
        public Func<int> DisplayableCharsFunc = null;
        public int DisplayableChars()
        {
            return DisplayableCharsFunc?.Invoke() ?? 0;
        }

        public float PreferredTextHeight { get;set;  }

        public float RealTextHeight => Math.Min(Area.Height, PreferredTextHeight);

        public string Text { get; set; }

        public ISpriteFont SpriteFont
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
