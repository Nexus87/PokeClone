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
            var playerId = new ClientIdentifier();
            var aiId = new ClientIdentifier();
            playerId.IsPlayer = true;
            playerId.Name = "Player";

            aiId.IsPlayer = false;
            aiId.Name = "AI";

            PokeEngine.Init(config);
            var engine = PokeEngine.GetInstance();
            var graphic = new BattleGraphics(engine, playerId, aiId);
            var battleState = new BattleStateComponent(playerId, aiId, engine);
            
            engine.Graphic = graphic;
            var gui = new BattleGUI(config, engine);
            PokeEngine.ShowGUI();
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
