using Base.Rules;
using BattleLib;
using Game.Rules;
using GameEngine;
using GameEngine.Graphics;
using PokemonGame.Rules;
using System;

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
        static void Main()
        {
            var config = new Configuration();
            var rules = new RulesSet(new DummyBattleRules(), new DummyPokemonRules(), new DummyTable());

            var engine = new PokeEngine(config);
            var factory = new GraphicComponentFactory(config, engine);

            engine.ShowGUI();
            engine.AddGameComponent(new InitComponent(config, engine, engine.EventQueue, factory, rules, new DummyScheduler()));

            engine.Run();
        }
    }
}
