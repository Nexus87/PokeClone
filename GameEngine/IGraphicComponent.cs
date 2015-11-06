using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface IGraphicComponentOld
    {
        void Setup(Rectangle screen, ContentManager content);
        void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight);
    }

    public abstract class AbstractGraphicComponentOld : IGraphicComponentOld
    {
        protected AbstractGraphicComponentOld() {}

        public abstract void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight);

        public abstract void Setup(Rectangle screen, ContentManager content);
    }
}
