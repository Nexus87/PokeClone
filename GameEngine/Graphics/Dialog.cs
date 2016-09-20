using GameEngine.Graphics.GUI;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    public class Dialog : ForwardingGraphicComponent<Container>, IWidget
    {
        private readonly IGraphicComponent border;

        public Dialog(ITexture2D borderTexture = null) :
            this(new TextureBox(borderTexture))
        {}
        public Dialog(IGraphicComponent border)
            : base(new Container())

        {
            this.border = border;
            InnerComponent.Layout = new SingleComponentLayout();
        }

        public ILayout Layout
        {
            get { return InnerComponent.Layout; }
            set
            {
                InnerComponent.Layout = value;
                Invalidate();
            }
        }

        public void AddWidget(IWidget widget)
        {
            widget.CheckNull("widget");
            InnerComponent.AddComponent(widget);
        }

        public override void Setup()
        {
            border.Setup();
            base.Setup();
        }

        public bool HandleInput(CommandKeys key)
        {
            var compontents = InnerComponent.Components;

            foreach (var c in compontents)
            {
                if (((IWidget)c).HandleInput(key))
                    return true;
            }

            return false;
        }

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            border.Draw(time, batch);
            base.Draw(time, batch);
        }

        protected override void Update()
        {
            Layout.SetMargin(100, 50, 100, 50);
            border.XPosition = XPosition;
            border.YPosition = YPosition;
            border.Width = Width;
            border.Height = Height;
        }
    }
}