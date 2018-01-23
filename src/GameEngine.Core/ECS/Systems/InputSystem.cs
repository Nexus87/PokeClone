using System;
using System.Linq;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;
using GameEngine.Globals;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.ECS.Systems
{
    public class InputSystem
    {
        private readonly KeyboardManager _keyboardManager;
        private readonly IMessageBus _messageBus;

        public InputSystem(IMessageBus messageBus)
        {
            _keyboardManager = new KeyboardManager();
            _messageBus = messageBus;
        }

        public void Update(TimeAction action, IEntityManager entityManager)
        {
            var keyMap = entityManager.GetFirstComponentOfType<KeyMapComponent>().KeyMap;
            var guiVisible = entityManager.GetFirstComponentOfType<GuiComponent>().GuiVisible;
            var send = guiVisible
                ? (Action<CommandKeys>) (key => _messageBus.SendAction(new GuiKeyInputAction(key)))
                : key => _messageBus.SendAction(new KeyInputAction(key));

            _keyboardManager.Update();

            keyMap.Where(x => HasKeyChangedToDown(x.Key))
                .Select(x => x.Value)
                .ToList()
                .ForEach(send);
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