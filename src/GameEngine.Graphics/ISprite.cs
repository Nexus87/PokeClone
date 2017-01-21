using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public interface ISprite
    {
        ITexture2D Texture { get; }
        SpriteEffects SpriteEffect { get; }
        Rectangle Position { get; set; }
    }
}