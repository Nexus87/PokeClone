﻿using System;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicComponentMock : GraphicComponentMock, IGuiComponent
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
