using GameEngine.GUI.General;

namespace GameEngine.GUI.Renderers
{
    public interface IRenderer<in TComponentType> where TComponentType : IGraphicComponent
    {
        void Render(ISpriteBatch spriteBatch, TComponentType component);
    }
}