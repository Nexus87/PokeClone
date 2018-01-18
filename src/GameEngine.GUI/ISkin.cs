using System;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        TRenderer GetRendererForComponent<TComponent, TRenderer>()
            where TRenderer : AbstractRenderer<TComponent>
            where TComponent : IGuiComponent;

        IRenderer GetRendererForComponent(Type componentType);

        void SetRendererAs<T, TRenderer, TComponent>(T renderer) 
            where TRenderer : AbstractRenderer<TComponent> 
            where T : TRenderer
            where TComponent : IGuiComponent;

        void Init(TextureProvider textureProvider);
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}