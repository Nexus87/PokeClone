using GameEngine.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public class NullRenderer : IRenderer
    {
        public ISpriteBatch SpriteBatch { get; set; }
        public void Render(IArea area)
        {
        }
    }
}