using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class MenuItem
    {
        SpriteFont font;
        Texture2D arrow;
        Rectangle arrowConstraints;
        Vector2 textPosition;

        internal MenuItem(SpriteFont font, Texture2D arrow)
        {
            this.font = font;
            this.arrow = arrow;

            var size = font.MeasureString("A");
            arrowConstraints = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            textPosition = new Vector2(size.X + 5, 0);
        }

        public bool Selected { get; set; }
        public String Text { get; set; }

        public void Draw(Vector2 origin, SpriteBatch batch, GameTime time)
        {
            if (Selected)
            {
                arrowConstraints.Location = origin.ToPoint();
                batch.Draw(arrow, arrowConstraints, Color.White);
            }
            batch.DrawString(font, Text, origin + textPosition, Color.Black);
        }

        public void Setup(Rectangle screen, ContentManager content)
        {

        }
    }
}
