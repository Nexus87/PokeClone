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
        private const float aspectRation = screenWidth / screenHeight;
        private const float screenHeight = 1080;
        private const float screenWidth = 1920;
        private static readonly Color backgroundColor = new Color(248, 248, 248, 0);

        public IGameTypeRegistry registry;
        GraphicResources factory;
        private IEventQueue EventQueue { get; set; }
        private GUIManager GUIManager { get; set; }

        private XNASpriteBatch batch;
        private IInputHandler DefaultInputHandler;
        private Rectangle display;
        private InputComponent input;

        private RenderTarget2D target;

        public PokeEngine(Configuration config)         {
            config.CheckNull("config");
            factory = new GraphicResources(config, this);
            registry = new AutofacGameTypeRegistry();
            GameEngineTypes.Register(registry, factory, this, config);

            new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            Content.RootDirectory = "Content";
        }

        public IGraphicComponent Graphic { get; set; }

        public void AddGameComponent(IGameComponent component)
        {
            Components.Add(new GameComponentWrapper(component, this));
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
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(BackgroundColor);

            batch.Begin();
            Graphic.Draw(gameTime, batch);
            GUIManager.Draw(gameTime, batch);
            batch.End();

            GraphicsDevice.SetRenderTarget(null);
            batch.Begin();
            batch.Draw(target, destinationRectangle: display);
            batch.End();
        }

        protected override void Initialize()
        {
            input = registry.ResolveType<InputComponent>();
            GUIManager = registry.ResolveType<GUIManager>();
            EventQueue = registry.ResolveType<IEventQueue>();

            AddGameComponent(input);
            AddGameComponent(EventQueue);
            base.Initialize();
            target = new RenderTarget2D(GraphicsDevice, (int)ScreenWidth, (int)ScreenHeight);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            batch = new XNASpriteBatch(GraphicsDevice);
            factory.Setup(this);
            if (Graphic == null)
                throw new InvalidOperationException("Graphic component is not set");
            Graphic.Setup();
            GUIManager.Setup();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            var bufferX = (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
            var bufferY = (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

            var windowX = (float)Window.ClientBounds.Width;
            var windowY = (float)Window.ClientBounds.Height;

            var displayRatio = windowX / windowY;
            var invBufferRatio = bufferY / bufferX;

            var scaleX = bufferX / ScreenWidth;
            var scaleY = displayRatio * invBufferRatio * scaleX;

            if (scaleY * ScreenHeight > GraphicsDevice.PresentationParameters.BackBufferHeight)
            {
                scaleY = bufferY / ScreenHeight;
                scaleX = scaleY / (displayRatio * invBufferRatio);
            }

            display.Width = (int)(scaleX * ScreenWidth);
            display.Height = (int)(scaleY * ScreenHeight);
            display.X = (int)((bufferX - display.Width) / 2.0f);
            display.Y = (int)((bufferY - display.Height) / 2.0f);
        }

        public float ScreenHeight
        {
            get { return screenHeight; }
        }

        public float ScreenWidth
        {
            get { return screenWidth; }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
        }
    }
}