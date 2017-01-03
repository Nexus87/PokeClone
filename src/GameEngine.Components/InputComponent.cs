using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Components
{
    internal class InputComponent : IGameComponent, IInputHandlerManager
    {
        private readonly IReadOnlyDictionary<Keys, CommandKeys> _keyMap;
        private readonly IKeyboardManager _manager;
        private readonly List<IInputHandler> _handlers = new List<IInputHandler>();
        private IInputHandler _priorizedHandler;

        internal InputComponent(IKeyboardManager manager, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            _manager = manager;
            _keyMap = keyMap;
        }

        public InputComponent(IReadOnlyDictionary<Keys, CommandKeys> keyMap) : this(new KeyboardManager(), keyMap) { }

        public void Update(GameTime gameTime)
        {
            _manager.Update();

            foreach (var entry in _keyMap.Where(x => HasKeyChangedToDown(x.Key)))
            {
                if(_priorizedHandler != null)
                    _priorizedHandler.HandleKeyInput(entry.Value);
                else
                    _handlers.ForEach(x => x.HandleKeyInput(entry.Value));
            }

        }

        private bool HasKeyChangedToDown(Keys keys)
        {
            return _manager.IsKeyDown(keys) && !_manager.WasKeyDown(keys);
        }

        public void AddHandler(IInputHandler handler, bool exclusiveHandler = false)
        {
            _handlers.Add(handler);
            if (exclusiveHandler)
                _priorizedHandler = handler;
        }

        public void RemoveHandler(IInputHandler handler)
        {
            if (_priorizedHandler == handler)
                _priorizedHandler = null;

            _handlers.Remove(handler);
        }

        public void ClearHandlers()
        {
            _priorizedHandler = null;
            _handlers.Clear();
        }
    }
}
