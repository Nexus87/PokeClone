using System;
using System.Collections.Generic;
using GameEngine.Core.GameStates;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        private readonly Screen _screen;
        public readonly GraphicsDeviceManager GraphicsDeviceManager;

        internal Action<GameRunner> OnContentLoad;
        internal StateManager StateManager;
        private IReadOnlyDictionary<Keys, CommandKeys> _keyMap;

        public GameRunner(IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            _keyMap = keyMap;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            _screen = new Screen(new ScreenConstants(), GraphicsDeviceManager);

            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            GraphicsDeviceManager.ApplyChanges();
        }

        public State InitialState { get; set; }

        protected override void Draw(GameTime gameTime)
        {
            _screen.Draw();
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
                _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            };
            _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);

            StateManager = new StateManager(_screen, _keyMap);
            StateManager.PushState(InitialState);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            OnContentLoad?.Invoke(this);
        }
    }
}