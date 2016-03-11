using GameEngine.EventComponent;
using GameEngine.Graphics;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class PokeEngine : Game
    {
        public static readonly float AspectRation = ScreenWidth / ScreenHeight;
        public static readonly float ScreenHeight = 1080;
        public static readonly float ScreenWidth = 1920;
        public static readonly Color BackgroundColor = new Color(248, 248, 248, 0);
        public bool IsRunning { get; private set; }

        public GUIManager GUIManager { get; private set; }

        private XNASpriteBatch batch;
        private IInputHandler DefaultInputHandler;
        private Rectangle display;
        private InputComponent input;

        private RenderTarget2D target;

        public PokeEngine(Configuration config) : base()
        {
            config.CheckNull("config");

            IsRunning = false;
            input = new InputComponent(this);
            GUIManager = new GUIManager();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            GUIManager.GUIClose += GUI_GUIClose;
            Content.RootDirectory = "Content";

            AddKeyListener(config.KeyBack);
            AddKeyListener(config.KeyDown);
            AddKeyListener(config.KeyLeft);
            AddKeyListener(config.KeyRight);
            AddKeyListener(config.KeyUp);
            AddKeyListener(config.KeySelect);

            Components.Add(input);
            Components.Add(new EventQueue(this));
        }

        // For testing only
        internal PokeEngine(){}

        public IGraphicComponent Graphic { get; set; }


        public IInputHandler InputHandler
        {
            set
            {
                DefaultInputHandler = value;
                if (!GUIManager.IsActive)
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

        public void AddKeyListener(Keys key)
        {
            input.Keys.Add(key);
        }

        public void AddKeyListener(IEnumerable<Keys> keys)
        {
            input.Keys.AddRange(keys);
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
            if (Graphic == null)
                throw new InvalidOperationException("Graphic component is not set");
            Graphic.Setup(Content);
            GUIManager.Setup(Content);
            IsRunning = true;
        }

        private void GUI_GUIClose(object sender, EventArgs e)
        {
            GUIManager.Close();
            input.handler = DefaultInputHandler;
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
    }
}