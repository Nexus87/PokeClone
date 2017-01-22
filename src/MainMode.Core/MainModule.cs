using System.Collections.Generic;
using System.Reflection;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Entities;
using GameEngine.Graphics.Textures;
using GameEngine.Tools.Storages;
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
        public string ModuleName => "MainModule";

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.ScanAssembly(typeof(GameStateEntity).Assembly);
            registry.ScanAssembly(typeof(GraphicController).Assembly);
            registry.ScanAssembly(typeof(TextureKey).Assembly);
        }

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            var tileMapping = JsonSpriteSheetConfigStorage.Load(@"Content\MainMode\TilesetMapping.json");
            var characterMapping = JsonSpriteSheetConfigStorage.Load(@"Content\MainMode\CharactersMapping.json");

            var spriteSheetItems = new List<SpriteSheetItem>{
                new SpriteSheetItem(@"MainMode\TileSet", tileMapping, true),
                new SpriteSheetItem(@"MainMode\Characters Overworld", characterMapping, true)
            };

            builder.AddSpriteSheet(TextureKey.Key, spriteSheetItems);
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
            throw new System.NotImplementedException();
        }

        public void AddBuilderAndRenderer()
        {
        }
    }
}