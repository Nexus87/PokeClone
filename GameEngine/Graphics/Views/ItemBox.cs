﻿using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    internal class ItemBox : AbstractGraphicComponent
    {
        public bool IsSelected { get; set; }
        private TextureBox arrow;
        private TextBox textBox;

        public ItemBox(String displayedText, ISpriteFont spriteFont)
        {
            arrow = new TextureBox("arrow");
            textBox = new TextBox("MenuFont", spriteFont);
            textBox.Text = displayedText;
        }
        public override void Setup(ContentManager content)
        {
            arrow.Setup(content);
            textBox.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (IsSelected)
                arrow.Draw(time, batch);
            
            textBox.Draw(time, batch);
        }

        protected override void Update()
        {

            textBox.Height = Height;

            // If we can't draw the whole arrow, draw no arrow at all
            float arrowHeight = textBox.RealTextHeight;
            float arrowWidth = Width.CompareTo(arrowHeight) <= 0 ? 0 : arrowHeight;

            arrow.X = X;
            arrow.Y = Y;
            arrow.Width = arrowWidth;
            arrow.Height = arrowHeight;

            textBox.X = X + arrowWidth;
            textBox.Y = Y;
            textBox.Width = Math.Max(Width - arrowWidth, 0);

        }
    }
}
