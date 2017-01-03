using System.Linq;
using System.Reflection;
using BattleMode.Components.AI;
using BattleMode.Components.BattleState;
using BattleMode.Core.Components;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Components;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace BattleMode.Core
{
    public class BattleModule : IModule
    {
        private IBattleGraphicController _graphic;
        private IBattleStateService _battleState;
        private AiComponent _aiComponent;
        private Client _player;

        public string ModuleName => "BattleModule";

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(Assembly.GetAssembly(typeof(ClientIdentifier)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IBattleStateService)));
            registry.ScanAssembly(Assembly.GetAssembly(typeof(IGUIService)));
            registry.RegisterType(a => _player);
        }

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
        }

        public void Start(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            var data = registry.ResolveType<BattleData>();
            var playerId = data.PlayerId;
            var aiId = data.Clients.First(id => !id.IsPlayer);
            _player = new Client(playerId);
            var ai = new Client(aiId);

            _battleState = registry.ResolveType<IBattleStateService>();
            _graphic = registry.ResolveType<IBattleGraphicController>();
            _aiComponent = new AiComponent(_battleState, ai, playerId);
            // Needs to be created since no other class depend on it.
            registry.ResolveType<BattleEventProcessor>();


            componentManager.AddGameComponent(_aiComponent);
            componentManager.AddGameComponent(_battleState);
            componentManager.Graphic = _graphic;

            _battleState.SetCharacter(_player.Id, _player.Pokemons.First());

            registry.ResolveType<GuiManager>().Show();
        }

        public void Stop(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager)
        {
            componentManager.RemoveGameComponent(_aiComponent);
            componentManager.RemoveGameComponent(_battleState);
            componentManager.Graphic = null;

            _battleState = null;
            _graphic = null;
            _aiComponent = null;
        }
    }
}
