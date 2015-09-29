using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace BattleLib.GraphicComponent
{
    class MenuItem : AbstractGraphicComponent
    {
        SpriteFont font;
        Texture2D arrow;
        Rectangle arrowConstraints;
        Vector2 textPosition;

        internal MenuItem(SpriteFont font, Texture2D arrow, Game game) : base(game)
        {
            this.font = font;
            this.arrow = arrow;
        }

        public bool Selected { get; set; }
        public String Text { get; set; }

        public override void Draw(SpriteBatch batch, GameTime time)
        {
            if (Selected)
                batch.Draw(arrow, arrowConstraints, Color.White);
            batch.DrawString(font, Text, textPosition, Color.Black);
        }

        public override void Setup(Rectangle screen)
        {

            var size = font.MeasureString("A");
            int x = Constraints.Location.X;
            int y = Constraints.Location.Y;
            arrowConstraints = new Rectangle(x, y, (int) size.X, (int) size.Y);
            textPosition = Constraints.Location.ToVector2() + new Vector2(size.X + 5, 0);
        }
    }
}
