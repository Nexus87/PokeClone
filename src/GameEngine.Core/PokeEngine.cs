using Autofac;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    public class PokeEngine
    {
        private const string EngineContentRoot = "Content";
        private readonly ContainerBuilder _builder = new ContainerBuilder();
        private readonly TextureConfigurationBuilder _textureConfigurationBuilder;
        private GameRunner _gameRunner;
        private readonly TextureProvider _textureProvider = new TextureProvider();
        private readonly TextureProvider _skinTextureProvider = new TextureProvider();
        private string ContentRoot { get; }

        private ISkin _skin;

        public PokeEngine(Configuration config, string contentRoot)
        {
            RegisterModule(new GameEngineModule(_gameRunner, _textureProvider));
            _gameRunner.TextureProviders.Add(_textureProvider);
            _gameRunner.TextureProviders.Add(_skinTextureProvider);
            ContentRoot = contentRoot;
            _textureConfigurationBuilder = new TextureConfigurationBuilder(ContentRoot);
        }

        public void RegisterModule(Module module)
        {
            _builder.RegisterModule(module);
        }


        public void Run()
        {
            Init();
            var container = _builder.Build();
            _gameRunner = container.Resolve<GameRunner>();
            _gameRunner.OnInit = () =>
            {
            };
            //DebugRectangle.Enable(_textureProvider.Pixel);
            _gameRunner.Run();
        }

        private void Init()
        {
            _skin.Init(_skinTextureProvider);

            BuildTextureConfiguration();
        }

        private void BuildTextureConfiguration()
        {
            _gameRunner.Content.RootDirectory = ContentRoot;
            var textureConfigBuilder = new TextureConfigurationBuilder(ContentRoot);
            var skinTextureConfigBuilder = new TextureConfigurationBuilder(EngineContentRoot);
            _textureProvider.SetConfiguration(textureConfigBuilder.BuildConfiguration(), _gameRunner.Content);
            _skinTextureProvider.SetConfiguration(skinTextureConfigBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, EngineContentRoot));
        }

        public void SetSkin(ISkin skin)
        {
            _skin = skin;
        }

        public void RegisterContentModule(IContentModule module)
        {
            module.AddTextureConfigurations(_textureConfigurationBuilder);
        }
    }
}