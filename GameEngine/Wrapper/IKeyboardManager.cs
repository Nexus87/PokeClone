using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Wrapper
{
    interface IKeyboardManager
    {
        bool IsKeyDown(Keys key);
        void Update();
        bool WasKeyDown(Keys key);
    }
}
