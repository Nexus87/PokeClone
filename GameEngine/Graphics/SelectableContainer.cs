using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    [GameType]
    public class SelectableContainer<T> : AbstractGraphicComponent, ISelectableGraphicComponent where T : IGraphicComponent
    {
        private Container container;

        private ArrowDecorator arrowBox;

        private T content;

        public SelectableContainer(GraphicResources resources) : 
            this(new TextureBox(resources.DefaultArrowTexture))
        {}
       

        internal SelectableContainer(IGraphicComponent arrowTextureBox, T component)
        {
            container = new Container { Layout = new HBoxLayout() };
            arrowBox = new ArrowDecorator(arrowTextureBox);

            container.AddComponent(arrowBox);
            Content = component;
        }

        internal SelectableContainer(IGraphicComponent arrowTextureBox) :
            this(arrowTextureBox, default(T))
        {
        }

        public T Content
        {
            get
            {
                return content;
            }
            set
            {
                container.RemoveComponent(content);
                UnregisterHandler();
                content = value;
                if (content != null)
                {
                    PreferredHeight = content.PreferredHeight;
                    RegisterHandler();
                    container.AddComponent(content);
                }
            }
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

        public override void Setup()
        {
            container.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            container.Draw(time, batch);
        }

        protected override void Update()
        {
            container.SetCoordinates(this);
            SetArrowBoxSize();
        }

        private void UnregisterHandler()
        {
            if (content != null)
                content.PreferredSizeChanged -= OnPreferredSizeChanged;
        }

        private void RegisterHandler()
        {
            content.PreferredSizeChanged += OnPreferredSizeChanged;
        }

        private void OnPreferredSizeChanged(Object sender, GraphicComponentSizeChangedEventArgs args)
        {
            PreferredHeight = content.PreferredHeight;
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

        private class ArrowDecorator : AbstractGraphicComponent
        {
            private IGraphicComponent arrow;

            public ArrowDecorator(IGraphicComponent arrow)
            {
                this.arrow = arrow;
                HorizontalPolicy = VerticalPolicy = ResizePolicy.Fixed;
            }

            public bool ShouldDraw { get; set; }

            public override void Setup()
            {
                arrow.Setup();
            }

            protected override void DrawComponent(GameTime time, ISpriteBatch batch)
            {
                if (ShouldDraw)
                    arrow.Draw(time, batch);
            }

            protected override void Update()
            {
                arrow.SetCoordinates(this);
            }
        }
    }
}