using System;
using System.Collections.Generic;
using GameEngine.Core.GameStates;
using GameEngine.Globals;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        internal readonly Screen Screen;
        public readonly GraphicsDeviceManager GraphicsDeviceManager;

        internal Action<GameRunner> OnContentLoad;
        internal StateManager StateManager;
        internal ISkin Skin;
        internal GuiFactory GuiFactory;
        private readonly IReadOnlyDictionary<Keys, CommandKeys> _keyMap;

        public GameRunner(IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            _keyMap = keyMap;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Screen = new Screen(new ScreenConstants(), GraphicsDeviceManager);

            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            GraphicsDeviceManager.ApplyChanges();
        }

        public State InitialState { get; set; }

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

            StateManager = new StateManager(Screen, _keyMap, Skin, GuiFactory);
            StateManager.PushState(InitialState);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            OnContentLoad?.Invoke(this);
        }
    }
}