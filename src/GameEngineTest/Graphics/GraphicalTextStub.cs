using System;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;

namespace GameEngineTest.Graphics
{
    public class GraphicalTextStub
    {
        public float SingleCharWidth { get; set; }

        public float CalculateTextLength(string testText)
        {
            return SingleCharWidth * testText.Length;
        }

        public void Draw(ISpriteBatch batch)
        {
        }

        public float GetSingleCharWidth()
        {
            return SingleCharWidth;
        }

        public void Setup()
        {
        }

        public ISpriteFont SpriteFont { get; set; }

        public string Text { get; set; }

        public float CharHeight { get; set; }

        public float TextWidth
        {
            get { throw new NotImplementedException(); }
        }

        public float XPosition { get; set; }
        public float YPosition { get; set; }


        public float GetSingleCharWidth(float charHeight)
        {
            return SingleCharWidth;
        }
    }
}