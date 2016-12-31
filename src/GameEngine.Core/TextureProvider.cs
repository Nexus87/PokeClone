using GameEngine.GUI.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    [GameService(typeof(TextureProvider))]
    public class TextureProvider
    {
        private ContentManager Content { get; set; }
        public TextureProvider(ContentManager content)
        {
            Content = content;
        }


        public ITexture2D GetTexturesFront(int id)
        {
            if (id == -1)
                return null;
            var texture = new XnaTexture2D("charmander-front", Content);
            texture.LoadContent();
            return texture;
        }

        public ITexture2D GetTextureBack(int id)
        {
            if (id == -1)
                return null;
            var texture = new XnaTexture2D("charmander-back", Content);
            texture.LoadContent();
            return texture;
        }

        public ITexture2D GetIcon(int id)
        {
            return GetTexturesFront(id);
        }
    }
}
