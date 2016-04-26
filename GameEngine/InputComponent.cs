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

    public enum CommandKeys
    {
        Up,
        Down,
        Left,
        Right,
        Select,
        Back
    }

    class InputComponent : IGameComponent
    {
        KeyboardState oldState;
        IReadOnlyDictionary<Keys, CommandKeys> keyMap;
        internal IInputHandler handler;
        InputManager manager;

        internal InputComponent(Game game, InputManager manager, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            this.manager = manager;
            this.keyMap = keyMap;
        }

        public InputComponent(Game game, Configuration config) : this(game, new InputManager(), config.KeyMap) { }

        public void Update(GameTime gameTime)
        {
            manager.Update();

            foreach (var entry in keyMap)
            {
                if (manager.IsKeyDown(entry.Key) && !oldState.IsKeyDown(entry.Key))
                    handler.HandleInput(entry.Value);
            }

            oldState = manager.GetState();
        }

        public void Initialize()
        {
        }
    }
}
