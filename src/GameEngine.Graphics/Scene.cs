using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using GameEngine.Graphics.Diagnostic;
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

        private readonly Vector2 _screenCenter;
        private readonly Rectangle _debugRectangle;
        public Scene(ScreenConstants screenConstants)
        {
            _screenCenter = new Vector2(screenConstants.ScreenWidth/2f, screenConstants.ScreenHeight/2f);
            _debugRectangle = new Rectangle(new Point((int) (_screenCenter.X - 15) , (int) (_screenCenter.Y - 15)), new Point(30, 30) );
        }
        public void CenterPosition(Vector2 position)
        {
            _scenePosition = _screenCenter - position;
        }
        public void MoveSceneTo(Vector2 position)
        {
            _scenePosition = position;
        }

        public void SetBackground(ITexture2D backgroundImage)
        {
            _background = backgroundImage;
            if(_background != null)
                _backgroundSize = new Vector2(backgroundImage.Width, backgroundImage.Height);
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


            foreach (var sprite in _sprites.Where(x => x.Texture != null))
            {
                batch.Draw(sprite.Texture, ToRelativePosition(sprite.Position), Color.White, sprite.SpriteEffect);
            }

            DebugRectangle.Rectangle?.Draw(batch, _debugRectangle, Color.Black);
        }

        private Rectangle ToRelativePosition(Rectangle spritePosition)
        {
            return new Rectangle(spritePosition.Location + _scenePosition.ToPoint(), spritePosition.Size);
        }
    }
}