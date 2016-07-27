using Base;
using BattleLib.Components;
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

            registry.RegisterAsService<BattleStateComponent, IBattleStateService>();
            registry.RegisterAsService<EventCreator, IEventCreator>();
            registry.RegisterAsService<BattleGUI, IGUIService>();
            registry.RegisterAsService<BattleData, BattleData>();
            registry.RegisterAsService<BattleGraphicController, IBattleGraphicController>();
            registry.RegisterAsService<BattleEventProcessor, BattleEventProcessor>();

            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
