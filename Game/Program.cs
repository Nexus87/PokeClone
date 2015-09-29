using System;
using GameEngine;
using BattleLib.GraphicComponent;
namespace PokemonGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var engine = new Engine();
            var graphic = new BattleGraphics(engine);
            engine.setGraphicCompomnent(graphic);
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
