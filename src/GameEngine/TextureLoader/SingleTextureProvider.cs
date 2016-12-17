using System.Collections.Generic;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Texture = GameEngine.Configuration.Texture;

namespace GameEngine.TextureLoader
{
    public class SingleTextureProvider : ITextureProvider
    {
        private readonly Texture texture;
        private readonly ContentManager contentManager;
        private ITexture2D texture2D;
        private readonly List<string> providedNames = new List<string>();

        public SingleTextureProvider(Texture texture, ContentManager contentManager)
        {
            this.texture = texture;
            this.contentManager = contentManager;
            providedNames.Add(texture.Key);
        }
        public IEnumerable<string> GetProvidedNames()
        {
            return providedNames;
        }

        public IImageBox GetTexture(string name)
        {
            if (texture2D == null)
                LoadTexture();

            return new TextureBox(texture2D);
        }

        private void LoadTexture()
        {
            texture2D = new XnaTexture2D(contentManager.Load<Texture2D>(texture.FileName));
        }
    }
}