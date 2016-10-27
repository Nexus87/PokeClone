using GameEngine.Graphics.General;

namespace GameEngine.Graphics.NewGUI.Renderers
{
    public class NullRenderer : IRenderer
    {
        public ISpriteBatch SpriteBatch { get; set; }
        public void Render(IArea area)
        {
        }
    }
}