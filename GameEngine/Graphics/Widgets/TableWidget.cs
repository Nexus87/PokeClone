using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using GameEngine.Utils;
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

        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };
        public event EventHandler OnExitRequested = delegate { };
        public event EventHandler<VisibilityChangedEventArgs> OnVisibilityChanged = delegate { };
        private ITableView<T> tableView;

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

        public ITableView<T> TableView
        {
            get { return tableView; }
            internal set
            {
                value.CheckNull("value");
                tableView = value;
                Invalidate();
            } 
        }


        public ITableModel<T> Model
        {
            get { return tableView.Model; }
            set { tableView.Model = value; }
        }

        public bool HandleInput(CommandKeys key)
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
            tableView.XPosition = XPosition;
            tableView.YPosition = YPosition;
            tableView.Width = Width;
            tableView.Height = Height;
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, ISpriteBatch batch)
        {
            tableView.Draw(time, batch);
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