using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class SelectableContainer : AbstractGraphicComponent, ISelectableGraphicComponent
    {
        private class ArrowDecorator : AbstractGraphicComponent
        {
            private IGraphicComponent arrow;
            public ArrowDecorator(IGraphicComponent arrow)
            {
                this.arrow = arrow;
            }

            public bool ShouldDraw { get; set; }

            protected override void DrawComponent(GameTime time, ISpriteBatch batch)
            {
                if (ShouldDraw)
                    arrow.Draw(time, batch);
            }

            public override void Setup()
            {
                arrow.Setup();
            }

            protected override void Update()
            {
                arrow.SetCoordinates(this);
            }
        }

        Container container;
        private ArrowDecorator arrowBox;

        public SelectableContainer(IGraphicComponent arrowTextureBox, IGraphicComponent component)
        {
            container = new Container { Layout = new HBoxLayout() };
            arrowBox = new ArrowDecorator(arrowTextureBox);

            PreferredHeight = component.PreferredHeight;
            component.PreferredSizeChanged += delegate { PreferredHeight = component.PreferredHeight; };

            container.AddComponent(arrowBox);
            container.AddComponent(component);
        }

        public bool IsSelected { get; private set; }

        public void Select()
        {
            IsSelected = true;
            arrowBox.ShouldDraw = true;
        }

        public void Unselect()
        {
            IsSelected = false;
            arrowBox.ShouldDraw = false;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            container.Draw(time, batch);
        }

        public override void Setup()
        {
            container.Setup();
            arrowBox.VerticalPolicy = ResizePolicy.Fixed;
            arrowBox.HorizontalPolicy = ResizePolicy.Fixed;
        }

        protected override void Update()
        {
            container.SetCoordinates(this);
            SetArrowBoxSize();
        }

        private void SetArrowBoxSize()
        {
            if (CanArrowBoxBeSquare())
                arrowBox.Height = arrowBox.Width = container.Height;
            else
                arrowBox.Height = arrowBox.Width = 0;
            
        }

        private bool CanArrowBoxBeSquare()
        {
            return container.Height < container.Width;
        }
    }
}
