using System.Collections.Generic;
using System.Linq;
using GameEngine.Core.ECS.Messages;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.ECS.Systems
{
    public class InputSystem : ISystem
    {
        private readonly IReadOnlyDictionary<Keys, CommandKeys> _keyMap;
        private readonly KeyboardManager _keyboardManager;
        private MessagingSystem _messagingSystem;

        public InputSystem(IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            _keyMap = keyMap;
            _keyboardManager = new KeyboardManager();
        }
        public void Init(MessagingSystem messagingSystem)
        {
            _messagingSystem = messagingSystem;
        }

        public void Update(GameTime time, EntityManager entityManager)
        {
            _keyboardManager.Update();

            foreach (var entry in _keyMap.Where(x => HasKeyChangedToDown(x.Key)))
            {
                _messagingSystem.SendMessage(new KeyInputMessage{Key = entry.Value});
            }
        }

        private bool HasKeyChangedToDown(Keys keys)
        {
            return _keyboardManager.IsKeyDown(keys) && !_keyboardManager.WasKeyDown(keys);
        }
    }

    internal class KeyboardManager
    {
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        public void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public bool WasKeyDown(Keys key)
        {
            return _previousState.IsKeyDown(key);
        }
    }
}
