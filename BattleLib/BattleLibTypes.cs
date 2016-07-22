﻿using Base;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    internal static class BattleLibTypes
    {
        public enum ResourceKeys
        {
            PlayerId,
            AIId,
            PlayerPokemonWrapper
        }

        public static void RegisterTypes(IGameTypeRegistry registry)
        {

            registry.RegisterAsService<BattleStateComponent, IBattleStateService>(reg => {
                return new BattleStateComponent(reg.ResolveType<BattleData>(), reg.ResolveType<WaitForActionState>(),
                    reg.ResolveType<WaitForCharState>(), reg.ResolveType<ExecuteState>(), reg.ResolveType<IEventCreator>());
            });

            registry.RegisterType<MoveModel>();
            registry.RegisterType<MoveTableSelectionModel>();
            registry.RegisterType<AttackTableRenderer>();
            registry.RegisterType<ExecuteState>();
            registry.RegisterType<WaitForCharState>();
            registry.RegisterType<WaitForActionState>();
            registry.RegisterAsService<EventCreator, IEventCreator>();
            registry.RegisterAsService<BattleGUI, IGUIService>();
            registry.RegisterType<CommandExecuter>();
            registry.RegisterTypeAs<MoveMenuWidget, IMenuWidget<Move>>();
            registry.RegisterTypeAs<ItemMenuWidget, IMenuWidget<Item>>();
            registry.RegisterTypeAs<MainMenuWidget, IMenuWidget<MainMenuEntries>>();
            registry.RegisterTypeAs<PokemonMenuWidget, IMenuWidget<Pokemon>>();
            registry.RegisterAsService<BattleData, BattleData>();

        }
    }
}
