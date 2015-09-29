using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleLib.GraphicComponent
{
    public class SelectBox : AbstractGraphicComponent
    {
        readonly String[] mainMenu = { "Attack", "Item", "PKMN", "Escape" };
        readonly Vector2 margin = new Vector2(50, 40);
        readonly Vector2 xSpacing = new Vector2(150, 0);
        readonly Vector2 ySpacing = new Vector2(0, 50);
        readonly Vector2 offsetArrow = new Vector2(-20, 0);
        SpriteFont font;
        Texture2D border;
        Texture2D arrow;
        List<MenuItem> items = new List<MenuItem>();
        Random rnd = new Random();
        MenuItem lastSelected;
        TimeSpan lastTime;

        public SelectBox(Game game) : base(game)
        {}

        void update(GameTime time)
        {
            if (lastTime == null)
            {
                lastTime = new TimeSpan(time.TotalGameTime.Ticks);
                return;
            }

            var limit = new TimeSpan(0, 0, 0, 2, 0);
            var diff = time.TotalGameTime.Subtract(lastTime);
            if(diff > limit)
            {
                
                lastSelected.Selected = false;
                lastSelected = items[rnd.Next(0, items.Count)];
                lastSelected.Selected = true;
                lastTime = new TimeSpan(time.TotalGameTime.Ticks);
            }
        }
        public override void Draw(SpriteBatch batch, GameTime time)
        {
            update(time);
            batch.Draw(border, Constraints, Color.White);
            foreach (var item in items)
                item.Draw(batch, time);
        }

        public override void Setup(Rectangle screen)
        {
            border = Game.Content.Load<Texture2D>("border");
            font = Game.Content.Load<SpriteFont>("MenuFont");
            arrow = Game.Content.Load<Texture2D>("arrow");

            int i = 0;
            var origin = Constraints.Location.ToVector2() + margin;
            foreach (var text in mainMenu)
            {
                var item = new MenuItem(font, arrow, Game);
                var x = (i % 2) * xSpacing; ;
                var y = ((int)(i / 2.0f)) * ySpacing;
                item.Constraints = new Rectangle((origin + x + y).ToPoint(),  (x + y).ToPoint());
                item.Text = text;
                item.Setup(screen);
                items.Add(item);
                i++;
            }
            items[0].Selected = true;
            lastSelected = items[0];
        }
    }
}
