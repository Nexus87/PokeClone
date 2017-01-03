using Microsoft.Xna.Framework.Input;

namespace GameEngine.Components
{
    internal interface IKeyboardManager
    {
        bool IsKeyDown(Keys key);
        void Update();
        bool WasKeyDown(Keys key);
    }
}
