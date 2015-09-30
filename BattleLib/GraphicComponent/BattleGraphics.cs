using BattleLib.Components;
using GameEngine;
using Microsoft.Xna.Framework;
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

        MessageBox box;
        MenuGraphics menu;
        MainMenuModel model;

        public BattleGraphics(MainMenuModel model, Game game) : base(game)
        {
            this.model = model;
        }

        public override void Setup(Rectangle screen)
        {
            screenWidth = screen.Size.X;
            screenHeight = screen.Size.Y;

            var border = Game.Content.Load<Texture2D>("border");
            var arrow = Game.Content.Load<Texture2D>("arrow");
            var font = Game.Content.Load<SpriteFont>("MenuFont");

            box = new MessageBox(font, border, Game);
            var select = new SelectBox(font, border, arrow, model, Game);

            menu = new MenuGraphics();
            menu.Add(MenuType.Main, select);
            menu.SetMenu(MenuType.Main);
        }

        public override void Draw(Vector2 origin, SpriteBatch batch, GameTime time)
        {
            box.Draw(time, batch, screenWidth, screenHeight);
            menu.Draw(time, batch, screenWidth, screenHeight);
        }
    }
}
