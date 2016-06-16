﻿using GameEngine.Registry;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    [GameComponentAttribute(registerType: typeof(IKeyboardManager))]
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