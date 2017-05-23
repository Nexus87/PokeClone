using System;
using System.Collections.Generic;
using GameEngine.Core.GameStates;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        internal Action OnInit;
        private readonly Screen _screen;
        private readonly StateManager _stateManager;
        internal readonly List<TextureProvider> TextureProviders = new List<TextureProvider>();
        public readonly GraphicsDeviceManager GraphicsDeviceManager;

        public GameRunner(StateManager stateManager, Screen screen)         {
            _stateManager = stateManager;
            _screen = screen;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            GraphicsDeviceManager.ApplyChanges();
        }


        protected override void Update(GameTime gameTime)
        {
            _stateManager.CurrentState.Update(gameTime);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.ClientSizeChanged += delegate { _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);

            OnInit?.Invoke();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            TextureProviders.ForEach(x => x.Init(GraphicsDevice));
        }

    }
}