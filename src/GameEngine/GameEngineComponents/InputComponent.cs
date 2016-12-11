using System.Collections.Generic;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.GameEngineComponents
{
    internal class InputComponent : IGameComponent
    {
        private readonly IReadOnlyDictionary<Keys, CommandKeys> keyMap;
        internal IInputHandler Handler;
        private readonly IKeyboardManager manager;

        internal InputComponent(IKeyboardManager manager, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            this.manager = manager;
            this.keyMap = keyMap;
        }

        public InputComponent(Configuration.Configuration config) : this(new KeyboardManager(), config.KeyMap) { }

        public void Update(GameTime gameTime)
        {
            manager.Update();

            foreach (var entry in keyMap)
            {
                if (HasKeyChangedToDown(entry.Key))
                    Handler.HandleInput(entry.Value);
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
