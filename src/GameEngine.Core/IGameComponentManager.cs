using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Graphics;

namespace GameEngine.Core
{
    public interface IGameComponentManager
    {
        void AddGameComponent(IGameComponent component);
        void RemoveGameComponent(IGameComponent component);
        IGraphicComponent Graphic { get; set; }
        IInputHandler InputHandler { get; set; }
    }
}