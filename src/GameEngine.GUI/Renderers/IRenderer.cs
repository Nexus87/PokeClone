using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public interface IRenderer<in TComponentType> where TComponentType : IGraphicComponent
    {
        void Render(ISpriteBatch spriteBatch, TComponentType component);
    }
}