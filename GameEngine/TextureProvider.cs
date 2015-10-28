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

        Texture2D getTextures(int id)
        {
            return Content.Load<Texture2D>(id.ToString());
        }
    }
}
