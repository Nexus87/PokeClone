using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace GameEngine
{
 
    class InputComponent : GameComponent
    {
        private KeyboardState oldState;
        internal List<Keys> Keys = new List<Keys>();
        internal IInputHandler handler;
        private InputManager manager;

        internal InputComponent(Game game, InputManager manager) : base(game)
        {
            this.manager = manager;
        }

        public InputComponent(Game game) : this(game, new InputManager()) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            manager.Update();

            foreach (var entry in Keys)
            {
                if (manager.IsKeyDown(entry) && !oldState.IsKeyDown(entry))
                    handler.HandleInput(entry);
            }

            oldState = manager.GetState();
        }
    }
}
