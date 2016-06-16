using GameEngine.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

    [GameComponentAttribute(SingleInstance=true)]
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
