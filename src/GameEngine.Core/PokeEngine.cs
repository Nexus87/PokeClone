using GameEngine.Core.GameStates;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    public class PokeEngine
    {
        private const string EngineContentRoot = "Content";
        private readonly GameRunner _gameRunner;
        public GuiConfig GuiConfig { get; } = new GuiConfig();
        private readonly TextureConfigurationBuilder _textureConfigurationBuilder ;

        public PokeEngine(string contentRoot)
        {
            _gameRunner = new GameRunner(new Configuration().KeyMap);
            _textureConfigurationBuilder = new TextureConfigurationBuilder(contentRoot);
        }


        public void Run()
        {
            var textureProvider = new TextureProvider();


            _gameRunner.Skin = GuiConfig.CurrentSkin;
            _gameRunner.OnContentLoad = (gameRunner) =>
            {
                InitSkin(gameRunner);
                gameRunner.GuiFactory = GuiConfig.Init(gameRunner.Screen.Constants);
                textureProvider.SetConfiguration(_textureConfigurationBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, _textureConfigurationBuilder.ContentRoot));
                textureProvider.Init(gameRunner.GraphicsDevice);
                
            };
            //DebugRectangle.Enable(_textureProvider.Pixel);
            _gameRunner.Run();
        }

        private void InitSkin(GameRunner gameRunner)
        {
            var skinTextureConfigBuilder = new TextureConfigurationBuilder(EngineContentRoot);
            GuiConfig.CurrentSkin.AddTextureConfigurations(skinTextureConfigBuilder);

            var skinTextureProvider = new TextureProvider();
            skinTextureProvider.SetConfiguration(skinTextureConfigBuilder.BuildConfiguration(), new ContentManager(_gameRunner.Services, skinTextureConfigBuilder.ContentRoot));

            skinTextureProvider.Init(gameRunner.GraphicsDevice);
            GuiConfig.CurrentSkin.Init(skinTextureProvider);

        }

        public void RegisterContentModule(IContentModule contentModule)
        {
            contentModule.AddTextureConfigurations(_textureConfigurationBuilder);
        }

        public void SetState(State state)
        {
            _gameRunner.InitialState = state;
        }
    }
}