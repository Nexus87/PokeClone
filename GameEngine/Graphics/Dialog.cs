using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
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
        string texture;

        public ILayout Layout
        {
            get { return InnerComponent.Layout; }
            set
            {
                InnerComponent.Layout = value;
                Invalidate();
            }
        }

        public void AddWidget(IWidget w)
        {
            InnerComponent.AddComponent(w);
        }

        public Dialog(PokeEngine game) : this(null, game) { }

        public Dialog(string borderTexture, PokeEngine game) : base(new Container(game), game)
        {
            border = new TextureBox(borderTexture, game);
            texture = borderTexture;
            InnerComponent.Layout = new SingleComponentLayout();
        }

        public override void Setup(ContentManager content)
        {
            border.Setup(content);
            base.Setup(content);
        }

        public event EventHandler<VisibilityChangedArgs> OnVisibilityChanged;

        public bool IsVisible { get; set; }

        public bool HandleInput(Keys key)
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
            if (texture == null)
                return;

            Layout.SetMargin(100, 50, 100, 50);
            border.X = X;
            border.Y = Y;
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
