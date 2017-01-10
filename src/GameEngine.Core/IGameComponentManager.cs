using GameEngine.Entities;
using GameEngine.Graphics;

namespace GameEngine.Core
{
    public interface IGameComponentManager
    {
        void AddGameComponent(IGameEntity entity);
        void RemoveGameComponent(IGameEntity entity);
        Scene Scene{ get; set; }
    }
}