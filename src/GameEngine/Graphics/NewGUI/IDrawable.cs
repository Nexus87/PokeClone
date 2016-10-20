using GameEngine.Graphics.General;

namespace GameEngine.Graphics.NewGUI
{
    public interface IDrawable
    {
        void Draw(ISpriteBatch spriteBatch, IArea area);
    }

    public interface IImage : IDrawable
    {
        double ImageWidth { get; set; }
        double ImageHeight { get; set; }
    }
}