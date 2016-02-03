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


    public abstract class IGraphicComponent : DrawableGameComponent
    {
        public IGraphicComponent(Game game) : base(game) { }

        public abstract event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged;
        public abstract event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged;

        public abstract float X { get; set; }
        public abstract float Y { get; set; }

        public abstract float Width { get; set; }
        public abstract float Height { get; set; }

        public abstract void Draw(GameTime time, ISpriteBatch batch);
        public abstract void Setup(ContentManager content);
    }
}
