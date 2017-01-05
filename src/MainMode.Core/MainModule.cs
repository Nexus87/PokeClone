using System.Collections.Generic;
using System.Reflection;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Entities;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;
using MainMode.Core.Graphics;

namespace MainMode.Core
{
    public class MainModule : IModule
    {
        private readonly Map _map;

        public MainModule(Map map)
        {
            _map = map;
        }
        public string ModuleName => "MainModule";
        public static object Key { get; } = new object();

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            var tileMapping = JsonSpriteSheetConfigLoader.Load(@"MainMode\TilesetMapping.json");
            var characterMapping = JsonSpriteSheetConfigLoader.Load(@"MainMode\CharactersMapping.json");

            var spriteSheetItems = new List<SpriteSheetItem>{
                new SpriteSheetItem(@"MainMode\TileSet", tileMapping, true),
                new SpriteSheetItem(@"MainMode\Characters Overworld", characterMapping, true)
            };
            builder.AddSpriteSheet(Key, spriteSheetItems);
        }

        public void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            manager.Graphic = registry.ResolveType<IWorldScreenController>();
            inputHandlerManager.AddHandler(registry.ResolveType<GameInputHandler>());
            var gameStateComponent = registry.ResolveType<IGameStateComponent>();
            gameStateComponent.SetMap(_map);
            gameStateComponent.PlaceSprite(0, new FieldCoordinate(1, 0));
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