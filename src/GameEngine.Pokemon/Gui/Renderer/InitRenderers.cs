using GameEngine.GUI;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;

namespace GameEngine.Pokemon.Gui.Renderer
{
    public static class InitRenderers
    {
        public static void Init()
        {
            ClassicSkin.AddAdditionalRenderer<ClassicLineRenderer, HpLineRenderer>(
                t => new ClassicLineRenderer(t.GetTexture(ClassicSkin.Key, ClassicSkin.Circle), t.Pixel, ClassicSkin.BackgroundColor)
            );
        }
    }
}