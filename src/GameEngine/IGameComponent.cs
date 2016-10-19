using Microsoft.Xna.Framework;

namespace GameEngine
{
    public interface IGameComponent
    {
        void Initialize();
        void Update(GameTime time);
    }
}
