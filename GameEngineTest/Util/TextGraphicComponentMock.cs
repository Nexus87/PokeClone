using GameEngine.Graphics.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
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

        public float PreferedTextHeight { get;set;  }

        public float RealTextHeight
        {
            get { return Math.Min(Height, PreferedTextHeight); }
        }

        public string Text { get; set; }

        public GameEngine.Wrapper.ISpriteFont SpriteFont
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
