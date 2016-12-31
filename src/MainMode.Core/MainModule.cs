﻿using System.Reflection;
using GameEngine.Core;
using GameEngine.Core.Registry;
using GameEngine.TypeRegistry;
using MainMode.Core.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MainMode.Core
{
    public class MainModule : IModule
    {
        private readonly Map map;

        public MainModule(Map map)
        {
            this.map = map;
        }
        public string ModuleName { get { return "MainModule"; } }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.RegisterTypeAs<FileSpriteSheetProvider, ISpriteSheetProvider>(r => new FileSpriteSheetProvider("Tileset", "Content/TilesetMap.txt", r.ResolveType<ContentManager>()));
        }

        public void Start(IGameComponentManager manager, IGameTypeRegistry registry)
        {
            manager.Graphic = registry.ResolveType<IWorldScreenController>();
            manager.InputHandler = registry.ResolveType<GameInputHandler>();
            var gameStateComponent = registry.ResolveType<IGameStateComponent>();
            gameStateComponent.SetMap(map);
            gameStateComponent.PlaceSprite(0, new FieldCoordinate(1, 0));
        }

        public void Stop(IGameComponentManager engine)
        {
            throw new System.NotImplementedException();
        }
    }
}