using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    class InputComponent : GameComponent
    {
        private KeyboardState oldState;
        internal List<Keys> Keys = new List<Keys>();
        internal IInputHandler handler;

        public InputComponent(Game game) : base(game)
        { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var state = Keyboard.GetState();
            foreach (var entry in Keys)
            {
                handler.HandleInput(entry);
            }

            oldState = state;
        }
    }
}
