using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;

namespace GameEngine.Core
{
    public class PokeEngine
    {
        private readonly GameRunner _gameRunner;
        private readonly IModuleManager _moduleRegistry;
        private readonly TextureProvider _textureProvider;
        private string _startModule;
        private ISkin _skin;

        public PokeEngine(Configuration config, string contentRoot)
        {
            _gameRunner = new GameRunner();
            _moduleRegistry = new AutofacModuleManager();
            _textureProvider = new TextureProvider();
            var gameEngine = new GameEngineModule(_gameRunner, _textureProvider, config, contentRoot);
            _moduleRegistry.RegisterModule(gameEngine);
            _moduleRegistry.RegisterContentModule(gameEngine);
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
            _moduleRegistry.StartModule(GameEngineModule.Name);
            _gameRunner.OnInit = () =>
            {
                _moduleRegistry.StartModule(_startModule);
            };
            //DebugRectangle.Enable(_textureProvider.Pixel);
            _gameRunner.Run();
        }

        private void Init()
        {
            BuildTextureConfiguration();
            RegisterModuleTypes();
            AddBuilderAndRenderer();

        }

        private void AddBuilderAndRenderer()
        {
        }

        private void RegisterModuleTypes()
        {
            _moduleRegistry.RegisterTypes();
            _moduleRegistry.AddBuilderAndRenderer();
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

        public void RegisterContentModule(IContentModule module)
        {
            _moduleRegistry.RegisterContentModule(module);
        }
    }
}