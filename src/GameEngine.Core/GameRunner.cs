using System;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        internal GuiManager GuiManager { get; set; }

        private XnaSpriteBatch _batch;
        internal Screen Screen { get; set; }

        internal TextureProvider TextureProvider;
        public readonly GraphicsDeviceManager GraphicsDeviceManager;

        public GameRunner()         {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = @"Content";

            GraphicsDeviceManager.ApplyChanges();
        }

        public IGuiComponent Gui { get; set; }


        protected override void Draw(GameTime gameTime)
        {
            Screen.Begin(_batch);
            Screen.Draw(Gui, _batch, gameTime);
            Screen.Draw(GuiManager, _batch, gameTime);
            Screen.End(_batch);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.ClientSizeChanged += delegate { Screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            Screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);

            if (Gui == null)
                throw new InvalidOperationException("Graphic component is not set");
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            TextureProvider.Init(GraphicsDevice);
            _batch = new XnaSpriteBatch(GraphicsDevice);
        }

    }
}