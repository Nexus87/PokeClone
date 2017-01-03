using System.Collections.Generic;
using System.Linq.Expressions;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics
{
    public class Scene
    {
        private ITexture2D _background;
        private readonly List<Sprite> _sprites = new List<Sprite>();
        private Vector2 _scenePosition = new Vector2(0, 0);
        private Vector2 _backgroundSize = new Vector2(0, 0);


        public void SetBackground(ITexture2D backgroundImage, Vector2 size)
        {
            _background = backgroundImage;
            _backgroundSize = size;
        }

        public void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
        }

        public void RemoveSprite(Sprite sprite)
        {
            _sprites.Remove(sprite);
        }

        internal void DrawScene(ISpriteBatch batch)
        {
            if(_background != null)
                batch.Draw(_background, _scenePosition, _backgroundSize, Color.White);

            foreach (var sprite in _sprites)
            {
                batch.Draw(sprite.Texture, ToRelativePosition(sprite.Position), Color.White, sprite.SpriteEffect);
            }
        }

        private Rectangle ToRelativePosition(Rectangle spritePosition)
        {
            return new Rectangle(spritePosition.Location + _scenePosition.ToPoint(), spritePosition.Size);
        }
    }
}