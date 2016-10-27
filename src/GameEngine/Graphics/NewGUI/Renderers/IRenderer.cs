using GameEngine.Graphics.General;

namespace GameEngine.Graphics.NewGUI.Renderers
{
    public interface IRenderer
    {
        ISpriteBatch SpriteBatch { get; set; }
        void Render(IArea area);
    }
}