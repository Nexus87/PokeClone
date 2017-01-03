using Microsoft.Xna.Framework.Input;

namespace GameEngine.Components
{
    internal class KeyboardManager : IKeyboardManager
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
