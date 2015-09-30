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
        Rectangle Constraints = new Rectangle();
        readonly Vector2 margin = new Vector2(50, 30);
        Texture2D border;
        SpriteFont font;
        String _currentMessage = "Text";
        
        public override Point Size
        {
            get { return Constraints.Size; }
            set { Constraints.Size = value; }
        }
        public MessageBox(SpriteFont font, Texture2D border, Game game) : base(game)
        {
            this.border = border;
            this.font = font;
        }

        public override void Draw(Vector2 Origin, SpriteBatch batch, GameTime gameTime)
        {
            Constraints.Location = Origin.ToPoint();
            Vector2 textVec = Origin + margin;
            batch.Draw(border,Constraints, Color.White);
            batch.DrawString(font, _currentMessage, textVec, Color.Black);
        }
        public override void Setup(Rectangle screen)
        {
        }
    }
}
