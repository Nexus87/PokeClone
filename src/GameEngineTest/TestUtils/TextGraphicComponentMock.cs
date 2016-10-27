using GameEngine.Graphics;
using System;
using GameEngine.Graphics.General;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicComponentMock : GraphicComponentMock, ITextGraphicComponent
    {
        public Func<int> DisplayableCharsFunc = null;
        public int DisplayableChars()
        {
            if (DisplayableCharsFunc == null)
                return 0;
            return DisplayableCharsFunc();
        }

        public float PreferredTextHeight { get;set;  }

        public float RealTextHeight
        {
            get { return Math.Min(Height, PreferredTextHeight); }
        }

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
