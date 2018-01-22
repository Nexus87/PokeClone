using GameEngine.Core.GameStates;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        private readonly Screen _screen;
        private StateManager _stateManager;
        private GuiFactory _guiFactory;
        private readonly GameConfiguration _gameConfiguration;
        private readonly GuiConfig _guiConfig;
        private TextureProvider _textureProvider;

        public GameRunner(
            GameConfiguration gameConfiguration,
            GuiConfig guiConfig
        )
        {
            _gameConfiguration = gameConfiguration;
            _guiConfig = guiConfig;

            var graphicsDeviceManager = new GraphicsDeviceManager(this);
            _screen = new Screen(new ScreenConstants(), graphicsDeviceManager);

            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            graphicsDeviceManager.ApplyChanges();
        }

        protected override void Draw(GameTime gameTime)
        {
            _screen.Draw();
        }

        protected override void Update(GameTime gameTime)
        {
            _stateManager.CurrentState.Update(gameTime);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.ClientSizeChanged += delegate
            {
                _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            };
            _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);

            _stateManager = new StateManager(_screen, _gameConfiguration.KeyMap, _guiConfig.CurrentSkin, _guiFactory, _textureProvider);
            _stateManager.PushState(_gameConfiguration.InitialState);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _guiFactory = _guiConfig.Init(_screen.Constants);

            _textureProvider = InitTextureProvider(_gameConfiguration.TextureConfigurationBuilder);
            _guiConfig.CurrentSkin.Init(InitTextureProvider(_guiConfig.SkinTextureConfigurationBuilder));
        }

        private TextureProvider InitTextureProvider(TextureConfigurationBuilder builder)
        {
            var provider = new TextureProvider();
            provider.SetConfiguration(builder.BuildConfiguration(), new ContentManager(Services, builder.ContentRoot));
            provider.Init(GraphicsDevice);

            return provider;
        }
    }
}