using System.Linq;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.ECS.Systems
{
    public class InputSystem
    {
        private readonly KeyboardManager _keyboardManager;
        private IMessageBus _messageBus;

        public InputSystem()
        {
            _keyboardManager = new KeyboardManager();
        }

        public void Init(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Update(TimeAction action, IEntityManager entityManager)
        {
            var keyMap = entityManager.GetFirstCompnentOfType<KeyMapComponent>().KeyMap;
            _keyboardManager.Update();

            foreach (var entry in keyMap.Where(x => HasKeyChangedToDown(x.Key)))
            {
                _messageBus.SendAction(new KeyInputAction{Key = entry.Value});
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
