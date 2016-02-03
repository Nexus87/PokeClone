using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using BattleLib.GraphicComponents.GUI;
using GameEngine;
using System;

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
            var config = new Configuration();
            Engine.Init(config);
            var engine = Engine.GetInstance();
            var graphic = new BattleGraphics(engine);
            var battleState = new BattleStateComponent(new ClientIdentifier(), new ClientIdentifier(), engine);
            
            engine.Graphic = graphic;
            engine.GUI = new BattleGUI(config, engine);
            Engine.ShowGUI();
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
