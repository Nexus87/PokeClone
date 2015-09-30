using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public class InputComponent : GameComponent
    {
        MenuComponent menu;
        public InputComponent(MenuComponent menu, Game game) : base(game)
        {
            this.menu = menu;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
                menu.HandleDirection(Direction.Left);
            if (state.IsKeyDown(Keys.Right))
                menu.HandleDirection(Direction.Right);
            if (state.IsKeyDown(Keys.Up))
                menu.HandleDirection(Direction.Up);
            if (state.IsKeyDown(Keys.Down))
                menu.HandleDirection(Direction.Down);
        }
    }
}
