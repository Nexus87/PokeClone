using System;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.Layouts;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class SelectableContainer<T> : AbstractGraphicComponent, ISelectableGraphicComponent where T : IGraphicComponent
    {
        private readonly Container _container;

        private readonly ArrowDecorator _arrowBox;

        private T _content;

        public SelectableContainer(GraphicResources resources) : 
            this(new TextureBox(resources.DefaultArrowTexture))
        {}


        public SelectableContainer(IGraphicComponent arrowTextureBox, T component)
        {
            _container = new Container { Layout = new HBoxLayout() };
            _arrowBox = new ArrowDecorator(arrowTextureBox);

            _container.AddComponent(_arrowBox);
            Content = component;
        }

        public SelectableContainer(IGraphicComponent arrowTextureBox) :
            this(arrowTextureBox, default(T))
        {
        }

        public T Content
        {
            get
            {
                return _content;
            }
            set
            {
                _container.RemoveComponent(_content);
                UnregisterHandler();
                _content = value;
                if (_content != null)
                {
                    PreferredHeight = _content.PreferredHeight;
                    RegisterHandler();
                    _container.AddComponent(_content);
                }
            }
        }

        public void Select()
        {
            IsSelected = true;
            _arrowBox.ShouldDraw = true;
        }

        public void Unselect()
        {
            IsSelected = false;
            _arrowBox.ShouldDraw = false;
        }

        public override void Setup()
        {
            _container.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _container.Draw(time, batch);
        }

        protected override void Update()
        {
            _container.SetCoordinates(this);
            SetArrowBoxSize();
            _arrowBox.ShouldDraw = IsSelected;
        }

        private void UnregisterHandler()
        {
            if (_content != null)
                _content.PreferredSizeChanged -= OnPreferredSizeChanged;
        }

        private void RegisterHandler()
        {
            _content.PreferredSizeChanged += OnPreferredSizeChanged;
        }

        private void OnPreferredSizeChanged(Object sender, GraphicComponentSizeChangedEventArgs args)
        {
            PreferredHeight = _content.PreferredHeight;
        }

        private void SetArrowBoxSize()
        {
            if (CanArrowBoxBeSquare())
            {
                _arrowBox.SetCoordinates(_arrowBox.Area.X, _arrowBox.Area.Y, _container.Area.Height, _container.Area.Height);
            }
            else
            {
                _arrowBox.SetCoordinates(_arrowBox.Area.X, _arrowBox.Area.Y, 0, 0);
            }
        }

        private bool CanArrowBoxBeSquare()
        {
            return _container.Area.Height < _container.Area.Width;
        }

        private class ArrowDecorator : AbstractGraphicComponent
        {
            private readonly IGraphicComponent _arrow;

            public ArrowDecorator(IGraphicComponent arrow)
            {
                _arrow = arrow;
                HorizontalPolicy = VerticalPolicy = ResizePolicy.Fixed;
            }

            public bool ShouldDraw { private get; set; }

            public override void Setup()
            {
                _arrow.Setup();
            }

            protected override void DrawComponent(GameTime time, ISpriteBatch batch)
            {
                if (ShouldDraw)
                    _arrow.Draw(time, batch);
            }

            protected override void Update()
            {
                _arrow.SetCoordinates(this);
            }
        }
    }
}