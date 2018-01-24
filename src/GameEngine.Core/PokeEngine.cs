using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;

namespace GameEngine.Core
{
    public class PokeEngine
    {
        private const string EngineContentRoot = "Content";
        private GameRunner _gameRunner;
        public GuiConfig GuiConfig { get; } = new GuiConfig(EngineContentRoot);
        public GameConfiguration GameConfiguration {get;} = new GameConfiguration(EngineContentRoot);
        private readonly TextureConfigurationBuilder _textureConfigurationBuilder;

        public PokeEngine(string contentRoot)
        {
            _textureConfigurationBuilder = new TextureConfigurationBuilder(contentRoot);
        }

        public void Run()
        {
            _gameRunner = new GameRunner(GameConfiguration, GuiConfig);
            //DebugRectangle.Enable(_textureProvider.Pixel);
            _gameRunner.Run();
        }

        public void RegisterContentModule(IContentModule contentModule)
        {
            contentModule.AddTextureConfigurations(_textureConfigurationBuilder);
        }
    }
}