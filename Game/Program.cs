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
            //    var playerId = new ClientIdentifier();
            //    var aiId = new ClientIdentifier();
            //    playerId.IsPlayer = true;
            //    playerId.Name = "Player";

            //    aiId.IsPlayer = false;
            //    aiId.Name = "AI";

            var engine = new PokeEngine(config);
            //    var graphic = new BattleGraphics(engine, playerId, aiId);
            //var battleState = new BattleStateComponent(playerId, aiId, engine);

            engine.ShowGUI();
            //engine.Graphic = graphic;
            engine.Components.Add(new InitComponent(config, engine));
            //var gui = new BattleGUI(config, engine, battleState, playerId);

            engine.Run();
        }
    }
#endif
}
