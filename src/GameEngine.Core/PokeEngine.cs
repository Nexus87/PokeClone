
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics;
using GameEngine.GUI;
using GameEngine.GUI.Configuration;

namespace GameEngine.Core
{
    public class PokeEngine
    {
        private readonly GameRunner _gameRunner;
        private readonly IModuleManager _moduleRegistry;
        private readonly Graphics.TextureProvider _textureProvider;
        private string _startModule;
        private ISkin _skin;

        public PokeEngine(Configuration config)
        {
            _gameRunner = new GameRunner();
            _moduleRegistry = new AutofacModuleManager();
            _textureProvider = new Graphics.TextureProvider();
            _moduleRegistry.RegisterModule(new GameEngineModule(_gameRunner, _textureProvider, config));
        }

        public void RegisterModule(IModule module)
        {
            _moduleRegistry.RegisterModule(module);
        }

        public void SetStartModule(string name)
        {
            _startModule = name;
        }

        public void Run()
        {
            Init();
            var typeRegistry = _moduleRegistry.TypeRegistry;
            _moduleRegistry.StartModule(GameEngineModule.Name);
            _moduleRegistry.StartModule(_startModule);
            _gameRunner.Run();
        }

        private void Init()
        {
            BuildTextureConfiguration();
            RegisterModuleTypes();

        }

        private void RegisterModuleTypes()
        {
            _moduleRegistry.RegisterTypes();
            _skin.RegisterRenderers(_moduleRegistry.TypeRegistry, _textureProvider);
        }

        private void BuildTextureConfiguration()
        {
            var textureConfigBuilder = new TextureConfigurationBuilder();
            _moduleRegistry.AddTextureConfigurations(textureConfigBuilder);
            _skin.AddTextureConfigurations(textureConfigBuilder);
            _textureProvider.SetConfiguration(textureConfigBuilder.BuildConfiguration(), _gameRunner.Content);
        }

        public void SetSkin(ISkin skin)
        {
            _skin = skin;
        }
    }
}