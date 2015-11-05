using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Engine : Game
    {
        readonly List<GameComponent> _components = new List<GameComponent>();
        readonly List<GameComponent> _suspended = new List<GameComponent> ();
        IGraphicComponent _grapics = null;
        Vector2 origin = new Vector2(0, 0);
        GraphicsDeviceManager graphics;
        Matrix transformation = Matrix.Identity;
        SpriteBatch _batch;
        private int screenHeight;
        private int screenWidth;

        public Engine() : base()
        {
            this.Window.AllowUserResizing = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void setGraphicCompomnent(IGraphicComponent component)
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
            screenWidth = GraphicsDevice.Viewport.Bounds.Width;
            screenHeight = GraphicsDevice.Viewport.Bounds.Height;
            _grapics.Setup(GraphicsDevice.Viewport.Bounds, Content);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(248, 248, 248, 0));
            transformation.M11 = screenWidth;
            transformation.M22 = screenHeight;
            _batch.Begin(transformMatrix: transformation);
            _grapics.Draw(gameTime, _batch, 1, 1);
            _batch.End();
        }

        protected override void Initialize()
        {
            foreach (var comp in _components)
                comp.Initialize();
            base.Initialize();
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

