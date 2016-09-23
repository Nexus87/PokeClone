using BattleLib;
using GameEngine;
using System;
using System.Linq;

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
            var startModule = "BattleModule";
            if (args.Length != 0)
                startModule = args[0];

            var config = new Configuration();
            var engine = new PokeEngine(config);

            engine.Registry.RegisterModule(new PokemonGameModule());
            engine.Registry.RegisterModule(new BattleModule(engine));
            engine.Registry.RegisterModule(new MainModule.MainModule());
            engine.SetStartModule(startModule);
            engine.Run();
        }
    }
}
