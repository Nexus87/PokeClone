using GameEngine.Graphics.Basic;
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

        public ItemBox(String displayedText)
        {
            arrow = new TextureBox("arrow");
            textBox = new TextBox("MenuFont");
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
            float internalHight = textBox.TextSize;

            arrow.X = X;
            arrow.Y = Y;
            arrow.Width = Math.Min(internalHight, Width);
            arrow.Height = internalHight;

            textBox.X = X + internalHight;
            textBox.Y = Y;
            textBox.Width = Math.Max(Width - internalHight, 0);
            textBox.Height = internalHight;

        }
    }
}
