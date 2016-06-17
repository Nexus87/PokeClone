using Base.Rules;
using BattleLib;
using Game.Rules;
using GameEngine;
using GameEngine.Graphics;
using PokemonGame.Rules;
using System;
using System.Reflection;

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
            var calculator = new DefaultMoveEffectCalculator(new DummyBattleRules());

            var engine = new PokeEngine(config);
            var factory = new GraphicComponentFactory(config, engine);
            factory.registry.ScanAssembly(Assembly.GetExecutingAssembly());

            engine.ShowGUI();
            engine.AddGameComponent(new InitComponent(config, engine, engine.EventQueue, factory, calculator, new DummyScheduler()));

            engine.Run();
        }
    }
}
