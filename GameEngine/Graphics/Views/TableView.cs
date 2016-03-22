using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

    public class TableView<T> : ForwardingGraphicComponent<Container>, ITableView<T>
    {
        private ITableModel<T> model;
        private ITableRenderer<T> renderer;
        private ITableSelectionModel selectionModel;

        public TableView(ITableModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel, PokeEngine game)
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
            int row = e.Row;
            int column = e.Column;

            // The cell is currently not visible. So nothing needs to be done.
            if (row < StartRow || row > EndRow || column < StartColumn || column > EndColumn)
                return;

            var components = InnerComponent.Components;
            // FillLayout has not yet been called.
            if (components.Count == 0)
                return;

            int idx = CalculateIndex(row - StartRow, column - StartColumn, VisibleColumns);

            Debug.Assert(idx < components.Count);

            if (e.IsSelected)
                ((ISelectableGraphicComponent)components[idx]).Select();
            else
                ((ISelectableGraphicComponent)components[idx]).Unselect();
            
        }

        private int StartRow { get { return StartIndex.HasValue ? StartIndex.Value.Row : 0; } }

        private int StartColumn { get { return StartIndex.HasValue ? StartIndex.Value.Column : 0; } }

        private int EndRow { get { return EndIndex.HasValue ? EndIndex.Value.Row : Rows - 1; } }

        private int EndColumn { get { return EndIndex.HasValue ? EndIndex.Value.Column : Columns - 1; } }

        private int VisibleColumns { get { return EndColumn - StartColumn + 1; } }
        private int VisibleRows { get { return EndRow - StartRow + 1; } }

        private static int CalculateIndex(int row, int column, int columns)
        {
            return column + columns * row;
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize = delegate { };

        public int Columns { get { return model.Columns; } }

        public TableIndex? EndIndex { 
            get { return endIndex; }
            set
            {
                if (value == null)
                {
                    if (endIndex == null)
                        return;

                    endIndex = value;
                    Invalidate();
                    return;
                }

                TableIndex tmp = value.Value;
                CheckIndexBound(tmp);
                
                if (StartIndex != null)
                    CheckStartEndBounds(StartIndex.Value, tmp);

                bool needsInvalidation = endIndex == null ||
                    endIndex.Value.Row != tmp.Row ||
                    endIndex.Value.Column != tmp.Column;

                if (!needsInvalidation)
                    return;

                endIndex = value;
                Invalidate();

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
                    if (startIndex == null)
                        return;

                    startIndex = value;
                    Invalidate();
                    return;
                }

                TableIndex tmp = value.Value;

                CheckIndexBound(tmp);
                
                if (EndIndex != null)
                    CheckStartEndBounds(tmp, EndIndex.Value);

                bool needsInvalidation = startIndex == null ||
                    startIndex.Value.Row != tmp.Row ||
                    startIndex.Value.Column != tmp.Column;

                if (!needsInvalidation)
                    return;

                startIndex = value;
                Invalidate();
            }
        }

        private TableIndex? startIndex = null;
        private TableIndex? endIndex = null;
        private GridLayout layout;
        public ITableModel<T> Model
        {
            get { return model; }
            set
            {
                value.CheckNull("value");

                bool hasSizeChanged = model.Rows != value.Rows || model.Columns != value.Columns;
                SetModel(value);
                
                if(hasSizeChanged)
                    model_SizeChanged(null, new TableResizeEventArgs(Rows, Columns));
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


            layout.Rows = VisibleRows;
            layout.Columns = VisibleColumns;
            
            for (int row = StartRow; row <= EndRow; row++)
            {
                for (int column = StartColumn; column <= EndColumn; column++)
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

        private void model_SizeChanged(object sender, TableResizeEventArgs e)
        {
            Invalidate();

            // Check the start and end index
            if (StartIndex != null)
                startIndex = MoveIndexes(startIndex.Value, e.Rows, e.Columns);
            if (EndIndex != null)
                endIndex = MoveIndexes(endIndex.Value, e.Rows, e.Columns);

            OnTableResize(this, e);
        }

        private TableIndex MoveIndexes(TableIndex index, int rows, int columns)
        {
            var ret = new TableIndex(index.Row, index.Column);
            if (index.Row >= rows)
                ret.Row = rows - 1;
            if (index.Column >= columns)
                ret.Column = columns - 1;

            return ret;
        }

        private void SetModel(ITableModel<T> value)
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


        public event EventHandler<SelectionChangedEventArgs> SelectionChanged
        {
            add { selectionModel.SelectionChanged += value; }
            remove { selectionModel.SelectionChanged -= value; }
        }
    }
}