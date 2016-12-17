using GameEngine.GUI.Graphics.General;

namespace GameEngine.GUI.Graphics
{
    public interface IGraphicalText
    {
        float CalculateTextLength(string testText);
        void Draw(ISpriteBatch batch);
        float GetSingleCharWidth();
        float GetSingleCharWidth(float charHeight);
        void Setup();

        ISpriteFont SpriteFont { get; set; }

        string Text { get; set; }

        float CharHeight { get; set; }
        float TextWidth { get; }

        float XPosition { get; set; }
        float YPosition { get; set; }
    }
}
