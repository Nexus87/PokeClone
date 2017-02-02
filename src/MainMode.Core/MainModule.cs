using System;
using System.Reflection;
using GameEngine.Core;
using GameEngine.Entities;
using GameEngine.TypeRegistry;
using MainMode.Core.Loader;
using MainMode.Entities;
using MainMode.Globals;
using MainMode.Graphic;
using Module = GameEngine.Core.ModuleManager.Module;

namespace MainMode.Core
{
    public class MainModule : Module
    {

        private MainModeController _mainModeController;

        public override void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(typeof(GameStateEntity).Assembly);
            registry.ScanAssembly(typeof(GraphicController).Assembly);
            registry.ScanAssembly(typeof(TextureKey).Assembly);
        }

        public override void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            registry.ResolveType<CharacterSpriteLoader>().LoadEntityMapping(@"MainMode\EntitySpriteMap.json");

            manager.AddGameComponent(registry.ResolveType<GameStateEntity>());
            manager.AddGameComponent(registry.ResolveType<GraphicController>());

            _mainModeController = registry.ResolveType<MainModeController>();
            _mainModeController.SetMap("Test");
            inputHandlerManager.AddHandler(_mainModeController.InputHandler);

            Connector.Connect(registry.ResolveType<GameStateEntity>(), registry.ResolveType<GraphicController>());

        }

        public override void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager)
        {
            throw new NotImplementedException();
        }

        public MainModule() :
            base(TextureKey.Key, "MainModule", "MainMode")
        {
        }
    }
}