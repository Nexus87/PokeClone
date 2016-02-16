using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public class GraphicComponentSizeChangedArgs : EventArgs
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        public GraphicComponentSizeChangedArgs(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }

    public class GraphicComponentPositionChangedArgs : EventArgs
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public GraphicComponentPositionChangedArgs(float x, float y)
        {
            X = x;
            Y = y;
        }
    }


    public interface IGraphicComponent
    {
        PokeEngine Game { get; }

        void PlayAnimation(IAnimation animation);

        event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged;
        event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged;

        float X { get; set; }
        float Y { get; set; }

        float Width { get; set; }
        float Height { get; set; }

        void Draw(GameTime time, ISpriteBatch batch);
        void Setup(ContentManager content);
    }
}
