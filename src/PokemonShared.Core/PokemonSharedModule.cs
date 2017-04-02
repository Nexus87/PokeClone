using System.IO;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Entities;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using GameEngine.Tools;
using GameEngine.TypeRegistry;
using Newtonsoft.Json;
using PokemonShared.Core.GameConfiguration;
using PokemonShared.Data;
using PokemonShared.Gui;
using PokemonShared.Gui.Builder;
using PokemonShared.Gui.Renderer;
using PokemonShared.Gui.Renderer.PokemonClassicRenderer;
using PokemonShared.Service;

namespace PokemonShared.Core
{
    public class PokemonSharedModule : ContentModule, IModule
    {
        private readonly Game _gameConfig;
        public static readonly object TextureKey = new object();

        public PokemonSharedModule(string gameConfig) : 
            base(Path.GetDirectoryName(gameConfig))
        {
            _gameConfig = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameConfig));
        }

        public string ModuleName => "Shared";

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterAsService<JSonStorage<MoveData>, IStorage<MoveData>>(x => new JSonStorage<MoveData>(_gameConfig.Moves));
            registry.RegisterAsService<JSonStorage<PokemonData>, IStorage<PokemonData>>(x => new JSonStorage<PokemonData>(_gameConfig.Pokemons));
            registry.RegisterAsService<JSonStorage<MoveSetItem>, IStorage<MoveSetItem>>(x => new JSonStorage<MoveSetItem>(_gameConfig.MoveSet));
            registry.RegisterAsService<SpriteProvider, SpriteProvider>(x => new SpriteProvider(x.ResolveType<TextureProvider>(), TextureKey));

            registry.ScanAssembly(typeof(HpLine).Assembly);
        }

        public void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            throw new System.NotImplementedException();
        }

        public void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager)
        {
            throw new System.NotImplementedException();
        }

        public override void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            ReadConfiguration(builder, TextureKey, _gameConfig.Textures);
        }
    }
}