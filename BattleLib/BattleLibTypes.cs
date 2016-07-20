using Base;
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
            var player = new ClientIdentifier() { IsPlayer = true, Name = "Player" };
            var ai = new ClientIdentifier() { IsPlayer = false, Name = "AI" };
            var wrapper = new PokemonWrapper(player);

            registry.RegisterType<MoveWidget>(
                r => new MoveWidget(
                        new TableWidget<Move>(null, null,
                            r.ResolveType<AttackModel>(),
                            new DefaultTableRenderer<Move>(r) { DefaultString = "------" },
                            new AttackTableSelectionModel(r.ResolveType<AttackModel>())),
                    r.ResolveType<Dialog>()));

            registry.RegisterAsService<BattleStateComponent, IBattleStateService>(reg => {
                return new BattleStateComponent(player, ai, reg.ResolveType<WaitForActionState>(),
                    reg.ResolveType<WaitForCharState>(), reg.ResolveType<ExecuteState>(), reg.ResolveType<IEventCreator>());
            });

            registry.RegisterType<AttackModel>(reg => new AttackModel(wrapper));
            registry.RegisterType<AttackTableSelectionModel>();
            registry.RegisterType<ExecuteState>();
            registry.RegisterType<WaitForCharState>();
            registry.RegisterType<WaitForActionState>();
            registry.RegisterAsService<EventCreator, IEventCreator>();
            registry.RegisterAsService<BattleGUI, IGUIService>();
            registry.RegisterType<CommandExecuter>();

            registry.RegisterParameter(ResourceKeys.PlayerId, player);
            registry.RegisterParameter(ResourceKeys.AIId, ai);
            registry.RegisterParameter(ResourceKeys.PlayerPokemonWrapper, wrapper);
        }
    }
}
