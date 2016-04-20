using GameEngine.Wrapper;
using System;

namespace GameEngine.Graphics.Basic
{
    public interface IGraphicalText
    {
        float CalculateTextLength(string testText);
        void Draw(ISpriteBatch batch);
        float GetSingleCharWidth();
        void Setup();

        ISpriteFont SpriteFont { get; set; }

        string Text { get; set; }

        float CharHeight { get; set; }
        float TextWidth { get; }

        float XPosition { get; set; }
        float YPosition { get; set; }
    }
}
