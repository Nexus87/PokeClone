﻿using Base.Rules;
using BattleLib.Components.BattleState;
using Game.Rules;
using GameEngine;
using GameEngine.Registry;
using PokemonGame.Rules;
using System;

namespace PokemonGame
{
    public class PokemonGameModule : IModule
    {
        public string ModuleName
        {
            get { return "PokemonGameModule"; }
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterAsService<DummyBattleRules, IBattleRules>();
            registry.RegisterAsService<DummyScheduler, ICommandScheduler>();
            registry.RegisterAsService<DummyTable, ITypeTable>();
            registry.RegisterAsService<DummyPokemonRules, IPokemonRules>();
            registry.RegisterAsService<DefaultMoveEffectCalculator, IMoveEffectCalculator>();
        }

        public void Start(IGameComponentManager componentManager, IGameTypeRegistry registry)
        {
            throw new NotImplementedException();
        }

        public void Stop(IGameComponentManager componentManager)
        {
            throw new NotImplementedException();
        }
    }
}
