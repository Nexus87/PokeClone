using GameEngine.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public interface IRenderer
    {
        ISpriteBatch SpriteBatch { get; set; }
        void Render(IArea area);
    }
}