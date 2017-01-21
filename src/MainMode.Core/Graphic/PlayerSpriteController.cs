using GameEngine.Graphics;
using MainMode.Graphic;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphic
{
    public class PlayerSpriteController : SpriteControllerEntity
    {
        private readonly Scene _scene;

        public PlayerSpriteController(Scene scene, SpriteEntityTextures spriteEntityTextures)
            : base(spriteEntityTextures)
        {
            _scene = scene;
        }

        protected override void MoveSprite(Point position)
        {
            var offset = 0.5f * Sprite.Position.Size.ToVector2();
            _scene.CenterPosition(position.ToVector2() + offset);
            Sprite.Position = new Rectangle(position, Sprite.Position.Size);
        }
    }
}