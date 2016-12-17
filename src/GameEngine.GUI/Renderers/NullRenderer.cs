using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public class NullRenderer<T> : IRenderer<T> where T : IGraphicComponent
    {
        public void Render(ISpriteBatch spriteBatch, T component)
        {
        }
    }
}