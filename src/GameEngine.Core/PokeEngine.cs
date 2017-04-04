using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    public class PokeEngine
    {
        private const string EngineContentRoot = "Content";

        public GuiSystem GuiSystem { get; } = new GuiSystem();

        private readonly GameRunner _gameRunner = new GameRunner();
        private readonly IModuleManager _moduleRegistry = new AutofacModuleManager();
        private readonly TextureProvider _textureProvider = new TextureProvider();
        private readonly TextureProvider _skinTextureProvider = new TextureProvider();
        public string ContentRoot { get; set; }

        private string _startModule;
        private ISkin _skin;

        public PokeEngine(Configuration config, string contentRoot)
        {
            _moduleRegistry.RegisterModule(new GameEngineModule(_gameRunner, _textureProvider, config));
            _gameRunner.TextureProviders.Add(_textureProvider);
            _gameRunner.TextureProviders.Add(_skinTextureProvider);
            ContentRoot = contentRoot;
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
        }


        private void RegisterModuleTypes()
        {
            _moduleRegistry.RegisterTypes();
            _skin.RegisterRenderers(_moduleRegistry.TypeRegistry, _skinTextureProvider);
        }

        private void BuildTextureConfiguration()
        {
            _gameRunner.Content.RootDirectory = ContentRoot;
            var textureConfigBuilder = new TextureConfigurationBuilder(ContentRoot);
            var skinTextureConfigBuilder = new TextureConfigurationBuilder(EngineContentRoot);
            _moduleRegistry.AddTextureConfigurations(textureConfigBuilder);
            _skin.AddTextureConfigurations(skinTextureConfigBuilder);
            _textureProvider.SetConfiguration(textureConfigBuilder.BuildConfiguration(), _gameRunner.Content);
            _skinTextureProvider.SetConfiguration(skinTextureConfigBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, EngineContentRoot));
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