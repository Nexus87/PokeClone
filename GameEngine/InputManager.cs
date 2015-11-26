using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class InputManager
    {
        private KeyboardState currentState;

        public virtual void Update()
        {
            currentState = Keyboard.GetState();
        }

        public virtual bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public virtual KeyboardState GetState()
        {
            return currentState;
        }
    }
}
