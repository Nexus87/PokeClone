using System;
using System.Collections.Generic;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public class ClassicSkin : ISkin
    {
        private readonly Dictionary<Type, IRenderer> _renderers = new Dictionary<Type, IRenderer>();
        private readonly Dictionary<Type, Action<TextureProvider>> _initFunctions = new Dictionary<Type, Action<TextureProvider>>();

        public const string Arrow = "arrow";
        public const string Border = "border";
        public const string Circle = "circle";
        public const string DefaultFont = "DefaultFont";

        public ClassicSkin()
        {
            SetRendererAs<ClassicButtonRenderer, ButtonRenderer, Button>(new ClassicButtonRenderer());
            SetRendererAs<ClassicWindowRenderer, WindowRenderer, Window>(new ClassicWindowRenderer());
            SetRendererAs<ClassicLabelRenderer, LabelRenderer, Label>(new ClassicLabelRenderer());
            SetRendererAs<ClassicTextAreaRenderer, TextAreaRenderer, TextArea>(new ClassicTextAreaRenderer());
            SetRendererAs<ClassicImageBoxRenderer, ImageBoxRenderer, ImageBox>(new ClassicImageBoxRenderer());
            SetRendererAs<ClassicPanelRenderer, PanelRenderer, Panel>(new ClassicPanelRenderer());
            SetRendererAs<ClassicSelectablePanelRenderer, SelectablePanelRenderer, SelectablePanel>(new ClassicSelectablePanelRenderer());
            SetRendererAs<ClassicScrollAreaRenderer, ScrollAreaRenderer, ScrollArea>(new ClassicScrollAreaRenderer());

        }
        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            builder.ReadConfigFile(@"GameEngine/Textures/TextureConfig.json");
        }

        public static Color BackgroundColor { get; } = new Color(248, 248, 248, 0);

        public IRenderer GetRendererForComponent(Type componentType)
        {
            if (!_renderers.TryGetValue(componentType, out var renderer))
            {
                return null;
            }

            return renderer;
        }

        public void SetRendererAs<T, TRenderer, TComponent>(T renderer) where T : TRenderer where TRenderer : AbstractRenderer<TComponent> where TComponent : IGuiComponent
        {
            _renderers[typeof(TComponent)] = renderer;
            _initFunctions[typeof(T)] = t => renderer?.Init(t);

        }

        public void Init(TextureProvider textureProvider)
        {
            foreach (var initFunctionsValue in _initFunctions.Values)
            {
                initFunctionsValue(textureProvider);
            }
        }
    }
}