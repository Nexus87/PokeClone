using Base.Rules;
using BattleLib;
using BattleLib.Components.BattleState;
using Game.Rules;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
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
            RegisterTypes(engine.registry);
            engine.AddGameComponent(new InitComponent(engine, engine.registry));

            engine.Run();
        }

        private static void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterAsService<DummyBattleRules, IBattleRules>();
            registry.RegisterAsService<DummyScheduler, ICommandScheduler>();
            registry.RegisterAsService<DummyTable, ITypeTable>();
            registry.RegisterAsService<DummyPokemonRules, IPokemonRules>();
            registry.RegisterAsService<DefaultMoveEffectCalculator, IMoveEffectCalculator>();
        }
    }
}
