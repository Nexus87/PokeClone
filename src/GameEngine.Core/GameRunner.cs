using System;
using System.Collections.Generic;
using GameEngine.Core.GameStates;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        private readonly Screen Screen;
        public readonly GraphicsDeviceManager GraphicsDeviceManager;
        private StateManager StateManager;
        private GuiFactory GuiFactory;
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

            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Screen = new Screen(new ScreenConstants(), GraphicsDeviceManager);

            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            GraphicsDeviceManager.ApplyChanges();
        }

        protected override void Draw(GameTime gameTime)
        {
            Screen.Draw();
        }

        protected override void Update(GameTime gameTime)
        {
            StateManager.CurrentState.Update(gameTime);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.ClientSizeChanged += delegate
            {
                Screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            };
            Screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);

            StateManager = new StateManager(Screen, _gameConfiguration.KeyMap, _guiConfig.CurrentSkin, GuiFactory);
            StateManager.PushState(_gameConfiguration.InitialState);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            GuiFactory = _guiConfig.Init(Screen.Constants);

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