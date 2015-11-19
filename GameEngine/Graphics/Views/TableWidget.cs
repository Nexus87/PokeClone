using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Views
{
    public class TableWidget<T> : AbstractGraphicComponent, IWidget
    {
        private ISelectionHandler<T> handler;
        private SingleComponentLayout layout;
        private IItemModel<T> model;
        private TableView<T> view;

        public TableWidget()
        {
            handler = new DefaultSelectionHandler<T>();
            model = new DefaultTableModel<T>();
            view = new TableView<T>(model);
            layout = new SingleComponentLayout();

            layout.AddComponent(view);

            InitHandlerEvents();
            InitModelEvents();

            model_SizeChanged(null, null);
        }

        public TableWidget(Configuration config)
        {
            handler = new DefaultSelectionHandler<T>(config);
            model = new DefaultTableModel<T>();
            view = new TableView<T>(model);
            layout = new SingleComponentLayout();

            layout.AddComponent(view);

            InitHandlerEvents();
            InitModelEvents();

            model_SizeChanged(null, null);
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected;

        public ISelectionHandler<T> Handler
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Handler must not be null");

                if (handler != null)
                {
                    handler.ItemSelected -= handler_ItemSelected;
                    handler.SelectionChanged -= handler_SelectionChanged;
                }

                handler = value;
                InitHandlerEvents();
                handler_SelectionChanged(null, null);
            }
        }

        public IItemModel<T> Model
        {
            get { return model; }
            set
            {
                model = value;
                view.Model = model;
                InitModelEvents();
                model_SizeChanged(null, null);
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

        protected override void DrawComponent(GameTime time, SpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        private void handler_ItemSelected(object sender, EventArgs e)
        {
            var Item = Model.DataAt(handler.SelectedRow, handler.SelectedColumn);
            ItemSelected(this, new SelectionEventArgs<T> { SelectedData = Item });
        }

        private void handler_SelectionChanged(object sender, EventArgs e)
        {
            view.SelectItem(handler.SelectedRow, handler.SelectedColumn);
        }

        private void InitHandlerEvents()
        {
            handler.ItemSelected += handler_ItemSelected;
            handler.SelectionChanged += handler_SelectionChanged;
        }

        private void InitModelEvents()
        {
            model.SizeChanged += model_SizeChanged;
        }

        private void model_SizeChanged(object sender, EventArgs e)
        {
            handler.Init(model);
        }
    }
}