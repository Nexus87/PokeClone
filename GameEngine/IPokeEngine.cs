using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace GameEngine
{
    public interface IPokeEngine
    {
        ContentManager Content { get; }
        void Exit();
        void AddGameComponent(IGameComponent component);
        void RemoveGameComponent(IGameComponent component);
        IGraphicComponent Graphic { get; set; }
    }
}
