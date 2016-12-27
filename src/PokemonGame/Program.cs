using GameEngine;
using System;
using BattleMode.Core;
using GameEngine.Core;
using GameEngine.GUI.Configuration;
using GameEngine.GUI.Utils;
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

            engine.Registry.RegisterModule(new PokemonGameModule());
            engine.Registry.RegisterModule(new BattleModule(engine));
            engine.Registry.RegisterModule(new MainModule(CreateDummyMap()));
            engine.SetStartModule(startModule);
            engine.Run();
        }

        private static Map CreateDummyMap()
        {
            var table = new Table<Tile>
            {
                [0, 0] = new Tile("Tile0303", false),
                [0, 1] = new Tile("Tile0304", true),
                [0, 2] = new Tile("Tile0305", false),
                [1, 0] = new Tile("Tile0403", false),
                [1, 1] = new Tile("Tile0404", true),
                [1, 2] = new Tile("Tile0405", false),
                [2, 0] = new Tile("Tile0503", false),
                [2, 1] = new Tile("Tile0504", false),
                [2, 2] = new Tile("Tile0505", false)
            };

            return new Map(table, new TilePosition(1, 0));
        }
    }
}
