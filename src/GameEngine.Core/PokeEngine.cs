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

        private ISkin _skin;
        private readonly TextureConfigurationBuilder _textureConfigurationBuilder ;

        public PokeEngine(Configuration config, string contentRoot)
        {
            _gameRunner = new GameRunner();
            _textureConfigurationBuilder = new TextureConfigurationBuilder(contentRoot);
        }


        public void Run()
        {
            var textureProvider = new TextureProvider();



            _gameRunner.OnContentLoad = (gameRunner) =>
            {
                InitSkin(gameRunner);

                textureProvider.SetConfiguration(_textureConfigurationBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, _textureConfigurationBuilder.ContentRoot));
                textureProvider.Init(gameRunner.GraphicsDevice);
                
            };
            //DebugRectangle.Enable(_textureProvider.Pixel);
            _gameRunner.Run();
        }

        private void InitSkin(GameRunner gameRunner)
        {
            var skinTextureConfigBuilder = new TextureConfigurationBuilder(EngineContentRoot);
            _skin.AddTextureConfigurations(skinTextureConfigBuilder);

            var skinTextureProvider = new TextureProvider();
            skinTextureProvider.SetConfiguration(skinTextureConfigBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, skinTextureConfigBuilder.ContentRoot));

            skinTextureProvider.Init(gameRunner.GraphicsDevice);
            _skin.Init(skinTextureProvider);

        }
        public void SetSkin(ISkin skin)
        {
            _skin = skin;
        }

        public void RegisterContentModule(IContentModule contentModule)
        {
            contentModule.AddTextureConfigurations(_textureConfigurationBuilder);
        }
    }
}