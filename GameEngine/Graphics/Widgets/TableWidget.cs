using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class TableWidget<T> : AbstractGraphicComponent, IWidget
    {
        private bool isVisible;
        private TableView<T> view;

        public int? VisibleRows { get; set; }
        public int? VisibleColumns { get; set; }

        public TableWidget(PokeEngine game)
            : base(game)
        {
        }

        public TableWidget(Configuration config, PokeEngine game)
            : base(game)
        {
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };
        public event EventHandler OnExitRequested = delegate { };
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

        public ITableView<T> TableView { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public ITableModel<T> Model { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public bool HandleInput(CommandKeys key)
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, ISpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public override void Setup(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void SelectCell(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}