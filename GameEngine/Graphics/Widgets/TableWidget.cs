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
        private IItemModel<T> model;
        private TableView<T> view;

        public TableWidget()
        {
            handler = new DefaultSelectionHandler();
            model = new DefaultTableModel<T>();
            view = new TableView<T>(model);
            layout = new SingleComponentLayout();

            layout.AddComponent(view);

            InitHandlerEvents();
        }

        public TableWidget(Configuration config)
        {
            handler = new DefaultSelectionHandler(config);
            model = new DefaultTableModel<T>();
            view = new TableView<T>(model);
            layout = new SingleComponentLayout();

            layout.AddComponent(view);

            InitHandlerEvents();
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected;

        private ISelectionHandler Handler
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Handler must not be null");

                if (handler != null)
                {
                    handler.ItemSelected -= handler_ItemSelected;
                }

                handler = value;
                InitHandlerEvents();
            }
        }

        public IItemModel<T> Model
        {
            get { return model; }
            private set
            {
                model = value;
            }
        }

        public void HandleInput(Keys key)
        {
            if (handler != null)
                handler.HandleInput(key);
        }

        public void SetData(T data, int row, int column)
        {
            model.SetData(data, row, column);
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

        private void InitHandlerEvents()
        {
            handler.ItemSelected += handler_ItemSelected;
        }
    }
}