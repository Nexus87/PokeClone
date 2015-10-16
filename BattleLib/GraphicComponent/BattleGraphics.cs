using BattleLib.Components;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponent
{
    public class BattleGraphics : AbstractGraphicComponent
    {
        int screenWidth;
        int screenHeight;

        public MessageBox MessageBox { get; set; }
        public MenuGraphics Menu { get; set; }

        public BattleGraphics(Game game) : base(game)
        {
        }

        public override void Setup(Rectangle screen, ContentManager content)
        {
            screenWidth = screen.Size.X;
            screenHeight = screen.Size.Y;

            var border = Game.Content.Load<Texture2D>("border");
            var arrow = Game.Content.Load<Texture2D>("arrow");
            var font = Game.Content.Load<SpriteFont>("MenuFont");

            MessageBox.Setup(screen, content);
            Menu.Setup(content);
        }

        public override void Draw(Vector2 origin, SpriteBatch batch, GameTime time)
        {
            MessageBox.Draw(time, batch, screenWidth, screenHeight);
            Menu.Draw(time, batch, screenWidth, screenHeight);
        }

    }
}
