using GameEngine.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public class NullRenderer<T> : IRenderer<T> where T : IGuiComponent
    {
        public void Render(ISpriteBatch spriteBatch, T component)
        {
        }
    }
}