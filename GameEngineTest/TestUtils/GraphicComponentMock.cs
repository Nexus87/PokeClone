using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace GameEngineTest.TestUtils
{
    public class GraphicComponentMock : IGraphicComponent
    {
        public bool WasDrawn { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public IPokeEngine Game { get; set; }

        public void PlayAnimation(IAnimation animation)
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged;
        public event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged;

        public float XPosition { get; set; }
        public float YPosition { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }
        public Action DrawCallback = null;
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            WasDrawn = true;
            if (DrawCallback != null)
                DrawCallback();
        }

        public void Setup()
        {

        }



        public Color Color { get; set; }
    }
}
