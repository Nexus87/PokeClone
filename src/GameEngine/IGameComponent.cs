using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public interface IGameComponent
    {
        void Initialize();
        void Update(GameTime time);
    }
}
