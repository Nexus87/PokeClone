using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    internal interface IKeyboardManager
    {
        bool IsKeyDown(Keys key);
        void Update();
        bool WasKeyDown(Keys key);
    }
}
