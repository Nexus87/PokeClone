using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    [GameTypeAttribute]
    [DefaultParameter("borderTexture", GameEngineTypes.ResourceKeys.BorderTexture)]
    public class Dialog : ForwardingGraphicComponent<Container>, IWidget
    {
        private TextureBox border;

        private bool isVisible;

        public Dialog(ITexture2D borderTexture = null)
            : base(new Container())
        {
            border = new TextureBox(borderTexture);
            InnerComponent.Layout = new SingleComponentLayout();
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };

        public ILayout Layout
        {
            get { return InnerComponent.Layout; }
            set
            {
                InnerComponent.Layout = value;
                Invalidate();
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
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
            if (border.Image == null)
                return;

            Layout.SetMargin(100, 50, 100, 50);
            border.XPosition = XPosition;
            border.YPosition = YPosition;
            border.Width = Width;
            border.Height = Height;
        }
    }
}