using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameEngineComponents
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

        public InputComponent(Configuration config) : this(new KeyboardManager(), config.KeyMap) { }

        public void Update(GameTime gameTime)
        {
            manager.Update();

            foreach (var entry in keyMap)
            {
                if (HasKeyChangedToDown(entry.Key))
                    Handler.HandleKeyInput(entry.Value);
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
