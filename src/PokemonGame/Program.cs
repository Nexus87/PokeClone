using System;
using BattleMode.Core;
//using BattleMode.Core;
using GameEngine.Core;
using PokemonShared.Core;
using PokemonShared.Gui;
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
        private static void Main()
        {
            //var startModule = "MainMode";

            var engine = new PokeEngine("Content");
            //RegisterRenderers(engine.GuiSystem);

            //engine.RegisterModule(new PokemonGameModule());
            //engine.RegisterModule(new BattleModule());
            //engine.RegisterModule(new MainModule());
            var pokemonSharedModule = new PokemonSharedModule(@"Content/Game.json");
            //engine.RegisterModule(pokemonSharedModule);
            engine.RegisterContentModule(pokemonSharedModule);
            //engine.SetSkin(engine.GuiSystem.ClassicSkin);
            //engine.SetStartModule(startModule);

            engine.GuiConfig.AddGuiElement("HpLine", new HpLineBuilder(), skin => new HpLine());
            engine.GuiConfig.AddGuiElement("HpText", new HpTextBuilder(), skin => new HpText());

            engine.GuiConfig.ClassicSkin.SetRendererAs<ClassicLineRenderer, HpLineRenderer, HpLine>(new ClassicLineRenderer());
            engine.GuiConfig.ClassicSkin.SetRendererAs<ClassicHpTextRenderer, HpTextRenderer, HpText>(new ClassicHpTextRenderer());

            engine.SetState(new BattleModule());
            engine.Run();
        }
    }
}
