using GameEngine.GUI.Graphics;

namespace GameEngine
{
    public interface IGameComponentManager
    {
        void AddGameComponent(IGameComponent component);
        void RemoveGameComponent(IGameComponent component);
        IGraphicComponent Graphic { get; set; }
        IInputHandler InputHandler { get; set; }
    }
}