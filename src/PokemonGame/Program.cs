using System;
using BattleMode.Core;
//using BattleMode.Core;
using GameEngine.Core;
using GameEngine.GUI;
using PokemonShared.Core;

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
            var skin = new ClassicSkin();
            //RegisterRenderers(engine.GuiSystem);

            //engine.RegisterModule(new PokemonGameModule());
            //engine.RegisterModule(new BattleModule());
            //engine.RegisterModule(new MainModule());
            var pokemonSharedModule = new PokemonSharedModule(@"Content/Game.json");
            //engine.RegisterModule(pokemonSharedModule);
            engine.RegisterContentModule(pokemonSharedModule);
            //engine.SetSkin(engine.GuiSystem.ClassicSkin);
            //engine.SetStartModule(startModule);
            engine.SetSkin(skin);
            engine.SetState(new BattleModule());
            engine.Run();
        }
    }
}
