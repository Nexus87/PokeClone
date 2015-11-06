using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public interface IGraphicComponent
    {
        float X { get; set; }
        float Y { get; set; }

        float Width { get; set; }
        float Height { get; set; }

        void Draw(GameTime time, SpriteBatch batch);
        void Setup(ContentManager content);
    }
}
