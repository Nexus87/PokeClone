using GameEngine.GUI.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Graphics
{
    public class GraphicResources
    {
        private static XnaTexture2D _pixel;
        public ISpriteFont DefaultFont { get; set; }
        public ITexture2D DefaultArrowTexture { get; set; }
        public ITexture2D DefaultBorderTexture { get; set; }
        public static ITexture2D Pixel => _pixel;
        public ITexture2D Cup { get; set; }
        public ContentManager ContentManager { get; set; }
        public Configuration.Configuration Configuration { get; set; }

        public GraphicResources(Configuration.Configuration config, ContentManager content)
        {
            DefaultArrowTexture = new XnaTexture2D(config.DefaultArrowTexture, content);
            DefaultFont = new XnaSpriteFont(config.DefaultFont, content);
            DefaultBorderTexture = new XnaTexture2D(config.DefaultBorderTexture, content);
            _pixel = new XnaTexture2D();
            Cup = new XnaTexture2D("circle", content);
            ContentManager = content;
            Configuration = config;
        }

        public void Setup(Game game)
        {
            _pixel.Texture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color, 1);
            _pixel.SetData(new[] { Color.White });
            DefaultFont.LoadContent();
            DefaultArrowTexture.LoadContent();
            DefaultBorderTexture.LoadContent();
            Cup.LoadContent();
        }

    }
}
