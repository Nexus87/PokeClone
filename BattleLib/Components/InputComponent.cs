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
        MenuModel model;
        public InputComponent(MenuModel model, Game game) : base(game)
        {
            this.model = model;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
                model.HandleDirection(Direction.Left);
            if (state.IsKeyDown(Keys.Right))
                model.HandleDirection(Direction.Right);
            if (state.IsKeyDown(Keys.Up))
                model.HandleDirection(Direction.Up);
            if (state.IsKeyDown(Keys.Down))
                model.HandleDirection(Direction.Down);
        }
    }
}
