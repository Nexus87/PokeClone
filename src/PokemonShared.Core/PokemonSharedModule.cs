using System.IO;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using Newtonsoft.Json;
using PokemonShared.Core.GameConfiguration;

namespace PokemonShared.Core
{
    public class PokemonSharedModule : IContentModule //, IModule
    {
        private readonly Game _gameConfig;

        public PokemonSharedModule(string gameConfig)
        {
            _gameConfig = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameConfig));
        }


        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            builder.ReadConfigFile(_gameConfig.Textures);
        }
    }
}