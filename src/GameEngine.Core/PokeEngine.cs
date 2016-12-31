using System;
using System.Linq;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.Registry;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Configuration;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public class PokeEngine : Game, IEngineInterface, IGameComponentManager
    {
        private readonly Configuration _config;
        public IModuleRegistry Registry;
        private readonly GraphicResources _factory;
        private GuiManager GuiManager { get; set; }

        private XnaSpriteBatch _batch;
        private InputComponent _input;
        private Screen _screen;

        private string _startModule;
        private IInputHandler _inputHandler;
        private ISkin _skin;

        public PokeEngine(Configuration config)         {
            _config = config;
            config.CheckNull("config");
            Registry = new AutofacModuleRegistry();
            _factory = new GraphicResources(config, Content);
            new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            
            var runningOnMono = Type.GetType ("Mono.Runtime") != null;
            Content.RootDirectory = runningOnMono ? @"Content\Linux" : @"Content\Windows";

            Registry.RegisterModule(new GameEngineModule(_factory, this));
        }

        public IGraphicComponent Graphic { get; set; }

        public void AddGameComponent(IGameComponent component)
        {
            Components.Add(new GameComponentWrapper(component, this));
            component.Initialize();
        }

        public void RemoveGameComponent(IGameComponent component)
        {
            var res = Components.FirstOrDefault( c =>
            {
                var comp = c as GameComponentWrapper;
                if (comp == null)
                    return false;

                return comp.Component == component;
            });

            if (res == null)
                return;

            Components.Remove(res);
        }

        public IInputHandler InputHandler
        {
            set
            {
                _inputHandler = value;
                if (_input.Handler == null)
                    _input.Handler = _inputHandler;
            }
            get
            {
                return _inputHandler;
            }
        }

        public void ShowGUI()
        {
            GuiManager.Show();
            _input.Handler = GuiManager;
        }

        public void CloseGUI()
        {
            GuiManager.Close();
            _input.Handler = _inputHandler;
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
            _skin.RegisterRenderers(Registry.TypeRegistry);
            _screen = new Screen(Registry.TypeRegistry.ResolveType<ScreenConstants>(), GraphicsDevice);
            GuiManager = Registry.TypeRegistry.ResolveType<GuiManager>();
            _input = Registry.TypeRegistry.ResolveType<InputComponent>();

            Window.ClientSizeChanged += delegate { _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            _screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            _skin.LoadContent(Content, this, _config);

            Registry.StartModule("GameEngine", this);
            Registry.StartModule(_startModule, this);

            if (Graphic == null)
                throw new InvalidOperationException("Graphic component is not set");
            Graphic.Setup();
            GuiManager.Setup();

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _batch = new XnaSpriteBatch(GraphicsDevice);
            _factory.Setup(this);
        }

        public void SetStartModule(string name)
        {
            _startModule = name;
        }

        public void SetSkin(ISkin skin)
        {
            _skin = skin;
        }
    }
}