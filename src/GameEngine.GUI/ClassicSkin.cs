using GameEngine.GUI.General;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI
{
    public class ClassicSkin : ISkin
    {
        private ITexture2D ArrowTexture { get; set; }
        private XnaTexture2D Pixel { get; set; }
        private ITexture2D BorderTexture { get; set; }
        private ITexture2D Cup { get; set; }
        private ISpriteFont Font { get; set; }

        public void RegisterRenderers(IGameTypeRegistry registry)
        {
            registry.RegisterAsService<ClassicButtonRenderer, ButtonRenderer>(r => new ClassicButtonRenderer(ArrowTexture, Font));
            registry.RegisterAsService<ClassicWindowRenderer, WindowRenderer>(r => new ClassicWindowRenderer(BorderTexture));
            registry.RegisterAsService<ClassicLabelRenderer, LabelRenderer>(r => new ClassicLabelRenderer(Font));
            registry.RegisterAsService<ClassicTextAreaRenderer, TextAreaRenderer>(r => new ClassicTextAreaRenderer(Font));
            registry.RegisterAsService<ClassicLineRenderer, HpLineRenderer>(r => new ClassicLineRenderer(Cup, Pixel, BackgroundColor));
            registry.RegisterAsService<ClassicImageBoxRenderer, ImageBoxRenderer>();
            registry.RegisterAsService<ClassicPanelRenderer, PanelRenderer>(r => new ClassicPanelRenderer(Pixel));
            registry.RegisterAsService<ClassicSelectablePanelRenderer, SelectablePanelRenderer>(r => new ClassicSelectablePanelRenderer(ArrowTexture));
            registry.RegisterAsService<ClassicScrollAreaRenderer, ScrollAreaRenderer>();

        }

        public void LoadContent(ContentManager content, Game game, Configuration.Configuration config)
        {
            ArrowTexture = new XnaTexture2D(config.DefaultArrowTexture, content);
            Font = new XnaSpriteFont(config.DefaultFont, content);
            BorderTexture = new XnaTexture2D(config.DefaultBorderTexture, content);
            Pixel = new XnaTexture2D();
            Cup = new XnaTexture2D("circle", content);

            Pixel.Texture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color, 1);
            Pixel.SetData(new[] { Color.White });

            ArrowTexture.LoadContent();
            BorderTexture.LoadContent();
            Cup.LoadContent();
            Font.LoadContent();
        }

        public Color BackgroundColor { get; } = new Color(248, 248, 248, 0);
    }
}