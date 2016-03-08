using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class TextureProvider
    {
        public ContentManager Content { private get; set; }
        public TextureProvider(ContentManager content)
        {
            Content = content;
        }

        public TextureProvider() { }

        public Texture2D getTexturesFront(int id)
        {
            if (id == -1)
                return null;
            return Content.Load<Texture2D>("charmander-front");
        }

        public Texture2D getTextureBack(int id)
        {
            if (id == -1)
                return null;
            return Content.Load<Texture2D>("charmander-back");
        }
    }
}
