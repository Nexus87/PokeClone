﻿using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class MessageBox : ForwardingGraphicComponent<MultlineTextBox>, IWidget
    {
        public Keys SelectKey;
        private TextureBox frameBox;
        private MultlineTextBox textBox;
        private SingleComponentLayout layout = new SingleComponentLayout();

        public MessageBox(Configuration config, PokeEngine game)
            : this(config.BoxBorder, config, game)
        {
        }

        public MessageBox(string border, Configuration config, PokeEngine game)
            : base(new MultlineTextBox(config.MenuFont, game), game)
        {
            SelectKey = config.KeySelect;
            frameBox = new TextureBox(border, game);
            textBox = InnerComponent;
        }

        public event EventHandler OnAllLineShowed = delegate { };

        public event EventHandler<VisibilityChangedArgs> OnVisibilityChanged;

        public bool IsVisible { get; private set; }

        public void DisplayText(string text)
        {
            textBox.Text = text;
        }

        public bool HandleInput(Keys key)
        {
            if (key != SelectKey)
                return false;

            if (textBox.HasNext())
                textBox.NextLine();
            else
                OnAllLineShowed(this, null);

            return true;
        }

        public void ResetText()
        {
            textBox.Text = "";
        }

        protected override void Update()
        {
        }
    }
}