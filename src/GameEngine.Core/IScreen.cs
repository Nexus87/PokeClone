using GameEngine.Graphics.General;
using GameEngine.GUI;

namespace GameEngine.Core
{
    public interface IScreen
    {
        ISkin Skin { get; set; }
        ISpriteBatch GuiSpriteBatch { get; }
        ISpriteBatch SceneSpriteBatch { get; }
        void WindowsResizeHandler(float windowWidth, float windowHeight);
        void Draw();
    }
}