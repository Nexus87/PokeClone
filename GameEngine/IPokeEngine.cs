using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace GameEngine
{
    public interface IPokeEngine
    {
        float ScreenHeight { get; }
        float ScreenWidth { get; }
        Color BackgroundColor { get; }
        GUIManager GUIManager { get; }
        ContentManager Content { get; }
        void Exit();
        void AddGameComponent(IGameComponent component);
        void RemoveGameComponent(IGameComponent component);
        IGraphicComponent Graphic { get; set; }
    }
}
