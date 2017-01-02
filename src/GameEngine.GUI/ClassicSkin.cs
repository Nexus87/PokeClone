using System.Collections.Generic;
using GameEngine.Graphics;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public class ClassicSkin : ISkin
    {
        private const string Arrow = "arrow";
        private const string Border = "border";
        private const string Circle = "circle";
        private const string DefaultFont = "DefaultFont";


        public void RegisterRenderers(IGameTypeRegistry registry, TextureProvider provider)
        {
            var arrow = provider.GetTexture(this, Arrow);
            var border = provider.GetTexture(this, Border);
            var circle = provider.GetTexture(this, Circle);
            var font = provider.GetFont(this, DefaultFont);
            var pixel = provider.Pixel;

            registry.RegisterAsService<ClassicButtonRenderer, ButtonRenderer>(r => new ClassicButtonRenderer(arrow, font));
            registry.RegisterAsService<ClassicWindowRenderer, WindowRenderer>(r => new ClassicWindowRenderer(border));
            registry.RegisterAsService<ClassicLabelRenderer, LabelRenderer>(r => new ClassicLabelRenderer(font));
            registry.RegisterAsService<ClassicTextAreaRenderer, TextAreaRenderer>(r => new ClassicTextAreaRenderer(font));
            registry.RegisterAsService<ClassicLineRenderer, HpLineRenderer>(r => new ClassicLineRenderer(circle, pixel, BackgroundColor));
            registry.RegisterAsService<ClassicImageBoxRenderer, ImageBoxRenderer>();
            registry.RegisterAsService<ClassicPanelRenderer, PanelRenderer>(r => new ClassicPanelRenderer(pixel));
            registry.RegisterAsService<ClassicSelectablePanelRenderer, SelectablePanelRenderer>(r => new ClassicSelectablePanelRenderer(arrow));
            registry.RegisterAsService<ClassicScrollAreaRenderer, ScrollAreaRenderer>();

        }

        public Color BackgroundColor { get; } = new Color(248, 248, 248, 0);

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            var textureItems = new List<TextureItem>
            {
                new TextureItem(@"GuiSkins\ClassicSkin\arrow", Arrow, true),
                new TextureItem(@"GuiSkins\ClassicSkin\border", Border, true),
                new TextureItem(@"GuiSkins\ClassicSkin\circle", Circle, true),
            };

            var fontItems = new List<FontItem>
            {
                new FontItem(@"GuiSkins\ClassicSkin\MenuFont", DefaultFont, true)
            };

            builder.AddTextureConfig(this, textureItems);
            builder.AddFont(this, fontItems);
        }
    }
}