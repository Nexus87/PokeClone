﻿using System;
using System.Linq;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.ModuleManager;
using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.GUI;
using GameEngine.GUI.Configuration;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class PokeEngine : Game, IEngineInterface
    {
        private readonly Configuration _config;
        private readonly IModuleManager _manager;
        private GuiManager GuiManager { get; set; }

        private XnaSpriteBatch _batch;
        private InputComponent _input;
        private Screen _screen;

        private string _startModule;
        private ISkin _skin;
        private Graphics.TextureProvider _textureProvider;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private GameComponentManager _gameComponentManager;

        public PokeEngine(Configuration config)         {
            _config = config;
            config.CheckNull("config");
            _manager = new AutofacModuleManager();
            _graphicsDeviceManager = new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            
            Content.RootDirectory = @"Content";

            _manager.RegisterModule(new GameEngineModule(this, config));
        }

        public IGraphicComponent Graphic { get; set; }



        public void ShowGUI()
        {
            GuiManager.Show();
            _input.Handler = GuiManager;
        }

        public void CloseGUI()
        {
            GuiManager.Close();
            _input.Handler = _gameComponentManager.InputHandler;
        }

        protected override void Draw(GameTime gameTime)
        {
            _screen.Begin(_batch);
            _screen.Draw(Graphic, _batch, gameTime);
            _screen.Draw(GuiManager, _batch, gameTime);
            _screen.End(_batch);
        }

        protected override void Initialize()
        {
            base.Initialize();
            var textureConfigBuilder = new TextureConfigurationBuilder();
            _skin.AddTextureConfigurations(textureConfigBuilder);
            _textureProvider = textureConfigBuilder.BuildProvider(Content);
            _skin.RegisterRenderers(_manager.TypeRegistry, _textureProvider);
            _screen = new Screen(_manager.TypeRegistry.ResolveType<ScreenConstants>(), GraphicsDevice);
            GuiManager = _manager.TypeRegistry.ResolveType<GuiManager>();
            _input = _manager.TypeRegistry.ResolveType<InputComponent>();
            _gameComponentManager = new GameComponentManager(Components, _input, this);
            Window.ClientSizeChanged += delegate { _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            _textureProvider.Init(_graphicsDeviceManager.GraphicsDevice);

            _manager.StartModule("GameEngine", _gameComponentManager);
            _manager.StartModule(_startModule, _gameComponentManager);

            if (Graphic == null)
                throw new InvalidOperationException("Graphic component is not set");
            Graphic.Setup();
            GuiManager.Setup();

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _batch = new XnaSpriteBatch(GraphicsDevice);
        }

        public void SetStartModule(string name)
        {
            _startModule = name;
        }

        public void SetSkin(ISkin skin)
        {
            _skin = skin;
        }

        public void RegisterModule(IModule module)
        {
            _manager.RegisterModule(module);
        }
    }
}