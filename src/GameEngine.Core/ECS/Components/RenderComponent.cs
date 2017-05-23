using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core.ECS.Components
{
    public class RenderComponent
    {
        public Rectangle Destination { get; set; }
        public int Z { get; set; }
        public ITexture2D Texture { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
    }
}
