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
        KeyboardState oldState;
        MenuComponent menu;
        public InputComponent(MenuComponent menu, Game game) : base(game)
        {
            this.menu = menu;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            var newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
                menu.HandleDirection(Direction.Left);
            if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
                menu.HandleDirection(Direction.Right);
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
                menu.HandleDirection(Direction.Up);
            if (newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
                menu.HandleDirection(Direction.Down);
            if (newState.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
                menu.Select();
            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                menu.Back();

            oldState = newState;
        }
    }
}
