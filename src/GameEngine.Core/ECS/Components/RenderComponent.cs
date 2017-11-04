using System;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core.ECS.Components
{
    public class RenderComponent : Component
    {
        public int Z { get; set; }
        public ITexture2D Texture { get; set; }
        public SpriteEffects SpriteEffect { get; set; }

        public RenderComponent(Guid entityId) : base(entityId)
        {
        }
    }
}
