using GameEngine.GameEngineComponents;
using Microsoft.Xna.Framework.Input;

namespace GameEngineTest.TestUtils
{
    internal class KeyboardManagerStub : IKeyboardManager
    {
        private KeyboardState oldState;
        private KeyboardState newState;

        private KeyboardState targetState;

        public KeyboardManagerStub(KeyboardState state)
        {
            targetState = state;
        }
        public bool IsKeyDown(Keys key)
        {
            return newState.IsKeyDown(key);
        }

        public void Update()
        {
            oldState = newState;
            newState = targetState;
        }

        public bool WasKeyDown(Keys key)
        {
            return oldState.IsKeyDown(key);
        }
    }
}
