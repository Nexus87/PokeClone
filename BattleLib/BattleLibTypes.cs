using Base;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
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

        public static void RegisterTypes(IGameRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            registry.RegisterTypeAs<MoveWidget, IMoveWidget>(
                r => new MoveWidget(
                        new TableWidget<Move>(null, null,
                            r.ResolveType<AttackModel>(),
                            new DefaultTableRenderer<Move>(r) { DefaultString = "------" },
                            new AttackTableSelectionModel(r.ResolveType<AttackModel>())),
                    r.ResolveType<Dialog>()));

            registry.RegisterParameter(ResourceKeys.PlayerId, new ClientIdentifier() { IsPlayer = true, Name = "Player" });
            registry.RegisterParameter(ResourceKeys.AIId, new ClientIdentifier() { IsPlayer = false, Name = "AI" });
            registry.RegisterParameter(ResourceKeys.PlayerPokemonWrapper, new PokemonWrapper(registry.GetParameter<ClientIdentifier>(ResourceKeys.PlayerId)));
        }
    }
}
