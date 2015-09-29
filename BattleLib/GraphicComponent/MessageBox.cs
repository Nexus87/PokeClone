using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine;

namespace BattleLib.GraphicComponent
{
    internal class MessageBox : AbstractGraphicComponent
    {
        readonly Vector2 margin = new Vector2(50, 30);
        Texture2D _border;
        SpriteFont _font;
        String _currentMessage = "Text";

        public MessageBox(Game game) : base(game)
        {}

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Vector2 textVec = Constraints.Location.ToVector2() + margin;
            batch.Draw(_border, Constraints, Color.White);
            batch.DrawString(_font, _currentMessage, textVec, Color.Black);
        }
        public override void Setup(Rectangle screen)
        {
            _border = Game.Content.Load<Texture2D>("border");
            _font = Game.Content.Load<SpriteFont>("MenuFont");
        }
    }
}
