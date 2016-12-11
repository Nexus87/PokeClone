using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics
{
    public class GraphicResources
    {
        private XnaTexture2D pixel;
        public ISpriteFont DefaultFont { get; set; }
        public ITexture2D DefaultArrowTexture { get; set; }
        public ITexture2D DefaultBorderTexture { get; set; }
        public ITexture2D Pixel { get { return pixel; } }
        public ITexture2D Cup { get; set; }
        public ContentManager ContentManager { get; set; }
        public Configuration.Configuration Configuration { get; set; }

        public GraphicResources(Configuration.Configuration config, ContentManager content)
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
