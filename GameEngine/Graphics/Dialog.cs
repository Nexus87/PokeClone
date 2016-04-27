using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class Dialog : ForwardingGraphicComponent<Container>, IWidget
    {
        TextureBox border;

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

        public Dialog(IPokeEngine game) : this(null, game) { }

        public Dialog(ITexture2D borderTexture, IPokeEngine game) : base(new Container(game), game)
        {
            border = new TextureBox(borderTexture, game);
            InnerComponent.Layout = new SingleComponentLayout();
        }

        public override void Setup()
        {
            border.Setup();
            base.Setup();
        }

        public event EventHandler<VisibilityChangedEventArgs> OnVisibilityChanged = delegate { };

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                OnVisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
            }
        }
        private bool isVisible;

        public bool HandleInput(CommandKeys key)
        {
            var compontents = InnerComponent.Components;
            
            foreach( var c in compontents){
                if (((IWidget)c).HandleInput(key))
                    return true;
            }

            return false;
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
        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            border.Draw(time, batch);
            base.Draw(time, batch);
        }
    }
}
