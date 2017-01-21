using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public class Sprite : ISprite
    {
        public ITexture2D Texture { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public Rectangle Position { get; set; }
    }
}