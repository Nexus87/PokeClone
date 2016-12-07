using GameEngine.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public interface IRenderer<in TComponentType> where TComponentType : IGuiComponent
    {
        void Render(ISpriteBatch spriteBatch, TComponentType component);
    }
}