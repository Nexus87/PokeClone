using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
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
        private IItemModel<T> model;
        private ITableRenderer<T> renderer;
        private ITableSelectionModel selectionModel;

        public TableView(IItemModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel, PokeEngine game)
            : base(new Container(game), game)
        {
            renderer.CheckNull("renderer");
            model.CheckNull("model");
            game.CheckNull("game");
            selectionModel.CheckNull("selectionModel");

            this.renderer = renderer;
            SetSelectionModel(selectionModel);
            SetModel(model);
            layout =  new GridLayout(Rows, Columns);
            InnerComponent.Layout = layout;
        }

        private void SetSelectionModel(ITableSelectionModel model)
        {
            if (selectionModel != null)
                selectionModel.SelectionChanged -= SelectionChangedHandler;

            selectionModel = model;
            selectionModel.SelectionChanged += SelectionChangedHandler;
            Invalidate();
        }

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            Invalidate();
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize;

        public int Columns { get { return model.Columns; } }

        public TableIndex? EndIndex { 
            get { return endIndex; }
            set
            {
                if (value == null)
                {
                    endIndex = value;
                    return;
                }

                TableIndex tmp = value.Value;
                CheckIndexBound(tmp);
                
                if (StartIndex == null)
                {
                    endIndex = value;
                    return;
                }

                CheckStartEndBounds(StartIndex.Value, tmp);


                endIndex = value;

            }
        }

        private static void CheckStartEndBounds(TableIndex start, TableIndex end)
        {
            if (start.Row > end.Row || start.Column > end.Column)
                throw new ArgumentOutOfRangeException("EndIndex must be greater than StartIndex");
        }

        private void CheckIndexBound(TableIndex tmp)
        {
            if (tmp.Column >= Columns || tmp.Row >= Rows)
                throw new ArgumentOutOfRangeException("Index must be less than (Rows, Columns)");
            if (tmp.Column < 0 || tmp.Row < 0)
                throw new ArgumentOutOfRangeException("Index must be positive");
        }

        public int Rows { get { return model.Rows; } }

        public TableIndex? StartIndex { 
            get { return startIndex; }
            set
            {
                if (value == null)
                {
                    startIndex = value;
                    return;
                }

                TableIndex tmp = value.Value;

                CheckIndexBound(tmp);
                
                if (EndIndex == null)
                {
                    startIndex = value;
                    return;
                }

                CheckStartEndBounds(tmp, EndIndex.Value);

                startIndex = value;

            }
        }

        private TableIndex? startIndex = null;
        private TableIndex? endIndex = null;
        private GridLayout layout;
        private IItemModel<T> Model
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

        public void SetCellSelection(int row, int column, bool isSelected)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("Selection must be between row and column");

            if (isSelected)
                selectionModel.SelectIndex(row, column);
            else
                selectionModel.UnselectIndex(row, column);
        }

        public override void Setup(ContentManager content)
        {
            base.Setup(content);
        }

        protected override void Update()
        {
            FillLayout();
            layout.LayoutContainer(InnerComponent);
        }

        private void FillLayout()
        {
            var container = InnerComponent;
            container.RemoveAllComponents();

            int startRow = StartIndex.HasValue ? StartIndex.Value.Row : 0;
            int startColumn = StartIndex.HasValue ? StartIndex.Value.Column : 0;

            int endRow = EndIndex.HasValue ? EndIndex.Value.Row : Rows - 1;
            int endColumn = EndIndex.HasValue ? EndIndex.Value.Column : Columns - 1;

            int realRows = endRow - startRow + 1;
            int realColumns = endColumn - startColumn + 1;

            layout.Rows = realRows;
            layout.Columns = realColumns;
            
            for (int row = startRow; row <= endRow; row++)
            {
                for (int column = startColumn; column <= endColumn; column++)
                {
                    var data = model[row, column];
                    var selection = selectionModel.IsSelected(row, column);
                    container.AddComponent(renderer.GetComponent(row, column, data, selection));
                }
            }
        }

        private void model_DataChanged(object sender, DataChangedEventArgs<T> e)
        {
            Invalidate();
        }

        private void model_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Invalidate();
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