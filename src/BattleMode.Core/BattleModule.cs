using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BattleMode.Core.Components;
using BattleMode.Entities.AI;
using BattleMode.Entities.BattleState;
using BattleMode.Graphic;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Entities;
using GameEngine.TypeRegistry;
using Module = GameEngine.Core.ModuleManager.Module;

namespace BattleMode.Core
{
    public class BattleModule : Module
    {
        private IBattleGraphicController _graphic;
        private IBattleStateService _battleState;
        private AiEntity _aiEntity;
        private Client _player;

        public override void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssemblies(new List<Assembly>
            {
                typeof(BattleModule).Assembly,
                typeof(IBattleStateService).Assembly,
                typeof(IBattleGraphicController).Assembly,
                typeof(IGuiController).Assembly,
                typeof(ClientIdentifier).Assembly
            });

            registry.RegisterType(a => _player);
        }

        public override void Start(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            var data = registry.ResolveType<BattleData>();
            var playerId = data.PlayerId;
            var aiId = data.Clients.First(id => !id.IsPlayer);
            _player = new Client(playerId);
            var ai = new Client(aiId);

            _battleState = registry.ResolveType<IBattleStateService>();
            _graphic = registry.ResolveType<IBattleGraphicController>();
            _aiEntity = new AiEntity(_battleState, ai, playerId);
            // Needs to be created since no other class depend on it.
            registry.ResolveType<BattleEventProcessor>();


            componentManager.AddGameComponent(_aiEntity);
            componentManager.AddGameComponent(_battleState);
            componentManager.AddGameComponent(registry.ResolveType<IGuiController>());
            componentManager.Scene = _graphic.Scene;

            _battleState.SetCharacter(_player.Id, _player.Pokemons.First());

            registry.ResolveType<GuiManager>().Show();
        }

        public override void Stop(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager)
        {
            componentManager.RemoveGameComponent(_aiEntity);
            componentManager.RemoveGameComponent(_battleState);
            componentManager.Scene = null;

            _battleState = null;
            _graphic = null;
            _aiEntity = null;
        }

        public BattleModule() : base(new object(), nameof(BattleModule), "BattleMode")
        {
        }
    }
}
