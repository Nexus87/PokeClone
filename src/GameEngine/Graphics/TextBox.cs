﻿using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Graphics.General;

namespace GameEngine.Graphics
{
    [GameType]
    public class TextBox : AbstractGraphicComponent, ITextGraphicComponent
    {
        private string text = "";
        private readonly IGraphicalText textGraphic;
        private float preferredTextSize;

        public TextBox(ISpriteFont font)
            : this(new TextGraphic(font))
        { }

        public TextBox(IGraphicalText textGraphic)
        {
            this.textGraphic = textGraphic;
            PreferredTextHeight = textGraphic.CharHeight;
        }

        public float PreferredTextHeight
        { 
            get { return preferredTextSize; } 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("PreferredTextSize must be >= 0");

                if (preferredTextSize.AlmostEqual(value))
                    return;

                preferredTextSize = value;
                Invalidate();
            } 
        }

        private void SetPreferredSize()
        {
            PreferredHeight = PreferredTextHeight;
            PreferredWidth = textGraphic.GetSingleCharWidth(PreferredTextHeight) * text.Length;
        }

        public string Text { 
            get
            {
                return text;
            }
            set {
                text = value;
                Invalidate();
            }
        }
        public float RealTextHeight { get { return preferredTextSize <= Height ? preferredTextSize : Height; } }
        
        public int DisplayableChars()
        {
            textGraphic.CharHeight = RealTextHeight;
            var charWidth = textGraphic.GetSingleCharWidth();
            if (charWidth.CompareTo(0) == 0)
                return 0;

            var num = (int)Math.Floor(Width / charWidth);
            return num;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            textGraphic.Draw(batch);
        }

        public override void Setup()
        {
            textGraphic.Setup();
        }

        protected override void Update()
        {
            SetPreferredSize();
            textGraphic.XPosition = XPosition;
            textGraphic.YPosition = YPosition;
            textGraphic.CharHeight = RealTextHeight;

            var length = textGraphic.CalculateTextLength(" ");
            if (length.CompareTo(0) == 0)
            {
                textGraphic.Text = "";
                return;
            }

            var cnt = (int)Math.Floor(Width / length);
            textGraphic.Text = text.Substring(0, Math.Min(text.Length, cnt));
        }


        public ISpriteFont SpriteFont { get { return textGraphic.SpriteFont; } set { textGraphic.SpriteFont = value; } }
    }
}