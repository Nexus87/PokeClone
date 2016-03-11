using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Views;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class TableWidget<T> : ForwardingGraphicComponent<TableView<T>>, IWidget
    {
        private ISelectionHandler handler;
        private TableView<T> view;

        public TableWidget(PokeEngine game)
            : base(new TableView<T>(new DefaultTableModel<T>(), game), game)
        {
            view = InnerComponent;
            Handler = new DefaultSelectionHandler();
        }

        public TableWidget(Configuration config, PokeEngine game)
            : base(new TableView<T>(new DefaultTableModel<T>(), game), game)
        {
            view = InnerComponent;
            Handler = new DefaultSelectionHandler(config);
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };
        public event EventHandler OnExitRequested = delegate { };

        public event EventHandler<VisibilityChangedEventArgs> OnVisibilityChanged = delegate { };

        public bool IsVisible { 
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
        public IItemModel<T> Model
        {
            get { return view.Model; }
            set { view.Model = value; }
        }

        private ISelectionHandler Handler
        {
            set
            {
                value.CheckNull("value");

                if (handler != null)
                {
                    handler.ItemSelected -= handler_ItemSelected;
                    handler.CloseRequested -= handler_CloseRequested;
                }

                handler = value;
                handler.ItemSelected += handler_ItemSelected;
                handler.CloseRequested += handler_CloseRequested;
                handler.Init(view);
            }
        }

        public T GetData(int row, int column)
        {
            return Model.DataAt(row, column);
        }

        public bool HandleInput(Keys key)
        {
            return handler.HandleInput(key);
        }

        public void SetData(T data, int row, int column)
        {
            Model.SetData(data, row, column);
        }

        private void handler_CloseRequested(object sender, EventArgs e)
        {
            OnExitRequested(this, null);
        }

        private void handler_ItemSelected(object sender, EventArgs e)
        {
            var Item = Model.DataAt(handler.SelectedRow, handler.SelectedColumn);
            ItemSelected(this, new SelectionEventArgs<T>(selectedData: Item));
        }

        protected override void Update()
        {
        }
    }
}