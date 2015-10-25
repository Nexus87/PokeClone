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
        SpriteBatch _batch;

        public Engine() : base()
        {
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
            _grapics.Setup(GraphicsDevice.Viewport.Bounds, Content);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(248, 248, 248, 0));
            _batch.Begin();
            _grapics.Draw(origin, _batch, gameTime);
            _batch.End();
        }

        protected override void Initialize()
        {
            foreach (var comp in _components)
                comp.Initialize();
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

