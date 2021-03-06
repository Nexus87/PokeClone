﻿using System;
using System.Collections.Generic;
using GameEngine.Graphics;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class GameRunner : Game, IEngineInterface
    {
        internal Action OnInit;
        internal GuiManager GuiManager { get; set; }

        private XnaSpriteBatch _batch;
        internal Screen Screen { get; set; }

        internal List<TextureProvider> TextureProviders = new List<TextureProvider>();
        public readonly GraphicsDeviceManager GraphicsDeviceManager;

        public GameRunner()         {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = @".";

            GraphicsDeviceManager.ApplyChanges();
        }

        public Scene Scene { get; set; }


        protected override void Draw(GameTime gameTime)
        {
            Screen.Begin(_batch);
            Screen.Draw(Scene, _batch, gameTime);
            Screen.Draw(GuiManager, _batch, gameTime);
            Screen.End(_batch);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.ClientSizeChanged += delegate { Screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            Screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);

            OnInit?.Invoke();

            if (Scene == null)
                throw new InvalidOperationException("Graphic component is not set");
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            TextureProviders.ForEach(x => x.Init(GraphicsDevice));
            _batch = new XnaSpriteBatch(GraphicsDevice);
        }

    }
}