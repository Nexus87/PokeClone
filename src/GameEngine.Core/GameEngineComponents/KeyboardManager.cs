using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameEngineComponents
{
    internal class KeyboardManager : IKeyboardManager
    {
        private KeyboardState currentState;
        private KeyboardState previousState;

        public void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public bool WasKeyDown(Keys key)
        {
            return previousState.IsKeyDown(key);
        }
    }
}
