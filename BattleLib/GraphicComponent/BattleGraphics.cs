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
        SelectBox _selection;

        public BattleGraphics(Game game) : base(game)
        {
            _box = new MessageBox(game);
            _selection = new SelectBox(game);
        }

        public override void Setup(Rectangle screen)
        {
            int y = (int) (0.7 * screen.Size.Y);
            int height =  screen.Size.Y - y;

            int x = (int)(screen.Size.X/2.0f);
            int width = screen.Size.X - x;

            _box.Constraints = new Rectangle(0, y, screen.Size.X, height);
            _selection.Constraints = new Rectangle(x, y, width, height);

            _box.Setup(screen);
            _selection.Setup(screen);


        }

        public override void Draw(SpriteBatch batch, GameTime time)
        {
            _box.Draw(batch, time);
            _selection.Draw(batch, time);
        }
    }
}
