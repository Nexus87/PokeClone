using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float Width { get { return width; } set { width = value; CalculateScale(); } }
        public float Height { get { return height; } set { height = value; CalculateScale(); } }

        protected float width;
        protected float height;

        protected Vector2 position;
        protected Vector2 scale;

        public abstract void Draw(GameTime time, SpriteBatch batch);
        public abstract void Setup(ContentManager content);

        protected virtual void CalculateScale() { }
    }
}
