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
        MessageBox _box;
        Vector2 boxOffset;
        SelectBox _selection;
        Vector2 selectionOffset;
        MenuModel model;
        public BattleGraphics(MenuModel model, Game game) : base(game)
        {
            this.model = model;
        }

        public override void Setup(Rectangle screen)
        {
            int y = (int) (0.7 * screen.Size.Y);
            int height =  screen.Size.Y - y;

            int x = (int)(screen.Size.X/2.0f);
            int width = screen.Size.X - x;

            var border = Game.Content.Load<Texture2D>("border");
            var arrow = Game.Content.Load<Texture2D>("arrow");
            var font = Game.Content.Load<SpriteFont>("MenuFont");

            _box = new MessageBox(font, border, Game);
            _selection = new SelectBox(font, border, arrow, model, Game);

            _box.Size = new Point(screen.Size.X, height);
            boxOffset = new Vector2(0, y);
            _selection.Size = new Point(width, height);
            selectionOffset = new Vector2(x, y);

            _box.Setup(screen);
            _selection.Setup(screen);
        }

        public override void Draw(Vector2 origin, SpriteBatch batch, GameTime time)
        {
            _box.Draw(origin + boxOffset, batch, time);
            _selection.Draw(origin + selectionOffset, batch, time);
        }
    }
}
