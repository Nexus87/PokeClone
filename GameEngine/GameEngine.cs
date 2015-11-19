using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Engine : Game
    {
        public static readonly float ScreenWidth = 1920;
        public static readonly float ScreenHeight = 1080;
        
        public readonly Configuration Config;
        public static readonly float AspectRation = ScreenWidth/ScreenHeight;

        public readonly GUIManager GUI = new GUIManager();

        public static void ShowGUI()
        {
            engine.GUI.Show();
            engine.input.handler = engine.GUI;
        }

        public static void Init(Configuration config)
        {
            engine = new Engine(config);
        }

        public static Engine GetInstance()
        {
            return engine;
        }

        private static Engine engine;
        public IInputHandler DefaultInputHandler;

        RenderTarget2D target;
        GraphicsDeviceManager manager;
        readonly List<GameComponent> _components = new List<GameComponent>();
        readonly List<GameComponent> _suspended = new List<GameComponent> ();

        IGraphicComponentOld _grapics = null;
        private Matrix transformation = Matrix.Identity;
        private SpriteBatch _batch;
        private Rectangle display;
        private InputComponent input;

        public void AddKeyListener(Keys key)
        {
            input.Keys.Add(key);
        }

        public void AddKeyListener(IEnumerable<Keys> keys)
        {
            input.Keys.AddRange(keys);
        }

        private Engine(Configuration config) : base()
        {
            this.Config = config;
            input = new InputComponent(this);
            manager = new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            GUI.GUIClose += GUI_GUIClose;

            Content.RootDirectory = "Content";
        }

        void GUI_GUIClose(object sender, EventArgs e)
        {
            input.handler = DefaultInputHandler;
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
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

            display.Width = (int) (scaleX * ScreenWidth);
            display.Height = (int)(scaleY * ScreenHeight);
            display.X = (int)((bufferX - display.Width) / 2.0f);
            display.Y = (int)((bufferY - display.Height) / 2.0f);
        }

        public void setGraphicCompomnent(IGraphicComponentOld component)
        {
            _grapics = component;
        }
        public void AddComponent(GameComponent component){
            
            component.Initialize();
            _components.Add (component);
        }

        public void RemoveComponent(GameComponent component){
            if (_components.Remove(component) || _suspended.Remove(component))
            {
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _batch = new SpriteBatch(GraphicsDevice);
            if (_grapics == null)
                throw new InvalidOperationException("Graphic component is not set");
            _grapics.Setup(GraphicsDevice.Viewport.Bounds, Content);

            //transformation = Matrix.CreateOrthographic(screenWidth, screenHeight, 0, 0);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(new Color(248, 248, 248, 0));
            
            _batch.Begin();
            _grapics.Draw(gameTime, _batch, 1, 1);
            _batch.End();

            GraphicsDevice.SetRenderTarget(null);
            _batch.Begin();
            _batch.Draw(target, destinationRectangle: display);
            _batch.End();

        }

        protected override void Initialize()
        {
            foreach (var comp in _components)
                comp.Initialize();
            base.Initialize();
            target = new RenderTarget2D(GraphicsDevice, (int)ScreenWidth, (int)ScreenHeight);
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach(var comp in _components)
            {
                if(comp.Enabled)
                    comp.Update(gameTime);
            }

        }
    }
}

