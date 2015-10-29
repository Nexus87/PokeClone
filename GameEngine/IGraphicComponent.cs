using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface IGraphicComponent
    {
        void Setup(Rectangle screen, ContentManager content);
        void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight);
    }

    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected AbstractGraphicComponent() {}

        public abstract void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight);

        public abstract void Setup(Rectangle screen, ContentManager content);
    }
}
