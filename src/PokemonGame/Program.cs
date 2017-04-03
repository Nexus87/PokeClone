using System;
using BattleMode.Core;
using GameEngine.Core;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using MainMode.Core;
using PokemonShared.Core;
using PokemonShared.Gui.Builder;
using PokemonShared.Gui.Renderer;
using PokemonShared.Gui.Renderer.PokemonClassicRenderer;

namespace PokemonGame
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            var startModule = "BattleMode";
            //var startModule = "MainMode";
            if (args.Length != 0)
                startModule = args[0];

            var config = new Configuration();
            var engine = new PokeEngine(config, "Content");
            RegisterRenderers(engine.GuiSystem);

            engine.RegisterModule(new PokemonGameModule());
            engine.RegisterModule(new BattleModule());
            engine.RegisterModule(new MainModule());
            var pokemonSharedModule = new PokemonSharedModule(@"Content/Game.json");
            engine.RegisterModule(pokemonSharedModule);
            engine.RegisterContentModule(pokemonSharedModule);
            engine.SetSkin(engine.GuiSystem.ClassicSkin);
            engine.SetStartModule(startModule);
            engine.Run();
        }

        private static void RegisterRenderers(GuiSystem guiSystem)
        {
             guiSystem.ClassicSkin.AddAdditionalRenderer<ClassicLineRenderer, HpLineRenderer>(
                t => new ClassicLineRenderer(t.GetTexture(ClassicSkin.Circle), t.Pixel, ClassicSkin.BackgroundColor)
);
            guiSystem.ClassicSkin.AddAdditionalRenderer<ClassicHpTextRenderer, HpTextRenderer>(
                t => new ClassicHpTextRenderer(t.GetFont(ClassicSkin.DefaultFont))
            );

            guiSystem.AddGuiElement("HpLine", (r, c) => new HpLineBuilder(r, c));
            guiSystem.AddGuiElement("HpText", (r, c) => new HpTextBuilder(r, c));
        }
    }
}
