using System.Reflection;
using GameEngine;
using GameEngine.Registry;
using Microsoft.Xna.Framework.Content;

namespace MainModule
{
    public class MainModule : IModule
    {
        public string ModuleName { get { return "MainModule"; } }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.RegisterTypeAs<FileSpriteSheetProvider, ISpriteSheetProvider>(r => new FileSpriteSheetProvider("Tileset", "Content/TilesetMap.txt", r.ResolveType<ContentManager>()));
        }

        public void Start(IGameComponentManager manager, IGameTypeRegistry registry)
        {
            manager.Graphic = registry.ResolveType<WorldScreen>();
        }

        public void Stop(IGameComponentManager engine)
        {
            throw new System.NotImplementedException();
        }
    }
}