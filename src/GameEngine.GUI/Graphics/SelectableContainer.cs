using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class SelectableContainer<T> : AbstractGraphicComponent where T : IGraphicComponent
    {
        private readonly ArrowDecorator _arrowBox;

        private T _content;
        private readonly Grid _grid;

        public SelectableContainer(GraphicResources resources) : 
            this(new TextureBox(resources.DefaultArrowTexture))
        {}


        public SelectableContainer(IGraphicComponent arrowTextureBox, T component)
        {
            _arrowBox = new ArrowDecorator(arrowTextureBox);
            _grid = new Grid();
            _grid.AddPercentRow();
            _grid.AddColumn(new ColumnProperty{Type = ValueType.Auto});
            _grid.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});
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
                UnregisterHandler();
                _content = value;
                if (_content != null)
                {
                    PreferredHeight = _content.PreferredHeight;
                    RegisterHandler();
                    _grid.SetComponent(_content, 0, 1);
                }
                else
                {
                    _grid.SetComponent(new NullGraphicObject(), 0, 1);
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
            _grid.SetComponent(_arrowBox, 0, 0);
            _grid.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _grid.Draw(time, batch);
        }

        protected override void Update()
        {
            SetArrowBoxSize();
            _arrowBox.ShouldDraw = IsSelected;
            _grid.SetCoordinates(this);
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

        private void OnPreferredSizeChanged(object sender, GraphicComponentSizeChangedEventArgs args)
        {
            PreferredHeight = _content.PreferredHeight;
        }

        private void SetArrowBoxSize()
        {
            if (CanArrowBoxBeSquare())
            {
                _arrowBox.PreferredHeight = Area.Height;
                _arrowBox.PreferredWidth = Area.Height;
            }
            else
            {
                _arrowBox.PreferredHeight = 0;
                _arrowBox.PreferredWidth = 0;
            }
        }

        private bool CanArrowBoxBeSquare()
        {
            return Area.Height < Area.Width;
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