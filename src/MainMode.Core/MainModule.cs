using System;
using System.Reflection;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Entities;
using GameEngine.TypeRegistry;
using MainMode.Core.Loader;
using MainMode.Entities;
using MainMode.Globals;
using MainMode.Graphic;

namespace MainMode.Core
{
    public class MainModule : IModule
    {

        private MainModeController _mainModeController;
        public string ModuleName => "MainMode";

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(typeof(GameStateEntity).Assembly);
            registry.ScanAssembly(typeof(GraphicController).Assembly);
            registry.ScanAssembly(typeof(SpriteId).Assembly);
        }

        public void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            registry.ResolveType<CharacterSpriteLoader>().LoadEntityMapping(@"MainMode\EntitySpriteMap.json");

            manager.AddGameComponent(registry.ResolveType<GameStateEntity>());
            manager.AddGameComponent(registry.ResolveType<GraphicController>());

            _mainModeController = registry.ResolveType<MainModeController>();
            _mainModeController.SetMap("Test");
            inputHandlerManager.AddHandler(_mainModeController.InputHandler);

            Connector.Connect(registry.ResolveType<GameStateEntity>(), registry.ResolveType<GraphicController>());

        }

        public void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager)
        {
            throw new NotImplementedException();
        }
    }
}