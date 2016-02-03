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
        private IGraphicComponent box;
        public void AddContent(IGraphicComponent component)
        {
            Layout.AddComponent(component);
        }

        public Frame(String backgroundTexture, Game game) : base(game)
        {
            box = new TextureBox(backgroundTexture, game);
            box.PositionChanged += box_PositionChanged;
            box.SizeChanged += box_SizeChanged;
            Layout = new SingleComponentLayout();

            SetMargins(90, 90, 80, 80);
        }

        public override event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = delegate { };

        public override event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = delegate { };

        public override float Height { get { return box.Height; } set { box.Height = value; } }
        public ILayout Layout { get; set; }
        public override float Width { get { return box.Width; } set { box.Width = value; } }
        public override float X { get { return box.X; } set { box.X = value; } }
        public override float Y { get { return box.Y; } set { box.Y = value; } }

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            box.Draw(time, batch);
            Layout.Draw(time, batch);
        }

        public void SetMargins(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            if (Layout == null)
                return;

            Layout.SetMargin(left, right, top, bottom);
        }

        public override void Setup(ContentManager content)
        {
            if (box == null)
                return;

            box.Setup(content);

            if (Layout == null)
                return;

            Layout.Init(box);
            Layout.Setup(content);
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