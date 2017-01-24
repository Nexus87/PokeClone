using System;
using System.Collections.Generic;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public class ClassicSkin : ISkin
    {
        private static readonly List<Action<IGameTypeRegistry, TextureProvider>> AdditionalRenderers = new List<Action<IGameTypeRegistry, TextureProvider>>();

        public static void AddAdditionalRenderer<T, TService>(Func<TextureProvider, T> factory)
        {
            AdditionalRenderers.Add((r, t) => { r.RegisterAsService<T, TService>(reg => factory(t)); });
        }

        public const string Arrow = "arrow";
        public const string Border = "border";
        public const string Circle = "circle";
        public const string DefaultFont = "DefaultFont";
        public static readonly object Key = new object();

        public void RegisterRenderers(IGameTypeRegistry registry, TextureProvider provider)
        {
            var arrow = provider.GetTexture(Key, Arrow);
            var border = provider.GetTexture(Key, Border);
            var font = provider.GetFont(Key, DefaultFont);
            var pixel = provider.Pixel;

            registry.RegisterAsService<ClassicButtonRenderer, ButtonRenderer>(r => new ClassicButtonRenderer(arrow, font));
            registry.RegisterAsService<ClassicWindowRenderer, WindowRenderer>(r => new ClassicWindowRenderer(border));
            registry.RegisterAsService<ClassicLabelRenderer, LabelRenderer>(r => new ClassicLabelRenderer(font));
            registry.RegisterAsService<ClassicTextAreaRenderer, TextAreaRenderer>(r => new ClassicTextAreaRenderer(font));
            registry.RegisterAsService<ClassicImageBoxRenderer, ImageBoxRenderer>();
            registry.RegisterAsService<ClassicPanelRenderer, PanelRenderer>(r => new ClassicPanelRenderer(pixel));
            registry.RegisterAsService<ClassicSelectablePanelRenderer, SelectablePanelRenderer>(r => new ClassicSelectablePanelRenderer(arrow));
            registry.RegisterAsService<ClassicScrollAreaRenderer, ScrollAreaRenderer>();

            foreach (var rendererFactory in AdditionalRenderers)
            {
                rendererFactory(registry, provider);
            }

        }

        public static Color BackgroundColor { get; } = new Color(248, 248, 248, 0);

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
        }
    }
}