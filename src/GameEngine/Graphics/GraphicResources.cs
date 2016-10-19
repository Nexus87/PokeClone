using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public class GraphicResources
    {
        internal XnaTexture2D pixel;
        internal ISpriteFont DefaultFont { get; set; }
        internal ITexture2D DefaultArrowTexture { get; set; }
        internal ITexture2D DefaultBorderTexture { get; set; }
        internal ITexture2D Pixel { get { return pixel; } }
        internal ITexture2D Cup { get; set; }
        internal ContentManager ContentManager { get; set; }
        internal Configuration.Configuration Configuration { get; set; }

        internal GraphicResources(Configuration.Configuration config, ContentManager content)
        {
            DefaultArrowTexture = new XnaTexture2D(config.DefaultArrowTexture, content);
            DefaultFont = new XnaSpriteFont(config.DefaultFont, content);
            DefaultBorderTexture = new XnaTexture2D(config.DefaultBorderTexture, content);
            pixel = new XnaTexture2D();
            Cup = new XnaTexture2D("circle", content);
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
