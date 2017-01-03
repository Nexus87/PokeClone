using GameEngine.Components;
using Microsoft.Xna.Framework.Input;

namespace GameEngineTest.TestUtils
{
    internal class KeyboardManagerStub : IKeyboardManager
    {
        private KeyboardState _oldState;
        private KeyboardState _newState;

        private readonly KeyboardState _targetState;

        public KeyboardManagerStub(KeyboardState state)
        {
            _targetState = state;
        }
        public bool IsKeyDown(Keys key)
        {
            return _newState.IsKeyDown(key);
        }

        public void Update()
        {
            _oldState = _newState;
            _newState = _targetState;
        }

        public bool WasKeyDown(Keys key)
        {
            return _oldState.IsKeyDown(key);
        }
    }
}
