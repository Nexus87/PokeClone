using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
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
        IReadOnlyDictionary<Keys, CommandKeys> keyMap;
        internal IInputHandler handler;
        IKeyboardManager manager;

        internal InputComponent(Game game, IKeyboardManager manager, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            this.manager = manager;
            this.keyMap = keyMap;
        }

        public InputComponent(Game game, Configuration config) : this(game, new KeyboardManager(), config.KeyMap) { }

        public void Update(GameTime gameTime)
        {
            manager.Update();

            foreach (var entry in keyMap)
            {
                if (HasKeyChangedToDown(entry.Key))
                    handler.HandleInput(entry.Value);
            }

        }

        private bool HasKeyChangedToDown(Keys keys)
        {
            return manager.IsKeyDown(keys) && !manager.WasKeyDown(keys);
        }

        public void Initialize()
        {
        }
    }
}
