using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class GraphicResources
    {
        internal XNATexture2D pixel;
        internal ISpriteFont DefaultFont { get; set; }
        internal ITexture2D DefaultArrowTexture { get; set; }
        internal ITexture2D DefaultBorderTexture { get; set; }
        internal ITexture2D Pixel { get { return pixel; } }
        internal ITexture2D Cup { get; set; }
        internal ContentManager ContentManager { get; set; }
        internal Configuration Configuration { get; set; }

        internal GraphicResources(Configuration config, ContentManager content)
        {
            DefaultArrowTexture = new XNATexture2D(config.DefaultArrowTexture, content);
            DefaultFont = new XNASpriteFont(config.DefaultFont, content);
            DefaultBorderTexture = new XNATexture2D(config.DefaultBorderTexture, content);
            pixel = new XNATexture2D();
            Cup = new XNATexture2D("circle", content);
            ContentManager = content;
            Configuration = config;
        }

        public void Setup(Game game)
        {
            pixel.Texture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });
        }

    }
}
