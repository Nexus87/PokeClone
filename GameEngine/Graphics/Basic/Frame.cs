using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics.Basic
{
    public class Frame : IGraphicComponent
    {
        public PokeEngine Game { get; private set; }

        private Container container;
        private IGraphicComponent box;

        public void AddContent(IGraphicComponent component)
        {
            container.RemoveAllComponents();
            container.AddComponent(component);
        }

        public Frame(String backgroundTexture, PokeEngine game)
        {
            Game = game;
            container = new Container(game);
            box = new TextureBox(backgroundTexture, game);
            box.PositionChanged += box_PositionChanged;
            box.SizeChanged += box_SizeChanged;
            container.Layout = new SingleComponentLayout();

            SetMargins(90, 90, 80, 80);
        }

        public event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = delegate { };

        public event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = delegate { };

        public float Height { get { return box.Height; } set { box.Height = container.Height = value; } }
        public ILayout Layout { get; set; }
        public float Width { get { return box.Width; } set { box.Width = container.Width = value; } }
        public float X { get { return box.X; } set { box.X = container.X = value; } }
        public float Y { get { return box.Y; } set { box.Y = container.Y = value; } }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            box.Draw(time, batch);
            container.Draw(time, batch);
        }

        public void SetMargins(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            if (Layout == null)
                return;

            Layout.SetMargin(left, right, top, bottom);
        }

        public void Setup(ContentManager content)
        {
            if (box == null)
                return;

            box.Setup(content);
            container.Setup(content);
        }

        private void box_PositionChanged(object sender, GraphicComponentPositionChangedArgs e)
        {
            PositionChanged(this, e);
        }

        private void box_SizeChanged(object sender, GraphicComponentSizeChangedArgs e)
        {
            SizeChanged(this, e);
        }
    }
}