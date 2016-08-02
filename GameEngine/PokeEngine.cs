using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace GameEngine
{
    public class PokeEngine : Game, IPokeEngine
    {
        public IModuleRegistry registry;
        GraphicResources factory;
        private GUIManager GUIManager { get; set; }

        private XNASpriteBatch batch;
        private IInputHandler DefaultInputHandler;
        private InputComponent input;
        private Screen screen;

        private string startModule;

        public PokeEngine(Configuration config)         {
            config.CheckNull("config");
            registry = new AutofacModuleRegistry();
            factory = new GraphicResources(config, this);

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
                DefaultInputHandler = value;
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
            GUIManager.Show();
            input.handler = GUIManager;
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Begin(batch);
            screen.Draw(Graphic, batch, gameTime);
            screen.Draw(GUIManager, batch, gameTime);
            screen.End(batch);
        }

        protected override void Initialize()
        {
            base.Initialize();
            screen = new Screen(registry.TypeRegistry.ResolveType<ScreenConstants>(), GraphicsDevice);
            GUIManager = registry.TypeRegistry.ResolveType<GUIManager>();
            input = registry.TypeRegistry.ResolveType<InputComponent>();

            Window.ClientSizeChanged += delegate { screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); };
            screen.WindowsResizeHandler(Window.ClientBounds.Width, Window.ClientBounds.Height); 

            registry.StartModule("GameEngine", this);
            registry.StartModule(startModule, this);

            if (Graphic == null)
                throw new InvalidOperationException("Graphic component is not set");
            Graphic.Setup();
            GUIManager.Setup();

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