using System;
using BattleMode.Core;
using GameEngine.Core;
using GameEngine.GUI;
using MainMode.Core;
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
        private static void Main(string[] args)
        {
            var startModule = "BattleMode";
            //var startModule = "MainMode";
            if (args.Length != 0)
                startModule = args[0];

            var config = new Configuration();
            var engine = new PokeEngine(config, "Content");
            engine.RegisterModule(new PokemonGameModule());
            engine.RegisterModule(new BattleModule());
            engine.RegisterModule(new MainModule());
            var pokemonSharedModule = new PokemonSharedModule(@"Content/Game.json");
            engine.RegisterModule(pokemonSharedModule);
            engine.RegisterContentModule(pokemonSharedModule);
            engine.SetSkin(new ClassicSkin());
            engine.SetStartModule(startModule);
            engine.Run();
        }
    }
}
