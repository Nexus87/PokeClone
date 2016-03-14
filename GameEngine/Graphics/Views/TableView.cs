using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Views
{
    public class TableView<T> : ForwardingGraphicComponent<Container>, ITableView
    {
        private static XNASpriteFont DefaultCreator()
        {
            return new XNASpriteFont();
        }

        private const int visibleColumns = 8;
        private const int visibleRows = 8;
        internal ISelectableGraphicComponent[,] items;
        private GridLayout layout;
        private IItemModel<T> model;
        private int startColumn = 0;
        private int startRow = 0;
        private readonly SpriteFontCreator creator;

        private ITableRenderer<T> renderer;
        public TableView(IItemModel<T> model, PokeEngine game)
            : this(model, new DefaultTableRenderer<T>(game, DefaultCreator), game, DefaultCreator)
        {}

        public TableView(IItemModel<T> model, ITableRenderer<T> renderer, PokeEngine game, SpriteFontCreator creator)
            : base(new Container(game), game)
        {
            model.CheckNull("model");

            this.renderer = renderer;
            this.creator = creator;

            SetModel(model);
            layout = new GridLayout(1, 1);
            InnerComponent.Layout = layout;
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize = delegate { };

        public int Columns { get { return Model.Columns; } }
        public int Rows { get { return Model.Rows; } }

        public int ViewportColumns { get { return Math.Min(visibleColumns, Model.Columns); } }
        public int ViewportRows { get { return Math.Min(visibleRows, Model.Rows); } }

        public IItemModel<T> Model { 
            get { return model; }
            set
            {
                value.CheckNull("value");

                bool sizeChanged = value.Rows != model.Rows || value.Columns != model.Columns;

                SetModel(value);
                Invalidate();

                if (sizeChanged)
                    OnTableResize(this, new TableResizeEventArgs(value.Rows, value.Columns));
            }
        }

        private void SetModel(IItemModel<T> value)
        {
            if (model != null)
            {
                model.DataChanged -= model_DataChanged;
                model.SizeChanged -= model_SizeChanged;
            }

            model = value;
            model.DataChanged += model_DataChanged;
            model.SizeChanged += model_SizeChanged;

            items = new ISelectableGraphicComponent[model.Rows, model.Columns];
            InitItems();
        }

        public int ViewportStartColumn
        {
            get { return startColumn; }
            set
            {
                int newColumn = Math.Min(value, Model.Columns - ViewportColumns);

                if (startColumn == newColumn)
                    return;

                startColumn = newColumn;
                Invalidate();
            }
        }

        public int ViewportStartRow
        {
            get { return startRow; }
            set
            {
                int newRow = Math.Min(value, Model.Rows - ViewportRows);
                if (startRow == newRow)
                    return;

                startRow = newRow;
                Invalidate();
            }
        }

        public bool IsCellSelected(int row, int column)
        {
            if (row >= Model.Rows || column >= Model.Columns || row < 0 || column < 0)
                return false;

            return items[row, column] != null && items[row, column].IsSelected;
        }

        public bool SetCellSelection(int row, int column, bool isSelected)
        {
            if (row >= Model.Rows || column >= Model.Columns || row < 0 || column < 0)
                return false;

            // Can't select a non existing entry
            if (items[row, column] == null)
                return false;

            if (isSelected)
                items[row, column].Select();
            else
                items[row, column].Unselect();

            return true;
        }

        public override void Setup(ContentManager content)
        {
            foreach (var i in items)
                i.Setup(content);

            base.Setup(content);
        }
        protected override void Update()
        {
            FillLayout();
        }

        private void FillLayout()
        {
            var container = InnerComponent;

            container.RemoveAllComponents();
            for (int i = 0; i < ViewportRows; i++)
            {
                for (int j = 0; j < ViewportColumns; j++)
                    container.AddComponent(items[startRow + i, startColumn + j]);
            }
        }

        private void InitItems()
        {
            for (int i = 0; i < Model.Rows; i++)
            {
                for (int j = 0; j < Model.Columns; j++)
                {
                    items[i, j] = renderer.CreateComponent(i, j, Model.DataAt(i, j));
                }
            }
        }

        private void model_DataChanged(object sender, DataChangedEventArgs<T> e)
        {
            items [e.Row, e.Column] = renderer.CreateComponent(e.Row, e.Column, Model.DataAt(e.Row, e.Column));
        }

        private void model_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int oldRows = items.GetLength(0);
            int oldColumnns = items.GetLength(1);

            if (e.NewRows == oldRows && e.NewColumns == oldColumnns)
                return;

            var newItems = new ISelectableGraphicComponent[e.NewRows, e.NewColumns];

            items.Copy(newItems, (row, column) => renderer.CreateComponent(row, column, default(T)));
            items = newItems;

            layout.Columns = e.NewColumns;
            layout.Rows = e.NewRows;
            
            OnTableResize(this, new TableResizeEventArgs(e.NewRows, e.NewColumns));
        }
    }

    public class SelectionEventArgs<T> : EventArgs
    {
        public T SelectedData { get; private set; }
        public SelectionEventArgs(T selectedData)
        {
            SelectedData = selectedData;
        }
    }
}