using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    public class TextureProvider
    {
        public ContentManager Content { get; set; }
        public TextureProvider(ContentManager content)
        {
            Content = content;
        }

        public TextureProvider() { }

        public ITexture2D GetTexturesFront(int id)
        {
            if (id == -1)
                return null;
            var texture = new XNATexture2D("charmander-front", Content);
            texture.LoadContent();
            return texture;
        }

        public ITexture2D GetTextureBack(int id)
        {
            if (id == -1)
                return null;
            var texture = new XNATexture2D("charmander-back", Content);
            texture.LoadContent();
            return texture;
        }
    }
}
