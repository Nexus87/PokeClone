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
        private readonly GameRunner _gameRunner;
        private string ContentRoot { get; }

        private ISkin _skin;

        public PokeEngine(Configuration config, string contentRoot)
        {
            _gameRunner = new GameRunner();
            ContentRoot = contentRoot;
        }

        public void RegisterModule(Module module)
        {
            _builder.RegisterModule(module);
        }


        public void Run()
        {
            var textureProvider = new TextureProvider();
            var skinTextureProvider= new TextureProvider();

            var skinTextureConfigBuilder = new TextureConfigurationBuilder(EngineContentRoot);
            var textureConfigurationBuilder = new TextureConfigurationBuilder(ContentRoot);

            _skin.AddTextureConfigurations(skinTextureConfigBuilder);

            _gameRunner.OnContentLoad = (gameRunner) =>
            {
                textureProvider.SetConfiguration(textureConfigurationBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, ContentRoot));
                skinTextureProvider.SetConfiguration(skinTextureConfigBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, EngineContentRoot));

                textureProvider.Init(gameRunner.GraphicsDevice);
                skinTextureProvider.Init(gameRunner.GraphicsDevice);

                _skin.Init(skinTextureProvider);
            };
            //DebugRectangle.Enable(_textureProvider.Pixel);
            _gameRunner.Run();
        }


        public void SetSkin(ISkin skin)
        {
            _skin = skin;
        }

    }
}