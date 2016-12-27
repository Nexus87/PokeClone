using System.Collections.Generic;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Texture = GameEngine.GUI.Configuration.Texture;

namespace GameEngine.Core.TextureLoader
{
    public class SingleTextureProvider : ITextureProvider
    {
        private readonly Texture _texture;
        private readonly ContentManager _contentManager;
        private ITexture2D _texture2D;
        private readonly List<string> _providedNames = new List<string>();

        public SingleTextureProvider(Texture texture, ContentManager contentManager)
        {
            _texture = texture;
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

            return new TextureBox(_texture2D);
        }

        private void LoadTexture()
        {
            _texture2D = new XnaTexture2D(_contentManager.Load<Texture2D>(_texture.FileName));
        }
    }
}