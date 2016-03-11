using Base;
using Base.Rules;
using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using BattleLib.GraphicComponents.GUI;
using Game.Rules;
using GameEngine;
using PokemonGame.Rules;
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
            var rules = new RulesSet(new DummyBattleRules(), new DummyPokemonRules(), new DummyTable());

            var engine = new PokeEngine(config);
            //    var graphic = new BattleGraphics(engine, playerId, aiId);
            //var battleState = new BattleStateComponent(playerId, aiId, engine);

            engine.ShowGUI();
            //engine.Graphic = graphic;
            engine.Components.Add(new InitComponent(config, engine, rules, new DummyScheduler()));
            //var gui = new BattleGUI(config, engine, battleState, playerId);

            engine.Run();
        }
    }
#endif
}
