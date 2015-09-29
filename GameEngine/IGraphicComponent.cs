using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IGraphicComponent
    {
        Game Game { get; }
        Rectangle Constraints { get; set; }

        void Setup(Rectangle screen);
        void Draw(SpriteBatch batch, GameTime time);
    }

    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected AbstractGraphicComponent(Game game)
        {
            Game = game;
        }
        public Rectangle Constraints { get; set; }
        public Game Game { get; protected set;}

        public abstract void Draw(SpriteBatch batch, GameTime time);
        public abstract void Setup(Rectangle screen);
    }
}
