using GameEngine.Graphics.Textures;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        T GetRenderer<T>() where  T : class;

        void SetRendererAs<T, TRenderer, TComponent>(T renderer) 
            where TRenderer : AbstractRenderer<TComponent> 
            where T : TRenderer
            where TComponent : IGuiComponent;

        void Init(TextureProvider textureProvider);
    }
}