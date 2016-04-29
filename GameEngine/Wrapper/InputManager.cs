using GameEngine.Wrapper;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
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
