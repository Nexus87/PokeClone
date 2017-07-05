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

            _spriteBatch.GraphicsDevice.SetRenderTarget(_screen.Target);

            _spriteBatch.Begin();
            _spriteBatch.Draw(screenState.Scene);
            _spriteBatch.Draw(screenState.Gui);
            _spriteBatch.End();

            _spriteBatch.GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_screen.Target, _screen.TargetRectangle);
            _spriteBatch.End();

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
            _spriteBatch = new XnaSpriteBatch(GraphicsDevice);

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            OnContentLoad?.Invoke(this);
        }
    }
}