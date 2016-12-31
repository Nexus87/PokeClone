using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameEngineComponents
{
    internal class InputComponent : IGameComponent
    {
        private readonly IReadOnlyDictionary<Keys, CommandKeys> _keyMap;
        internal IInputHandler Handler;
        private readonly IKeyboardManager _manager;

        internal InputComponent(IKeyboardManager manager, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            _manager = manager;
            _keyMap = keyMap;
        }

        public InputComponent(Configuration config) : this(new KeyboardManager(), config.KeyMap) { }

        public void Update(GameTime gameTime)
        {
            _manager.Update();

            foreach (var entry in _keyMap)
            {
                if (HasKeyChangedToDown(entry.Key))
                    Handler.HandleKeyInput(entry.Value);
            }

        }

        private bool HasKeyChangedToDown(Keys keys)
        {
            return _manager.IsKeyDown(keys) && !_manager.WasKeyDown(keys);
        }

        public void Initialize()
        {
        }
    }
}
