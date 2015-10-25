using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface IGraphicComponent
    {
        Game Game { get; }

        void Setup(Rectangle screen, ContentManager content);
        void Draw(Vector2 origin, SpriteBatch batch, GameTime time);
        void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight);
    }

    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected AbstractGraphicComponent() {}
        protected AbstractGraphicComponent(Game game)
        {
            Game = game;
        }

        public Game Game { get; protected set; }

        public abstract void Draw(Vector2 origin, SpriteBatch batch, GameTime time);
        public virtual void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {}

        public abstract void Setup(Rectangle screen, ContentManager content);
    }
}
