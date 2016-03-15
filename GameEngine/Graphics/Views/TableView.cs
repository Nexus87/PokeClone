using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Views
{
    public class SelectionEventArgs<T> : EventArgs
    {
        public SelectionEventArgs(T selectedData)
        {
            SelectedData = selectedData;
        }

        public T SelectedData { get; private set; }
    }

    public class TableView<T> : ForwardingGraphicComponent<Container>, ITableView
    {
        private int? columns;

        private GridLayout layout;

        private IItemModel<T> model;

        private ITableRenderer<T> renderer;

        private int? rows;

        private int startColumn = 0;

        private int startRow = 0;

        public TableView(int rows, int columns, IItemModel<T> model, PokeEngine game)
            : this(rows, columns, model, new DefaultTableRenderer<T>(game, DefaultCreator), game)
        { }

        public TableView(int rows, int columns, IItemModel<T> model, ITableRenderer<T> renderer, PokeEngine game)
            : base(new Container(game), game)
        {
            model.CheckNull("model");

            Rows = rows;
            Columns = columns;
            this.renderer = renderer;

            SetModel(model);
            layout = new GridLayout(Rows, Columns);
            InnerComponent.Layout = layout;
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize = delegate { };

        public int Columns
        {
            get
            {
                return columns.HasValue ? columns.Value : model.Columns;
            }
            set
            {
                columns = value;
            }
        }

        public IItemModel<T> Model
        {
            get { return model; }
            set
            {
                value.CheckNull("value");

                bool sizeChanged = value.Rows != model.Rows || value.Columns != model.Columns;

                SetModel(value);
                Invalidate();
            }
        }

        public int Rows
        {
            get
            {
                return rows.HasValue ? rows.Value : model.Rows;
            }
            set
            {
                rows = value;
            }
        }

        public int StartColumn
        {
            get { return startColumn; }
            set
            {
                if (value >= Columns || value < 0)
                    throw new ArgumentOutOfRangeException("value is out of bound");

                if (startColumn == value)
                    return;

                startColumn = value;
                Invalidate();
            }
        }

        public int StartRow
        {
            get { return startRow; }
            set
            {
                if (value >= Rows || value < 0)
                    throw new ArgumentOutOfRangeException("value is out of bound");

                if (startRow == value)
                    return;

                startRow = value;
                Invalidate();
            }
        }

        public void ResetColumns()
        {
            columns = null;
        }

        public void ResetRows()
        {
            rows = null;
        }

        public bool SetCellSelection(int row, int column, bool isSelected)
        {
            if (row >= Model.Rows || column >= Model.Columns || row < 0 || column < 0)
                return false;

            renderer.GetComponent(row, column, model.DataAt(row, column), isSelected);

            return true;
        }

        public override void Setup(ContentManager content)
        {
            base.Setup(content);
        }

        protected override void Update()
        {
            layout.Columns = Columns;
            layout.Rows = Rows;
            FillLayout();
        }

        private static XNASpriteFont DefaultCreator()
        {
            return new XNASpriteFont();
        }

        private void FillLayout()
        {
            var container = InnerComponent;
            container.RemoveAllComponents();

            for (int row = StartRow; row < Rows; row++)
            {
                for (int column = StartColumn; column < Columns; column++)
                    container.AddComponent(renderer.GetComponent(row, column, model[row, column], true));
            }
        }

        private void model_DataChanged(object sender, DataChangedEventArgs<T> e)
        {
            Invalidate();
        }

        private void model_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Invalidate();

            if (StartColumn >= Columns)
                StartColumn = Math.Max(0, Columns - 1);
            if (StartRow >= Rows)
                StartRow = Math.Max(0, Rows - 1);
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

            Invalidate();
        }
    }
}