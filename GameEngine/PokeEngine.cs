using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
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

        internal GraphicComponentFactory factory;

        public IEventQueue EventQueue { get; set; }
        public GUIManager GUIManager { get; private set; }

        private XNASpriteBatch batch;
        private IInputHandler DefaultInputHandler;
        private Rectangle display;
        private InputComponent input;

        private RenderTarget2D target;

        public PokeEngine(Configuration config) : base()
        {
            config.CheckNull("config");

            input = new InputComponent(this, config);
            GUIManager = new GUIManager();

            new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            Content.RootDirectory = "Content";

            AddGameComponent(input);
            EventQueue = new EventQueue();
            AddGameComponent(EventQueue);
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
            float bufferX = (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
            float bufferY = (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

            float windowX = (float)Window.ClientBounds.Width;
            float windowY = (float)Window.ClientBounds.Height;

            float displayRatio = windowX / windowY;
            float invBufferRatio = bufferY / bufferX;

            float scaleX = bufferX / ScreenWidth;
            float scaleY = displayRatio * invBufferRatio * scaleX;

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