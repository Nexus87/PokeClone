using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Views
{
    public class InternalTableView<T, SpriteFontClass> : AbstractGraphicComponent, IItemView where SpriteFontClass : ISpriteFont, new()
    {
        private const int visibleColumns = 8;
        private const int visibleRows = 8;
        private ContentManager content;
        internal ItemBox[,] items;
        private TableLayout layout;

        private IItemModel<T> model;
        private int startColumn = 0;
        private int startRow = 0;

        public InternalTableView(IItemModel<T> model)
        {
            if (model == null)
                throw new ArgumentNullException("model must not be null");

            SetModel(model);

            layout = new TableLayout(ViewportRows, ViewportColumns);
            layout.Init(this);
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
                if (value == null)
                    throw new ArgumentNullException("null is not a valid value for Model");

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

            items = new ItemBox[model.Rows, model.Columns];
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
            if (row >= Model.Rows || column >= Model.Columns)
                return false;

            return items[row, column] != null && items[row, column].IsSelected;
        }

        public bool SetCellSelection(int row, int column, bool isSelected)
        {
            if (row >= Model.Rows || column >= Model.Columns)
                return false;

            // Can't select a non existing entry
            if (items[row, column] == null)
                return false;

            items[row, column].IsSelected = isSelected;
            return true;
        }

        public override void Setup(ContentManager content)
        {
            this.content = content;
            foreach (var item in items)
                if (item != null)
                    item.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        protected override void Update()
        {
            // At the moment there is no way to shrink the TableLayout
            if (layout.Rows > ViewportRows || layout.Columns > ViewportColumns)
            {
                layout.Resize(ViewportRows, ViewportColumns);
            }

            FillLayout();
        }

        private void FillLayout()
        {
            for (int i = 0; i < ViewportRows; i++)
            {
                for (int j = 0; j < ViewportColumns; j++)
                    layout.SetComponent(i, j, items[startRow + i, startColumn + j]);
            }
        }

        private void InitItems()
        {
            for (int i = 0; i < Model.Rows; i++)
            {
                for (int j = 0; j < Model.Columns; j++)
                {
                    var str = Model.DataStringAt(i, j);
                    if (str == null)
                        continue;

                    items[i, j] = new ItemBox(str, new SpriteFontClass());
                }
            }
        }

        private void model_DataChanged(object sender, DataChangedArgs<T> e)
        {
            if (items[e.row, e.column] == null)
            {
                var item = new ItemBox(new SpriteFontClass());
                if (content != null)
                    item.Setup(content);
                items[e.row, e.column] = item;
            }

            items[e.row, e.column].Text = Model.DataStringAt(e.row, e.column);
        }

        private void model_SizeChanged(object sender, SizeChangedArgs e)
        {
            if (e.newRows == items.GetLength(0) && e.newColumns == items.GetLength(1))
                return;

            var newItems = new ItemBox[e.newRows, e.newColumns];
            layout.Resize(ViewportRows, ViewportColumns);
            items.Copy(newItems);
            items = newItems;
            OnTableResize(this, new TableResizeEventArgs(e.newRows, e.newColumns));
        }
    }

    public class SelectionEventArgs<T> : EventArgs
    {
        public T SelectedData;
    }

    public class TableView<T> : InternalTableView<T, XNASpriteFont>
    {
        public TableView(IItemModel<T> model)
            : base(model)
        {
        }
    }
}