using GameEngine.GUI;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngine.Pokemon.Gui.Renderer.PokemonClassicRenderer;

namespace GameEngine.Pokemon.Gui.Renderer
{
    public static class InitRenderers
    {
        public static void Init()
        {
            ClassicSkin.AddAdditionalRenderer<ClassicLineRenderer, HpLineRenderer>(
                t => new ClassicLineRenderer(t.GetTexture(ClassicSkin.Key, ClassicSkin.Circle), t.Pixel, ClassicSkin.BackgroundColor)
            );
            ClassicSkin.AddAdditionalRenderer<ClassicHpTextRenderer, HpTextRenderer>(
                t => new ClassicHpTextRenderer(t.GetFont(ClassicSkin.Key, ClassicSkin.DefaultFont))
            );
        }
    }
}