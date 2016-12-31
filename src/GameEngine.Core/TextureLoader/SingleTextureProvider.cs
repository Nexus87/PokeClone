using System.Collections.Generic;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Renderers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Texture = GameEngine.GUI.Configuration.Texture;

namespace GameEngine.Core.TextureLoader
{
    public class SingleTextureProvider : ITextureProvider
    {
        private readonly Texture _texture;
        private readonly IImageBoxRenderer _renderer;
        private readonly ContentManager _contentManager;
        private ITexture2D _texture2D;
        private readonly List<string> _providedNames = new List<string>();

        public SingleTextureProvider(Texture texture, IImageBoxRenderer renderer, ContentManager contentManager)
        {
            _texture = texture;
            _renderer = renderer;
            _contentManager = contentManager;
            _providedNames.Add(texture.Key);
        }
        public IEnumerable<string> GetProvidedNames()
        {
            return _providedNames;
        }

        public IImageBox GetTexture(string name)
        {
            if (_texture2D == null)
                LoadTexture();

            return new ImageBox(_renderer) {Image = _texture2D};
        }

        private void LoadTexture()
        {
            _texture2D = new XnaTexture2D(_contentManager.Load<Texture2D>(_texture.FileName));
        }
    }
}