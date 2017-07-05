using System;
using System.Collections.Generic;
using GameEngine.Core.ECS.Messages;
using GameEngine.Core.GameStates;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        private readonly Screen _screen;
        public readonly GraphicsDeviceManager GraphicsDeviceManager;
        internal readonly List<TextureProvider> TextureProviders = new List<TextureProvider>();
        private XnaSpriteBatch _spriteBatch;
        private StateManager _stateManager;
        internal Action<GameRunner> OnContentLoad;

        public GameRunner()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            _screen = new Screen(new ScreenConstants(), GraphicsDeviceManager);

            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            GraphicsDeviceManager.ApplyChanges();
        }

        protected override void Draw(GameTime gameTime)
        {
            var screenState = _stateManager.CurrentState.ScreenState;

            _screen.Draw(screenState.Scene, screenState.Gui, _spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            _stateManager.CurrentState.MessagingSystem.SendMessage(new TimerAction {Time = gameTime});
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.ClientSizeChanged += delegate
            {
                _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            };
            _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            _stateManager = new StateManager(GraphicsDevice, new ScreenConstants());
            OnContentLoad?.Invoke(this);
            _spriteBatch = new XnaSpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            TextureProviders.ForEach(x => x.Init(GraphicsDevice));
        }
    }
}