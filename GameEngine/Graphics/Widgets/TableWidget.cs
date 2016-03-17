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
        private TableView<T> view;
        private static ISpriteFont DefaultCreator()
        {
            return new XNASpriteFont();
        }

        public TableWidget(PokeEngine game)
            : base(new TableView<T>(new DefaultTableModel<T>(), new DefaultTableRenderer<T>(game, DefaultCreator), new DefaultTableSelectionModel(), game), game)
        {
            view = InnerComponent;
        }

        public TableWidget(Configuration config, PokeEngine game)
            : base(new TableView<T>(new DefaultTableModel<T>(), new DefaultTableRenderer<T>(game, DefaultCreator), new DefaultTableSelectionModel(), game), game)
        {
            view = InnerComponent;
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
        public ITableModel<T> Model
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        public T GetData(int row, int column)
        {
            return Model.DataAt(row, column);
        }

        public bool HandleInput(Keys key)
        {
            return false;
        }

        public void SetData(T data, int row, int column)
        {
            Model.SetData(data, row, column);
        }

        private void handler_CloseRequested(object sender, EventArgs e)
        {
            OnExitRequested(this, null);
        }

        protected override void Update()
        {
        }
    }
}