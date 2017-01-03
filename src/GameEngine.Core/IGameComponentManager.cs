using GameEngine.Entities;
using GameEngine.GUI;

namespace GameEngine.Core
{
    public interface IGameComponentManager
    {
        void AddGameComponent(IGameEntity entity);
        void RemoveGameComponent(IGameEntity entity);
        IGraphicComponent Graphic { get; set; }
    }
}