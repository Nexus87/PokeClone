using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace GameEngine
{
    public class PokeEngine : Game, IEngineInterface, IGameComponentManager
    {
        public IModuleRegistry registry;
        private readonly GraphicResources factory;
        private GUIManager GuiManager { get; set; }

        private XNASpriteBatch batch;
        private InputComponent input;
        private Screen screen;

        private string startModule;

        public PokeEngine(Configuration config)         {
            config.CheckNull("config");
            registry = new AutofacModuleRegistry();
            factory = new GraphicResources(config, Content);

            new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            

            Content.RootDirectory = "Content";

            registry.RegisterModule(new GameEngineModule(factory));
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

        private IInputHandler InputHandler
        {
            set
            {
                if (input.handler == null)
                    input.handler = value;
            }
            get
            {
                return input.handler;
            }
        }

        public void ShowGUI()
        {
            GuiManager.Show();
            input.handler = GuiManager;
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Begin(batch);
            screen.Draw(Graphic, batch, gameTime);
            screen.Draw(GuiManager, batch, gameTime);
            screen.End(batch);
        }

        protected override void Initialize()
        {
            base.Initialize();
            screen = new Screen(registry.TypeRegistry.ResolveType<ScreenConstants>(), GraphicsDevice);
            GuiManager = registry.TypeRegistry.ResolveType<GUIManager>();
            input = registry.TypeRegistry.ResolveType<InputComponent>();

            Window.ClientSizeChanged += delegate { screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); 

            registry.StartModule("GameEngine", this);
            registry.StartModule(startModule, this);

            if (Graphic == null)
                throw new InvalidOperationException("Graphic component is not set");
            Graphic.Setup();
            GuiManager.Setup();

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            batch = new XNASpriteBatch(GraphicsDevice);
            factory.Setup(this);
        }

        public void SetStartModule(string name)
        {
            startModule = name;
        }

    }
}