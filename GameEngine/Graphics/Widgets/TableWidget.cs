using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class TableWidget<T> : AbstractGraphicComponent, IWidget
    {
        private ISelectionHandler handler;
        private SingleComponentLayout layout;
        private TableView<T> view;

        public TableWidget()
        {
            
            var model = new DefaultTableModel<T>();
            view = new TableView<T>(model);
            Handler = new DefaultSelectionHandler();
            layout = new SingleComponentLayout();

            layout.AddComponent(view);
        }

        public TableWidget(Configuration config)
        {
            var model = new DefaultTableModel<T>();
            view = new TableView<T>(model);
            Handler = new DefaultSelectionHandler(config);
            layout = new SingleComponentLayout();

            layout.AddComponent(view);
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected = delegate { };
        public event EventHandler OnExitRequested = delegate { };

        private ISelectionHandler Handler
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Handler must not be null");

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

        void handler_CloseRequested(object sender, EventArgs e)
        {
            OnExitRequested(this, null);
        }

        public IItemModel<T> Model
        {
            get { return view.Model; }
            set { view.Model = value; }
        }

        public void HandleInput(Keys key)
        {
            handler.HandleInput(key);
        }

        public void SetData(T data, int row, int column)
        {
            Model.SetData(data, row, column);
        }

        public T GetData(int row, int column)
        {
            return Model.DataAt(row, column);
        }

        public override void Setup(ContentManager content)
        {
            layout.Init(this);
            layout.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        private void handler_ItemSelected(object sender, EventArgs e)
        {
            var Item = Model.DataAt(handler.SelectedRow, handler.SelectedColumn);
            ItemSelected(this, new SelectionEventArgs<T> { SelectedData = Item });
        }
    }
}