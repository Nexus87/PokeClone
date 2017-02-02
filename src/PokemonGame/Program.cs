using System;
using BattleMode.Core;
using GameEngine.Core;
using GameEngine.GUI;
using MainMode.Core;

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

            engine.RegisterModule(new PokemonGameModule());
            engine.RegisterModule(new BattleModule());
            engine.RegisterModule(new MainModule());
            engine.SetSkin(new ClassicSkin());
            engine.SetStartModule(startModule);
            engine.Run();
        }
    }
}
