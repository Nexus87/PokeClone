using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using System;
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

    public class TableView<T> : AbstractGraphicComponent, ITableView<T>
    {
        ITableModel<T> model;
        ITableRenderer<T> renderer;
        ITableSelectionModel selectionModel;
        ITableGrid tableGrid;
        bool isSetup;

        public TableView(ITableModel<T> model, ITableRenderer<T> renderer, ITableSelectionModel selectionModel, PokeEngine game)
            : base(game)
        {
            renderer.CheckNull("renderer");
            model.CheckNull("model");
            game.CheckNull("game");
            selectionModel.CheckNull("selectionModel");

            //CreateGrid is only virtual for testing purpose.
#pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            tableGrid = CreateGrid(model.Rows, model.Columns);
#pragma warning restore RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            tableGrid.StartIndex = null;
            tableGrid.EndIndex = null;

            this.renderer = renderer;
            this.model = model;
            this.selectionModel = selectionModel;
        }

        public event EventHandler<TableResizeEventArgs> OnTableResize = delegate { };

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged
        {
            add { selectionModel.SelectionChanged += value; }
            remove { selectionModel.SelectionChanged -= value; }
        }

        public int Columns { get { return model.Columns; } }
        public int Rows { get { return model.Rows; } }

        public TableIndex? EndIndex
        {
            get { return tableGrid.EndIndex; }
            set { tableGrid.EndIndex = value; }
        }

        public TableIndex? StartIndex
        {
            get { return tableGrid.StartIndex; }
            set { tableGrid.StartIndex = value; }
        }

        void CellIsInBound(int row, int column)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException("row", "value is: " + row);
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("column", "value is: " + column);
        }

        public ITableModel<T> Model
        {
            get { return model; }
            set
            {
                value.CheckNull("value");
                SetModel(value);
            }
        }

        void SetModel(ITableModel<T> newModel)
        {
            if (isSetup)
            {
                UnsubscribeModelEvents(model);
                SubscribeToModelEvents(newModel);
            }

            model = newModel;
            ModelSizeChanged();
        }

        void ModelSizeChanged()
        {
            tableGrid.Rows = model.Rows;
            tableGrid.Columns = model.Columns;
            Invalidate();
        }

        void UnsubscribeModelEvents(ITableModel<T> tableModel)
        {
            tableModel.DataChanged -= DataChangedHandler;
            tableModel.SizeChanged -= SizeChangedHandler;
        }

        public bool SetCellSelection(int row, int column, bool isSelected)
        {
            CellIsInBound(row, column);

            if (isSelected)
                return selectionModel.SelectIndex(row, column);
            else
                return selectionModel.UnselectIndex(row, column);
        }

        public override void Setup()
        {
            SubscribeToModelEvents(model);
            SubscribeToSelectionEvents();

            FillTableGrid();

            isSetup = true;
        }

        void SubscribeToModelEvents(ITableModel<T> tableModel)
        {
            tableModel.DataChanged += DataChangedHandler;
            tableModel.SizeChanged += SizeChangedHandler;
        }

        void SubscribeToSelectionEvents()
        {
            selectionModel.SelectionChanged += SelectionChangedHandler;
        }

        void FillTableGrid()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    tableGrid.SetComponentAt(i, j, GetComponent(i, j));
            }
        }

        protected override void Update()
        {
            tableGrid.SetCoordinates(XPosition, YPosition, Width, Height);
            FillTableGrid();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            tableGrid.Draw(time, batch);
        }

        void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            var component = tableGrid.GetComponentAt(e.Row, e.Column);
            Debug.Assert(component != null);
            SetComponentSelection(component, e.IsSelected);
        }

        static void SetComponentSelection(ISelectableGraphicComponent component, bool isSelected)
        {
            if (isSelected)
                component.Select();
            else
                component.Unselect();
        }

        void DataChangedHandler(object sender, DataChangedEventArgs<T> e)
        {
            tableGrid.SetComponentAt(e.Row, e.Column, GetComponent(e.Row, e.Column, e.NewData));
        }

        void SizeChangedHandler(object sender, TableResizeEventArgs e)
        {
            ModelSizeChanged();
            OnTableResize(this, e);
        }

        ISelectableGraphicComponent GetComponent(int row, int column)
        {
            var data = model[row, column];
            return GetComponent(row, column, data);
        }

        ISelectableGraphicComponent GetComponent(int row, int column, T data)
        {
            bool isSelected = selectionModel.IsSelected(row, column);
            return renderer.GetComponent(row, column, data, isSelected);
        }

        internal virtual ITableGrid CreateGrid(int rows, int columns)
        {
            return new TableGrid(rows, columns, Game);
        }
    }
}